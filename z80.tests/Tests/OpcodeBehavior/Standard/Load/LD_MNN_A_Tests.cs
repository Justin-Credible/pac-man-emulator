using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_MNN_A_Tests : BaseTest
    {
        [Fact]
        public void Test_LD_MNN_A()
        {
            var rom = AssembleSource($@"
                org 00h
                LD (2477h), A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x42;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x42, state.Memory[0x2477]);
            Assert.Equal(0x42, state.Registers.A);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 13, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
