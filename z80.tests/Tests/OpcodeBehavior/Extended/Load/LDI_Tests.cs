using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LDI_Tests : BaseTest
    {
        [Fact]
        public void Test_LDI()
        {
            var rom = AssembleSource($@"
                org 00h
                LDI
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
                    HalfCarry = true,

                    // Should be affected.
                    ParityOverflow = false,
                },
            };

            var cpu = new CPU(initialState);

            var state = Execute(rom, memory, cpu);

            Assert.Equal(0x06, state.Registers.BC);
            Assert.Equal(0x2223, state.Registers.DE);
            Assert.Equal(0x1112, state.Registers.HL);
            Assert.Equal(0x88, state.Memory[0x1111]);
            Assert.Equal(0x88, state.Memory[0x2222]);

            // Should remain unaffected.
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Carry);
            Assert.True(state.Flags.Sign);

            // Should be reset
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.HalfCarry);

            // Should be affected.
            Assert.True(state.Flags.ParityOverflow);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_LDI_ParityOverflowFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                LDI
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x1111] = 0x88;
            memory[0x2222] = 0x66;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    BC = 0x0001,
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
                    HalfCarry = true,

                    // Should be affected.
                    ParityOverflow = true,
                },
            };

            var cpu = new CPU(initialState);

            var state = Execute(rom, memory, cpu);

            Assert.Equal(0x00, state.Registers.BC);
            Assert.Equal(0x2223, state.Registers.DE);
            Assert.Equal(0x1112, state.Registers.HL);
            Assert.Equal(0x88, state.Memory[0x1111]);
            Assert.Equal(0x88, state.Memory[0x2222]);

            // Should remain unaffected.
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Carry);
            Assert.True(state.Flags.Sign);

            // Should be reset
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.HalfCarry);

            // Should be affected.
            Assert.False(state.Flags.ParityOverflow);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
