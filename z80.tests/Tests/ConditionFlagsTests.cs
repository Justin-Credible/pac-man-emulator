using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ConditionFlagsTests : BaseTest
    {
        [Theory]
        [InlineData(false, false, false, false, false, false, 0b00000000)]
        [InlineData(true, false, false, false, false, false, 0b10000000)]
        [InlineData(true, true, false, false, false, false, 0b11000000)]
        [InlineData(true, true, true, false, false, false, 0b11010000)]
        [InlineData(true, true, true, true, false, false, 0b11010100)]
        [InlineData(true, true, true, true, true, false, 0b11010110)]
        [InlineData(true, true, true, true, true, true, 0b11010111)]
        [InlineData(false, true, true, true, true, true, 0b01010111)]
        [InlineData(false, false, true, true, true, true, 0b00010111)]
        [InlineData(false, false, false, true, true, true, 0b00000111)]
        [InlineData(false, false, false, false, true, true, 0b00000011)]
        [InlineData(false, false, false, false, false, true, 0b00000001)]

        public void TestToByte(bool sign, bool zero, bool auxCarry, bool parity, bool addSub, bool carry, byte expected)
        {
            var flags = new ConditionFlags()
            {
                Sign = sign,
                Zero = zero,
                AuxCarry = auxCarry,
                Parity = parity,
                AddSub = addSub,
                Carry = carry,
            };

            var actual = flags.ToByte();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, false, false, false, false, false, 0b00000000)]
        [InlineData(true, false, false, false, false, false, 0b10000000)]
        [InlineData(true, true, false, false, false, false, 0b11000000)]
        [InlineData(true, true, true, false, false, false, 0b11010000)]
        [InlineData(true, true, true, true, false, false, 0b11010100)]
        [InlineData(true, true, true, true, true, false, 0b11010110)]
        [InlineData(true, true, true, true, true, true, 0b11010111)]
        [InlineData(false, true, true, true, true, true, 0b01010111)]
        [InlineData(false, false, true, true, true, true, 0b00010111)]
        [InlineData(false, false, false, true, true, true, 0b00000111)]
        [InlineData(false, false, false, false, true, true, 0b00000011)]
        [InlineData(false, false, false, false, false, true, 0b00000001)]
        public void TestSetFromByte(bool expectedSign, bool expectedZero, bool expectedAuxCarry, bool expectedParity, bool exepectedAddSub, bool expectedCarry, byte flagsAsByte)
        {
            var flags = new ConditionFlags();
            flags.SetFromByte(flagsAsByte);

            Assert.Equal(expectedSign, flags.Sign);
            Assert.Equal(expectedZero, flags.Zero);
            Assert.Equal(expectedAuxCarry, flags.AuxCarry);
            Assert.Equal(expectedParity, flags.Parity);
            Assert.Equal(exepectedAddSub, flags.AddSub);
            Assert.Equal(expectedCarry, flags.Carry);
        }
    }
}