using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADC_HL_RR_Tests : BaseTest
    {
        // TODO: Tests for ADC HL, HL

        public static IEnumerable<object[]> GetData()
        {
            var sourceRegisters = new List<RegisterPair>() { RegisterPair.BC, RegisterPair.DE, RegisterPair.SP };
            var list = new List<object[]>();

            foreach (var sourceRegister in sourceRegisters)
            {
                list.Add(new object[] { sourceRegister, 0x0000, 0x0000, 0x0000, false, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = true, Sign = false } });
                list.Add(new object[] { sourceRegister, 0xFFFF, 0x0001, 0x0000, false, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = true, Sign = false } });
                list.Add(new object[] { sourceRegister, 0x0FFF, 0x0001, 0x1000, false, new ConditionFlags() { Carry = false, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
                list.Add(new object[] { sourceRegister, 0xF000, 0xF000, 0xE000, false, new ConditionFlags() { Carry = true, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });
                list.Add(new object[] { sourceRegister, 0x0800, 0x0800, 0x1000, false, new ConditionFlags() { Carry = false, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
                list.Add(new object[] { sourceRegister, 0x0800, 0x1000, 0x1800, false, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = false } });

                list.Add(new object[] { sourceRegister, 0x7FFF, 0x0001, 0x8000, false, new ConditionFlags() { Carry = false, HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });
                list.Add(new object[] { sourceRegister, 0x7FFF, 0x7FFF, 0xFFFE, false, new ConditionFlags() { Carry = false, HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });

                // -1768 + 2328 = 560
                list.Add(new object[] { sourceRegister, 0xF918, 0x0918, 0x0230, false, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });

                // -1768 + 280 = -1488
                list.Add(new object[] { sourceRegister, 0xF918, 0x0118, 0xFA30, false, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });

                // -1768 + -1768 = -3536
                list.Add(new object[] { sourceRegister, 0xF918, 0xF918, 0xF230, false, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });

                // 2328 + -1768 = 560
                list.Add(new object[] { sourceRegister, 0x0918, 0xF918, 0x0230, false, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });

                // 280 + -1768 = -1488
                list.Add(new object[] { sourceRegister, 0x0118, 0xF918, 0xFA30, false, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });

                // -28930 + -4000 = -32930 (overflow)
                list.Add(new object[] { sourceRegister, 0x8EFE, 0xF060, 0x7F5E, false, new ConditionFlags() { Carry = true, HalfCarry = false, ParityOverflow = true, Zero = false, Sign = false } });

                // Carry flag set variations

                list.Add(new object[] { sourceRegister, 0xFFFE, 0x0001, 0x0000, true, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = true, Sign = false } });
                list.Add(new object[] { sourceRegister, 0x0000, 0x0000, 0x0001, true, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = false } });
                list.Add(new object[] { sourceRegister, 0xFFFF, 0x0001, 0x0001, true, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
                list.Add(new object[] { sourceRegister, 0x0FFF, 0x0001, 0x1001, true, new ConditionFlags() { Carry = false, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
                list.Add(new object[] { sourceRegister, 0xF000, 0xF000, 0xE001, true, new ConditionFlags() { Carry = true, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });
                list.Add(new object[] { sourceRegister, 0x0800, 0x0800, 0x1001, true, new ConditionFlags() { Carry = false, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
                list.Add(new object[] { sourceRegister, 0x0800, 0x1000, 0x1801, true, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = false } });

                list.Add(new object[] { sourceRegister, 0x7FFF, 0x0001, 0x8001, true, new ConditionFlags() { Carry = false, HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });

                // TODO: This test passes, but is a mismatch with 8bitworkshop.com's Williams (Z80) emulator.
                // Based on my understanding, this test should result in H/V/S being set, but according to the reference emulator only S is set...
                // org 00h
                // LD A, 0xFF
                // ADD A, 1 ; Ensure the carry bit is set
                // LD HL, 0x7FFF
                // LD BC, 0x7FFF
                // ADC HL, BC ; Notice when C=1 this sets the H/V/S bits, but when C=0 only S is set in the emulator
                // HALT
                list.Add(new object[] { sourceRegister, 0x7FFF, 0x7FFF, 0xFFFF, true, new ConditionFlags() { Carry = false, HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });
                // list.Add(new object[] { sourceRegister, 0x7FFF, 0x7FFF, 0xFFFF, true, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });

                // -1768 + 2328 = 560
                list.Add(new object[] { sourceRegister, 0xF918, 0x0918, 0x0231, true, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });

                // -1768 + 280 = -1488
                list.Add(new object[] { sourceRegister, 0xF918, 0x0118, 0xFA31, true, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });

                // -1768 + -1768 = -3536
                list.Add(new object[] { sourceRegister, 0xF918, 0xF918, 0xF231, true, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });

                // 2328 + -1768 = 560
                list.Add(new object[] { sourceRegister, 0x0918, 0xF918, 0x0231, true, new ConditionFlags() { Carry = true, HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });

                // 280 + -1768 = -1488
                list.Add(new object[] { sourceRegister, 0x0118, 0xF918, 0xFA31, true, new ConditionFlags() { Carry = false, HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });

                // -28930 + -4000 = -32930 (overflow)
                list.Add(new object[] { sourceRegister, 0x8EFE, 0xF060, 0x7F5F, true, new ConditionFlags() { Carry = true, HalfCarry = false, ParityOverflow = true, Zero = false, Sign = false } });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_ADC_HL_NoFlags(RegisterPair sourceRegister, UInt16 initialValue, UInt16 valueToAdd, UInt16 expectedValue, bool initialCarryFlag, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, {sourceRegister}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = initialValue;
            registers[sourceRegister] = valueToAdd;

            var flags = new ConditionFlags()
            {
                // Should be affected.
                Carry = initialCarryFlag,
                HalfCarry = !expectedFlags.HalfCarry,
                ParityOverflow = !expectedFlags.ParityOverflow,
                Zero = !expectedFlags.Zero,
                Sign = !expectedFlags.Sign,

                // Should be reset.
                Subtract = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers.HL);
            Assert.Equal(valueToAdd, state.Registers[sourceRegister]);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be reset.
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
