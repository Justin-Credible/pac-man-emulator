using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class PUSH_IY_Tests : BaseTest
    {
        [Fact]
        public void Test_PUSH_IY()
        {
            var rom = AssembleSource($@"
                org 00h
                PUSH IY
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    SP = 0x3000,
                    IY= 0x2477,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x2477, state.Registers.IY);
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
