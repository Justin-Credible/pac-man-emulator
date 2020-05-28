using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_RR_MNN_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        public void Test_LD_RR_MNN(RegisterPair registerPair)
        {
            var rom = AssembleSource($@"
                org 00h
                LD {registerPair}, (2477h)
                HALT
            ");

            var memory = new byte[20490];
            memory[0x2477] = 0x77;
            memory[0x2478] = 0x66;

            var initialState = new CPUConfig()
            {
                MemorySize = 20490,
                WriteableMemoryStart = 0x0000,
                WriteableMemoryEnd = 0x500A,
                Flags = new ConditionFlags()
                {
                    // Should remain unmodified.
                    Sign = true,
                    Zero = true,
                    AuxCarry = true,
                    Parity = true,
                    Subtract = true,
                    Carry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x6677, state.Registers[registerPair]);

            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 20, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_HL_MNN()
        {
            // We have to assemble by hand here since LD SP, (**) is a duplicate instruction
            // and the assembler will use the standard instruction instead of the undocumented
            // extended instruction.
            var rom = new byte[]
            {
                0xED, 0x6B, // LD SP,
                0x77, 0x24, // (2477h)
                0x76        // HALT
            };

            var memory = new byte[20490];
            memory[0x2477] = 0x77;
            memory[0x2478] = 0x66;

            var initialState = new CPUConfig()
            {
                MemorySize = 20490,
                WriteableMemoryStart = 0x0000,
                WriteableMemoryEnd = 0x500A,
                Flags = new ConditionFlags()
                {
                    // Should remain unmodified.
                    Sign = true,
                    Zero = true,
                    AuxCarry = true,
                    Parity = true,
                    Subtract = true,
                    Carry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x6677, state.Registers.HL);

            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 20, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_SP_MNN()
        {
            var rom = AssembleSource($@"
                org 00h
                LD SP, (2477h)
                HALT
            ");

            var memory = new byte[20490];
            memory[0x2477] = 0x77;
            memory[0x2478] = 0x66;

            var initialState = new CPUConfig()
            {
                MemorySize = 20490,
                WriteableMemoryStart = 0x0000,
                WriteableMemoryEnd = 0x500A,
                Flags = new ConditionFlags()
                {
                    // Should remain unmodified.
                    Sign = true,
                    Zero = true,
                    AuxCarry = true,
                    Parity = true,
                    Subtract = true,
                    Carry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x6677, state.Registers.SP);

            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 20, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
