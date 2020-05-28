using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_MRR_A_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_LD_MRR_A(RegisterPair registerPair)
        {
            var rom = AssembleSource($@"
                org 00h
                LD ({registerPair}), A
                HALT
            ");

            var registers = new CPURegisters();
            registers.A = 0x42;
            registers[registerPair] = 0x2477;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x42, state.Memory[0x2477]);
            Assert.Equal(0x42, state.Registers.A);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
