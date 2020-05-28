using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RETI_Tests : BaseTest
    {
        [Fact]
        public void Test_RETI()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                NOP         ; $0002
                HALT        ; $0003
                HALT        ; $0004
                HALT        ; $0005
                NOP         ; $0006
                RETI        ; $0007
                NOP         ; $0009
                NOP         ; $000A
                HALT        ; $000B
            ");

            var memory = new byte[16384];
            memory[0x2720] = 0xFF;
            memory[0x271F] = 0x00;
            memory[0x271E] = 0x02;
            memory[0x271D] = 0xFF;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    PC = 0x0007,
                    SP = 0x271E,
                },
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2720, state.Registers.SP);
            Assert.Equal(0xFF, state.Memory[0x2720]);
            Assert.Equal(0x00, state.Memory[0x271F]);
            Assert.Equal(0x02, state.Memory[0x271E]);
            Assert.Equal(0xFF, state.Memory[0x271D]);

            AssertFlagsFalse(state);

            Assert.Equal(3, state.Iterations);
            Assert.Equal(4 + 14 + 4, state.Cycles);
            Assert.Equal(0x0003, state.Registers.PC);
        }
    }
}
