using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LDD_Tests : BaseTest
    {
        [Fact]
        public void Test_LDD()
        {
            var rom = AssembleSource($@"
                org 00h
                LDD
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x1111] = 0x88;
            memory[0x2222] = 0x66;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    BC = 0x0007,
                    DE = 0x2222,
                    HL = 0x1111,
                },
                Flags = new ConditionFlags()
                {
                    // Should remain unaffected.
                    Carry = true,
                    Sign = true,
                    Zero = true,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,

                    // Should be affected.
                    Parity = false,
                },
                MemorySize = memory.Length,
            };

            var cpu = new CPU(initialState);

            var state = Execute(rom, memory, cpu);

            Assert.Equal(0x06, state.Registers.BC);
            Assert.Equal(0x2221, state.Registers.DE);
            Assert.Equal(0x1110, state.Registers.HL);
            Assert.Equal(0x88, state.Memory[0x1111]);
            Assert.Equal(0x88, state.Memory[0x2222]);

            // Should remain unaffected.
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Carry);
            Assert.True(state.Flags.Sign);

            // Should be reset
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.AuxCarry);

            // Should be affected.
            Assert.True(state.Flags.Parity);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }
    }
}
