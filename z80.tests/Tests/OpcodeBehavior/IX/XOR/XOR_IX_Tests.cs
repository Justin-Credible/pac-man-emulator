using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class XOR_IX_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0b10001101, 0b10101110, 0b00100011, new ConditionFlags() { Sign = false, Zero = false, ParityOverflow = false } });
                list.Add(new object[] { offset, 0b10001101, 0b10101111, 0b00100010, new ConditionFlags() { Sign = false, Zero = false, ParityOverflow = true } });
                list.Add(new object[] { offset, 0b10001101, 0b00101111, 0b10100010, new ConditionFlags() { Sign = true, Zero = false, ParityOverflow = false } });
                list.Add(new object[] { offset, 0b11111111, 0b11111111, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, ParityOverflow = true } });
                list.Add(new object[] { offset, 0b00000000, 0b11111111, 0b11111111, new ConditionFlags() { Sign = true, Zero = false, ParityOverflow = true } });
                list.Add(new object[] { offset, 0b00000000, 0b00000000, 0b00000000, new ConditionFlags() { Sign = false, Zero = true, ParityOverflow = true } });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_XOR_IX(int offset, byte initialValue, byte valueToXOR, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                XOR (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = valueToXOR;

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
                    HalfCarry = true,
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
            Assert.False(state.Flags.HalfCarry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
