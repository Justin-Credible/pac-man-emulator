using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADD_IX_RR_Tests : BaseTest
    {
        [Theory]
        [InlineData(RegisterPair.BC, 0x1212, 0x3434, 0x4646, 0x3434)]
        [InlineData(RegisterPair.DE, 0x1212, 0x3434, 0x4646, 0x3434)]
        [InlineData(RegisterPair.IX, 0x1212, 0x1212, 0x2424, 0x2424)]
        [InlineData(RegisterPair.SP, 0x1212, 0x3434, 0x4646, 0x3434)]
        public void Test_ADD_IX_RR_NoCarry_NoHalfCarry(RegisterPair pair, UInt16 ixInitialValue, UInt16 pairInitialValue, UInt16 ixExpectedValue, UInt16 pairExpectedValue)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD IX, {pair}
                HALT
            ");

            var registers = new CPURegisters()
            {
                IX = ixInitialValue,
                [pair] = pairInitialValue,
            };

            var flags = new ConditionFlags()
            {
                // Should be unaffected.
                Sign = true,
                Zero = true,
                ParityOverflow = true,

                // Should be reset.
                Subtract = true,

                // Should be affected.
                HalfCarry = false,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(pairExpectedValue, state.Registers[pair]);
            Assert.Equal(ixExpectedValue, state.Registers.IX);

            // Should be unaffected.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.Subtract);

            // Should be affected.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [InlineData(RegisterPair.BC, 0xFFFE, 0x0005, 0x0003, 0x0005)]
        [InlineData(RegisterPair.DE, 0xFFFE, 0x0005, 0x0003, 0x0005)]
        [InlineData(RegisterPair.IX, 0xFFF0, 0xFFF0, 0xFFE0, 0xFFE0)]
        [InlineData(RegisterPair.SP, 0xFFFE, 0x0005, 0x0003, 0x0005)]
        public void Test_ADD_IX_RR_Carry_HalfCarry(RegisterPair pair, UInt16 ixInitialValue, UInt16 pairInitialValue, UInt16 ixExpectedValue, UInt16 pairExpectedValue)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD IX, {pair}
                HALT
            ");

            var registers = new CPURegisters()
            {
                IX = ixInitialValue,
                [pair] = pairInitialValue,
            };

            var flags = new ConditionFlags()
            {
                // Should be unaffected.
                Sign = true,
                Zero = true,
                ParityOverflow = true,

                // Should be reset.
                Subtract = true,

                // Should be affected.
                HalfCarry = false,
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(pairExpectedValue, state.Registers[pair]);
            Assert.Equal(ixExpectedValue, state.Registers.IX);

            // Should be unaffected.
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.True(state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.Subtract);

            // Should be affected.
            Assert.True(state.Flags.HalfCarry);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
