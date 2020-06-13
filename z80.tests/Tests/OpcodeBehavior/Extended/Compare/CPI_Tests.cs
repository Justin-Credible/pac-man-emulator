using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CPI_Tests : BaseTest
    {
        [Fact]
        public void Test_CPI()
        {
            var rom = AssembleSource($@"
                org 00h
                CPI
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x1111] = 24;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 42,
                    BC = 0x0001,
                    HL = 0x1111,
                },
                Flags = new ConditionFlags()
                {
                    // Should remain unaffected.
                    Carry = true,

                    // Should be affected.
                    Zero = true,
                    Subtract = false,
                    Sign = true,
                    // AuxCarry = true, // TODO
                    ParityOverflow = true,
                },
                MemorySize = memory.Length,
            };

            var cpu = new CPU(initialState);

            var state = Execute(rom, memory, cpu);

            Assert.Equal(0, state.Registers.BC);
            Assert.Equal(0x1112, state.Registers.HL);
            Assert.Equal(24, state.Memory[0x1111]);

            // Should remain unaffected.
            Assert.True(state.Flags.Carry);

            // Should be affected.
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Sign);
            // Assert.True(state.Flags.AuxCarry); // TODO
            Assert.True(state.Flags.ParityOverflow);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_CPI_SetsZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                CPI
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x1111] = 0x3B;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x3B,
                    BC = 0x0001,
                    HL = 0x1111,
                },
                Flags = new ConditionFlags()
                {
                    // Should remain unaffected.
                    Carry = true,

                    // Should be affected.
                    Zero = false,
                    Subtract = false,
                    Sign = true,
                    // AuxCarry = true, // TODO
                    ParityOverflow = false,
                },
                MemorySize = memory.Length,
            };

            var cpu = new CPU(initialState);

            var state = Execute(rom, memory, cpu);

            Assert.Equal(0, state.Registers.BC);
            Assert.Equal(0x1112, state.Registers.HL);
            Assert.Equal(0x3B, state.Memory[0x1111]);

            // Should remain unaffected.
            Assert.True(state.Flags.Carry);

            // Should be affected.
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Sign);
            // Assert.True(state.Flags.AuxCarry); // TODO
            Assert.True(state.Flags.ParityOverflow);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
