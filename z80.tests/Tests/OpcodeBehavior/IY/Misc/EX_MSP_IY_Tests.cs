using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class EX_MSP_IY_Tests : BaseTest
    {
        [Fact]
        public void Test_EX_MSP_IY()
        {
            var rom = AssembleSource($@"
                org 00h
                EX (SP), IY
                HALT
            ");

            var memory = new byte[16384];
            memory[0x2222] = 0x99;
            memory[0x2223] = 0x88;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x4277,
                    SP = 0x2222,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x8899, state.Registers.IY);
            Assert.Equal(0x77, state.Memory[0x2222]);
            Assert.Equal(0x42, state.Memory[0x2223]);
            Assert.Equal(0x2222, state.Registers.SP);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
