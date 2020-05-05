using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class PUSH_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.HL)]
        public void Test_PUSH_RR(RegisterPair pair)
        {
            var rom = AssembleSource($@"
                org 00h
                PUSH {pair}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [pair] = 0x2477,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                StackPointer = 0x3000,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x2477, state.Registers[pair]);
            Assert.Equal(0x00, state.Memory[0x3000]);
            Assert.Equal(0x24, state.Memory[0x2FFF]);
            Assert.Equal(0x77, state.Memory[0x2FFE]);
            Assert.Equal(0x2FFE, state.StackPointer);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void Test_PUSH_AF()
        {
            var rom = AssembleSource($@"
                org 00h
                PUSH AF
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x42;

            // 7 6 5 4 3 2 1 0
            // S Z - H - P N C
            // 1 1 0 1 0 1 1 1
            //       D 7
            var flags = new ConditionFlags()
            {
                Sign = true,
                Zero = true,
                AuxCarry = true,
                Parity = true,
                Subtract = true,
                Carry = true,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
                StackPointer = 0x3000,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x42, state.Registers.A);
            Assert.Equal(0x00, state.Memory[0x3000]);
            Assert.Equal(0x42, state.Memory[0x2FFF]);
            Assert.Equal(0xD7, state.Memory[0x2FFE]);
            Assert.Equal(0x2FFE, state.StackPointer);

            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
