using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class JP_HL_Tests : BaseTest
    {
          // WARNING: Note that although the variants that use register pairs look like they are using indirect addressing,
        // JP (HL) jumps to the address stored in the register HL, not the address stored at the address HL points to.
        // https://wiki.specnext.dev/Extended_Z80_instruction_set
        [Fact]
        public void Test_JP_HL()
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
