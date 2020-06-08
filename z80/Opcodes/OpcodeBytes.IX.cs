
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IX" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IX (0xDD).
    public partial class OpcodeBytes
    {
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
    }
}
