using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class OpcodesTests : BaseTest
    {
        [Fact]
        public void TestGetOpcode()
        {
            // TODO
        }

        [Theory]
        [InlineData(0xED, true)]
        [InlineData(0xCB, true)]
        [InlineData(0xDD, true)]
        [InlineData(0xFD, true)]
        [InlineData(0x00, false)]
        [InlineData(0x10, false)]
        public void TestIsExtendedPreambleByte(byte opcodeByte, bool expected)
        {
            var actual = Opcodes.IsExtendedPreambleByte(opcodeByte);
            Assert.Equal(expected, actual);
        }
    }
}
