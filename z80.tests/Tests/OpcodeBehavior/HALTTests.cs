using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class HALTTests : BaseTest
    {
        [Fact]
        public void TestHALT()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                HALT        ; $0002
            ");

            var initialState = new CPUConfig();

            var state = Execute(rom, initialState);

            Assert.Equal(0x0000, state.StackPointer);

            AssertFlagsSame(initialState, state);

            Assert.Equal(3, state.Iterations);
            Assert.Equal(4 + (4*2), state.Cycles);
            Assert.Equal(0x0002, state.ProgramCounter);
        }
    }
}
