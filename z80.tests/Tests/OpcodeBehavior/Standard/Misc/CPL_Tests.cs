using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CPL_Tests : BaseTest
    {
        [Fact]
        public void Test_CPL_DoesNotSetParityOrSignFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                CPL
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b01011010,
                },
                Flags = new ConditionFlags()
                {
                    // Should be set.
                    HalfCarry = false,
                    Subtract = false,

                    // Should be unaffected.
                    Sign = true,
                    Zero = true,
                    ParityOverflow = true,
                    Carry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b10100101, state.Registers.A);

            // Should be set.
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.HalfCarry);

            // Should be unaffected
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_CPL_DoesNotSetZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                CPL
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b11111111,
                },
                Flags = new ConditionFlags()
                {
                    // Should be set.
                    HalfCarry = false,
                    Subtract = false,

                    // Should be unaffected.
                    Sign = true,
                    Zero = true,
                    ParityOverflow = true,
                    Carry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b00000000, state.Registers.A);

            // Should be set.
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.HalfCarry);

            // Should be unaffected
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
