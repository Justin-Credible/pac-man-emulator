using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class OR_Tests : BaseTest
    {
        [Theory]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_OR_NoFlags(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                OR {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b01100101;
            registers[sourceReg] = 0b01101100;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01101101, state.Registers.A);
            Assert.Equal(0b01101100, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
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
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_OR_SignFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                OR {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b11100101;
            registers[sourceReg] = 0b00101110;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b11101111, state.Registers.A);
            Assert.Equal(0b00101110, state.Registers[sourceReg]);

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
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_OR_ParityFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                OR {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A =          0b01100101;
            registers[sourceReg] = 0b00101010;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01101111, state.Registers.A);
            Assert.Equal(0b00101010, state.Registers[sourceReg]);

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
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_OR_ZeroFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                OR {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b00000000;
            registers[sourceReg] = 0b00000000;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b00000000, state.Registers.A);
            Assert.Equal(0b00000000, state.Registers[sourceReg]);

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
        public void Test_OR_A_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                OR A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b01100100;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01100100, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_OR_A_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                OR A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b10100100;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b10100100, state.Registers.A);

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

        [Fact]
        public void Test_OR_A_ParityFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                OR A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b01101100;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b01101100, state.Registers.A);

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

        [Fact]
        public void Test_OR_A_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                OR A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b00000000;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0b00000000, state.Registers.A);

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
        public void Test_OR_MHL_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                OR (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b01100101;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0b01101100;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b01101101, state.Registers.A);
            Assert.Equal(0b01101100, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_OR_MHL_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                OR (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b11100101;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0b00101110;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b11101111, state.Registers.A);
            Assert.Equal(0b00101110, state.Memory[0x2477]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_OR_MHL_ParityFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                OR (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b11100100;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0b00101010;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b11101110, state.Registers.A);
            Assert.Equal(0b00101010, state.Memory[0x2477]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_OR_MHL_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                OR (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0b00000000;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0b00000000;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b00000000, state.Registers.A);
            Assert.Equal(0b00000000, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
