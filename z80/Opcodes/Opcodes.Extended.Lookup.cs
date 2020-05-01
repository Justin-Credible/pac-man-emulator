
using System.Collections.Generic;

namespace JustinCredible.ZilogZ80
{
    // A lookup table to locate opcode definitions by the opcode byte.
    public partial class Opcodes
    {
        // TODO
        public static Dictionary<byte, Opcode> ExtendedMainLookup = new Dictionary<byte, Opcode>();

        // TODO
        public static Dictionary<byte, Opcode> ExtendedBitLookup = new Dictionary<byte, Opcode>()
        {
            #region Extended - Bit Instructions

                #region RLC - Rotate Left
                    [OpcodeBytes.XT_BIT_RLC_B] = XT_BIT_RLC_B,
                    [OpcodeBytes.XT_BIT_RLC_C] = XT_BIT_RLC_C,
                    [OpcodeBytes.XT_BIT_RLC_D] = XT_BIT_RLC_D,
                    [OpcodeBytes.XT_BIT_RLC_E] = XT_BIT_RLC_E,
                    [OpcodeBytes.XT_BIT_RLC_H] = XT_BIT_RLC_H,
                    [OpcodeBytes.XT_BIT_RLC_L] = XT_BIT_RLC_L,
                    [OpcodeBytes.XT_BIT_RLC_HL] = XT_BIT_RLC_HL,
                    [OpcodeBytes.XT_BIT_RLC_A] = XT_BIT_RLC_A,
                #endregion

            #endregion
        };

        // TODO
        public static Dictionary<byte, Opcode> ExtendedIXBitLookup = new Dictionary<byte, Opcode>();

        // TODO
        public static Dictionary<byte, Opcode> ExtendedIXLookup = new Dictionary<byte, Opcode>();

        // TODO
        public static Dictionary<byte, Opcode> ExtendedIYBitLookup = new Dictionary<byte, Opcode>();

        // TODO
        public static Dictionary<byte, Opcode> ExtendedIYLookup = new Dictionary<byte, Opcode>();
    }
}
