using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class EX_DE_HL_Tests : BaseTest
    {
        [Fact]
        public void TestXCHG_NoFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                EX DE, HL
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    H = 0x42,
                    D = 0x99,
                    L = 0x77,
                    E = 0x88,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x99, state.Registers.H);
            Assert.Equal(0x42, state.Registers.D);
            Assert.Equal(0x88, state.Registers.L);
            Assert.Equal(0x77, state.Registers.E);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 5, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
