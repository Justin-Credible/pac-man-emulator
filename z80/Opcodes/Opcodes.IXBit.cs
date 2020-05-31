
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IX bit" opcodes and their metadata.
    // These are all three byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IX (0xDD)
    // and the second byte is defined by OpcodeBytes.PREAMBLE_IX_BIT (0xCB).
    public partial class Opcodes
    {
        #region RLC (IX+n), r - Rotate left
            public static Opcode RLC_IX_B = new Opcode(OpcodeBytes.RLC_IX_B, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RLC (IX+n), B", cycles: 23, pseudocode: "B = (IX+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IX_C = new Opcode(OpcodeBytes.RLC_IX_C, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RLC (IX+n), C", cycles: 23, pseudocode: "C = (IX+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IX_D = new Opcode(OpcodeBytes.RLC_IX_D, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RLC (IX+n), D", cycles: 23, pseudocode: "D = (IX+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IX_E = new Opcode(OpcodeBytes.RLC_IX_E, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RLC (IX+n), E", cycles: 23, pseudocode: "E = (IX+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IX_H = new Opcode(OpcodeBytes.RLC_IX_H, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RLC (IX+n), H", cycles: 23, pseudocode: "H = (IX+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IX_L = new Opcode(OpcodeBytes.RLC_IX_L, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RLC (IX+n), L", cycles: 23, pseudocode: "L = (IX+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IX_HL = new Opcode(OpcodeBytes.RLC_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RLC (IX+n)", cycles: 23, pseudocode: "(IX+n) = (IX+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
            public static Opcode RLC_IX_A = new Opcode(OpcodeBytes.RLC_IX_A, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RLC (IX+n), A", cycles: 23, pseudocode: "A = (IX+n) << 1; bit 0 = prev bit 7; CY = prev bit 7");
        #endregion

        #region RRC (IX+n), r - Rotate right
            public static Opcode RRC_IX_B = new Opcode(OpcodeBytes.RRC_IX_B, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RRC (IX+n), B", cycles: 23, pseudocode: "B = (IX+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IX_C = new Opcode(OpcodeBytes.RRC_IX_C, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RRC (IX+n), C", cycles: 23, pseudocode: "C = (IX+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IX_D = new Opcode(OpcodeBytes.RRC_IX_D, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RRC (IX+n), D", cycles: 23, pseudocode: "D = (IX+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IX_E = new Opcode(OpcodeBytes.RRC_IX_E, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RRC (IX+n), E", cycles: 23, pseudocode: "E = (IX+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IX_H = new Opcode(OpcodeBytes.RRC_IX_H, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RRC (IX+n), H", cycles: 23, pseudocode: "H = (IX+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IX_L = new Opcode(OpcodeBytes.RRC_IX_L, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RRC (IX+n), L", cycles: 23, pseudocode: "L = (IX+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IX_HL = new Opcode(OpcodeBytes.RRC_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RRC (IX+n)", cycles: 23, pseudocode: "(IX+n) = (IX+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            public static Opcode RRC_IX_A = new Opcode(OpcodeBytes.RRC_IX_A, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RRC (IX+n), A", cycles: 23, pseudocode: "A = (IX+n) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
        #endregion

        #region RL (IX+n), r - Rotate left through carry
            public static Opcode RL_IX_B = new Opcode(OpcodeBytes.RL_IX_B, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RL (IX+n), B", cycles: 23, pseudocode: "B = (IX+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IX_C = new Opcode(OpcodeBytes.RL_IX_C, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RL (IX+n), C", cycles: 23, pseudocode: "C = (IX+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IX_D = new Opcode(OpcodeBytes.RL_IX_D, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RL (IX+n), D", cycles: 23, pseudocode: "D = (IX+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IX_E = new Opcode(OpcodeBytes.RL_IX_E, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RL (IX+n), E", cycles: 23, pseudocode: "E = (IX+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IX_H = new Opcode(OpcodeBytes.RL_IX_H, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RL (IX+n), H", cycles: 23, pseudocode: "H = (IX+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IX_L = new Opcode(OpcodeBytes.RL_IX_L, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RL (IX+n), L", cycles: 23, pseudocode: "L = (IX+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IX_HL = new Opcode(OpcodeBytes.RL_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RL (IX+n)", cycles: 23, pseudocode: "(IX+n) = (IX+n) << 1; bit 0 = prev CY; CY = prev bit 7");
            public static Opcode RL_IX_A = new Opcode(OpcodeBytes.RL_IX_A, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RL (IX+n), A", cycles: 23, pseudocode: "A = (IX+n) << 1; bit 0 = prev CY; CY = prev bit 7");
        #endregion

        #region RR (IX+n), r - Rotate right through carry
            public static Opcode RR_IX_B = new Opcode(OpcodeBytes.RR_IX_B, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RR (IX+n), B", cycles: 23, pseudocode: "B = (IX+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IX_C = new Opcode(OpcodeBytes.RR_IX_C, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RR (IX+n), C", cycles: 23, pseudocode: "C = (IX+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IX_D = new Opcode(OpcodeBytes.RR_IX_D, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RR (IX+n), D", cycles: 23, pseudocode: "D = (IX+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IX_E = new Opcode(OpcodeBytes.RR_IX_E, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RR (IX+n), E", cycles: 23, pseudocode: "E = (IX+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IX_H = new Opcode(OpcodeBytes.RR_IX_H, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RR (IX+n), H", cycles: 23, pseudocode: "H = (IX+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IX_L = new Opcode(OpcodeBytes.RR_IX_L, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RR (IX+n), L", cycles: 23, pseudocode: "L = (IX+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IX_HL = new Opcode(OpcodeBytes.RR_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RR (IX+n)", cycles: 23, pseudocode: "(IX+n) = (IX+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            public static Opcode RR_IX_A = new Opcode(OpcodeBytes.RR_IX_A, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RR (IX+n), A", cycles: 23, pseudocode: "A = (IX+n) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
        #endregion
    }
}
