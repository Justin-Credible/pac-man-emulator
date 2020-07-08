using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RST_Tests : BaseTest
    {
        [Theory]
        [InlineData("00h", 0x0000)]
        [InlineData("08h", 0x0008)]
        [InlineData("10h", 0x0010)]
        [InlineData("18h", 0x0018)]
        [InlineData("20h", 0x0020)]
        [InlineData("28h", 0x0028)]
        [InlineData("30h", 0x0030)]
        [InlineData("38h", 0x0038)]
        public void Test_RST(string opcodeArg, UInt16 expectedProgramCounterValue)
        {
            var rom = AssembleSource($@"
                org 00h
                HALT

                org 08h
                HALT

                org 10h
                HALT

                org 18h
                HALT

                org 20h
                HALT

                org 28h
                HALT

                org 30h
                HALT

                org 38h
                HALT

                org 40h
                RST {opcodeArg}
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
                    PC = 0x0040,
                    SP = 0x2720,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x271E, state.Registers.SP);
            Assert.Equal(0xFF, state.Memory[0x2720]);
            Assert.Equal(0x00, state.Memory[0x271F]);
            Assert.Equal(0x41, state.Memory[0x271E]);
            Assert.Equal(0xFF, state.Memory[0x271D]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(expectedProgramCounterValue, state.Registers.PC);
        }

        [Fact]
        public void Test_RST_And_RET()
        {
            var rom = AssembleSource($@"
                org 00h     ; RST 0
                LD A, 1D
                RET

                org 08h     ; RST 1
                LD B, 2D
                RET

                org 010h    ; RST 2
                LD C, 3D
                RET

                org 018h    ; RST 3
                LD D, 4D
                RET

                org 20h     ; RST 4
                LD E, 5D
                RET

                org 28h     ; RST 5
                LD H, 6D
                RET

                org 30h     ; RST 6
                RST 38h
                RET

                org 38h     ; RST 7
                RST 28h
                RET

                LD L, 7D

                org 40h
                RST 00h       ; 0x40
                RST 08h       ; 0x41
                RST 10h       ; 0x42
                RST 18h       ; 0x43
                RST 20h       ; 0x44
                RST 30h       ; 0x45
                HALT          ; 0x46
            ");

            var memory = new byte[16384];
            memory[0x2720] = 0xFF;
            memory[0x271F] = 0xFF;
            memory[0x271E] = 0xFF;
            memory[0x271D] = 0xFF;
            memory[0x271C] = 0xFF;
            memory[0x271B] = 0xFF;
            memory[0x271A] = 0xFF;
            memory[0x2719] = 0xFF;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    PC = 0x0040,
                    SP = 0x2720,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2720, state.Registers.SP);
            Assert.Equal(0xFF, state.Memory[0x2720]);
            Assert.Equal(0x00, state.Memory[0x271F]);
            Assert.Equal(0x46, state.Memory[0x271E]);
            Assert.Equal(0x00, state.Memory[0x271D]);
            Assert.Equal(0x31, state.Memory[0x271C]);
            Assert.Equal(0x00, state.Memory[0x271B]);
            Assert.Equal(0x39, state.Memory[0x271A]);
            Assert.Equal(0xFF, state.Memory[0x2719]);

            AssertFlagsFalse(state);

            Assert.Equal(23, state.Iterations);
            Assert.Equal(4 + (11 * 8) + (10 * 8) + (7 * 6), state.Cycles);
            Assert.Equal(0x46, state.Registers.PC);
        }
    }
}
