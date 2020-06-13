using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_IY_MNN_Tests : BaseTest
    {
        [Fact]
        public void Test_LD_IY_MNN()
        {
            var rom = AssembleSource($@"
                org 00h
                LD IY, (2234h)
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234] = 0xAF;
            memory[0x2235] = 0x42;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x7777,
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

            var state = Execute(rom, memory, initialState);

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
