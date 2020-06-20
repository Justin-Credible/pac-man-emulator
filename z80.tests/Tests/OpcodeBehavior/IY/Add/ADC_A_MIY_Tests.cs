using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADC_A_MIY_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0x42, 0x19, 0x5B, false, new ConditionFlags() { Carry = false, HalfCarry = false, Zero = false, Sign = false, ParityOverflow = false  } });
                list.Add(new object[] { offset, 0x42, 0x4A, 0x8C, false, new ConditionFlags() { Carry = false, HalfCarry = false, Zero = false, Sign = true, ParityOverflow = true } });
                list.Add(new object[] { offset, 0xEE, 0x1D, 0x0B, false, new ConditionFlags() { Carry = true, HalfCarry = true, Zero = false, Sign = false, ParityOverflow = false } });
                list.Add(new object[] { offset, 0xEE, 0x12, 0x00, false, new ConditionFlags() { Carry = true, HalfCarry = true, Zero = true, Sign = false, ParityOverflow = false } });
                list.Add(new object[] { offset, 0x42, 0x19, 0x5C, true, new ConditionFlags() { Carry = false, HalfCarry = false, Zero = false, Sign = false, ParityOverflow = false } });
                list.Add(new object[] { offset, 0x42, 0x4A, 0x8D, true, new ConditionFlags() { Carry = false, HalfCarry = false, Zero = false, Sign = true, ParityOverflow = true } });
                list.Add(new object[] { offset, 0xEE, 0x1D, 0x0C, true, new ConditionFlags() { Carry = true, HalfCarry = true, Zero = false, Sign = false, ParityOverflow = false } });
                list.Add(new object[] { offset, 0xEE, 0x11, 0x00, true, new ConditionFlags() { Carry = true, HalfCarry = true, Zero = true, Sign = false, ParityOverflow = false } });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_ADC_A_MIY(int offset, byte initialValue, byte valueToAdd, byte expectedValue, bool initialCarryFlag, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = valueToAdd;

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
                    Carry = initialCarryFlag,
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    ParityOverflow = !expectedFlags.ParityOverflow,
                    HalfCarry = !expectedFlags.HalfCarry,

                    // Should be reset.
                    Subtract = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(expectedValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);

            // Should be reset.
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
