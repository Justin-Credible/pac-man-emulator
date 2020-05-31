
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IY bit" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all three byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IY (0xFD)
    // and the second byte is defined by OpcodeBytes.PREAMBLE_IY_BIT (0xCB).
    public partial class OpcodeBytes
    {
        #region Rotate

            #region RLC (IY+n), r - Rotate left
                public const byte RLC_IY_B = 0x00;
                public const byte RLC_IY_C = 0x01;
                public const byte RLC_IY_D = 0x02;
                public const byte RLC_IY_E = 0x03;
                public const byte RLC_IY_H = 0x04;
                public const byte RLC_IY_L = 0x05;
                public const byte RLC_IY = 0x06;
                public const byte RLC_IY_A = 0x07;
            #endregion

            #region RRC (IY+n), r - Rotate right
                public const byte RRC_IY_B = 0x08;
                public const byte RRC_IY_C = 0x09;
                public const byte RRC_IY_D = 0x0A;
                public const byte RRC_IY_E = 0x0B;
                public const byte RRC_IY_H = 0x0C;
                public const byte RRC_IY_L = 0x0D;
                public const byte RRC_IY = 0x0E;
                public const byte RRC_IY_A = 0x0F;
            #endregion

            #region RL (IY+n), r - Rotate left through carry
                public const byte RL_IY_B = 0x10;
                public const byte RL_IY_C = 0x11;
                public const byte RL_IY_D = 0x12;
                public const byte RL_IY_E = 0x13;
                public const byte RL_IY_H = 0x14;
                public const byte RL_IY_L = 0x15;
                public const byte RL_IY = 0x16;
                public const byte RL_IY_A = 0x17;
            #endregion

            #region RR (IY+n), r - Rotate right through carry
                public const byte RR_IY_B = 0x18;
                public const byte RR_IY_C = 0x19;
                public const byte RR_IY_D = 0x1A;
                public const byte RR_IY_E = 0x1B;
                public const byte RR_IY_H = 0x1C;
                public const byte RR_IY_L = 0x1D;
                public const byte RR_IY = 0x1E;
                public const byte RR_IY_A = 0x1F;
            #endregion

        #endregion
    }
}
