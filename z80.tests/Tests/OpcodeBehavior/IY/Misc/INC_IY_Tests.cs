using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class INC_IY_Tests : BaseTest
    {
        [Fact]
        public void Test_INC_IY()
        {
            var rom = AssembleSource($@"
                org 00h
                INC IY
                HALT
            ");

            var registers = new CPURegisters()
            {
                IY = 0x38FF,
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
                    Subtract = true,
                    Carry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x3900, state.Registers.IY);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
