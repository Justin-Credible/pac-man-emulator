using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class JP_NZ_Tests : BaseTest
    {
        [Fact]
        public void Test_JP_NZ_Jumps()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                JP NZ, 000Ah; $0002
                HALT        ; $0005
                NOP         ; $0006
                NOP         ; $0007
                NOP         ; $0008
                NOP         ; $0009
                HALT        ; $000A
            ");

            var initialState = new CPUConfig()
            {
                Flags = new ConditionFlags()
                {
                    Zero = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x0000, state.Registers.SP);

            AssertFlagsSame(initialState, state);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(4 + (4*2) + 10, state.Cycles);
            Assert.Equal(0x000A, state.Registers.PC);
        }

        [Fact]
        public void Test_JP_NZ_DoesNotJump()
        {
            var rom = AssembleSource($@"
                org 00h
                NOP         ; $0000
                NOP         ; $0001
                JP NZ, 000Ah; $0002
                HALT        ; $0005
                NOP         ; $0006
                NOP         ; $0007
                NOP         ; $0008
                NOP         ; $0009
                HALT        ; $000A
            ");

            var initialState = new CPUConfig()
            {
                Flags = new ConditionFlags()
                {
                    Zero = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0x0000, state.Registers.SP);

            AssertFlagsSame(initialState, state);

            Assert.Equal(4, state.Iterations);
            Assert.Equal(4 + (4*2) + 10, state.Cycles);
            Assert.Equal(0x0005, state.Registers.PC);
        }
    }
}
