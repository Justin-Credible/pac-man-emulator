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
                },
                Flags = new ConditionFlags()
                {
                    Sign = true,
                    Zero = false,
                    AuxCarry = true,
                    Parity = false,
                    Subtract = true,
                    Carry = false,
                },
                ShadowRegisters = new CPURegisters()
                {
                    A = 0x19,
                    B = 0x29,
                    C = 0x39,
                    D = 0x49,
                    E = 0x59,
                    H = 0x69,
                    L = 0x79,
                },
                ShadowFlags = new ConditionFlags()
                {
                    Sign = false,
                    Zero = true,
                    AuxCarry = false,
                    Parity = true,
                    Subtract = false,
                    Carry = true,
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

            Assert.Equal(0x19, state.ShadowRegisters.A);
            Assert.Equal(0x22, state.ShadowRegisters.B);
            Assert.Equal(0x33, state.ShadowRegisters.C);
            Assert.Equal(0x44, state.ShadowRegisters.D);
            Assert.Equal(0x55, state.ShadowRegisters.E);
            Assert.Equal(0x66, state.ShadowRegisters.H);
            Assert.Equal(0x77, state.ShadowRegisters.L);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.True(state.Flags.AuxCarry);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.False(state.ShadowFlags.Sign);
            Assert.True(state.ShadowFlags.Zero);
            Assert.False(state.ShadowFlags.AuxCarry);
            Assert.True(state.ShadowFlags.Parity);
            Assert.False(state.ShadowFlags.Subtract);
            Assert.True(state.ShadowFlags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
