using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class EX_AF_AFP_Tests : BaseTest
    {
        [Fact]
        public void Test_EX_AF_AFP()
        {
            var rom = AssembleSource($@"
                org 00h
                EX AF, AF'
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

            Assert.Equal(0x19, state.Registers.A);
            Assert.Equal(0x22, state.Registers.B);
            Assert.Equal(0x33, state.Registers.C);
            Assert.Equal(0x44, state.Registers.D);
            Assert.Equal(0x55, state.Registers.E);
            Assert.Equal(0x66, state.Registers.H);
            Assert.Equal(0x77, state.Registers.L);

            Assert.Equal(0x11, state.ShadowRegisters.A);
            Assert.Equal(0x29, state.ShadowRegisters.B);
            Assert.Equal(0x39, state.ShadowRegisters.C);
            Assert.Equal(0x49, state.ShadowRegisters.D);
            Assert.Equal(0x59, state.ShadowRegisters.E);
            Assert.Equal(0x69, state.ShadowRegisters.H);
            Assert.Equal(0x79, state.ShadowRegisters.L);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.AuxCarry);
            Assert.True(state.Flags.Parity);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.True(state.ShadowFlags.Sign);
            Assert.False(state.ShadowFlags.Zero);
            Assert.True(state.ShadowFlags.AuxCarry);
            Assert.False(state.ShadowFlags.Parity);
            Assert.True(state.ShadowFlags.Subtract);
            Assert.False(state.ShadowFlags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
