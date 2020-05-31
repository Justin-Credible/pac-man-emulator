using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RL_R_Tests : BaseTest
    {
        [Theory]
        [InlineData(Register.A, 0b11001001, false, 0b10010010, true, false)]
        [InlineData(Register.A, 0b11001001, true, 0b10010011, true, true)]
        [InlineData(Register.A, 0b01001000, false, 0b10010000, false, true)]
        [InlineData(Register.A, 0b01001000, true, 0b10010001, false, false)]
        [InlineData(Register.B, 0b11001001, false, 0b10010010, true, false)]
        [InlineData(Register.B, 0b11001001, true, 0b10010011, true, true)]
        [InlineData(Register.B, 0b01001000, false, 0b10010000, false, true)]
        [InlineData(Register.B, 0b01001000, true, 0b10010001, false, false)]
        [InlineData(Register.C, 0b11001001, false, 0b10010010, true, false)]
        [InlineData(Register.C, 0b11001001, true, 0b10010011, true, true)]
        [InlineData(Register.C, 0b01001000, false, 0b10010000, false, true)]
        [InlineData(Register.C, 0b01001000, true, 0b10010001, false, false)]
        [InlineData(Register.D, 0b11001001, false, 0b10010010, true, false)]
        [InlineData(Register.D, 0b11001001, true, 0b10010011, true, true)]
        [InlineData(Register.D, 0b01001000, false, 0b10010000, false, true)]
        [InlineData(Register.D, 0b01001000, true, 0b10010001, false, false)]
        [InlineData(Register.E, 0b11001001, false, 0b10010010, true, false)]
        [InlineData(Register.E, 0b11001001, true, 0b10010011, true, true)]
        [InlineData(Register.E, 0b01001000, false, 0b10010000, false, true)]
        [InlineData(Register.E, 0b01001000, true, 0b10010001, false, false)]
        [InlineData(Register.H, 0b11001001, false, 0b10010010, true, false)]
        [InlineData(Register.H, 0b11001001, true, 0b10010011, true, true)]
        [InlineData(Register.H, 0b01001000, false, 0b10010000, false, true)]
        [InlineData(Register.H, 0b01001000, true, 0b10010001, false, false)]
        [InlineData(Register.L, 0b11001001, false, 0b10010010, true, false)]
        [InlineData(Register.L, 0b11001001, true, 0b10010011, true, true)]
        [InlineData(Register.L, 0b01001000, false, 0b10010000, false, true)]
        [InlineData(Register.L, 0b01001000, true, 0b10010001, false, false)]
        public void Test_RL_R(Register register, byte initialValue, bool initialCarryFlag, byte expectedValue, bool expectedCarryFlag, bool expectedPartyFlag)
        {
            var rom = AssembleSource($@"
                org 00h
                RL {register}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    [register] = initialValue,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = initialCarryFlag,
                    Zero = true,
                    Sign = false,
                    Parity = !expectedPartyFlag,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers[register]);

            // Should be affected.
            Assert.Equal(expectedCarryFlag, state.Flags.Carry);
            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.Equal(expectedPartyFlag, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [InlineData(0b11001001, false, 0b10010010, true, false)]
        [InlineData(0b11001001, true, 0b10010011, true, true)]
        [InlineData(0b01001000, false, 0b10010000, false, true)]
        [InlineData(0b01001000, true, 0b10010001, false, false)]
        public void Test_RL_HL(byte initialValue, bool initialCarryFlag, byte expectedValue, bool expectedCarryFlag, bool expectedPartyFlag)
        {
            var rom = AssembleSource($@"
                org 00h
                RL (HL)
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234] = initialValue;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    HL = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = initialCarryFlag,
                    Zero = true,
                    Sign = false,
                    Parity = !expectedPartyFlag,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(expectedValue, state.Memory[0x2234]);

            // Should be affected.
            Assert.Equal(expectedCarryFlag, state.Flags.Carry);
            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.Equal(expectedPartyFlag, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
