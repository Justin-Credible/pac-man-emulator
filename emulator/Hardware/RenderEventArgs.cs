using System;

namespace JustinCredible.PacEmu
{
    public class RenderEventArgs : EventArgs
    {
        /**
         * The frame to be renderd to the screen in the Bitmap file format.
         */
        public byte[] FrameBuffer { get; set; }
    }
}
