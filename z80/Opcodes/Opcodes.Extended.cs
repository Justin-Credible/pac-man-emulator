
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

        #endregion
    }
}
