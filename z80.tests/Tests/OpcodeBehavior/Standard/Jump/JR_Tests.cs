using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class JR_Tests : BaseTest
    {
        [Fact]
        public void Test_JR_JumpsForward2()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                NOP         ; $0002
                NOP         ; $0003
                JR $+2      ; $0004
                HALT        ; $0006
                HALT        ; $0007
                HALT        ; $0008
                HALT        ; $0009
                HALT        ; $000A
            ");

            var state = Execute(rom);

            Assert.Equal(0x0000, state.StackPointer);

            AssertFlagsFalse(state);

            Assert.Equal(6, state.Iterations);
            Assert.Equal(4 + (4*4) + 12, state.Cycles);
            Assert.Equal(0x0006, state.ProgramCounter);
        }

        // Flags: S Z - H - P N C
        [Theory]
        [InlineData("Z", 0b01000000, true)]
        [InlineData("Z", 0b00000000, false)]
        [InlineData("NZ", 0b00000000, true)]
        [InlineData("NZ", 0b01000000, false)]
        [InlineData("C", 0b00000001, true)]
        [InlineData("C", 0b00000000, false)]
        [InlineData("NC", 0b00000000, true)]
        [InlineData("NC", 0b00000001, false)]
        public void Test_JR_JumpsForward_Conditionally(string operand, byte initialFlags, bool expectedJump)
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                NOP         ; $0002
                NOP         ; $0003
                JR {operand}, $+4      ; $0004
                NOP         ; $0006
                HALT        ; $0007
                HALT        ; $0008
                HALT        ; $0009
                HALT        ; $000A
            ");

            var initialState = new CPUConfig()
            {
                ProgramCounter = 0x0000,
            };

            initialState.Flags.SetFromByte(initialFlags);

            var state = Execute(rom, initialState);

            Assert.Equal(0x0000, state.StackPointer);

            Assert.Equal(expectedJump ? 6 : 7, state.Iterations);
            Assert.Equal(4 + (4*4) + (expectedJump ? 0 : 4) + (expectedJump ? 12 : 7), state.Cycles);
            Assert.Equal(expectedJump ? 0x0008 : 0x0007, state.ProgramCounter);
        }

        [Fact]
        public void Test_JR_JumpsForward5()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                NOP         ; $0002
                NOP         ; $0003
                JR $+5      ; $0004
                HALT        ; $0006
                HALT        ; $0007
                HALT        ; $0008
                HALT        ; $0009
                HALT        ; $000A
            ");

            var state = Execute(rom);

            Assert.Equal(0x0000, state.StackPointer);

            AssertFlagsFalse(state);

            Assert.Equal(6, state.Iterations);
            Assert.Equal(4 + (4*4) + 12, state.Cycles);
            Assert.Equal(0x0009, state.ProgramCounter);
        }

        [Fact]
        public void Test_JR_JumpsBackward4()
        {
            var rom = AssembleSource($@"
                org 00h
                HALT        ; $0000
                NOP         ; $0001
                NOP         ; $0002
                NOP         ; $0003
                JR $-4      ; $0004
                HALT        ; $0006
                HALT        ; $0007
                HALT        ; $0008
                HALT        ; $0009
                HALT        ; $000A
            ");

            var initialState = new CPUConfig()
            {
                ProgramCounter = 0x0001,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x0000, state.StackPointer);

            AssertFlagsFalse(state);

            Assert.Equal(5, state.Iterations);
            Assert.Equal(4 + (4*3) + 12, state.Cycles);
            Assert.Equal(0x0000, state.ProgramCounter);
        }

        [Fact]
        public void Test_JR_JumpsBackward2()
        {
            var rom = AssembleSource($@"
                org 00h
                HALT        ; $0000
                NOP         ; $0001
                HALT        ; $0002
                NOP         ; $0003
                JR $-2      ; $0004
                HALT        ; $0006
                HALT        ; $0007
                HALT        ; $0008
                HALT        ; $0009
                HALT        ; $000A
            ");

            var initialState = new CPUConfig()
            {
                ProgramCounter = 0x0003,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x0000, state.StackPointer);

            AssertFlagsFalse(state);

            Assert.Equal(3, state.Iterations);
            Assert.Equal(4 + (4*1) + 12, state.Cycles);
            Assert.Equal(0x0002, state.ProgramCounter);
        }

        [Theory]
        [InlineData("Z", 0b01000000, true)]
        [InlineData("Z", 0b00000000, false)]
        [InlineData("NZ", 0b00000000, true)]
        [InlineData("NZ", 0b01000000, false)]
        [InlineData("C", 0b00000001, true)]
        [InlineData("C", 0b00000000, false)]
        [InlineData("NC", 0b00000000, true)]
        [InlineData("NC", 0b00000001, false)]
        public void Test_JR_JumpsBackward_Conditionally(string operand, byte initialFlags, bool expectedJump)
        {
            var rom = AssembleSource($@"
                org 00h
                HALT        ; $0000
                NOP         ; $0001
                NOP         ; $0002
                NOP         ; $0003
                JR {operand}, $-4      ; $0004
                NOP         ; $0006
                HALT        ; $0007
                HALT        ; $0008
                HALT        ; $0009
                HALT        ; $000A
            ");

            var initialState = new CPUConfig()
            {
                ProgramCounter = 0x0001,
            };

            initialState.Flags.SetFromByte(initialFlags);

            var state = Execute(rom, initialState);

            Assert.Equal(0x0000, state.StackPointer);

            Assert.Equal(expectedJump ? 5 : 6, state.Iterations);
            Assert.Equal(4 + (4*3) + (expectedJump ? 0 : 4) + (expectedJump ? 12 : 7), state.Cycles);
            Assert.Equal(expectedJump ? 0x0000 : 0x0007, state.ProgramCounter);
        }
    }
}
