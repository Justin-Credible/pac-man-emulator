using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class DEC_IX_Tests : BaseTest
    {
        [Fact]
        public void Test_DEC_IX()
        {
            var rom = AssembleSource($@"
                org 00h
                DEC IX
                HALT
            ");

            var registers = new CPURegisters()
            {
                IX = 0x38FF,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = new ConditionFlags()
                {
                    // Should be unaffected.
                    Sign = true,
                    Zero = true,
                    AuxCarry = true,
                    Parity = true,
                    Subtract = false,
                    Carry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x38FE, state.Registers.IX);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
