using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SBC_HL_RR_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.SP)]
        public void Test_SBC_HL_NoFlags(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SBC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x42;
            registers[sourceReg] = 0x15;

            var flags = new ConditionFlags()
            {
                Carry = true,
                Subtract = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x2C, state.Registers.HL);
            Assert.Equal(0x15, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.SP)]
        public void Test_SBC_HL_CarryFlag(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SBC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x02;
            registers[sourceReg] = 0x03;

            var flags = new ConditionFlags()
            {
                Carry = true,
                Subtract = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0xFFFE, state.Registers.HL);
            Assert.Equal(0x03, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.SP)]
        public void Test_SBC_HL_CarryFlag_CausedByExtraMinusOneFromFlag(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SBC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x02;
            registers[sourceReg] = 0x02;

            var flags = new ConditionFlags()
            {
                Carry = true,
                Subtract = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0xFFFF, state.Registers.HL);
            Assert.Equal(0x02, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.SP)]
        public void Test_SBC_HL_ZeroFlag(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SBC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x02;
            registers[sourceReg] = 0x01;

            var flags = new ConditionFlags()
            {
                Carry = true,
                Subtract = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x00, state.Registers.HL);
            Assert.Equal(0x01, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.SP)]
        public void Test_SBC_HL_OverflowFlag(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SBC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x8004;
            registers[sourceReg] = 0x0006;

            var flags = new ConditionFlags()
            {
                Carry = true,
                Subtract = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x7FFD, state.Registers.HL);
            Assert.Equal(0x0006, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.SP)]
        public void Test_SBC_HL_SignFlag(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                SBC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x8D8D;
            registers[sourceReg] = 0x09;

            var flags = new ConditionFlags()
            {
                Carry = true,
                Subtract = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x8D83, state.Registers.HL);
            Assert.Equal(0x09, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_SBC_HL_HL_SignCarryFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                SBC HL, HL
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x8000;

            var flags = new ConditionFlags()
            {
                Carry = true,
                Subtract = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0xFFFF, state.Registers.HL);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
