using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class IN_R_MC_Tests : BaseTest
    {
        [Theory]
        [InlineData(Register.A, 2, 0, false, true, true)]
        [InlineData(Register.B, 4, 66, false, false, true)]
        [InlineData(Register.C, 2, 2, false, false, false)]
        [InlineData(Register.D, 9, 11, false, false, false)]
        [InlineData(Register.E, 1, 54, false, false, true)]
        [InlineData(Register.H, 254, 254, true, false, false)]
        [InlineData(Register.L, 144, 111, false, false, true)]
        public void Test_IN_R_MC(Register register, byte deviceId, byte value, bool sign, bool zero, bool parity)
        {
            var rom = AssembleSource($@"
                org 00h
                IN {register}, (C)
                HALT
            ");

            var initialState = new CPUConfig();

            initialState.Flags = new ConditionFlags()
            {
                // Should remain unchanged.
                Carry = true,

                // Should be reset.
                Subtract = true,
                HalfCarry = true,
            };

            initialState.Registers = new CPURegisters();
            initialState.Registers.C = deviceId;

            var cpu = new CPU(initialState);

            var actualDeviceId = 0;

            cpu.OnDeviceRead += (int deviceID) => {
                actualDeviceId = deviceID;
                return value;
            };

            var state = Execute(rom, cpu);

            Assert.Equal(deviceId, state.Registers.C);
            Assert.Equal(deviceId, actualDeviceId);
            Assert.Equal(value, state.Registers[register]);

            // Should remain unchanged.
            Assert.True(state.Flags.Carry);

            // Should be reset.
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.HalfCarry);

            // Affected flags.
            Assert.Equal(sign, state.Flags.Sign);
            Assert.Equal(zero, state.Flags.Zero);
            Assert.Equal(parity, state.Flags.ParityOverflow);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 12, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_IN_R_MC_SetsFlags()
        {
            var rom = AssembleSource($@"
                org 00h
                IN (C)
                HALT
            ");

            var initialState = new CPUConfig();

            initialState.Registers = new CPURegisters();
            initialState.Registers.C = 77;

            var cpu = new CPU(initialState);

            var actualDeviceId = -1;

            cpu.OnDeviceRead += (int deviceID) => {
                actualDeviceId = deviceID;
                return 213;
            };

            var state = Execute(rom, cpu);

            Assert.Equal(77, state.Registers.C);
            Assert.Equal(77, actualDeviceId);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.ParityOverflow);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 12, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_IN_R_MC_DoesNotThrowExceptionIfNoHandlersPresent()
        {
            var rom = AssembleSource($@"
                org 00h
                IN B, (C)
                HALT
            ");

            var initialState = new CPUConfig();

            initialState.Registers = new CPURegisters();
            initialState.Registers.C = 123;
            initialState.Registers.B = 44;

            var cpu = new CPU(initialState);

            var state = Execute(rom, cpu);

            Assert.Equal(0, state.Registers.B);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 12, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
