using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace JustinCredible.PacEmu
{
    /**
     * The main entry point into the emulator which handles parsing CLI arguments
     * as well as instantiating and configuring the GUI and PacManPCB classes.
     */
    class Program
    {
        private static CommandLineApplication _app;

        // The arcade printed circuit board for Pac-Man... the emulator!
        private static PacManPCB _game;

        // Used to pass data from the emulator thread's loop to the GUI loop: the
        // framebuffer to be rendered and sounds effects to be played with matching
        // flags indicating if a frame/sfx should be rendered/played or not on the
        // next GUI event loop tick (to avoid rendering the same frame multiple times).
        private static Image<Rgba32> _frameBuffer;
        private static bool _renderFrameNextTick = false;
        // private static List<SoundEffect> _soundEffects = new List<SoundEffect>();
        private static bool _playSoundNextTick = false;

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

            var romPathArg = command.Argument("[ROM path]", "The path to a directory containing the Pac-Man ROM set to load.");

            // var sfxOption = command.Option("-sfx|--sound-effects", "The path to a directory containing the WAV sound effects to be used.", CommandOptionType.SingleValue);
            // var shipsOption = command.Option("-ss|--starting-ships", "Specify the number of ships the player starts with; 3 (default), 4, 5, or 6.", CommandOptionType.SingleValue);
            // var extraShipOption = command.Option("-es|--extra-ship", "Specify the number points needed to get an extra ship; 1000 (default) or 1500.", CommandOptionType.SingleValue);
            var loadStateOption = command.Option("-l|--load-state", "Loads an emulator save state from the given path.", CommandOptionType.SingleValue);
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

                var romData = ROMLoader.LoadFromDisk(romPathArg.Value);
                // var sfx = sfxOption.HasValue() ? GetSoundEffects(sfxOption.Value()) : null;

                Thread.CurrentThread.Name = "GUI Loop";

                // Initialize the user interface (window) and wire an event handler
                // that will handle receiving user input as well as sending the
                // framebuffer to be rendered. Note that we pass the width/height
                // parameters backwards on purpose because the CRT screen in the
                // cabinet is rotated to the left (-90 degrees) for a 3:4 display.
                var gui = new GUI();
                gui.OnTick += GUI_OnTick;
                gui.Initialize("Pac-Man Arcade Hardware Emulator", VideoHardware.RESOLUTION_HEIGHT, VideoHardware.RESOLUTION_WIDTH, 2, 2);

                // Initialize sound effects if the sfx option was passed.
                // if (sfx != null)
                //     gui.InitializeAudio(sfx);

                // Initialize the Pac-Man arcade hardware/emulator and wire event
                // handlers to receive the framebuffer/sfx to be rendered/played.
                _game = new PacManPCB();
                _game.OnRender += PacManPCB_OnRender;
                // _game.OnSound += PacManPCB_OnSound;

                #region Set Game Options

                // if (shipsOption.HasValue())
                // {
                //     switch (shipsOption.Value())
                //     {
                //         case "6":
                //             _game.StartingShips = StartingShipsSetting.Six;
                //             break;
                //         case "5":
                //             _game.StartingShips = StartingShipsSetting.Five;
                //             break;
                //         case "4":
                //             _game.StartingShips = StartingShipsSetting.Four;
                //             break;
                //         case "3":
                //             _game.StartingShips = StartingShipsSetting.Three;
                //             break;
                //         default:
                //             throw new ArgumentException("Invaild value specified via --starting-ships command line option.");
                //     }
                // }

                // if (extraShipOption.HasValue())
                // {
                //     switch (extraShipOption.Value())
                //     {
                //         case "1000":
                //             _game.ExtraShipAt = ExtraShipAtSetting.Points1000;
                //             break;
                //         case "1500":
                //             _game.ExtraShipAt = ExtraShipAtSetting.Points1500;
                //             break;
                //         default:
                //             throw new ArgumentException("Invaild value specified via --extra-ship command line option.");
                //     }
                // }

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
                gui.StartLoop();

                // Ensure the GUI resources are cleaned up and stop the emulation.
                gui.Dispose();
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
                    var addressString = line.Substring(0, 4);

                    if (!addressRegEx.IsMatch(addressString))
                        continue;

                    var address = Convert.ToUInt16(addressString, 16);

                    var parts = line.Split(";");

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

        #region Glue Methods - Connects the emulator and SDL/renderer threads

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
         * Fired when the emulator needs to play a sound.
         */
        // private static void PacManPCB_OnSound(SoundEventArgs eventArgs)
        // {
        //     _soundEffects.Add(eventArgs.SoundEffect);
        //     _playSoundNextTick = true;
        // }

        /**
         * Fired when the GUI event loop "ticks". This provides an opportunity
         * to receive user input as well as send the framebuffer to be rendered.
         */
        private static void GUI_OnTick(GUITickEventArgs eventArgs)
        {
            _game.ButtonP1Left = eventArgs.ButtonP1Left;
            _game.ButtonP1Right = eventArgs.ButtonP1Right;
            _game.ButtonP1Up = eventArgs.ButtonP1Up;
            _game.ButtonP1Down = eventArgs.ButtonP1Down;
            _game.ButtonP2Left = eventArgs.ButtonP2Left;
            _game.ButtonP2Right = eventArgs.ButtonP2Right;
            _game.ButtonP2Up = eventArgs.ButtonP2Up;
            _game.ButtonP2Down = eventArgs.ButtonP2Down;
            _game.ButtonStart1P = eventArgs.ButtonStart1P;
            _game.ButtonStart2P = eventArgs.ButtonStart2P;
            _game.ButtonCredit = eventArgs.ButtonCredit;

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

            if (_playSoundNextTick)
            {
                // eventArgs.ShouldPlaySounds = true;
                // eventArgs.SoundEffects.AddRange(_soundEffects);
                // _soundEffects.Clear();
                _playSoundNextTick = false;
            }
        }

        #endregion
    }
}
