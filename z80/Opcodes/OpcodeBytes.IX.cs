
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
                public const byte ADD_A_IXH = 0x84;
                public const byte ADD_A_IXL = 0x85;
                public const byte ADD_A_IX = 0x86;
            #endregion

        #endregion
    }
}
