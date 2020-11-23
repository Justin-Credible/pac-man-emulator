using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using SDL2;

namespace JustinCredible.PacEmu
{
    // The platform code specific to the interactive debugger.
    partial class Platform
    {
        #region Instance Variables

        private DebuggerState _debuggerState = DebuggerState.Idle;

        // A string used to hold the contents of user input (during EditBreakpoints, LoadState, etc).
        private string _debuggerInputString = String.Empty;

        // A list of file paths to save state files in the current directory (during the LoadState and SaveState menu states).
        private List<string> _debuggerFileList = new List<string>();

        // Mutex for signalling when the debugger window should be re-rendered.
        private object _debuggerRenderingLock = new Object();

        // Used to ensure we only render the debugger window when it has changed (must use lock on _debuggerRenderingLock).
        private bool _debuggerNeedsRendering = true;

        // The current game PCB; used to render the debugger.
        private PacManPCB _debuggerPcb = null;

        // Indicates if annotated disassembly should be shown (as opposed to pseudocode code).
        private bool _debuggerShowAnnotatedDisassembly = true;

        #endregion

        #region Events

        // Fired when a user presses a key with the interactive debugger GUI window focused
        // which indicates the user selected a debugger command to execute.
        public delegate void DebugCommandEvent(DebugCommandEventArgs eventArgs);
        public event DebugCommandEvent OnDebugCommand;

        #endregion

        #region Public Methods

        /**
         * Used to create a GUI window via SDL for the interactive debugger.
         */
        public void InitializeDebugger(float scaleX = 1, float scaleY = 1)
        {
            var initResult = SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO);

            if (initResult != 0)
                throw new Exception(String.Format("Failure while initializing SDL. SDL Error: {0}", SDL.SDL_GetError()));

            var width = 640;
            var height = 480;

            _debugWindow = SDL.SDL_CreateWindow("Interactive Debugger",
                10,
                10,
                (int)(width * scaleX),
                (int)(height * scaleY),
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
            );

            if (_debugWindow == IntPtr.Zero)
                throw new Exception(String.Format("Unable to create a window. SDL Error: {0}", SDL.SDL_GetError()));

            _debugRendererSurface = SDL.SDL_CreateRenderer(_debugWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED /*| SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC*/);

            if (_debugRendererSurface == IntPtr.Zero)
                throw new Exception(String.Format("Unable to create a renderer. SDL Error: {0}", SDL.SDL_GetError()));

            // We can scale the image up or down based on the scaling factor.
            SDL.SDL_RenderSetScale(_debugRendererSurface, scaleX, scaleY);

            // By setting the logical size we ensure that the image will scale to fit the window while
            // still maintaining the original aspect ratio.
            SDL.SDL_RenderSetLogicalSize(_debugRendererSurface, width, height);
        }

        /**
         * Used to start the interactive debugger session with the given game PCB state.
         * This enables the user to enter commands (continue, step, etc) and examine CPU state etc.
         */
        public void StartInteractiveDebugger(PacManPCB pcb)
        {
            _debuggerPcb = pcb;
            _debuggerState = DebuggerState.Breakpoint;
            SignalDebuggerNeedsRendering();
        }

        #endregion

        #region Private Methods

        private void SignalDebuggerNeedsRendering()
        {
            lock(_debuggerRenderingLock)
            {
                _debuggerNeedsRendering = true;
            }
        }

        private List<string> GetDebuggerSaveStateFileList()
        {
            var files = new List<string>();

            var filesAbsolutePaths = Directory.GetFiles(Environment.CurrentDirectory, "*.state.json");

            foreach (var file in filesAbsolutePaths)
            {
                files.Add(file.Replace(Environment.CurrentDirectory, "."));
            }

            return files;
        }

        private void HandleDebuggerEvent(SDL.SDL_Event sdlEvent)
        {
            if (_debuggerState == DebuggerState.Breakpoint) // Stopped on a breakpoint waiting for user input.
            {
                HandleDebuggerEventForBreakpointState(sdlEvent);
            }
            else if (_debuggerState == DebuggerState.EditBreakpoints) // In the breakpoint editor menu.
            {
                HandleDebuggerEventForEditBreakpointsState(sdlEvent);
            }
            else if (_debuggerState == DebuggerState.SaveState || _debuggerState == DebuggerState.LoadState) // In the Load or Save State menu.
            {
                HandleDebuggerEventForLoadOrSaveStateState(sdlEvent);
            }
            else if (_debuggerState == DebuggerState.InstructionHistory) // In the "Show Last 50 Opcodes" menu.
            {
                HandleDebuggerEventForInstructionHistory(sdlEvent);
            }
        }

