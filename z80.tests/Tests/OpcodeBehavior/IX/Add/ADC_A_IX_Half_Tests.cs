using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADC_A_IX_Half_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { false, 0x42, 0x2219, 0x5B, false, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
            list.Add(new object[] { false, 0x42, 0x334A, 0x8C, false, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
            list.Add(new object[] { false, 0xEE, 0x441D, 0x0B, false, new ConditionFlags() { Carry = true, Zero = false, Sign = false } });
            list.Add(new object[] { false, 0xEE, 0x5512, 0x00, false, new ConditionFlags() { Carry = true, Zero = true, Sign = false } });
            list.Add(new object[] { true, 0x42, 0x1922, 0x5B, false, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
            list.Add(new object[] { true, 0x42, 0x4A33, 0x8C, false, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
            list.Add(new object[] { true, 0xEE, 0x1D44, 0x0B, false, new ConditionFlags() { Carry = true, Zero = false, Sign = false } });
            list.Add(new object[] { true, 0xEE, 0x1255, 0x00, false, new ConditionFlags() { Carry = true, Zero = true, Sign = false } });

            list.Add(new object[] { false, 0x42, 0x2219, 0x5C, true, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
            list.Add(new object[] { false, 0x42, 0x334A, 0x8D, true, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
            list.Add(new object[] { false, 0xEE, 0x441D, 0x0C, true, new ConditionFlags() { Carry = true, Zero = false, Sign = false } });
            list.Add(new object[] { false, 0xEE, 0x5511, 0x00, true, new ConditionFlags() { Carry = true, Zero = true, Sign = false } });
            list.Add(new object[] { true, 0x42, 0x1922, 0x5C, true, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
            list.Add(new object[] { true, 0x42, 0x4A33, 0x8D, true, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
            list.Add(new object[] { true, 0xEE, 0x1D44, 0x0C, true, new ConditionFlags() { Carry = true, Zero = false, Sign = false } });
            list.Add(new object[] { true, 0xEE, 0x1155, 0x00, true, new ConditionFlags() { Carry = true, Zero = true, Sign = false } });

            return list;
        }

        // TODO: Test Parity/Overflow and AuxCarry flags.
        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_ADC_A_IX_Half(bool high, byte initialValue, UInt16 valueToAdd, byte expectedValue, bool initialCarryFlag, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, {(high ? "IXH" : "IXL")}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = initialValue,
                    IX = valueToAdd,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = initialCarryFlag,
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    // Parity = !expectedFlags.Parity,

                    // Should be reset.
                    Subtract = true,

                    // AuxCarry = ???
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            // Assert.Equal(expectedFlags.Parity, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.Subtract);

            // Assert.False(state.Flags.AuxCarry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
