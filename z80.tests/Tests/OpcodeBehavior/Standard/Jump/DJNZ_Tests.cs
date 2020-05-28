using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class DJNZ_Tests : BaseTest
    {
        [Fact]
        public void Test_DJNZ()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                DJNZ $+5    ; $0002
                NOP         ; $0004
                HALT        ; $0005
                HALT        ; $0006
                NOP         ; $0007
                JP $0002    ; $0008
                HALT        ; $0009
                HALT        ; $000A
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    B = 4,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x0000, state.Registers.SP);

            AssertFlagsFalse(state);

            Assert.Equal(14, state.Iterations);
            Assert.Equal(4 + (4*6) + (13*3) + (10*3) + 8, state.Cycles);
            Assert.Equal(0x0005, state.Registers.PC);
        }
    }
}
