
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IY bit" opcodes and their metadata.
    // These are all three byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IY (0xFD)
    // and the second byte is defined by OpcodeBytes.PREAMBLE_IY_BIT (0xCB).
    public partial class Opcodes
    {
        #region RLC (IY+n), r - Rotate left
            public static Opcode RLC_IY_B = new Opcode(OpcodeBytes.RLC_IY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), B", cycles: 23, pseudocode: "B = (IY+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IY_C = new Opcode(OpcodeBytes.RLC_IY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), C", cycles: 23, pseudocode: "C = (IY+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IY_D = new Opcode(OpcodeBytes.RLC_IY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), D", cycles: 23, pseudocode: "D = (IY+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IY_E = new Opcode(OpcodeBytes.RLC_IY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), E", cycles: 23, pseudocode: "E = (IY+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IY_H = new Opcode(OpcodeBytes.RLC_IY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), H", cycles: 23, pseudocode: "H = (IY+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IY_L = new Opcode(OpcodeBytes.RLC_IY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), L", cycles: 23, pseudocode: "L = (IY+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IY_HL = new Opcode(OpcodeBytes.RLC_IY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n)", cycles: 23, pseudocode: "(IY+n) = (IY+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IY_A = new Opcode(OpcodeBytes.RLC_IY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), A", cycles: 23, pseudocode: "A = (IY+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
        #endregion

        #region RRC (IY+n), r - Rotate right
            public static Opcode RRC_IY_B = new Opcode(OpcodeBytes.RRC_IY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), B", cycles: 23, pseudocode: "B = (IY+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IY_C = new Opcode(OpcodeBytes.RRC_IY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), C", cycles: 23, pseudocode: "C = (IY+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IY_D = new Opcode(OpcodeBytes.RRC_IY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), D", cycles: 23, pseudocode: "D = (IY+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IY_E = new Opcode(OpcodeBytes.RRC_IY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), E", cycles: 23, pseudocode: "E = (IY+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IY_H = new Opcode(OpcodeBytes.RRC_IY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), H", cycles: 23, pseudocode: "H = (IY+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IY_L = new Opcode(OpcodeBytes.RRC_IY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), L", cycles: 23, pseudocode: "L = (IY+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IY_HL = new Opcode(OpcodeBytes.RRC_IY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n)", cycles: 23, pseudocode: "(IY+n) = (IY+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IY_A = new Opcode(OpcodeBytes.RRC_IY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), A", cycles: 23, pseudocode: "A = (IY+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
        #endregion

        #region RL (IY+n), r - Rotate left through carry
            public static Opcode RL_IY_B = new Opcode(OpcodeBytes.RL_IY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), B", cycles: 23, pseudocode: "B = (IY+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IY_C = new Opcode(OpcodeBytes.RL_IY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), C", cycles: 23, pseudocode: "C = (IY+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IY_D = new Opcode(OpcodeBytes.RL_IY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), D", cycles: 23, pseudocode: "D = (IY+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IY_E = new Opcode(OpcodeBytes.RL_IY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), E", cycles: 23, pseudocode: "E = (IY+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IY_H = new Opcode(OpcodeBytes.RL_IY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), H", cycles: 23, pseudocode: "H = (IY+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IY_L = new Opcode(OpcodeBytes.RL_IY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), L", cycles: 23, pseudocode: "L = (IY+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IY_HL = new Opcode(OpcodeBytes.RL_IY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n)", cycles: 23, pseudocode: "(IY+n) = (IY+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IY_A = new Opcode(OpcodeBytes.RL_IY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), A", cycles: 23, pseudocode: "A = (IY+n) << 1; bit 0 = prev CY; CY = prev bit 7");
        #endregion

        #region RR (IY+n), r - Rotate right through carry
            public static Opcode RR_IY_B = new Opcode(OpcodeBytes.RR_IY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), B", cycles: 23, pseudocode: "B = (IY+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IY_C = new Opcode(OpcodeBytes.RR_IY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), C", cycles: 23, pseudocode: "C = (IY+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IY_D = new Opcode(OpcodeBytes.RR_IY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), D", cycles: 23, pseudocode: "D = (IY+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IY_E = new Opcode(OpcodeBytes.RR_IY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), E", cycles: 23, pseudocode: "E = (IY+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IY_H = new Opcode(OpcodeBytes.RR_IY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), H", cycles: 23, pseudocode: "H = (IY+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IY_L = new Opcode(OpcodeBytes.RR_IY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), L", cycles: 23, pseudocode: "L = (IY+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IY_HL = new Opcode(OpcodeBytes.RR_IY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n)", cycles: 23, pseudocode: "(IY+n) = (IY+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IY_A = new Opcode(OpcodeBytes.RR_IY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), A", cycles: 23, pseudocode: "A = (IY+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
        #endregion
    }
}
