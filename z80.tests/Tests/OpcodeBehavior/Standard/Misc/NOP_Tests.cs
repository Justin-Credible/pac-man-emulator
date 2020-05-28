using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class NOP_Tests : BaseTest
    {
        [Fact]
        public void Test_NOP()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                NOP         ; $0002
                NOP         ; $0003
                HALT        ; $0004
            ");

            var initialState = new CPUConfig();

            var state = Execute(rom, initialState);

            Assert.Equal(0x0000, state.Registers.SP);

            AssertFlagsSame(initialState, state);

            Assert.Equal(5, state.Iterations);
            Assert.Equal(4 + (4*4), state.Cycles);
            Assert.Equal(0x0004, state.Registers.PC);
        }
    }
}
