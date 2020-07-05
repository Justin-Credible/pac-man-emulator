using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class DEC_IX_Half_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0x01, 0x00, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = true, Sign = false } });
            list.Add(new object[] { 0xFF, 0xFE, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0x10, 0x0F, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0x7E, 0x7D, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0x00, 0xFF, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0x80, 0x7F, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = false } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_DEC_IXL(byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC IXL
                HALT
            ");

            var registers = new CPURegisters()
            {
                IXH = 0xFF,
                IXL = initialValue,
            };

            var flags = new ConditionFlags()
            {
                // Should be affected.
                HalfCarry = !expectedFlags.HalfCarry,
                ParityOverflow = !expectedFlags.ParityOverflow,
                Zero = !expectedFlags.Zero,
                Sign = !expectedFlags.Sign,

                // Should be set.
                Subtract = false,

                // Should be unaffected.
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0xFF, state.Registers.IXH);
            Assert.Equal(expectedValue, state.Registers.IXL);

            // Should be affected.
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be set.
            Assert.True(state.Flags.Subtract);

            // Should be unaffected.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_DEC_IXH(byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC IXH
                HALT
            ");

            var registers = new CPURegisters()
            {
                IXH = initialValue,
                IXL = 0xFF,
            };

            var flags = new ConditionFlags()
            {
                // Should be affected.
                HalfCarry = !expectedFlags.HalfCarry,
                ParityOverflow = !expectedFlags.ParityOverflow,
                Zero = !expectedFlags.Zero,
                Sign = !expectedFlags.Sign,

                // Should be set.
                Subtract = false,

                // Should be unaffected.
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers.IXH);
            Assert.Equal(0xFF, state.Registers.IXL);

            // Should be affected.
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be set.
            Assert.True(state.Flags.Subtract);

            // Should be unaffected.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
