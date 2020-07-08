using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SUB_Tests : BaseTest
    {
        [Theory]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_SUB_NoFlags(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SUB {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x02;
            registers[sourceReg] = 0x01;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x01, state.Registers.A);
            Assert.Equal(0x01, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
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
        public void Test_SUB_CarryFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SUB {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x02;
            registers[sourceReg] = 0x04;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0xFE, state.Registers.A);
            Assert.Equal(0x04, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

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
        public void Test_SUB_ZeroFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SUB {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x02;
            registers[sourceReg] = 0x02;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers.A);
            Assert.Equal(0x02, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
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
        public void Test_SUB_OverflowFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SUB {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x81;
            registers[sourceReg] = 0x02;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x7F, state.Registers.A);
            Assert.Equal(0x02, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
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
        public void Test_SUB_SignFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SUB {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x8D;
            registers[sourceReg] = 0x0A;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x83, state.Registers.A);
            Assert.Equal(0x0A, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_SUB_A_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                SUB A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x80;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Subtract = false,
                }
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
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_SUB_MHL_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                SUB (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x02;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0x01;

            var initialState = new CPUConfig()
            {
                Registers = registers,                Flags = new ConditionFlags()
                {
                    Subtract = false,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x01, state.Registers.A);
            Assert.Equal(0x01, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_SUB_MHL_ZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                SUB (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x40;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0x40;

            var initialState = new CPUConfig()
            {
                Registers = registers,                Flags = new ConditionFlags()
                {
                    Subtract = false,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x00, state.Registers.A);
            Assert.Equal(0x40, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_SUB_MHL_CarryAndSignFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                SUB (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x02;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0x04;

            var initialState = new CPUConfig()
            {
                Registers = registers,                Flags = new ConditionFlags()
                {
                    Subtract = false,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0xFE, state.Registers.A);
            Assert.Equal(0x04, state.Memory[0x2477]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
