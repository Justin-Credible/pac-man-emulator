using System;
using SDL2;

namespace JustinCredible.PacEmu
{
    class DebugCommandEventArgs : EventArgs
    {
        public bool Continue { get; set; }
        public bool SingleStep { get; set; }
    }
}
