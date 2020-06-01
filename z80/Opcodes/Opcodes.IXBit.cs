
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IX bit" opcodes and their metadata.
    // These are all three byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IX (0xDD)
    // and the second byte is defined by OpcodeBytes.PREAMBLE_IX_BIT (0xCB).
    public partial class Opcodes
    {
        #region Rotate

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

        #endregion

        #region Shift

            #region SLA r - Shift left (arithmetic)
                public static Opcode SLA_IX_B = new Opcode(OpcodeBytes.SLA_IX_B, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLA (IX+n), B", cycles: 23, pseudocode: "B = (IX+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IX_C = new Opcode(OpcodeBytes.SLA_IX_C, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLA (IX+n), C", cycles: 23, pseudocode: "C = (IX+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IX_D = new Opcode(OpcodeBytes.SLA_IX_D, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLA (IX+n), D", cycles: 23, pseudocode: "D = (IX+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IX_E = new Opcode(OpcodeBytes.SLA_IX_E, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLA (IX+n), E", cycles: 23, pseudocode: "E = (IX+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IX_H = new Opcode(OpcodeBytes.SLA_IX_H, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLA (IX+n), H", cycles: 23, pseudocode: "H = (IX+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IX_L = new Opcode(OpcodeBytes.SLA_IX_L, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLA (IX+n), L", cycles: 23, pseudocode: "L = (IX+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IX = new Opcode(OpcodeBytes.SLA_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLA (IX+n), H", cycles: 23, pseudocode: "(IX+n) = (IX+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IX_A = new Opcode(OpcodeBytes.SLA_IX_A, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLA (IX+n), A", cycles: 23, pseudocode: "A = (IX+n) >> 1; CY = prev bit 7");
            #endregion

            #region SRA r - Shift right (arithmetic)
                public static Opcode SRA_IX_B = new Opcode(OpcodeBytes.SRA_IX_B, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRA (IX+n), B", cycles: 23, pseudocode: "B = (IX+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IX_C = new Opcode(OpcodeBytes.SRA_IX_C, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRA (IX+n), C", cycles: 23, pseudocode: "C = (IX+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IX_D = new Opcode(OpcodeBytes.SRA_IX_D, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRA (IX+n), D", cycles: 23, pseudocode: "D = (IX+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IX_E = new Opcode(OpcodeBytes.SRA_IX_E, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRA (IX+n), E", cycles: 23, pseudocode: "E = (IX+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IX_H = new Opcode(OpcodeBytes.SRA_IX_H, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRA (IX+n), H", cycles: 23, pseudocode: "H = (IX+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IX_L = new Opcode(OpcodeBytes.SRA_IX_L, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRA (IX+n), L", cycles: 23, pseudocode: "L = (IX+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IX = new Opcode(OpcodeBytes.SRA_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRA (IX+n), H", cycles: 23, pseudocode: "(IX+n) = (IX+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IX_A = new Opcode(OpcodeBytes.SRA_IX_A, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRA (IX+n), A", cycles: 23, pseudocode: "A = (IX+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
            #endregion

            // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
            // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
            #region SLL r - Shift left ?? (undocumented)
                public static Opcode SLL_IX_B = new Opcode(OpcodeBytes.SLL_IX_B, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLL (IX+n), B", cycles: 23, pseudocode: "B = (IX+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IX_C = new Opcode(OpcodeBytes.SLL_IX_C, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLL (IX+n), C", cycles: 23, pseudocode: "C = (IX+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IX_D = new Opcode(OpcodeBytes.SLL_IX_D, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLL (IX+n), D", cycles: 23, pseudocode: "D = (IX+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IX_E = new Opcode(OpcodeBytes.SLL_IX_E, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLL (IX+n), E", cycles: 23, pseudocode: "E = (IX+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IX_H = new Opcode(OpcodeBytes.SLL_IX_H, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLL (IX+n), H", cycles: 23, pseudocode: "H = (IX+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IX_L = new Opcode(OpcodeBytes.SLL_IX_L, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLL (IX+n), L", cycles: 23, pseudocode: "L = (IX+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IX = new Opcode(OpcodeBytes.SLL_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLL (IX+n), H", cycles: 23, pseudocode: "(IX+n) = (IX+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IX_A = new Opcode(OpcodeBytes.SLL_IX_A, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SLL (IX+n), A", cycles: 23, pseudocode: "A = (IX+n) << 1; CY = prev bit 7; bit 0 = 1;");
            #endregion

            #region SRL r - Shift right logical
                public static Opcode SRL_IX_B = new Opcode(OpcodeBytes.SRL_IX_B, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRL (IX+n), B", cycles: 23, pseudocode: "B = (IX+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IX_C = new Opcode(OpcodeBytes.SRL_IX_C, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRL (IX+n), C", cycles: 23, pseudocode: "C = (IX+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IX_D = new Opcode(OpcodeBytes.SRL_IX_D, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRL (IX+n), D", cycles: 23, pseudocode: "D = (IX+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IX_E = new Opcode(OpcodeBytes.SRL_IX_E, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRL (IX+n), E", cycles: 23, pseudocode: "E = (IX+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IX_H = new Opcode(OpcodeBytes.SRL_IX_H, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRL (IX+n), H", cycles: 23, pseudocode: "H = (IX+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IX_L = new Opcode(OpcodeBytes.SRL_IX_L, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRL (IX+n), L", cycles: 23, pseudocode: "L = (IX+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IX = new Opcode(OpcodeBytes.SRL_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRL (IX+n), H", cycles: 23, pseudocode: "(IX+n) = (IX+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IX_A = new Opcode(OpcodeBytes.SRL_IX_A, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SRL (IX+n), A", cycles: 23, pseudocode: "A = (IX+n) << 1; CY = prev bit 0;");
            #endregion

        #endregion
    }
}
