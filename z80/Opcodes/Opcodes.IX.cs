
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IX" opcodes and their metadata.
    // These are multi-byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IX (0xDD).
    public partial class Opcodes
    {
        #region Add

            #region ADD IX, rr
                public static Opcode ADD_IX_BC = new Opcode(OpcodeBytes.ADD_IX_BC, instructionSet: InstructionSet.IX, size: 2, instruction: "ADD IX, BC", cycles: 15, pseudocode: "IX = IX + BC");
                public static Opcode ADD_IX_DE = new Opcode(OpcodeBytes.ADD_IX_DE, instructionSet: InstructionSet.IX, size: 2, instruction: "ADD IX, DE", cycles: 15, pseudocode: "IX = IX + DE");
                public static Opcode ADD_IX_IX = new Opcode(OpcodeBytes.ADD_IX_IX, instructionSet: InstructionSet.IX, size: 2, instruction: "ADD IX, IX", cycles: 15, pseudocode: "IX = IX + IX");
                public static Opcode ADD_IX_SP = new Opcode(OpcodeBytes.ADD_IX_SP, instructionSet: InstructionSet.IX, size: 2, instruction: "ADD IX, SP", cycles: 15, pseudocode: "IX = IX + SP");
            #endregion

            #region ADD A, (IX+n)
                public static Opcode ADD_A_IXH = new Opcode(OpcodeBytes.ADD_A_IXH, instructionSet: InstructionSet.IX, size: 2, instruction: "ADD A, (IX+H)", cycles: 8, pseudocode: "A = A + (IX+H)");
                public static Opcode ADD_A_IXL = new Opcode(OpcodeBytes.ADD_A_IXL, instructionSet: InstructionSet.IX, size: 2, instruction: "ADD A, (IX+L)", cycles: 8, pseudocode: "A = A + (IX+L)");
                public static Opcode ADD_A_IX = new Opcode(OpcodeBytes.ADD_A_IX, instructionSet: InstructionSet.IX, size: 3, instruction: "ADD A, (IX+n)", cycles: 19, pseudocode: "A = A + (IX+n)");
            #endregion

        #endregion
    }
}
