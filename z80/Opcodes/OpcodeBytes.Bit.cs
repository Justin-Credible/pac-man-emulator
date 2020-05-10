
namespace JustinCredible.ZilogZ80
{
    // A list of all the "bit" instruction opcode bytes; can be used to lookup the opcode definition.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_BIT (0xCB).
    public partial class OpcodeBytes
    {
        #region Extended - Bit Instructions

            #region RLC - Rotate Left
                public const byte BIT_RLC_B = 0x00;
                public const byte BIT_RLC_C = 0x01;
                public const byte BIT_RLC_D = 0x02;
                public const byte BIT_RLC_E = 0x03;
                public const byte BIT_RLC_H = 0x04;
                public const byte BIT_RLC_L = 0x05;
                public const byte BIT_RLC_HL = 0x06;
                public const byte BIT_RLC_A = 0x07;
            #endregion

        #endregion
    }
}
