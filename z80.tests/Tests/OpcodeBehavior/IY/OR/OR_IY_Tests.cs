using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class OR_IY_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0b01100100, 0b01001001, 0b01101101, new ConditionFlags() { Sign = false, Zero = false, Parity = false } });
                list.Add(new object[] { offset, 0b01100100, 0b01001000, 0b01101100, new ConditionFlags() { Sign = false, Zero = false, Parity = true } });
                list.Add(new object[] { offset, 0b10010111, 0b11010111, 0b11010111, new ConditionFlags() { Sign = true, Zero = false, Parity = true } });
                list.Add(new object[] { offset, 0b11111111, 0b11111111, 0b11111111, new ConditionFlags() { Sign = true, Zero = false, Parity = true } });
                list.Add(new object[] { offset, 0b00000000, 0b11111111, 0b11111111, new ConditionFlags() { Sign = true, Zero = false, Parity = true } });
                list.Add(new object[] { offset, 0b00000000, 0b00000000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, Parity = true } });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_OR_IY(int offset, byte initialValue, byte valueToOR, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                OR (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = valueToOR;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = initialValue,
                    IY = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    Parity = !expectedFlags.Parity,

                    // Should be reset.
                    Subtract = true,
                    Carry = true,
                    AuxCarry = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(expectedValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Parity, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.Carry);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.AuxCarry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
