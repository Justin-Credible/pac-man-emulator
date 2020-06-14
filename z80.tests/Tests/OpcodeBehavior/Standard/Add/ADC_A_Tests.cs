using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADC_A_Tests : BaseTest
    {
        [Theory]
        [InlineData(Register.B)]
        [InlineData(Register.C)]
        [InlineData(Register.D)]
        [InlineData(Register.E)]
        [InlineData(Register.H)]
        [InlineData(Register.L)]
        public void Test_ADC_A_NoFlags(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x42;
            registers[sourceReg] = 0x15;

            var flags = new ConditionFlags()
            {
                Carry = true,

                // Ensure this is flipped to zero because this was an addition.
                Subtract = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x58, state.Registers.A);
            Assert.Equal(0x15, state.Registers[sourceReg]);

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
        public void Test_ADC_A_CarryFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0xFE;
            registers[sourceReg] = 0x03;

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x02, state.Registers.A);
            Assert.Equal(0x03, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
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
        public void Test_ADC_A_ZeroFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0xFE;
            registers[sourceReg] = 0x01;

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers.A);
            Assert.Equal(0x01, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
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
        public void Test_ADC_A_OverflowFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 125;
            registers[sourceReg] = 3;

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(129, state.Registers.A);
            Assert.Equal(3, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
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
        public void Test_ADC_A_SignFlag(Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x4D;
            registers[sourceReg] = 0x39;

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x87, state.Registers.A);
            Assert.Equal(0x39, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_ADC_A_A_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x03;

            var flags = new ConditionFlags()
            {
                Carry = true,

                // Ensure this is flipped to zero because this was an addition.
                Subtract = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x07, state.Registers.A);

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
        public void Test_ADC_A_A_CarryFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x80;

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x01, state.Registers.A);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_ADC_A_A_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x44;

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };
            var state = Execute(rom, initialState);

            Assert.Equal(0x89, state.Registers.A);

            Assert.True(state.Flags.Sign);
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
        public void Test_ADC_A_A_HL_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x42;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0x15;

            var flags = new ConditionFlags()
            {
                Carry = true,

                // Ensure this is flipped to zero because this was an addition.
                Subtract = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x58, state.Registers.A);
            Assert.Equal(0x15, state.Memory[0x2477]);

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
        public void Test_ADC_A_A_HL_ZeroAndCarryFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0xC0;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0x3F;

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x00, state.Registers.A);
            Assert.Equal(0x3F, state.Memory[0x2477]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_ADC_A_A_HL_SignAndOverflowFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC A, (HL)
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 127;
            registers.HL = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 2;

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(130, state.Registers.A);
            Assert.Equal(2, state.Memory[0x2477]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
