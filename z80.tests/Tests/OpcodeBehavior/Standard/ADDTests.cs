using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADDTests : BaseTest
    {
        [Theory]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void TestADD_NoFlags(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x42;
            registers[sourceReg] = 0x16;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    // Ensure this is flipped to zero because this was an addition.
                    Subtract = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x58, state.Registers.A);
            Assert.Equal(0x16, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Theory]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void TestADD_CarryFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0xFE;
            registers[sourceReg] = 0x04;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x02, state.Registers.A);
            Assert.Equal(0x04, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Theory]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void TestADD_ZeroFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0xFE;
            registers[sourceReg] = 0x02;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers.A);
            Assert.Equal(0x02, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Theory]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void TestADD_ParityFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x44;
            registers[sourceReg] = 0x33;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x77, state.Registers.A);
            Assert.Equal(0x33, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Theory]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void TestADD_SignFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x4D;
            registers[sourceReg] = 0x3A;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x87, state.Registers.A);
            Assert.Equal(0x3A, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void TestADD_A_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x02;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x04, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void TestADD_A_ZeroAndCarryFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x80;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void TestADD_A_SignAndParityFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x44;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x88, state.Registers.A);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void TestADD_M_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x42;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0x16;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x58, state.Registers.A);
            Assert.Equal(0x16, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void TestADD_M_ZeroAndCarryFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0xC0;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0x40;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x00, state.Registers.A);
            Assert.Equal(0x40, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void TestADD_M_SignAndParityFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x47;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0x44;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x8B, state.Registers.A);
            Assert.Equal(0x44, state.Memory[0x2477]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
