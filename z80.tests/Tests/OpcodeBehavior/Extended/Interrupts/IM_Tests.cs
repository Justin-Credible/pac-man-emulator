using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class IM_Tests : BaseTest
    {
        [Theory]
        [InlineData(0, InterruptMode.Zero)]
        [InlineData(1, InterruptMode.One)]
        [InlineData(2, InterruptMode.Two)]
        public void Test_IM(int modeNumber, InterruptMode mode)
        {
            var rom = AssembleSource($@"
                org 00h
                IM {modeNumber}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                InterruptsEnabled = false,
                InterruptsEnabledPreviousValue = false,
            };

            var cpu = new CPU(initialState);

            var state = Execute(rom, cpu);

            // Ensure the mode was set.
            Assert.Equal(mode, cpu.InterruptMode);

            // Setting mode does not enable interrupts.
            Assert.False(cpu.InterruptsEnabled);
            Assert.False(cpu.InterruptsEnabledPreviousValue);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }
    }
}
