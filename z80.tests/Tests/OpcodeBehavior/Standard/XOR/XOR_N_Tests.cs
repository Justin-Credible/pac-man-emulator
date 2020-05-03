using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class XOR_N_Tests : BaseTest
    {
        [Fact]
        public void Test_XOR_N_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                XOR 10110001B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b11010101,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01100100, state.Registers.A);

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
        public void Test_XOR_N_ParityFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                XOR 01001001B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b01101101,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b00100100, state.Registers.A);

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
        public void Test_XOR_N_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                XOR 11001001B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b01101101,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b10100100, state.Registers.A);

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
        public void Test_XOR_N_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                XOR 11001001B
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b11001001,
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
