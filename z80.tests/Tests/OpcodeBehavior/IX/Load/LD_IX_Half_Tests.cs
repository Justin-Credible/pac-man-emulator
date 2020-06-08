using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_IX_Half_Tests : BaseTest
    {
        [Theory]
        [InlineData(false, 0x42AF, 0x77, 0x4277)]
        [InlineData(true, 0x42AF, 0x77, 0x77AF)]
        public void Test_LD_IX_Half(bool high, UInt16 initialValue, byte valueToSet, UInt16 expectedValue)
        {
            var rom = AssembleSource($@"
                org 00h
                LD {(high ? "IXH" : "IXL")}, {Convert.ToString(valueToSet, 16)}h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IX = initialValue,
                },
                Flags = new ConditionFlags()
                {
                    // Should be unaffected.
                    Carry = true,
                    Sign = true,
                    Zero = true,
                    Parity = true,
                    Subtract = true,
                    AuxCarry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers.IX);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
