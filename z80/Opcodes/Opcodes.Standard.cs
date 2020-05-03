
namespace JustinCredible.ZilogZ80
{
    // A list of all of the "standard" opcodes and their metadata.
    public partial class Opcodes
    {
        /** Halt */
        public static Opcode HALT = new Opcode(OpcodeBytes.HALT, size: 1, instruction: "HALT", cycles: 4);
 
        #region NOP - No operation
            public static Opcode NOP = new Opcode(OpcodeBytes.NOP, size: 1, instruction: "NOP", cycles: 4);
            public static Opcode NOP2 = new Opcode(OpcodeBytes.NOP2, size: 1, instruction: "NOP2", cycles: 4);
            public static Opcode NOP3 = new Opcode(OpcodeBytes.NOP3, size: 1, instruction: "NOP3", cycles: 4);
            public static Opcode NOP4 = new Opcode(OpcodeBytes.NOP4, size: 1, instruction: "NOP4", cycles: 4);
            public static Opcode NOP5 = new Opcode(OpcodeBytes.NOP5, size: 1, instruction: "NOP5", cycles: 4);
            public static Opcode NOP6 = new Opcode(OpcodeBytes.NOP6, size: 1, instruction: "NOP6", cycles: 4);
            public static Opcode NOP7 = new Opcode(OpcodeBytes.NOP7, size: 1, instruction: "NOP7", cycles: 4);
            public static Opcode NOP8 = new Opcode(OpcodeBytes.NOP8, size: 1, instruction: "NOP8", cycles: 4);
        #endregion

        #region Carry bit instructions

            /** Set Carry */
            public static Opcode STC = new Opcode(OpcodeBytes.STC, size: 1, instruction: "STC", cycles: 4, pseudocode: "CY = 1");

            /** Complement Carry */
            public static Opcode CMC = new Opcode(OpcodeBytes.CMC, size: 1, instruction: "CMC", cycles: 4, pseudocode: "CY=!CY");

        #endregion

        #region Single register instructions

        #region INR - Increment Register or Memory
            public static Opcode INR_B = new Opcode(OpcodeBytes.INR_B, size: 1, instruction: "INR B", cycles: 5, pseudocode: "B <- B+1");
            public static Opcode INR_C = new Opcode(OpcodeBytes.INR_C, size: 1, instruction: "INR C", cycles: 5, pseudocode: "C <- C+1");
            public static Opcode INR_D = new Opcode(OpcodeBytes.INR_D, size: 1, instruction: "INR D", cycles: 5, pseudocode: "D <- D+1");
            public static Opcode INR_E = new Opcode(OpcodeBytes.INR_E, size: 1, instruction: "INR E", cycles: 5, pseudocode: "E <-E+1");
            public static Opcode INR_H = new Opcode(OpcodeBytes.INR_H, size: 1, instruction: "INR H", cycles: 5, pseudocode: "H <- H+1");
            public static Opcode INR_L = new Opcode(OpcodeBytes.INR_L, size: 1, instruction: "INR L", cycles: 5, pseudocode: "L <- L+1");
            public static Opcode INR_M = new Opcode(OpcodeBytes.INR_M, size: 1, instruction: "INR M", cycles: 10, pseudocode: "(HL) <- (HL)+1");
            public static Opcode INR_A = new Opcode(OpcodeBytes.INR_A, size: 1, instruction: "INR A", cycles: 5, pseudocode: "A <- A+1");
        #endregion

        #region DCR - Decrement Register or Memory
            public static Opcode DCR_B = new Opcode(OpcodeBytes.DCR_B, size: 1, instruction: "DCR B", cycles: 5, pseudocode: "B <- B-1");
            public static Opcode DCR_C = new Opcode(OpcodeBytes.DCR_C, size: 1, instruction: "DCR C", cycles: 5, pseudocode: "C <-C-1");
            public static Opcode DCR_D = new Opcode(OpcodeBytes.DCR_D, size: 1, instruction: "DCR D", cycles: 5, pseudocode: "D <- D-1");
            public static Opcode DCR_E = new Opcode(OpcodeBytes.DCR_E, size: 1, instruction: "DCR E", cycles: 5, pseudocode: "E <- E-1");
            public static Opcode DCR_H = new Opcode(OpcodeBytes.DCR_H, size: 1, instruction: "DCR H", cycles: 5, pseudocode: "H <- H-1");
            public static Opcode DCR_L = new Opcode(OpcodeBytes.DCR_L, size: 1, instruction: "DCR L", cycles: 5, pseudocode: "L <- L-1");
            public static Opcode DCR_M = new Opcode(OpcodeBytes.DCR_M, size: 1, instruction: "DCR M", cycles: 10, pseudocode: "(HL) <- (HL)-1");
            public static Opcode DCR_A = new Opcode(OpcodeBytes.DCR_A, size: 1, instruction: "DCR A", cycles: 5, pseudocode: "A <- A-1");
        #endregion

        /** Compliment Accumulator */
        public static Opcode CMA = new Opcode(OpcodeBytes.CMA, size: 1, instruction: "CMA", cycles: 4, pseudocode: "A <- !A");

        /** Decimal Adjust Accumulator */
        public static Opcode DAA = new Opcode(OpcodeBytes.DAA, size: 1, instruction: "DAA", cycles: 4);

        #endregion

        #region Data transfer instructions

            #region LD rr, A - Store accumulator
                public static Opcode LD_BC_A = new Opcode(OpcodeBytes.LD_BC_A, size: 1, instruction: "LD (BC), A", cycles: 7, pseudocode: "(BC) <- A");
                public static Opcode LD_DE_A = new Opcode(OpcodeBytes.LD_DE_A, size: 1, instruction: "LD (DE), A", cycles: 7, pseudocode: "(DE) <- A");
            #endregion

            #region LD A, rr - Load accumulator
                public static Opcode LD_A_BC = new Opcode(OpcodeBytes.LD_A_BC, size: 1, instruction: "LD A, (BC)", cycles: 7, pseudocode: "A <- (BC)");
                public static Opcode LD_A_DE = new Opcode(OpcodeBytes.LD_A_DE, size: 1, instruction: "LD A, (DE)", cycles: 7, pseudocode: "A <- (DE)");
            #endregion

