using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace JustinCredible.PacEmu
{
    public class RenderEventArgs : EventArgs
    {
        public byte[] FrameBuffer { get; set; } // Bitmap
    }
}
