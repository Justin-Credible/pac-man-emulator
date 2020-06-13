using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class XOR_IX_Half_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { false, 0b10001101, 0b1001100110101110, 0b00100011, new ConditionFlags() { Sign = false, Zero = false, ParityOverflow = false } });
            list.Add(new object[] { false, 0b10001101, 0b0110000010101111, 0b00100010, new ConditionFlags() { Sign = false, Zero = false, ParityOverflow = true } });
            list.Add(new object[] { false, 0b10001101, 0b0000000000101111, 0b10100010, new ConditionFlags() { Sign = true, Zero = false, ParityOverflow = false } });
            list.Add(new object[] { false, 0b11111111, 0b0001000011111111, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, ParityOverflow = true } });
            list.Add(new object[] { false, 0b00000000, 0b1001100011111111, 0b11111111, new ConditionFlags() { Sign = true, Zero = false, ParityOverflow = true } });
            list.Add(new object[] { false, 0b00000000, 0b0001000000000000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, ParityOverflow = true } });

            list.Add(new object[] { true, 0b10001101, 0b1010111010011001, 0b00100011, new ConditionFlags() { Sign = false, Zero = false, ParityOverflow = false } });
            list.Add(new object[] { true, 0b10001101, 0b1010111101100000, 0b00100010, new ConditionFlags() { Sign = false, Zero = false, ParityOverflow = true } });
            list.Add(new object[] { true, 0b10001101, 0b0010111100000000, 0b10100010, new ConditionFlags() { Sign = true, Zero = false, ParityOverflow = false } });
            list.Add(new object[] { true, 0b11111111, 0b1111111100010000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, ParityOverflow = true } });
            list.Add(new object[] { true, 0b00000000, 0b1111111110011000, 0b11111111, new ConditionFlags() { Sign = true, Zero = false, ParityOverflow = true } });
            list.Add(new object[] { true, 0b00000000, 0b0000000000010000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, ParityOverflow = true } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_XOR_IX(bool high, byte initialValue, UInt16 valueToXOR, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                XOR {(high ? "IXH" : "IXL")}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = initialValue,
                    IX = valueToXOR,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    ParityOverflow = !expectedFlags.ParityOverflow,

                    // Should be reset.
                    Subtract = true,
                    Carry = true,
                    HalfCarry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.Carry);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.HalfCarry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
