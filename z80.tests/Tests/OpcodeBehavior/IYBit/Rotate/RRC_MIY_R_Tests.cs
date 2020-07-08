using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RRC_MIY_R_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForRegisters()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var register in RegistersClassData.StandardRegisters)
            {
                foreach (var offset in offsets)
                {
                    list.Add(new object[] { register, offset });
                }
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForRegisters))]
        public void Test_RRC_MIY_R_SetsCarryFlagTrue(Register register, int offset)
        {
            var rom = AssembleSource($@"
                org 00h
                RRC (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)}), {register}
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = 0b01100101;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = false,
                    Sign = false,
                    Zero = true,
                    ParityOverflow = false,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2234, state.Registers.IY);
            Assert.Equal(0b10110010, state.Registers[register]);

            // Should be affected.
            Assert.True(state.Flags.Carry);
            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }

        [Theory]
        [MemberData(nameof(GetDataForRegisters))]
        public void Test_RRC_MIY_R_SetsCarryFlagFalse(Register register, int offset)
        {
            var rom = AssembleSource($@"
                org 00h
                RRC (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)}), {register}
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = 0b11100100;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = true,
                    Sign = false,
                    Zero = true,
                    ParityOverflow = false,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2234, state.Registers.IY);
            Assert.Equal(0b01110010, state.Registers[register]);

            // Should be affected.
            Assert.False(state.Flags.Carry);
            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(27)]
        [InlineData(-33)]
        [InlineData(-62)]
        public void Test_RRC_MIY_SetsCarryFlagTrue(int offset)
        {
            var rom = AssembleSource($@"
                org 00h
                RRC (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = 0b01100101;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = false,
                    Sign = false,
                    Zero = true,
                    ParityOverflow = false,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b10110010, state.Memory[0x2234 + offset]);

            // Should be affected.
            Assert.True(state.Flags.Carry);
            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(27)]
        [InlineData(-33)]
        [InlineData(-62)]
        public void Test_RRC_MIY_SetsCarryFlagFalse(int offset)
        {
            var rom = AssembleSource($@"
                org 00h
                RRC (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = 0b11100100;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = true,
                    Sign = false,
                    Zero = true,
                    ParityOverflow = false,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b01110010, state.Memory[0x2234 + offset]);

            // Should be affected.
            Assert.False(state.Flags.Carry);
            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
