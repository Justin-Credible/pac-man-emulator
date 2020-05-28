using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class EXX_Tests : BaseTest
    {
        [Fact]
        public void Test_EXX()
        {
            var rom = AssembleSource($@"
                org 00h
                EXX
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x11,
                    B = 0x22,
                    C = 0x33,
                    D = 0x44,
                    E = 0x55,
                    H = 0x66,
                    L = 0x77,

                    Shadow_A = 0x19,
                    Shadow_B = 0x29,
                    Shadow_C = 0x39,
                    Shadow_D = 0x49,
                    Shadow_E = 0x59,
                    Shadow_H = 0x69,
                    Shadow_L = 0x79,
                },
                Flags = new ConditionFlags()
                {
                    Sign = true,
                    Zero = false,
                    AuxCarry = true,
                    Parity = false,
                    Subtract = true,
                    Carry = false,

                    Shadow = (new ConditionFlags()
                    {
                        Sign = false,
                        Zero = true,
                        AuxCarry = false,
                        Parity = true,
                        Subtract = false,
                        Carry = true,
                    }).ToByte(),
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x11, state.Registers.A);
            Assert.Equal(0x29, state.Registers.B);
            Assert.Equal(0x39, state.Registers.C);
            Assert.Equal(0x49, state.Registers.D);
            Assert.Equal(0x59, state.Registers.E);
            Assert.Equal(0x69, state.Registers.H);
            Assert.Equal(0x79, state.Registers.L);

            Assert.Equal(0x19, state.Registers.Shadow_A);
            Assert.Equal(0x22, state.Registers.Shadow_B);
            Assert.Equal(0x33, state.Registers.Shadow_C);
            Assert.Equal(0x44, state.Registers.Shadow_D);
            Assert.Equal(0x55, state.Registers.Shadow_E);
            Assert.Equal(0x66, state.Registers.Shadow_H);
            Assert.Equal(0x77, state.Registers.Shadow_L);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            var shadowFlags = new ConditionFlags(state.Flags.Shadow);

            Assert.False(shadowFlags.Sign);
            Assert.True(shadowFlags.Zero);
            Assert.False(shadowFlags.AuxCarry);
            Assert.True(shadowFlags.Parity);
            Assert.False(shadowFlags.Subtract);
            Assert.True(shadowFlags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
