using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RR_IX_R_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForRegisters()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var register in RegistersClassData.StandardRegisters)
            {
                foreach (var offset in offsets)
                {
                    list.Add(new object[] { register, offset, 0b01001001, false, 0b00100100, true, true, false });
                    list.Add(new object[] { register, offset, 0b01001001, true, 0b10100100, true, false, true });
                    list.Add(new object[] { register, offset, 0b01001000, false, 0b00100100, false, true, false });
                    list.Add(new object[] { register, offset, 0b01001000, true, 0b10100100, false, false, true });
                }
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForRegisters))]
        public void Test_RR_IX_R(Register register, int offset, byte initialValue, bool initialCarryFlag, byte expectedValue, bool expectedCarryFlag, bool expectedPartyFlag, bool expectedSignFlag)
        {
            var rom = AssembleSource($@"
                org 00h
                RR (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)}), {register}
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
            Assert.Equal(expectedSignFlag, state.Flags.Sign);
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
                list.Add(new object[] { offset, 0b01001001, false, 0b00100100, true, true, false });
                list.Add(new object[] { offset, 0b01001001, true, 0b10100100, true, false, true });
                list.Add(new object[] { offset, 0b01001000, false, 0b00100100, false, true, false });
                list.Add(new object[] { offset, 0b01001000, true, 0b10100100, false, false, true });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_RR_IX(int offset, byte initialValue, bool initialCarryFlag, byte expectedValue, bool expectedCarryFlag, bool expectedPartyFlag, bool expectedSignFlag)
        {
            var rom = AssembleSource($@"
                org 00h
                RR (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
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
            Assert.Equal(expectedSignFlag, state.Flags.Sign);
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
