
namespace JustinCredible.ZilogZ80
{
    // A list of all the "bit" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_BIT (0xCB).
    public partial class OpcodeBytes
    {
        #region RLC, r - Rotate left
            public const byte RLC_B = 0x00;
            public const byte RLC_C = 0x01;
            public const byte RLC_D = 0x02;
            public const byte RLC_E = 0x03;
            public const byte RLC_H = 0x04;
            public const byte RLC_L = 0x05;
            public const byte RLC_HL = 0x06;
            public const byte RLC_A = 0x07;
        #endregion

        #region RRC, r - Rotate right
            public const byte RRC_B = 0x08;
            public const byte RRC_C = 0x09;
            public const byte RRC_D = 0x0A;
            public const byte RRC_E = 0x0B;
            public const byte RRC_H = 0x0C;
            public const byte RRC_L = 0x0D;
            public const byte RRC_HL = 0x0E;
            public const byte RRC_A = 0x0F;
        #endregion

        #region RL, r - Rotate left through carry
            public const byte RL_B = 0x10;
            public const byte RL_C = 0x11;
            public const byte RL_D = 0x12;
            public const byte RL_E = 0x13;
            public const byte RL_H = 0x14;
            public const byte RL_L = 0x15;
            public const byte RL_HL = 0x16;
            public const byte RL_A = 0x17;
        #endregion

        #region RR, r - Rotate right through carry
            public const byte RR_B = 0x18;
            public const byte RR_C = 0x19;
            public const byte RR_D = 0x1A;
            public const byte RR_E = 0x1B;
            public const byte RR_H = 0x1C;
            public const byte RR_L = 0x1D;
            public const byte RR_HL = 0x1E;
            public const byte RR_A = 0x1F;
        #endregion
    }
}
