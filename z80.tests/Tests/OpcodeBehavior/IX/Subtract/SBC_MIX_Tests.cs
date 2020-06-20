using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SBC_MIX_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0x42, 0x19, 0x29, false, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
                list.Add(new object[] { offset, 0xFF, 0x16, 0xE9, false, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
                list.Add(new object[] { offset, 0x02, 0x07, 0xFB, false, new ConditionFlags() { Carry = true, Zero = false, Sign = true } });
                list.Add(new object[] { offset, 0x12, 0x12, 0x00, false, new ConditionFlags() { Carry = false, Zero = true, Sign = false } });

                list.Add(new object[] { offset, 0x42, 0x19, 0x28, true, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
                list.Add(new object[] { offset, 0xFF, 0x16, 0xE8, true, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
                list.Add(new object[] { offset, 0x02, 0x07, 0xFA, true, new ConditionFlags() { Carry = true, Zero = false, Sign = true } });
                list.Add(new object[] { offset, 0x13, 0x12, 0x00, true, new ConditionFlags() { Carry = false, Zero = true, Sign = false } });
            }

            return list;
        }

        // TODO: Test Parity/Overflow and AuxCarry flags.
        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_SBC_MIX(int offset, byte initialValue, byte valueToAdd, byte expectedValue, bool initialCarryFlag, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                SBC (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = valueToAdd;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = initialValue,
                    IX = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = initialCarryFlag,
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    // Parity = !expectedFlags.Parity,

                    // Should be set.
                    Subtract = false,

                    // AuxCarry = ???
                },
            };

            var state = Execute(rom, memory, initialState);

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
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
