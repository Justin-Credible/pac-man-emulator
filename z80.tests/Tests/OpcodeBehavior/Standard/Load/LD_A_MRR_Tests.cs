using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_A_MRR_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_LD_A_MRR(RegisterPair registerPair)
        {
            var rom = AssembleSource($@"
                org 00h
                LD A, ({registerPair})
                HALT
            ");

            var registers = new CPURegisters();
            registers[registerPair] = 0x2477;

            var memory = new byte[16384];
            memory[0x2477] = 0x42;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x42, state.Memory[0x2477]);
            Assert.Equal(0x42, state.Registers.A);
            Assert.Equal(0x2477, state.Registers[registerPair]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
