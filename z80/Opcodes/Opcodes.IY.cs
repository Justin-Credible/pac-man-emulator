
namespace JustinCredible.ZilogZ80
{
    // A list of all the "IY" opcodes and their metadata.
    // These are multi-byte opcodes, where the first byte is defined by OpcodeBytes.PREAMBLE_IY (0xDD).
    public partial class Opcodes
    {
        public static Opcode JP_IY = new Opcode(OpcodeBytes.JP_IY, instructionSet: InstructionSet.IY, size: 2, instruction: "JP (IY)", cycles: 8, pseudocode: "PC <- IY");

        #region Increment/Decrement

            public static Opcode INC_IY = new Opcode(OpcodeBytes.INC_IY, instructionSet: InstructionSet.IY, size: 2, instruction: "INC IY", cycles: 10, pseudocode: "IY++");
            public static Opcode DEC_IY = new Opcode(OpcodeBytes.DEC_IY, instructionSet: InstructionSet.IY, size: 2, instruction: "DEC IY", cycles: 10, pseudocode: "IY--");

            public static Opcode INC_IYH = new Opcode(OpcodeBytes.INC_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "INC IYH", cycles: 8, pseudocode: "IYH++");
            public static Opcode DEC_IYH = new Opcode(OpcodeBytes.DEC_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "DEC IYH", cycles: 8, pseudocode: "IYH--");

            public static Opcode INC_IYL = new Opcode(OpcodeBytes.INC_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "INC IYL", cycles: 8, pseudocode: "IYL++");
            public static Opcode DEC_IYL = new Opcode(OpcodeBytes.DEC_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "DEC IYL", cycles: 8, pseudocode: "IYL--");

            public static Opcode INC_MIY = new Opcode(OpcodeBytes.INC_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "INC (IY+n)", cycles: 23, pseudocode: "(IY+n)++");
            public static Opcode DEC_MIY = new Opcode(OpcodeBytes.DEC_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "DEC (IY+n)", cycles: 23, pseudocode: "(IY+n)--");

        #endregion

        #region Stack Operations

            public static Opcode POP_IY = new Opcode(OpcodeBytes.POP_IY, instructionSet: InstructionSet.IY, size: 2, instruction: "POP IY", cycles: 14, pseudocode: "IYL <- (sp); IYH <- (sp + 1); sp <- sp + 2");
            public static Opcode PUSH_IY = new Opcode(OpcodeBytes.PUSH_IY, instructionSet: InstructionSet.IY, size: 2, instruction: "PUSH IY", cycles: 15, pseudocode: "(sp - 2) <-IYL; (sp - 1) <- IYH; sp <- sp - 2");
            public static Opcode EX_MSP_IY = new Opcode(OpcodeBytes.EX_MSP_IY, instructionSet: InstructionSet.IY, size: 2, instruction: "EX (SP), IY", cycles: 23, pseudocode: "IYL <-> (SP); IYH <-> (SP+1)");

        #endregion

        #region Add

            #region Add (Addresses)

                public static Opcode ADD_IY_BC = new Opcode(OpcodeBytes.ADD_IY_BC, instructionSet: InstructionSet.IY, size: 2, instruction: "ADD IY, BC", cycles: 15, pseudocode: "IY <- IY + BC");
                public static Opcode ADD_IY_DE = new Opcode(OpcodeBytes.ADD_IY_DE, instructionSet: InstructionSet.IY, size: 2, instruction: "ADD IY, DE", cycles: 15, pseudocode: "IY <- IY + DE");
                public static Opcode ADD_IY_IY = new Opcode(OpcodeBytes.ADD_IY_IY, instructionSet: InstructionSet.IY, size: 2, instruction: "ADD IY, IY", cycles: 15, pseudocode: "IY <- IY + IY");
                public static Opcode ADD_IY_SP = new Opcode(OpcodeBytes.ADD_IY_SP, instructionSet: InstructionSet.IY, size: 2, instruction: "ADD IY, SP", cycles: 15, pseudocode: "IY <- IY + SP");

            #endregion

            #region Add (Arithmetic)

                public static Opcode ADD_A_MIY = new Opcode(OpcodeBytes.ADD_A_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "ADD A, (IY+n)", cycles: 19, pseudocode: "A <- A + (IY+n)");

                public static Opcode ADD_A_IYH = new Opcode(OpcodeBytes.ADD_A_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "ADD A, IYH", cycles: 8, pseudocode: "A <- A + IY.hi");
                public static Opcode ADD_A_IYL = new Opcode(OpcodeBytes.ADD_A_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "ADD A, IYL", cycles: 8, pseudocode: "A <- A + IY.lo");

