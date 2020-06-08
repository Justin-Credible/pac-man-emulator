using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class BIT_X_MIY_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForStandardOpcodes()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0b10010010, 0, null, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { offset, 0b10010010, 1, null, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { offset, 0b10010010, 2, null, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { offset, 0b10010010, 3, null, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { offset, 0b10010010, 4, null, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { offset, 0b10010010, 5, null, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { offset, 0b10010010, 6, null, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { offset, 0b10010010, 7, null, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { offset, 0b01101101, 0, null, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { offset, 0b01101101, 1, null, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { offset, 0b01101101, 2, null, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { offset, 0b01101101, 3, null, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { offset, 0b01101101, 4, null, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { offset, 0b01101101, 5, null, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { offset, 0b01101101, 6, null, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { offset, 0b01101101, 7, null, new ConditionFlags() { Zero = true } });
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
                for (int b = 0x40; b <= 0x47; b++)
                {
                    list.Add(new object[] { offset, 0b10010010, null, b, new ConditionFlags() { Zero = true } });
                    list.Add(new object[] { offset, 0b01101101, null, b, new ConditionFlags() { Zero = false } });
                }

                // Bit 1
                for (int b = 0x48; b <= 0x4F; b++)
                {
                    list.Add(new object[] { offset, 0b10010010, null, b, new ConditionFlags() { Zero = false } });
                    list.Add(new object[] { offset, 0b01101101, null, b, new ConditionFlags() { Zero = true } });
                }

                // Bit 2
                for (int b = 0x50; b <= 0x57; b++)
                {
                    list.Add(new object[] { offset, 0b10010010, null, b, new ConditionFlags() { Zero = true } });
                    list.Add(new object[] { offset, 0b01101101, null, b, new ConditionFlags() { Zero = false } });
                }

                // Bit 3
                for (int b = 0x58; b <= 0x5F; b++)
                {
                    list.Add(new object[] { offset, 0b10010010, null, b, new ConditionFlags() { Zero = true } });
                    list.Add(new object[] { offset, 0b01101101, null, b, new ConditionFlags() { Zero = false } });
                }

                // Bit 4
                for (int b = 0x60; b <= 0x67; b++)
                {
                    list.Add(new object[] { offset, 0b10010010, null, b, new ConditionFlags() { Zero = false } });
                    list.Add(new object[] { offset, 0b01101101, null, b, new ConditionFlags() { Zero = true } });
                }

                // Bit 5
                for (int b = 0x68; b <= 0x6F; b++)
                {
                    list.Add(new object[] { offset, 0b10010010, null, b, new ConditionFlags() { Zero = true } });
                    list.Add(new object[] { offset, 0b01101101, null, b, new ConditionFlags() { Zero = false } });
                }

                // Bit 6
                for (int b = 0x70; b <= 0x77; b++)
                {
                    list.Add(new object[] { offset, 0b10010010, null, b, new ConditionFlags() { Zero = true } });
                    list.Add(new object[] { offset, 0b01101101, null, b, new ConditionFlags() { Zero = false } });
                }

                // Bit 7
                for (int b = 0x78; b <= 0x7F; b++)
                {
                    list.Add(new object[] { offset, 0b10010010, null, b, new ConditionFlags() { Zero = false } });
                    list.Add(new object[] { offset, 0b01101101, null, b, new ConditionFlags() { Zero = true } });
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
        public void Test_BIT_X_MIY(int offset, byte value, int? index, int? opcodeByte, ConditionFlags expectedFlags)
        {
            if ((index == null && opcodeByte == null) || (index != null && opcodeByte != null))
                throw new Exception("A bit index or opcode byte are required, but not both.");

            // If an index isn't present, we're assuming a opcode byte is there, which will be directly inserted
            // into the assembled ROM below, so we can just use a place holder index here.
            if (index == null)
                index = 0;

            var rom = AssembleSource($@"
                org 00h
                BIT {index}, (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            // Replace the fourth byte with the "alias" opcode so we can test the undocumented instructions.
            // These have the same behavior and mneumonics as the "documented" instruction 0x46, but different bytes.
            //   0  1  2  3
            //  DD CB ** XX
            //  ┃     ┃  ┃
            //  ┃     ┃  ┗━━ opcode byte
            //  ┃     ┗━━━━━ IY address offset
            //  ┗━━━━━━━━━━━ IY Bit instructions preamble (0xDD 0xCB)
            if (opcodeByte != null)
                rom[3] = (byte)opcodeByte.Value;

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = value;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Zero = !expectedFlags.Zero,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,

                    // Should be unaffected.
                    Carry = true,
                    Sign = true,
                    Parity = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(value, memory[0x2234 + offset]);

            // Should be affected.
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            // Should be unaffected
            Assert.True(state.Flags.Carry);
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Parity);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 20, state.Cycles);
            Assert.Equal(0x04, state.Registers.PC);
        }
    }
}
