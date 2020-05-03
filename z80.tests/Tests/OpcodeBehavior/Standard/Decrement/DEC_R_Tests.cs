using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class DEC_R_Tests : BaseTest
    {
        [Theory]
        [InlineData(Register.A)]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_DEC_R_NoFlags(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC {sourceReg}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [sourceReg] = 0x44,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x43, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 5, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Theory]
        [InlineData(Register.A)]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_DEC_R_ParityFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC {sourceReg}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [sourceReg] = 0x45,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x44, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 5, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Theory]
        [InlineData(Register.A)]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_DEC_R_SignFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC {sourceReg}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [sourceReg] = 0x81,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x80, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 5, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Theory]
        [InlineData(Register.A)]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_DEC_R_ZeroFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC {sourceReg}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [sourceReg] = 0x01,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 5, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Theory]
        [InlineData(Register.A)]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_DEC_R_NoCarryFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC {sourceReg}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [sourceReg] = 0x00,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0xFF, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 5, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_DEC_R_M_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                DEC (HL)
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477] = 0x44;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x43, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_DEC_R_M_ParityFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                DEC (HL)
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477] = 0x45;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x44, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_DEC_R_M_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                DEC (HL)
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477] = 0x81;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x80, state.Memory[0x2477]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_DEC_R_M_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                DEC (HL)
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477] = 0x01;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x00, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_DEC_R_M_NoCarryFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                DEC (HL)
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477] = 0x00;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0xFF, state.Memory[0x2477]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
