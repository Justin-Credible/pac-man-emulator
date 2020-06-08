using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RL_MIX_R_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForRegisters()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var register in RegistersClassData.StandardRegisters)
            {
                foreach (var offset in offsets)
                {
                    list.Add(new object[] { register, offset, 0b11001001, false, 0b10010010, true, false });
                    list.Add(new object[] { register, offset, 0b11001001, true, 0b10010011, true, true });
                    list.Add(new object[] { register, offset, 0b01001000, false, 0b10010000, false, true });
                    list.Add(new object[] { register, offset, 0b01001000, true, 0b10010001, false, false });
                }
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForRegisters))]
        public void Test_RL_MIX_R(Register register, int offset, byte initialValue, bool initialCarryFlag, byte expectedValue, bool expectedCarryFlag, bool expectedPartyFlag)
        {
            var rom = AssembleSource($@"
                org 00h
                RL (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)}), {register}
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
                    Carry = initialCarryFlag,
                    Zero = true,
                    Sign = false,
                    Parity = !expectedPartyFlag,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2234, state.Registers.IX);
            Assert.Equal(expectedValue, state.Registers[register]);

            // Should be affected.
            Assert.Equal(expectedCarryFlag, state.Flags.Carry);
            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.Equal(expectedPartyFlag, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }

        public static IEnumerable<object[]> GetData()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0b11001001, false, 0b10010010, true, false });
                list.Add(new object[] { offset, 0b11001001, true, 0b10010011, true, true });
                list.Add(new object[] { offset, 0b01001000, false, 0b10010000, false, true });
                list.Add(new object[] { offset, 0b01001000, true, 0b10010001, false, false });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_RL_MIX(int offset, byte initialValue, bool initialCarryFlag, byte expectedValue, bool expectedCarryFlag, bool expectedPartyFlag)
        {
            var rom = AssembleSource($@"
                org 00h
                RL (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
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
                    Carry = initialCarryFlag,
                    Zero = true,
                    Sign = false,
                    Parity = !expectedPartyFlag,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2234, state.Registers.IX);
            Assert.Equal(expectedValue, state.Memory[0x2234 + offset]);

            // Should be affected.
            Assert.Equal(expectedCarryFlag, state.Flags.Carry);
            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.Equal(expectedPartyFlag, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
