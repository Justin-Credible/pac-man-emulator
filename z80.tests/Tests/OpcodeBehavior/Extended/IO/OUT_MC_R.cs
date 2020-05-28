using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class OUT_MC_R : BaseTest
    {
        [Theory]
        [InlineData(Register.A, 2, 33)]
        [InlineData(Register.B, 4, 66)]
        [InlineData(Register.C, 2, 2)]
        [InlineData(Register.D, 9, 11)]
        [InlineData(Register.E, 1, 54)]
        [InlineData(Register.H, 254, 254)]
        [InlineData(Register.L, 144, 111)]
        public void Test_OUT_MC_R(Register register, byte deviceId, byte value)
        {
            var rom = AssembleSource($@"
                org 00h
                OUT (C), {register}
                HALT
            ");

            var initialState = new CPUConfig();

            initialState.Registers = new CPURegisters();
            initialState.Registers.C = deviceId;
            initialState.Registers[register] = value;

            var cpu = new CPU(initialState);

            byte actualData = 0;
            var actualDeviceID = -1;

            cpu.OnDeviceWrite += (int deviceID, byte data) => {
                actualDeviceID = deviceID;
                actualData = data;
            };

            var state = Execute(rom, cpu);

            Assert.Equal(value, actualData);
            Assert.Equal(deviceId, actualDeviceID);

            Assert.Equal(deviceId, state.Registers.C);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 12, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_OUT_MC_0()
        {
            var rom = AssembleSource($@"
                org 00h
                OUT (C), 0
                HALT
            ");

            var initialState = new CPUConfig();

            initialState.Registers = new CPURegisters();
            initialState.Registers.C = 213;

            var cpu = new CPU(initialState);

            byte actualData = 0;
            var actualDeviceID = -1;

            cpu.OnDeviceWrite += (int deviceID, byte data) => {
                actualDeviceID = deviceID;
                actualData = data;
            };

            var state = Execute(rom, cpu);

            Assert.Equal(0, actualData);
            Assert.Equal(213, actualDeviceID);

            Assert.Equal(213, state.Registers.C);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 12, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }

        [Fact]
        public void Test_OUT_MC_R_DoesNotThrowExceptionIfNoHandlersPresent()
        {
            var rom = AssembleSource($@"
                org 00h
                OUT (C), B
                HALT
            ");

            var initialState = new CPUConfig();

            initialState.Registers = new CPURegisters();
            initialState.Registers.C = 123;
            initialState.Registers.B = 44;

            var cpu = new CPU(initialState);

            var state = Execute(rom, cpu);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 12, state.Cycles);
            Assert.Equal(0x02, state.ProgramCounter);
        }
    }
}
