using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class JMPTests : BaseTest
    {
        [Fact]
        public void TestJP()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                JP 000Ah    ; $0002
                NOP         ; $0005
                NOP         ; $0006
                NOP         ; $0007
                NOP         ; $0008
                NOP         ; $0009
                HALT        ; $000A
            ");

            var state = Execute(rom);

            Assert.Equal(0x0000, state.StackPointer);

            AssertFlagsFalse(state);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(7 + (4*2) + 10, state.Cycles);
            Assert.Equal(0x000A, state.ProgramCounter);
        }
    }
}
