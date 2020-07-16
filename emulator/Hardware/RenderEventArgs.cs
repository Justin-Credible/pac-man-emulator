using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace JustinCredible.PacEmu
{
    public class RenderEventArgs : EventArgs
    {
        public Image<Rgba32> FrameBuffer { get; set; }
        public bool ShouldRender { get; set; }
    }
}
