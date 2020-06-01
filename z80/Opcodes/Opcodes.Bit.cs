
namespace JustinCredible.ZilogZ80
{
    // A list of all the "bit" opcodes and their metadata.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_BIT (0xCB).
    public partial class Opcodes
    {
        #region Rotate

            #region RLC r - Rotate left
                public static Opcode RLC_B = new Opcode(OpcodeBytes.RLC_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "RLC B", cycles: 8, pseudocode: "B = B << 1; bit 0 = prev bit 7; CY = prev bit 7");
                public static Opcode RLC_C = new Opcode(OpcodeBytes.RLC_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "RLC C", cycles: 8, pseudocode: "C = C << 1; bit 0 = prev bit 7; CY = prev bit 7");
                public static Opcode RLC_D = new Opcode(OpcodeBytes.RLC_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "RLC D", cycles: 8, pseudocode: "D = D << 1; bit 0 = prev bit 7; CY = prev bit 7");
                public static Opcode RLC_E = new Opcode(OpcodeBytes.RLC_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "RLC E", cycles: 8, pseudocode: "E = E << 1; bit 0 = prev bit 7; CY = prev bit 7");
                public static Opcode RLC_H = new Opcode(OpcodeBytes.RLC_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "RLC H", cycles: 8, pseudocode: "H = H << 1; bit 0 = prev bit 7; CY = prev bit 7");
                public static Opcode RLC_L = new Opcode(OpcodeBytes.RLC_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "RLC L", cycles: 8, pseudocode: "L = L << 1; bit 0 = prev bit 7; CY = prev bit 7");
                public static Opcode RLC_MHL = new Opcode(OpcodeBytes.RLC_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "RLC (HL)", cycles: 15, pseudocode: "(HL) = (HL) << 1; bit 0 = prev bit 7; CY = prev bit 7");
                public static Opcode RLC_A = new Opcode(OpcodeBytes.RLC_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "RLC A", cycles: 8, pseudocode: "A = A << 1; bit 0 = prev bit 7; CY = prev bit 7");
            #endregion

            #region RRC r - Rotate right
                public static Opcode RRC_B = new Opcode(OpcodeBytes.RRC_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "RRC B", cycles: 8, pseudocode: "B = B >> 1; bit 7 = prev bit 0; CY = prev bit 0");
                public static Opcode RRC_C = new Opcode(OpcodeBytes.RRC_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "RRC C", cycles: 8, pseudocode: "C = C >> 1; bit 7 = prev bit 0; CY = prev bit 0");
                public static Opcode RRC_D = new Opcode(OpcodeBytes.RRC_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "RRC D", cycles: 8, pseudocode: "D = D >> 1; bit 7 = prev bit 0; CY = prev bit 0");
                public static Opcode RRC_E = new Opcode(OpcodeBytes.RRC_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "RRC E", cycles: 8, pseudocode: "E = E >> 1; bit 7 = prev bit 0; CY = prev bit 0");
                public static Opcode RRC_H = new Opcode(OpcodeBytes.RRC_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "RRC H", cycles: 8, pseudocode: "H = H >> 1; bit 7 = prev bit 0; CY = prev bit 0");
                public static Opcode RRC_L = new Opcode(OpcodeBytes.RRC_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "RRC L", cycles: 8, pseudocode: "L = L >> 1; bit 7 = prev bit 0; CY = prev bit 0");
                public static Opcode RRC_MHL = new Opcode(OpcodeBytes.RRC_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "RRC (HL)", cycles: 15, pseudocode: "(HL) = (HL) >> 1; bit 7 = prev bit 0; CY = prev bit 0");
                public static Opcode RRC_A = new Opcode(OpcodeBytes.RRC_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "RRC A", cycles: 8, pseudocode: "A = A >> 1; bit 7 = prev bit 0; CY = prev bit 0");
            #endregion

