using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_IY_NN_Tests : BaseTest
    {
        [Fact]
        public void Test_LD_IY_NN()
        {
            var rom = AssembleSource($@"
                org 00h
                LD IY, 42h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x99,
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

            Assert.Equal(0x42, state.Registers.IY);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 14, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
