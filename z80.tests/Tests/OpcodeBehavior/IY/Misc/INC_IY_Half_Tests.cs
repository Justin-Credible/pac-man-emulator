using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class INC_IY_Half_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0x00, 0x01, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0x07, 0x08, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0x0F, 0x10, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0xFE, 0xFF, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0xFF, 0x00, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = true, Sign = false } });
            list.Add(new object[] { 0x7E, 0x7F, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0x7F, 0x80, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = true } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_INC_IYL(byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                INC IYL
                HALT
            ");

            var registers = new CPURegisters()
            {
                IYH = 0xFF,
                IYL = initialValue,
            };

            var flags = new ConditionFlags()
            {
                // Should be affected.
                HalfCarry = !expectedFlags.HalfCarry,
                ParityOverflow = !expectedFlags.ParityOverflow,
                Zero = !expectedFlags.Zero,
                Sign = !expectedFlags.Sign,

                // Should be reset.
                Subtract = true,

                // Should be unaffected.
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(0xFF, state.Registers.IYH);
            Assert.Equal(expectedValue, state.Registers.IYL);

            // Should be affected.
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be reset.
            Assert.False(state.Flags.Subtract);

            // Should be unaffected.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_INC_IYH(byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                INC IYH
                HALT
            ");

            var registers = new CPURegisters()
            {
                IYH = initialValue,
                IYL = 0xFF,
            };

            var flags = new ConditionFlags()
            {
                // Should be affected.
                HalfCarry = !expectedFlags.HalfCarry,
                ParityOverflow = !expectedFlags.ParityOverflow,
                Zero = !expectedFlags.Zero,
                Sign = !expectedFlags.Sign,

                // Should be reset.
                Subtract = true,

                // Should be unaffected.
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers.IYH);
            Assert.Equal(0xFF, state.Registers.IYL);

            // Should be affected.
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be reset.
            Assert.False(state.Flags.Subtract);

            // Should be unaffected.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
