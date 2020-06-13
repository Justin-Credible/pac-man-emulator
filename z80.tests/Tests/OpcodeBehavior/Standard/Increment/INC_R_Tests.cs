using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class INC_R_Tests : BaseTest
    {
        [Theory]
        [InlineData(Register.A)]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_INC_R_NoFlags(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                INC {sourceReg}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [sourceReg] = 0x42,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x43, state.Registers[sourceReg]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Theory]
        [InlineData(Register.A)]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_INC_R_ParityFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                INC {sourceReg}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [sourceReg] = 0x43,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x44, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Theory]
        [InlineData(Register.A)]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_INC_R_SignFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                INC {sourceReg}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [sourceReg] = 0x7F,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x80, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Theory]
        [InlineData(Register.A)]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_INC_R_ZeroButNoCarryFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                INC {sourceReg}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [sourceReg] = 0xFF,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_INC_MHL_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                INC (HL)
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477] = 0x42;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x43, state.Memory[0x2477]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void TestINR_MHL_ParityFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                INC (HL)
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477] = 0x43;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x44, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void TestINR_MHL_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                INC (HL)
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477] = 0x7F;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x80, state.Memory[0x2477]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void TestINR_MHL_ZeroButNoCarryFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                INC (HL)
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477] = 0xFF;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x00, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
