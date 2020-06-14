using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class Test8BitArithmeticAdditionFlags : BaseTest
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var list = new List<object[]>();

            // 0000 0101    5
            // 0000 1011 + 11
            // 0001 0000 = 16   Flags: H
            list.Add(new object[] { "5", "11", 16, 16, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "11", "5", 16, 16, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });

            // 0000 1111   15
            // 0000 0001 +  1
            // 0001 0000 = 16   Flags: H
            list.Add(new object[] { "15", "1", 16, 16, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "1", "15", 16, 16, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });

            // 0000 0101 +  5
            // 0001 0000   16
            // 0001 0101 = 21   Flags: None
            list.Add(new object[] { "5", "16", 21, 21, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "16", "5", 21, 21, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });

            // 0000 1011   11
            // 0000 0111 +  7
            // 0001 0010 = 18   Flags: H
            list.Add(new object[] { "11", "7", 18, 18, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "7", "11", 18, 18, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });

            // 0001 0000   16
            // 0000 1000 +  8
            // 0001 1000 = 24   Flags: None
            list.Add(new object[] { "16", "8", 24, 24, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "8", "16", 24, 24, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });

            //   1000 0000   128               -128
            //   1000 0000 + 128             + -128
            // 1 0000 0000 = 256    =   0    = -256    Flags: Z C V
            list.Add(new object[] { "128", "128", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = false, ParityOverflow = true } });

            //   1111 0000   240               -16
            //   0001 0000 +  16             +  16
            // 1 0000 0000 = 256    =   0    =   0    Flags: Z C
            list.Add(new object[] { "240", "16", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "16", "240", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = false, ParityOverflow = false } });

            //   1111 1111   255                -1
            //   0000 0001 +   1             +   1
            // 1 0000 0000 = 256    =   0    =   0    Flags: Z C H
            list.Add(new object[] { "255", "1", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "1", "255", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   0000 1000     8                 8
            //   1111 1000 + 248             +  -8
            // 1 0000 0000 = 256    =   0    =   0    Flags: Z C H
            list.Add(new object[] { "8", "248", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "248", "8", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   0000 1101    13                13
            //   1111 1000 + 248             +  -8
            // 1 0000 0101 = 261    =   5    =   5    Flags: C H
            list.Add(new object[] { "13", "248", 5, 5, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "248", "13", 5, 5, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   0111 1110   126
            //   0000 0001 +   1
            // 0 0111 1111 = 127   Flags: None
            list.Add(new object[] { "126", "1", 127, 127, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "1", "126", 127, 127, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });

            //   0111 1110   126      126
            //   0000 0010 +   2    +   2
            // 0 1000 0000 = 128    =-128    Flags: S H V
            list.Add(new object[] { "126", "2", 128, -128, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });
            list.Add(new object[] { "2", "126", 128, -128, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });

            //   0111 1110   126     126
            //   0000 1011 +  11   +  11
            // 0 1000 1001 = 137   =-119    Flags: S H V
            list.Add(new object[] { "126", "11", 137, -119, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });
            list.Add(new object[] { "11", "126", 137, -119, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });

            //   1000 1000   136     -120
            //   0000 0100 +   4    +   4
            // 0 1000 1100 = 140    =-116    Flags: S
            list.Add(new object[] { "136", "4", 140, -116, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "4", "136", 140, -116, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void TestArithmeticAdditionSetsFlags(string valueA, string valueB, int expectedRegValue, int expectedRegSignedValue, ConditionFlags expected)
        {
            var rom = AssembleSource($@"
                org 00h
                LD A, {valueA}
                LD B, {valueB}
                ADD B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Flags = new ConditionFlags()
                {
                    Sign = !expected.Sign,
                    Zero = !expected.Zero,
                    HalfCarry = !expected.HalfCarry,
                    ParityOverflow = !expected.ParityOverflow,
                    Subtract = !expected.Subtract,
                    Carry = !expected.Carry,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedRegValue, state.Registers.A);
            Assert.Equal(expectedRegSignedValue, (sbyte)state.Registers.A);

            Assert.Equal(expected.Sign, state.Flags.Sign);
            Assert.Equal(expected.Zero, state.Flags.Zero);
            Assert.Equal(expected.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expected.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expected.Subtract, state.Flags.Subtract);
            Assert.Equal(expected.Carry, state.Flags.Carry);
        }
    }
}
