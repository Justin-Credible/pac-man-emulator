using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class NEG_Tests : BaseTest
    {
        [Fact]
        public void Test_NEG()
        {
            var rom = AssembleSource($@"
                org 00h
                NEG
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b10011000,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Sign = true,
                    Zero = true,
                    Subtract = false,
                    Carry = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01101000, state.Registers.A);

            // TODO: Test other flags.
            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_NEG_Handles80h()
        {
            var rom = AssembleSource($@"
                org 00h
                NEG
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x80,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Sign = false,
                    Zero = true,
                    Subtract = false,
                    Carry = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x80, state.Registers.A);

            // TODO: Test other flags.
            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_NEG_Handles00h()
        {
            var rom = AssembleSource($@"
                org 00h
                NEG
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x00,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Sign = true,
                    Zero = false,
                    Subtract = false,
                    Carry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers.A);

            // TODO: Test other flags.
            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }
    }
}
