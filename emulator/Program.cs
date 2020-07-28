using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;

namespace JustinCredible.PacEmu
{
    /**
     * The main entry point into the emulator which handles parsing CLI arguments
     * as well as instantiating and configuring the GUI and PacManPCB classes.
     */
    class Program
    {
        private static CommandLineApplication _app;

        // Used to interact with the device we're running on (e.g. graphics, audio, input).
        private static Platform _platform;

        // The arcade printed circuit board for Pac-Man... the emulator!
        private static PacManPCB _game;

        // Used to pass data from the emulator thread's loop to the GUI loop: the
        // framebuffer to be rendered with flag indicating if a frame should be rendered
        // or not on the next GUI event loop tick (to avoid rendering/playing the same
        // frame multiple times).
        private static byte[] _frameBuffer; // Bitmap File Format
        private static bool _renderFrameNextTick = false;

        #region CLI / Entrypoint

        public static void Main(string[] args)
        {
            var version = Utilities.AppVersion;

            _app = new CommandLineApplication();
            _app.Name = "pacemu";
            _app.Description = "Pac-Man Arcade Hardware Emulator";
            _app.HelpOption("-?|-h|--help");

            _app.VersionOption("-v|--version",

                // Used for HelpOption() header
                $"{_app.Name} {version}",

                // Used for output of --version option.
                version
            );

            // When launched without any commands or options.
            _app.OnExecute(() =>
            {
                _app.ShowHelp();
                return 0;
            });

            _app.Command("run", Run);

            _app.Execute(args);
        }

        private static void Run(CommandLineApplication command)
        {
            command.Description = "Runs the emulator using the given ROM files.";
            command.HelpOption("-?|-h|--help");

            var romPathArg = command.Argument("[ROM path]", "The path to a directory containing the ROM set to load.");

            var romsetOption = command.Option("-rs|--rom-set", "The name of an alternative ROM set and/or PCB configuration to use; pacman or mspacman; defaults to pacman", CommandOptionType.SingleValue);
            var dipSwitchesOption = command.Option("-dw|--dip-switches", "The path to a JSON file containing DIP switch settings; defaults to dip-switches.json in CWD.", CommandOptionType.SingleValue);
            var loadStateOption = command.Option("-l|--load-state", "Loads an emulator save state from the given path.", CommandOptionType.SingleValue);
            var skipChecksumsOption = command.Option("-sc|--skip-checksums", "Allow running a ROM with invalid checksums.", CommandOptionType.NoValue);
            var writableRomOption = command.Option("-wr|--writable-rom", "Allow memory writes to the ROM address space.", CommandOptionType.NoValue);
            var debugOption = command.Option("-d|--debug", "Run in debug mode; enables internal statistics and logs useful when debugging.", CommandOptionType.NoValue);
            var breakOption = command.Option("-b|--break", "Used with debug, will break at the given address and allow single stepping opcode execution (e.g. --break 0x0248)", CommandOptionType.MultipleValue);
            var rewindOption = command.Option("-r|--rewind", "Used with debug, allows for single stepping in reverse to rewind opcode execution.", CommandOptionType.NoValue);
            var annotationsPathOption = command.Option("-a|--annotations", "Used with debug, a path to a text file containing memory address annotations for interactive debugging (line format: 0x1234 .... ; Annotation)", CommandOptionType.SingleValue);

            command.OnExecute(() =>
            {
                if (String.IsNullOrWhiteSpace(romPathArg.Value))
                    throw new Exception("A directory containing Pac-Man arcade hardware compatible ROM files is required.");

                if (!Directory.Exists(romPathArg.Value))
                    throw new Exception($"Could not locate a directory at path {romPathArg.Value}");

                // Determine which ROM set we need to load.

                var romset = ROMSet.PacMan;

                if (romsetOption.HasValue())
                {
                    if (romsetOption.Value() == "pacman")
                        romset = ROMSet.PacMan;
                    else if (romsetOption.Value() == "mspacman")
                        romset = ROMSet.MsPacMan;
                    else
                        throw new ArgumentException($"Unexpected ROM set: {romsetOption.Value()}");
                }

                // Load and validate all of the ROM files needed.
                var enforceValidChecksums = !skipChecksumsOption.HasValue();
                var romData = ROMLoader.LoadFromDisk(romset, romPathArg.Value, enforceValidChecksums);

                // Name the current thread so we can distinguish between the emulator's
                // CPU thread when using a debugger.
                Thread.CurrentThread.Name = "Platform (SDL)";

                // Initialize the platform wrapper which allows us to interact with
                // the platform's graphics, audio, and input devices. Wire an event
                // handler that will handle receiving user input as well as sending
                // the framebuffer to be rendered.
                _platform = new Platform();
                _platform.OnTick += Platform_OnTick;
                _platform.Initialize("Pac-Man Arcade Hardware Emulator", VideoHardware.RESOLUTION_WIDTH, VideoHardware.RESOLUTION_HEIGHT, 2, 2);

                // Initialize the Pac-Man arcade hardware/emulator and wire event
                // handlers to receive the framebuffer/samples to be rendered/played.
                _game = new PacManPCB();
                _game.ROMSet = romset;
                _game.AllowWritableROM = writableRomOption.HasValue();
                _game.OnRender += PacManPCB_OnRender;
                _game.OnAudioSample += PacManPCB_OnAudioSample;

                #region Set Game Options

                // Use the default values for the hardware DIP switches.
                var dipSwitchState = new DIPSwitches();

                // Look in the current working directory for a settings file.
                var dipSwitchesPath = "dip-switches.json";

                // If the user specified a path for the settings file, use it instead.
                if (dipSwitchesOption.HasValue())
                    dipSwitchesPath = dipSwitchesOption.Value();

                if (File.Exists(dipSwitchesPath))
                {
                    // We found a settings file! Parse and load it.
                    var json = File.ReadAllText(dipSwitchesPath);

                    var serializerOptions = new JsonSerializerOptions()
                    {
                        ReadCommentHandling = JsonCommentHandling.Skip,
                    };

                    try
                    {
                        dipSwitchState = JsonSerializer.Deserialize<DIPSwitches>(json, serializerOptions);
                    }
                    catch (Exception parseException)
                    {
                        throw new Exception($"Error parsing DIP switch settings JSON file at: {dipSwitchesPath}", parseException);
                    }
                }
                else
                {
                    // If the file doesn't exist and the user specified the path, fail fast.
                    // There must have been a typo or something. In any case the user should fix it.
                    if (dipSwitchesOption.HasValue())
                        throw new ArgumentException($"Unable to locate a DIP switch settings JSON file at: {dipSwitchesPath}");
                }

                _game.DIPSwitchState = dipSwitchState;

                #endregion

                #region Load State

                // If the path to a save state was specified to be loaded, deserialize
                // it from disk and ensure it gets passed into the emulator on start.

                EmulatorState state = null;

                if (loadStateOption.HasValue())
                {
                    var json = File.ReadAllText(loadStateOption.Value());
                    state = JsonSerializer.Deserialize<EmulatorState>(json);
                }

                #endregion

                #region Set Debugging Flags

                if (debugOption.HasValue())
                {
                    _game.Debug = true;

                    if (breakOption.HasValue())
                    {
                        var addresses = new List<UInt16>();

                        foreach (var addressString in breakOption.Values)
                        {
                            UInt16 address = Convert.ToUInt16(addressString, 16);
                            addresses.Add(address);
                        }

                        _game.BreakAtAddresses = addresses;
                    }

                    if (rewindOption.HasValue())
                        _game.RewindEnabled = true;

                    if (annotationsPathOption.HasValue())
                    {
                        var annotationsFilePath = annotationsPathOption.Value();

                        if (!File.Exists(annotationsFilePath))
                            throw new Exception($"Could not locate an annotations file at path {annotationsFilePath}");

                        try
                        {
                            var annotations = ParseAnnotationsFile(annotationsFilePath);
                            _game.Annotations = annotations;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Error parsing annotations file.", ex);
                        }
                    }
                }

                #endregion

                // Start the emulation; this occurs in a seperate thread and
                // therefore this call is non-blocking.
                _game.Start(romData, state);

                // Starts the event loop for the user interface; this occurs on
                // the same thread and is a blocking call. Once this method returns
                // we know that the user closed the window or quit the program via
                // the OS (e.g. ALT+F4 / CMD+Q).
                _platform.StartLoop();

                // Ensure the platform resources are cleaned up and stop the emulation.
                _platform.Dispose();
                _game.Stop();

                return 0;
            });
        }

