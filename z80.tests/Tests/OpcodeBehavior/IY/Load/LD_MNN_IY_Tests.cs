using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_MNN_IY_Tests : BaseTest
    {
        [Fact]
        public void Test_LD_MNN_IY()
        {
            var rom = AssembleSource($@"
                org 00h
                LD (2234h), IY
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x42AF,
                },
                Flags = new ConditionFlags()
                {
                    // Should be unaffected.
                    Carry = true,
                    Sign = true,
                    Zero = true,
                    Parity = true,
                    Subtract = true,
                    AuxCarry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x42AF, state.Registers.IY);
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
