
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

        #region Shift

            #region SLA (IX+n), r - Shift left (arithmetic)
                public const byte SLA_IX_B = 0x20;
                public const byte SLA_IX_C = 0x21;
                public const byte SLA_IX_D = 0x22;
                public const byte SLA_IX_E = 0x23;
                public const byte SLA_IX_H = 0x24;
                public const byte SLA_IX_L = 0x25;
                public const byte SLA_IX = 0x26;
                public const byte SLA_IX_A = 0x27;
            #endregion

            #region SRA (IX+n), r - Shift right (arithmetic)
                public const byte SRA_IX_B = 0x28;
                public const byte SRA_IX_C = 0x29;
                public const byte SRA_IX_D = 0x2A;
                public const byte SRA_IX_E = 0x2B;
                public const byte SRA_IX_H = 0x2C;
                public const byte SRA_IX_L = 0x2D;
                public const byte SRA_IX = 0x2E;
                public const byte SRA_IX_A = 0x2F;
            #endregion

            // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
            // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
            #region SLL (IX+n), r - Shift left ?? (undocumented)
                public const byte SLL_IX_B = 0x30;
                public const byte SLL_IX_C = 0x31;
                public const byte SLL_IX_D = 0x32;
                public const byte SLL_IX_E = 0x33;
                public const byte SLL_IX_H = 0x34;
                public const byte SLL_IX_L = 0x35;
                public const byte SLL_IX = 0x36;
                public const byte SLL_IX_A = 0x37;
            #endregion

            #region SRL (IX+n), r - Shift right logical
                public const byte SRL_IX_B = 0x38;
                public const byte SRL_IX_C = 0x39;
                public const byte SRL_IX_D = 0x3A;
                public const byte SRL_IX_E = 0x3B;
                public const byte SRL_IX_H = 0x3C;
                public const byte SRL_IX_L = 0x3D;
                public const byte SRL_IX = 0x3E;
                public const byte SRL_IX_A = 0x3F;
            #endregion

        #endregion
    }
}
