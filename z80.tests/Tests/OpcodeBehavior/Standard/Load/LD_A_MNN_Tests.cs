using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_A_MNN_Tests : BaseTest
    {
        [Fact]
        public void TestLDA()
        {
            var rom = AssembleSource($@"
                org 00h
                LD A, (2477h)
                HALT
            ");

            var memory = new byte[16384];
            memory[0x2477] = 0x42;

            var initialState = new CPUConfig()
            {            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x42, state.Memory[0x2477]);
            Assert.Equal(0x42, state.Registers.A);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 13, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
