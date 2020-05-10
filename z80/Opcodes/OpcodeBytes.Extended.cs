
namespace JustinCredible.ZilogZ80
{
    // A list of all the "bit" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_BIT (0xCB).
    public partial class OpcodeBytes
    {
        #region Input/Output Instructions

            /** Write value from given register to device mapped to I/O port identified by register C */
            public const byte OUT_MC_A = 0x79;
            public const byte OUT_MC_B = 0x41;
            public const byte OUT_MC_C = 0x49;
            public const byte OUT_MC_D = 0x51;
            public const byte OUT_MC_E = 0x59;
            public const byte OUT_MC_H = 0x61;
            public const byte OUT_MC_L = 0x69;
            public const byte OUT_MC_0 = 0x71;

        #endregion
    }
}
