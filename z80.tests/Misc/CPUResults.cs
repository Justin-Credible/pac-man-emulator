using System;
using System.Collections.Generic;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CPUResults
    {
        /** The number of CPU steps or instructions executed */
        public int Iterations { get; set; }

        /** The number of CPU cycles ellapsed. */
        public int Cycles { get; set; }

        /** The values of the program counter at each CPU step that occurred. */
        public List<UInt16> ProgramCounterAddresses { get; set; }

        public byte[] Memory { get; set; }
        public CPURegisters Registers { get; set; }
        public CPURegisters ShadowRegisters { get; set; }
        public ConditionFlags Flags { get; set; }
        public ConditionFlags ShadowFlags { get; set; }
        public UInt16 MemoryRefresh { get; set; } // TODO
        public UInt16 IndexIX { get; set; } // TODO
        public UInt16 IndexIY { get; set; } // TODO
        public UInt16 ProgramCounter { get; set; }
        public UInt16 StackPointer { get; set; }
        public bool InterruptsEnabled { get; set; }
    }
}