        private void HandleDebuggerEventForBreakpointState(SDL.SDL_Event sdlEvent)
        {
            if (sdlEvent.type != SDL.SDL_EventType.SDL_KEYDOWN)
                return;

            var keycode = sdlEvent.key.keysym.sym;

            switch (keycode)
            {
                case SDL.SDL_Keycode.SDLK_F1: // F1 = Save State
                    _debuggerState = DebuggerState.SaveState;
                    _debuggerFileList = GetDebuggerSaveStateFileList();
                    _debuggerInputString = String.Empty;
                    SignalDebuggerNeedsRendering();
                    break;

                case SDL.SDL_Keycode.SDLK_F2: // F2 = Load State
                    _debuggerState = DebuggerState.LoadState;
                    _debuggerFileList = GetDebuggerSaveStateFileList();
                    _debuggerInputString = _debuggerFileList.Count > 0 ? _debuggerFileList.Last() : String.Empty;
                    SignalDebuggerNeedsRendering();
                    break;

                case SDL.SDL_Keycode.SDLK_F4: // F4 = Edit Breakpoints
                    _debuggerState = DebuggerState.EditBreakpoints;
                    SignalDebuggerNeedsRendering();
                    break;

                case SDL.SDL_Keycode.SDLK_F5: // F5 = Continue
                    _debuggerState = DebuggerState.Idle;
                    _debuggerPcb = null;
                    SignalDebuggerNeedsRendering();
                    OnDebugCommand?.Invoke(new DebugCommandEventArgs() { Action = DebugAction.ResumeContinue });
                    break;

                case SDL.SDL_Keycode.SDLK_F9: // F9 = Step Backwards

                    if (_debuggerPcb.ReverseStepEnabled && _debuggerPcb._executionHistory.Count > 0)
                    {
                        _debuggerState = DebuggerState.SingleStepping;
                        _debuggerPcb = null;
                        SignalDebuggerNeedsRendering();
                        OnDebugCommand?.Invoke(new DebugCommandEventArgs() { Action = DebugAction.ReverseStep });
                    }

                    break;

                case SDL.SDL_Keycode.SDLK_F10: // F10 = Single Step
                    _debuggerState = DebuggerState.SingleStepping;
                    _debuggerPcb = null;
                    SignalDebuggerNeedsRendering();
                    OnDebugCommand?.Invoke(new DebugCommandEventArgs() { Action = DebugAction.ResumeStep });
                    break;

                case SDL.SDL_Keycode.SDLK_F11: // F11 = Toggle Annotated Disassembly
                    _debuggerShowAnnotatedDisassembly = !_debuggerShowAnnotatedDisassembly;
                    SignalDebuggerNeedsRendering();
                    break;

                case SDL.SDL_Keycode.SDLK_F12: // F12 = Print Last 50 Opcodes
                    _debuggerState = DebuggerState.InstructionHistory;
                    SignalDebuggerNeedsRendering();
                    break;
            }
        }