                public static Opcode ADC_A_MIY = new Opcode(OpcodeBytes.ADC_A_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "ADC A, (IY+n)", cycles: 19, pseudocode: "A <- A + (IY+n) + CY");

                public static Opcode ADC_A_IYH = new Opcode(OpcodeBytes.ADC_A_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "ADC A, IYH", cycles: 8, pseudocode: "A <- A + IY.hi + CY");
                public static Opcode ADC_A_IYL = new Opcode(OpcodeBytes.ADC_A_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "ADC A, IYL", cycles: 8, pseudocode: "A <- A + IY.lo + CY");

            #endregion

        #endregion

        #region Subtract

            public static Opcode SUB_MIY = new Opcode(OpcodeBytes.SUB_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "SUB (IY+n)", cycles: 19, pseudocode: "A <- A - (IY+n)");

            public static Opcode SUB_IYH = new Opcode(OpcodeBytes.SUB_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "SUB IYH", cycles: 8, pseudocode: "A <- A - IY.hi");
            public static Opcode SUB_IYL = new Opcode(OpcodeBytes.SUB_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "SUB IYL", cycles: 8, pseudocode: "A <- A - IY.lo");

            public static Opcode SBC_A_MIY = new Opcode(OpcodeBytes.SBC_A_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "SBC A, (IY+n)", cycles: 19, pseudocode: "A <- A - (IY+n) - CY");

            public static Opcode SBC_A_IYH = new Opcode(OpcodeBytes.SBC_A_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "SBC A, IYH", cycles: 8, pseudocode: "A <- A - IY.hi - CY");
            public static Opcode SBC_A_IYL = new Opcode(OpcodeBytes.SBC_A_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "SBC A, IYL", cycles: 8, pseudocode: "A <- A - IY.lo - CY");

        #endregion

        #region Compare

            public static Opcode CP_IY = new Opcode(OpcodeBytes.CP_IY, instructionSet: InstructionSet.IY, size: 3, instruction: "CP (IY+n)", cycles: 19, pseudocode: "A - (IY+n)");

            public static Opcode CP_IYH = new Opcode(OpcodeBytes.CP_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "CP IYH", cycles: 8, pseudocode: "A - IY.hi");
            public static Opcode CP_IYL = new Opcode(OpcodeBytes.CP_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "CP IYL", cycles: 8, pseudocode: "A - IY.lo");

        #endregion

        #region Bitwise Operations

            #region Bitwise AND

                public static Opcode AND_MIY = new Opcode(OpcodeBytes.AND_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "AND (IY+x)", cycles: 19, pseudocode: "A <- A & (IY+x)");

                public static Opcode AND_IYH = new Opcode(OpcodeBytes.AND_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "AND IYH", cycles: 8, pseudocode: "A <- A & IY.hi");
                public static Opcode AND_IYL = new Opcode(OpcodeBytes.AND_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "AND IYL", cycles: 8, pseudocode: "A <- A & IY.lo");

            #endregion

            #region Bitwise OR

                public static Opcode OR_MIY = new Opcode(OpcodeBytes.OR_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "OR (IY+n)", cycles: 19, pseudocode: "A <- A & (IY+x)");

                public static Opcode OR_IYH = new Opcode(OpcodeBytes.OR_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "OR IYH", cycles: 8, pseudocode: "A <- A | IY.hi");
                public static Opcode OR_IYL = new Opcode(OpcodeBytes.OR_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "OR IYL", cycles: 8, pseudocode: "A <- A | IY.lo");

            #endregion

            #region Bitwise XOR

                public static Opcode XOR_MIY = new Opcode(OpcodeBytes.XOR_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "XOR (IY+n)", cycles: 19, pseudocode: "A <- A & (IY+x)");

                public static Opcode XOR_IYH = new Opcode(OpcodeBytes.XOR_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "XOR IYH", cycles: 8, pseudocode: "A <- A ^ IY.hi");
                public static Opcode XOR_IYL = new Opcode(OpcodeBytes.XOR_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "XOR IYL", cycles: 8, pseudocode: "A <- A ^ IY.lo");

            #endregion

        #endregion

        #region Load

            public static Opcode LD_SP_IY = new Opcode(OpcodeBytes.LD_SP_IY, instructionSet: InstructionSet.IY, size: 2, instruction: "LD SP, IY", cycles: 10, pseudocode: "SP <- IY");

