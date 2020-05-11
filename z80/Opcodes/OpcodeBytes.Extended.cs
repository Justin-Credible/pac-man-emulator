
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

        #endregion
    }
}