            #region LD - Load (copy) data
                public static Opcode LD_B_B = new Opcode(OpcodeBytes.LD_B_B, size: 1, instruction: "LD B, B", cycles: 5, pseudocode: "B <- B");
                public static Opcode LD_B_C = new Opcode(OpcodeBytes.LD_B_C, size: 1, instruction: "LD B, C", cycles: 5, pseudocode: "B <- C");
                public static Opcode LD_B_D = new Opcode(OpcodeBytes.LD_B_D, size: 1, instruction: "LD B, D", cycles: 5, pseudocode: "B <- D");
                public static Opcode LD_B_E = new Opcode(OpcodeBytes.LD_B_E, size: 1, instruction: "LD B, E", cycles: 5, pseudocode: "B <- E");
                public static Opcode LD_B_H = new Opcode(OpcodeBytes.LD_B_H, size: 1, instruction: "LD B, H", cycles: 5, pseudocode: "B <- H");
                public static Opcode LD_B_L = new Opcode(OpcodeBytes.LD_B_L, size: 1, instruction: "LD B, L", cycles: 5, pseudocode: "B <- L");
                public static Opcode LD_B_HL = new Opcode(OpcodeBytes.LD_B_HL, size: 1, instruction: "LD B, (HL)", cycles: 7, pseudocode: "B <- (HL)");
                public static Opcode LD_B_A = new Opcode(OpcodeBytes.LD_B_A, size: 1, instruction: "LD B, A", cycles: 5, pseudocode: "B <- A");
                public static Opcode LD_C_B = new Opcode(OpcodeBytes.LD_C_B, size: 1, instruction: "LD C, B", cycles: 5, pseudocode: "C <- B");
                public static Opcode LD_C_C = new Opcode(OpcodeBytes.LD_C_C, size: 1, instruction: "LD C, C", cycles: 5, pseudocode: "C <- C");
                public static Opcode LD_C_D = new Opcode(OpcodeBytes.LD_C_D, size: 1, instruction: "LD C, D", cycles: 5, pseudocode: "C <- D");
                public static Opcode LD_C_E = new Opcode(OpcodeBytes.LD_C_E, size: 1, instruction: "LD C, E", cycles: 5, pseudocode: "C <- E");
                public static Opcode LD_C_H = new Opcode(OpcodeBytes.LD_C_H, size: 1, instruction: "LD C, H", cycles: 5, pseudocode: "C <- H");
                public static Opcode LD_C_L = new Opcode(OpcodeBytes.LD_C_L, size: 1, instruction: "LD C, L", cycles: 5, pseudocode: "C <- L");
                public static Opcode LD_C_HL = new Opcode(OpcodeBytes.LD_C_HL, size: 1, instruction: "LD C, (HL)", cycles: 7, pseudocode: "C <- (HL)");
                public static Opcode LD_C_A = new Opcode(OpcodeBytes.LD_C_A, size: 1, instruction: "LD C, A", cycles: 5, pseudocode: "C <- A");
                public static Opcode LD_D_B = new Opcode(OpcodeBytes.LD_D_B, size: 1, instruction: "LD D, B", cycles: 5, pseudocode: "D <- B");
                public static Opcode LD_D_C = new Opcode(OpcodeBytes.LD_D_C, size: 1, instruction: "LD D, C", cycles: 5, pseudocode: "D <- C");
                public static Opcode LD_D_D = new Opcode(OpcodeBytes.LD_D_D, size: 1, instruction: "LD D, D", cycles: 5, pseudocode: "D <- D");
                public static Opcode LD_D_E = new Opcode(OpcodeBytes.LD_D_E, size: 1, instruction: "LD D, E", cycles: 5, pseudocode: "D <- E");
                public static Opcode LD_D_H = new Opcode(OpcodeBytes.LD_D_H, size: 1, instruction: "LD D, H", cycles: 5, pseudocode: "D <- H");
                public static Opcode LD_D_L = new Opcode(OpcodeBytes.LD_D_L, size: 1, instruction: "LD D, L", cycles: 5, pseudocode: "D <- L");
                public static Opcode LD_D_HL = new Opcode(OpcodeBytes.LD_D_HL, size: 1, instruction: "LD D, (HL)", cycles: 7, pseudocode: "D <- (HL)");
                public static Opcode LD_D_A = new Opcode(OpcodeBytes.LD_D_A, size: 1, instruction: "LD D, A", cycles: 5, pseudocode: "D <- A");
                public static Opcode LD_E_B = new Opcode(OpcodeBytes.LD_E_B, size: 1, instruction: "LD E, B", cycles: 5, pseudocode: "E <- B");
                public static Opcode LD_E_C = new Opcode(OpcodeBytes.LD_E_C, size: 1, instruction: "LD E, C", cycles: 5, pseudocode: "E <- C");
                public static Opcode LD_E_D = new Opcode(OpcodeBytes.LD_E_D, size: 1, instruction: "LD E, D", cycles: 5, pseudocode: "E <- D");
                public static Opcode LD_E_E = new Opcode(OpcodeBytes.LD_E_E, size: 1, instruction: "LD E, E", cycles: 5, pseudocode: "E <- E");
                public static Opcode LD_E_H = new Opcode(OpcodeBytes.LD_E_H, size: 1, instruction: "LD E, H", cycles: 5, pseudocode: "E <- H");
                public static Opcode LD_E_L = new Opcode(OpcodeBytes.LD_E_L, size: 1, instruction: "LD E, L", cycles: 5, pseudocode: "E <- L");
                public static Opcode LD_E_HL = new Opcode(OpcodeBytes.LD_E_HL, size: 1, instruction: "LD E, (HL)", cycles: 7, pseudocode: "E <- (HL)");
                public static Opcode LD_E_A = new Opcode(OpcodeBytes.LD_E_A, size: 1, instruction: "LD E, A", cycles: 5, pseudocode: "E <- A");
                public static Opcode LD_H_B = new Opcode(OpcodeBytes.LD_H_B, size: 1, instruction: "LD H, B", cycles: 5, pseudocode: "H <- B");
                public static Opcode LD_H_C = new Opcode(OpcodeBytes.LD_H_C, size: 1, instruction: "LD H, C", cycles: 5, pseudocode: "H <- C");
                public static Opcode LD_H_D = new Opcode(OpcodeBytes.LD_H_D, size: 1, instruction: "LD H, D", cycles: 5, pseudocode: "H <- D");
                public static Opcode LD_H_E = new Opcode(OpcodeBytes.LD_H_E, size: 1, instruction: "LD H, E", cycles: 5, pseudocode: "H <- E");
                public static Opcode LD_H_H = new Opcode(OpcodeBytes.LD_H_H, size: 1, instruction: "LD H, H", cycles: 5, pseudocode: "H <- H");
                public static Opcode LD_H_L = new Opcode(OpcodeBytes.LD_H_L, size: 1, instruction: "LD H, L", cycles: 5, pseudocode: "H <- L");
                public static Opcode LD_H_HL = new Opcode(OpcodeBytes.LD_H_HL, size: 1, instruction: "LD H, (HL)", cycles: 7, pseudocode: "H <- (HL)");
                public static Opcode LD_H_A = new Opcode(OpcodeBytes.LD_H_A, size: 1, instruction: "LD H, A", cycles: 5, pseudocode: "H <- A");
                public static Opcode LD_L_B = new Opcode(OpcodeBytes.LD_L_B, size: 1, instruction: "LD L, B", cycles: 5, pseudocode: "L <- B");
                public static Opcode LD_L_C = new Opcode(OpcodeBytes.LD_L_C, size: 1, instruction: "LD L, C", cycles: 5, pseudocode: "L <- C");
                public static Opcode LD_L_D = new Opcode(OpcodeBytes.LD_L_D, size: 1, instruction: "LD L, D", cycles: 5, pseudocode: "L <- D");
                public static Opcode LD_L_E = new Opcode(OpcodeBytes.LD_L_E, size: 1, instruction: "LD L, E", cycles: 5, pseudocode: "L <- E");
                public static Opcode LD_L_H = new Opcode(OpcodeBytes.LD_L_H, size: 1, instruction: "LD L, H", cycles: 5, pseudocode: "L <- H");
                public static Opcode LD_L_L = new Opcode(OpcodeBytes.LD_L_L, size: 1, instruction: "LD L, L", cycles: 5, pseudocode: "L <- L");
                public static Opcode LD_L_HL = new Opcode(OpcodeBytes.LD_L_HL, size: 1, instruction: "LD L, (HL)", cycles: 7, pseudocode: "L <- (HL)");
                public static Opcode LD_L_A = new Opcode(OpcodeBytes.LD_L_A, size: 1, instruction: "LD L, A", cycles: 5, pseudocode: "L <- A");
                public static Opcode LD_HL_B = new Opcode(OpcodeBytes.LD_HL_B, size: 1, instruction: "LD (HL), B", cycles: 7, pseudocode: "(HL) <- B");
                public static Opcode LD_HL_C = new Opcode(OpcodeBytes.LD_HL_C, size: 1, instruction: "LD (HL), C", cycles: 7, pseudocode: "(HL) <- C");
                public static Opcode LD_HL_D = new Opcode(OpcodeBytes.LD_HL_D, size: 1, instruction: "LD (HL), D", cycles: 7, pseudocode: "(HL) <- D");
                public static Opcode LD_HL_E = new Opcode(OpcodeBytes.LD_HL_E, size: 1, instruction: "LD (HL), E", cycles: 7, pseudocode: "(HL) <- E");
                public static Opcode LD_HL_H = new Opcode(OpcodeBytes.LD_HL_H, size: 1, instruction: "LD (HL), H", cycles: 7, pseudocode: "(HL) <- H");
                public static Opcode LD_HL_L = new Opcode(OpcodeBytes.LD_HL_L, size: 1, instruction: "LD (HL), L", cycles: 7, pseudocode: "(HL) <- L");
                public static Opcode LD_HL_A = new Opcode(OpcodeBytes.LD_HL_A, size: 1, instruction: "LD (HL), A", cycles: 7, pseudocode: "(HL) <- A");
                public static Opcode LD_A_B = new Opcode(OpcodeBytes.LD_A_B, size: 1, instruction: "LD A, B", cycles: 5, pseudocode: "A <- B");
                public static Opcode LD_A_C = new Opcode(OpcodeBytes.LD_A_C, size: 1, instruction: "LD A, C", cycles: 5, pseudocode: "A <- C");
                public static Opcode LD_A_D = new Opcode(OpcodeBytes.LD_A_D, size: 1, instruction: "LD A, D", cycles: 5, pseudocode: "A <- D");
                public static Opcode LD_A_E = new Opcode(OpcodeBytes.LD_A_E, size: 1, instruction: "LD A, E", cycles: 5, pseudocode: "A <- E");
                public static Opcode LD_A_H = new Opcode(OpcodeBytes.LD_A_H, size: 1, instruction: "LD A, H", cycles: 5, pseudocode: "A <- H");
                public static Opcode LD_A_L = new Opcode(OpcodeBytes.LD_A_L, size: 1, instruction: "LD A, L", cycles: 5, pseudocode: "A <- L");
                public static Opcode LD_A_HL = new Opcode(OpcodeBytes.LD_A_HL, size: 1, instruction: "LD A, (HL)", cycles: 7, pseudocode: "A <- (HL)");
                public static Opcode LD_A_A = new Opcode(OpcodeBytes.LD_A_A, size: 1, instruction: "LD A, A", cycles: 5, pseudocode: "A <- A");
            #endregion

