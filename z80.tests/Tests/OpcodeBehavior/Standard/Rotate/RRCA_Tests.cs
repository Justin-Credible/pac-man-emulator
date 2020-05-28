using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RRCA_Tests : BaseTest
    {
        [Fact]
        public void Test_RRCA_SetsCarryFlagTrue()
        {
            var rom = AssembleSource($@"
                org 00h
                RRCA
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b01100101,
                },
                Flags = new ConditionFlags()
                {
                    Carry = false,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b10110010, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_RRCA_SetsCarryFlagFalse()
        {
            var rom = AssembleSource($@"
                org 00h
                RRCA
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b11100100,
                },
                Flags = new ConditionFlags()
                {
                    Carry = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01110010, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
