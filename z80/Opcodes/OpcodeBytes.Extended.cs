using System;

namespace JustinCredible.ZilogZ80
{
    // A list of all the "extended instruction" opcode bytes; can be used to lookup the opcode definition.
    public partial class OpcodeBytes
    {
        /** Extended Instructions, Standard, preamble byte */
        public const byte XT_STD = 0xED; // TODO

        /** Extended Instructions, Bit Operations, preamble byte */
        public const byte XT_BIT = 0xCB; // TODO

        /** Extended Instructions, IX Register Operations, preamble byte */
        public const byte XT_IX = 0xDD; // TODO

        /** Extended Instructions, IX Register Bit Operations, preamble byte */
        public const byte XT_IX_BIT = 0xCB; // TODO

        /** Extended Instructions, IY Register Operations, preamble byte */
        public const byte XT_IY = 0xFD; // TODO

        /** Extended Instructions, IY Register Bit Operations, preamble byte */
        public const byte XT_IY_BIT = 0xCB; // TODO

        #region Extended - Bit Instructions

            #region RLC - Rotate Left
                public const byte XT_BIT_RLC_B = 0x00;
                public const byte XT_BIT_RLC_C = 0x01;
                public const byte XT_BIT_RLC_D = 0x02;
                public const byte XT_BIT_RLC_E = 0x03;
                public const byte XT_BIT_RLC_H = 0x04;
                public const byte XT_BIT_RLC_L = 0x05;
                public const byte XT_BIT_RLC_HL = 0x06;
                public const byte XT_BIT_RLC_A = 0x07;
            #endregion

        #endregion
    }
}
