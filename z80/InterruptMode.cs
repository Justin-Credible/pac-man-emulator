
namespace JustinCredible.ZilogZ80
{
    /**
     * The Zilog Z80 CPU has three interrupt modes.
     */
    public enum InterruptMode
    {
        /**
         * Maskable Interrupt: Mode 0
         * 
         * Gets instruction from the data bus and executes it. This is normally RST 0 - RST 7, but
         * could be any instruction.
         */
        Zero,

        /**
         * Maskable Interrupt: Mode 1
         * 
         * Jumps to address 0x0038 and runs the routine there.
         */
        One,

        /**
         * Maskable Interrupt: Mode 2 (Vectored Inputs)
         * 
         * Gets a byte from the data bus and jumps to the 16-bit address formed by combining the
         * interrupt vector (register I) as the MSB and the supplied byte as the LSB.
         */
        Two,
    }
}
