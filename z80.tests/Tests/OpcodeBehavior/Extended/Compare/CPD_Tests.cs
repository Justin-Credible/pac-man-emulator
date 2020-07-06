using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CPD_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0x00, 0x01, 0x0002, 0x123A, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });
            list.Add(new object[] { 0x00, 0x00, 0x1001, 0x2323, new ConditionFlags() { HalfCarry = false, ParityOverflow = true, Zero = true, Sign = false } });
            list.Add(new object[] { 0x00, 0x80, 0x1777, 0x1122, new ConditionFlags() { HalfCarry = false, ParityOverflow = true, Zero = false, Sign = true } });
            list.Add(new object[] { 0x00, 0xFE, 0x0001, 0x2222, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0x00, 0xFF, 0xEEEE, 0x2222, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = false } });
            list.Add(new object[] { 0x00, 0x7E, 0x0002, 0x0003, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });
            list.Add(new object[] { 0x00, 0x7F, 0xABCD, 0x2FF2, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });
            list.Add(new object[] { 0x00, 0x26, 0x1101, 0x2222, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });
            list.Add(new object[] { 0x00, 0x8A, 0x122A, 0x0FFF, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = false } });
            list.Add(new object[] { 0x40, 0x01, 0x0002, 0x123A, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = false } });
            list.Add(new object[] { 0xFF, 0x00, 0x1001, 0x2323, new ConditionFlags() { HalfCarry = false, ParityOverflow = true, Zero = false, Sign = true } });
            list.Add(new object[] { 0x5B, 0x10, 0x1777, 0x1122, new ConditionFlags() { HalfCarry = false, ParityOverflow = true, Zero = false, Sign = false } });
            list.Add(new object[] { 0x36, 0xAF, 0x1777, 0x1122, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_CPD(byte initialAValue, byte initialMemValue, UInt16 initialBCValue, UInt16 initialHLValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                CPD
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = initialAValue,
                    BC = initialBCValue,
                    HL = initialHLValue,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    HalfCarry = !expectedFlags.HalfCarry,
                    ParityOverflow = !expectedFlags.ParityOverflow,
                    Zero = !expectedFlags.Zero,
                    Sign = !expectedFlags.Sign,

                    // Should be set.
                    Subtract = false,

                    // Should be unaffected.
                    Carry = false,
                },
            };

            var memory = new byte[16*1024];
            memory[initialHLValue] = initialMemValue;

            var state = Execute(rom, memory, initialState);

            // Register A shouldn't be changed.
            Assert.Equal(initialAValue, state.Registers.A);

            // Register pairs BC and HL should be decremented.
            Assert.Equal(initialBCValue - 1, state.Registers.BC);
            Assert.Equal(initialHLValue - 1, state.Registers.HL);

            // Original (HL) value shouldn't be changed.
            Assert.Equal(initialMemValue, state.Memory[initialHLValue]);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);

            // Should be set.
            Assert.True(state.Flags.Subtract);

            // Should be unaffected.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
