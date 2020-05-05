using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_SP_HL_Tests : BaseTest
    {
        [Fact]
        public void Test_LD_SP_HL()
        {
            var rom = AssembleSource($@"
                org 00h
                LD SP, HL
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    HL = 0x2477,
                },
            };

            var state = Execute(rom, initialState);

            AssertFlagsFalse(state);

            Assert.Equal(0x2477, state.StackPointer);
            Assert.Equal(0x2477, state.Registers.HL);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 5, state.Cycles);
            Assert.Equal(0x0001, state.ProgramCounter);
        }
    }
}
