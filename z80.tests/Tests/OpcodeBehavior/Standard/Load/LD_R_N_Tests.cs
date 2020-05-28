using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_R_N_Tests : BaseTest
    {
        [Theory]
        [ClassData(typeof(RegistersClassData))]
        public void Test_LD_R_N_ToRegister(Register destReg)
        {
            var rom = AssembleSource($@"
                org 00h
                LD {destReg}, 42h
                HALT
            ");

            var state = Execute(rom);

            Assert.Equal(0x42, state.Registers[destReg]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 7, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_MRR_N_ToMemory()
        {
            var rom = AssembleSource($@"
                org 00h
                LD (HL), 42h
                HALT
            ");

            var registers = new CPURegisters();
            registers[Register.H] = 0x22;
            registers[Register.L] = 0x33;

            var initialState = new CPUConfig()
            {
                Registers = registers,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x42, state.Memory[0x2233]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