            #region RL r - Rotate left through carry
                public static Opcode RL_B = new Opcode(OpcodeBytes.RL_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "RL B", cycles: 8, pseudocode: "B = B << 1; bit 0 = prev CY; CY = prev bit 7");
                public static Opcode RL_C = new Opcode(OpcodeBytes.RL_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "RL C", cycles: 8, pseudocode: "C = C << 1; bit 0 = prev CY; CY = prev bit 7");
                public static Opcode RL_D = new Opcode(OpcodeBytes.RL_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "RL D", cycles: 8, pseudocode: "D = D << 1; bit 0 = prev CY; CY = prev bit 7");
                public static Opcode RL_E = new Opcode(OpcodeBytes.RL_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "RL E", cycles: 8, pseudocode: "E = E << 1; bit 0 = prev CY; CY = prev bit 7");
                public static Opcode RL_H = new Opcode(OpcodeBytes.RL_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "RL H", cycles: 8, pseudocode: "H = H << 1; bit 0 = prev CY; CY = prev bit 7");
                public static Opcode RL_L = new Opcode(OpcodeBytes.RL_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "RL L", cycles: 8, pseudocode: "L = L << 1; bit 0 = prev CY; CY = prev bit 7");
                public static Opcode RL_MHL = new Opcode(OpcodeBytes.RL_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "RL (HL)", cycles: 15, pseudocode: "(HL) = (HL) << 1; bit 0 = prev CY; CY = prev bit 7");
                public static Opcode RL_A = new Opcode(OpcodeBytes.RL_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "RL A", cycles: 8, pseudocode: "A = A << 1; bit 0 = prev CY; CY = prev bit 7");
            #endregion

            #region RR r - Rotate right through carry
                public static Opcode RR_B = new Opcode(OpcodeBytes.RR_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "RR B", cycles: 8, pseudocode: "B = B >> 1; bit 7 = prev bit 7; CY = prev bit 0");
                public static Opcode RR_C = new Opcode(OpcodeBytes.RR_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "RR C", cycles: 8, pseudocode: "C = C >> 1; bit 7 = prev bit 7; CY = prev bit 0");
                public static Opcode RR_D = new Opcode(OpcodeBytes.RR_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "RR D", cycles: 8, pseudocode: "D = D >> 1; bit 7 = prev bit 7; CY = prev bit 0");
                public static Opcode RR_E = new Opcode(OpcodeBytes.RR_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "RR E", cycles: 8, pseudocode: "E = E >> 1; bit 7 = prev bit 7; CY = prev bit 0");
                public static Opcode RR_H = new Opcode(OpcodeBytes.RR_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "RR H", cycles: 8, pseudocode: "H = H >> 1; bit 7 = prev bit 7; CY = prev bit 0");
                public static Opcode RR_L = new Opcode(OpcodeBytes.RR_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "RR L", cycles: 8, pseudocode: "L = L >> 1; bit 7 = prev bit 7; CY = prev bit 0");
                public static Opcode RR_MHL = new Opcode(OpcodeBytes.RR_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "RR (HL)", cycles: 15, pseudocode: "(HL) = (HL) >> 1; bit 7 = prev bit 7; CY = prev bit 0");
                public static Opcode RR_A = new Opcode(OpcodeBytes.RR_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "RR A", cycles: 8, pseudocode: "A = A >> 1; bit 7 = prev bit 7; CY = prev bit 0");
            #endregion

        #endregion

        #region Shift

