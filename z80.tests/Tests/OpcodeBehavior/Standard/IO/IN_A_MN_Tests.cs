using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class IN_A_MN_Tests : BaseTest
    {
        [Fact]
        public void Test_IN_A_MN()
        {
            var rom = AssembleSource($@"
                org 00h
                IN A, (2)
                HALT
            ");

            var initialState = GetCPUConfig();

            initialState.Registers = new CPURegisters()
            {
                A = 0x00,
            };

            var cpu = new CPU(initialState);

            cpu.OnDeviceRead += (int deviceID) => {

                if (deviceID == 1) {
                    return 0x77;
                }
                else if (deviceID == 2) {
                    return 0x66;
                }
                else {
                    return 0x55;
                }
            };

            var state = Execute(rom, cpu);

            Assert.Equal(0x66, state.Registers.A);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_IN_A_MN_DoesNotThrowExceptionIfNoHandlersPresent()
        {
            var rom = AssembleSource($@"
                org 00h
                IN A, (2)
                HALT
            ");

            var initialState = GetCPUConfig();

            initialState.Registers = new CPURegisters()
            {
                A = 0x11,
            };

            var cpu = new CPU(initialState);

            var state = Execute(rom, cpu);

            Assert.Equal(0x00, state.Registers.A);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }
    }
}
