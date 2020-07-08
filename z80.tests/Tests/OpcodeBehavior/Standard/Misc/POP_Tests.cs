using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class POP_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.HL)]
        public void Test_POP(RegisterPair pair)
        {
            var rom = AssembleSource($@"
                org 00h
                POP {pair}
                HALT
            ");

            var memory = new byte[16384];
            memory[0x2FFE] = 0x77;
            memory[0x2FFF] = 0x24;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    SP = 0x2FFE
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2477, state.Registers[pair]);
            Assert.Equal(0x00, state.Memory[0x3000]);
            Assert.Equal(0x24, state.Memory[0x2FFF]);
            Assert.Equal(0x77, state.Memory[0x2FFE]);
            Assert.Equal(0x3000, state.Registers.SP);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Fact]
        public void Test_POP_AF()
        {
            var rom = AssembleSource($@"
                org 00h
                POP AF
                HALT
            ");

            var memory = new byte[16384];

            // 7 6 5 4 3  2  1 0
            // S Z - H - P/V N C
            // 1 1 0 1 0  1  1 1
            //       D 7
            memory[0x2FFE] = 0xD7;

            memory[0x2FFF] = 0x42;

            var registers = new CPURegisters();
            registers.A = 0x42;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    SP = 0x2FFE
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x42, state.Registers.A);
            Assert.Equal(0x00, state.Memory[0x3000]);
            Assert.Equal(0x42, state.Memory[0x2FFF]);
            Assert.Equal(0xD7, state.Memory[0x2FFE]);
            Assert.Equal(0x3000, state.Registers.SP);

            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
