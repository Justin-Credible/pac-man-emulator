using System;
using SDL2;

namespace JustinCredible.PacEmu
{
    class DebugCommandEventArgs : EventArgs
    {
        public DebugAction Action { get; set; }

        // The address to add or remove a breakpoint for (when action is AddBreakpoint/RemoveBreakpoint).
        public ushort Address { get; set; }

        // File name for the state when loading/saving (when action is SaveState/LoadState).
        public string FileName { get; set; }
    }
}
