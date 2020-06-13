using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CALL_NZ_Tests : BaseTest
    {
        [Fact]
        public void Test_CALL_NZ_Calls()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                CALL NZ, 000Ah   ; $0002
                HALT        ; $0005
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
                Flags = new ConditionFlags()
                {
                    Zero = false,
                },
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            AssertFlagsSame(initialState, state);

            Assert.Equal(0x271E, state.Registers.SP);
            Assert.Equal(0xFF, state.Memory[0x2720]);
            Assert.Equal(0x00, state.Memory[0x271F]);
            Assert.Equal(0x05, state.Memory[0x271E]);
            Assert.Equal(0xFF, state.Memory[0x271D]);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(4 + (4*2) + 17, state.Cycles);
            Assert.Equal(0x000A, state.Registers.PC);
        }

        [Fact]
        public void Test_CALL_NZ_DoesNotCall()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                CALL NZ, 000Ah   ; $0002
                HALT        ; $0005
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
                Flags = new ConditionFlags()
                {
                    Zero = true,
                },
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            AssertFlagsSame(initialState, state);

            Assert.Equal(0x2720, state.Registers.SP);
            Assert.Equal(0xFF, state.Memory[0x2720]);
            Assert.Equal(0xFF, state.Memory[0x271F]);
            Assert.Equal(0xFF, state.Memory[0x271E]);
            Assert.Equal(0xFF, state.Memory[0x271D]);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(4 + (4*2) + 10, state.Cycles);
            Assert.Equal(0x0005, state.Registers.PC);
        }
    }
}
