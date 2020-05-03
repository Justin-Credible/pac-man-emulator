using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class XOR_Tests : BaseTest
    {
        [Theory]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_XOR_NoFlags(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                XOR {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b11010101;
            registers[sourceReg] = 0b10110001;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01100100, state.Registers.A);
            Assert.Equal(0b10110001, state.Registers[sourceReg]);

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
        public void Test_XOR_SignFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                XOR {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b01101101;
            registers[sourceReg] = 0b11001001;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b10100100, state.Registers.A);
            Assert.Equal(0b11001001, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
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
        public void Test_XOR_ParityFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                XOR {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b01101101;
            registers[sourceReg] = 0b01001001;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b00100100, state.Registers.A);
            Assert.Equal(0b01001001, state.Registers[sourceReg]);

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
        public void Test_XOR_ZeroFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                XOR {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b11001001;
            registers[sourceReg] = 0b11001001;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b00000000, state.Registers.A);
            Assert.Equal(0b11001001, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_XOR_A_ZeroAndParityFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                XOR A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b01100100;

            var initialState = new CPUConfig()
            {
                Registers = registers,
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
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_XOR_MHL_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                XOR (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b01001010;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0b00101110;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b01100100, state.Registers.A);
            Assert.Equal(0b00101110, state.Memory[0x2477]);

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
        public void Test_XOR_MHL_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                XOR (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b11001001;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0b01101101;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b10100100, state.Registers.A);
            Assert.Equal(0b01101101, state.Memory[0x2477]);

            Assert.True(state.Flags.Sign);
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
        public void Test_XOR_MHL_ParityFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                XOR (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b10000110;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0b10100010;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b00100100, state.Registers.A);
            Assert.Equal(0b10100010, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_XOR_MHL_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                XOR (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b11000101;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0b11000101;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b00000000, state.Registers.A);
            Assert.Equal(0b11000101, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
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
