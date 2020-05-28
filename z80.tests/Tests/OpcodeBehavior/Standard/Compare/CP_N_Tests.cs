using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CP_N_Tests : BaseTest
    {
        [Fact]
        public void Test_CP_N_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                CP 16h
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
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x42, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_CP_N_CarryFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                CP 04h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x02,
                },
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x02, state.Registers.A);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_CP_N_ParityFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                CP 33h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x44,
                },
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x44, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_CP_N_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                CP 0Ah
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x8D,
                },
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x8D, state.Registers.A);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_CP_N_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                CP 02h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x02,
                },
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x02, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
