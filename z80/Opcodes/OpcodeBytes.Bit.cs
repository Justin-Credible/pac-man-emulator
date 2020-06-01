
namespace JustinCredible.ZilogZ80
{
    // A list of all the "bit" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_BIT (0xCB).
    public partial class OpcodeBytes
    {
        #region Rotate

            #region RLC r - Rotate left
                public const byte RLC_B = 0x00;
                public const byte RLC_C = 0x01;
                public const byte RLC_D = 0x02;
                public const byte RLC_E = 0x03;
                public const byte RLC_H = 0x04;
                public const byte RLC_L = 0x05;
                public const byte RLC_HL = 0x06;
                public const byte RLC_A = 0x07;
            #endregion

            #region RRC r - Rotate right
                public const byte RRC_B = 0x08;
                public const byte RRC_C = 0x09;
                public const byte RRC_D = 0x0A;
                public const byte RRC_E = 0x0B;
                public const byte RRC_H = 0x0C;
                public const byte RRC_L = 0x0D;
                public const byte RRC_HL = 0x0E;
                public const byte RRC_A = 0x0F;
            #endregion

            #region RL r - Rotate left through carry
                public const byte RL_B = 0x10;
                public const byte RL_C = 0x11;
                public const byte RL_D = 0x12;
                public const byte RL_E = 0x13;
                public const byte RL_H = 0x14;
                public const byte RL_L = 0x15;
                public const byte RL_HL = 0x16;
                public const byte RL_A = 0x17;
            #endregion

            #region RR r - Rotate right through carry
                public const byte RR_B = 0x18;
                public const byte RR_C = 0x19;
                public const byte RR_D = 0x1A;
                public const byte RR_E = 0x1B;
                public const byte RR_H = 0x1C;
                public const byte RR_L = 0x1D;
                public const byte RR_HL = 0x1E;
                public const byte RR_A = 0x1F;
            #endregion

        #endregion

        #region Shift

            #region SLA r - Shift left (arithmetic)
                public const byte SLA_B = 0x20;
                public const byte SLA_C = 0x21;
                public const byte SLA_D = 0x22;
                public const byte SLA_E = 0x23;
                public const byte SLA_H = 0x24;
                public const byte SLA_L = 0x25;
                public const byte SLA_HL = 0x26;
                public const byte SLA_A = 0x27;
            #endregion

            #region SRA r - Shift right (arithmetic)
                public const byte SRA_B = 0x28;
                public const byte SRA_C = 0x29;
                public const byte SRA_D = 0x2A;
                public const byte SRA_E = 0x2B;
                public const byte SRA_H = 0x2C;
                public const byte SRA_L = 0x2D;
                public const byte SRA_HL = 0x2E;
                public const byte SRA_A = 0x2F;
            #endregion

            // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
            // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
            #region SLL r - Shift left ?? (undocumented)
                public const byte SLL_B = 0x30;
                public const byte SLL_C = 0x31;
                public const byte SLL_D = 0x32;
                public const byte SLL_E = 0x33;
                public const byte SLL_H = 0x34;
                public const byte SLL_L = 0x35;
                public const byte SLL_HL = 0x36;
                public const byte SLL_A = 0x37;
            #endregion

            #region SRL r - Shift right logical
                public const byte SRL_B = 0x38;
                public const byte SRL_C = 0x39;
                public const byte SRL_D = 0x3A;
                public const byte SRL_E = 0x3B;
                public const byte SRL_H = 0x3C;
                public const byte SRL_L = 0x3D;
                public const byte SRL_HL = 0x3E;
                public const byte SRL_A = 0x3F;
            #endregion

        #endregion
    }
}