        #endregion

        #region CLI Helpers

        private static Dictionary<UInt16, String> ParseAnnotationsFile(string path)
        {
            var annotations = new Dictionary<UInt16, String>();

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (line.Length < 5)
                    continue;

                var addressRegEx = new Regex("^[0-9A-F]{4}$");

                try
                {
                    if (line.Length < 4)
                        continue;

                    var addressString = line.Substring(0, 4).ToUpper();

                    if (!addressRegEx.IsMatch(addressString))
                        continue;

                    var address = Convert.ToUInt16(addressString, 16);

                    var parts = line.Split(";");

                    if (parts.Length < 2)
                        continue;

                    annotations.Add(address, parts[1]);
                }
                catch
                {
                    // Do nothing; the annotation file can vary wildly with new lines,
                    // lines that are just comments, labels, etc. We only care about a
                    // parsable memory address and if that line has a comment at the end.
                }
            }

            return annotations;
        }

        #endregion

        #region Glue Methods - Connects the emulator and platform threads

        /**
         * Fired when the emulator has a full frame to be rendered.
         * This should occur at approximately 60hz.
         */
        private static void PacManPCB_OnRender(RenderEventArgs eventArgs)
        {
            _frameBuffer = eventArgs.FrameBuffer;
            _renderFrameNextTick = true;
        }

        /**
         * Fired when the emulator has audio samples to be played.
         * Should occur at approximately 60hz.
         */
        private static void PacManPCB_OnAudioSample(AudioSampleEventArgs eventArgs)
        {
            foreach (var sample in eventArgs.Samples)
                _platform.QueueAudioSamples(sample);
        }

        /**
         * Fired when the GUI event loop "ticks". This provides an opportunity
         * to receive user input as well as send the framebuffer to be rendered.
         */
        private static void Platform_OnTick(GUITickEventArgs eventArgs)
        {
            // Receive user input.
            _game.ButtonState.SetState(eventArgs.ButtonState);

            // If the PAUSE key was pressed (e.g. CTRL/CMD+BREAK), invoke the
            // interactive debugger.
            if (_game.Debug && eventArgs.ShouldBreak)
                _game.Break();

            if (_renderFrameNextTick)
            {
                eventArgs.FrameBuffer = _frameBuffer;
                eventArgs.ShouldRender = true;
                _renderFrameNextTick = false;
            }
        }

        #endregion
    }
}
