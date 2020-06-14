using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class Test16BitArithmeticAdditionFlags : BaseTest
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var list = new List<object[]>();

            // 0000 0000 0111 1110      126
            // 0000 0000 0000 0010    +   2
            // 0000 0000 1000 0000    = 128   Flags: None
            list.Add(new object[] { "126", "2", 128, new ConditionFlags() { Carry = false, HalfCarry = false } });
            list.Add(new object[] { "2", "126", 128, new ConditionFlags() { Carry = false, HalfCarry = false } });

            //   1111 1111 1111 1110      65534
            //   0000 0000 0000 0010    +     2
            // 1 0000 0000 0000 0000    = 65536   Flags: C H
            list.Add(new object[] { "65534", "2", 0, new ConditionFlags() { Carry = true, HalfCarry = true } });
            list.Add(new object[] { "2", "65534", 0, new ConditionFlags() { Carry = true, HalfCarry = true } });

            //   1111 0000 0000 0000      61440
            //   0001 0000 0000 0000    +  4096
            // 1 0000 0000 0000 0000    = 65536   Flags: C
            list.Add(new object[] { "61440", "4096", 0, new ConditionFlags() { Carry = true, HalfCarry = false } });
            list.Add(new object[] { "4096", "61440", 0, new ConditionFlags() { Carry = true, HalfCarry = false } });

            //   1111 0000 1001 0100      61588
            //   0001 0000 1000 0010    +  4226
            // 1 0000 0001 0001 0110    = 65841   Flags: C
            list.Add(new object[] { "61588", "4226", 278, new ConditionFlags() { Carry = true, HalfCarry = false } });
            list.Add(new object[] { "4226", "61588", 278, new ConditionFlags() { Carry = true, HalfCarry = false } });

            //   0000 0111 0000 0000       1792
            //   0000 0001 0000 0000    +   256
            // 0 0000 1000 0000 0000    =  2048   Flags: None
            list.Add(new object[] { "1792", "256", 2048, new ConditionFlags() { Carry = false, HalfCarry = false } });
            list.Add(new object[] { "256", "1792", 2048, new ConditionFlags() { Carry = false, HalfCarry = false } });

            //   0000 1111 0000 0000       3840
            //   0000 0001 0000 0000    +   256
            // 0 0001 0000 0000 0000    =  4096   Flags: H
            list.Add(new object[] { "3840", "256", 4096, new ConditionFlags() { Carry = false, HalfCarry = true } });
            list.Add(new object[] { "256", "3840", 4096, new ConditionFlags() { Carry = false, HalfCarry = true } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void TestArithmeticAdditionSetsFlags(string valueA, string valueB, int expectedRegValue, ConditionFlags expected)
        {
            var rom = AssembleSource($@"
                org 00h
                LD HL, {valueA}
                LD BC, {valueB}
                ADD HL, BC
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Flags = new ConditionFlags()
                {
                    // These flags should remain unchanged.
                    Sign = false,
                    Zero = false,
                    ParityOverflow = false,

                    // This flag should be reset.
                    Subtract = true,

                    // These flags should be affected.
                    Carry = !expected.Carry,
                    HalfCarry = !expected.HalfCarry,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedRegValue, state.Registers.HL);

            // These flags should remain unchanged.
            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.ParityOverflow);

            // This flag should be reset.
            Assert.False(state.Flags.Subtract);

            // These flags should be affected.
            Assert.Equal(expected.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expected.Carry, state.Flags.Carry);
        }
    }
}
