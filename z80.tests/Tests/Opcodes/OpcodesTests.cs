using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class OpcodesTests : BaseTest
    {
        [Fact]
        public void TestStandardTableHasExpectedContent()
        {
            foreach (var entry in Opcodes.Standard)
            {
                var opcodeByteIndex = entry.Key;
                var opcode = entry.Value;

                var byteAssertionDescription = String.Format("The byte defined at the index (0x{0:X2}) and the byte as defined in the Opcode instance (0x{1:X2}) should be the same (for Opcode {2}).",
                    opcodeByteIndex,
                    opcode.Code,
                    opcode.Instruction
                );

                var enumAssertionDescription = String.Format("The byte defined at The byte defined at the index (0x{0:X2}) with name {1} should be of the set {2}.",
                    opcodeByteIndex,
                    opcode.Instruction,
                    InstructionSet.Standard.ToString()
                );

                Assert.True(opcodeByteIndex == opcode.Code, byteAssertionDescription);
                Assert.True(InstructionSet.Standard == opcode.InstructionSet, enumAssertionDescription);
            }
        }

        [Fact(Skip = "TODO: Not implemented yet")]
        public void TestExtendedTableHasExpectedContent()
        {
            foreach (var entry in Opcodes.Extended)
            {
                var opcodeByteIndex = entry.Key;
                var opcode = entry.Value;

                var byteAssertionDescription = String.Format("The byte defined at the index (0x{0:X2}) and the byte as defined in the Opcode instance (0x{1:X2}) should be the same (for Opcode {2}).",
                    opcodeByteIndex,
                    opcode.Code,
                    opcode.Instruction
                );

                var enumAssertionDescription = String.Format("The byte defined at The byte defined at the index (0x{0:X2}) with name {1} should be of the set {2}.",
                    opcodeByteIndex,
                    opcode.Instruction,
                    InstructionSet.Extended.ToString()
                );

                Assert.True(opcodeByteIndex == opcode.Code, byteAssertionDescription);
                Assert.True(InstructionSet.Extended == opcode.InstructionSet, enumAssertionDescription);
            }
        }

        [Fact(Skip = "TODO: Not implemented yet")]
        public void TestBitTableHasExpectedContent()
        {
            foreach (var entry in Opcodes.Bit)
            {
                var opcodeByteIndex = entry.Key;
                var opcode = entry.Value;

                var byteAssertionDescription = String.Format("The byte defined at the index (0x{0:X2}) and the byte as defined in the Opcode instance (0x{1:X2}) should be the same (for Opcode {2}).",
                    opcodeByteIndex,
                    opcode.Code,
                    opcode.Instruction
                );

                var enumAssertionDescription = String.Format("The byte defined at The byte defined at the index (0x{0:X2}) with name {1} should be of the set {2}.",
                    opcodeByteIndex,
                    opcode.Instruction,
                    InstructionSet.Bit.ToString()
                );

                Assert.True(opcodeByteIndex == opcode.Code, byteAssertionDescription);
                Assert.True(InstructionSet.Bit == opcode.InstructionSet, enumAssertionDescription);
            }
        }

        [Fact(Skip = "TODO: Not implemented yet")]
        public void TestIXTableHasExpectedContent()
        {
            foreach (var entry in Opcodes.IX)
            {
                var opcodeByteIndex = entry.Key;
                var opcode = entry.Value;

                var byteAssertionDescription = String.Format("The byte defined at the index (0x{0:X2}) and the byte as defined in the Opcode instance (0x{1:X2}) should be the same (for Opcode {2}).",
                    opcodeByteIndex,
                    opcode.Code,
                    opcode.Instruction
                );

                var enumAssertionDescription = String.Format("The byte defined at The byte defined at the index (0x{0:X2}) with name {1} should be of the set {2}.",
                    opcodeByteIndex,
                    opcode.Instruction,
                    InstructionSet.IX.ToString()
                );

                Assert.True(opcodeByteIndex == opcode.Code, byteAssertionDescription);
                Assert.True(InstructionSet.IX == opcode.InstructionSet, enumAssertionDescription);
            }
        }

        [Fact(Skip = "TODO: Not implemented yet")]
        public void TestIXBitTableHasExpectedContent()
        {
            foreach (var entry in Opcodes.IXBit)
            {
                var opcodeByteIndex = entry.Key;
                var opcode = entry.Value;

                var byteAssertionDescription = String.Format("The byte defined at the index (0x{0:X2}) and the byte as defined in the Opcode instance (0x{1:X2}) should be the same (for Opcode {2}).",
                    opcodeByteIndex,
                    opcode.Code,
                    opcode.Instruction
                );

                var enumAssertionDescription = String.Format("The byte defined at The byte defined at the index (0x{0:X2}) with name {1} should be of the set {2}.",
                    opcodeByteIndex,
                    opcode.Instruction,
                    InstructionSet.IXBit.ToString()
                );

                Assert.True(opcodeByteIndex == opcode.Code, byteAssertionDescription);
                Assert.True(InstructionSet.IXBit == opcode.InstructionSet, enumAssertionDescription);
            }
        }

        [Fact(Skip = "TODO: Not implemented yet")]
        public void TestIYTableHasExpectedContent()
        {
            foreach (var entry in Opcodes.IY)
            {
                var opcodeByteIndex = entry.Key;
                var opcode = entry.Value;

                var byteAssertionDescription = String.Format("The byte defined at the index (0x{0:X2}) and the byte as defined in the Opcode instance (0x{1:X2}) should be the same (for Opcode {2}).",
                    opcodeByteIndex,
                    opcode.Code,
                    opcode.Instruction
                );

                var enumAssertionDescription = String.Format("The byte defined at The byte defined at the index (0x{0:X2}) with name {1} should be of the set {2}.",
                    opcodeByteIndex,
                    opcode.Instruction,
                    InstructionSet.IY.ToString()
                );

                Assert.True(opcodeByteIndex == opcode.Code, byteAssertionDescription);
                Assert.True(InstructionSet.IY == opcode.InstructionSet, enumAssertionDescription);
            }
        }

        [Fact(Skip = "TODO: Not implemented yet")]
        public void TestIYBitTableHasExpectedContent()
        {
            foreach (var entry in Opcodes.IYBit)
            {
                var opcodeByteIndex = entry.Key;
                var opcode = entry.Value;

                var byteAssertionDescription = String.Format("The byte defined at the index (0x{0:X2}) and the byte as defined in the Opcode instance (0x{1:X2}) should be the same (for Opcode {2}).",
                    opcodeByteIndex,
                    opcode.Code,
                    opcode.Instruction
                );

                var enumAssertionDescription = String.Format("The byte defined at The byte defined at the index (0x{0:X2}) with name {1} should be of the set {2}.",
                    opcodeByteIndex,
                    opcode.Instruction,
                    InstructionSet.IYBit.ToString()
                );

                Assert.True(opcodeByteIndex == opcode.Code, byteAssertionDescription);
                Assert.True(InstructionSet.IYBit == opcode.InstructionSet, enumAssertionDescription);
            }
        }

        [Fact]
        public void TestStandardLookupTableDoesNotContainPreambleBytes()
        {
            // These were duplicate/alias opcodes on the 8080, but on the Z80
            // they are used as "preamble" bytes to indicate extended opcode
            // groups. Here we ensure that they aren't in the standard lookup
            // table as aliases anymore.
            Assert.False(Opcodes.Standard.ContainsKey(0xED));
            Assert.False(Opcodes.Standard.ContainsKey(0xCB));
            Assert.False(Opcodes.Standard.ContainsKey(0xDD));
            Assert.False(Opcodes.Standard.ContainsKey(0xFD));
        }

        [Fact]
        public void TestIXLookupTableDoesNotContainPreambleBytes()
        {
            // Ensure the third byte of the three byte opcodes is not present
            // in this table as an instruction, otherwise we'd have undefined
            // behavior at runtime.
            Assert.False(Opcodes.Standard.ContainsKey(0xCB));
        }

        [Fact]
        public void TestIYLookupTableDoesNotContainPreambleBytes()
        {
            // Ensure the third byte of the three byte opcodes is not present
            // in this table as an instruction, otherwise we'd have undefined
            // behavior at runtime.
            Assert.False(Opcodes.Standard.ContainsKey(0xCB));
        }

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