        #endregion

        #region Register or memory to accumulator instructions

            #region ADD - Add register or memory to accumulator
                public static Opcode ADD_A_B = new Opcode(OpcodeBytes.ADD_A_B, size: 1, instruction: "ADD B", cycles: 4, pseudocode: "A <- A + B");
                public static Opcode ADD_A_C = new Opcode(OpcodeBytes.ADD_A_C, size: 1, instruction: "ADD C", cycles: 4, pseudocode: "A <- A + C");
                public static Opcode ADD_A_D = new Opcode(OpcodeBytes.ADD_A_D, size: 1, instruction: "ADD D", cycles: 4, pseudocode: "A <- A + D");
                public static Opcode ADD_A_E = new Opcode(OpcodeBytes.ADD_A_E, size: 1, instruction: "ADD E", cycles: 4, pseudocode: "A <- A + E");
                public static Opcode ADD_A_H = new Opcode(OpcodeBytes.ADD_A_H, size: 1, instruction: "ADD H", cycles: 4, pseudocode: "A <- A + H");
                public static Opcode ADD_A_L = new Opcode(OpcodeBytes.ADD_A_L, size: 1, instruction: "ADD L", cycles: 4, pseudocode: "A <- A + L");
                public static Opcode ADD_A_HL = new Opcode(OpcodeBytes.ADD_A_HL, size: 1, instruction: "ADD M", cycles: 7, pseudocode: "A <- A + (HL)");
                public static Opcode ADD_A_A = new Opcode(OpcodeBytes.ADD_A_A, size: 1, instruction: "ADD A", cycles: 4, pseudocode: "A <- A + A");
            #endregion

