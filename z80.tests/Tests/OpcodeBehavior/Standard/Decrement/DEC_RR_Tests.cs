using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class DEC_RR_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.HL)]
        public void Test_DEC_RR(RegisterPair pair)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC {pair}
                DEC {pair}
                DEC {pair}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [pair] = 0x3902,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Sign = true,
                    Zero = true,
                    AuxCarry = true,
                    Parity = true,
                    Subtract = true,
                    Carry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x38FF, state.Registers[pair]);

            // This opcode shouldn't affect flags.
            AssertFlagsSame(initialState, state);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(4 + (6*3), state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }

        [Fact]
        public void Test_DEC_SP()
        {
            var rom = AssembleSource($@"
                org 00h
                DEC SP
                DEC SP
                DEC SP
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    SP = 0x3902
                },
                Flags = new ConditionFlags()
                {
                    Sign = true,
                    Zero = true,
                    AuxCarry = true,
                    Parity = true,
                    Subtract = true,
                    Carry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x38FF, state.Registers.SP);

            // This opcode shouldn't affect flags.
            AssertFlagsSame(initialState, state);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(4 + (6*3), state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
