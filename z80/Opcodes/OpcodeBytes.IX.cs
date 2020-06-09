
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IX" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IX (0xDD).
    public partial class OpcodeBytes
    {
        public const byte JP_IX = 0xE9;

        #region Add

            #region Add (Addresses)

                public const byte ADD_IX_BC = 0x09;
                public const byte ADD_IX_DE = 0x19;
                public const byte ADD_IX_IX = 0x29;
                public const byte ADD_IX_SP = 0x39;

            #endregion

            #region Add (Arithmetic)

                public const byte ADD_A_IX = 0x86;

                public const byte ADD_A_IXH = 0x84;
                public const byte ADD_A_IXL = 0x85;

                public const byte ADC_A_IX = 0x8E;

                public const byte ADC_A_IXH = 0x8C;
                public const byte ADC_A_IXL = 0x8D;

            #endregion

        #endregion

        #region Subtract

            public const byte SUB_IX = 0x96;

            public const byte SUB_IXH = 0x94;
            public const byte SUB_IXL = 0x95;

            public const byte SBC_A_IX = 0x9E;

            public const byte SBC_A_IXH = 0x9C;
            public const byte SBC_A_IXL = 0x9D;

        #endregion

        #region Compare

            public const byte CP_IX = 0xBE;

            public const byte CP_IXH = 0xBC;
            public const byte CP_IXL = 0xBD;

        #endregion

        #region Bitwise Operations

            #region Bitwise AND

                public const byte AND_IX = 0xA6;

                public const byte AND_IXH = 0xA4;
                public const byte AND_IXL = 0xA5;

            #endregion

            #region Bitwise OR

                public const byte OR_IX = 0xB6;

                public const byte OR_IXH = 0xB4;
                public const byte OR_IXL = 0xB5;

            #endregion

            #region Bitwise XOR

                public const byte XOR_IX = 0xAE;

                public const byte XOR_IXH = 0xAC;
                public const byte XOR_IXL = 0xAD;

            #endregion

        #endregion

        #region Load

            public const byte LD_SP_IX = 0xF9;

            public const byte LD_IX_NN = 0x21;

            public const byte LD_MNN_IX = 0x22;
            public const byte LD_IX_MNN = 0x2A;

            public const byte LD_MIX_N = 0x36;

            public const byte LD_MIX_B = 0x70;
            public const byte LD_MIX_C = 0x71;
            public const byte LD_MIX_D = 0x72;
            public const byte LD_MIX_E = 0x73;
            public const byte LD_MIX_H = 0x74;
            public const byte LD_MIX_L = 0x75;
            public const byte LD_MIX_A = 0x77;

            public const byte LD_B_MIX = 0x46;
            public const byte LD_C_MIX = 0x4E;
            public const byte LD_D_MIX = 0x56;
            public const byte LD_E_MIX = 0x5E;
            public const byte LD_H_MIX = 0x66;
            public const byte LD_L_MIX = 0x6E;
            public const byte LD_A_MIX = 0x7E;

            public const byte LD_A_IXH = 0x7C;
            public const byte LD_B_IXH = 0x44;
            public const byte LD_C_IXH = 0x4C;
            public const byte LD_D_IXH = 0x54;
            public const byte LD_E_IXH = 0x5C;

            public const byte LD_A_IXL = 0x7D;
            public const byte LD_B_IXL = 0x45;
            public const byte LD_C_IXL = 0x4D;
            public const byte LD_D_IXL = 0x55;
            public const byte LD_E_IXL = 0x5D;

            public const byte LD_IXH_A = 0x67;
            public const byte LD_IXH_B = 0x60;
            public const byte LD_IXH_C = 0x61;
            public const byte LD_IXH_D = 0x62;
            public const byte LD_IXH_E = 0x63;

            public const byte LD_IXL_A = 0x6F;
            public const byte LD_IXL_B = 0x68;
            public const byte LD_IXL_C = 0x69;
            public const byte LD_IXL_D = 0x6A;
            public const byte LD_IXL_E = 0x6B;

            public const byte LD_IXH_IXH = 0x64;
            public const byte LD_IXH_IXL = 0x65;
            public const byte LD_IXL_IXH = 0x6C;
            public const byte LD_IXL_IXL = 0x6D;

            public const byte LD_IXH_N = 0x26;
            public const byte LD_IXL_N = 0x2E;

        #endregion
    }
}