            #region SUB - Subtract register or memory from accumulator
                public static Opcode SUB_B = new Opcode(OpcodeBytes.SUB_B, size: 1, instruction: "SUB B", cycles: 4, pseudocode: "A <- A - B");
                public static Opcode SUB_C = new Opcode(OpcodeBytes.SUB_C, size: 1, instruction: "SUB C", cycles: 4, pseudocode: "A <- A - C");
                public static Opcode SUB_D = new Opcode(OpcodeBytes.SUB_D, size: 1, instruction: "SUB D", cycles: 4, pseudocode: "A <- A - D");
                public static Opcode SUB_E = new Opcode(OpcodeBytes.SUB_E, size: 1, instruction: "SUB E", cycles: 4, pseudocode: "A <- A - E");
                public static Opcode SUB_H = new Opcode(OpcodeBytes.SUB_H, size: 1, instruction: "SUB H", cycles: 4, pseudocode: "A <- A - H");
                public static Opcode SUB_L = new Opcode(OpcodeBytes.SUB_L, size: 1, instruction: "SUB L", cycles: 4, pseudocode: "A <- A - L");
                public static Opcode SUB_HL = new Opcode(OpcodeBytes.SUB_HL, size: 1, instruction: "SUB (HL)", cycles: 7, pseudocode: "A <- A - (HL)");
                public static Opcode SUB_A = new Opcode(OpcodeBytes.SUB_A, size: 1, instruction: "SUB A", cycles: 4, pseudocode: "A <- A - A");
            #endregion

            #region AND - Logical AND register or memory with accumulator
                public static Opcode AND_B = new Opcode(OpcodeBytes.AND_B, size: 1, instruction: "AND B", cycles: 4, pseudocode: "A <- A & B");
                public static Opcode AND_C = new Opcode(OpcodeBytes.AND_C, size: 1, instruction: "AND C", cycles: 4, pseudocode: "A <- A & C");
                public static Opcode AND_D = new Opcode(OpcodeBytes.AND_D, size: 1, instruction: "AND D", cycles: 4, pseudocode: "A <- A & D");
                public static Opcode AND_E = new Opcode(OpcodeBytes.AND_E, size: 1, instruction: "AND E", cycles: 4, pseudocode: "A <- A & E");
                public static Opcode AND_H = new Opcode(OpcodeBytes.AND_H, size: 1, instruction: "AND H", cycles: 4, pseudocode: "A <- A & H");
                public static Opcode AND_L = new Opcode(OpcodeBytes.AND_L, size: 1, instruction: "AND L", cycles: 4, pseudocode: "A <- A & L");
                public static Opcode AND_HL = new Opcode(OpcodeBytes.AND_HL, size: 1, instruction: "AND (HL)", cycles: 7, pseudocode: "A <- A & (HL)");
                public static Opcode AND_A = new Opcode(OpcodeBytes.AND_A, size: 1, instruction: "AND A", cycles: 4, pseudocode: "A <- A & A");
            #endregion

            #region OR - Logical OR register or memory with accumulator
                public static Opcode OR_B = new Opcode(OpcodeBytes.OR_B, size: 1, instruction: "OR B", cycles: 4, pseudocode: "A <- A | B");
                public static Opcode OR_C = new Opcode(OpcodeBytes.OR_C, size: 1, instruction: "OR C", cycles: 4, pseudocode: "A <- A | C");
                public static Opcode OR_D = new Opcode(OpcodeBytes.OR_D, size: 1, instruction: "OR D", cycles: 4, pseudocode: "A <- A | D");
                public static Opcode OR_E = new Opcode(OpcodeBytes.OR_E, size: 1, instruction: "OR E", cycles: 4, pseudocode: "A <- A | E");
                public static Opcode OR_H = new Opcode(OpcodeBytes.OR_H, size: 1, instruction: "OR H", cycles: 4, pseudocode: "A <- A | H");
                public static Opcode OR_L = new Opcode(OpcodeBytes.OR_L, size: 1, instruction: "OR L", cycles: 4, pseudocode: "A <- A | L");
                public static Opcode OR_HL = new Opcode(OpcodeBytes.OR_HL, size: 1, instruction: "OR HL", cycles: 7, pseudocode: "A <- A | (HL)");
                public static Opcode OR_A = new Opcode(OpcodeBytes.OR_A, size: 1, instruction: "OR A", cycles: 4, pseudocode: "A <- A | A");
            #endregion

            #region ADC - Add register or memory to accumulator with carry
                public static Opcode ADC_A_B = new Opcode(OpcodeBytes.ADC_A_B, size: 1, instruction: "ADC A, B", cycles: 4, pseudocode: "A <- A + B + CY");
                public static Opcode ADC_A_C = new Opcode(OpcodeBytes.ADC_A_C, size: 1, instruction: "ADC A, C", cycles: 4, pseudocode: "A <- A + C + CY");
                public static Opcode ADC_A_D = new Opcode(OpcodeBytes.ADC_A_D, size: 1, instruction: "ADC A, D", cycles: 4, pseudocode: "A <- A + D + CY");
                public static Opcode ADC_A_E = new Opcode(OpcodeBytes.ADC_A_E, size: 1, instruction: "ADC A, E", cycles: 4, pseudocode: "A <- A + E + CY");
                public static Opcode ADC_A_H = new Opcode(OpcodeBytes.ADC_A_H, size: 1, instruction: "ADC A, H", cycles: 4, pseudocode: "A <- A + H + CY");
                public static Opcode ADC_A_L = new Opcode(OpcodeBytes.ADC_A_L, size: 1, instruction: "ADC A, L", cycles: 4, pseudocode: "A <- A + L + CY");
                public static Opcode ADC_A_HL = new Opcode(OpcodeBytes.ADC_A_HL, size: 1, instruction: "ADC A, (HL)", cycles: 7, pseudocode: "A <- A + (HL) + CY");
                public static Opcode ADC_A_A = new Opcode(OpcodeBytes.ADC_A_A, size: 1, instruction: "ADC A, A", cycles: 4, pseudocode: "A <- A + A + CY");
            #endregion