            public static Opcode LD_IY_NN = new Opcode(OpcodeBytes.LD_IY_NN, instructionSet: InstructionSet.IY, size: 4, instruction: "LD IY, D16", cycles: 14, pseudocode: "IY <- D16");

            public static Opcode LD_MNN_IY = new Opcode(OpcodeBytes.LD_MNN_IY, instructionSet: InstructionSet.IY, size: 4, instruction: "LD (D16), IY", cycles: 20, pseudocode: "(D16) <- IY");
            public static Opcode LD_IY_MNN = new Opcode(OpcodeBytes.LD_IY_MNN, instructionSet: InstructionSet.IY, size: 4, instruction: "LD IY, (D16)", cycles: 20, pseudocode: "IY <- (D16)");

            public static Opcode LD_MIY_N = new Opcode(OpcodeBytes.LD_MIY_N, instructionSet: InstructionSet.IY, size: 4, instruction: "LD (IY+n), D8", cycles: 19, pseudocode: "(IY+n) <- D8");

            public static Opcode LD_MIY_B = new Opcode(OpcodeBytes.LD_MIY_B, instructionSet: InstructionSet.IY, size: 3, instruction: "LD (IY+n), B", cycles: 19, pseudocode: "(IY+n) <- B");
            public static Opcode LD_MIY_C = new Opcode(OpcodeBytes.LD_MIY_C, instructionSet: InstructionSet.IY, size: 3, instruction: "LD (IY+n), C", cycles: 19, pseudocode: "(IY+n) <- C");
            public static Opcode LD_MIY_D = new Opcode(OpcodeBytes.LD_MIY_D, instructionSet: InstructionSet.IY, size: 3, instruction: "LD (IY+n), D", cycles: 19, pseudocode: "(IY+n) <- D");
            public static Opcode LD_MIY_E = new Opcode(OpcodeBytes.LD_MIY_E, instructionSet: InstructionSet.IY, size: 3, instruction: "LD (IY+n), E", cycles: 19, pseudocode: "(IY+n) <- E");
            public static Opcode LD_MIY_H = new Opcode(OpcodeBytes.LD_MIY_H, instructionSet: InstructionSet.IY, size: 3, instruction: "LD (IY+n), H", cycles: 19, pseudocode: "(IY+n) <- H");
            public static Opcode LD_MIY_L = new Opcode(OpcodeBytes.LD_MIY_L, instructionSet: InstructionSet.IY, size: 3, instruction: "LD (IY+n), L", cycles: 19, pseudocode: "(IY+n) <- L");
            public static Opcode LD_MIY_A = new Opcode(OpcodeBytes.LD_MIY_A, instructionSet: InstructionSet.IY, size: 3, instruction: "LD (IY+n), A", cycles: 19, pseudocode: "(IY+n) <- A");

            public static Opcode LD_B_MIY = new Opcode(OpcodeBytes.LD_B_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "LD B, (IY+n)", cycles: 19, pseudocode: "B <- (IY+n)");
            public static Opcode LD_C_MIY = new Opcode(OpcodeBytes.LD_C_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "LD C, (IY+n)", cycles: 19, pseudocode: "C <- (IY+n)");
            public static Opcode LD_D_MIY = new Opcode(OpcodeBytes.LD_D_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "LD D, (IY+n)", cycles: 19, pseudocode: "D <- (IY+n)");
            public static Opcode LD_E_MIY = new Opcode(OpcodeBytes.LD_E_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "LD E, (IY+n)", cycles: 19, pseudocode: "E <- (IY+n)");
            public static Opcode LD_H_MIY = new Opcode(OpcodeBytes.LD_H_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "LD H, (IY+n)", cycles: 19, pseudocode: "H <- (IY+n)");
            public static Opcode LD_L_MIY = new Opcode(OpcodeBytes.LD_L_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "LD L, (IY+n)", cycles: 19, pseudocode: "L <- (IY+n)");
            public static Opcode LD_A_MIY = new Opcode(OpcodeBytes.LD_A_MIY, instructionSet: InstructionSet.IY, size: 3, instruction: "LD A, (IY+n)", cycles: 19, pseudocode: "A <- (IY+n)");

