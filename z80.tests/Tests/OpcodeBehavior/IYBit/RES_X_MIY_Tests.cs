using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RES_X_MIY_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForStandardOpcodes()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0b00000000, 0, null, 0b00000000 });
                list.Add(new object[] { offset, 0b00000000, 1, null, 0b00000000 });
                list.Add(new object[] { offset, 0b00000000, 2, null, 0b00000000 });
                list.Add(new object[] { offset, 0b00000000, 3, null, 0b00000000 });
                list.Add(new object[] { offset, 0b00000000, 4, null, 0b00000000 });
                list.Add(new object[] { offset, 0b00000000, 5, null, 0b00000000 });
                list.Add(new object[] { offset, 0b00000000, 6, null, 0b00000000 });
                list.Add(new object[] { offset, 0b00000000, 7, null, 0b00000000 });
                list.Add(new object[] { offset, 0b11111111, 0, null, 0b11111110 });
                list.Add(new object[] { offset, 0b11111111, 1, null, 0b11111101 });
                list.Add(new object[] { offset, 0b11111111, 2, null, 0b11111011 });
                list.Add(new object[] { offset, 0b11111111, 3, null, 0b11110111 });
                list.Add(new object[] { offset, 0b11111111, 4, null, 0b11101111 });
                list.Add(new object[] { offset, 0b11111111, 5, null, 0b11011111 });
                list.Add(new object[] { offset, 0b11111111, 6, null, 0b10111111 });
                list.Add(new object[] { offset, 0b11111111, 7, null, 0b01111111 });
                list.Add(new object[] { offset, 0b11001010, 0, null, 0b11001010 });
                list.Add(new object[] { offset, 0b11001010, 1, null, 0b11001000 });
                list.Add(new object[] { offset, 0b11001010, 2, null, 0b11001010 });
                list.Add(new object[] { offset, 0b11001010, 3, null, 0b11000010 });
                list.Add(new object[] { offset, 0b11001010, 4, null, 0b11001010 });
                list.Add(new object[] { offset, 0b11001010, 5, null, 0b11001010 });
                list.Add(new object[] { offset, 0b11001010, 6, null, 0b10001010 });
                list.Add(new object[] { offset, 0b11001010, 7, null, 0b01001010 });
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
                for (int b = 0x80; b <= 0x87; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111110 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001010 });
                }

                // Bit 1
                for (int b = 0x88; b <= 0x8F; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111101 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001000 });
                }

                // Bit 2
                for (int b = 0x90; b <= 0x97; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11111011 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001010 });
                }

                // Bit 3
                for (int b = 0x98; b <= 0x9F; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11110111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11000010 });
                }

                // Bit 4
                for (int b = 0xA0; b <= 0xA7; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11101111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001010 });
                }

                // Bit 5
                for (int b = 0xA8; b <= 0xAF; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b11011111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b11001010 });
                }

                // Bit 6
                for (int b = 0xB0; b <= 0xB7; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b10111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b10001010 });
                }

                // Bit 7
                for (int b = 0xB8; b <= 0xBF; b++)
                {
                    list.Add(new object[] { offset, 0b00000000, null, b, 0b00000000 });
                    list.Add(new object[] { offset, 0b11111111, null, b, 0b01111111 });
                    list.Add(new object[] { offset, 0b11001010, null, b, 0b01001010 });
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
        public void Test_RES_X_MIY(int offset, byte initialValue, int? index, int? opcodeByte, byte expectedValue)
        {
            if ((index == null && opcodeByte == null) || (index != null && opcodeByte != null))
                throw new Exception("A bit index or opcode byte are required, but not both.");

            // If an index isn't present, we're assuming a opcode byte is there, which will be directly inserted
            // into the assembled ROM below, so we can just use a place holder index here.
            if (index == null)
                index = 0;

            var rom = AssembleSource($@"
                org 00h
                RES {index}, (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            // Replace the fourth byte with the "alias" opcode so we can test the undocumented instructions.
            // These have the same behavior and mneumonics as the "documented" instruction 0x46, but different bytes.
            //   0  1  2  3
            //  FD CB ** XX
            //  ┃     ┃  ┃
            //  ┃     ┃  ┗━━ opcode byte
            //  ┃     ┗━━━━━ IY address offset
            //  ┗━━━━━━━━━━━ IY Bit instructions preamble (0xFD 0xCB)
            if (opcodeByte != null)
                rom[3] = (byte)opcodeByte.Value;

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = initialValue;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x2234,
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

            Assert.Equal(expectedValue, memory[0x2234 + offset]);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
