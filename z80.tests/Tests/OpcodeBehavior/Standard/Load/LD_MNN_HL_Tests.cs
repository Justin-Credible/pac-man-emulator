using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_MNN_HL_Tests : BaseTest
    {
        [Fact]
        public void Test_LD_MNN_HL()
        {
            var rom = AssembleSource($@"
                org 00h
                LD (2477h), HL
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    H = 0x66,
                    L = 0x77,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x77, state.Memory[0x2477]);
            Assert.Equal(0x66, state.Memory[0x2478]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
