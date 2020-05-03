using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_RR_NN_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC)]
        [InlineData(RegisterPair.DE)]
        [InlineData(RegisterPair.HL)]
        public void Test_LD_RR_NN(RegisterPair pair)
        {
            var rom = AssembleSource($@"
                org 00h
                LD {pair}, 4277h
                HALT
            ");

            var state = Execute(rom);

            Assert.Equal(0x4277, state.Registers[pair]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x03, state.ProgramCounter);
        }

        [Fact]
        public void Test_LD_SP_NN()
        {
            var rom = AssembleSource($@"
                org 00h
                LD SP, 4277h
                HALT
            ");

            var state = Execute(rom);

            Assert.Equal(0x4277, state.StackPointer);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x03, state.ProgramCounter);
        }
    }
}
