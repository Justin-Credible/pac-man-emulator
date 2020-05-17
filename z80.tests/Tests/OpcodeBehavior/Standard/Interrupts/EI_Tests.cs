using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class EI_Tests : BaseTest
    {
        [Fact]
        public void Test_EI()
        {
            var rom = AssembleSource($@"
                org 00h
                EI
                HALT
            ");

            var initialState = new CPUConfig()
            {
                InterruptsEnabled = false,
                InterruptsEnabledPreviousValue = false,
            };

            var state = Execute(rom, initialState);

            Assert.True(state.InterruptsEnabled);
            Assert.True(state.InterruptsEnabledPreviousValue);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
