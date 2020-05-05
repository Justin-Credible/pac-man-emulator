using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class DI_Tests : BaseTest
    {
        [Fact]
        public void Test_DI()
        {
            var rom = AssembleSource($@"
                org 00h
                DI
                HALT
            ");

            var initialState = new CPUConfig()
            {
                InterruptsEnabled = true,
            };

            var state = Execute(rom, initialState);

            Assert.False(state.InterruptsEnabled);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
