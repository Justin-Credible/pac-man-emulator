using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class JP_IY_Tests : BaseTest
    {
        // WARNING: Note that although the variants that use register pairs look like they are using indirect addressing,
        // JP (IY) jumps to the address stored in the register IY, not the address stored at the address IY points to.
        // https://wiki.specnext.dev/Extended_Z80_instruction_set
        [Fact]
        public void Test_JP_IY()
        {
            var rom = AssembleSource($@"
                org 00h
                JP (IY) ; $0000
                NOP     ; $0002
                NOP     ; $0003
                HALT    ; $0004
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x0004,
                },
            };

            var state = Execute(rom, initialState);

            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x0004, state.Registers.PC);
        }
    }
}
