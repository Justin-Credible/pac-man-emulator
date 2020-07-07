using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SCF_Tests : BaseTest
    {
        [Fact]
        public void Test_SCF_SetsFalseToTrue()
        {
            var rom = AssembleSource($@"
                org 00h
                SCF
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Flags = new ConditionFlags()
                {
                    Carry = false,
                    Subtract = true,
                    HalfCarry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_SCF_LeavesTrue()
        {
            var rom = AssembleSource($@"
                org 00h
                SCF
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Flags = new ConditionFlags()
                {
                    Carry = true,
                    Subtract = true,
                    HalfCarry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
