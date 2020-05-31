
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IX bit" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all three byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IX (0xDD)
    // and the second byte is defined by OpcodeBytes.PREAMBLE_IX_BIT (0xCB).
    public partial class OpcodeBytes
    {
        #region Rotate

            #region RLC (IX+n), r - Rotate left
                public const byte RLC_IX_B = 0x00;
                public const byte RLC_IX_C = 0x01;
                public const byte RLC_IX_D = 0x02;
                public const byte RLC_IX_E = 0x03;
                public const byte RLC_IX_H = 0x04;
                public const byte RLC_IX_L = 0x05;
                public const byte RLC_IX = 0x06;
                public const byte RLC_IX_A = 0x07;
            #endregion

            #region RRC (IX+n), r - Rotate right
                public const byte RRC_IX_B = 0x08;
                public const byte RRC_IX_C = 0x09;
                public const byte RRC_IX_D = 0x0A;
                public const byte RRC_IX_E = 0x0B;
                public const byte RRC_IX_H = 0x0C;
                public const byte RRC_IX_L = 0x0D;
                public const byte RRC_IX = 0x0E;
                public const byte RRC_IX_A = 0x0F;
            #endregion

            #region RL (IX+n), r - Rotate left through carry
                public const byte RL_IX_B = 0x10;
                public const byte RL_IX_C = 0x11;
                public const byte RL_IX_D = 0x12;
                public const byte RL_IX_E = 0x13;
                public const byte RL_IX_H = 0x14;
                public const byte RL_IX_L = 0x15;
                public const byte RL_IX = 0x16;
                public const byte RL_IX_A = 0x17;
            #endregion

            #region RR (IX+n), r - Rotate right through carry
                public const byte RR_IX_B = 0x18;
                public const byte RR_IX_C = 0x19;
                public const byte RR_IX_D = 0x1A;
                public const byte RR_IX_E = 0x1B;
                public const byte RR_IX_H = 0x1C;
                public const byte RR_IX_L = 0x1D;
                public const byte RR_IX = 0x1E;
                public const byte RR_IX_A = 0x1F;
            #endregion

        #endregion
    }
}