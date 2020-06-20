using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class AND_MIX_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0b00010111, 0b11010101, 0b00010101, new ConditionFlags() { Sign = false, Zero = false, ParityOverflow = false } });
                list.Add(new object[] { offset, 0b00010111, 0b11010111, 0b00010111, new ConditionFlags() { Sign = false, Zero = false, ParityOverflow = true } });
                list.Add(new object[] { offset, 0b10010111, 0b11010111, 0b10010111, new ConditionFlags() { Sign = true, Zero = false, ParityOverflow = false } });
                list.Add(new object[] { offset, 0b10010111, 0b00000000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, ParityOverflow = true } });
                list.Add(new object[] { offset, 0b11111111, 0b11111111, 0b11111111, new ConditionFlags() { Sign = true, Zero = false, ParityOverflow = true } });
                list.Add(new object[] { offset, 0b00000000, 0b11111111, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, ParityOverflow = true } });
                list.Add(new object[] { offset, 0b00000000, 0b00000000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, ParityOverflow = true } });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_AND_MIX(int offset, byte initialValue, byte valueToAND, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                AND (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = valueToAND;

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
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    ParityOverflow = !expectedFlags.ParityOverflow,

                    // Should be reset.
                    Subtract = true,
                    Carry = true,

                    // Should be set.
                    HalfCarry = false,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(expectedValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.Carry);
            Assert.False(state.Flags.Subtract);

            // Should be set.
            Assert.True(state.Flags.HalfCarry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