            #region SBC - Subtract register or memory from accumulator with borrow
                public static Opcode SBC_A_B = new Opcode(OpcodeBytes.SBC_A_B, size: 1, instruction: "SBC A, B", cycles: 4, pseudocode: "A <- A - B - CY");
                public static Opcode SBC_A_C = new Opcode(OpcodeBytes.SBC_A_C, size: 1, instruction: "SBC A, C", cycles: 4, pseudocode: "A <- A - C - CY");
                public static Opcode SBC_A_D = new Opcode(OpcodeBytes.SBC_A_D, size: 1, instruction: "SBC A, D", cycles: 4, pseudocode: "A <- A - D - CY");
                public static Opcode SBC_A_E = new Opcode(OpcodeBytes.SBC_A_E, size: 1, instruction: "SBC A, E", cycles: 4, pseudocode: "A <- A - E - CY");
                public static Opcode SBC_A_H = new Opcode(OpcodeBytes.SBC_A_H, size: 1, instruction: "SBC A, H", cycles: 4, pseudocode: "A <- A - H - CY");
                public static Opcode SBC_A_L = new Opcode(OpcodeBytes.SBC_A_L, size: 1, instruction: "SBC A, L", cycles: 4, pseudocode: "A <- A - L - CY");
                public static Opcode SBC_A_HL = new Opcode(OpcodeBytes.SBC_A_HL, size: 1, instruction: "SBC A, (HL)", cycles: 7, pseudocode: "A <- A - (HL) - CY");
                public static Opcode SBC_A_A = new Opcode(OpcodeBytes.SBC_A_A, size: 1, instruction: "SBC A, A", cycles: 4, pseudocode: "A <- A - A - CY");
            #endregion

            #region XOR - Logical XOR register or memory with accumulator
                public static Opcode XOR_B = new Opcode(OpcodeBytes.XOR_B, size: 1, instruction: "XOR B", cycles: 4, pseudocode: "A <- A ^ B");
                public static Opcode XOR_C = new Opcode(OpcodeBytes.XOR_C, size: 1, instruction: "XOR C", cycles: 4, pseudocode: "A <- A ^ C");
                public static Opcode XOR_D = new Opcode(OpcodeBytes.XOR_D, size: 1, instruction: "XOR D", cycles: 4, pseudocode: "A <- A ^ D");
                public static Opcode XOR_E = new Opcode(OpcodeBytes.XOR_E, size: 1, instruction: "XOR E", cycles: 4, pseudocode: "A <- A ^ E");
                public static Opcode XOR_H = new Opcode(OpcodeBytes.XOR_H, size: 1, instruction: "XOR H", cycles: 4, pseudocode: "A <- A ^ H");
                public static Opcode XOR_L = new Opcode(OpcodeBytes.XOR_L, size: 1, instruction: "XOR L", cycles: 4, pseudocode: "A <- A ^ L");
                public static Opcode XOR_HL = new Opcode(OpcodeBytes.XOR_HL, size: 1, instruction: "XOR (HL)", cycles: 7, pseudocode: "A <- A ^ (HL)");
                public static Opcode XOR_A = new Opcode(OpcodeBytes.XOR_A, size: 1, instruction: "XOR A", cycles: 4, pseudocode: "A <- A ^ A");
            #endregion

            #region CP - Compare register or memory with accumulator
                public static Opcode CP_B = new Opcode(OpcodeBytes.CP_B, size: 1, instruction: "CP B", cycles: 4, pseudocode: "A - B");
                public static Opcode CP_C = new Opcode(OpcodeBytes.CP_C, size: 1, instruction: "CP C", cycles: 4, pseudocode: "A - C");
                public static Opcode CP_D = new Opcode(OpcodeBytes.CP_D, size: 1, instruction: "CP D", cycles: 4, pseudocode: "A - D");
                public static Opcode CP_E = new Opcode(OpcodeBytes.CP_E, size: 1, instruction: "CP E", cycles: 4, pseudocode: "A - E");
                public static Opcode CP_H = new Opcode(OpcodeBytes.CP_H, size: 1, instruction: "CP H", cycles: 4, pseudocode: "A - H");
                public static Opcode CP_L = new Opcode(OpcodeBytes.CP_L, size: 1, instruction: "CP L", cycles: 4, pseudocode: "A - L");
                public static Opcode CP_HL = new Opcode(OpcodeBytes.CP_HL, size: 1, instruction: "CP (HL)", cycles: 7, pseudocode: "A - (HL)");
                public static Opcode CP_A = new Opcode(OpcodeBytes.CP_A, size: 1, instruction: "CP A", cycles: 4, pseudocode: "A - A");
            #endregion

        #endregion

        #region Rotate accumulator instructions

            /** Rotate accumulator left */
            public static Opcode RLC = new Opcode(OpcodeBytes.RLC, size: 1, instruction: "RLC", cycles: 4, pseudocode: "A = A << 1; bit 0 = prev bit 7; CY = prev bit 7");

            /** Rotate accumulator right */
            public static Opcode RRC = new Opcode(OpcodeBytes.RRC, size: 1, instruction: "RRC", cycles: 4, pseudocode: "A = A >> 1; bit 7 = prev bit 0; CY = prev bit 0");

            /** Rotate accumulator left through carry */
            public static Opcode RAL = new Opcode(OpcodeBytes.RAL, size: 1, instruction: "RAL", cycles: 4, pseudocode: "A = A << 1; bit 0 = prev CY; CY = prev bit 7");

            /** Rotate accumulator right through carry */
            public static Opcode RAR = new Opcode(OpcodeBytes.RAR, size: 1, instruction: "RAR", cycles: 4, pseudocode: "A = A >> 1; bit 7 = prev bit 7; CY = prev bit 0");

        #endregion

        #region Register pair instructions

