using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RLD_Tests : BaseTest
    {
        [Fact]
        public void Test_RLD()
        {
            var rom = AssembleSource($@"
                org 00h
                RLD         ; $0000
                HALT        ; $0002
            ");

            var memory = new byte[20490];
            memory[0x5000] = 0b00110001;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b01111010,
                    HL = 0x5000,
                },
                Flags = new ConditionFlags()
                {
                    // Should be reset.
                    HalfCarry = true,
                    Subtract = true,

                    // Should not be affected.
                    Carry = true,

                    // Should be affected.
                    Sign = true,
                    Zero = true,
                    ParityOverflow = true,
                },
                MemorySize = 20490,
                WriteableMemoryStart = 0x0000,
                WriteableMemoryEnd = 0x500A,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b01110011, state.Registers.A);
            Assert.Equal(0b00011010, state.Memory[0x5000]);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            // Should not be affected.
            Assert.True(state.Flags.Carry);

            // Should be affected.
            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.ParityOverflow);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 18, state.Cycles);
            Assert.Equal(0x0002, state.Registers.PC);
        }
    }
}
