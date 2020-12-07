using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace JustinCredible.PacEmu
{
    /**
     * The main entry point into the emulator which handles emulator configuration
     * and instantiating and configuring the GUI and PacManPCB classes.
     */
    public class Emulator
    {
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

        // The scaling factor for the windows.
        private const float GAME_WINDOW_SCALE_X = 2;
        private const float GAME_WINDOW_SCALE_Y = 2;
        private const float DEBUG_WINDOW_SCALE_X = 2f;
        private const float DEBUG_WINDOW_SCALE_Y = 2f;

        #region Entrypoint

        public static void Start(EmulatorConfig config)
        {
            if (String.IsNullOrWhiteSpace(config.RomPath))
                throw new Exception("A directory containing Pac-Man arcade hardware compatible ROM files is required.");

            if (!Directory.Exists(config.RomPath))
                throw new Exception($"Could not locate a directory at path {config.RomPath}");

            // Load and validate all of the ROM files needed.
            var enforceValidChecksums = !config.SkipChecksums;
            var romData = ROMLoader.LoadFromDisk(config.RomSet, config.RomPath, enforceValidChecksums);

            // Name the current thread so we can distinguish between the emulator's
            // CPU thread when using a debugger.
            Thread.CurrentThread.Name = "Platform (SDL)";

            // Initialize the platform wrapper which allows us to interact with
            // the platform's graphics, audio, and input devices. Wire an event
            // handler that will handle receiving user input as well as sending
            // the framebuffer to be rendered.
            _platform = new Platform();
            _platform.OnTick += Platform_OnTick;
            _platform.OnDebugCommand += Platform_OnDebugCommand;
            _platform.Initialize("Pac-Man Arcade Hardware Emulator", VideoHardware.RESOLUTION_WIDTH, VideoHardware.RESOLUTION_HEIGHT, GAME_WINDOW_SCALE_X, GAME_WINDOW_SCALE_Y);

            // Initialize the Pac-Man arcade hardware/emulator and wire event
            // handlers to receive the framebuffer/samples to be rendered/played.
            _game = new PacManPCB();
            _game.ROMSet = config.RomSet;
            _game.AllowWritableROM = config.WritableRom;
            _game.OnRender += PacManPCB_OnRender;
            _game.OnAudioSample += PacManPCB_OnAudioSample;
            _game.OnBreakpointHitEvent += PacManPCB_OnBreakpointHit;

            #region Set Game Options

            // Use the default values for the hardware DIP switches.
            var dipSwitchState = new DIPSwitches();

            // Look in the current working directory for a settings file.
            var dipSwitchesPath = "dip-switches.json";

            // If the user specified a path for the settings file, use it instead.
            if (!String.IsNullOrWhiteSpace(config.DipSwitchesConfigPath))
                dipSwitchesPath = config.DipSwitchesConfigPath;

            if (File.Exists(dipSwitchesPath))
            {
                // We found a settings file! Parse and load it.
                var json = File.ReadAllText(dipSwitchesPath);

                try
                {
                    dipSwitchState = JsonConvert.DeserializeObject<DIPSwitches>(json);
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
                if (!String.IsNullOrWhiteSpace(config.DipSwitchesConfigPath))
                    throw new ArgumentException($"Unable to locate a DIP switch settings JSON file at: {dipSwitchesPath}");
            }

            _game.DIPSwitchState = dipSwitchState;

            #endregion

            #region Load State

            // If the path to a save state was specified to be loaded, deserialize
            // it from disk and ensure it gets passed into the emulator on start.

            EmulatorState state = null;

            if (!String.IsNullOrWhiteSpace(config.LoadStateFilePath))
            {
                var json = File.ReadAllText(config.LoadStateFilePath);
                state = JsonConvert.DeserializeObject<EmulatorState>(json);
            }

            #endregion

            #region Set Debugging Flags

            if (config.Debug)
            {
                _game.Debug = true;
                _platform.InitializeDebugger(DEBUG_WINDOW_SCALE_X, DEBUG_WINDOW_SCALE_Y);

                if (config.Breakpoints != null)
                    _game.BreakAtAddresses = config.Breakpoints;

                if (config.ReverseStep)
                    _game.ReverseStepEnabled = true;

                if (!String.IsNullOrWhiteSpace(config.AnnotationsFilePath))
                {
                    if (!File.Exists(config.AnnotationsFilePath))
                        throw new Exception($"Could not locate an annotations file at path {config.AnnotationsFilePath}");

                    try
                    {
                        var annotations = Annotations.ParseFile(config.AnnotationsFilePath);
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

        private static void PacManPCB_OnBreakpointHit()
        {
            if (!_game.Debug)
                return;

            _platform.StartInteractiveDebugger(_game);
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

            if (eventArgs.ShouldPause)
                _game.Pause();

            if (eventArgs.ShouldUnPause)
                _game.UnPause();

            if (_renderFrameNextTick)
            {
                eventArgs.FrameBuffer = _frameBuffer;
                eventArgs.ShouldRender = true;
                _renderFrameNextTick = false;
            }
        }

        /**
         * Fired when the interactive debugger GUI receives a keypress indicating a user
         * command to be processed (continue, single step, etc).
         */
        private static void Platform_OnDebugCommand(DebugCommandEventArgs eventArgs)
        {
            if (!_game.Debug)
                return;

            switch (eventArgs.Action)
            {
                case DebugAction.ResumeContinue:
                    _game.Continue(singleStep: false);
                    break;

                case DebugAction.ResumeStep:
                    _game.Continue(singleStep: true);
                    break;

                case DebugAction.ReverseStep:
                    _game.ReverseStep();
                    break;

                case DebugAction.AddBreakpoint:
                    if (!_game.BreakAtAddresses.Contains(eventArgs.Address))
                        _game.BreakAtAddresses.Add(eventArgs.Address);
                    break;

                case DebugAction.RemoveBreakpoint:
                    if (_game.BreakAtAddresses.Contains(eventArgs.Address))
                        _game.BreakAtAddresses.Remove(eventArgs.Address);
                    break;

                case DebugAction.SaveState:
                {
                    var state = _game.SaveState();
                    var json = JsonConvert.SerializeObject(state);
                    File.WriteAllText(eventArgs.FileName, json);
                    break;
                }

                case DebugAction.LoadState:
                {
                    var json = File.ReadAllText(eventArgs.FileName);
                    var state = JsonConvert.DeserializeObject<EmulatorState>(json);
                    _game.LoadState(state);
                    break;
                }
            }
        }

        #endregion
    }
}
