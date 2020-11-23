
namespace JustinCredible.PacEmu
{
    public enum DebuggerState
    {
        /**
         * The PCB/CPU is running and the debbuger is waiting for a breakpoint to be hit.
         */
        Idle,

        /**
         * The PCB/CPU is paused on a breakpoint and the debugger is waiting for user input.
         */
        Breakpoint,

        /**
         * The user requested to single step the CPU and is waiting for the single opcode to execute.
         */
        SingleStepping,

        /**
         * The user is in the Edit Breakpoints menu.
         */
        EditBreakpoints,

        /**
         * The user is in the Load State menu.
         */
        LoadState,

        /**
         * The user is in the Save State menu.
         */
        SaveState,
    }
}
