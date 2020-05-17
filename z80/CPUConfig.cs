using System;

namespace JustinCredible.ZilogZ80
{
    /**
     * Initial configuration for the CPU; values also used when resetting the CPU.
     */
    public class CPUConfig
    {
        /** Size, in bytes, of the memory space for the CPU. */
        public int MemorySize { get; set; }

        /** Starting memory location that should be writeable (leave both to 0 to allow all writes). */
        public UInt16 WriteableMemoryStart { get; set; }

        /** Ending memory location that should be writeable (leave both to 0 to allow all writes). */
        public UInt16 WriteableMemoryEnd { get; set; }

        /** Starting memory location that should be mirrored to the writeable memory (RAM mirror)  (leave bot to 0 to disable RAM mirror). */
        public UInt16 MirrorMemoryStart { get; set; }

        /** Ending memory location that should be mirrored to the writeable memory (RAM mirror) (leave bot to 0 to disable RAM mirror). */
        public UInt16 MirrorMemoryEnd { get; set; }

        /** The primary CPU registers (A B C D E H L) */
        public CPURegisters Registers { get; set; }

        /** Alternative register set (A' B' C' D' E' H' L') */
        public CPURegisters ShadowRegisters { get; set; }

        /** The encapsulated condition/flags register (F) */
        public ConditionFlags Flags { get; set; }

        /** Alternative flag register (F') */
        public ConditionFlags ShadowFlags { get; set; }

        /** The memory refresh register (R) */
        public UInt16 MemoryRefresh { get; set; } // TODO

        /** The index register (IX) */
        public UInt16 IndexIX { get; set; } // TODO

        /** The index register (IY) */
        public UInt16 IndexIY { get; set; } // TODO

        public UInt16 ProgramCounter { get; set; } = 0x0000;

        public UInt16 StackPointer { get; set; } = 0x0000;

        /** Indicates if interrupts are enabled or not: IFF1. */
        public bool InterruptsEnabled { get; set; } = false;

        /** The previous value of the interrupts enabled flag (IFF1) when a non-maskable interrupt is used: IFF2. */
        public bool InterruptsEnabledPreviousValue { get; set; } = false;

        /** The mode of interrupt the CPU is currently using. */
        public InterruptMode InterruptMode { get; set; } = InterruptMode.Zero;

        /**
         * Special flag used to patch the CALL calls for the cpudiag.bin program.
         * This allow CALL 0x05 to simulate CP/M writing the console and will exit
         * on JP 0x00. This is only used for testing the CPU with this specific ROM.
         */
        public bool EnableDiagnosticsMode { get; set; } = false;
    }
}
