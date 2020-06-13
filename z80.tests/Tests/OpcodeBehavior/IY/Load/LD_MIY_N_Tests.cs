using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_MIY_N_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                    list.Add(new object[] { offset });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_LD_MIY_N(int offset)
        {
            var rom = AssembleSource($@"
                org 00h
                LD (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)}), 42h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be unaffected.
                    Carry = true,
                    Sign = true,
                    Zero = true,
                    Subtract = true,
                    HalfCarry = true,
                    ParityOverflow = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x42, state.Memory[0x2234 + offset]);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