            #region INX - Increment register pair
                public static Opcode INX_B = new Opcode(OpcodeBytes.INX_B, size: 1, instruction: "INX B", cycles: 5, pseudocode: "BC <- BC+1");
                public static Opcode INX_D = new Opcode(OpcodeBytes.INX_D, size: 1, instruction: "INX D", cycles: 5, pseudocode: "DE <- DE + 1");
                public static Opcode INX_H = new Opcode(OpcodeBytes.INX_H, size: 1, instruction: "INX H", cycles: 5, pseudocode: "HL <- HL + 1");
                public static Opcode INX_SP = new Opcode(OpcodeBytes.INX_SP, size: 1, instruction: "INX SP", cycles: 5, pseudocode: "SP = SP + 1");
            #endregion

            #region DCX - Decrement register pair
                public static Opcode DCX_B = new Opcode(OpcodeBytes.DCX_B, size: 1, instruction: "DCX B", cycles: 5, pseudocode: "BC = BC-1");
                public static Opcode DCX_D = new Opcode(OpcodeBytes.DCX_D, size: 1, instruction: "DCX D", cycles: 5, pseudocode: "DE = DE-1");
                public static Opcode DCX_H = new Opcode(OpcodeBytes.DCX_H, size: 1, instruction: "DCX H", cycles: 5, pseudocode: "HL = HL-1");
                public static Opcode DCX_SP = new Opcode(OpcodeBytes.DCX_SP, size: 1, instruction: "DCX SP", cycles: 5, pseudocode: "SP = SP-1");
            #endregion

            #region PUSH - Push data onto the stack
                public static Opcode PUSH_B = new Opcode(OpcodeBytes.PUSH_B, size: 1, instruction: "PUSH B", cycles: 11, pseudocode: "(sp-2)<-C; (sp-1)<-B; sp <- sp - 2");
                public static Opcode PUSH_D = new Opcode(OpcodeBytes.PUSH_D, size: 1, instruction: "PUSH D", cycles: 11, pseudocode: "(sp-2)<-E; (sp-1)<-D; sp <- sp - 2");
                public static Opcode PUSH_H = new Opcode(OpcodeBytes.PUSH_H, size: 1, instruction: "PUSH H", cycles: 11, pseudocode: "(sp-2)<-L; (sp-1)<-H; sp <- sp - 2");
                public static Opcode PUSH_PSW = new Opcode(OpcodeBytes.PUSH_PSW, size: 1, instruction: "PUSH PSW", cycles: 11, pseudocode: "(sp-2)<-flags; (sp-1)<-A; sp <- sp - 2");
            #endregion

            #region POP - Pop data off of the stack
                public static Opcode POP_B = new Opcode(OpcodeBytes.POP_B, size: 1, instruction: "POP B", cycles: 10, pseudocode: "C <- (sp); B <- (sp+1); sp <- sp+2");
                public static Opcode POP_D = new Opcode(OpcodeBytes.POP_D, size: 1, instruction: "POP D", cycles: 10, pseudocode: "E <- (sp); D <- (sp+1); sp <- sp+2");
                public static Opcode POP_H = new Opcode(OpcodeBytes.POP_H, size: 1, instruction: "POP H", cycles: 10, pseudocode: "L <- (sp); H <- (sp+1); sp <- sp+2");
                public static Opcode POP_PSW = new Opcode(OpcodeBytes.POP_PSW, size: 1, instruction: "POP PSW", cycles: 10, pseudocode: "flags <- (sp); A <- (sp+1); sp <- sp+2");
            #endregion

            #region DAD - Double (16-bit) add
                public static Opcode DAD_B = new Opcode(OpcodeBytes.DAD_B, size: 1, instruction: "DAD B", cycles: 10, pseudocode: "HL = HL + BC");
                public static Opcode DAD_D = new Opcode(OpcodeBytes.DAD_D, size: 1, instruction: "DAD D", cycles: 10, pseudocode: "HL = HL + DE");
                public static Opcode DAD_H = new Opcode(OpcodeBytes.DAD_H, size: 1, instruction: "DAD H", cycles: 10, pseudocode: "HL = HL + HL");
                public static Opcode DAD_SP = new Opcode(OpcodeBytes.DAD_SP, size: 1, instruction: "DAD SP", cycles: 10, pseudocode: "HL = HL + SP");
            #endregion

            /** Load SP from H and L */
            public static Opcode SPHL = new Opcode(OpcodeBytes.SPHL, size: 1, instruction: "SPHL", cycles: 5, pseudocode: "SP=HL");

            /** Exchange stack */
            public static Opcode XTHL = new Opcode(OpcodeBytes.XTHL, size: 1, instruction: "XTHL",	18, pseudocode: "L <-> (SP); H <-> (SP+1)");

            /** Exchange registers */
            public static Opcode XCHG = new Opcode(OpcodeBytes.XCHG, size: 1, instruction: "XCHG",	5, pseudocode: "H <-> D; L <-> E");

        #endregion

        #region Immediate instructions

            #region Load r, nn - Load immediate data
                public static Opcode LD_B_N = new Opcode(OpcodeBytes.LD_B_N, size: 2, instruction: "LD B, D8", cycles: 7, pseudocode: "B <- byte 2");
                public static Opcode LD_C_N = new Opcode(OpcodeBytes.LD_C_N, size: 2, instruction: "LD C, D8", cycles: 7, pseudocode: "C <- byte 2");
                public static Opcode LD_D_N = new Opcode(OpcodeBytes.LD_D_N, size: 2, instruction: "LD D, D8", cycles: 7, pseudocode: "D <- byte 2");
                public static Opcode LD_E_N = new Opcode(OpcodeBytes.LD_E_N, size: 2, instruction: "LD E, D8", cycles: 7, pseudocode: "E <- byte 2");
                public static Opcode LD_H_N = new Opcode(OpcodeBytes.LD_H_N, size: 2, instruction: "LD H, D8", cycles: 7, pseudocode: "L <- byte 2");
                public static Opcode LD_L_N = new Opcode(OpcodeBytes.LD_L_N, size: 2, instruction: "LD L, D8", cycles: 7, pseudocode: "L <- byte 2");
                public static Opcode LD_HL_N = new Opcode(OpcodeBytes.LD_HL_N, size: 2, instruction: "LD (HL), D8", cycles: 10, pseudocode: "(HL) <- byte 2");
                public static Opcode LD_A_N = new Opcode(OpcodeBytes.LD_A_N, size: 2, instruction: "LD A, D8", cycles: 7, pseudocode: "A <- byte 2");
            #endregion

