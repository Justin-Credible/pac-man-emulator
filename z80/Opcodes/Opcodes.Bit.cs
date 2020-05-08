
namespace JustinCredible.ZilogZ80
{
    // A list of all the "bit" opcodes and their metadata.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_BIT (0xCB).
    public partial class Opcodes
    {
        #region Extended - Bit Instructions

            #region RLC - Rotate Left
                public static Opcode XT_BIT_RLC_B = new Opcode(OpcodeBytes.BIT_RLC_B, size: 2, instruction: "RLC B", cycles: 8);
                public static Opcode XT_BIT_RLC_C = new Opcode(OpcodeBytes.BIT_RLC_C, size: 2, instruction: "RLC C", cycles: 8);
                public static Opcode XT_BIT_RLC_D = new Opcode(OpcodeBytes.BIT_RLC_D, size: 2, instruction: "RLC D", cycles: 8);
                public static Opcode XT_BIT_RLC_E = new Opcode(OpcodeBytes.BIT_RLC_E, size: 2, instruction: "RLC E", cycles: 8);
                public static Opcode XT_BIT_RLC_H = new Opcode(OpcodeBytes.BIT_RLC_H, size: 2, instruction: "RLC H", cycles: 8);
                public static Opcode XT_BIT_RLC_L = new Opcode(OpcodeBytes.BIT_RLC_L, size: 2, instruction: "RLC L", cycles: 8);
                public static Opcode XT_BIT_RLC_HL = new Opcode(OpcodeBytes.BIT_RLC_HL, size: 2, instruction: "RLC (HL)", cycles: 15);
                public static Opcode XT_BIT_RLC_A = new Opcode(OpcodeBytes.BIT_RLC_A, size: 2, instruction: "RLC A", cycles: 8);
            #endregion

        #endregion
    }
}
