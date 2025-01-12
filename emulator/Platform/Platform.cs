using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SDL2;

namespace JustinCredible.PacEmu
{
    /**
     * The "platform" code which handles creating of the GUI window to show graphics,
     * play audio samples, and receive keyboard input. Implemented with the SDL2 library
     * via the SDL# wrapper.
     */
    partial class Platform : IDisposable
    {
        #region Instance Variables

        // References to SDL resources.
        private IntPtr _gameWindow = IntPtr.Zero;
        private IntPtr _gameRendererSurface = IntPtr.Zero;
        private uint _audioDevice = 0;
        private List<IntPtr> _controllers = new List<IntPtr>();
        private IntPtr _debugWindow = IntPtr.Zero;
        private IntPtr _debugRendererSurface = IntPtr.Zero;
        private bool _isWinRT = false; // UWP App

        // Used to throttle the GUI event loop so we don't fire the OnTick event
        // more than needed. During each tick we can send key presses as well as
        // receive a framebuffer.
        private int _targetTicksHz = 60;

        // Used to determine the location and size of the texture to render.
        private SDL.SDL_Rect _renderLocation = new SDL.SDL_Rect()
        {
            x = 0,
            y = 0,
            w = VideoHardware.RESOLUTION_WIDTH,
            h = VideoHardware.RESOLUTION_HEIGHT,
        };

        // Indicates if the toggle switch for the board's test mode is switched on or not.
        private bool _boardTestSwitchActive = false;

        // A flag that allows us to make a keypress behave as a toggle switch.
        private bool _allowChangeBoardTestSwitch = true;

        // Determines the audio sampling rate for playback on the target platform.
        // Pac-Man uses 96 kHz; if set to any other value downsampling will occur.
        // See QueueAudioSamples() for more details.
        // private const int AUDIO_SAMPLE_RATE = 96000;
        private const int AUDIO_SAMPLE_RATE = 44100;
        // private const int AUDIO_SAMPLE_RATE = 22050;
        // private const int AUDIO_SAMPLE_RATE = 11025;

        private bool _hasEmittedDownsamplingAlert = false;

        #endregion

        #region Events

        // Fired when the SDL event loop "ticks" which is ~60hz. This allows us to send out
        // the keys that were pressed, as well as optionally receive a framebuffer to be
        // rendered in the window or sound effects to be played.
        public delegate void TickEvent(GUITickEventArgs eventArgs);
        public event TickEvent OnTick;

        #endregion

        #region Public Methods

        /**
         * Used to initialize the GUI by creating a window using SDL. This window is where the framebuffer
         * will be rendered. This method must be called before any other methods.
         */
        public void Initialize(string title, int width = 640, int height = 480, float scaleX = 1, float scaleY = 1, int targetTicskHz = 60)
        {
            var initResult = SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO | SDL.SDL_INIT_GAMECONTROLLER);

            if (initResult != 0)
                throw new Exception(String.Format("Failure while initializing SDL. SDL Error: {0}", SDL.SDL_GetError()));

            _isWinRT = SDL.SDL_GetPlatform() == "WinRT";

            _gameWindow = SDL.SDL_CreateWindow(title,
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                (int)(width * scaleX),
                (int)(height * scaleY),
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
            );

            if (_gameWindow == IntPtr.Zero)
                throw new Exception(String.Format("Unable to create a window. SDL Error: {0}", SDL.SDL_GetError()));

            _gameRendererSurface = SDL.SDL_CreateRenderer(_gameWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED /*| SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC*/);

            if (_gameRendererSurface == IntPtr.Zero)
                throw new Exception(String.Format("Unable to create a renderer. SDL Error: {0}", SDL.SDL_GetError()));

            // We can scale the image up or down based on the scaling factor.
            SDL.SDL_RenderSetScale(_gameRendererSurface, scaleX, scaleY);

            // By setting the logical size we ensure that the image will scale to fit the window while
            // still maintaining the original aspect ratio.
            SDL.SDL_RenderSetLogicalSize(_gameRendererSurface, width, height);

            _targetTicksHz = targetTicskHz;

