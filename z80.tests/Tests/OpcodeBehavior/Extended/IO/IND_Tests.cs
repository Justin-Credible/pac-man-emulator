using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class IND_Tests : BaseTest
    {
        [Fact]
        public void Test_IND()
        {
            var rom = AssembleSource($@"
                org 00h
                IND
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x1234] = 77;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    B = 32,
                    C = 44,
                    HL = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should remain unaffected.
                    Sign = true,
                    HalfCarry = true,
                    ParityOverflow = true,
                    Carry = true,

                    // Should be affected.
                    Zero = true,
                    Subtract = false,
                },
            };

            var cpu = new CPU(initialState);

            var actualDeviceID = -1;

            cpu.OnDeviceRead += (int deviceID) => {
                actualDeviceID = deviceID;
                return 42;
            };

            var state = Execute(rom, memory, cpu);

            Assert.Equal(44, actualDeviceID);

            Assert.Equal(31, state.Registers.B);
            Assert.Equal(44, state.Registers.C);
            Assert.Equal(0x2233, state.Registers.HL);
            Assert.Equal(42, state.Memory[0x2234]);

            // Should remain unaffected.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Carry);

            // Should be affected.
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_OUTD_SetsZeroFlag()
        {
            var rom = AssembleSource($@"
                org 00h
                IND
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234] = 77;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    B = 1,
                    C = 44,
                    HL = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should remain unaffected.
                    Sign = true,
                    HalfCarry = true,
                    ParityOverflow = true,
                    Carry = true,

                    // Should be affected.
                    Zero = false,
                    Subtract = false,
                },
            };

            var cpu = new CPU(initialState);

            var actualDeviceID = -1;

            cpu.OnDeviceRead += (int deviceID) => {
                actualDeviceID = deviceID;
                return 42;
            };

            var state = Execute(rom, memory, cpu);

            Assert.Equal(44, actualDeviceID);

            Assert.Equal(0, state.Registers.B);
            Assert.Equal(44, state.Registers.C);
            Assert.Equal(0x2233, state.Registers.HL);
            Assert.Equal(42, state.Memory[0x2234]);

            // Should remain unaffected.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Carry);

            // Should be affected.
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 16, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
