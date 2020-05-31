using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RLA_Tests : BaseTest
    {
        [Theory]
        [InlineData(0b11001001, false, 0b10010010, true)]
        [InlineData(0b11001001, true, 0b10010011, true)]
        [InlineData(0b01001000, false, 0b10010000, false)]
        [InlineData(0b01001000, true, 0b10010001, false)]
        public void Test_RLA(byte initialValue, bool initialCarryFlag, byte expectedValue, bool expectedCarryFlag)
        {
            var rom = AssembleSource($@"
                org 00h
                RLA
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
                    Carry = initialCarryFlag,

                    // Should remain unaffected.
                    Sign = false,
                    Zero = true,
                    Parity = false,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedCarryFlag, state.Flags.Carry);

            // Should remain unaffected.
            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
