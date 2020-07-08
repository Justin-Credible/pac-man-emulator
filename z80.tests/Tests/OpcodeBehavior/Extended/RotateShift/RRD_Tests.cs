using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RRD_Tests : BaseTest
    {
        [Fact]
        public void Test_RRD()
        {
            var rom = AssembleSource($@"
                org 00h
                RRD         ; $0000
                HALT        ; $0002
            ");

            var memory = new byte[20490];
            memory[0x5000] = 0b00100000;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0b10000100,
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
                    Sign = false,
                    Zero = true,
                    ParityOverflow = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0b10000000, state.Registers.A);
            Assert.Equal(0b01000010, state.Memory[0x5000]);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            // Should not be affected.
            Assert.True(state.Flags.Carry);

            // Should be affected.
            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.ParityOverflow);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 18, state.Cycles);
            Assert.Equal(0x0002, state.Registers.PC);
        }
    }
}
