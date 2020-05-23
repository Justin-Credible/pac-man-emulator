using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADC_HL_RR_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_ADC_HL_NoFlags(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x42;
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

            Assert.Equal(0x58, state.Registers.HL);
            Assert.Equal(0x15, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_ADC_HL_CarryFlag(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0xFFFE;
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

            Assert.Equal(0x02, state.Registers.HL);
            Assert.Equal(0x03, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_ADC_HL_ZeroFlag(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0xFFFE;
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

            Assert.Equal(0x00, state.Registers.HL);
            Assert.Equal(0x01, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_ADC_HL_ParityFlag(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x44;
            registers[sourceReg] = 0x32;

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

            Assert.Equal(0x77, state.Registers.HL);
            Assert.Equal(0x32, state.Registers[sourceReg]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_ADC_HL_SignFlag(RegisterPair sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, {sourceReg}
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x7F45; // 01111111 01000101 = 32,581
            registers[sourceReg] = 0xFF; // 255

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

            Assert.Equal(0x8045, state.Registers.HL); // 10000000 01000100 = 32,837
            Assert.Equal(0xFF, state.Registers[sourceReg]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADC_HL_HL_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, HL
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x03;

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

            Assert.Equal(0x07, state.Registers.HL);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADC_HL_HL_CarryFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, HL
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x8000;

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

            Assert.Equal(0x01, state.Registers.HL);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADC_HL_HL_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, HL
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x4400;

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

            Assert.Equal(0x8801, state.Registers.HL);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADC_HL_SP_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, SP
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x03;

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
                StackPointer = 0x03,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x07, state.Registers.HL);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADC_HL_SP_CarryFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, SP
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x8000;

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
                StackPointer = 0x8000,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x0001, state.Registers.HL);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADC_HL_SP_SignFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                ADC HL, SP
                HALT
            ");

            var registers = new CPURegisters();
            registers.HL = 0x7F45; // 01111111 01000101 = 32,581

            var flags = new ConditionFlags()
            {
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
                StackPointer = 0xFF,
            };
            var state = Execute(rom, initialState);

            Assert.Equal(0x8045, state.Registers.HL);  // 10000000 01000100 = 32,837

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }
    }
}
