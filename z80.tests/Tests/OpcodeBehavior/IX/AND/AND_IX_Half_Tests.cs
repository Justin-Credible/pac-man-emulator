using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class AND_IX_Half_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { false, 0b00010111, 0b1001100111010101, 0b00010101, new ConditionFlags() { Sign = false, Zero = false, Parity = false } });
            list.Add(new object[] { false, 0b00010111, 0b0110000011010111, 0b00010111, new ConditionFlags() { Sign = false, Zero = false, Parity = true } });
            list.Add(new object[] { false, 0b10010111, 0b0000000011010111, 0b10010111, new ConditionFlags() { Sign = true, Zero = false, Parity = false } });
            list.Add(new object[] { false, 0b10010111, 0b0001000000000000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, Parity = true } });
            list.Add(new object[] { false, 0b11111111, 0b1001100011111111, 0b11111111, new ConditionFlags() { Sign = true, Zero = false, Parity = true } });
            list.Add(new object[] { false, 0b00000000, 0b0001000011111111, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, Parity = true } });
            list.Add(new object[] { false, 0b00000000, 0b0100010000000000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, Parity = true } });

            list.Add(new object[] { true, 0b00010111, 0b1101010110011001, 0b00010101, new ConditionFlags() { Sign = false, Zero = false, Parity = false } });
            list.Add(new object[] { true, 0b00010111, 0b1101011101100000, 0b00010111, new ConditionFlags() { Sign = false, Zero = false, Parity = true } });
            list.Add(new object[] { true, 0b10010111, 0b1101011100000000, 0b10010111, new ConditionFlags() { Sign = true, Zero = false, Parity = false } });
            list.Add(new object[] { true, 0b10010111, 0b0000000000010000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, Parity = true } });
            list.Add(new object[] { true, 0b11111111, 0b1111111110011000, 0b11111111, new ConditionFlags() { Sign = true, Zero = false, Parity = true } });
            list.Add(new object[] { true, 0b00000000, 0b1111111100010000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, Parity = true } });
            list.Add(new object[] { true, 0b00000000, 0b0000000001000100, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, Parity = true } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_AND_IX_Half(bool high, byte initialValue, UInt16 valueToAND, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                AND {(high ? "IXH" : "IXL")}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = initialValue,
                    IX = valueToAND,
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

                    // Should be set.
                    AuxCarry = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Parity, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.Carry);
            Assert.False(state.Flags.Subtract);

            // Should be set.
            Assert.True(state.Flags.AuxCarry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
