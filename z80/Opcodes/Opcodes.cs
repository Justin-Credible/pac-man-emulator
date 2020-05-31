
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JustinCredible.ZilogZ80
{
    // Methods for the Opcodes class.
    public partial class Opcodes
    {
        /**
         * A lookup dictionary for all of the single-byte standard opcodes.
         */
        internal static Dictionary<byte, Opcode> Standard;

        /**
         * A lookup dictionary for all of the "extended" opcodes, which are two
         * bytes long, with the first byte being OpcodeBytes.PREAMBLE_EXTENDED (0xED).
         */
        internal static Dictionary<byte, Opcode> Extended;

        /**
         * A lookup dictionary for all of the "bit" opcodes, which are two
         * bytes long, with the first byte being OpcodeBytes.PREAMBLE_BIT (0xCB).
         */
        internal static Dictionary<byte, Opcode> Bit;

        /**
         * A lookup dictionary for all of the IX register opcodes, which are two
         * bytes long, with the first byte being OpcodeBytes.PREAMBLE_IX (0xDD).
         */
        internal static Dictionary<byte, Opcode> IX;

        /**
         * A lookup dictionary for all of the IX register bit opcodes, which are three
         * bytes long, with the first byte being OpcodeBytes.PREAMBLE_IX (0xDD)
         * and the second byte being OpcodeBytes.PREAMBLE_IX_BIT (0xCB).
         */
        internal static Dictionary<byte, Opcode> IXBit;

        /**
         * A lookup dictionary for all of the IY register opcodes, which are two
         * bytes long, with the first byte being OpcodeBytes.PREAMBLE_IY (0xFD).
         */
        internal static Dictionary<byte, Opcode> IY;

        /**
         * A lookup dictionary for all of the IX register bit opcodes, which are three
         * bytes long, with the first byte being OpcodeBytes.PREAMBLE_IY (0xFD)
         * and the second byte being OpcodeBytes.PREAMBLE_IY_BIT (0xCB).
         */
        internal static Dictionary<byte, Opcode> IYBit;

        /**
         * Used to fetch an opcode from memory at the given address of the program counter.
         * This handles both single and multi-byte opcodes.
         */
        public static Opcode GetOpcode(UInt16 programCounter, byte[] memory)
        {
            try
            {
                return GetOpcodeInternal(programCounter, memory);
            }
            catch (Exception exception) {
                // Wrap the exception with some helpful information: the opcode byte as well
                // as the location of the program counter.
                throw new Exception(String.Format("Unable to fetch opcode structure for byte 0x{0:X2} at memory address 0x{1:X4}.", memory[programCounter], programCounter), exception);
            }
        }

        private static Opcode GetOpcodeInternal(UInt16 programCounter, byte[] memory)
        {
            var opcodeByte1 = memory[programCounter];

            // If this isn't a preamble byte, we can just bail early and lookup
            // one of the "standard" instructions.
            if (!IsExtendedPreambleByte(opcodeByte1))
                return Standard[opcodeByte1];

            // If we had a preamble byte there could be up to 2 more bytes that
            // determine which extended instruction set to use.
            if (opcodeByte1 == OpcodeBytes.PREAMBLE_EXTENDED)
            {
                // Extended Instructions
                var opcodeByte2 = memory[programCounter + 1];
                return Extended[opcodeByte2];
            }
            else if (opcodeByte1 == OpcodeBytes.PREAMBLE_BIT)
            {
                // Extended Instructions, Bit Operations
                var opcodeByte2 = memory[programCounter + 1];
                return Bit[opcodeByte2];
            }
            else if (opcodeByte1 == OpcodeBytes.PREAMBLE_IX)
            {
                var opcodeByte2 = memory[programCounter + 1];

                if (opcodeByte2 == OpcodeBytes.PREAMBLE_IX_BIT)
                {
                    // Extended Instructions, IX Register Bit Operations
                    // Third byte is an address offset, and the fourth byte is the actual opcode.
                    var opcodeByte4 = memory[programCounter + 3];
                    return IXBit[opcodeByte4];
                }
                else
                {
                    // Extended Instructions, IX Register
                    return IX[opcodeByte2];
                }
            }
            else if (opcodeByte1 == OpcodeBytes.PREAMBLE_IY)
            {
                var opcodeByte2 = memory[programCounter + 1];

                if (opcodeByte2 == OpcodeBytes.PREAMBLE_IY_BIT)
                {
                    // Extended Instructions, IY Register Bit Operations
                    // Third byte is an address offset, and the fourth byte is the actual opcode.
                    var opcodeByte4 = memory[programCounter + 3];
                    return IY[opcodeByte4];
                }
                else
                {
                    // Extended Instructions, IY Register
                    return IYBit[opcodeByte2];
                }
            }
            else
            {
                // The IsExtendedPreambleByte indicated this was a preamble byte
                // but it was not handled above.
                throw new NotImplementedException("Unhandled preamble byte.");
            }
        }

        /**
         * Used to check if this byte is a preamble byte that indicates a
         * multi-byte opcode, which are the extended opcode sets.
         */
        public static bool IsExtendedPreambleByte(byte bite)
        {
            return bite == OpcodeBytes.PREAMBLE_EXTENDED
                || bite == OpcodeBytes.PREAMBLE_BIT
                || bite == OpcodeBytes.PREAMBLE_IX
                || bite == OpcodeBytes.PREAMBLE_IY;
        }
    }
}
