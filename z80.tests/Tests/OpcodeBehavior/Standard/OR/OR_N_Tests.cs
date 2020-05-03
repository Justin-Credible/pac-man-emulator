using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class OR_N_Tests : BaseTest
    {
        [Fact]
        public void Test_OR_N_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                OR 01101100B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b01100101,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01101101, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_OR_N_ParityFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                OR 00101010B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b01100101,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01101111, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_OR_N_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                OR 10100100B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b01100101,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b11100101, state.Registers.A);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_OR_N_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                OR 00000000B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b00000000,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b00000000, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }
    }
}
