
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IY bit" opcodes and their metadata.
    // These are all three byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IY (0xFD)
    // and the second byte is defined by OpcodeBytes.PREAMBLE_IY_BIT (0xCB).
    public partial class Opcodes
    {
        #region Rotate

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

        #endregion

        #region Shift

            #region SLA r - Shift left (arithmetic)
                public static Opcode SLA_IY_B = new Opcode(OpcodeBytes.SLA_IY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), B", cycles: 23, pseudocode: "B = (IY+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IY_C = new Opcode(OpcodeBytes.SLA_IY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), C", cycles: 23, pseudocode: "C = (IY+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IY_D = new Opcode(OpcodeBytes.SLA_IY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), D", cycles: 23, pseudocode: "D = (IY+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IY_E = new Opcode(OpcodeBytes.SLA_IY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), E", cycles: 23, pseudocode: "E = (IY+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IY_H = new Opcode(OpcodeBytes.SLA_IY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), H", cycles: 23, pseudocode: "H = (IY+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IY_L = new Opcode(OpcodeBytes.SLA_IY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), L", cycles: 23, pseudocode: "L = (IY+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IY = new Opcode(OpcodeBytes.SLA_IY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), H", cycles: 23, pseudocode: "(IY+n) = (IY+n) >> 1; CY = prev bit 7");
                public static Opcode SLA_IY_A = new Opcode(OpcodeBytes.SLA_IY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), A", cycles: 23, pseudocode: "A = (IY+n) >> 1; CY = prev bit 7");
            #endregion

            #region SRA r - Shift right (arithmetic)
                public static Opcode SRA_IY_B = new Opcode(OpcodeBytes.SRA_IY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), B", cycles: 23, pseudocode: "B = (IY+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IY_C = new Opcode(OpcodeBytes.SRA_IY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), C", cycles: 23, pseudocode: "C = (IY+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IY_D = new Opcode(OpcodeBytes.SRA_IY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), D", cycles: 23, pseudocode: "D = (IY+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IY_E = new Opcode(OpcodeBytes.SRA_IY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), E", cycles: 23, pseudocode: "E = (IY+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IY_H = new Opcode(OpcodeBytes.SRA_IY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), H", cycles: 23, pseudocode: "H = (IY+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IY_L = new Opcode(OpcodeBytes.SRA_IY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), L", cycles: 23, pseudocode: "L = (IY+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IY = new Opcode(OpcodeBytes.SRA_IY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), H", cycles: 23, pseudocode: "(IY+n) = (IY+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_IY_A = new Opcode(OpcodeBytes.SRA_IY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), A", cycles: 23, pseudocode: "A = (IY+n) >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
            #endregion

            // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
            // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
            #region SLL r - Shift left ?? (undocumented)
                public static Opcode SLL_IY_B = new Opcode(OpcodeBytes.SLL_IY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), B", cycles: 23, pseudocode: "B = (IY+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IY_C = new Opcode(OpcodeBytes.SLL_IY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), C", cycles: 23, pseudocode: "C = (IY+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IY_D = new Opcode(OpcodeBytes.SLL_IY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), D", cycles: 23, pseudocode: "D = (IY+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IY_E = new Opcode(OpcodeBytes.SLL_IY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), E", cycles: 23, pseudocode: "E = (IY+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IY_H = new Opcode(OpcodeBytes.SLL_IY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), H", cycles: 23, pseudocode: "H = (IY+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IY_L = new Opcode(OpcodeBytes.SLL_IY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), L", cycles: 23, pseudocode: "L = (IY+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IY = new Opcode(OpcodeBytes.SLL_IY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), H", cycles: 23, pseudocode: "(IY+n) = (IY+n) << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_IY_A = new Opcode(OpcodeBytes.SLL_IY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), A", cycles: 23, pseudocode: "A = (IY+n) << 1; CY = prev bit 7; bit 0 = 1;");
            #endregion

            #region SRL r - Shift right logical
                public static Opcode SRL_IY_B = new Opcode(OpcodeBytes.SRL_IY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), B", cycles: 23, pseudocode: "B = (IY+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IY_C = new Opcode(OpcodeBytes.SRL_IY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), C", cycles: 23, pseudocode: "C = (IY+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IY_D = new Opcode(OpcodeBytes.SRL_IY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), D", cycles: 23, pseudocode: "D = (IY+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IY_E = new Opcode(OpcodeBytes.SRL_IY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), E", cycles: 23, pseudocode: "E = (IY+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IY_H = new Opcode(OpcodeBytes.SRL_IY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), H", cycles: 23, pseudocode: "H = (IY+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IY_L = new Opcode(OpcodeBytes.SRL_IY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), L", cycles: 23, pseudocode: "L = (IY+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IY = new Opcode(OpcodeBytes.SRL_IY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), H", cycles: 23, pseudocode: "(IY+n) = (IY+n) << 1; CY = prev bit 0;");
                public static Opcode SRL_IY_A = new Opcode(OpcodeBytes.SRL_IY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), A", cycles: 23, pseudocode: "A = (IY+n) << 1; CY = prev bit 0;");
            #endregion

        #endregion
    }
}
