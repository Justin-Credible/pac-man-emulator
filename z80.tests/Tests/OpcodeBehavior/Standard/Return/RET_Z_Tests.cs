using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RET_Z_Tests : BaseTest
    {
        [Fact]
        public void Test_RET_Z_Returns()
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
                RET Z       ; $0007
                NOP         ; $0008
                NOP         ; $0009
                HALT        ; $000A
            ");

            var memory = new byte[16384];
            memory[0x2720] = 0xFF;
            memory[0x271F] = 0x00;
            memory[0x271E] = 0x02;
            memory[0x271D] = 0xFF;

            var initialState = new CPUConfig()
            {
                ProgramCounter = 0x0007,
                StackPointer = 0x271E,
                Flags = new ConditionFlags()
                {
                    Zero = true,
                },
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            AssertFlagsSame(initialState, state);

            Assert.Equal(0x2720, state.StackPointer);
            Assert.Equal(0xFF, state.Memory[0x2720]);
            Assert.Equal(0x00, state.Memory[0x271F]);
            Assert.Equal(0x02, state.Memory[0x271E]);
            Assert.Equal(0xFF, state.Memory[0x271D]);

            Assert.Equal(3, state.Iterations);
            Assert.Equal(4 + 11 + 4, state.Cycles);
            Assert.Equal(0x0003, state.ProgramCounter);
        }

        [Fact]
        public void Test_RET_Z_DoesNotReturn()
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
                RET Z       ; $0007
                NOP         ; $0008
                NOP         ; $0009
                HALT        ; $000A
            ");

            var memory = new byte[16384];
            memory[0x2720] = 0xFF;
            memory[0x271F] = 0x00;
            memory[0x271E] = 0x02;
            memory[0x271D] = 0xFF;

            var initialState = new CPUConfig()
            {
                ProgramCounter = 0x0007,
                StackPointer = 0x271E,
                Flags = new ConditionFlags()
                {
                    Zero = false,
                },
                MemorySize = memory.Length,
            };

            var state = Execute(rom, memory, initialState);

            AssertFlagsSame(initialState, state);

            Assert.Equal(0x271E, state.StackPointer);
            Assert.Equal(0xFF, state.Memory[0x2720]);
            Assert.Equal(0x00, state.Memory[0x271F]);
            Assert.Equal(0x02, state.Memory[0x271E]);
            Assert.Equal(0xFF, state.Memory[0x271D]);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(4 + 5 + (2*4), state.Cycles);
            Assert.Equal(0x000A, state.ProgramCounter);
        }
    }
}
