
namespace JustinCredible.ZilogZ80
{
    /**
     * The 16-bit registers as well as the pairs of 8-bit registers.
     */
    public enum RegisterPair
    {
        HL,
        BC,
        DE,

        /** Index/Base Register */
        IX,

        /** Index/Base Register */
        IY,

        /** Program Counter */
        PC,

        /** Stack Pointer */
        SP,
    }
}