            #region LXI - Load register pair immediate
                public static Opcode LD_BC_NN = new Opcode(OpcodeBytes.LD_BC_NN, size: 3, instruction: "LD BC, D16", cycles: 10, pseudocode: "B <- byte 3, C <- byte 2");
                public static Opcode LD_DE_NN = new Opcode(OpcodeBytes.LD_DE_NN, size: 3, instruction: "LD DE, D16", cycles: 10, pseudocode: "D <- byte 3, E <- byte 2");
                public static Opcode LD_HL_NN = new Opcode(OpcodeBytes.LD_HL_NN, size: 3, instruction: "LD HL, D16", cycles: 10, pseudocode: "H <- byte 3, L <- byte 2");
                public static Opcode LD_SP_NN = new Opcode(OpcodeBytes.LD_SP_NN, size: 3, instruction: "LD SP, D16", cycles: 10, pseudocode: "SP.hi <- byte 3, SP.lo <- byte 2");
            #endregion

            /** Add immediate to accumulator */
            public static Opcode ADD_A_N = new Opcode(OpcodeBytes.ADD_A_N, size: 2, instruction: "ADD A, D8", cycles: 7, pseudocode: "A <- A + byte");

            /** Add immediate to accumulator with carry */
            public static Opcode ACI = new Opcode(OpcodeBytes.ACI, size: 2, instruction: "ACI D8", cycles: 7, pseudocode: "A <- A + data + CY");

            /** Subtract immediate from accumulator */
            public static Opcode SUB_N = new Opcode(OpcodeBytes.SUB_N, size: 2, instruction: "SUB D8", cycles: 7, pseudocode: "A <- A - data");

            /** Subtract immediate from accumulator with borrow */
            public static Opcode SBC_A_N = new Opcode(OpcodeBytes.SBC_A_N, size: 2, instruction: "SBC A, D8", cycles: 7, pseudocode: "A <- A - data - CY");

            /** Logical AND immediate with accumulator */
            public static Opcode AND_N = new Opcode(OpcodeBytes.AND_N, size: 2, instruction: "AND D8", cycles: 7, pseudocode: "A <- A & data");

            /** XOR immediate with accumulator */
            public static Opcode XOR_N = new Opcode(OpcodeBytes.XOR_N, size: 2, instruction: "XOR D8", cycles: 7, pseudocode: "A <- A ^ data");

            /** Logical OR immediate with accumulator */
            public static Opcode OR_N = new Opcode(OpcodeBytes.OR_N, size: 2, instruction: "OR D8", cycles: 7, pseudocode: "A <- A | data");

            /** Compare immediate with accumulator */
            public static Opcode CP_N = new Opcode(OpcodeBytes.CP_N, size: 2, instruction: "CP D8", cycles: 7, pseudocode: "A - data");

        #endregion

        #region Direct addressing instructions

            /** Store accumulator direct */
            public static Opcode STA = new Opcode(OpcodeBytes.STA, size: 3, instruction: "STA adr", cycles: 13, pseudocode: "(adr) <- A");

            /** Load accumulator direct */
            public static Opcode LDA = new Opcode(OpcodeBytes.LDA, size: 3, instruction: "LDA adr", cycles: 13, pseudocode: "A <- (adr)");

            /** Store H and L direct */
            public static Opcode SHLD = new Opcode(OpcodeBytes.SHLD, size: 3, instruction: "SHLD adr", cycles: 16, pseudocode: "(adr) <-L; (adr+1)<-H");

            /** Load H and L direct */
            public static Opcode LHLD = new Opcode(OpcodeBytes.LHLD, size: 3, instruction: "LHLD adr", cycles: 16, pseudocode: "L <- (adr); H<-(adr+1)");

        #endregion

        #region Jump instructions

            /** Load program counter */
            public static Opcode JP_HL = new Opcode(OpcodeBytes.JP_HL, size: 1, instruction: "JP (HL)", cycles: 4, pseudocode: "PC.hi <- H; PC.lo <- L");

            /** Jump */
            public static Opcode JP = new Opcode(OpcodeBytes.JP, size: 3, instruction: "JP adr", cycles: 10, pseudocode: "PC <= adr");

            /** Jump if parity odd */
            public static Opcode JP_PO = new Opcode(OpcodeBytes.JP_PO, size: 3, instruction: "JP PO, adr", cycles: 10, pseudocode: "if PO, PC <- adr");

            /** Jump if parity even */
            public static Opcode JP_PE = new Opcode(OpcodeBytes.JP_PE, size: 3, instruction: "JP PE, adr", cycles: 10, pseudocode: "if PE, PC <- adr");

            /** Jump if plus/positive */
            public static Opcode JP_P = new Opcode(OpcodeBytes.JP_P, size: 3, instruction: "JP P, adr", cycles: 10, pseudocode: "if P=1 PC <- adr");

            /** Jump if zero */
            public static Opcode JP_Z = new Opcode(OpcodeBytes.JP_Z, size: 3, instruction: "JP Z, adr", cycles: 10, pseudocode: "if Z, PC <- adr");

            /** Jump if not zero */
            public static Opcode JP_NZ = new Opcode(OpcodeBytes.JP_NZ, size: 3, instruction: "JP NZ, adr", cycles: 10, pseudocode: "if NZ, PC <- adr");

            /** Jump if not carry */
            public static Opcode JP_NC = new Opcode(OpcodeBytes.JP_NC, size: 3, instruction: "JP NC, adr", cycles: 10, pseudocode: "if NCY, PC<-adr");

            /** Jump if carry */
            public static Opcode JP_C = new Opcode(OpcodeBytes.JP_C, size: 3, instruction: "JP C, adr", cycles: 10, pseudocode: "if CY, PC<-adr");

            /** Jump if minus/negative */
            public static Opcode JP_M = new Opcode(OpcodeBytes.JP_M, size: 3, instruction: "JP M, adr", cycles: 10, pseudocode: "if M, PC <- adr");

        #endregion

        #region Call subroutine instructions

            public static Opcode CALL = new Opcode(OpcodeBytes.CALL, size: 3, instruction: "CALL adr", cycles: 17, pseudocode: "(SP-1)<-PC.hi;(SP-2)<-PC.lo;SP<-SP-2;PC=adr");

