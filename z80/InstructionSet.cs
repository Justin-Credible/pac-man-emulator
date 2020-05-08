
namespace JustinCredible.ZilogZ80
{
    /**
     * The Z80 has several different "sets" of instructions. The main instruction
     * set is single byte opcodes, while the other are multi-byte opcodes.
     */
    public enum InstructionSet
    {
        // Single Byte
        Standard,

        // Two Bytes
        Extended,
        Bit,
        IX,
        IY,

        // Three Bytes
        IXBit,
        IYBit,
    }
}
