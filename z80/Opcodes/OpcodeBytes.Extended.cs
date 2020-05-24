
namespace JustinCredible.ZilogZ80
{
    // A list of all the "bit" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_BIT (0xCB).
    public partial class OpcodeBytes
    {
        #region Input/Output Instructions

            // Device[C] <- R
            public const byte OUT_MC_A = 0x79;
            public const byte OUT_MC_B = 0x41;
            public const byte OUT_MC_C = 0x49;
            public const byte OUT_MC_D = 0x51;
            public const byte OUT_MC_E = 0x59;
            public const byte OUT_MC_H = 0x61;
            public const byte OUT_MC_L = 0x69;
            public const byte OUT_MC_0 = 0x71;

            /* Device[C] <- (HL); HL++; B--; */
            public const byte OUTI = 0xA3;

            /* Device[C] <- (HL); HL--; B--; */
            public const byte OUTD = 0xAB;

            // R <- Device[C]
            public const byte IN_A_MC = 0x78;
            public const byte IN_B_MC = 0x40;
            public const byte IN_C_MC = 0x48;
            public const byte IN_D_MC = 0x50;
            public const byte IN_E_MC = 0x58;
            public const byte IN_H_MC = 0x60;
            public const byte IN_L_MC = 0x68;
            public const byte IN_MC = 0x70;

            /* (HL) <- Device[C]; HL++; B--; */
            public const byte INI = 0xA2;

            /* (HL) <- Device[C]; HL++; B--; if B != 0, repeat(); */
            public const byte INIR = 0xB2;

            /* (HL) <- Device[C]; HL--; B--; */
            public const byte IND = 0xAA;

        #endregion

        #region Set Interrupt Mode

            /** Set Interrupt Mode 0 */
            public const byte IM0 = 0x46;
            public const byte IM0_2 = 0x66;

            // Undocumented IM0 equivalents, see:
            // https://stackoverflow.com/questions/39436351/what-does-im0-1-mean-in-z80-info-decoding#comment66229644_39436440
            public const byte IM0_3 = 0x4E;
            public const byte IM0_4 = 0x6E;

            /** Set Interrupt Mode 1 */
            public const byte IM1 = 0x56;
            public const byte IM1_2 = 0x76;

            /** Set Interrupt Mode 2 */
            public const byte IM2 = 0x5E;
            public const byte IM2_2 = 0x7E;

        #endregion

        #region Return from Non-maskable Interrupt

            public const byte RETN = 0x45;
            public const byte RETN_2 = 0x55;
            public const byte RETN_3 = 0x65;
            public const byte RETN_4 = 0x75;
            public const byte RETN_5 = 0x5D;
            public const byte RETN_6 = 0x6D;
            public const byte RETN_7 = 0x7D;

        #endregion

        #region Return from Interrupt

            public const byte RETI = 0x4D;

        #endregion

        #region Rotate and Shift

            public const byte RRD = 0x67;
            public const byte RLD = 0x6F;

        #endregion

        #region ADC HL, rr - Add register or memory to accumulator with carry

            public const byte ADC_HL_BC = 0x4A;
            public const byte ADC_HL_DE = 0x5A;
            public const byte ADC_HL_HL = 0x6A;
            public const byte ADC_HL_SP = 0x7A;

        #endregion

        #region SBC HL, rr - Subtract register or memory from accumulator with borrow

            public const byte SBC_HL_BC = 0x42;
            public const byte SBC_HL_DE = 0x52;
            public const byte SBC_HL_HL = 0x62;
            public const byte SBC_HL_SP = 0x72;

        #endregion
    }
}
