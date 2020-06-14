using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADC_A_N_Tests : BaseTest
    {
        [Fact]
        public void Test_ADC_A_N_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, 18h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x42,
                },
                Flags = new ConditionFlags()
                {
                    Carry = true,

                    // Ensure this is flipped to zero because this was an addition.
                    Subtract = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x5B, state.Registers.A);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_ADC_A_N_OverflowFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, 124
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 3,
                },
                Flags = new ConditionFlags()
                {
                    Carry = true,

                    // Ensure this is flipped to zero because this was an addition.
                    Subtract = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(128, state.Registers.A);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_ADC_A_N_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, 4Ah
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x42,
                },
                Flags = new ConditionFlags()
                {
                    // Ensure this is flipped to zero because this was an addition.
                    Subtract = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x8C, state.Registers.A);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_ADC_A_N_CarryFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, 1Ch
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0xEE,
                },
                Flags = new ConditionFlags()
                {
                    Carry = true,

                    // Ensure this is flipped to zero because this was an addition.
                    Subtract = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x0B, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_ADC_A_N_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, 11h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0xEE,
                },
                Flags = new ConditionFlags()
                {
                    Carry = true,

                    // Ensure this is flipped to zero because this was an addition.
                    Subtract = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