            public static Opcode LD_A_IYH = new Opcode(OpcodeBytes.LD_A_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "LD A, IYH", cycles: 8, pseudocode: "A <- IYH");
            public static Opcode LD_B_IYH = new Opcode(OpcodeBytes.LD_B_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "LD B, IYH", cycles: 8, pseudocode: "B <- IYH");
            public static Opcode LD_C_IYH = new Opcode(OpcodeBytes.LD_C_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "LD C, IYH", cycles: 8, pseudocode: "C <- IYH");
            public static Opcode LD_D_IYH = new Opcode(OpcodeBytes.LD_D_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "LD D, IYH", cycles: 8, pseudocode: "D <- IYH");
            public static Opcode LD_E_IYH = new Opcode(OpcodeBytes.LD_E_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "LD E, IYH", cycles: 8, pseudocode: "E <- IYH");

            public static Opcode LD_A_IYL = new Opcode(OpcodeBytes.LD_A_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "LD A, IYL", cycles: 8, pseudocode: "A <- IYL");
            public static Opcode LD_B_IYL = new Opcode(OpcodeBytes.LD_B_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "LD B, IYL", cycles: 8, pseudocode: "B <- IYL");
            public static Opcode LD_C_IYL = new Opcode(OpcodeBytes.LD_C_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "LD C, IYL", cycles: 8, pseudocode: "C <- IYL");
            public static Opcode LD_D_IYL = new Opcode(OpcodeBytes.LD_D_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "LD D, IYL", cycles: 8, pseudocode: "D <- IYL");
            public static Opcode LD_E_IYL = new Opcode(OpcodeBytes.LD_E_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "LD E, IYL", cycles: 8, pseudocode: "E <- IYL");

            public static Opcode LD_IYH_A = new Opcode(OpcodeBytes.LD_IYH_A, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYH, A", cycles: 8, pseudocode: "IYH <- A");
            public static Opcode LD_IYH_B = new Opcode(OpcodeBytes.LD_IYH_B, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYH, B", cycles: 8, pseudocode: "IYH <- B");
            public static Opcode LD_IYH_C = new Opcode(OpcodeBytes.LD_IYH_C, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYH, C", cycles: 8, pseudocode: "IYH <- C");
            public static Opcode LD_IYH_D = new Opcode(OpcodeBytes.LD_IYH_D, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYH, D", cycles: 8, pseudocode: "IYH <- D");
            public static Opcode LD_IYH_E = new Opcode(OpcodeBytes.LD_IYH_E, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYH, E", cycles: 8, pseudocode: "IYH <- E");

            public static Opcode LD_IYL_A = new Opcode(OpcodeBytes.LD_IYL_A, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYL, A", cycles: 8, pseudocode: "IYL <- A");
            public static Opcode LD_IYL_B = new Opcode(OpcodeBytes.LD_IYL_B, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYL, B", cycles: 8, pseudocode: "IYL <- B");
            public static Opcode LD_IYL_C = new Opcode(OpcodeBytes.LD_IYL_C, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYL, C", cycles: 8, pseudocode: "IYL <- C");
            public static Opcode LD_IYL_D = new Opcode(OpcodeBytes.LD_IYL_D, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYL, D", cycles: 8, pseudocode: "IYL <- D");
            public static Opcode LD_IYL_E = new Opcode(OpcodeBytes.LD_IYL_E, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYL, E", cycles: 8, pseudocode: "IYL <- E");

            public static Opcode LD_IYH_IYH = new Opcode(OpcodeBytes.LD_IYH_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYH, IYH", cycles: 8, pseudocode: "IYH <- IYH");
            public static Opcode LD_IYH_IYL = new Opcode(OpcodeBytes.LD_IYH_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYH, IYL", cycles: 8, pseudocode: "IYH <- IYL");
            public static Opcode LD_IYL_IYH = new Opcode(OpcodeBytes.LD_IYL_IYH, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYL, IYH", cycles: 8, pseudocode: "IYL <- IYH");
            public static Opcode LD_IYL_IYL = new Opcode(OpcodeBytes.LD_IYL_IYL, instructionSet: InstructionSet.IY, size: 2, instruction: "LD IYL, IYL", cycles: 8, pseudocode: "IYL <- IYL");

            public static Opcode LD_IYH_N = new Opcode(OpcodeBytes.LD_IYH_N, instructionSet:InstructionSet.IY, size: 3, instruction: "LD IYH, D8", cycles: 11, pseudocode: "IYH <- D8");
            public static Opcode LD_IYL_N = new Opcode(OpcodeBytes.LD_IYL_N, instructionSet:InstructionSet.IY, size: 3, instruction: "LD IYL, D8", cycles: 11, pseudocode: "IYL <- D8");

        #endregion
    }
}