        private void HandleDebuggerEventForEditBreakpointsState(SDL.SDL_Event sdlEvent)
        {
            HandleDebuggerInputText(sdlEvent);

            if (sdlEvent.type != SDL.SDL_EventType.SDL_KEYDOWN)
                return;

            var keycode = sdlEvent.key.keysym.sym;

            switch (keycode)
            {
                case SDL.SDL_Keycode.SDLK_RETURN: // Return/Enter = Finish input and toggle breakpoint
                {
                    UInt16 address = 0;

                    var input = _debuggerInputString;
                    _debuggerInputString = String.Empty;

                    if (input.ToLower() == "clear")
                    {
                        _debuggerPcb.BreakAtAddresses.Clear();
                    }
                    else if (!String.IsNullOrWhiteSpace(input))
                    {
                        try
                        {
                            address = Convert.ToUInt16(input, 16);

                            if (_debuggerPcb.BreakAtAddresses.Contains(address))
                                _debuggerPcb.BreakAtAddresses.Remove(address);
                            else
                                _debuggerPcb.BreakAtAddresses.Add(address);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("Error parsing address in breakpoint editor.", exception);
                            SDL2.SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR, "Breakpoint Editor", $"Error parsing address; enter a hexidecimal address in the format: 0x1234 or 1234.", _debugWindow);
                        }
                    }

                    SignalDebuggerNeedsRendering();
                    break;
                }

                case SDL.SDL_Keycode.SDLK_ESCAPE: // Escape = Cancel editing breakpoints
                    _debuggerInputString = String.Empty;
                    _debuggerState = DebuggerState.Breakpoint;
                    SignalDebuggerNeedsRendering();
                    break;
            }
        }

        private void HandleDebuggerEventForLoadOrSaveStateState(SDL.SDL_Event sdlEvent)
        {
            HandleDebuggerInputText(sdlEvent);

            if (sdlEvent.type != SDL.SDL_EventType.SDL_KEYDOWN)
                return;

            var keycode = sdlEvent.key.keysym.sym;

            switch (keycode)
            {
                case SDL.SDL_Keycode.SDLK_UP: // Up = Move selection up
                {
                    if (_debuggerFileList.Count == 0)
                        break;

                    var index = _debuggerFileList.IndexOf(_debuggerInputString);

                    if (index > 0)
                        index--;

                    if (index == -1)
                        _debuggerInputString = _debuggerFileList.Count > 0 ? _debuggerFileList.Last() : String.Empty;
                    else
                        _debuggerInputString = _debuggerFileList[index];

                    SignalDebuggerNeedsRendering();

                    break;
                }

                case SDL.SDL_Keycode.SDLK_DOWN: // Down = Move selection down
                {
                    if (_debuggerFileList.Count == 0)
                        break;

                    var index = _debuggerFileList.IndexOf(_debuggerInputString);

                    if (index != -1 && index < _debuggerFileList.Count - 1)
                        index++;

                    if (index == -1)
                        _debuggerInputString = _debuggerFileList.Count > 0 ? _debuggerFileList.Last() : String.Empty;
                    else
                        _debuggerInputString = _debuggerFileList[index];

                    SignalDebuggerNeedsRendering();

                    break;
                }

                case SDL.SDL_Keycode.SDLK_RETURN: // Return/Enter = Finish input and load/save state
                {
                    var filePath = _debuggerInputString;
                    _debuggerInputString = String.Empty;

                    if (String.IsNullOrWhiteSpace(filePath))
                        break;

                    if (!filePath.EndsWith(".state.json"))
                        filePath += ".state.json";

                    if (_debuggerState == DebuggerState.LoadState && !File.Exists(filePath))
                    {
                        var errorMessage = $"Error {(_debuggerState == DebuggerState.LoadState ? "Loading" : "Saving")} state; file does not exist: {filePath}.";
                        Console.WriteLine(errorMessage);
                        SDL2.SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_WARNING, $"{(_debuggerState == DebuggerState.LoadState ? "Load" : "Save")} State", errorMessage, _debugWindow);
                        SignalDebuggerNeedsRendering();
                        break;
                    }

                    try
                    {
                        if (_debuggerState == DebuggerState.LoadState)
                        {
                            var json = File.ReadAllText(filePath);
                            var state = JsonSerializer.Deserialize<EmulatorState>(json);
                            _debuggerPcb.LoadState(state);
                        }
                        else
                        {
                            var state = _debuggerPcb.SaveState();
                            var json = JsonSerializer.Serialize<EmulatorState>(state);
                            File.WriteAllText(filePath, json);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"Error {(_debuggerState == DebuggerState.LoadState ? "Loading" : "Saving")} state for file: {filePath}", exception);
                        SDL2.SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR, $"{(_debuggerState == DebuggerState.LoadState ? "Load" : "Save")} State", $"Error {(_debuggerState == DebuggerState.LoadState ? "Loading" : "Saving")} state: {exception.Message}", _debugWindow);
                        SignalDebuggerNeedsRendering();
                        break;
                    }

                    var message = $"{(_debuggerState == DebuggerState.LoadState ? "Loading" : "Saving")} state was successful!";
                    SDL2.SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION, $"{(_debuggerState == DebuggerState.LoadState ? "Load" : "Save")} State", message, _debugWindow);

                    _debuggerInputString = String.Empty;
                    _debuggerFileList.Clear();
                    _debuggerState = DebuggerState.Breakpoint;
                    SignalDebuggerNeedsRendering();
                    break;
                }

                case SDL.SDL_Keycode.SDLK_ESCAPE: // Escape = Cancel load/save state
                    _debuggerInputString = String.Empty;
                    _debuggerFileList.Clear();
                    _debuggerState = DebuggerState.Breakpoint;
                    SignalDebuggerNeedsRendering();
                    break;
            }
        }

        private void HandleDebuggerEventForInstructionHistory(SDL.SDL_Event sdlEvent)
        {
            if (sdlEvent.type != SDL.SDL_EventType.SDL_KEYDOWN)
                return;

            if (sdlEvent.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
            {
                _debuggerState = DebuggerState.Breakpoint;
                SignalDebuggerNeedsRendering();
            }
        }

        private void HandleDebuggerInputText(SDL.SDL_Event sdlEvent)
        {
            if (sdlEvent.type == SDL.SDL_EventType.SDL_KEYDOWN) // Single key press; used for commands.
            {
                var keycode = sdlEvent.key.keysym.sym;

                switch (keycode)
                {
                    case SDL.SDL_Keycode.SDLK_BACKSPACE: // Backspace
                    {
                        if (_debuggerInputString != String.Empty)
                            _debuggerInputString = _debuggerInputString.Substring(0, _debuggerInputString.Length - 1);

                        SignalDebuggerNeedsRendering();
                        break;
                    }
                }
            }
            else if (sdlEvent.type == SDL.SDL_EventType.SDL_TEXTINPUT) // Used for text entry by the user.
            {
                // Read the user's text input and add to the input string.
                // https://github.com/flibitijibibo/SDL2-CS/issues/70#issuecomment-53173978

                byte[] rawBytes = new byte[SDL2.SDL.SDL_TEXTINPUTEVENT_TEXT_SIZE];
                unsafe { Marshal.Copy((IntPtr)sdlEvent.text.text, rawBytes, 0, SDL2.SDL.SDL_TEXTINPUTEVENT_TEXT_SIZE); }

                int length = Array.IndexOf(rawBytes, (byte)0);
                string input = System.Text.Encoding.UTF8.GetString(rawBytes, 0, length);

                if (!String.IsNullOrEmpty(input))
                    _debuggerInputString += input;

                SignalDebuggerNeedsRendering();
            }
        }

        #endregion
    }

}
