
namespace JustinCredible.ZilogZ80
{
    // A list of all of the "standard" opcodes and their metadata.
    // These are all two byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_EXTENDED (0xED).
    public partial class Opcodes
    {

        #region Input/Output Instructions

            // Write to device at address as specified in register C.

            // Device[C] <- R
            public static Opcode OUT_MC_A = new Opcode(OpcodeBytes.OUT_MC_A, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUT (C), A", cycles: 12, pseudocode: "Device[C] <- A");
            public static Opcode OUT_MC_B = new Opcode(OpcodeBytes.OUT_MC_B, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUT (C), B", cycles: 12, pseudocode: "Device[C] <- B");
            public static Opcode OUT_MC_C = new Opcode(OpcodeBytes.OUT_MC_C, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUT (C), C", cycles: 12, pseudocode: "Device[C] <- C");
            public static Opcode OUT_MC_D = new Opcode(OpcodeBytes.OUT_MC_D, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUT (C), D", cycles: 12, pseudocode: "Device[C] <- D");
            public static Opcode OUT_MC_E = new Opcode(OpcodeBytes.OUT_MC_E, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUT (C), E", cycles: 12, pseudocode: "Device[C] <- E");
            public static Opcode OUT_MC_H = new Opcode(OpcodeBytes.OUT_MC_H, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUT (C), H", cycles: 12, pseudocode: "Device[C] <- H");
            public static Opcode OUT_MC_L = new Opcode(OpcodeBytes.OUT_MC_L, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUT (C), L", cycles: 12, pseudocode: "Device[C] <- L");
            public static Opcode OUT_MC_0 = new Opcode(OpcodeBytes.OUT_MC_0, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUT (C), 0", cycles: 12, pseudocode: "Device[C] <- 0");

            /* Device[C] <- (HL); HL++; B--; */
            public static Opcode OUTI = new Opcode(OpcodeBytes.OUTI, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUTI", cycles: 16, pseudocode: "Device[C] <- (HL); HL++; B--;");

            /* Device[C] <- (HL); HL--; B--; */
            public static Opcode OUTD = new Opcode(OpcodeBytes.OUTD, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUTD", cycles: 16, pseudocode: "Device[C] <- (HL); HL--; B--;");

            // R <- Device[C]
            public static Opcode IN_A_MC = new Opcode(OpcodeBytes.IN_A_MC, instructionSet: InstructionSet.Extended, size: 2, instruction: "IN A, (C)", cycles: 12, pseudocode: "A <- Device[C]");
            public static Opcode IN_B_MC = new Opcode(OpcodeBytes.IN_B_MC, instructionSet: InstructionSet.Extended, size: 2, instruction: "IN B, (C)", cycles: 12, pseudocode: "B <- Device[C]");
            public static Opcode IN_C_MC = new Opcode(OpcodeBytes.IN_C_MC, instructionSet: InstructionSet.Extended, size: 2, instruction: "IN C, (C)", cycles: 12, pseudocode: "C <- Device[C]");
            public static Opcode IN_D_MC = new Opcode(OpcodeBytes.IN_D_MC, instructionSet: InstructionSet.Extended, size: 2, instruction: "IN D, (C)", cycles: 12, pseudocode: "D <- Device[C]");
            public static Opcode IN_E_MC = new Opcode(OpcodeBytes.IN_E_MC, instructionSet: InstructionSet.Extended, size: 2, instruction: "IN E, (C)", cycles: 12, pseudocode: "E <- Device[C]");
            public static Opcode IN_H_MC = new Opcode(OpcodeBytes.IN_H_MC, instructionSet: InstructionSet.Extended, size: 2, instruction: "IN H, (C)", cycles: 12, pseudocode: "H <- Device[C]");
            public static Opcode IN_L_MC = new Opcode(OpcodeBytes.IN_L_MC, instructionSet: InstructionSet.Extended, size: 2, instruction: "IN L, (C)", cycles: 12, pseudocode: "L <- Device[C]");
            public static Opcode IN_MC = new Opcode(OpcodeBytes.IN_MC, instructionSet: InstructionSet.Extended, size: 2, instruction: "IN (C)", cycles: 12, pseudocode: "0 <- Device[C]");

            /* (HL) <- Device[C]; HL++; B--; */
            public static Opcode INI = new Opcode(OpcodeBytes.INI, instructionSet: InstructionSet.Extended, size: 2, instruction: "INI", cycles: 16, pseudocode: "(HL) <- Device[C]; HL++; B--;");

            /* (HL) <- Device[C]; HL--; B--; */
            public static Opcode IND = new Opcode(OpcodeBytes.IND, instructionSet: InstructionSet.Extended, size: 2, instruction: "IND", cycles: 16, pseudocode: "(HL) <- Device[C]; HL--; B--;");

        #endregion
    }
}
