using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_I_and_R_Tests : BaseTest
    {
        [Theory]
        [InlineData(Register.I, Register.A)]
        [InlineData(Register.A, Register.I)]
        [InlineData(Register.R, Register.A)]
        [InlineData(Register.A, Register.R)]
        public void Test_LD(Register destReg, Register sourceReg)
        {
            var rom = AssembleSource($@"
                org 00h
                LD {destReg}, {sourceReg}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    [sourceReg] = 0x42,
                    [destReg] = 0x77,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x42, state.Registers[sourceReg]);
            Assert.Equal(0x42, state.Registers[destReg]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 9, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
