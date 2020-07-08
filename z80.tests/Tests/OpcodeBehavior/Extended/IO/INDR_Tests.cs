using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class INDR_Tests : BaseTest
    {
        [Fact]
        public void Test_INDR()
        {
            var rom = AssembleSource($@"
                org 00h
                INDR
                HALT
            ");

            var memory = new byte[16*1024];

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
                    Zero = false,
                    Subtract = false,
                },
            };

            var cpu = new CPU(initialState);

            var actualDeviceIDs = new List<int>();

            var valueToReturn = 0;

            cpu.OnDeviceRead += (int deviceID) => {
                actualDeviceIDs.Add(deviceID);
                valueToReturn++;
                return (byte)valueToReturn;
            };

            var state = Execute(rom, memory, cpu);

            Assert.Equal(0, state.Registers.B);
            Assert.Equal(44, state.Registers.C);
            Assert.Equal(0x2214, state.Registers.HL);

            for (var i = 0; i < 32; i++)
            {
                var actual = actualDeviceIDs[i];
                Assert.Equal(44, actual);
            }

            var memLocation = 0x2234;
            for (int i = 1; i <= 32; i++)
            {
                Assert.Equal(i, state.Memory[memLocation]);
                memLocation--;
            }

            // Should remain unaffected.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Carry);

            // Should be affected.
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.Subtract);

            Assert.Equal(33, state.Iterations);
            Assert.Equal(4 + 16 + (21 * 31), state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
