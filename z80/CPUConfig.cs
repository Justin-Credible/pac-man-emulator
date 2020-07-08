using System;

namespace JustinCredible.ZilogZ80
{
    /**
     * Initial configuration for the CPU; values also used when resetting the CPU.
     */
    public class CPUConfig
    {
        /** The memory implementation to use. */
        public IMemory Memory { get; set; }

        /** The CPU registers (A B C D E H L I R IX IY PC SP) */
        public CPURegisters Registers { get; set; } = new CPURegisters();

        /** The encapsulated condition/flags register (F) */
        public ConditionFlags Flags { get; set; } = new ConditionFlags();

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
