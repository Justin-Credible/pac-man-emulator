
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IY" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IY (0xFD).
    public partial class OpcodeBytes
    {
        public const byte JP_IY = 0xE9;

        #region Increment/Decrement

            public const byte INC_IY = 0x23;
            public const byte DEC_IY = 0x2B;

            public const byte INC_IYH = 0x24;
            public const byte DEC_IYH = 0x25;

            public const byte INC_IYL = 0x2C;
            public const byte DEC_IYL = 0x2D;

            public const byte INC_MIY = 0x34;
            public const byte DEC_MIY = 0x35;

        #endregion

        #region Stack Operations

            public const byte POP_IY = 0xE1;
            public const byte PUSH_IY = 0xE5;
            public const byte EX_MSP_IY = 0xE3;

        #endregion

        #region Add

            #region Add (Addresses)

                public const byte ADD_IY_BC = 0x09;
                public const byte ADD_IY_DE = 0x19;
                public const byte ADD_IY_IY = 0x29;
                public const byte ADD_IY_SP = 0x39;

            #endregion

            #region Add (Arithmetic)

                public const byte ADD_A_MIY = 0x86;

                public const byte ADD_A_IYH = 0x84;
                public const byte ADD_A_IYL = 0x85;

                public const byte ADC_A_MIY = 0x8E;

                public const byte ADC_A_IYH = 0x8C;
                public const byte ADC_A_IYL = 0x8D;

            #endregion

        #endregion

        #region Subtract

            public const byte SUB_MIY = 0x96;

            public const byte SUB_IYH = 0x94;
            public const byte SUB_IYL = 0x95;

            public const byte SBC_A_MIY = 0x9E;

            public const byte SBC_A_IYH = 0x9C;
            public const byte SBC_A_IYL = 0x9D;

        #endregion

        #region Compare

            public const byte CP_IY = 0xBE;

            public const byte CP_IYH = 0xBC;
            public const byte CP_IYL = 0xBD;

        #endregion

        #region Bitwise Operations

            #region Bitwise AND

                public const byte AND_MIY = 0xA6;

                public const byte AND_IYH = 0xA4;
                public const byte AND_IYL = 0xA5;

            #endregion

            #region Bitwise OR

                public const byte OR_MIY = 0xB6;

                public const byte OR_IYH = 0xB4;
                public const byte OR_IYL = 0xB5;

            #endregion

            #region Bitwise XOR

                public const byte XOR_MIY = 0xAE;

                public const byte XOR_IYH = 0xAC;
                public const byte XOR_IYL = 0xAD;

            #endregion

        #endregion

        #region Load

            public const byte LD_SP_IY = 0xF9;

            public const byte LD_IY_NN = 0x21;

            public const byte LD_MNN_IY = 0x22;
            public const byte LD_IY_MNN = 0x2A;

            public const byte LD_MIY_N = 0x36;

            public const byte LD_MIY_B = 0x70;
            public const byte LD_MIY_C = 0x71;
            public const byte LD_MIY_D = 0x72;
            public const byte LD_MIY_E = 0x73;
            public const byte LD_MIY_H = 0x74;
            public const byte LD_MIY_L = 0x75;
            public const byte LD_MIY_A = 0x77;

            public const byte LD_B_MIY = 0x46;
            public const byte LD_C_MIY = 0x4E;
            public const byte LD_D_MIY = 0x56;
            public const byte LD_E_MIY = 0x5E;
            public const byte LD_H_MIY = 0x66;
            public const byte LD_L_MIY = 0x6E;
            public const byte LD_A_MIY = 0x7E;

            public const byte LD_A_IYH = 0x7C;
            public const byte LD_B_IYH = 0x44;
            public const byte LD_C_IYH = 0x4C;
            public const byte LD_D_IYH = 0x54;
            public const byte LD_E_IYH = 0x5C;

            public const byte LD_A_IYL = 0x7D;
            public const byte LD_B_IYL = 0x45;
            public const byte LD_C_IYL = 0x4D;
            public const byte LD_D_IYL = 0x55;
            public const byte LD_E_IYL = 0x5D;

            public const byte LD_IYH_A = 0x67;
            public const byte LD_IYH_B = 0x60;
            public const byte LD_IYH_C = 0x61;
            public const byte LD_IYH_D = 0x62;
            public const byte LD_IYH_E = 0x63;

            public const byte LD_IYL_A = 0x6F;
            public const byte LD_IYL_B = 0x68;
            public const byte LD_IYL_C = 0x69;
            public const byte LD_IYL_D = 0x6A;
            public const byte LD_IYL_E = 0x6B;

            public const byte LD_IYH_IYH = 0x64;
            public const byte LD_IYH_IYL = 0x65;
            public const byte LD_IYL_IYH = 0x6C;
            public const byte LD_IYL_IYL = 0x6D;

            public const byte LD_IYH_N = 0x26;
            public const byte LD_IYL_N = 0x2E;

        #endregion
    }
}
