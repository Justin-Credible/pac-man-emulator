using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class PUSH_IX_Tests : BaseTest
    {
        [Fact]
        public void Test_PUSH_IX()
        {
            var rom = AssembleSource($@"
                org 00h
                PUSH IX
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    SP = 0x3000,
                    IX= 0x2477,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x2477, state.Registers.IX);
            Assert.Equal(0x00, state.Memory[0x3000]);
            Assert.Equal(0x24, state.Memory[0x2FFF]);
            Assert.Equal(0x77, state.Memory[0x2FFE]);
            Assert.Equal(0x2FFE, state.Registers.SP);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