            #region SLA r - Shift left (arithmetic)
                public static Opcode SLA_B = new Opcode(OpcodeBytes.SLA_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLA B", cycles: 8, pseudocode: "B = B >> 1; CY = prev bit 7");
                public static Opcode SLA_C = new Opcode(OpcodeBytes.SLA_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLA C", cycles: 8, pseudocode: "C = C >> 1; CY = prev bit 7");
                public static Opcode SLA_D = new Opcode(OpcodeBytes.SLA_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLA D", cycles: 8, pseudocode: "D = D >> 1; CY = prev bit 7");
                public static Opcode SLA_E = new Opcode(OpcodeBytes.SLA_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLA E", cycles: 8, pseudocode: "E = E >> 1; CY = prev bit 7");
                public static Opcode SLA_H = new Opcode(OpcodeBytes.SLA_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLA H", cycles: 8, pseudocode: "H = H >> 1; CY = prev bit 7");
                public static Opcode SLA_L = new Opcode(OpcodeBytes.SLA_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLA L", cycles: 8, pseudocode: "L = L >> 1; CY = prev bit 7");
                public static Opcode SLA_MHL = new Opcode(OpcodeBytes.SLA_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLA (HL)", cycles: 15, pseudocode: "HL = HL >> 1; CY = prev bit 7");
                public static Opcode SLA_A = new Opcode(OpcodeBytes.SLA_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLA A", cycles: 8, pseudocode: "A = A >> 1; CY = prev bit 7");
            #endregion

            #region SRA r - Shift right (arithmetic)
                public static Opcode SRA_B = new Opcode(OpcodeBytes.SRA_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRA B", cycles: 8, pseudocode: "B = B >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_C = new Opcode(OpcodeBytes.SRA_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRA C", cycles: 8, pseudocode: "C = C >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_D = new Opcode(OpcodeBytes.SRA_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRA D", cycles: 8, pseudocode: "D = D >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_E = new Opcode(OpcodeBytes.SRA_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRA E", cycles: 8, pseudocode: "E = E >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_H = new Opcode(OpcodeBytes.SRA_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRA H", cycles: 8, pseudocode: "H = H >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_L = new Opcode(OpcodeBytes.SRA_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRA L", cycles: 8, pseudocode: "L = L >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_MHL = new Opcode(OpcodeBytes.SRA_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRA (HL)", cycles: 15, pseudocode: "HL = HL >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
                public static Opcode SRA_A = new Opcode(OpcodeBytes.SRA_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRA A", cycles: 8, pseudocode: "A = A >> 1; CY = prev bit 0; bit 7 = prev bit 7;");
            #endregion

            // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
            // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
            #region SLL r - Shift left ?? (undocumented)
                public static Opcode SLL_B = new Opcode(OpcodeBytes.SLL_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLL B", cycles: 8, pseudocode: "B = B << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_C = new Opcode(OpcodeBytes.SLL_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLL C", cycles: 8, pseudocode: "C = C << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_D = new Opcode(OpcodeBytes.SLL_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLL D", cycles: 8, pseudocode: "D = D << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_E = new Opcode(OpcodeBytes.SLL_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLL E", cycles: 8, pseudocode: "E = E << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_H = new Opcode(OpcodeBytes.SLL_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLL H", cycles: 8, pseudocode: "H = H << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_L = new Opcode(OpcodeBytes.SLL_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLL L", cycles: 8, pseudocode: "L = L << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_MHL = new Opcode(OpcodeBytes.SLL_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLL (HL)", cycles: 15, pseudocode: "HL = HL << 1; CY = prev bit 7; bit 0 = 1;");
                public static Opcode SLL_A = new Opcode(OpcodeBytes.SLL_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "SLL A", cycles: 8, pseudocode: "A = A << 1; CY = prev bit 7; bit 0 = 1;");
            #endregion

            #region SRL r - Shift right logical
                public static Opcode SRL_B = new Opcode(OpcodeBytes.SRL_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRL B", cycles: 8, pseudocode: "B = B << 1; CY = prev bit 0;");
                public static Opcode SRL_C = new Opcode(OpcodeBytes.SRL_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRL C", cycles: 8, pseudocode: "C = C << 1; CY = prev bit 0;");
                public static Opcode SRL_D = new Opcode(OpcodeBytes.SRL_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRL D", cycles: 8, pseudocode: "D = D << 1; CY = prev bit 0;");
                public static Opcode SRL_E = new Opcode(OpcodeBytes.SRL_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRL E", cycles: 8, pseudocode: "E = E << 1; CY = prev bit 0;");
                public static Opcode SRL_H = new Opcode(OpcodeBytes.SRL_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRL H", cycles: 8, pseudocode: "H = H << 1; CY = prev bit 0;");
                public static Opcode SRL_L = new Opcode(OpcodeBytes.SRL_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRL L", cycles: 8, pseudocode: "L = L << 1; CY = prev bit 0;");
                public static Opcode SRL_MHL = new Opcode(OpcodeBytes.SRL_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRL (HL)", cycles: 15, pseudocode: "HL = HL << 1; CY = prev bit 0;");
                public static Opcode SRL_A = new Opcode(OpcodeBytes.SRL_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "SRL A", cycles: 8, pseudocode: "A = A << 1; CY = prev bit 0;");
            #endregion

        #endregion

        #region Test Bit

            public static Opcode BIT_0_B = new Opcode(OpcodeBytes.BIT_0_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 0, B", cycles: 8, pseudocode: "Z = bit0 == 0");
            public static Opcode BIT_0_C = new Opcode(OpcodeBytes.BIT_0_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 0, C", cycles: 8, pseudocode: "Z = bit0 == 0");
            public static Opcode BIT_0_D = new Opcode(OpcodeBytes.BIT_0_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 0, D", cycles: 8, pseudocode: "Z = bit0 == 0");
            public static Opcode BIT_0_E = new Opcode(OpcodeBytes.BIT_0_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 0, E", cycles: 8, pseudocode: "Z = bit0 == 0");
            public static Opcode BIT_0_H = new Opcode(OpcodeBytes.BIT_0_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 0, H", cycles: 8, pseudocode: "Z = bit0 == 0");
            public static Opcode BIT_0_L = new Opcode(OpcodeBytes.BIT_0_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 0, L", cycles: 8, pseudocode: "Z = bit0 == 0");
            public static Opcode BIT_0_MHL = new Opcode(OpcodeBytes.BIT_0_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 0, (HL)", cycles: 12, pseudocode: "Z = bit0 == 0");
            public static Opcode BIT_0_A = new Opcode(OpcodeBytes.BIT_0_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 0, A", cycles: 8, pseudocode: "Z = bit0 == 0");

            public static Opcode BIT_1_B = new Opcode(OpcodeBytes.BIT_1_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 1, B", cycles: 8, pseudocode: "Z = bit1 == 0");
            public static Opcode BIT_1_C = new Opcode(OpcodeBytes.BIT_1_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 1, C", cycles: 8, pseudocode: "Z = bit1 == 0");
            public static Opcode BIT_1_D = new Opcode(OpcodeBytes.BIT_1_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 1, D", cycles: 8, pseudocode: "Z = bit1 == 0");
            public static Opcode BIT_1_E = new Opcode(OpcodeBytes.BIT_1_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 1, E", cycles: 8, pseudocode: "Z = bit1 == 0");
            public static Opcode BIT_1_H = new Opcode(OpcodeBytes.BIT_1_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 1, H", cycles: 8, pseudocode: "Z = bit1 == 0");
            public static Opcode BIT_1_L = new Opcode(OpcodeBytes.BIT_1_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 1, L", cycles: 8, pseudocode: "Z = bit1 == 0");
            public static Opcode BIT_1_MHL = new Opcode(OpcodeBytes.BIT_1_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 1, (HL)", cycles: 12, pseudocode: "Z = bit1 == 0");
            public static Opcode BIT_1_A = new Opcode(OpcodeBytes.BIT_1_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 1, A", cycles: 8, pseudocode: "Z = bit1 == 0");

            public static Opcode BIT_2_B = new Opcode(OpcodeBytes.BIT_2_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 2, B", cycles: 8, pseudocode: "Z = bit2 == 0");
            public static Opcode BIT_2_C = new Opcode(OpcodeBytes.BIT_2_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 2, C", cycles: 8, pseudocode: "Z = bit2 == 0");
            public static Opcode BIT_2_D = new Opcode(OpcodeBytes.BIT_2_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 2, D", cycles: 8, pseudocode: "Z = bit2 == 0");
            public static Opcode BIT_2_E = new Opcode(OpcodeBytes.BIT_2_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 2, E", cycles: 8, pseudocode: "Z = bit2 == 0");
            public static Opcode BIT_2_H = new Opcode(OpcodeBytes.BIT_2_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 2, H", cycles: 8, pseudocode: "Z = bit2 == 0");
            public static Opcode BIT_2_L = new Opcode(OpcodeBytes.BIT_2_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 2, L", cycles: 8, pseudocode: "Z = bit2 == 0");
            public static Opcode BIT_2_MHL = new Opcode(OpcodeBytes.BIT_2_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 2, (HL)", cycles: 12, pseudocode: "Z = bit2 == 0");
            public static Opcode BIT_2_A = new Opcode(OpcodeBytes.BIT_2_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 2, A", cycles: 8, pseudocode: "Z = bit2 == 0");

            public static Opcode BIT_3_B = new Opcode(OpcodeBytes.BIT_3_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 3, B", cycles: 8, pseudocode: "Z = bit3 == 0");
            public static Opcode BIT_3_C = new Opcode(OpcodeBytes.BIT_3_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 3, C", cycles: 8, pseudocode: "Z = bit3 == 0");
            public static Opcode BIT_3_D = new Opcode(OpcodeBytes.BIT_3_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 3, D", cycles: 8, pseudocode: "Z = bit3 == 0");
            public static Opcode BIT_3_E = new Opcode(OpcodeBytes.BIT_3_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 3, E", cycles: 8, pseudocode: "Z = bit3 == 0");
            public static Opcode BIT_3_H = new Opcode(OpcodeBytes.BIT_3_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 3, H", cycles: 8, pseudocode: "Z = bit3 == 0");
            public static Opcode BIT_3_L = new Opcode(OpcodeBytes.BIT_3_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 3, L", cycles: 8, pseudocode: "Z = bit3 == 0");
            public static Opcode BIT_3_MHL = new Opcode(OpcodeBytes.BIT_3_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 3, (HL)", cycles: 12, pseudocode: "Z = bit3 == 0");
            public static Opcode BIT_3_A = new Opcode(OpcodeBytes.BIT_3_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 3, A", cycles: 8, pseudocode: "Z = bit3 == 0");

            public static Opcode BIT_4_B = new Opcode(OpcodeBytes.BIT_4_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 4, B", cycles: 8, pseudocode: "Z = bit4 == 0");
            public static Opcode BIT_4_C = new Opcode(OpcodeBytes.BIT_4_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 4, C", cycles: 8, pseudocode: "Z = bit4 == 0");
            public static Opcode BIT_4_D = new Opcode(OpcodeBytes.BIT_4_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 4, D", cycles: 8, pseudocode: "Z = bit4 == 0");
            public static Opcode BIT_4_E = new Opcode(OpcodeBytes.BIT_4_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 4, E", cycles: 8, pseudocode: "Z = bit4 == 0");
            public static Opcode BIT_4_H = new Opcode(OpcodeBytes.BIT_4_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 4, H", cycles: 8, pseudocode: "Z = bit4 == 0");
            public static Opcode BIT_4_L = new Opcode(OpcodeBytes.BIT_4_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 4, L", cycles: 8, pseudocode: "Z = bit4 == 0");
            public static Opcode BIT_4_MHL = new Opcode(OpcodeBytes.BIT_4_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 4, (HL)", cycles: 12, pseudocode: "Z = bit4 == 0");
            public static Opcode BIT_4_A = new Opcode(OpcodeBytes.BIT_4_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 4, A", cycles: 8, pseudocode: "Z = bit4 == 0");

            public static Opcode BIT_5_B = new Opcode(OpcodeBytes.BIT_5_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 5, B", cycles: 8, pseudocode: "Z = bit5 == 0");
            public static Opcode BIT_5_C = new Opcode(OpcodeBytes.BIT_5_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 5, C", cycles: 8, pseudocode: "Z = bit5 == 0");
            public static Opcode BIT_5_D = new Opcode(OpcodeBytes.BIT_5_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 5, D", cycles: 8, pseudocode: "Z = bit5 == 0");
            public static Opcode BIT_5_E = new Opcode(OpcodeBytes.BIT_5_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 5, E", cycles: 8, pseudocode: "Z = bit5 == 0");
            public static Opcode BIT_5_H = new Opcode(OpcodeBytes.BIT_5_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 5, H", cycles: 8, pseudocode: "Z = bit5 == 0");
            public static Opcode BIT_5_L = new Opcode(OpcodeBytes.BIT_5_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 5, L", cycles: 8, pseudocode: "Z = bit5 == 0");
            public static Opcode BIT_5_MHL = new Opcode(OpcodeBytes.BIT_5_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 5, (HL)", cycles: 12, pseudocode: "Z = bit5 == 0");
            public static Opcode BIT_5_A = new Opcode(OpcodeBytes.BIT_5_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 5, A", cycles: 8, pseudocode: "Z = bit5 == 0");

            public static Opcode BIT_6_B = new Opcode(OpcodeBytes.BIT_6_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 6, B", cycles: 8, pseudocode: "Z = bit6 == 0");
            public static Opcode BIT_6_C = new Opcode(OpcodeBytes.BIT_6_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 6, C", cycles: 8, pseudocode: "Z = bit6 == 0");
            public static Opcode BIT_6_D = new Opcode(OpcodeBytes.BIT_6_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 6, D", cycles: 8, pseudocode: "Z = bit6 == 0");
            public static Opcode BIT_6_E = new Opcode(OpcodeBytes.BIT_6_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 6, E", cycles: 8, pseudocode: "Z = bit6 == 0");
            public static Opcode BIT_6_H = new Opcode(OpcodeBytes.BIT_6_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 6, H", cycles: 8, pseudocode: "Z = bit6 == 0");
            public static Opcode BIT_6_L = new Opcode(OpcodeBytes.BIT_6_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 6, L", cycles: 8, pseudocode: "Z = bit6 == 0");
            public static Opcode BIT_6_MHL = new Opcode(OpcodeBytes.BIT_6_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 6, (HL)", cycles: 12, pseudocode: "Z = bit6 == 0");
            public static Opcode BIT_6_A = new Opcode(OpcodeBytes.BIT_6_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 6, A", cycles: 8, pseudocode: "Z = bit6 == 0");

            public static Opcode BIT_7_B = new Opcode(OpcodeBytes.BIT_7_B, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 7, B", cycles: 8, pseudocode: "Z = bit7 == 0");
            public static Opcode BIT_7_C = new Opcode(OpcodeBytes.BIT_7_C, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 7, C", cycles: 8, pseudocode: "Z = bit7 == 0");
            public static Opcode BIT_7_D = new Opcode(OpcodeBytes.BIT_7_D, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 7, D", cycles: 8, pseudocode: "Z = bit7 == 0");
            public static Opcode BIT_7_E = new Opcode(OpcodeBytes.BIT_7_E, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 7, E", cycles: 8, pseudocode: "Z = bit7 == 0");
            public static Opcode BIT_7_H = new Opcode(OpcodeBytes.BIT_7_H, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 7, H", cycles: 8, pseudocode: "Z = bit7 == 0");
            public static Opcode BIT_7_L = new Opcode(OpcodeBytes.BIT_7_L, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 7, L", cycles: 8, pseudocode: "Z = bit7 == 0");
            public static Opcode BIT_7_MHL = new Opcode(OpcodeBytes.BIT_7_MHL, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 7, (HL)", cycles: 12, pseudocode: "Z = bit7 == 0");
            public static Opcode BIT_7_A = new Opcode(OpcodeBytes.BIT_7_A, instructionSet: InstructionSet.Bit, size: 2, instruction: "BIT 7, A", cycles: 8, pseudocode: "Z = bit7 == 0");

        #endregion
    }
}
