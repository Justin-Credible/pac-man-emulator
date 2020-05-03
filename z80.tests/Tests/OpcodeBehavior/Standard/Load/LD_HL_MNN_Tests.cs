using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_HL_MNN_Tests : BaseTest
    {
        [Fact]
        public void Test_LD_HL_MNN()
        {
            var rom = AssembleSource($@"
                org 00h
                LD HL, (2477h)
                HALT
            ");

            var memory = new byte[16384];
            memory[0x2477] = 0x77;
            memory[0x2478] = 0x66;

            var initialState = new CPUConfig()
            {
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x66, state.Registers.H);
            Assert.Equal(0x77, state.Registers.L);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x03, state.ProgramCounter);
        }
    }
}
