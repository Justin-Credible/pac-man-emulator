using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class Test8BitArithmeticAdditionWithCarryFlags : BaseTest
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var list = new List<object[]>();

            // 0000 0101    5
            // 0000 1011 + 11
            // 0000 0001 +  1
            // 0001 0001 = 17   Flags: H
            list.Add(new object[] { "5", "11", 17, 17, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "11", "5", 17, 17, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });

            // 0000 0101    5
            // 0000 1111 + 15
            // 0000 0001 +  1
            // 0001 0101 = 21   Flags: H
            list.Add(new object[] { "5", "15", 21, 21, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "15", "5", 21, 21, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });

            // 0000 0111    7
            // 0000 0111 +  7
            // 0000 0001 +  1
            // 0000 1111 = 15   Flags: None
            list.Add(new object[] { "7", "7", 15, 15, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });

            // 0000 1111   15
            // 0000 0001 +  1
            // 0000 0001 +  1
            // 0001 0001 = 17   Flags: H
            list.Add(new object[] { "15", "1", 17, 17, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "1", "15", 17, 17, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });

            // 0000 1011   11
            // 0000 0111 +  7
            // 0000 0001 +  1
            // 0001 0011 = 19   Flags: H
            list.Add(new object[] { "11", "7", 19, 19, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "7", "11", 19, 19, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = false } });

            // 0001 0000   16
            // 0000 1000 +  8
            // 0000 0001 +  1
            // 0001 1001 = 25   Flags: None
            list.Add(new object[] { "16", "8", 25, 25, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "8", "16", 25, 25, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });

            //   1000 0000   128               -128
            //   1000 0000 + 128             + -128
            //   0000 0001 +   1             +    1
            // 1 0000 0001 = 257    =   1    = -255    Flags: C V
            list.Add(new object[] { "128", "128", 1, 1, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = false, ParityOverflow = true } });

            //   1000 0000   128               -128
            //   1000 0001 + 129             + -127
            //   0000 0001 +   1             +    1
            // 1 0000 0010 = 258    =   2    = -254    Flags: C V
            list.Add(new object[] { "128", "129", 2, 2, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = false, ParityOverflow = true } });

            //   1111 0000   240               -16
            //   0001 0000 +  16             +  16
            //   0000 0001 +   1             +   1
            // 1 0000 0001 = 257    =   1    =   1    Flags: C
            list.Add(new object[] { "240", "16", 1, 1, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "16", "240", 1, 1, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = false, ParityOverflow = false } });

            //   1111 0000   240               -16
            //   0000 1111 +  15             +  15
            //   0000 0001 +   1             +   1
            // 1 0000 0001 = 256    =   0    =   0    Flags: Z C H
            list.Add(new object[] { "240", "15", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "15", "240", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   1111 1111   255                -1
            //   0000 0001 +   1             +   1
            //   0000 0001 +   1             +   1
            // 1 0000 0001 = 257    =   1    =   1    Flags: C H
            list.Add(new object[] { "255", "1", 1, 1, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "1", "255", 1, 1, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   1111 1110   254                -2
            //   0000 0001 +   1             +   1
            //   0000 0001 +   1             +   1
            // 1 0000 0000 = 256    =   0    =   0    Flags: Z C H
            list.Add(new object[] { "254", "1", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "1", "254", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   0000 1000     8                 8
            //   1111 1000 + 248             +  -8
            //   0000 0001 +   1             +   1
            // 1 0000 0001 = 257    =   1    =   1    Flags: C H
            list.Add(new object[] { "8", "248", 1, 1, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "248", "8", 1, 1, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   0000 1000     8                 8
            //   1111 0111 + 247             +  -7
            //   0000 0001 +   1             +   1
            // 1 0000 0000 = 256    =   0    =   0    Flags: Z C H
            list.Add(new object[] { "8", "247", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "247", "8", 0, 0, new ConditionFlags() { Zero = true, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   0000 1101    13                13
            //   1111 1000 + 248             +  -8
            //   0000 0001 +   1             +   1
            // 1 0000 0110 = 262    =   6    =   6    Flags: C H
            list.Add(new object[] { "13", "248", 6, 6, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "248", "13", 6, 6, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   0000 1101    13                13
            //   1111 0111 + 247             +  -9
            //   0000 0001 +   1             +   1
            // 1 0000 0110 = 261    =   5    =   5    Flags: C H
            list.Add(new object[] { "13", "247", 5, 5, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });
            list.Add(new object[] { "247", "13", 5, 5, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = true, HalfCarry = true, ParityOverflow = false } });

            //   0111 1110   126
            //   0000 0001 +   1
            //   0000 0001 +   1
            // 0 1000 0000 = 128   Flags: S H V
            list.Add(new object[] { "126", "1", 128, -128, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });
            list.Add(new object[] { "1", "126", 128, -128, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });

            //   0111 1111   127
            //   0000 0001 +   1
            //   0000 0001 +   1
            // 0 1000 0001 = 129   Flags: S H V
            list.Add(new object[] { "127", "1", 129, -127, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });
            list.Add(new object[] { "1", "127", 129, -127, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });

            //   0111 1101   125
            //   0000 0001 +   1
            //   0000 0001 +   1
            // 0 0111 1111 = 127   Flags: None
            list.Add(new object[] { "125", "1", 127, 127, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "1", "125", 127, 127, new ConditionFlags() { Zero = false, Sign = false, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });

            //   0111 1110   126      126
            //   0000 0010 +   2    +   2
            //   0000 0001 +   1    +   1
            // 0 1000 0001 = 129    =-127    Flags: S H V
            list.Add(new object[] { "126", "2", 129, -127, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });
            list.Add(new object[] { "2", "126", 129, -127, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });

            //   0111 1101   125      125
            //   0000 0010 +   2    +   2
            //   0000 0001 +   1    +   1
            // 0 1000 0000 = 128    =-128    Flags: S H V
            list.Add(new object[] { "125", "2", 128, -128, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });
            list.Add(new object[] { "2", "125", 128, -128, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });

            //   0111 1110   126     126
            //   0000 1011 +  11   +  11
            //   0000 0001 +   1   +   1
            // 0 1000 1010 = 138   =-118    Flags: S H V
            list.Add(new object[] { "126", "11", 138, -118, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });
            list.Add(new object[] { "11", "126", 138, -118, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });

            //   0111 1101   125     125
            //   0000 1011 +  11   +  11
            //   0000 0001 +   1   +   1
            // 0 1000 1001 = 137   =-119    Flags: S H V
            list.Add(new object[] { "125", "11", 137, -119, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });
            list.Add(new object[] { "11", "125", 137, -119, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = true, ParityOverflow = true } });

            //   1000 1000   136     -120
            //   0000 0100 +   4    +   4
            //   0000 0001 +   1    +   1
            // 0 1000 1101 = 141    =-115    Flags: S
            list.Add(new object[] { "136", "4", 141, -115, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "4", "136", 141, -115, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });

            //   1000 0111   135     -121
            //   0000 0100 +   4    +   4
            //   0000 0001 +   1    +   1
            // 0 1000 1100 = 140    =-116    Flags: S
            list.Add(new object[] { "135", "4", 140, -116, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });
            list.Add(new object[] { "4", "135", 140, -116, new ConditionFlags() { Zero = false, Sign = true, Subtract = false, Carry = false, HalfCarry = false, ParityOverflow = false } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void TestArithmeticAdditionWithCarrySetsFlags(string valueA, string valueB, int expectedRegValue, int expectedRegSignedValue, ConditionFlags expected)
        {
            var rom = AssembleSource($@"
                org 00h
                LD A, {valueA}
                LD B, {valueB}
                ADC B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Flags = new ConditionFlags()
                {
                    // We're testing add with carry behavior, so ensure this is set initially.
                    Carry = true,

                    Sign = !expected.Sign,
                    Zero = !expected.Zero,
                    HalfCarry = !expected.HalfCarry,
                    ParityOverflow = !expected.ParityOverflow,
                    Subtract = !expected.Subtract,
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
