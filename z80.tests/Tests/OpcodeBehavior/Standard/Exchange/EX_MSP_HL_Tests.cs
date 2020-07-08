using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class EX_MSP_HL_Tests : BaseTest
    {
        [Fact]
        public void Test_EX_MSP_HL()
        {
            var rom = AssembleSource($@"
                org 00h
                EX (SP), HL
                HALT
            ");

            var memory = new byte[16384];
            memory[0x2222] = 0x99;
            memory[0x2223] = 0x88;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    H = 0x42,
                    L = 0x77,
                    SP = 0x2222,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x88, state.Registers.H);
            Assert.Equal(0x99, state.Registers.L);
            Assert.Equal(0x77, state.Memory[0x2222]);
            Assert.Equal(0x42, state.Memory[0x2223]);
            Assert.Equal(0x2222, state.Registers.SP);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
