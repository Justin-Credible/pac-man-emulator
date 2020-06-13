using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RLCA_Tests : BaseTest
    {
        [Fact]
        public void Test_RLCA_SetsCarryFlagTrue()
        {
            var rom = AssembleSource($@"
                org 00h
                RLCA
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
                    // Should be affected.
                    Carry = false,

                    // Should remain unaffected.
                    Sign = false,
                    Zero = true,
                    ParityOverflow = false,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b11001001, state.Registers.A);

            // Should be affected.
            Assert.True(state.Flags.Carry);

            // Should remain unaffected.
            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_RLCA_SetsCarryFlagFalse()
        {
            var rom = AssembleSource($@"
                org 00h
                RLCA
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
                    // Should be affected.
                    Carry = true,

                    // Should remain unaffected.
                    Sign = false,
                    Zero = true,
                    ParityOverflow = false,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b11001010, state.Registers.A);

            // Should be affected.
            Assert.False(state.Flags.Carry);

            // Should remain unaffected.
            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
