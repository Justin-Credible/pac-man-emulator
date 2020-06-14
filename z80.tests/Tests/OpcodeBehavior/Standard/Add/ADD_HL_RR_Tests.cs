using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADD_HL_RR_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.SP)]
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
                HalfCarry = false,
                ParityOverflow = true,
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
            Assert.True(state.Flags.ParityOverflow);

            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            // No carry in this case.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_ADD_HL_HL_NoCarry()
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
                HalfCarry = false,
                ParityOverflow = true,
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
            Assert.True(state.Flags.ParityOverflow);

            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            // No carry in this case.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.SP)]
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
                HalfCarry = false,
                ParityOverflow = true,
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
            Assert.True(state.Flags.ParityOverflow);

            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_ADD_HL_HL_Carry()
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
                HalfCarry = false,
                ParityOverflow = true,
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
            Assert.True(state.Flags.ParityOverflow);

            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.SP)]
        public void Test_ADD_HL_RR_HalfCarry(RegisterPair pair)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD HL, {pair}
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 3840,
                [pair] = 256,
            };

            var flags = new ConditionFlags()
            {
                Sign = true,
                Zero = true,
                HalfCarry = false,
                ParityOverflow = true,
                Subtract = true,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(256, state.Registers[pair]);
            Assert.Equal(4096, state.Registers.HL);

            // Ensure these flags remain unchanged.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);

            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_ADD_HL_HL_HalfCarry()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD HL, HL
                HALT
            ");

            var registers = new CPURegisters()
            {
                HL = 2048,
            };

            var flags = new ConditionFlags()
            {
                Sign = true,
                Zero = true,
                HalfCarry = false,
                ParityOverflow = true,
                Subtract = true,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(4096, state.Registers.HL);

            // Ensure these flags remain unchanged.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);

            Assert.True(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
