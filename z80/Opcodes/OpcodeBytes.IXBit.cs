
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

        #region Test Bit

            public const byte BIT_0_IX_2 = 0x40;
            public const byte BIT_0_IX_3 = 0x41;
            public const byte BIT_0_IX_4 = 0x42;
            public const byte BIT_0_IX_5 = 0x43;
            public const byte BIT_0_IX_6 = 0x44;
            public const byte BIT_0_IX_7 = 0x45;
            public const byte BIT_0_IX = 0x46;
            public const byte BIT_0_IX_8 = 0x47;

            public const byte BIT_1_IX_2 = 0x48;
            public const byte BIT_1_IX_3 = 0x49;
            public const byte BIT_1_IX_4 = 0x4A;
            public const byte BIT_1_IX_5 = 0x4B;
            public const byte BIT_1_IX_6 = 0x4C;
            public const byte BIT_1_IX_7 = 0x4D;
            public const byte BIT_1_IX = 0x4E;
            public const byte BIT_1_IX_8 = 0x4F;

            public const byte BIT_2_IX_2 = 0x50;
            public const byte BIT_2_IX_3 = 0x51;
            public const byte BIT_2_IX_4 = 0x52;
            public const byte BIT_2_IX_5 = 0x53;
            public const byte BIT_2_IX_6 = 0x54;
            public const byte BIT_2_IX_7 = 0x55;
            public const byte BIT_2_IX = 0x56;
            public const byte BIT_2_IX_8 = 0x57;

            public const byte BIT_3_IX_2 = 0x58;
            public const byte BIT_3_IX_3 = 0x59;
            public const byte BIT_3_IX_4 = 0x5A;
            public const byte BIT_3_IX_5 = 0x5B;
            public const byte BIT_3_IX_6 = 0x5C;
            public const byte BIT_3_IX_7 = 0x5D;
            public const byte BIT_3_IX = 0x5E;
            public const byte BIT_3_IX_8 = 0x5F;

            public const byte BIT_4_IX_2 = 0x60;
            public const byte BIT_4_IX_3 = 0x61;
            public const byte BIT_4_IX_4 = 0x62;
            public const byte BIT_4_IX_5 = 0x63;
            public const byte BIT_4_IX_6 = 0x64;
            public const byte BIT_4_IX_7 = 0x65;
            public const byte BIT_4_IX = 0x66;
            public const byte BIT_4_IX_8 = 0x67;

            public const byte BIT_5_IX_2 = 0x68;
            public const byte BIT_5_IX_3 = 0x69;
            public const byte BIT_5_IX_4 = 0x6A;
            public const byte BIT_5_IX_5 = 0x6B;
            public const byte BIT_5_IX_6 = 0x6C;
            public const byte BIT_5_IX_7 = 0x6D;
            public const byte BIT_5_IX = 0x6E;
            public const byte BIT_5_IX_8 = 0x6F;

            public const byte BIT_6_IX_2 = 0x70;
            public const byte BIT_6_IX_3 = 0x71;
            public const byte BIT_6_IX_4 = 0x72;
            public const byte BIT_6_IX_5 = 0x73;
            public const byte BIT_6_IX_6 = 0x74;
            public const byte BIT_6_IX_7 = 0x75;
            public const byte BIT_6_IX = 0x76;
            public const byte BIT_6_IX_8 = 0x77;

            public const byte BIT_7_IX_2 = 0x78;
            public const byte BIT_7_IX_3 = 0x79;
            public const byte BIT_7_IX_4 = 0x7A;
            public const byte BIT_7_IX_5 = 0x7B;
            public const byte BIT_7_IX_6 = 0x7C;
            public const byte BIT_7_IX_7 = 0x7D;
            public const byte BIT_7_IX = 0x7E;
            public const byte BIT_7_IX_8 = 0x7F;

        #endregion

        #region Reset Bit

            public const byte RES_0_IX_2 = 0x80;
            public const byte RES_0_IX_3 = 0x81;
            public const byte RES_0_IX_4 = 0x82;
            public const byte RES_0_IX_5 = 0x83;
            public const byte RES_0_IX_6 = 0x84;
            public const byte RES_0_IX_7 = 0x85;
            public const byte RES_0_IX = 0x86;
            public const byte RES_0_IX_8 = 0x87;

            public const byte RES_1_IX_2 = 0x88;
            public const byte RES_1_IX_3 = 0x89;
            public const byte RES_1_IX_4 = 0x8A;
            public const byte RES_1_IX_5 = 0x8B;
            public const byte RES_1_IX_6 = 0x8C;
            public const byte RES_1_IX_7 = 0x8D;
            public const byte RES_1_IX = 0x8E;
            public const byte RES_1_IX_8 = 0x8F;

            public const byte RES_2_IX_2 = 0x90;
            public const byte RES_2_IX_3 = 0x91;
            public const byte RES_2_IX_4 = 0x92;
            public const byte RES_2_IX_5 = 0x93;
            public const byte RES_2_IX_6 = 0x94;
            public const byte RES_2_IX_7 = 0x95;
            public const byte RES_2_IX = 0x96;
            public const byte RES_2_IX_8 = 0x97;

            public const byte RES_3_IX_2 = 0x98;
            public const byte RES_3_IX_3 = 0x99;
            public const byte RES_3_IX_4 = 0x9A;
            public const byte RES_3_IX_5 = 0x9B;
            public const byte RES_3_IX_6 = 0x9C;
            public const byte RES_3_IX_7 = 0x9D;
            public const byte RES_3_IX = 0x9E;
            public const byte RES_3_IX_8 = 0x9F;

            public const byte RES_4_IX_2 = 0xA0;
            public const byte RES_4_IX_3 = 0xA1;
            public const byte RES_4_IX_4 = 0xA2;
            public const byte RES_4_IX_5 = 0xA3;
            public const byte RES_4_IX_6 = 0xA4;
            public const byte RES_4_IX_7 = 0xA5;
            public const byte RES_4_IX = 0xA6;
            public const byte RES_4_IX_8 = 0xA7;

            public const byte RES_5_IX_2 = 0xA8;
            public const byte RES_5_IX_3 = 0xA9;
            public const byte RES_5_IX_4 = 0xAA;
            public const byte RES_5_IX_5 = 0xAB;
            public const byte RES_5_IX_6 = 0xAC;
            public const byte RES_5_IX_7 = 0xAD;
            public const byte RES_5_IX = 0xAE;
            public const byte RES_5_IX_8 = 0xAF;

            public const byte RES_6_IX_2 = 0xB0;
            public const byte RES_6_IX_3 = 0xB1;
            public const byte RES_6_IX_4 = 0xB2;
            public const byte RES_6_IX_5 = 0xB3;
            public const byte RES_6_IX_6 = 0xB4;
            public const byte RES_6_IX_7 = 0xB5;
            public const byte RES_6_IX = 0xB6;
            public const byte RES_6_IX_8 = 0xB7;

            public const byte RES_7_IX_2 = 0xB8;
            public const byte RES_7_IX_3 = 0xB9;
            public const byte RES_7_IX_4 = 0xBA;
            public const byte RES_7_IX_5 = 0xBB;
            public const byte RES_7_IX_6 = 0xBC;
            public const byte RES_7_IX_7 = 0xBD;
            public const byte RES_7_IX = 0xBE;
            public const byte RES_7_IX_8 = 0xBF;

        #endregion

        #region Set Bit

            public const byte SET_0_IX_2 = 0xC0;
            public const byte SET_0_IX_3 = 0xC1;
            public const byte SET_0_IX_4 = 0xC2;
            public const byte SET_0_IX_5 = 0xC3;
            public const byte SET_0_IX_6 = 0xC4;
            public const byte SET_0_IX_7 = 0xC5;
            public const byte SET_0_IX = 0xC6;
            public const byte SET_0_IX_8 = 0xC7;

            public const byte SET_1_IX_2 = 0xC8;
            public const byte SET_1_IX_3 = 0xC9;
            public const byte SET_1_IX_4 = 0xCA;
            public const byte SET_1_IX_5 = 0xCB;
            public const byte SET_1_IX_6 = 0xCC;
            public const byte SET_1_IX_7 = 0xCD;
            public const byte SET_1_IX = 0xCE;
            public const byte SET_1_IX_8 = 0xCF;

            public const byte SET_2_IX_2 = 0xD0;
            public const byte SET_2_IX_3 = 0xD1;
            public const byte SET_2_IX_4 = 0xD2;
            public const byte SET_2_IX_5 = 0xD3;
            public const byte SET_2_IX_6 = 0xD4;
            public const byte SET_2_IX_7 = 0xD5;
            public const byte SET_2_IX = 0xD6;
            public const byte SET_2_IX_8 = 0xD7;

            public const byte SET_3_IX_2 = 0xD8;
            public const byte SET_3_IX_3 = 0xD9;
            public const byte SET_3_IX_4 = 0xDA;
            public const byte SET_3_IX_5 = 0xDB;
            public const byte SET_3_IX_6 = 0xDC;
            public const byte SET_3_IX_7 = 0xDD;
            public const byte SET_3_IX = 0xDE;
            public const byte SET_3_IX_8 = 0xDF;

            public const byte SET_4_IX_2 = 0xE0;
            public const byte SET_4_IX_3 = 0xE1;
            public const byte SET_4_IX_4 = 0xE2;
            public const byte SET_4_IX_5 = 0xE3;
            public const byte SET_4_IX_6 = 0xE4;
            public const byte SET_4_IX_7 = 0xE5;
            public const byte SET_4_IX = 0xE6;
            public const byte SET_4_IX_8 = 0xE7;

            public const byte SET_5_IX_2 = 0xE8;
            public const byte SET_5_IX_3 = 0xE9;
            public const byte SET_5_IX_4 = 0xEA;
            public const byte SET_5_IX_5 = 0xEB;
            public const byte SET_5_IX_6 = 0xEC;
            public const byte SET_5_IX_7 = 0xED;
            public const byte SET_5_IX = 0xEE;
            public const byte SET_5_IX_8 = 0xEF;

            public const byte SET_6_IX_2 = 0xF0;
            public const byte SET_6_IX_3 = 0xF1;
            public const byte SET_6_IX_4 = 0xF2;
            public const byte SET_6_IX_5 = 0xF3;
            public const byte SET_6_IX_6 = 0xF4;
            public const byte SET_6_IX_7 = 0xF5;
            public const byte SET_6_IX = 0xF6;
            public const byte SET_6_IX_8 = 0xF7;

            public const byte SET_7_IX_2 = 0xF8;
            public const byte SET_7_IX_3 = 0xF9;
            public const byte SET_7_IX_4 = 0xFA;
            public const byte SET_7_IX_5 = 0xFB;
            public const byte SET_7_IX_6 = 0xFC;
            public const byte SET_7_IX_7 = 0xFD;
            public const byte SET_7_IX = 0xFE;
            public const byte SET_7_IX_8 = 0xFF;

        #endregion
    }
}
