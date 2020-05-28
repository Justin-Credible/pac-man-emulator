using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_MNN_RR_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_LD_MNN_RR(RegisterPair registerPair)
        {
            var rom = AssembleSource($@"
                org 00h
                LD (2477h), {registerPair}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                MemorySize = 20490,
                WriteableMemoryStart = 0x0000,
                WriteableMemoryEnd = 0x500A,
            };

            initialState.Registers[registerPair] = 0x6677;

            var state = Execute(rom, initialState);

            Assert.Equal(0x77, state.Memory[0x2477]);
            Assert.Equal(0x66, state.Memory[0x2478]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 20, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_MNN_HL()
        {
            // We have to assemble by hand here since LD (**), HL is a duplicate instruction
            // and the assembler will use the standard instruction instead of the undocumented
            // extended instruction.
            var rom = new byte[]
            {
                0xED, 0x63, // LD (NN), HL
                0x77, 0x24, // (2477h)
                0x76        // HALT
            };

            var initialState = new CPUConfig()
            {
                MemorySize = 20490,
                WriteableMemoryStart = 0x0000,
                WriteableMemoryEnd = 0x500A,
                Registers = new CPURegisters()
                {
                    HL = 0x6677,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x77, state.Memory[0x2477]);
            Assert.Equal(0x66, state.Memory[0x2478]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 20, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_MNN_SP()
        {
            var rom = AssembleSource($@"
                org 00h
                LD (2477h), SP
                HALT
            ");

            var initialState = new CPUConfig()
            {
                MemorySize = 20490,
                WriteableMemoryStart = 0x0000,
                WriteableMemoryEnd = 0x500A,
                Registers = new CPURegisters()
                {
                    SP = 0x6677
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x77, state.Memory[0x2477]);
            Assert.Equal(0x66, state.Memory[0x2478]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 20, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
