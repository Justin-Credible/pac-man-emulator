using System;
using SDL2;

namespace JustinCredible.PacEmu
{
    class GUITickEventArgs : EventArgs
    {
        // Out

        // The state of the buttons/switches; true = pressed, false = not pressed.
        public Buttons ButtonState { get; set; } = new Buttons();

        // The keycode for a key that was pressed down _on this event loop tick only_.
        public SDL.SDL_Keycode? KeyDown { get; set; }

        // The Break/Pause key was pressed; if debugging, the emulator should break execution.
        public bool ShouldBreak { get; set; } = false;

        // Indicates if the CPU emulation was requested to be paused or un-paused.
        public bool ShouldPause { get; set; } = false;
        public bool ShouldUnPause { get; set; } = false;

        // In

        public byte[] FrameBuffer { get; set; } // Bitmap
        public bool ShouldRender { get; set; }

        public bool ShouldQuit { get; set; }
    }
}
