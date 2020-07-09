using System;

namespace JustinCredible.PacEmu
{
    public class RenderEventArgs : EventArgs
    {
        public byte[] FrameBuffer { get; set; }
        public bool ShouldRender { get; set; }
    }
}
