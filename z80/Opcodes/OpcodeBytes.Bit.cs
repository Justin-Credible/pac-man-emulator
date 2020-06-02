
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
                public const byte RLC_MHL = 0x06;
                public const byte RLC_A = 0x07;
            #endregion

            #region RRC r - Rotate right
                public const byte RRC_B = 0x08;
                public const byte RRC_C = 0x09;
                public const byte RRC_D = 0x0A;
                public const byte RRC_E = 0x0B;
                public const byte RRC_H = 0x0C;
                public const byte RRC_L = 0x0D;
                public const byte RRC_MHL = 0x0E;
                public const byte RRC_A = 0x0F;
            #endregion

            #region RL r - Rotate left through carry
                public const byte RL_B = 0x10;
                public const byte RL_C = 0x11;
                public const byte RL_D = 0x12;
                public const byte RL_E = 0x13;
                public const byte RL_H = 0x14;
                public const byte RL_L = 0x15;
                public const byte RL_MHL = 0x16;
                public const byte RL_A = 0x17;
            #endregion

            #region RR r - Rotate right through carry
                public const byte RR_B = 0x18;
                public const byte RR_C = 0x19;
                public const byte RR_D = 0x1A;
                public const byte RR_E = 0x1B;
                public const byte RR_H = 0x1C;
                public const byte RR_L = 0x1D;
                public const byte RR_MHL = 0x1E;
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
                public const byte SLA_MHL = 0x26;
                public const byte SLA_A = 0x27;
            #endregion

            #region SRA r - Shift right (arithmetic)
                public const byte SRA_B = 0x28;
                public const byte SRA_C = 0x29;
                public const byte SRA_D = 0x2A;
                public const byte SRA_E = 0x2B;
                public const byte SRA_H = 0x2C;
                public const byte SRA_L = 0x2D;
                public const byte SRA_MHL = 0x2E;
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
                public const byte SLL_MHL = 0x36;
                public const byte SLL_A = 0x37;
            #endregion

            #region SRL r - Shift right logical
                public const byte SRL_B = 0x38;
                public const byte SRL_C = 0x39;
                public const byte SRL_D = 0x3A;
                public const byte SRL_E = 0x3B;
                public const byte SRL_H = 0x3C;
                public const byte SRL_L = 0x3D;
                public const byte SRL_MHL = 0x3E;
                public const byte SRL_A = 0x3F;
            #endregion

        #endregion

        #region Test Bit

            public const byte BIT_0_B = 0x40;
            public const byte BIT_0_C = 0x41;
            public const byte BIT_0_D = 0x42;
            public const byte BIT_0_E = 0x43;
            public const byte BIT_0_H = 0x44;
            public const byte BIT_0_L = 0x45;
            public const byte BIT_0_MHL = 0x46;
            public const byte BIT_0_A = 0x47;

            public const byte BIT_1_B = 0x48;
            public const byte BIT_1_C = 0x49;
            public const byte BIT_1_D = 0x4A;
            public const byte BIT_1_E = 0x4B;
            public const byte BIT_1_H = 0x4C;
            public const byte BIT_1_L = 0x4D;
            public const byte BIT_1_MHL = 0x4E;
            public const byte BIT_1_A = 0x4F;

            public const byte BIT_2_B = 0x50;
            public const byte BIT_2_C = 0x51;
            public const byte BIT_2_D = 0x52;
            public const byte BIT_2_E = 0x53;
            public const byte BIT_2_H = 0x54;
            public const byte BIT_2_L = 0x55;
            public const byte BIT_2_MHL = 0x56;
            public const byte BIT_2_A = 0x57;

            public const byte BIT_3_B = 0x58;
            public const byte BIT_3_C = 0x59;
            public const byte BIT_3_D = 0x5A;
            public const byte BIT_3_E = 0x5B;
            public const byte BIT_3_H = 0x5C;
            public const byte BIT_3_L = 0x5D;
            public const byte BIT_3_MHL = 0x5E;
            public const byte BIT_3_A = 0x5F;

            public const byte BIT_4_B = 0x60;
            public const byte BIT_4_C = 0x61;
            public const byte BIT_4_D = 0x62;
            public const byte BIT_4_E = 0x63;
            public const byte BIT_4_H = 0x64;
            public const byte BIT_4_L = 0x65;
            public const byte BIT_4_MHL = 0x66;
            public const byte BIT_4_A = 0x67;

            public const byte BIT_5_B = 0x68;
            public const byte BIT_5_C = 0x69;
            public const byte BIT_5_D = 0x6A;
            public const byte BIT_5_E = 0x6B;
            public const byte BIT_5_H = 0x6C;
            public const byte BIT_5_L = 0x6D;
            public const byte BIT_5_MHL = 0x6E;
            public const byte BIT_5_A = 0x6F;

            public const byte BIT_6_B = 0x70;
            public const byte BIT_6_C = 0x71;
            public const byte BIT_6_D = 0x72;
            public const byte BIT_6_E = 0x73;
            public const byte BIT_6_H = 0x74;
            public const byte BIT_6_L = 0x75;
            public const byte BIT_6_MHL = 0x76;
            public const byte BIT_6_A = 0x77;

            public const byte BIT_7_B = 0x78;
            public const byte BIT_7_C = 0x79;
            public const byte BIT_7_D = 0x7A;
            public const byte BIT_7_E = 0x7B;
            public const byte BIT_7_H = 0x7C;
            public const byte BIT_7_L = 0x7D;
            public const byte BIT_7_MHL = 0x7E;
            public const byte BIT_7_A = 0x7F;

        #endregion

        #region Reset Bit

            public const byte RES_0_B = 0x80;
            public const byte RES_0_C = 0x81;
            public const byte RES_0_D = 0x82;
            public const byte RES_0_E = 0x83;
            public const byte RES_0_H = 0x84;
            public const byte RES_0_L = 0x85;
            public const byte RES_0_MHL = 0x86;
            public const byte RES_0_A = 0x87;

            public const byte RES_1_B = 0x88;
            public const byte RES_1_C = 0x89;
            public const byte RES_1_D = 0x8A;
            public const byte RES_1_E = 0x8B;
            public const byte RES_1_H = 0x8C;
            public const byte RES_1_L = 0x8D;
            public const byte RES_1_MHL = 0x8E;
            public const byte RES_1_A = 0x8F;

            public const byte RES_2_B = 0x90;
            public const byte RES_2_C = 0x91;
            public const byte RES_2_D = 0x92;
            public const byte RES_2_E = 0x93;
            public const byte RES_2_H = 0x94;
            public const byte RES_2_L = 0x95;
            public const byte RES_2_MHL = 0x96;
            public const byte RES_2_A = 0x97;

            public const byte RES_3_B = 0x98;
            public const byte RES_3_C = 0x99;
            public const byte RES_3_D = 0x9A;
            public const byte RES_3_E = 0x9B;
            public const byte RES_3_H = 0x9C;
            public const byte RES_3_L = 0x9D;
            public const byte RES_3_MHL = 0x9E;
            public const byte RES_3_A = 0x9F;

            public const byte RES_4_B = 0xA0;
            public const byte RES_4_C = 0xA1;
            public const byte RES_4_D = 0xA2;
            public const byte RES_4_E = 0xA3;
            public const byte RES_4_H = 0xA4;
            public const byte RES_4_L = 0xA5;
            public const byte RES_4_MHL = 0xA6;
            public const byte RES_4_A = 0xA7;

            public const byte RES_5_B = 0xA8;
            public const byte RES_5_C = 0xA9;
            public const byte RES_5_D = 0xAA;
            public const byte RES_5_E = 0xAB;
            public const byte RES_5_H = 0xAC;
            public const byte RES_5_L = 0xAD;
            public const byte RES_5_MHL = 0xAE;
            public const byte RES_5_A = 0xAF;

            public const byte RES_6_B = 0xB0;
            public const byte RES_6_C = 0xB1;
            public const byte RES_6_D = 0xB2;
            public const byte RES_6_E = 0xB3;
            public const byte RES_6_H = 0xB4;
            public const byte RES_6_L = 0xB5;
            public const byte RES_6_MHL = 0xB6;
            public const byte RES_6_A = 0xB7;

            public const byte RES_7_B = 0xB8;
            public const byte RES_7_C = 0xB9;
            public const byte RES_7_D = 0xBA;
            public const byte RES_7_E = 0xBB;
            public const byte RES_7_H = 0xBC;
            public const byte RES_7_L = 0xBD;
            public const byte RES_7_MHL = 0xBE;
            public const byte RES_7_A = 0xBF;

        #endregion

        #region Set Bit

            public const byte SET_0_B = 0xC0;
            public const byte SET_0_C = 0xC1;
            public const byte SET_0_D = 0xC2;
            public const byte SET_0_E = 0xC3;
            public const byte SET_0_H = 0xC4;
            public const byte SET_0_L = 0xC5;
            public const byte SET_0_MHL = 0xC6;
            public const byte SET_0_A = 0xC7;

            public const byte SET_1_B = 0xC8;
            public const byte SET_1_C = 0xC9;
            public const byte SET_1_D = 0xCA;
            public const byte SET_1_E = 0xCB;
            public const byte SET_1_H = 0xCC;
            public const byte SET_1_L = 0xCD;
            public const byte SET_1_MHL = 0xCE;
            public const byte SET_1_A = 0xCF;

            public const byte SET_2_B = 0xD0;
            public const byte SET_2_C = 0xD1;
            public const byte SET_2_D = 0xD2;
            public const byte SET_2_E = 0xD3;
            public const byte SET_2_H = 0xD4;
            public const byte SET_2_L = 0xD5;
            public const byte SET_2_MHL = 0xD6;
            public const byte SET_2_A = 0xD7;

            public const byte SET_3_B = 0xD8;
            public const byte SET_3_C = 0xD9;
            public const byte SET_3_D = 0xDA;
            public const byte SET_3_E = 0xDB;
            public const byte SET_3_H = 0xDC;
            public const byte SET_3_L = 0xDD;
            public const byte SET_3_MHL = 0xDE;
            public const byte SET_3_A = 0xDF;

            public const byte SET_4_B = 0xE0;
            public const byte SET_4_C = 0xE1;
            public const byte SET_4_D = 0xE2;
            public const byte SET_4_E = 0xE3;
            public const byte SET_4_H = 0xE4;
            public const byte SET_4_L = 0xE5;
            public const byte SET_4_MHL = 0xE6;
            public const byte SET_4_A = 0xE7;

            public const byte SET_5_B = 0xE8;
            public const byte SET_5_C = 0xE9;
            public const byte SET_5_D = 0xEA;
            public const byte SET_5_E = 0xEB;
            public const byte SET_5_H = 0xEC;
            public const byte SET_5_L = 0xED;
            public const byte SET_5_MHL = 0xEE;
            public const byte SET_5_A = 0xEF;

            public const byte SET_6_B = 0xF0;
            public const byte SET_6_C = 0xF1;
            public const byte SET_6_D = 0xF2;
            public const byte SET_6_E = 0xF3;
            public const byte SET_6_H = 0xF4;
            public const byte SET_6_L = 0xF5;
            public const byte SET_6_MHL = 0xF6;
            public const byte SET_6_A = 0xF7;

            public const byte SET_7_B = 0xF8;
            public const byte SET_7_C = 0xF9;
            public const byte SET_7_D = 0xFA;
            public const byte SET_7_E = 0xFB;
            public const byte SET_7_H = 0xFC;
            public const byte SET_7_L = 0xFD;
            public const byte SET_7_MHL = 0xFE;
            public const byte SET_7_A = 0xFF;

        #endregion
    }
}
