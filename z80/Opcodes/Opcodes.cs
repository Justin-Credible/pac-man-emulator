
using System;

namespace JustinCredible.ZilogZ80
{
    // Methods for the Opcodes class.
    public partial class Opcodes
    {
        /**
         * Used to fetch an opcode from memory at the given address of the program counter.
         * This handles both single and multi-byte opcodes.
         */
        public static Opcode GetOpcode(UInt16 programCounter, byte[] memory)
        {
            var opcodeByte1 = memory[programCounter];

            // If this isn't a preamble byte, we can just bail early and lookup
            // one of the "standard" instructions.
            if (!IsExtendedPreambleByte(opcodeByte1))
                return Lookup[opcodeByte1];

            // If we had a preamble byte there could be up to 2 more bytes that
            // determine which extended instruction set to use.
            if (opcodeByte1 == OpcodeBytes.XT_STD)
            {
                // Extended Instructions
                var opcodeByte2 = memory[programCounter + 1];
                return ExtendedMainLookup[opcodeByte2];
            }
            else if (opcodeByte1 == OpcodeBytes.XT_BIT)
            {
                // Extended Instructions, Bit Operations
                var opcodeByte2 = memory[programCounter + 1];
                return ExtendedBitLookup[opcodeByte2];
            }
            else if (opcodeByte1 == OpcodeBytes.XT_IX)
            {
                var opcodeByte2 = memory[programCounter + 1];

                if (opcodeByte2 == OpcodeBytes.XT_IX_BIT)
                {
                    // Extended Instructions, IX Register Bit Operations
                    var opcodeByte3 = memory[programCounter + 2];
                    return ExtendedIXBitLookup[opcodeByte3];
                }
                else
                {
                    // Extended Instructions, IX Register
                    return ExtendedIXLookup[opcodeByte2];
                }
            }
            else if (opcodeByte1 == OpcodeBytes.XT_IY)
            {
                var opcodeByte2 = memory[programCounter + 1];

                if (opcodeByte2 == OpcodeBytes.XT_IY_BIT)
                {
                    // Extended Instructions, IY Register Bit Operations
                    var opcodeByte3 = memory[programCounter + 2];
                    return ExtendedIYBitLookup[opcodeByte3];
                }
                else
                {
                    // Extended Instructions, IY Register
                    return ExtendedIYLookup[opcodeByte2];
                }
            }
            else
            {
                // The IsExtendedPreambleByte indicated this was a preamble byte
                // but it was not handled above.
                throw new Exception(String.Format("Encountered an unhandled preamble byte 0x{0:X2} at memory address 0x{1:X4} when attempting to grab an opcode.", opcodeByte1, programCounter));
            }
        }

        /**
         * Used to check if this byte is a preamble byte that indicates a
         * multi-byte opcode, which are the extended opcode sets.
         */
        public static bool IsExtendedPreambleByte(byte bite)
        {
            return bite == OpcodeBytes.XT_STD
                || bite == OpcodeBytes.XT_BIT
                || bite == OpcodeBytes.XT_IX
                || bite == OpcodeBytes.XT_IY;
        }
    }
}
