using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class POP_IY_Tests : BaseTest
    {
        [Fact]
        public void Test_POP_IY()
        {
            var rom = AssembleSource($@"
                org 00h
                POP IY
                HALT
            ");

            var memory = new byte[16384];
            memory[0x2FFE] = 0x77;
            memory[0x2FFF] = 0x24;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    SP = 0x2FFE
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2477, state.Registers.IY);
            Assert.Equal(0x00, state.Memory[0x3000]);
            Assert.Equal(0x24, state.Memory[0x2FFF]);
            Assert.Equal(0x77, state.Memory[0x2FFE]);
            Assert.Equal(0x3000, state.Registers.SP);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 14, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
