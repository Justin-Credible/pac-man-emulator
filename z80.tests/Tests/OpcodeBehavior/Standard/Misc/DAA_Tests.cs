using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class DAA_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForAddition()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0x15, 0x27, 42, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = true, ParityOverflow = true, Carry = false } });
            list.Add(new object[] { 0x93, 0x04, 97, new ConditionFlags() { Sign = true, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = false } });
            list.Add(new object[] { 0x66, 0x33, 99, new ConditionFlags() { Sign = true, Zero = false, HalfCarry = false, ParityOverflow = true, Carry = false } });
            list.Add(new object[] { 0x37, 0x56, 93, new ConditionFlags() { Sign = true, Zero = false, HalfCarry = true, ParityOverflow = true, Carry = false } });
            list.Add(new object[] { 0x34, 0x28, 62, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = true, ParityOverflow = false, Carry = false } });
            list.Add(new object[] { 0x00, 0x00, 0, new ConditionFlags() { Sign = false, Zero = true, HalfCarry = false, ParityOverflow = true, Carry = false } });
            list.Add(new object[] { 0x00, 0x01, 1, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = false } });
            list.Add(new object[] { 0x01, 0x00, 1, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = false } });
            list.Add(new object[] { 0x01, 0x01, 2, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = false } });
            list.Add(new object[] { 0x10, 0x10, 20, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = false } });
            list.Add(new object[] { 0x10, 0x19, 29, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = false } });
            list.Add(new object[] { 0x99, 0x01, 0, new ConditionFlags() { Sign = false, Zero = true, HalfCarry = true, ParityOverflow = true, Carry = true } });
            list.Add(new object[] { 0x99, 0x02, 1, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = true, ParityOverflow = false, Carry = true } });
            list.Add(new object[] { 0x76, 0x24, 0, new ConditionFlags() { Sign = false, Zero = true, HalfCarry = true, ParityOverflow = true, Carry = true } });
            list.Add(new object[] { 0x76, 0x25, 1, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = true, ParityOverflow = false, Carry = true } });
            list.Add(new object[] { 0x81, 0x29, 10, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = true, ParityOverflow = false, Carry = true } });
            list.Add(new object[] { 0x76, 0x89, 65, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = true, ParityOverflow = true, Carry = true } });

            return list;
        }

        public static IEnumerable<object[]> GetDataForSubtraction()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0x05, 0x03, 2, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = false } });
            list.Add(new object[] { 0x15, 0x04, 11, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = true, Carry = false } });
            list.Add(new object[] { 0x15, 0x15, 0, new ConditionFlags() { Sign = false, Zero = true, HalfCarry = false, ParityOverflow = true, Carry = false } });
            list.Add(new object[] { 0x27, 0x30, 97, new ConditionFlags() { Sign = true, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = true } });
            list.Add(new object[] { 0x16, 0x04, 12, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = true, Carry = false } });
            list.Add(new object[] { 0x97, 0x84, 13, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = false } });
            list.Add(new object[] { 0x10, 0x09, 1, new ConditionFlags() { Sign = false, Zero = false, HalfCarry = false, ParityOverflow = false, Carry = false } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForAddition))]
        public void Test_DAA_Addition(byte initialAValue, byte initialBValue, int expectedBcdResult, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD B
                DAA
                HALT
            ");

            var registers = new CPURegisters()
            {
                A = initialAValue,
                B = initialBValue,
            };

            var flags = new ConditionFlags()
            {
                // Affected flags.
                Zero = !expectedFlags.Zero,
                Sign = !expectedFlags.Sign,
                ParityOverflow = !expectedFlags.ParityOverflow,
                Carry = !expectedFlags.Carry,
                HalfCarry = !expectedFlags.HalfCarry,

                // Not affected (can't really test because ADD sets to false).
                Subtract = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            // Ensure these flags were updated.
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);

            // Ensure this flag was unaffected.
            Assert.False(state.Flags.Subtract);

            // Verify accumulator value.
            var actualValue = int.Parse(state.Registers.A.ToString("X2")); // Convert hex BCD value to an integer.
            Assert.Equal(expectedBcdResult, actualValue);

            Assert.Equal(3, state.Iterations);
            Assert.Equal(4 + 4 + 4, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [MemberData(nameof(GetDataForSubtraction))]
        public void Test_DAA_Subtraction(byte initialAValue, byte initialBValue, int expectedBcdResult, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                SUB B
                DAA
                HALT
            ");

            var registers = new CPURegisters()
            {
                A = initialAValue,
                B = initialBValue,
            };

            var flags = new ConditionFlags()
            {
                // Affected flags.
                Zero = !expectedFlags.Zero,
                Sign = !expectedFlags.Sign,
                ParityOverflow = !expectedFlags.ParityOverflow,
                Carry = !expectedFlags.Carry,
                HalfCarry = !expectedFlags.HalfCarry,

                // Not affected (can't really test because SUB sets to true).
                Subtract = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            // Ensure these flags were updated.
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);

            // Ensure this flag was unaffected (it will be affected though because we did a SUB operation).
            Assert.True(state.Flags.Subtract);

            // Verify accumulator value.
            var actualValue = int.Parse(state.Registers.A.ToString("X2")); // Convert hex BCD value to an integer.
            Assert.Equal(expectedBcdResult, actualValue);

            Assert.Equal(3, state.Iterations);
            Assert.Equal(4 + 4 + 4, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
