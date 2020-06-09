using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SUB_IX_Half_Tests : BaseTest
    {

        [Fact]
        public void Test_SUB_IXH_ArbitraryExample()
        {
            var rom = AssembleSource($@"
                org 00h
                LD A, 0x42
                LD IX, 0x2234
                SUB IXH
                HALT
            ");

            var state = Execute(rom);

            Assert.Equal(0x20, state.Registers.A);
        }

        [Fact]
        public void Test_SUB_IXL_ArbitraryExample()
        {
            var rom = AssembleSource($@"
                org 00h
                LD A, 0x42
                LD IX, 0x2234
                SUB IXL
                HALT
            ");

            var state = Execute(rom);

            Assert.Equal(0x0E, state.Registers.A);
        }

        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { false, 0x42, 0x2219, 0x29, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
            list.Add(new object[] { false, 0xFF, 0x3316, 0xE9, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
            list.Add(new object[] { false, 0x02, 0x4407, 0xFB, new ConditionFlags() { Carry = true, Zero = false, Sign = true } });
            list.Add(new object[] { false, 0x12, 0x5512, 0x00, new ConditionFlags() { Carry = false, Zero = true, Sign = false } });
            list.Add(new object[] { true, 0x42, 0x1922, 0x29, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
            list.Add(new object[] { true, 0xFF, 0x1633, 0xE9, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
            list.Add(new object[] { true, 0x02, 0x0744, 0xFB, new ConditionFlags() { Carry = true, Zero = false, Sign = true } });
            list.Add(new object[] { true, 0x12, 0x1255, 0x00, new ConditionFlags() { Carry = false, Zero = true, Sign = false } });

            return list;
        }

        // TODO: Test Parity/Overflow and AuxCarry flags.
        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_SUB_IX_Half(bool high, byte initialValue, UInt16 valueToAdd, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                SUB {(high ? "IXH" : "IXL")}
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

            Assert.Equal(expectedValue, state.Registers.A);

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
