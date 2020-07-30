using System;
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
    class Platform : IDisposable
    {
        #region Instance Variables

        // References to SDL resources.
        private IntPtr _window = IntPtr.Zero;
        private IntPtr _renderer = IntPtr.Zero;
        private uint _audioDevice = 0;

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
            var initResult = SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO);

            if (initResult != 0)
                throw new Exception(String.Format("Failure while initializing SDL. SDL Error: {0}", SDL.SDL_GetError()));

            _window = SDL.SDL_CreateWindow(title,
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                (int)(width * scaleX),
                (int)(height * scaleY),
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
            );

            if (_window == IntPtr.Zero)
                throw new Exception(String.Format("Unable to create a window. SDL Error: {0}", SDL.SDL_GetError()));

            _renderer = SDL.SDL_CreateRenderer(_window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED /*| SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC*/);

            if (_renderer == IntPtr.Zero)
                throw new Exception(String.Format("Unable to create a renderer. SDL Error: {0}", SDL.SDL_GetError()));

            // We can scale the image up or down based on the scaling factor.
            SDL.SDL_RenderSetScale(_renderer, scaleX, scaleY);

            // By setting the logical size we ensure that the image will scale to fit the window while
            // still maintaining the original aspect ratio.
            SDL.SDL_RenderSetLogicalSize(_renderer, width, height);

            _targetTicksHz = targetTicskHz;

            // Setup our audio format.
            SDL.SDL_AudioSpec audioSpec = new SDL.SDL_AudioSpec();
            audioSpec.freq = 96000; // sampling rate
            // audioSpec.freq = 44100; // sampling rate
            // audioSpec.freq = 22050; // sampling rate
            // audioSpec.freq = 11025; // sampling rate
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

                while (SDL.SDL_PollEvent(out sdlEvent) != 0)
                {
                    switch (sdlEvent.type)
                    {
                        // e.g. Command + Q, ALT+F4, or clicking X...
                        case SDL.SDL_EventType.SDL_QUIT:
                            // Break out of the SDL event loop, which will close the program.
                            return;

                        case SDL.SDL_EventType.SDL_KEYDOWN:
                            tickEventArgs.KeyDown = sdlEvent.key.keysym.sym;
                            UpdateKeys(tickEventArgs, sdlEvent.key.keysym.sym, true);

                            // If the break/pause or 9 key is pressed, set a flag indicating the
                            // emulator's should activate the interactive debugger.
                            if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_PAUSE
                                || sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_9)
                                tickEventArgs.ShouldBreak = true;

                            break;

                        case SDL.SDL_EventType.SDL_KEYUP:
                            UpdateKeys(tickEventArgs, sdlEvent.key.keysym.sym, false);
                            break;
                    }
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
                    SDL.SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 255);
                    SDL.SDL_RenderClear(_renderer);

                    // In order to pass the managed memory bitmap to the unmanaged SDL methods, we need to
                    // manually allocate memory for the byte array. This effectively "pins" it so it won't
                    // be garbage collected. We need to be sure to release this memory after we render.
                    var frameBuffer = GCHandle.Alloc(tickEventArgs.FrameBuffer, GCHandleType.Pinned);
                    var frameBufferPointer = frameBuffer.AddrOfPinnedObject();

                    // Now that we have an unmanaged pointer to the bitmap, we can use the SDL methods to
                    // get an abstract stream interface which will allow us to load the bitmap as a texture.
                    var rwops = SDL.SDL_RWFromConstMem(frameBufferPointer, tickEventArgs.FrameBuffer.Length);
                    var surface = SDL.INTERNAL_SDL_LoadBMP_RW(rwops, 0);
                    var texture = SDL.SDL_CreateTextureFromSurface(_renderer, surface);

                    // Now that we've loaded the framebuffer's bitmap as a texture, we can now render it to
                    // the SDL canvas at the given location.
                    SDL.SDL_RenderCopy(_renderer, texture, ref _renderLocation, ref _renderLocation);

                    // Ensure we release our unmanaged memory.
                    SDL.SDL_DestroyTexture(texture);
                    SDL.SDL_FreeSurface(surface);
                    SDL.SDL_FreeRW(rwops);
                    frameBuffer.Free();

                    // Now that we're done rendering, we can present this new frame.
                    SDL.SDL_RenderPresent(_renderer);

                    // renderStopwatch.Stop();
                    // Console.WriteLine("Render completed in: " + renderStopwatch.ElapsedMilliseconds + " ms");
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
        public void QueueAudioSamples(byte[] samples)
        {
            // Merge all three voices into one.
            var sampleFull = samples[0] + samples[1] + samples[2];

            // Clamp the value to the min/max of a 8-bit signed value to avoid distortion.
            if (sampleFull > 127)
                sampleFull = 127;
            else if (sampleFull < -128)
                sampleFull = -128;

            var sample = (sbyte)sampleFull;

            // Now that we have the combined sample, we need to allocate it as a pinned object
            // on the heap so that we can pass it through to the unmanaged SDL2 code.
            var samplePinned = GCHandle.Alloc(sample, GCHandleType.Pinned);
            var pointer = samplePinned.AddrOfPinnedObject();

            // Pass the value to SDL to be queued up for playback.
            uint sample_size = sizeof(sbyte) * 1;
            SDL.SDL_QueueAudio(_audioDevice, pointer, sample_size);

            // Unpin this so the GC can clean it up.
            samplePinned.Free();
        }

        /**
         * Used to cleanup after the SDL resources.
         */
        public void Dispose()
        {
            if (_renderer != IntPtr.Zero)
                SDL.SDL_DestroyRenderer(_renderer);

            if (_window != IntPtr.Zero)
                SDL.SDL_DestroyWindow(_window);

            SDL.SDL_CloseAudioDevice(_audioDevice);
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

        #endregion
    }
}