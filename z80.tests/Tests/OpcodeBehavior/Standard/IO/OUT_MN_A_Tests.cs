using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class OUT_MN_A_TEsts : BaseTest
    {
        [Fact]
        public void Test_OUT_MN_A()
        {
            var rom = AssembleSource($@"
                org 00h
                OUT (3), A
                HALT
            ");

            var initialState = new CPUConfig();

            initialState.Registers = new CPURegisters()
            {
                A = 0x42,
            };

            var cpu = new CPU(initialState);

            byte actualData = 0;
            var actualDeviceID = -1;

            cpu.OnDeviceWrite += (int deviceID, byte data) => {
                actualDeviceID = deviceID;
                actualData = data;
            };

            var state = Execute(rom, cpu);

            Assert.Equal(0x42, actualData);
            Assert.Equal(3, actualDeviceID);

            Assert.Equal(0x42, state.Registers.A);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_OUT_MN_A_DoesNotThrowExceptionIfNoHandlersPresent()
        {
            var rom = AssembleSource($@"
                org 00h
                OUT (3), A
                HALT
            ");

            var initialState = new CPUConfig();

            initialState.Registers = new CPURegisters()
            {
                A = 0x42,
            };

            var cpu = new CPU(initialState);

            var state = Execute(rom, cpu);

            Assert.Equal(0x42, state.Registers.A);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
