using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CPDR_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0x02, new byte[] { 0x09, 0xAA, 0x02 }, 0x0010, 0x123A, 4 + (21*2) + 16, 4, new ConditionFlags() { HalfCarry = false, ParityOverflow = true, Zero = true, Sign = false } });
            list.Add(new object[] { 0x02, new byte[] { 0x09, 0xAA, 0x02 }, 0x0002, 0x123A, 4 + (21*1) + 16, 3, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0xFF, new byte[] { 0x09, 0xAA, 0x02 }, 0x0003, 0x123A, 4 + (21*2) + 16, 4, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0x09, new byte[] { 0x09, 0xAA, 0x02 }, 0x0003, 0x123A, 4 + (21*0) + 16, 2, new ConditionFlags() { HalfCarry = false, ParityOverflow = true, Zero = true, Sign = false } });
            list.Add(new object[] { 0x02, new byte[] { 0x09, 0xAA, 0x02 }, 0x0001, 0x123A, 4 + (21*0) + 16, 2, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_CPDR(byte initialAValue, byte[] initialMemValues, UInt16 initialBCValue, UInt16 initialHLValue, int expectedCyles, int expectedIterations, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                CPDR
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

            // Load initial memory values.
            for (int i=0; i < initialMemValues.Length; i++)
                memory[initialHLValue - i] = initialMemValues[i];

            var state = Execute(rom, memory, initialState);

            // Register A shouldn't be changed.
            Assert.Equal(initialAValue, state.Registers.A);

            // Register pairs BC and HL should be decremented.
            // (expected iteration count includes HALT, so subtract that out first).
            Assert.Equal(initialBCValue - (expectedIterations - 1), state.Registers.BC);
            Assert.Equal(initialHLValue - (expectedIterations - 1), state.Registers.HL);

            // Original (HL) values shouldn't be changed.
            for (int i=0; i < initialMemValues.Length; i++)
                Assert.Equal(initialMemValues[i], state.Memory[initialHLValue - i]);

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

            Assert.Equal(expectedIterations, state.Iterations);
            Assert.Equal(expectedCyles, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