            // Setup our audio format.
            SDL.SDL_AudioSpec audioSpec = new SDL.SDL_AudioSpec();
            audioSpec.freq = AUDIO_SAMPLE_RATE; // sampling rate
            audioSpec.format = SDL.AUDIO_S8; // sample format: 8-bit, signed
            audioSpec.channels = 1; // number of channels
            audioSpec.samples = 4096; // buffer size

            SDL.SDL_AudioSpec audioSpecObtained;

            // Open the default audio device.
            _audioDevice = SDL.SDL_OpenAudioDevice(null, 0, ref audioSpec, out audioSpecObtained, 0);

            if (_audioDevice == 0)
                throw new Exception(String.Format("Unable to open the audio device. SDL Error: {0}", SDL.SDL_GetError()));

            // Unpause the audio device and so that it will play once samples are queued up.
            SDL.SDL_PauseAudioDevice(_audioDevice, 0);

            // Attempt to open controllers.

            var numJoysticks = SDL.SDL_NumJoysticks();

            for (var i = 0; i < numJoysticks; i++)
            {
                if (SDL.SDL_IsGameController(i) == SDL.SDL_bool.SDL_FALSE)
                    continue;

                var controller = SDL.SDL_GameControllerOpen(i);

                if (controller != IntPtr.Zero)
                    _controllers.Add(controller);
            }
        }

