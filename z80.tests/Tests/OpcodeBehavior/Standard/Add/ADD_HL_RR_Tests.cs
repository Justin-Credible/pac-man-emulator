using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADD_HL_RR_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_ADD_HL_RR_NoCarry(RegisterPair pair)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD HL, {pair}
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x1212,
                [pair] = 0x3434,
            };

            var flags = new ConditionFlags()
            {
                Sign = true,
                Zero = true,
                AuxCarry = false,
                Parity = true,
                Subtract = true,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x3434, state.Registers[pair]);
            Assert.Equal(0x4646, state.Registers.HL);

            // Ensure these flags remain unchanged.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Parity);

            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            // No carry in this case.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADD_HL_RR_H_NoCarry()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD HL, HL
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x1212,
            };

            var flags = new ConditionFlags()
            {
                Sign = true,
                Zero = true,
                AuxCarry = false,
                Parity = true,
                Subtract = true,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x2424, state.Registers.HL);

            // Ensure these flags remain unchanged.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Parity);

            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            // No carry in this case.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADD_HL_RR_SP_NoCarry()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD HL, SP
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0x1212,
            };

            var flags = new ConditionFlags()
            {
                Sign = true,
                Zero = true,
                AuxCarry = false,
                Parity = true,
                Subtract = true,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                StackPointer = 0x3434,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x3434, state.StackPointer);
            Assert.Equal(0x4646, state.Registers.HL);

            // Ensure these flags remain unchanged.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Parity);

            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            // No carry in this case.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_ADD_HL_RR_Carry(RegisterPair pair)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD HL, {pair}
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0xFFFE,
                [pair] = 0x0005,
            };

            var flags = new ConditionFlags()
            {
                Sign = true,
                Zero = true,
                AuxCarry = false,
                Parity = true,
                Subtract = true,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x0005, state.Registers[pair]);
            Assert.Equal(0x0003, state.Registers.HL);

            // Ensure these flags remain unchanged.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Parity);

            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADD_HL_RR_HL_Carry()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD HL, HL
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0xFFF0,
            };

            var flags = new ConditionFlags()
            {
                Sign = true,
                Zero = true,
                AuxCarry = false,
                Parity = true,
                Subtract = true,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0xFFE0, state.Registers.HL);

            // Ensure these flags remain unchanged.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Parity);

            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_ADD_HL_RR_SP_Carry()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD HL, SP
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 0xFFFE,
            };

            var flags = new ConditionFlags()
            {
                Sign = true,
                Zero = true,
                AuxCarry = false,
                Parity = true,
                Subtract = true,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                StackPointer = 0x0005,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x0005, state.StackPointer);
            Assert.Equal(0x0003, state.Registers.HL);

            // Ensure these flags remain unchanged.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Parity);

            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
