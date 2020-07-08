using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SET_X_MIX_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForStandardOpcodes()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0b00000000, 0, null, 0b00000001 });
                list.Add(new object[] { offset, 0b00000000, 1, null, 0b00000010 });
                list.Add(new object[] { offset, 0b00000000, 2, null, 0b00000100 });
                list.Add(new object[] { offset, 0b00000000, 3, null, 0b00001000 });
                list.Add(new object[] { offset, 0b00000000, 4, null, 0b00010000 });
                list.Add(new object[] { offset, 0b00000000, 5, null, 0b00100000 });
                list.Add(new object[] { offset, 0b00000000, 6, null, 0b01000000 });
                list.Add(new object[] { offset, 0b00000000, 7, null, 0b10000000 });
                list.Add(new object[] { offset, 0b11111111, 0, null, 0b11111111 });
                list.Add(new object[] { offset, 0b11111111, 1, null, 0b11111111 });
                list.Add(new object[] { offset, 0b11111111, 2, null, 0b11111111 });
                list.Add(new object[] { offset, 0b11111111, 3, null, 0b11111111 });
                list.Add(new object[] { offset, 0b11111111, 4, null, 0b11111111 });
                list.Add(new object[] { offset, 0b11111111, 5, null, 0b11111111 });
                list.Add(new object[] { offset, 0b11111111, 6, null, 0b11111111 });
                list.Add(new object[] { offset, 0b11111111, 7, null, 0b11111111 });
                list.Add(new object[] { offset, 0b11001010, 0, null, 0b11001011 });
                list.Add(new object[] { offset, 0b11001010, 1, null, 0b11001010 });
                list.Add(new object[] { offset, 0b11001010, 2, null, 0b11001110 });
                list.Add(new object[] { offset, 0b11001010, 3, null, 0b11001010 });
                list.Add(new object[] { offset, 0b11001010, 4, null, 0b11011010 });
                list.Add(new object[] { offset, 0b11001010, 5, null, 0b11101010 });
                list.Add(new object[] { offset, 0b11001010, 6, null, 0b11001010 });
                list.Add(new object[] { offset, 0b11001010, 7, null, 0b11001010 });
            }

            return list;
        }

        public static IEnumerable<object[]> GetDataForUndocumentedOpcodes()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                // Bit 0
                for (int b = 0xC0; b <= 0xC7; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000001 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001011 });
                }

                // Bit 1
                for (int b = 0xC8; b <= 0xCF; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000010 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001010 });
                }

                // Bit 2
                for (int b = 0xD0; b <= 0xD7; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000100 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001110 });
                }

                // Bit 3
                for (int b = 0xD8; b <= 0xDF; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00001000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001010 });
                }

                // Bit 4
                for (int b = 0xE0; b <= 0xE7; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00010000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11011010 });
                }

                // Bit 5
                for (int b = 0xE8; b <= 0xEF; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00100000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11101010 });
                }

                // Bit 6
                for (int b = 0xF0; b <= 0xF7; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b01000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001010 });
                }

                // Bit 7
                for (int b = 0xF8; b <= 0xFF; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b10000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001010 });
                }
            }

            return list;
        }

        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();
            list.AddRange(GetDataForStandardOpcodes());
            list.AddRange(GetDataForUndocumentedOpcodes());
            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_SET_X_MIX(int offset, byte initialValue, int? index, int? opcodeByte, byte expectedValue)
        {
            if ((index == null && opcodeByte == null) || (index != null && opcodeByte != null))
                throw new Exception("A bit index or opcode byte are required, but not both.");

            // If an index isn't present, we're assuming a opcode byte is there, which will be directly inserted
            // into the assembled ROM below, so we can just use a place holder index here.
            if (index == null)
                index = 0;

            var rom = AssembleSource($@"
                org 00h
                SET {index}, (IX {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            // Replace the fourth byte with the "alias" opcode so we can test the undocumented instructions.
            // These have the same behavior and mneumonics as the "documented" instruction 0x46, but different bytes.
            //   0  1  2  3
            //  DD CB ** XX
            //  ┃     ┃  ┃
            //  ┃     ┃  ┗━━ opcode byte
            //  ┃     ┗━━━━━ IX address offset
            //  ┗━━━━━━━━━━━ IX Bit instructions preamble (0xDD 0xCB)
            if (opcodeByte != null)
                rom[3] = (byte)opcodeByte.Value;

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = initialValue;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IX = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be unaffected.
                    Zero = true,
                    Subtract = true,
                    HalfCarry = true,
                    Carry = true,
                    Sign = true,
                    ParityOverflow = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(expectedValue, state.Memory[0x2234 + offset]);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