        /**
         * Used to start the GUI event loop using the SDL window to poll for events. When an event is
         * received, the keyboard state is read and the OnTick event is fired. After the event completes
         * a frame can be rendered based on the values set in the OnTick event args.
         * 
         * This is a BLOCKING call; the event loop will continue to be polled until (1) the user uses
         * the platform specific key combination or GUI action to close the window or (2) the OnTick
         * event arguments has ShouldQuit set to true.
         */
        public void StartLoop()
        {
            // Used to keep track of the time elapsed in each loop iteration. This is used to
            // notify the OnTick handlers so they can update their simulation, as well as throttle
            // the update loop to targetTicskHz if needed.
            var stopwatch = new Stopwatch();

            // For calculating how long the actual rendering of pixels take.
            // var renderStopwatch = new Stopwatch();

            // Structure used to pass data to and from the OnTick handlers. We initialize it once
            // outside of the loop to avoid eating a ton of memory putting GC into a tailspin.
            var tickEventArgs = new GUITickEventArgs();

            // The SDL event polled for in each iteration of the loop.
            SDL.SDL_Event sdlEvent;

            while (true)
            {
                stopwatch.Restart();

                tickEventArgs.KeyDown = null;
                tickEventArgs.ShouldBreak = false;
                tickEventArgs.ShouldUnPause = false;
                tickEventArgs.ShouldPause = false;

                while (SDL.SDL_PollEvent(out sdlEvent) != 0)
                {
                    switch (sdlEvent.type)
                    {
                        // e.g. Command + Q, ALT+F4, or clicking X...
                        case SDL.SDL_EventType.SDL_QUIT:
                            // Break out of the SDL event loop, which will close the program.
                            return;

                        case SDL.SDL_EventType.SDL_WINDOWEVENT:
                            {
                                if (_isWinRT)
                                {
                                    // On Xbox, most games keep running when focus is lost (e.g. the user pressed the home button
                                    // to show the overlay, but hasn't left the app yet). So instead of pausing then, we'll wait
                                    // until the app is hidden. This isn't great for UWP on Windows desktops, but the UWP app is
                                    // intended only for Xbox... Windows desktop users should be using the desktop app version.
                                    
                                    if (sdlEvent.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN)
                                        tickEventArgs.ShouldUnPause = true;

                                    if (sdlEvent.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN)
                                        tickEventArgs.ShouldPause = true;
                                }
                                else
                                {
                                    if (sdlEvent.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED)
                                        tickEventArgs.ShouldUnPause = true;

                                    if (sdlEvent.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST)
                                        tickEventArgs.ShouldPause = true;
                                }

                                if (sdlEvent.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE)
                                return; // Break out of the SDL event loop, which will close the program.

                                break;
                            }

                        case SDL.SDL_EventType.SDL_KEYDOWN:
                            tickEventArgs.KeyDown = sdlEvent.key.keysym.sym;
                            UpdateKeys(tickEventArgs, sdlEvent.key.keysym.sym, true);

                            // If the break/pause or F3 key is pressed, set a flag indicating the
                            // emulator's should activate the interactive debugger.
                            if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_PAUSE
                                || sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_F3)
                                tickEventArgs.ShouldBreak = true;

                            break;

                        case SDL.SDL_EventType.SDL_KEYUP:
                            UpdateKeys(tickEventArgs, sdlEvent.key.keysym.sym, false);
                            break;

                        case SDL.SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                            UpdateControllerButtons(tickEventArgs, sdlEvent.cbutton.which, sdlEvent.cbutton.button, true);
                            break;

                        case SDL.SDL_EventType.SDL_CONTROLLERBUTTONUP:
                            UpdateControllerButtons(tickEventArgs, sdlEvent.cbutton.which, sdlEvent.cbutton.button, false);
                            break;

                        case SDL.SDL_EventType.SDL_CONTROLLERAXISMOTION:
                            UpdateControllerAxis(tickEventArgs, sdlEvent.caxis.which, sdlEvent.caxis.axis, sdlEvent.caxis.axisValue);
                            break;
                    }

                    if (_debugRendererSurface != IntPtr.Zero)
                        HandleDebuggerEvent(sdlEvent);
                }

                // Update the state of the board test toggle switch.
                tickEventArgs.ButtonState.ServiceBoardTest = _boardTestSwitchActive;

                // Update the event arguments that will be sent with the event handler.
                tickEventArgs.ShouldRender = false;

                // Delegate out to the event handler so work can be done.
                if (OnTick != null)
                    OnTick(tickEventArgs);

                // We only want to re-render if the frame buffer has changed since last time because
                // the SDL_RenderPresent method is relatively expensive.
                if (tickEventArgs.ShouldRender && tickEventArgs.FrameBuffer != null)
                {
                    // renderStopwatch.Restart();
                    // Console.WriteLine("Starting Render!");

                    // Clear the background with black so that if the game is letterboxed or pillarboxed
                    // the background color of the unused space will be black.
                    SDL.SDL_SetRenderDrawColor(_gameRendererSurface, 0, 0, 0, 255);
                    SDL.SDL_RenderClear(_gameRendererSurface);

                    // In order to pass the managed memory bitmap to the unmanaged SDL methods, we need to
                    // manually allocate memory for the byte array. This effectively "pins" it so it won't
                    // be garbage collected. We need to be sure to release this memory after we render.
                    var frameBuffer = GCHandle.Alloc(tickEventArgs.FrameBuffer, GCHandleType.Pinned);
                    var frameBufferPointer = frameBuffer.AddrOfPinnedObject();

                    // Now that we have an unmanaged pointer to the bitmap, we can use the SDL methods to
                    // get an abstract stream interface which will allow us to load the bitmap as a texture.
                    var rwops = SDL.SDL_RWFromConstMem(frameBufferPointer, tickEventArgs.FrameBuffer.Length);
                    var surface = SDL.INTERNAL_SDL_LoadBMP_RW(rwops, 0);
                    var texture = SDL.SDL_CreateTextureFromSurface(_gameRendererSurface, surface);

                    // Now that we've loaded the framebuffer's bitmap as a texture, we can now render it to
                    // the SDL canvas at the given location.
                    SDL.SDL_RenderCopy(_gameRendererSurface, texture, ref _renderLocation, ref _renderLocation);

                    // Ensure we release our unmanaged memory.
                    SDL.SDL_DestroyTexture(texture);
                    SDL.SDL_FreeSurface(surface);
                    SDL.SDL_FreeRW(rwops);
                    frameBuffer.Free();

                    // Now that we're done rendering, we can present this new frame.
                    SDL.SDL_RenderPresent(_gameRendererSurface);

                    // renderStopwatch.Stop();
                    // Console.WriteLine("Render completed in: " + renderStopwatch.ElapsedMilliseconds + " ms");
                }

                // Re-render the debugger window; we only do this if it needs re-rendering because part of the
                // rendering can be expensive as it examines CPU state including registers and memory locations
                // in order to obtain the data needed to be shown in the window (no need to do this @ 60hz).
                if (_debugRendererSurface != IntPtr.Zero)
                {
                    lock (_debuggerRenderingLock)
                    {
                        if (_debuggerNeedsRendering)
                        {
                            DebugWindowRenderer.Render(_debugRendererSurface, _debuggerState, _debuggerInputString, _debuggerFileList, _debuggerPcb, _debuggerShowAnnotatedDisassembly);
                            _debuggerNeedsRendering = false;
                        }
                    }
                }

                // See if we need to delay to keep locked to ~ targetTicskHz.

                if (stopwatch.Elapsed.TotalMilliseconds < (1000 / _targetTicksHz))
                {
                    var delay = (1000 / _targetTicksHz) - stopwatch.Elapsed.TotalMilliseconds;
                    SDL.SDL_Delay((uint)delay);
                }

                // If the event handler indicated we should quit, then stop.
                if (tickEventArgs.ShouldQuit)
                    return;
            }
        }

        /**
         * Used to queue the given audio samples for playback. The parameter is expected
         * to be three 8-bit, signed values, one for each voice.
         */
        public void QueueAudioSamples(byte[][] samplesByChannel)
        {
            var sourceSamples = new sbyte[samplesByChannel.Length];

            for (int i = 0; i < sourceSamples.Length; i++)
            {
                var sampleChannel1 = samplesByChannel[i][0];
                var sampleChannel2 = samplesByChannel[i][1];
                var sampleChannel3 = samplesByChannel[i][2];

                // Merge all three voices into one.
                var sample = sampleChannel1 + sampleChannel2 + sampleChannel3;

                // Clamp the value to the min/max of a 8-bit signed value to avoid distortion.
                if (sample > 127)
                    sample = 127;
                else if (sample < -128)
                    sample = -128;

                sourceSamples[i] = (sbyte)sample;
            }

            var targetSamples = sourceSamples;

            var sourceFrameSize = sourceSamples.Length;
            var targetFrameSize = AUDIO_SAMPLE_RATE / 60;
            var shouldDownsample = sourceFrameSize != targetFrameSize;

            if (shouldDownsample)
            {
                if (!_hasEmittedDownsamplingAlert)
                {
                    _hasEmittedDownsamplingAlert = true;
                    Console.WriteLine($"Audio: Downsampling from {sourceFrameSize*60} hz to {targetFrameSize*60} hz.");
                }

                float factor = (float)sourceFrameSize / (float)targetFrameSize;

                var downsampledSamples = new sbyte[targetFrameSize];

                for (var i = 0; i < targetFrameSize; i++)
                {
                    var offset = (int)(factor * i);
                    downsampledSamples[i] = sourceSamples[offset];
                }

                targetSamples = downsampledSamples;
            }

            // Now that we have the combined sample, we need to allocate it as a pinned object
            // on the heap so that we can pass it through to the unmanaged SDL2 code.
            var samplesPinned = GCHandle.Alloc(targetSamples, GCHandleType.Pinned);
            var pointer = samplesPinned.AddrOfPinnedObject();

            // Pass the value to SDL to be queued up for playback.
            uint sample_size = (uint)(sizeof(sbyte) * 1 * targetSamples.Length);
            SDL.SDL_QueueAudio(_audioDevice, pointer, sample_size);

            // Unpin this so the GC can clean it up.
            samplesPinned.Free();
        }

        /**
         * Used to cleanup after the SDL resources.
         */
        public void Dispose()
        {
            if (_gameRendererSurface != IntPtr.Zero)
                SDL.SDL_DestroyRenderer(_gameRendererSurface);

            if (_gameWindow != IntPtr.Zero)
                SDL.SDL_DestroyWindow(_gameWindow);

            SDL.SDL_CloseAudioDevice(_audioDevice);

            foreach (var controller in _controllers)
                SDL.SDL_GameControllerClose(controller);
        }

        #endregion

        #region Private Methods

        private void UpdateKeys(GUITickEventArgs tickEventArgs, SDL.SDL_Keycode keycode, bool isDown)
        {
            switch (keycode)
            {
                // Player 1
                case SDL.SDL_Keycode.SDLK_LEFT:
                    tickEventArgs.ButtonState.P1Left = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_RIGHT:
                    tickEventArgs.ButtonState.P1Right = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_UP:
                    tickEventArgs.ButtonState.P1Up = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_DOWN:
                    tickEventArgs.ButtonState.P1Down= isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_1:
                    tickEventArgs.ButtonState.P1Start = isDown;
                    break;

                // Player 2
                case SDL.SDL_Keycode.SDLK_a:
                    tickEventArgs.ButtonState.P2Left = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_d:
                    tickEventArgs.ButtonState.P2Right = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_w:
                    tickEventArgs.ButtonState.P2Up = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_s:
                    tickEventArgs.ButtonState.P2Down = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_2:
                    tickEventArgs.ButtonState.P2Start = isDown;
                    break;

                // Coins
                case SDL.SDL_Keycode.SDLK_5:
                    tickEventArgs.ButtonState.CoinChute1 = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_6:
                    tickEventArgs.ButtonState.CoinChute2 = isDown;
                    break;

                // Servicing
                case SDL.SDL_Keycode.SDLK_3:
                    tickEventArgs.ButtonState.ServiceCredit = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_7:
                    tickEventArgs.ButtonState.ServiceRackAdvance = isDown;
                    break;
                case SDL.SDL_Keycode.SDLK_8:
                {
                    // The board test switch is generally a toggle switch because it must remain closed
                    // for the game program to remain in the test mode. Here we have some extra logic
                    // to make it so you don't have to hold down the button to stay in test mode. This
                    // makes it work like a toggle switch.

                    // If the key is pressed and we are allowing it to change, then flip its state.
                    if (isDown && _allowChangeBoardTestSwitch)
                    {
                        _boardTestSwitchActive = !_boardTestSwitchActive;

                        // Once we've flipped the state, don't allow changing it again until the key
                        // is released and pressed again.
                        _allowChangeBoardTestSwitch = false;
                    }

                    // Once the key is released, allow the pressing of the key to toggle the flag again
                    // next time the key is pressed.
                    if (!isDown)
                        _allowChangeBoardTestSwitch = true;

                    break;
                }
            }
        }

        private void UpdateControllerButtons(GUITickEventArgs tickEventArgs, int joystickId, byte buttonRaw, bool isDown)
        {
            var button = (SDL.SDL_GameControllerButton)buttonRaw;

            switch (button)
            {
                // Player 1 & 2
                case SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT:
                    tickEventArgs.ButtonState.P1Left = isDown;
                    tickEventArgs.ButtonState.P2Left = isDown;
                    break;
                case SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT:
                    tickEventArgs.ButtonState.P1Right = isDown;
                    tickEventArgs.ButtonState.P2Right = isDown;
                    break;
                case SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP:
                    tickEventArgs.ButtonState.P1Up = isDown;
                    tickEventArgs.ButtonState.P2Up = isDown;
                    break;
                case SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN:
                    tickEventArgs.ButtonState.P1Down = isDown;
                    tickEventArgs.ButtonState.P2Down = isDown;
                    break;

                // Player 1/2 Start, Coin
                case SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A:
                    tickEventArgs.ButtonState.P1Start = isDown;
                    break;
                case SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X:
                    tickEventArgs.ButtonState.P2Start = isDown;
                    break;
                case SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y:
                    tickEventArgs.ButtonState.CoinChute1 = isDown;
                    break;
            }
        }

        private void UpdateControllerAxis(GUITickEventArgs tickEventArgs, int joystickId, byte axisRaw, short axisValue)
        {
            var axis = (SDL.SDL_GameControllerAxis)axisRaw;

            switch (axis)
            {
                case SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY:
                    var isUp = axisValue < -20000;
                    var isDown = axisValue > 20000;
                    tickEventArgs.ButtonState.P1Up = isUp;
                    tickEventArgs.ButtonState.P2Up = isUp;
                    tickEventArgs.ButtonState.P1Down = isDown;
                    tickEventArgs.ButtonState.P2Down = isDown;
                    break;
                case SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX:
                    var isRight = axisValue > 20000;
                    var isLeft = axisValue < -20000;
                    tickEventArgs.ButtonState.P1Right = isRight;
                    tickEventArgs.ButtonState.P2Right = isRight;
                    tickEventArgs.ButtonState.P1Left = isLeft;
                    tickEventArgs.ButtonState.P2Left = isLeft;
                    break;
            }
        }

        #endregion
    }
}
