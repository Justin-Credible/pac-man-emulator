using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CALL_Tests : BaseTest
    {
        [Fact]
        public void Test_CALL()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                CALL 000Ah  ; $0002
                NOP         ; $0005
                NOP         ; $0006
                NOP         ; $0007
                NOP         ; $0008
                NOP         ; $0009
                HALT        ; $000A
            ");

            var memory = new byte[16384];
            memory[0x2720] = 0xFF;
            memory[0x271F] = 0xFF;
            memory[0x271E] = 0xFF;
            memory[0x271D] = 0xFF;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    SP = 0x2720
                },
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x271E, state.Registers.SP);
            Assert.Equal(0xFF, state.Memory[0x2720]);
            Assert.Equal(0x00, state.Memory[0x271F]);
            Assert.Equal(0x05, state.Memory[0x271E]);
            Assert.Equal(0xFF, state.Memory[0x271D]);

            AssertFlagsFalse(state);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(4 + (4*2) + 17, state.Cycles);
            Assert.Equal(0x000A, state.Registers.PC);
        }

        [Fact]
        public void Test_CALL_And_RET()
        {
            var rom = AssembleSource($@"
                org 00h
                LD SP, 2720h; $0000 - $0002
                NOP         ; $0003
                CALL 0011h  ; $0004 - $0006
                CALL 0014h  ; $0007 - $0009
                CALL 0017h  ; $000A - $000C
                HALT        ; $000D
                NOP         ; $000E
                LD H, 55h   ; $000F - $0010
                LD A, 42h   ; $0011 - $0012
                RET         ; $0013
                LD B, 66h   ; $0014 - $0015
                RET         ; $0016
                LD C, 77h   ; $0017 - $0018
                LD D, 88h   ; $0019 - $001A
                RET         ; $001B
                LD E, 99h   ; $001C - $001D
            ");

            var memory = new byte[16384];
            memory[0x2720] = 0xFF;
            memory[0x271F] = 0xFF;
            memory[0x271E] = 0xFF;
            memory[0x271D] = 0xFF;

            var initialState = new CPUConfig()
            {
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x42, state.Registers.A);
            Assert.Equal(0x66, state.Registers.B);
            Assert.Equal(0x77, state.Registers.C);
            Assert.Equal(0x88, state.Registers.D);
            Assert.Equal(0x00, state.Registers.E);
            Assert.Equal(0x00, state.Registers.H);

            Assert.Equal(0x2720, state.Registers.SP);
            Assert.Equal(0xFF, state.Memory[0x2720]);
            Assert.Equal(0x00, state.Memory[0x271F]);
            Assert.Equal(0x0D, state.Memory[0x271E]);
            Assert.Equal(0xFF, state.Memory[0x271D]);

            AssertFlagsFalse(state);

            Assert.Equal(13, state.Iterations);
            Assert.Equal(4 + 10 + 4 + (3*17) + (3*10) + (4*7), state.Cycles);
            Assert.Equal(0x000D, state.Registers.PC);
        }
    }
}
