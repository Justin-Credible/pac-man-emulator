using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SLA_MIX_R_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForRegisters()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                foreach (var register in RegistersClassData.StandardRegisters)
                {
                    list.Add(new object[] { register, offset, 0b11001001, 0b10010010, new ConditionFlags() { Carry = true, Zero = false, Sign = true, Parity = false } });
                    list.Add(new object[] { register, offset, 0b10010010, 0b00100100, new ConditionFlags() { Carry = true, Zero = false, Sign = false, Parity = true } });
                    list.Add(new object[] { register, offset, 0b00100100, 0b01001000, new ConditionFlags() { Carry = false, Zero = false, Sign = false, Parity = true } });
                    list.Add(new object[] { register, offset, 0b10000000, 0b00000000, new ConditionFlags() { Carry = true, Zero = true, Sign = false, Parity = true } });
                }
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForRegisters))]
        public void Test_SLA_MIX_R(Register register, int offset, byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                SLA (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)}), {register}
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = initialValue;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IX = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = !expectedFlags.Carry,
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    Parity = !expectedFlags.Parity,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2234, state.Registers.IX);
            Assert.Equal(expectedValue, state.Registers[register]);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.Parity, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }

        public static IEnumerable<object[]> GetDataForHLRegister()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0b11001001, 0b10010010, new ConditionFlags() { Carry = true, Zero = false, Sign = true, Parity = false } });
                list.Add(new object[] { offset, 0b10010010, 0b00100100, new ConditionFlags() { Carry = true, Zero = false, Sign = false, Parity = true } });
                list.Add(new object[] { offset, 0b00100100, 0b01001000, new ConditionFlags() { Carry = false, Zero = false, Sign = false, Parity = true } });
                list.Add(new object[] { offset, 0b10000000, 0b00000000, new ConditionFlags() { Carry = true, Zero = true, Sign = false, Parity = true } });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForHLRegister))]
        public void Test_SLA_MIX(int offset, byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                SLA (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = initialValue;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IX = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = !expectedFlags.Carry,
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    Parity = !expectedFlags.Parity,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2234, state.Registers.IX);
            Assert.Equal(expectedValue, state.Memory[0x2234 + offset]);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.Parity, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