            /** Call if minus/negative */
            public static Opcode CM = new Opcode(OpcodeBytes.CM, size: 3, instruction: "CM adr", cycles: 17, alternateCycles: 11, pseudocode: "if M, CALL adr");

            /** Call if party even */
            public static Opcode CPE = new Opcode(OpcodeBytes.CPE, size: 3, instruction: "CPE adr", cycles: 17, alternateCycles: 11, pseudocode: "if PE, CALL adr");

            /** Call if carry */
            public static Opcode CC = new Opcode(OpcodeBytes.CC, size: 3, instruction: "CC adr", cycles: 17, alternateCycles: 11, pseudocode: "if CY, CALL adr");

            /** Call if zero */
            public static Opcode CZ = new Opcode(OpcodeBytes.CZ, size: 3, instruction: "CZ adr", cycles: 17, alternateCycles: 11, pseudocode: "if Z, CALL adr");

            /** Call if plus/positive */
            public static Opcode CP = new Opcode(OpcodeBytes.CP, size: 3, instruction: "CP adr", cycles: 17, alternateCycles: 11, pseudocode: "if P, PC <- adr");

            /** Call if party odd */
            public static Opcode CPO = new Opcode(OpcodeBytes.CPO, size: 3, instruction: "CPO adr", cycles: 17, alternateCycles: 11, pseudocode: "if PO, CALL adr");

            /** Call if no carry */
            public static Opcode CNC = new Opcode(OpcodeBytes.CNC, size: 3, instruction: "CNC adr", cycles: 17, alternateCycles: 11, pseudocode: "if NCY, CALL adr");

            /** Call if not zero */
            public static Opcode CNZ = new Opcode(OpcodeBytes.CNZ, size: 3, instruction: "CNZ adr", cycles: 17, alternateCycles: 11, pseudocode: "if NZ, CALL adr");

        #endregion

        #region Return from subroutine instructions

            /** Return from subroutine */
            public static Opcode RET = new Opcode(OpcodeBytes.RET, size: 1, instruction: "RET", cycles: 10, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2");

            /** Return from subroutine (duplicate) */
            public static Opcode RET2 = new Opcode(OpcodeBytes.RET2, size: 1, instruction: "RET2", cycles: 10, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2");

            /** Return if not zero */
            public static Opcode RNZ = new Opcode(OpcodeBytes.RNZ, size: 1, instruction: "RNZ", cycles: 11, alternateCycles: 5, pseudocode: "if NZ, RET");

            /** Return if zero */
            public static Opcode RZ = new Opcode(OpcodeBytes.RZ, size: 1, instruction: "RZ", cycles: 11, alternateCycles: 5, pseudocode: "if Z, RET");

            /** Return if no carry */
            public static Opcode RNC = new Opcode(OpcodeBytes.RNC, size: 1, instruction: "RNC", cycles: 11, alternateCycles: 5, pseudocode: "if NCY, RET");

            /** Return if carry */
            public static Opcode RC = new Opcode(OpcodeBytes.RC, size: 1, instruction: "RC", cycles: 11, alternateCycles: 5, pseudocode: "if CY, RET");

            /** Return if parity odd */
            public static Opcode RPO = new Opcode(OpcodeBytes.RPO, size: 1, instruction: "RPO", cycles: 11, alternateCycles: 5, pseudocode: "if PO, RET");

            /** Return if parity even */
            public static Opcode RPE = new Opcode(OpcodeBytes.RPE, size: 1, instruction: "RPE", cycles: 11, alternateCycles: 5, pseudocode: "if PE, RET");

            /** Return if plus/positive */
            public static Opcode RP = new Opcode(OpcodeBytes.RP, size: 1, instruction: "RP", cycles: 11, alternateCycles: 5, pseudocode: "if P, RET");

            /** Return if minus/negative */
            public static Opcode RM = new Opcode(OpcodeBytes.RM, size: 1, instruction: "RM", cycles: 11, alternateCycles: 5, pseudocode: "if M, RET");

        #endregion

        #region Restart (interrupt handlers) instructions

            /** CALL $0 */
            public static Opcode RST_0 = new Opcode(OpcodeBytes.RST_0, size: 1, instruction: "RST 0", cycles: 11, pseudocode: "CALL $0");

            /** CALL $8 */
            public static Opcode RST_1 = new Opcode(OpcodeBytes.RST_1, size: 1, instruction: "RST 1", cycles: 11, pseudocode: "CALL $8");

            /** CALL $10 */
            public static Opcode RST_2 = new Opcode(OpcodeBytes.RST_2, size: 1, instruction: "RST 2", cycles: 11, pseudocode: "CALL $10");

            /** CALL $18 */
            public static Opcode RST_3 = new Opcode(OpcodeBytes.RST_3, size: 1, instruction: "RST 3", cycles: 11, pseudocode: "CALL $18");

            /** CALL $20 */
            public static Opcode RST_4 = new Opcode(OpcodeBytes.RST_4, size: 1, instruction: "RST 4", cycles: 11, pseudocode: "CALL $20");

            /** CALL $28 */
            public static Opcode RST_5 = new Opcode(OpcodeBytes.RST_5, size: 1, instruction: "RST 5", cycles: 11, pseudocode: "CALL $28");

            /** CALL $30 */
            public static Opcode RST_6 = new Opcode(OpcodeBytes.RST_6, size: 1, instruction: "RST 6", cycles: 11, pseudocode: "CALL $30");

            /** CALL $38 */
            public static Opcode RST_7 = new Opcode(OpcodeBytes.RST_7, size: 1, instruction: "RST 7", cycles: 11, pseudocode: "CALL $38");

        #endregion

        #region Interrupt flip-flop instructions

            /** Enable interrupts */
            public static Opcode EI = new Opcode(OpcodeBytes.EI, size: 1, instruction: "EI", cycles: 4);

            /** Disable interrupts */
            public static Opcode DI = new Opcode(OpcodeBytes.DI, size: 1, instruction: "DI", cycles: 4);

        #endregion

        #region Input/Output Instructions

            /** Output accumulator to given device number */
            public static Opcode OUT = new Opcode(OpcodeBytes.OUT, size: 2, instruction: "OUT", cycles: 10);

            /** Retrieve input from given device number and populate accumulator */
            public static Opcode IN = new Opcode(OpcodeBytes.IN, size: 2, instruction: "IN", cycles: 10);

        #endregion
    }
}
