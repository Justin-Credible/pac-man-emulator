using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_MNN_IX_Tests : BaseTest
    {
        [Fact]
        public void Test_LD_MNN_IX()
        {
            var rom = AssembleSource($@"
                org 00h
                LD (2234h), IX
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IX = 0x42AF,
                },
                Flags = new ConditionFlags()
                {
                    // Should be unaffected.
                    Carry = true,
                    Sign = true,
                    Zero = true,
                    ParityOverflow = true,
                    Subtract = true,
                    HalfCarry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x42AF, state.Registers.IX);
            Assert.Equal(0xAF, state.Memory[0x2234]);
            Assert.Equal(0x42, state.Memory[0x2235]);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 20, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
