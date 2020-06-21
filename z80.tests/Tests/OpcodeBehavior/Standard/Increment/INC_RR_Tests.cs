using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class INC_RR_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.HL)]
        [InlineData(RegisterPair.SP)]
        public void Test_INC_RR(RegisterPair pair)
        {
            var rom = AssembleSource($@"
                org 00h
                INC {pair}
                INC {pair}
                INC {pair}
                HALT
            ");

            var registers = new CPURegisters()
            {
                [pair] = 0x38FF,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    Sign = true,
                    Zero = true,
                    HalfCarry = true,
                    ParityOverflow = true,
                    Subtract = true,
                    Carry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x3902, state.Registers[pair]);

            // This opcode shouldn't affect flags.
            AssertFlagsSame(initialState, state);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(4 + (6*3), state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
