using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class NEG_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0x01, 0xFF, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0x00, 0x00, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = true, Sign = false } });
            list.Add(new object[] { 0x80, 0x80, new ConditionFlags() { Carry = true, HalfCarry = false, ParityOverflow = true, Zero = false, Sign = true } });
            list.Add(new object[] { 0xFE, 0x02, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0xFF, 0x01, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0x7E, 0x82, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0x7F, 0x81, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0x26, 0xDA, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0x8A, 0x76, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_NEG(byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                NEG
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = initialValue,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    HalfCarry = !expectedFlags.HalfCarry,
                    ParityOverflow = !expectedFlags.ParityOverflow,
                    Zero = !expectedFlags.Zero,
                    Sign = !expectedFlags.Sign,
                    Carry = !expectedFlags.Carry,

                    // Should be set.
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);

            // Should be set.
            Assert.True(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
