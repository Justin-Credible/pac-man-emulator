using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class JPHLTests : BaseTest
    {
        [Fact]
        public void TestJPHL()
        {
            var rom = AssembleSource($@"
                org 00h
                JP (HL) ; $0000
                NOP     ; $0001
                NOP     ; $0002
                HALT    ; $0003
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    HL = 0x003,
                },
            };

            var state = Execute(rom, initialState);

            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x0003, state.ProgramCounter);
        }
    }
}
