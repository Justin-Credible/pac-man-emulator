using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CP_IY_Half_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { false, 0x42, 0x2219, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
            list.Add(new object[] { false, 0xFF, 0x3316, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
            list.Add(new object[] { false, 0x02, 0x4407, new ConditionFlags() { Carry = true, Zero = false, Sign = true } });
            list.Add(new object[] { false, 0x12, 0x5512, new ConditionFlags() { Carry = false, Zero = true, Sign = false } });
            list.Add(new object[] { true, 0x42, 0x1922, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
            list.Add(new object[] { true, 0xFF, 0x1633, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
            list.Add(new object[] { true, 0x02, 0x0744, new ConditionFlags() { Carry = true, Zero = false, Sign = true } });
            list.Add(new object[] { true, 0x12, 0x1255, new ConditionFlags() { Carry = false, Zero = true, Sign = false } });

            return list;
        }

        // TODO: Test Parity/Overflow and AuxCarry flags.
        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_CP_IY_Half(bool high, byte initialValue, UInt16 valueToAdd, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                CP {(high ? "IYH" : "IYL")}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = initialValue,
                    IY = valueToAdd,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = !expectedFlags.Carry,
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    // Parity = !expectedFlags.Parity,

                    // Should be set.
                    Subtract = false,

                    // AuxCarry = ???
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(initialValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            // Assert.Equal(expectedFlags.Parity, state.Flags.Parity);

            // Should be set.
            Assert.True(state.Flags.Subtract);

            // Assert.False(state.Flags.AuxCarry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
