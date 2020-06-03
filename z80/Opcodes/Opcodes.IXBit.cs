
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

        #region Test Bit

            public static Opcode BIT_0_IX_2 = new Opcode(OpcodeBytes.BIT_0_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 0, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit0 == 0");
            public static Opcode BIT_0_IX_3 = new Opcode(OpcodeBytes.BIT_0_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 0, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit0 == 0");
            public static Opcode BIT_0_IX_4 = new Opcode(OpcodeBytes.BIT_0_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 0, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit0 == 0");
            public static Opcode BIT_0_IX_5 = new Opcode(OpcodeBytes.BIT_0_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 0, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit0 == 0");
            public static Opcode BIT_0_IX_6 = new Opcode(OpcodeBytes.BIT_0_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 0, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit0 == 0");
            public static Opcode BIT_0_IX_7 = new Opcode(OpcodeBytes.BIT_0_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 0, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit0 == 0");
            public static Opcode BIT_0_IX = new Opcode(OpcodeBytes.BIT_0_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 0, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit0 == 0");
            public static Opcode BIT_0_IX_8 = new Opcode(OpcodeBytes.BIT_0_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 0, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit0 == 0");

            public static Opcode BIT_1_IX_2 = new Opcode(OpcodeBytes.BIT_1_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 1, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit1 == 0");
            public static Opcode BIT_1_IX_3 = new Opcode(OpcodeBytes.BIT_1_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 1, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit1 == 0");
            public static Opcode BIT_1_IX_4 = new Opcode(OpcodeBytes.BIT_1_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 1, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit1 == 0");
            public static Opcode BIT_1_IX_5 = new Opcode(OpcodeBytes.BIT_1_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 1, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit1 == 0");
            public static Opcode BIT_1_IX_6 = new Opcode(OpcodeBytes.BIT_1_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 1, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit1 == 0");
            public static Opcode BIT_1_IX_7 = new Opcode(OpcodeBytes.BIT_1_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 1, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit1 == 0");
            public static Opcode BIT_1_IX = new Opcode(OpcodeBytes.BIT_1_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 1, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit1 == 0");
            public static Opcode BIT_1_IX_8 = new Opcode(OpcodeBytes.BIT_1_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 1, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit1 == 0");

            public static Opcode BIT_2_IX_2 = new Opcode(OpcodeBytes.BIT_2_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 2, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit2 == 0");
            public static Opcode BIT_2_IX_3 = new Opcode(OpcodeBytes.BIT_2_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 2, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit2 == 0");
            public static Opcode BIT_2_IX_4 = new Opcode(OpcodeBytes.BIT_2_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 2, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit2 == 0");
            public static Opcode BIT_2_IX_5 = new Opcode(OpcodeBytes.BIT_2_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 2, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit2 == 0");
            public static Opcode BIT_2_IX_6 = new Opcode(OpcodeBytes.BIT_2_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 2, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit2 == 0");
            public static Opcode BIT_2_IX_7 = new Opcode(OpcodeBytes.BIT_2_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 2, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit2 == 0");
            public static Opcode BIT_2_IX = new Opcode(OpcodeBytes.BIT_2_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 2, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit2 == 0");
            public static Opcode BIT_2_IX_8 = new Opcode(OpcodeBytes.BIT_2_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 2, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit2 == 0");

            public static Opcode BIT_3_IX_2 = new Opcode(OpcodeBytes.BIT_3_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 3, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit3 == 0");
            public static Opcode BIT_3_IX_3 = new Opcode(OpcodeBytes.BIT_3_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 3, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit3 == 0");
            public static Opcode BIT_3_IX_4 = new Opcode(OpcodeBytes.BIT_3_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 3, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit3 == 0");
            public static Opcode BIT_3_IX_5 = new Opcode(OpcodeBytes.BIT_3_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 3, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit3 == 0");
            public static Opcode BIT_3_IX_6 = new Opcode(OpcodeBytes.BIT_3_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 3, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit3 == 0");
            public static Opcode BIT_3_IX_7 = new Opcode(OpcodeBytes.BIT_3_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 3, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit3 == 0");
            public static Opcode BIT_3_IX = new Opcode(OpcodeBytes.BIT_3_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 3, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit3 == 0");
            public static Opcode BIT_3_IX_8 = new Opcode(OpcodeBytes.BIT_3_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 3, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit3 == 0");

            public static Opcode BIT_4_IX_2 = new Opcode(OpcodeBytes.BIT_4_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 4, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit4 == 0");
            public static Opcode BIT_4_IX_3 = new Opcode(OpcodeBytes.BIT_4_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 4, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit4 == 0");
            public static Opcode BIT_4_IX_4 = new Opcode(OpcodeBytes.BIT_4_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 4, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit4 == 0");
            public static Opcode BIT_4_IX_5 = new Opcode(OpcodeBytes.BIT_4_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 4, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit4 == 0");
            public static Opcode BIT_4_IX_6 = new Opcode(OpcodeBytes.BIT_4_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 4, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit4 == 0");
            public static Opcode BIT_4_IX_7 = new Opcode(OpcodeBytes.BIT_4_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 4, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit4 == 0");
            public static Opcode BIT_4_IX = new Opcode(OpcodeBytes.BIT_4_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 4, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit4 == 0");
            public static Opcode BIT_4_IX_8 = new Opcode(OpcodeBytes.BIT_4_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 4, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit4 == 0");

            public static Opcode BIT_5_IX_2 = new Opcode(OpcodeBytes.BIT_5_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 5, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit5 == 0");
            public static Opcode BIT_5_IX_3 = new Opcode(OpcodeBytes.BIT_5_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 5, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit5 == 0");
            public static Opcode BIT_5_IX_4 = new Opcode(OpcodeBytes.BIT_5_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 5, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit5 == 0");
            public static Opcode BIT_5_IX_5 = new Opcode(OpcodeBytes.BIT_5_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 5, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit5 == 0");
            public static Opcode BIT_5_IX_6 = new Opcode(OpcodeBytes.BIT_5_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 5, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit5 == 0");
            public static Opcode BIT_5_IX_7 = new Opcode(OpcodeBytes.BIT_5_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 5, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit5 == 0");
            public static Opcode BIT_5_IX = new Opcode(OpcodeBytes.BIT_5_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 5, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit5 == 0");
            public static Opcode BIT_5_IX_8 = new Opcode(OpcodeBytes.BIT_5_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 5, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit5 == 0");

            public static Opcode BIT_6_IX_2 = new Opcode(OpcodeBytes.BIT_6_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 6, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit6 == 0");
            public static Opcode BIT_6_IX_3 = new Opcode(OpcodeBytes.BIT_6_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 6, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit6 == 0");
            public static Opcode BIT_6_IX_4 = new Opcode(OpcodeBytes.BIT_6_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 6, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit6 == 0");
            public static Opcode BIT_6_IX_5 = new Opcode(OpcodeBytes.BIT_6_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 6, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit6 == 0");
            public static Opcode BIT_6_IX_6 = new Opcode(OpcodeBytes.BIT_6_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 6, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit6 == 0");
            public static Opcode BIT_6_IX_7 = new Opcode(OpcodeBytes.BIT_6_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 6, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit6 == 0");
            public static Opcode BIT_6_IX = new Opcode(OpcodeBytes.BIT_6_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 6, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit6 == 0");
            public static Opcode BIT_6_IX_8 = new Opcode(OpcodeBytes.BIT_6_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 6, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit6 == 0");

            public static Opcode BIT_7_IX_2 = new Opcode(OpcodeBytes.BIT_7_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 7, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit7 == 0");
            public static Opcode BIT_7_IX_3 = new Opcode(OpcodeBytes.BIT_7_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 7, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit7 == 0");
            public static Opcode BIT_7_IX_4 = new Opcode(OpcodeBytes.BIT_7_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 7, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit7 == 0");
            public static Opcode BIT_7_IX_5 = new Opcode(OpcodeBytes.BIT_7_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 7, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit7 == 0");
            public static Opcode BIT_7_IX_6 = new Opcode(OpcodeBytes.BIT_7_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 7, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit7 == 0");
            public static Opcode BIT_7_IX_7 = new Opcode(OpcodeBytes.BIT_7_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 7, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit7 == 0");
            public static Opcode BIT_7_IX = new Opcode(OpcodeBytes.BIT_7_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 7, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit7 == 0");
            public static Opcode BIT_7_IX_8 = new Opcode(OpcodeBytes.BIT_7_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "BIT 7, (IX+n)", cycles: 20, pseudocode: "Z = (IX+n) bit7 == 0");

        #endregion

        #region Reset Bit

            public static Opcode RES_0_IX_2 = new Opcode(OpcodeBytes.RES_0_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 0");
            public static Opcode RES_0_IX_3 = new Opcode(OpcodeBytes.RES_0_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 0");
            public static Opcode RES_0_IX_4 = new Opcode(OpcodeBytes.RES_0_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 0");
            public static Opcode RES_0_IX_5 = new Opcode(OpcodeBytes.RES_0_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 0");
            public static Opcode RES_0_IX_6 = new Opcode(OpcodeBytes.RES_0_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 0");
            public static Opcode RES_0_IX_7 = new Opcode(OpcodeBytes.RES_0_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 0");
            public static Opcode RES_0_IX = new Opcode(OpcodeBytes.RES_0_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (HL) to 0");
            public static Opcode RES_0_IX_8 = new Opcode(OpcodeBytes.RES_0_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 0");

            public static Opcode RES_1_IX_2 = new Opcode(OpcodeBytes.RES_1_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 0");
            public static Opcode RES_1_IX_3 = new Opcode(OpcodeBytes.RES_1_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 0");
            public static Opcode RES_1_IX_4 = new Opcode(OpcodeBytes.RES_1_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 0");
            public static Opcode RES_1_IX_5 = new Opcode(OpcodeBytes.RES_1_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 0");
            public static Opcode RES_1_IX_6 = new Opcode(OpcodeBytes.RES_1_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 0");
            public static Opcode RES_1_IX_7 = new Opcode(OpcodeBytes.RES_1_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 0");
            public static Opcode RES_1_IX = new Opcode(OpcodeBytes.RES_1_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (HL) to 0");
            public static Opcode RES_1_IX_8 = new Opcode(OpcodeBytes.RES_1_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 0");

            public static Opcode RES_2_IX_2 = new Opcode(OpcodeBytes.RES_2_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 0");
            public static Opcode RES_2_IX_3 = new Opcode(OpcodeBytes.RES_2_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 0");
            public static Opcode RES_2_IX_4 = new Opcode(OpcodeBytes.RES_2_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 0");
            public static Opcode RES_2_IX_5 = new Opcode(OpcodeBytes.RES_2_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 0");
            public static Opcode RES_2_IX_6 = new Opcode(OpcodeBytes.RES_2_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 0");
            public static Opcode RES_2_IX_7 = new Opcode(OpcodeBytes.RES_2_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 0");
            public static Opcode RES_2_IX = new Opcode(OpcodeBytes.RES_2_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (HL) to 0");
            public static Opcode RES_2_IX_8 = new Opcode(OpcodeBytes.RES_2_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 0");

            public static Opcode RES_3_IX_2 = new Opcode(OpcodeBytes.RES_3_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 0");
            public static Opcode RES_3_IX_3 = new Opcode(OpcodeBytes.RES_3_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 0");
            public static Opcode RES_3_IX_4 = new Opcode(OpcodeBytes.RES_3_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 0");
            public static Opcode RES_3_IX_5 = new Opcode(OpcodeBytes.RES_3_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 0");
            public static Opcode RES_3_IX_6 = new Opcode(OpcodeBytes.RES_3_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 0");
            public static Opcode RES_3_IX_7 = new Opcode(OpcodeBytes.RES_3_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 0");
            public static Opcode RES_3_IX = new Opcode(OpcodeBytes.RES_3_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (HL) to 0");
            public static Opcode RES_3_IX_8 = new Opcode(OpcodeBytes.RES_3_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 0");

            public static Opcode RES_4_IX_2 = new Opcode(OpcodeBytes.RES_4_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 0");
            public static Opcode RES_4_IX_3 = new Opcode(OpcodeBytes.RES_4_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 0");
            public static Opcode RES_4_IX_4 = new Opcode(OpcodeBytes.RES_4_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 0");
            public static Opcode RES_4_IX_5 = new Opcode(OpcodeBytes.RES_4_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 0");
            public static Opcode RES_4_IX_6 = new Opcode(OpcodeBytes.RES_4_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 0");
            public static Opcode RES_4_IX_7 = new Opcode(OpcodeBytes.RES_4_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 0");
            public static Opcode RES_4_IX = new Opcode(OpcodeBytes.RES_4_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (HL) to 0");
            public static Opcode RES_4_IX_8 = new Opcode(OpcodeBytes.RES_4_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 0");

            public static Opcode RES_5_IX_2 = new Opcode(OpcodeBytes.RES_5_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 0");
            public static Opcode RES_5_IX_3 = new Opcode(OpcodeBytes.RES_5_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 0");
            public static Opcode RES_5_IX_4 = new Opcode(OpcodeBytes.RES_5_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 0");
            public static Opcode RES_5_IX_5 = new Opcode(OpcodeBytes.RES_5_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 0");
            public static Opcode RES_5_IX_6 = new Opcode(OpcodeBytes.RES_5_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 0");
            public static Opcode RES_5_IX_7 = new Opcode(OpcodeBytes.RES_5_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 0");
            public static Opcode RES_5_IX = new Opcode(OpcodeBytes.RES_5_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (HL) to 0");
            public static Opcode RES_5_IX_8 = new Opcode(OpcodeBytes.RES_5_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 0");

            public static Opcode RES_6_IX_2 = new Opcode(OpcodeBytes.RES_6_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 0");
            public static Opcode RES_6_IX_3 = new Opcode(OpcodeBytes.RES_6_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 0");
            public static Opcode RES_6_IX_4 = new Opcode(OpcodeBytes.RES_6_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 0");
            public static Opcode RES_6_IX_5 = new Opcode(OpcodeBytes.RES_6_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 0");
            public static Opcode RES_6_IX_6 = new Opcode(OpcodeBytes.RES_6_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 0");
            public static Opcode RES_6_IX_7 = new Opcode(OpcodeBytes.RES_6_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 0");
            public static Opcode RES_6_IX = new Opcode(OpcodeBytes.RES_6_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (HL) to 0");
            public static Opcode RES_6_IX_8 = new Opcode(OpcodeBytes.RES_6_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 0");

            public static Opcode RES_7_IX_2 = new Opcode(OpcodeBytes.RES_7_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 0");
            public static Opcode RES_7_IX_3 = new Opcode(OpcodeBytes.RES_7_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 0");
            public static Opcode RES_7_IX_4 = new Opcode(OpcodeBytes.RES_7_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 0");
            public static Opcode RES_7_IX_5 = new Opcode(OpcodeBytes.RES_7_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 0");
            public static Opcode RES_7_IX_6 = new Opcode(OpcodeBytes.RES_7_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 0");
            public static Opcode RES_7_IX_7 = new Opcode(OpcodeBytes.RES_7_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 0");
            public static Opcode RES_7_IX = new Opcode(OpcodeBytes.RES_7_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (HL) to 0");
            public static Opcode RES_7_IX_8 = new Opcode(OpcodeBytes.RES_7_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "RES 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 0");

        #endregion

        #region Set Bit

            public static Opcode SET_0_IX_2 = new Opcode(OpcodeBytes.SET_0_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 1");
            public static Opcode SET_0_IX_3 = new Opcode(OpcodeBytes.SET_0_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 1");
            public static Opcode SET_0_IX_4 = new Opcode(OpcodeBytes.SET_0_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 1");
            public static Opcode SET_0_IX_5 = new Opcode(OpcodeBytes.SET_0_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 1");
            public static Opcode SET_0_IX_6 = new Opcode(OpcodeBytes.SET_0_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 1");
            public static Opcode SET_0_IX_7 = new Opcode(OpcodeBytes.SET_0_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 1");
            public static Opcode SET_0_IX = new Opcode(OpcodeBytes.SET_0_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (HL) to 1");
            public static Opcode SET_0_IX_8 = new Opcode(OpcodeBytes.SET_0_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 0, (IX+n)", cycles: 23, pseudocode: "Set bit 0 of (IX+n) to 1");

            public static Opcode SET_1_IX_2 = new Opcode(OpcodeBytes.SET_1_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 1");
            public static Opcode SET_1_IX_3 = new Opcode(OpcodeBytes.SET_1_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 1");
            public static Opcode SET_1_IX_4 = new Opcode(OpcodeBytes.SET_1_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 1");
            public static Opcode SET_1_IX_5 = new Opcode(OpcodeBytes.SET_1_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 1");
            public static Opcode SET_1_IX_6 = new Opcode(OpcodeBytes.SET_1_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 1");
            public static Opcode SET_1_IX_7 = new Opcode(OpcodeBytes.SET_1_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 1");
            public static Opcode SET_1_IX = new Opcode(OpcodeBytes.SET_1_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (HL) to 1");
            public static Opcode SET_1_IX_8 = new Opcode(OpcodeBytes.SET_1_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 1, (IX+n)", cycles: 23, pseudocode: "Set bit 1 of (IX+n) to 1");

            public static Opcode SET_2_IX_2 = new Opcode(OpcodeBytes.SET_2_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 1");
            public static Opcode SET_2_IX_3 = new Opcode(OpcodeBytes.SET_2_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 1");
            public static Opcode SET_2_IX_4 = new Opcode(OpcodeBytes.SET_2_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 1");
            public static Opcode SET_2_IX_5 = new Opcode(OpcodeBytes.SET_2_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 1");
            public static Opcode SET_2_IX_6 = new Opcode(OpcodeBytes.SET_2_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 1");
            public static Opcode SET_2_IX_7 = new Opcode(OpcodeBytes.SET_2_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 1");
            public static Opcode SET_2_IX = new Opcode(OpcodeBytes.SET_2_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (HL) to 1");
            public static Opcode SET_2_IX_8 = new Opcode(OpcodeBytes.SET_2_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 2, (IX+n)", cycles: 23, pseudocode: "Set bit 2 of (IX+n) to 1");

            public static Opcode SET_3_IX_2 = new Opcode(OpcodeBytes.SET_3_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 1");
            public static Opcode SET_3_IX_3 = new Opcode(OpcodeBytes.SET_3_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 1");
            public static Opcode SET_3_IX_4 = new Opcode(OpcodeBytes.SET_3_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 1");
            public static Opcode SET_3_IX_5 = new Opcode(OpcodeBytes.SET_3_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 1");
            public static Opcode SET_3_IX_6 = new Opcode(OpcodeBytes.SET_3_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 1");
            public static Opcode SET_3_IX_7 = new Opcode(OpcodeBytes.SET_3_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 1");
            public static Opcode SET_3_IX = new Opcode(OpcodeBytes.SET_3_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (HL) to 1");
            public static Opcode SET_3_IX_8 = new Opcode(OpcodeBytes.SET_3_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 3, (IX+n)", cycles: 23, pseudocode: "Set bit 3 of (IX+n) to 1");

            public static Opcode SET_4_IX_2 = new Opcode(OpcodeBytes.SET_4_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 1");
            public static Opcode SET_4_IX_3 = new Opcode(OpcodeBytes.SET_4_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 1");
            public static Opcode SET_4_IX_4 = new Opcode(OpcodeBytes.SET_4_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 1");
            public static Opcode SET_4_IX_5 = new Opcode(OpcodeBytes.SET_4_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 1");
            public static Opcode SET_4_IX_6 = new Opcode(OpcodeBytes.SET_4_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 1");
            public static Opcode SET_4_IX_7 = new Opcode(OpcodeBytes.SET_4_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 1");
            public static Opcode SET_4_IX = new Opcode(OpcodeBytes.SET_4_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (HL) to 1");
            public static Opcode SET_4_IX_8 = new Opcode(OpcodeBytes.SET_4_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 4, (IX+n)", cycles: 23, pseudocode: "Set bit 4 of (IX+n) to 1");

            public static Opcode SET_5_IX_2 = new Opcode(OpcodeBytes.SET_5_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 1");
            public static Opcode SET_5_IX_3 = new Opcode(OpcodeBytes.SET_5_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 1");
            public static Opcode SET_5_IX_4 = new Opcode(OpcodeBytes.SET_5_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 1");
            public static Opcode SET_5_IX_5 = new Opcode(OpcodeBytes.SET_5_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 1");
            public static Opcode SET_5_IX_6 = new Opcode(OpcodeBytes.SET_5_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 1");
            public static Opcode SET_5_IX_7 = new Opcode(OpcodeBytes.SET_5_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 1");
            public static Opcode SET_5_IX = new Opcode(OpcodeBytes.SET_5_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (HL) to 1");
            public static Opcode SET_5_IX_8 = new Opcode(OpcodeBytes.SET_5_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 5, (IX+n)", cycles: 23, pseudocode: "Set bit 5 of (IX+n) to 1");

            public static Opcode SET_6_IX_2 = new Opcode(OpcodeBytes.SET_6_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 1");
            public static Opcode SET_6_IX_3 = new Opcode(OpcodeBytes.SET_6_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 1");
            public static Opcode SET_6_IX_4 = new Opcode(OpcodeBytes.SET_6_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 1");
            public static Opcode SET_6_IX_5 = new Opcode(OpcodeBytes.SET_6_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 1");
            public static Opcode SET_6_IX_6 = new Opcode(OpcodeBytes.SET_6_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 1");
            public static Opcode SET_6_IX_7 = new Opcode(OpcodeBytes.SET_6_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 1");
            public static Opcode SET_6_IX = new Opcode(OpcodeBytes.SET_6_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (HL) to 1");
            public static Opcode SET_6_IX_8 = new Opcode(OpcodeBytes.SET_6_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 6, (IX+n)", cycles: 23, pseudocode: "Set bit 6 of (IX+n) to 1");

            public static Opcode SET_7_IX_2 = new Opcode(OpcodeBytes.SET_7_IX_2, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 1");
            public static Opcode SET_7_IX_3 = new Opcode(OpcodeBytes.SET_7_IX_3, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 1");
            public static Opcode SET_7_IX_4 = new Opcode(OpcodeBytes.SET_7_IX_4, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 1");
            public static Opcode SET_7_IX_5 = new Opcode(OpcodeBytes.SET_7_IX_5, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 1");
            public static Opcode SET_7_IX_6 = new Opcode(OpcodeBytes.SET_7_IX_6, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 1");
            public static Opcode SET_7_IX_7 = new Opcode(OpcodeBytes.SET_7_IX_7, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 1");
            public static Opcode SET_7_IX = new Opcode(OpcodeBytes.SET_7_IX, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (HL) to 1");
            public static Opcode SET_7_IX_8 = new Opcode(OpcodeBytes.SET_7_IX_8, instructionSet: InstructionSet.IXBit, size: 4, instruction: "SET 7, (IX+n)", cycles: 23, pseudocode: "Set bit 7 of (IX+n) to 1");

        #endregion
    }
}
