
namespace JustinCredible.ZilogZ80
{
    // A list of all the "preamble" bytes for the two and three byte opcodes.
    public partial class OpcodeBytes
    {
        /** Extended Instructions, preamble byte */
        public const byte PREAMBLE_EXTENDED = 0xED;

        /** Bit Instructions, preamble byte */
        public const byte PREAMBLE_BIT = 0xCB;

        /** IX Register Instructions, preamble byte */
        public const byte PREAMBLE_IX = 0xDD;

        /** IX Register Bit Instructions, preamble byte */
        public const byte PREAMBLE_IX_BIT = 0xCB;

        /** IY Register Instructions, preamble byte */
        public const byte PREAMBLE_IY = 0xFD;

        /** IY Register Bit Instructions, preamble byte */
        public const byte PREAMBLE_IY_BIT = 0xCB;
    }
}
