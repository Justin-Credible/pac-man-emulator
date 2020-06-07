
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IX" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IX (0xDD).
    public partial class OpcodeBytes
    {
        #region Add

            #region ADD IX, rr
                public const byte ADD_IX_BC = 0x09;
                public const byte ADD_IX_DE = 0x19;
                public const byte ADD_IX_IX = 0x29;
                public const byte ADD_IX_SP = 0x39;
            #endregion

            #region ADD A, (IX+n)
                public const byte ADD_A_IX = 0x86;
            #endregion

            #region ADD A, IXH/IXL
                public const byte ADD_A_IXH = 0x84;
                public const byte ADD_A_IXL = 0x85;
            #endregion

        #endregion

        #region Subtract

            #region SUB (IX+n)
                public const byte SUB_IX = 0x96;
            #endregion

            #region SUB IXH/IXL
                public const byte SUB_IXH = 0x94;
                public const byte SUB_IXL = 0x95;
            #endregion

        #endregion

        #region Bitwise Operations

            #region Bitwise AND

                public const byte AND_IXH = 0xA4;
                public const byte AND_IXL = 0xA5;
                public const byte AND_IX = 0xA6;

            #endregion

            #region Bitwise OR

                public const byte OR_IXH = 0xB4;
                public const byte OR_IXL = 0xB5;
                public const byte OR_IX = 0xB6;

            #endregion

            #region Bitwise XOR

                public const byte XOR_IXH = 0xAC;
                public const byte XOR_IXL = 0xAD;
                public const byte XOR_IX = 0xAE;

            #endregion

        #endregion
    }
}
