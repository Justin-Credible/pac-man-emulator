
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IY bit" opcodes and their metadata.
    // These are all four byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IY (0xFD)
    // and the second byte is defined by OpcodeBytes.PREAMBLE_IY_BIT (0xCB), the third byte is the IY offset,
    // and the fourth byte is the operation.
    public partial class Opcodes
    {
        #region Rotate

            #region RLC (IY+n), r - Rotate left
                public static Opcode RLC_MIY_B = new Opcode(OpcodeBytes.RLC_MIY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), B", cycles: 23, pseudocode: "B <- (IY+n) << 1; bit 0 <- prev bit 7; CY <- prev bit 7");
                public static Opcode RLC_MIY_C = new Opcode(OpcodeBytes.RLC_MIY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), C", cycles: 23, pseudocode: "C <- (IY+n) << 1; bit 0 <- prev bit 7; CY <- prev bit 7");
                public static Opcode RLC_MIY_D = new Opcode(OpcodeBytes.RLC_MIY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), D", cycles: 23, pseudocode: "D <- (IY+n) << 1; bit 0 <- prev bit 7; CY <- prev bit 7");
                public static Opcode RLC_MIY_E = new Opcode(OpcodeBytes.RLC_MIY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), E", cycles: 23, pseudocode: "E <- (IY+n) << 1; bit 0 <- prev bit 7; CY <- prev bit 7");
                public static Opcode RLC_MIY_H = new Opcode(OpcodeBytes.RLC_MIY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), H", cycles: 23, pseudocode: "H <- (IY+n) << 1; bit 0 <- prev bit 7; CY <- prev bit 7");
                public static Opcode RLC_MIY_L = new Opcode(OpcodeBytes.RLC_MIY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), L", cycles: 23, pseudocode: "L <- (IY+n) << 1; bit 0 <- prev bit 7; CY <- prev bit 7");
                public static Opcode RLC_MIY_HL = new Opcode(OpcodeBytes.RLC_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n)", cycles: 23, pseudocode: "(IY+n) <- (IY+n) << 1; bit 0 <- prev bit 7; CY <- prev bit 7");
                public static Opcode RLC_MIY_A = new Opcode(OpcodeBytes.RLC_MIY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RLC (IY+n), A", cycles: 23, pseudocode: "A <- (IY+n) << 1; bit 0 <- prev bit 7; CY <- prev bit 7");
            #endregion

            #region RRC (IY+n), r - Rotate right
                public static Opcode RRC_MIY_B = new Opcode(OpcodeBytes.RRC_MIY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), B", cycles: 23, pseudocode: "B <- (IY+n) >> 1; bit 7 <- prev bit 0; CY <- prev bit 0");
                public static Opcode RRC_MIY_C = new Opcode(OpcodeBytes.RRC_MIY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), C", cycles: 23, pseudocode: "C <- (IY+n) >> 1; bit 7 <- prev bit 0; CY <- prev bit 0");
                public static Opcode RRC_MIY_D = new Opcode(OpcodeBytes.RRC_MIY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), D", cycles: 23, pseudocode: "D <- (IY+n) >> 1; bit 7 <- prev bit 0; CY <- prev bit 0");
                public static Opcode RRC_MIY_E = new Opcode(OpcodeBytes.RRC_MIY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), E", cycles: 23, pseudocode: "E <- (IY+n) >> 1; bit 7 <- prev bit 0; CY <- prev bit 0");
                public static Opcode RRC_MIY_H = new Opcode(OpcodeBytes.RRC_MIY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), H", cycles: 23, pseudocode: "H <- (IY+n) >> 1; bit 7 <- prev bit 0; CY <- prev bit 0");
                public static Opcode RRC_MIY_L = new Opcode(OpcodeBytes.RRC_MIY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), L", cycles: 23, pseudocode: "L <- (IY+n) >> 1; bit 7 <- prev bit 0; CY <- prev bit 0");
                public static Opcode RRC_MIY_HL = new Opcode(OpcodeBytes.RRC_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n)", cycles: 23, pseudocode: "(IY+n) <- (IY+n) >> 1; bit 7 <- prev bit 0; CY <- prev bit 0");
                public static Opcode RRC_MIY_A = new Opcode(OpcodeBytes.RRC_MIY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RRC (IY+n), A", cycles: 23, pseudocode: "A <- (IY+n) >> 1; bit 7 <- prev bit 0; CY <- prev bit 0");
            #endregion

            #region RL (IY+n), r - Rotate left through carry
                public static Opcode RL_MIY_B = new Opcode(OpcodeBytes.RL_MIY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), B", cycles: 23, pseudocode: "B <- (IY+n) << 1; bit 0 <- prev CY; CY <- prev bit 7");
                public static Opcode RL_MIY_C = new Opcode(OpcodeBytes.RL_MIY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), C", cycles: 23, pseudocode: "C <- (IY+n) << 1; bit 0 <- prev CY; CY <- prev bit 7");
                public static Opcode RL_MIY_D = new Opcode(OpcodeBytes.RL_MIY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), D", cycles: 23, pseudocode: "D <- (IY+n) << 1; bit 0 <- prev CY; CY <- prev bit 7");
                public static Opcode RL_MIY_E = new Opcode(OpcodeBytes.RL_MIY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), E", cycles: 23, pseudocode: "E <- (IY+n) << 1; bit 0 <- prev CY; CY <- prev bit 7");
                public static Opcode RL_MIY_H = new Opcode(OpcodeBytes.RL_MIY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), H", cycles: 23, pseudocode: "H <- (IY+n) << 1; bit 0 <- prev CY; CY <- prev bit 7");
                public static Opcode RL_MIY_L = new Opcode(OpcodeBytes.RL_MIY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), L", cycles: 23, pseudocode: "L <- (IY+n) << 1; bit 0 <- prev CY; CY <- prev bit 7");
                public static Opcode RL_MIY_HL = new Opcode(OpcodeBytes.RL_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n)", cycles: 23, pseudocode: "(IY+n) <- (IY+n) << 1; bit 0 <- prev CY; CY <- prev bit 7");
                public static Opcode RL_MIY_A = new Opcode(OpcodeBytes.RL_MIY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RL (IY+n), A", cycles: 23, pseudocode: "A <- (IY+n) << 1; bit 0 <- prev CY; CY <- prev bit 7");
            #endregion

            #region RR (IY+n), r - Rotate right through carry
                public static Opcode RR_MIY_B = new Opcode(OpcodeBytes.RR_MIY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), B", cycles: 23, pseudocode: "B <- (IY+n) >> 1; bit 7 <- prev bit 7; CY <- prev bit 0");
                public static Opcode RR_MIY_C = new Opcode(OpcodeBytes.RR_MIY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), C", cycles: 23, pseudocode: "C <- (IY+n) >> 1; bit 7 <- prev bit 7; CY <- prev bit 0");
                public static Opcode RR_MIY_D = new Opcode(OpcodeBytes.RR_MIY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), D", cycles: 23, pseudocode: "D <- (IY+n) >> 1; bit 7 <- prev bit 7; CY <- prev bit 0");
                public static Opcode RR_MIY_E = new Opcode(OpcodeBytes.RR_MIY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), E", cycles: 23, pseudocode: "E <- (IY+n) >> 1; bit 7 <- prev bit 7; CY <- prev bit 0");
                public static Opcode RR_MIY_H = new Opcode(OpcodeBytes.RR_MIY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), H", cycles: 23, pseudocode: "H <- (IY+n) >> 1; bit 7 <- prev bit 7; CY <- prev bit 0");
                public static Opcode RR_MIY_L = new Opcode(OpcodeBytes.RR_MIY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), L", cycles: 23, pseudocode: "L <- (IY+n) >> 1; bit 7 <- prev bit 7; CY <- prev bit 0");
                public static Opcode RR_MIY_HL = new Opcode(OpcodeBytes.RR_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n)", cycles: 23, pseudocode: "(IY+n) <- (IY+n) >> 1; bit 7 <- prev bit 7; CY <- prev bit 0");
                public static Opcode RR_MIY_A = new Opcode(OpcodeBytes.RR_MIY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RR (IY+n), A", cycles: 23, pseudocode: "A <- (IY+n) >> 1; bit 7 <- prev bit 7; CY <- prev bit 0");
            #endregion

        #endregion

        #region Shift

            #region SLA r - Shift left (arithmetic)
                public static Opcode SLA_MIY_B = new Opcode(OpcodeBytes.SLA_MIY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), B", cycles: 23, pseudocode: "B <- (IY+n) >> 1; CY <- prev bit 7");
                public static Opcode SLA_MIY_C = new Opcode(OpcodeBytes.SLA_MIY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), C", cycles: 23, pseudocode: "C <- (IY+n) >> 1; CY <- prev bit 7");
                public static Opcode SLA_MIY_D = new Opcode(OpcodeBytes.SLA_MIY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), D", cycles: 23, pseudocode: "D <- (IY+n) >> 1; CY <- prev bit 7");
                public static Opcode SLA_MIY_E = new Opcode(OpcodeBytes.SLA_MIY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), E", cycles: 23, pseudocode: "E <- (IY+n) >> 1; CY <- prev bit 7");
                public static Opcode SLA_MIY_H = new Opcode(OpcodeBytes.SLA_MIY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), H", cycles: 23, pseudocode: "H <- (IY+n) >> 1; CY <- prev bit 7");
                public static Opcode SLA_MIY_L = new Opcode(OpcodeBytes.SLA_MIY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), L", cycles: 23, pseudocode: "L <- (IY+n) >> 1; CY <- prev bit 7");
                public static Opcode SLA_MIY = new Opcode(OpcodeBytes.SLA_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), H", cycles: 23, pseudocode: "(IY+n) <- (IY+n) >> 1; CY <- prev bit 7");
                public static Opcode SLA_MIY_A = new Opcode(OpcodeBytes.SLA_MIY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLA (IY+n), A", cycles: 23, pseudocode: "A <- (IY+n) >> 1; CY <- prev bit 7");
            #endregion

            #region SRA r - Shift right (arithmetic)
                public static Opcode SRA_MIY_B = new Opcode(OpcodeBytes.SRA_MIY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), B", cycles: 23, pseudocode: "B <- (IY+n) >> 1; CY <- prev bit 0; bit 7 <- prev bit 7;");
                public static Opcode SRA_MIY_C = new Opcode(OpcodeBytes.SRA_MIY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), C", cycles: 23, pseudocode: "C <- (IY+n) >> 1; CY <- prev bit 0; bit 7 <- prev bit 7;");
                public static Opcode SRA_MIY_D = new Opcode(OpcodeBytes.SRA_MIY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), D", cycles: 23, pseudocode: "D <- (IY+n) >> 1; CY <- prev bit 0; bit 7 <- prev bit 7;");
                public static Opcode SRA_MIY_E = new Opcode(OpcodeBytes.SRA_MIY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), E", cycles: 23, pseudocode: "E <- (IY+n) >> 1; CY <- prev bit 0; bit 7 <- prev bit 7;");
                public static Opcode SRA_MIY_H = new Opcode(OpcodeBytes.SRA_MIY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), H", cycles: 23, pseudocode: "H <- (IY+n) >> 1; CY <- prev bit 0; bit 7 <- prev bit 7;");
                public static Opcode SRA_MIY_L = new Opcode(OpcodeBytes.SRA_MIY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), L", cycles: 23, pseudocode: "L <- (IY+n) >> 1; CY <- prev bit 0; bit 7 <- prev bit 7;");
                public static Opcode SRA_MIY = new Opcode(OpcodeBytes.SRA_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), H", cycles: 23, pseudocode: "(IY+n) <- (IY+n) >> 1; CY <- prev bit 0; bit 7 <- prev bit 7;");
                public static Opcode SRA_MIY_A = new Opcode(OpcodeBytes.SRA_MIY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRA (IY+n), A", cycles: 23, pseudocode: "A <- (IY+n) >> 1; CY <- prev bit 0; bit 7 <- prev bit 7;");
            #endregion

            // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
            // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
            #region SLL r - Shift left ?? (undocumented)
                public static Opcode SLL_MIY_B = new Opcode(OpcodeBytes.SLL_MIY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), B", cycles: 23, pseudocode: "B <- (IY+n) << 1; CY <- prev bit 7; bit 0 <- 1;");
                public static Opcode SLL_MIY_C = new Opcode(OpcodeBytes.SLL_MIY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), C", cycles: 23, pseudocode: "C <- (IY+n) << 1; CY <- prev bit 7; bit 0 <- 1;");
                public static Opcode SLL_MIY_D = new Opcode(OpcodeBytes.SLL_MIY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), D", cycles: 23, pseudocode: "D <- (IY+n) << 1; CY <- prev bit 7; bit 0 <- 1;");
                public static Opcode SLL_MIY_E = new Opcode(OpcodeBytes.SLL_MIY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), E", cycles: 23, pseudocode: "E <- (IY+n) << 1; CY <- prev bit 7; bit 0 <- 1;");
                public static Opcode SLL_MIY_H = new Opcode(OpcodeBytes.SLL_MIY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), H", cycles: 23, pseudocode: "H <- (IY+n) << 1; CY <- prev bit 7; bit 0 <- 1;");
                public static Opcode SLL_MIY_L = new Opcode(OpcodeBytes.SLL_MIY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), L", cycles: 23, pseudocode: "L <- (IY+n) << 1; CY <- prev bit 7; bit 0 <- 1;");
                public static Opcode SLL_MIY = new Opcode(OpcodeBytes.SLL_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), H", cycles: 23, pseudocode: "(IY+n) <- (IY+n) << 1; CY <- prev bit 7; bit 0 <- 1;");
                public static Opcode SLL_MIY_A = new Opcode(OpcodeBytes.SLL_MIY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SLL (IY+n), A", cycles: 23, pseudocode: "A <- (IY+n) << 1; CY <- prev bit 7; bit 0 <- 1;");
            #endregion

            #region SRL r - Shift right logical
                public static Opcode SRL_MIY_B = new Opcode(OpcodeBytes.SRL_MIY_B, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), B", cycles: 23, pseudocode: "B <- (IY+n) << 1; CY <- prev bit 0;");
                public static Opcode SRL_MIY_C = new Opcode(OpcodeBytes.SRL_MIY_C, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), C", cycles: 23, pseudocode: "C <- (IY+n) << 1; CY <- prev bit 0;");
                public static Opcode SRL_MIY_D = new Opcode(OpcodeBytes.SRL_MIY_D, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), D", cycles: 23, pseudocode: "D <- (IY+n) << 1; CY <- prev bit 0;");
                public static Opcode SRL_MIY_E = new Opcode(OpcodeBytes.SRL_MIY_E, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), E", cycles: 23, pseudocode: "E <- (IY+n) << 1; CY <- prev bit 0;");
                public static Opcode SRL_MIY_H = new Opcode(OpcodeBytes.SRL_MIY_H, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), H", cycles: 23, pseudocode: "H <- (IY+n) << 1; CY <- prev bit 0;");
                public static Opcode SRL_MIY_L = new Opcode(OpcodeBytes.SRL_MIY_L, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), L", cycles: 23, pseudocode: "L <- (IY+n) << 1; CY <- prev bit 0;");
                public static Opcode SRL_MIY = new Opcode(OpcodeBytes.SRL_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), H", cycles: 23, pseudocode: "(IY+n) <- (IY+n) << 1; CY <- prev bit 0;");
                public static Opcode SRL_MIY_A = new Opcode(OpcodeBytes.SRL_MIY_A, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SRL (IY+n), A", cycles: 23, pseudocode: "A <- (IY+n) << 1; CY <- prev bit 0;");
            #endregion

        #endregion

        #region Test Bit

            public static Opcode BIT_0_MIY_2 = new Opcode(OpcodeBytes.BIT_0_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 0, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit0 == 0");
            public static Opcode BIT_0_MIY_3 = new Opcode(OpcodeBytes.BIT_0_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 0, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit0 == 0");
            public static Opcode BIT_0_MIY_4 = new Opcode(OpcodeBytes.BIT_0_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 0, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit0 == 0");
            public static Opcode BIT_0_MIY_5 = new Opcode(OpcodeBytes.BIT_0_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 0, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit0 == 0");
            public static Opcode BIT_0_MIY_6 = new Opcode(OpcodeBytes.BIT_0_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 0, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit0 == 0");
            public static Opcode BIT_0_MIY_7 = new Opcode(OpcodeBytes.BIT_0_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 0, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit0 == 0");
            public static Opcode BIT_0_MIY = new Opcode(OpcodeBytes.BIT_0_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 0, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit0 == 0");
            public static Opcode BIT_0_MIY_8 = new Opcode(OpcodeBytes.BIT_0_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 0, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit0 == 0");

            public static Opcode BIT_1_MIY_2 = new Opcode(OpcodeBytes.BIT_1_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 1, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit1 == 0");
            public static Opcode BIT_1_MIY_3 = new Opcode(OpcodeBytes.BIT_1_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 1, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit1 == 0");
            public static Opcode BIT_1_MIY_4 = new Opcode(OpcodeBytes.BIT_1_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 1, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit1 == 0");
            public static Opcode BIT_1_MIY_5 = new Opcode(OpcodeBytes.BIT_1_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 1, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit1 == 0");
            public static Opcode BIT_1_MIY_6 = new Opcode(OpcodeBytes.BIT_1_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 1, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit1 == 0");
            public static Opcode BIT_1_MIY_7 = new Opcode(OpcodeBytes.BIT_1_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 1, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit1 == 0");
            public static Opcode BIT_1_MIY = new Opcode(OpcodeBytes.BIT_1_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 1, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit1 == 0");
            public static Opcode BIT_1_MIY_8 = new Opcode(OpcodeBytes.BIT_1_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 1, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit1 == 0");

            public static Opcode BIT_2_MIY_2 = new Opcode(OpcodeBytes.BIT_2_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 2, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit2 == 0");
            public static Opcode BIT_2_MIY_3 = new Opcode(OpcodeBytes.BIT_2_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 2, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit2 == 0");
            public static Opcode BIT_2_MIY_4 = new Opcode(OpcodeBytes.BIT_2_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 2, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit2 == 0");
            public static Opcode BIT_2_MIY_5 = new Opcode(OpcodeBytes.BIT_2_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 2, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit2 == 0");
            public static Opcode BIT_2_MIY_6 = new Opcode(OpcodeBytes.BIT_2_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 2, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit2 == 0");
            public static Opcode BIT_2_MIY_7 = new Opcode(OpcodeBytes.BIT_2_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 2, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit2 == 0");
            public static Opcode BIT_2_MIY = new Opcode(OpcodeBytes.BIT_2_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 2, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit2 == 0");
            public static Opcode BIT_2_MIY_8 = new Opcode(OpcodeBytes.BIT_2_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 2, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit2 == 0");

            public static Opcode BIT_3_MIY_2 = new Opcode(OpcodeBytes.BIT_3_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 3, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit3 == 0");
            public static Opcode BIT_3_MIY_3 = new Opcode(OpcodeBytes.BIT_3_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 3, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit3 == 0");
            public static Opcode BIT_3_MIY_4 = new Opcode(OpcodeBytes.BIT_3_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 3, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit3 == 0");
            public static Opcode BIT_3_MIY_5 = new Opcode(OpcodeBytes.BIT_3_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 3, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit3 == 0");
            public static Opcode BIT_3_MIY_6 = new Opcode(OpcodeBytes.BIT_3_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 3, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit3 == 0");
            public static Opcode BIT_3_MIY_7 = new Opcode(OpcodeBytes.BIT_3_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 3, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit3 == 0");
            public static Opcode BIT_3_MIY = new Opcode(OpcodeBytes.BIT_3_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 3, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit3 == 0");
            public static Opcode BIT_3_MIY_8 = new Opcode(OpcodeBytes.BIT_3_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 3, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit3 == 0");

            public static Opcode BIT_4_MIY_2 = new Opcode(OpcodeBytes.BIT_4_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 4, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit4 == 0");
            public static Opcode BIT_4_MIY_3 = new Opcode(OpcodeBytes.BIT_4_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 4, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit4 == 0");
            public static Opcode BIT_4_MIY_4 = new Opcode(OpcodeBytes.BIT_4_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 4, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit4 == 0");
            public static Opcode BIT_4_MIY_5 = new Opcode(OpcodeBytes.BIT_4_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 4, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit4 == 0");
            public static Opcode BIT_4_MIY_6 = new Opcode(OpcodeBytes.BIT_4_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 4, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit4 == 0");
            public static Opcode BIT_4_MIY_7 = new Opcode(OpcodeBytes.BIT_4_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 4, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit4 == 0");
            public static Opcode BIT_4_MIY = new Opcode(OpcodeBytes.BIT_4_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 4, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit4 == 0");
            public static Opcode BIT_4_MIY_8 = new Opcode(OpcodeBytes.BIT_4_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 4, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit4 == 0");

            public static Opcode BIT_5_MIY_2 = new Opcode(OpcodeBytes.BIT_5_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 5, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit5 == 0");
            public static Opcode BIT_5_MIY_3 = new Opcode(OpcodeBytes.BIT_5_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 5, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit5 == 0");
            public static Opcode BIT_5_MIY_4 = new Opcode(OpcodeBytes.BIT_5_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 5, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit5 == 0");
            public static Opcode BIT_5_MIY_5 = new Opcode(OpcodeBytes.BIT_5_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 5, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit5 == 0");
            public static Opcode BIT_5_MIY_6 = new Opcode(OpcodeBytes.BIT_5_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 5, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit5 == 0");
            public static Opcode BIT_5_MIY_7 = new Opcode(OpcodeBytes.BIT_5_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 5, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit5 == 0");
            public static Opcode BIT_5_MIY = new Opcode(OpcodeBytes.BIT_5_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 5, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit5 == 0");
            public static Opcode BIT_5_MIY_8 = new Opcode(OpcodeBytes.BIT_5_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 5, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit5 == 0");

            public static Opcode BIT_6_MIY_2 = new Opcode(OpcodeBytes.BIT_6_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 6, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit6 == 0");
            public static Opcode BIT_6_MIY_3 = new Opcode(OpcodeBytes.BIT_6_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 6, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit6 == 0");
            public static Opcode BIT_6_MIY_4 = new Opcode(OpcodeBytes.BIT_6_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 6, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit6 == 0");
            public static Opcode BIT_6_MIY_5 = new Opcode(OpcodeBytes.BIT_6_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 6, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit6 == 0");
            public static Opcode BIT_6_MIY_6 = new Opcode(OpcodeBytes.BIT_6_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 6, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit6 == 0");
            public static Opcode BIT_6_MIY_7 = new Opcode(OpcodeBytes.BIT_6_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 6, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit6 == 0");
            public static Opcode BIT_6_MIY = new Opcode(OpcodeBytes.BIT_6_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 6, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit6 == 0");
            public static Opcode BIT_6_MIY_8 = new Opcode(OpcodeBytes.BIT_6_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 6, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit6 == 0");

            public static Opcode BIT_7_MIY_2 = new Opcode(OpcodeBytes.BIT_7_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 7, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit7 == 0");
            public static Opcode BIT_7_MIY_3 = new Opcode(OpcodeBytes.BIT_7_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 7, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit7 == 0");
            public static Opcode BIT_7_MIY_4 = new Opcode(OpcodeBytes.BIT_7_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 7, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit7 == 0");
            public static Opcode BIT_7_MIY_5 = new Opcode(OpcodeBytes.BIT_7_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 7, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit7 == 0");
            public static Opcode BIT_7_MIY_6 = new Opcode(OpcodeBytes.BIT_7_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 7, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit7 == 0");
            public static Opcode BIT_7_MIY_7 = new Opcode(OpcodeBytes.BIT_7_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 7, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit7 == 0");
            public static Opcode BIT_7_MIY = new Opcode(OpcodeBytes.BIT_7_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 7, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit7 == 0");
            public static Opcode BIT_7_MIY_8 = new Opcode(OpcodeBytes.BIT_7_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "BIT 7, (IY+n)", cycles: 20, pseudocode: "Z <- (IY+n) bit7 == 0");

        #endregion

        #region Reset Bit

            public static Opcode RES_0_MIY_2 = new Opcode(OpcodeBytes.RES_0_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 0");
            public static Opcode RES_0_MIY_3 = new Opcode(OpcodeBytes.RES_0_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 0");
            public static Opcode RES_0_MIY_4 = new Opcode(OpcodeBytes.RES_0_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 0");
            public static Opcode RES_0_MIY_5 = new Opcode(OpcodeBytes.RES_0_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 0");
            public static Opcode RES_0_MIY_6 = new Opcode(OpcodeBytes.RES_0_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 0");
            public static Opcode RES_0_MIY_7 = new Opcode(OpcodeBytes.RES_0_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 0");
            public static Opcode RES_0_MIY = new Opcode(OpcodeBytes.RES_0_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (HL) to 0");
            public static Opcode RES_0_MIY_8 = new Opcode(OpcodeBytes.RES_0_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 0");

            public static Opcode RES_1_MIY_2 = new Opcode(OpcodeBytes.RES_1_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 0");
            public static Opcode RES_1_MIY_3 = new Opcode(OpcodeBytes.RES_1_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 0");
            public static Opcode RES_1_MIY_4 = new Opcode(OpcodeBytes.RES_1_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 0");
            public static Opcode RES_1_MIY_5 = new Opcode(OpcodeBytes.RES_1_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 0");
            public static Opcode RES_1_MIY_6 = new Opcode(OpcodeBytes.RES_1_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 0");
            public static Opcode RES_1_MIY_7 = new Opcode(OpcodeBytes.RES_1_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 0");
            public static Opcode RES_1_MIY = new Opcode(OpcodeBytes.RES_1_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (HL) to 0");
            public static Opcode RES_1_MIY_8 = new Opcode(OpcodeBytes.RES_1_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 0");

            public static Opcode RES_2_MIY_2 = new Opcode(OpcodeBytes.RES_2_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 0");
            public static Opcode RES_2_MIY_3 = new Opcode(OpcodeBytes.RES_2_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 0");
            public static Opcode RES_2_MIY_4 = new Opcode(OpcodeBytes.RES_2_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 0");
            public static Opcode RES_2_MIY_5 = new Opcode(OpcodeBytes.RES_2_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 0");
            public static Opcode RES_2_MIY_6 = new Opcode(OpcodeBytes.RES_2_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 0");
            public static Opcode RES_2_MIY_7 = new Opcode(OpcodeBytes.RES_2_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 0");
            public static Opcode RES_2_MIY = new Opcode(OpcodeBytes.RES_2_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (HL) to 0");
            public static Opcode RES_2_MIY_8 = new Opcode(OpcodeBytes.RES_2_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 0");

            public static Opcode RES_3_MIY_2 = new Opcode(OpcodeBytes.RES_3_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 0");
            public static Opcode RES_3_MIY_3 = new Opcode(OpcodeBytes.RES_3_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 0");
            public static Opcode RES_3_MIY_4 = new Opcode(OpcodeBytes.RES_3_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 0");
            public static Opcode RES_3_MIY_5 = new Opcode(OpcodeBytes.RES_3_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 0");
            public static Opcode RES_3_MIY_6 = new Opcode(OpcodeBytes.RES_3_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 0");
            public static Opcode RES_3_MIY_7 = new Opcode(OpcodeBytes.RES_3_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 0");
            public static Opcode RES_3_MIY = new Opcode(OpcodeBytes.RES_3_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (HL) to 0");
            public static Opcode RES_3_MIY_8 = new Opcode(OpcodeBytes.RES_3_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 0");

            public static Opcode RES_4_MIY_2 = new Opcode(OpcodeBytes.RES_4_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 0");
            public static Opcode RES_4_MIY_3 = new Opcode(OpcodeBytes.RES_4_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 0");
            public static Opcode RES_4_MIY_4 = new Opcode(OpcodeBytes.RES_4_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 0");
            public static Opcode RES_4_MIY_5 = new Opcode(OpcodeBytes.RES_4_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 0");
            public static Opcode RES_4_MIY_6 = new Opcode(OpcodeBytes.RES_4_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 0");
            public static Opcode RES_4_MIY_7 = new Opcode(OpcodeBytes.RES_4_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 0");
            public static Opcode RES_4_MIY = new Opcode(OpcodeBytes.RES_4_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (HL) to 0");
            public static Opcode RES_4_MIY_8 = new Opcode(OpcodeBytes.RES_4_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 0");

            public static Opcode RES_5_MIY_2 = new Opcode(OpcodeBytes.RES_5_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 0");
            public static Opcode RES_5_MIY_3 = new Opcode(OpcodeBytes.RES_5_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 0");
            public static Opcode RES_5_MIY_4 = new Opcode(OpcodeBytes.RES_5_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 0");
            public static Opcode RES_5_MIY_5 = new Opcode(OpcodeBytes.RES_5_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 0");
            public static Opcode RES_5_MIY_6 = new Opcode(OpcodeBytes.RES_5_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 0");
            public static Opcode RES_5_MIY_7 = new Opcode(OpcodeBytes.RES_5_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 0");
            public static Opcode RES_5_MIY = new Opcode(OpcodeBytes.RES_5_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (HL) to 0");
            public static Opcode RES_5_MIY_8 = new Opcode(OpcodeBytes.RES_5_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 0");

            public static Opcode RES_6_MIY_2 = new Opcode(OpcodeBytes.RES_6_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 0");
            public static Opcode RES_6_MIY_3 = new Opcode(OpcodeBytes.RES_6_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 0");
            public static Opcode RES_6_MIY_4 = new Opcode(OpcodeBytes.RES_6_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 0");
            public static Opcode RES_6_MIY_5 = new Opcode(OpcodeBytes.RES_6_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 0");
            public static Opcode RES_6_MIY_6 = new Opcode(OpcodeBytes.RES_6_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 0");
            public static Opcode RES_6_MIY_7 = new Opcode(OpcodeBytes.RES_6_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 0");
            public static Opcode RES_6_MIY = new Opcode(OpcodeBytes.RES_6_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (HL) to 0");
            public static Opcode RES_6_MIY_8 = new Opcode(OpcodeBytes.RES_6_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 0");

            public static Opcode RES_7_MIY_2 = new Opcode(OpcodeBytes.RES_7_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 0");
            public static Opcode RES_7_MIY_3 = new Opcode(OpcodeBytes.RES_7_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 0");
            public static Opcode RES_7_MIY_4 = new Opcode(OpcodeBytes.RES_7_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 0");
            public static Opcode RES_7_MIY_5 = new Opcode(OpcodeBytes.RES_7_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 0");
            public static Opcode RES_7_MIY_6 = new Opcode(OpcodeBytes.RES_7_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 0");
            public static Opcode RES_7_MIY_7 = new Opcode(OpcodeBytes.RES_7_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 0");
            public static Opcode RES_7_MIY = new Opcode(OpcodeBytes.RES_7_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (HL) to 0");
            public static Opcode RES_7_MIY_8 = new Opcode(OpcodeBytes.RES_7_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "RES 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 0");

        #endregion

        #region Set Bit

            public static Opcode SET_0_MIY_2 = new Opcode(OpcodeBytes.SET_0_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 1");
            public static Opcode SET_0_MIY_3 = new Opcode(OpcodeBytes.SET_0_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 1");
            public static Opcode SET_0_MIY_4 = new Opcode(OpcodeBytes.SET_0_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 1");
            public static Opcode SET_0_MIY_5 = new Opcode(OpcodeBytes.SET_0_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 1");
            public static Opcode SET_0_MIY_6 = new Opcode(OpcodeBytes.SET_0_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 1");
            public static Opcode SET_0_MIY_7 = new Opcode(OpcodeBytes.SET_0_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 1");
            public static Opcode SET_0_MIY = new Opcode(OpcodeBytes.SET_0_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (HL) to 1");
            public static Opcode SET_0_MIY_8 = new Opcode(OpcodeBytes.SET_0_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 0, (IY+n)", cycles: 23, pseudocode: "Set bit 0 of (IY+n) to 1");

            public static Opcode SET_1_MIY_2 = new Opcode(OpcodeBytes.SET_1_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 1");
            public static Opcode SET_1_MIY_3 = new Opcode(OpcodeBytes.SET_1_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 1");
            public static Opcode SET_1_MIY_4 = new Opcode(OpcodeBytes.SET_1_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 1");
            public static Opcode SET_1_MIY_5 = new Opcode(OpcodeBytes.SET_1_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 1");
            public static Opcode SET_1_MIY_6 = new Opcode(OpcodeBytes.SET_1_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 1");
            public static Opcode SET_1_MIY_7 = new Opcode(OpcodeBytes.SET_1_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 1");
            public static Opcode SET_1_MIY = new Opcode(OpcodeBytes.SET_1_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (HL) to 1");
            public static Opcode SET_1_MIY_8 = new Opcode(OpcodeBytes.SET_1_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 1, (IY+n)", cycles: 23, pseudocode: "Set bit 1 of (IY+n) to 1");

            public static Opcode SET_2_MIY_2 = new Opcode(OpcodeBytes.SET_2_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 1");
            public static Opcode SET_2_MIY_3 = new Opcode(OpcodeBytes.SET_2_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 1");
            public static Opcode SET_2_MIY_4 = new Opcode(OpcodeBytes.SET_2_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 1");
            public static Opcode SET_2_MIY_5 = new Opcode(OpcodeBytes.SET_2_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 1");
            public static Opcode SET_2_MIY_6 = new Opcode(OpcodeBytes.SET_2_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 1");
            public static Opcode SET_2_MIY_7 = new Opcode(OpcodeBytes.SET_2_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 1");
            public static Opcode SET_2_MIY = new Opcode(OpcodeBytes.SET_2_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (HL) to 1");
            public static Opcode SET_2_MIY_8 = new Opcode(OpcodeBytes.SET_2_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 2, (IY+n)", cycles: 23, pseudocode: "Set bit 2 of (IY+n) to 1");

            public static Opcode SET_3_MIY_2 = new Opcode(OpcodeBytes.SET_3_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 1");
            public static Opcode SET_3_MIY_3 = new Opcode(OpcodeBytes.SET_3_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 1");
            public static Opcode SET_3_MIY_4 = new Opcode(OpcodeBytes.SET_3_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 1");
            public static Opcode SET_3_MIY_5 = new Opcode(OpcodeBytes.SET_3_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 1");
            public static Opcode SET_3_MIY_6 = new Opcode(OpcodeBytes.SET_3_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 1");
            public static Opcode SET_3_MIY_7 = new Opcode(OpcodeBytes.SET_3_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 1");
            public static Opcode SET_3_MIY = new Opcode(OpcodeBytes.SET_3_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (HL) to 1");
            public static Opcode SET_3_MIY_8 = new Opcode(OpcodeBytes.SET_3_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 3, (IY+n)", cycles: 23, pseudocode: "Set bit 3 of (IY+n) to 1");

            public static Opcode SET_4_MIY_2 = new Opcode(OpcodeBytes.SET_4_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 1");
            public static Opcode SET_4_MIY_3 = new Opcode(OpcodeBytes.SET_4_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 1");
            public static Opcode SET_4_MIY_4 = new Opcode(OpcodeBytes.SET_4_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 1");
            public static Opcode SET_4_MIY_5 = new Opcode(OpcodeBytes.SET_4_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 1");
            public static Opcode SET_4_MIY_6 = new Opcode(OpcodeBytes.SET_4_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 1");
            public static Opcode SET_4_MIY_7 = new Opcode(OpcodeBytes.SET_4_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 1");
            public static Opcode SET_4_MIY = new Opcode(OpcodeBytes.SET_4_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (HL) to 1");
            public static Opcode SET_4_MIY_8 = new Opcode(OpcodeBytes.SET_4_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 4, (IY+n)", cycles: 23, pseudocode: "Set bit 4 of (IY+n) to 1");

            public static Opcode SET_5_MIY_2 = new Opcode(OpcodeBytes.SET_5_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 1");
            public static Opcode SET_5_MIY_3 = new Opcode(OpcodeBytes.SET_5_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 1");
            public static Opcode SET_5_MIY_4 = new Opcode(OpcodeBytes.SET_5_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 1");
            public static Opcode SET_5_MIY_5 = new Opcode(OpcodeBytes.SET_5_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 1");
            public static Opcode SET_5_MIY_6 = new Opcode(OpcodeBytes.SET_5_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 1");
            public static Opcode SET_5_MIY_7 = new Opcode(OpcodeBytes.SET_5_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 1");
            public static Opcode SET_5_MIY = new Opcode(OpcodeBytes.SET_5_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (HL) to 1");
            public static Opcode SET_5_MIY_8 = new Opcode(OpcodeBytes.SET_5_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 5, (IY+n)", cycles: 23, pseudocode: "Set bit 5 of (IY+n) to 1");

            public static Opcode SET_6_MIY_2 = new Opcode(OpcodeBytes.SET_6_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 1");
            public static Opcode SET_6_MIY_3 = new Opcode(OpcodeBytes.SET_6_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 1");
            public static Opcode SET_6_MIY_4 = new Opcode(OpcodeBytes.SET_6_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 1");
            public static Opcode SET_6_MIY_5 = new Opcode(OpcodeBytes.SET_6_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 1");
            public static Opcode SET_6_MIY_6 = new Opcode(OpcodeBytes.SET_6_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 1");
            public static Opcode SET_6_MIY_7 = new Opcode(OpcodeBytes.SET_6_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 1");
            public static Opcode SET_6_MIY = new Opcode(OpcodeBytes.SET_6_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (HL) to 1");
            public static Opcode SET_6_MIY_8 = new Opcode(OpcodeBytes.SET_6_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 6, (IY+n)", cycles: 23, pseudocode: "Set bit 6 of (IY+n) to 1");

            public static Opcode SET_7_MIY_2 = new Opcode(OpcodeBytes.SET_7_MIY_2, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 1");
            public static Opcode SET_7_MIY_3 = new Opcode(OpcodeBytes.SET_7_MIY_3, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 1");
            public static Opcode SET_7_MIY_4 = new Opcode(OpcodeBytes.SET_7_MIY_4, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 1");
            public static Opcode SET_7_MIY_5 = new Opcode(OpcodeBytes.SET_7_MIY_5, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 1");
            public static Opcode SET_7_MIY_6 = new Opcode(OpcodeBytes.SET_7_MIY_6, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 1");
            public static Opcode SET_7_MIY_7 = new Opcode(OpcodeBytes.SET_7_MIY_7, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 1");
            public static Opcode SET_7_MIY = new Opcode(OpcodeBytes.SET_7_MIY, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (HL) to 1");
            public static Opcode SET_7_MIY_8 = new Opcode(OpcodeBytes.SET_7_MIY_8, instructionSet: InstructionSet.IYBit, size: 4, instruction: "SET 7, (IY+n)", cycles: 23, pseudocode: "Set bit 7 of (IY+n) to 1");

        #endregion
    }
}
