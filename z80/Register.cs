
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
    }
}
