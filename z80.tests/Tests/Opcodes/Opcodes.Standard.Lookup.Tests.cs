using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class OpcodesStandardLookupTests : BaseTest
    {
        // TODO: Add test for each lookup table.
        [Fact]
        public void TestStandardLookupTableHasCorrectBytes()
        {
            foreach (var entry in Opcodes.StandardLookup)
            {
                var opcodeByteIndex = entry.Key;
                var opcodeByteDefined = entry.Value.Code;

                var description = String.Format("The byte defined as the index (0x{0:X2}) and the byte as defined in the Opcode instance (0x{1:X2}) should be the same (for Opcode {2}).",
                    opcodeByteIndex,
                    opcodeByteDefined,
                    entry.Value.Instruction);

                Assert.True(opcodeByteIndex == opcodeByteDefined, description);
            }
        }

        [Fact]
        public void TestLookupTableDoesNotContainExtendedPreambleBytes()
        {
            // These were duplicate/alias opcodes on the 8080, but on the Z80
            // they are used as "preamble" bytes to indicate extended opcode
            // groups. Here we ensure that they aren't in the standard lookup
            // table as aliases anymore.
            // TODO: Remove Remaining aliases from OpcodeBytes.Standard and implement Z80 specific behavior.
            Assert.False(Opcodes.StandardLookup.ContainsKey(0xED));
            Assert.False(Opcodes.StandardLookup.ContainsKey(0xCB));
            Assert.False(Opcodes.StandardLookup.ContainsKey(0xDD));
            Assert.False(Opcodes.StandardLookup.ContainsKey(0xFD));
        }
    }
}
