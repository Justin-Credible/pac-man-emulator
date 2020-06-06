
namespace JustinCredible.ZilogZ80
{
    /**
     * The 8-bit registers.
     */
    public enum Register
    {
        A,
        B,
        C,
        D,
        E,
        H,
        L,

        /** Interrupt Vector */
        I,

        /** DRAM refresh counter */
        R,

        /** IX "half register", high byte */
        IXH,

        /** IX "half register", low byte */
        IXL,

        /** IY "half register", high byte */
        IYH,

        /** IY "half register", low byte */
        IYL,
    }
}
