using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SUB_N_Tests : BaseTest
    {
        [Fact]
        public void Test_SUB_N_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                SUB 01h
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

            Assert.Equal(0x01, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_SUB_N_OverflowFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                SUB 02h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x81,
                },
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x7F, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_SUB_N_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                SUB 16h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0xFF,
                },
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0xE9, state.Registers.A);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_SUB_N_CarryFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                SUB 07h
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

            Assert.Equal(0xFB, state.Registers.A);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_SUB_N_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                SUB 12h
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x12,
                },
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
