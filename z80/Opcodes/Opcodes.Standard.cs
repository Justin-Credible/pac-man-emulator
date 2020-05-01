
namespace JustinCredible.ZilogZ80
{
    // A list of all of the "standard" opcodes and their metadata.
    public partial class Opcodes
    {
        /** Halt */
        public static Opcode HLT = new Opcode(OpcodeBytes.HLT, size: 1, instruction: "HLT", cycles: 7);
 
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

            #region STAX - Store accumulator
                public static Opcode STAX_B = new Opcode(OpcodeBytes.STAX_B, size: 1, instruction: "STAX B", cycles: 7, pseudocode: "(BC) <- A");
                public static Opcode STAX_D = new Opcode(OpcodeBytes.STAX_D, size: 1, instruction: "STAX D", cycles: 7, pseudocode: "(DE) <- A");
            #endregion

            #region LDAX - Load accumulator
                public static Opcode LDAX_B = new Opcode(OpcodeBytes.LDAX_B, size: 1, instruction: "LDAX B", cycles: 7, pseudocode: "A <- (BC)");
                public static Opcode LDAX_D = new Opcode(OpcodeBytes.LDAX_D, size: 1, instruction: "LDAX D", cycles: 7, pseudocode: "A <- (DE)");
            #endregion

            #region MOV - Move (copy) data
                public static Opcode MOV_B_B = new Opcode(OpcodeBytes.MOV_B_B, size: 1, instruction: "MOV B,B", cycles: 5, pseudocode: "B <- B");
                public static Opcode MOV_B_C = new Opcode(OpcodeBytes.MOV_B_C, size: 1, instruction: "MOV B,C", cycles: 5, pseudocode: "B <- C");
                public static Opcode MOV_B_D = new Opcode(OpcodeBytes.MOV_B_D, size: 1, instruction: "MOV B,D", cycles: 5, pseudocode: "B <- D");
                public static Opcode MOV_B_E = new Opcode(OpcodeBytes.MOV_B_E, size: 1, instruction: "MOV B,E", cycles: 5, pseudocode: "B <- E");
                public static Opcode MOV_B_H = new Opcode(OpcodeBytes.MOV_B_H, size: 1, instruction: "MOV B,H", cycles: 5, pseudocode: "B <- H");
                public static Opcode MOV_B_L = new Opcode(OpcodeBytes.MOV_B_L, size: 1, instruction: "MOV B,L", cycles: 5, pseudocode: "B <- L");
                public static Opcode MOV_B_M = new Opcode(OpcodeBytes.MOV_B_M, size: 1, instruction: "MOV B,M", cycles: 7, pseudocode: "B <- (HL)");
                public static Opcode MOV_B_A = new Opcode(OpcodeBytes.MOV_B_A, size: 1, instruction: "MOV B,A", cycles: 5, pseudocode: "B <- A");
                public static Opcode MOV_C_B = new Opcode(OpcodeBytes.MOV_C_B, size: 1, instruction: "MOV C,B", cycles: 5, pseudocode: "C <- B");
                public static Opcode MOV_C_C = new Opcode(OpcodeBytes.MOV_C_C, size: 1, instruction: "MOV C,C", cycles: 5, pseudocode: "C <- C");
                public static Opcode MOV_C_D = new Opcode(OpcodeBytes.MOV_C_D, size: 1, instruction: "MOV C,D", cycles: 5, pseudocode: "C <- D");
                public static Opcode MOV_C_E = new Opcode(OpcodeBytes.MOV_C_E, size: 1, instruction: "MOV C,E", cycles: 5, pseudocode: "C <- E");
                public static Opcode MOV_C_H = new Opcode(OpcodeBytes.MOV_C_H, size: 1, instruction: "MOV C,H", cycles: 5, pseudocode: "C <- H");
                public static Opcode MOV_C_L = new Opcode(OpcodeBytes.MOV_C_L, size: 1, instruction: "MOV C,L", cycles: 5, pseudocode: "C <- L");
                public static Opcode MOV_C_M = new Opcode(OpcodeBytes.MOV_C_M, size: 1, instruction: "MOV C,M", cycles: 7, pseudocode: "C <- (HL)");
                public static Opcode MOV_C_A = new Opcode(OpcodeBytes.MOV_C_A, size: 1, instruction: "MOV C,A", cycles: 5, pseudocode: "C <- A");
                public static Opcode MOV_D_B = new Opcode(OpcodeBytes.MOV_D_B, size: 1, instruction: "MOV D,B", cycles: 5, pseudocode: "D <- B");
                public static Opcode MOV_D_C = new Opcode(OpcodeBytes.MOV_D_C, size: 1, instruction: "MOV D,C", cycles: 5, pseudocode: "D <- C");
                public static Opcode MOV_D_D = new Opcode(OpcodeBytes.MOV_D_D, size: 1, instruction: "MOV D,D", cycles: 5, pseudocode: "D <- D");
                public static Opcode MOV_D_E = new Opcode(OpcodeBytes.MOV_D_E, size: 1, instruction: "MOV D,E", cycles: 5, pseudocode: "D <- E");
                public static Opcode MOV_D_H = new Opcode(OpcodeBytes.MOV_D_H, size: 1, instruction: "MOV D,H", cycles: 5, pseudocode: "D <- H");
                public static Opcode MOV_D_L = new Opcode(OpcodeBytes.MOV_D_L, size: 1, instruction: "MOV D,L", cycles: 5, pseudocode: "D <- L");
                public static Opcode MOV_D_M = new Opcode(OpcodeBytes.MOV_D_M, size: 1, instruction: "MOV D,M", cycles: 7, pseudocode: "D <- (HL)");
                public static Opcode MOV_D_A = new Opcode(OpcodeBytes.MOV_D_A, size: 1, instruction: "MOV D,A", cycles: 5, pseudocode: "D <- A");
                public static Opcode MOV_E_B = new Opcode(OpcodeBytes.MOV_E_B, size: 1, instruction: "MOV E,B", cycles: 5, pseudocode: "E <- B");
                public static Opcode MOV_E_C = new Opcode(OpcodeBytes.MOV_E_C, size: 1, instruction: "MOV E,C", cycles: 5, pseudocode: "E <- C");
                public static Opcode MOV_E_D = new Opcode(OpcodeBytes.MOV_E_D, size: 1, instruction: "MOV E,D", cycles: 5, pseudocode: "E <- D");
                public static Opcode MOV_E_E = new Opcode(OpcodeBytes.MOV_E_E, size: 1, instruction: "MOV E,E", cycles: 5, pseudocode: "E <- E");
                public static Opcode MOV_E_H = new Opcode(OpcodeBytes.MOV_E_H, size: 1, instruction: "MOV E,H", cycles: 5, pseudocode: "E <- H");
                public static Opcode MOV_E_L = new Opcode(OpcodeBytes.MOV_E_L, size: 1, instruction: "MOV E,L", cycles: 5, pseudocode: "E <- L");
                public static Opcode MOV_E_M = new Opcode(OpcodeBytes.MOV_E_M, size: 1, instruction: "MOV E,M", cycles: 7, pseudocode: "E <- (HL)");
                public static Opcode MOV_E_A = new Opcode(OpcodeBytes.MOV_E_A, size: 1, instruction: "MOV E,A", cycles: 5, pseudocode: "E <- A");
                public static Opcode MOV_H_B = new Opcode(OpcodeBytes.MOV_H_B, size: 1, instruction: "MOV H,B", cycles: 5, pseudocode: "H <- B");
                public static Opcode MOV_H_C = new Opcode(OpcodeBytes.MOV_H_C, size: 1, instruction: "MOV H,C", cycles: 5, pseudocode: "H <- C");
                public static Opcode MOV_H_D = new Opcode(OpcodeBytes.MOV_H_D, size: 1, instruction: "MOV H,D", cycles: 5, pseudocode: "H <- D");
                public static Opcode MOV_H_E = new Opcode(OpcodeBytes.MOV_H_E, size: 1, instruction: "MOV H,E", cycles: 5, pseudocode: "H <- E");
                public static Opcode MOV_H_H = new Opcode(OpcodeBytes.MOV_H_H, size: 1, instruction: "MOV H,H", cycles: 5, pseudocode: "H <- H");
                public static Opcode MOV_H_L = new Opcode(OpcodeBytes.MOV_H_L, size: 1, instruction: "MOV H,L", cycles: 5, pseudocode: "H <- L");
                public static Opcode MOV_H_M = new Opcode(OpcodeBytes.MOV_H_M, size: 1, instruction: "MOV H,M", cycles: 7, pseudocode: "H <- (HL)");
                public static Opcode MOV_H_A = new Opcode(OpcodeBytes.MOV_H_A, size: 1, instruction: "MOV H,A", cycles: 5, pseudocode: "H <- A");
                public static Opcode MOV_L_B = new Opcode(OpcodeBytes.MOV_L_B, size: 1, instruction: "MOV L,B", cycles: 5, pseudocode: "L <- B");
                public static Opcode MOV_L_C = new Opcode(OpcodeBytes.MOV_L_C, size: 1, instruction: "MOV L,C", cycles: 5, pseudocode: "L <- C");
                public static Opcode MOV_L_D = new Opcode(OpcodeBytes.MOV_L_D, size: 1, instruction: "MOV L,D", cycles: 5, pseudocode: "L <- D");
                public static Opcode MOV_L_E = new Opcode(OpcodeBytes.MOV_L_E, size: 1, instruction: "MOV L,E", cycles: 5, pseudocode: "L <- E");
                public static Opcode MOV_L_H = new Opcode(OpcodeBytes.MOV_L_H, size: 1, instruction: "MOV L,H", cycles: 5, pseudocode: "L <- H");
                public static Opcode MOV_L_L = new Opcode(OpcodeBytes.MOV_L_L, size: 1, instruction: "MOV L,L", cycles: 5, pseudocode: "L <- L");
                public static Opcode MOV_L_M = new Opcode(OpcodeBytes.MOV_L_M, size: 1, instruction: "MOV L,M", cycles: 7, pseudocode: "L <- (HL)");
                public static Opcode MOV_L_A = new Opcode(OpcodeBytes.MOV_L_A, size: 1, instruction: "MOV L,A", cycles: 5, pseudocode: "L <- A");
                public static Opcode MOV_M_B = new Opcode(OpcodeBytes.MOV_M_B, size: 1, instruction: "MOV M,B", cycles: 7, pseudocode: "(HL) <- B");
                public static Opcode MOV_M_C = new Opcode(OpcodeBytes.MOV_M_C, size: 1, instruction: "MOV M,C", cycles: 7, pseudocode: "(HL) <- C");
                public static Opcode MOV_M_D = new Opcode(OpcodeBytes.MOV_M_D, size: 1, instruction: "MOV M,D", cycles: 7, pseudocode: "(HL) <- D");
                public static Opcode MOV_M_E = new Opcode(OpcodeBytes.MOV_M_E, size: 1, instruction: "MOV M,E", cycles: 7, pseudocode: "(HL) <- E");
                public static Opcode MOV_M_H = new Opcode(OpcodeBytes.MOV_M_H, size: 1, instruction: "MOV M,H", cycles: 7, pseudocode: "(HL) <- H");
                public static Opcode MOV_M_L = new Opcode(OpcodeBytes.MOV_M_L, size: 1, instruction: "MOV M,L", cycles: 7, pseudocode: "(HL) <- L");
                public static Opcode MOV_M_A = new Opcode(OpcodeBytes.MOV_M_A, size: 1, instruction: "MOV M,A", cycles: 7, pseudocode: "(HL) <- A");
                public static Opcode MOV_A_B = new Opcode(OpcodeBytes.MOV_A_B, size: 1, instruction: "MOV A,B", cycles: 5, pseudocode: "A <- B");
                public static Opcode MOV_A_C = new Opcode(OpcodeBytes.MOV_A_C, size: 1, instruction: "MOV A,C", cycles: 5, pseudocode: "A <- C");
                public static Opcode MOV_A_D = new Opcode(OpcodeBytes.MOV_A_D, size: 1, instruction: "MOV A,D", cycles: 5, pseudocode: "A <- D");
                public static Opcode MOV_A_E = new Opcode(OpcodeBytes.MOV_A_E, size: 1, instruction: "MOV A,E", cycles: 5, pseudocode: "A <- E");
                public static Opcode MOV_A_H = new Opcode(OpcodeBytes.MOV_A_H, size: 1, instruction: "MOV A,H", cycles: 5, pseudocode: "A <- H");
                public static Opcode MOV_A_L = new Opcode(OpcodeBytes.MOV_A_L, size: 1, instruction: "MOV A,L", cycles: 5, pseudocode: "A <- L");
                public static Opcode MOV_A_M = new Opcode(OpcodeBytes.MOV_A_M, size: 1, instruction: "MOV A,M", cycles: 7, pseudocode: "A <- (HL)");
                public static Opcode MOV_A_A = new Opcode(OpcodeBytes.MOV_A_A, size: 1, instruction: "MOV A,A", cycles: 5, pseudocode: "A <- A");
            #endregion

        #endregion

        #region Register or memory to accumulator instructions

            #region ADD - Add register or memory to accumulator
                public static Opcode ADD_B = new Opcode(OpcodeBytes.ADD_B, size: 1, instruction: "ADD B", cycles: 4, pseudocode: "A <- A + B");
                public static Opcode ADD_C = new Opcode(OpcodeBytes.ADD_C, size: 1, instruction: "ADD C", cycles: 4, pseudocode: "A <- A + C");
                public static Opcode ADD_D = new Opcode(OpcodeBytes.ADD_D, size: 1, instruction: "ADD D", cycles: 4, pseudocode: "A <- A + D");
                public static Opcode ADD_E = new Opcode(OpcodeBytes.ADD_E, size: 1, instruction: "ADD E", cycles: 4, pseudocode: "A <- A + E");
                public static Opcode ADD_H = new Opcode(OpcodeBytes.ADD_H, size: 1, instruction: "ADD H", cycles: 4, pseudocode: "A <- A + H");
                public static Opcode ADD_L = new Opcode(OpcodeBytes.ADD_L, size: 1, instruction: "ADD L", cycles: 4, pseudocode: "A <- A + L");
                public static Opcode ADD_M = new Opcode(OpcodeBytes.ADD_M, size: 1, instruction: "ADD M", cycles: 7, pseudocode: "A <- A + (HL)");
                public static Opcode ADD_A = new Opcode(OpcodeBytes.ADD_A, size: 1, instruction: "ADD A", cycles: 4, pseudocode: "A <- A + A");
            #endregion

            #region SUB - Subtract register or memory from accumulator
                public static Opcode SUB_B = new Opcode(OpcodeBytes.SUB_B, size: 1, instruction: "SUB B", cycles: 4, pseudocode: "A <- A - B");
                public static Opcode SUB_C = new Opcode(OpcodeBytes.SUB_C, size: 1, instruction: "SUB C", cycles: 4, pseudocode: "A <- A - C");
                public static Opcode SUB_D = new Opcode(OpcodeBytes.SUB_D, size: 1, instruction: "SUB D", cycles: 4, pseudocode: "A <- A - D");
                public static Opcode SUB_E = new Opcode(OpcodeBytes.SUB_E, size: 1, instruction: "SUB E", cycles: 4, pseudocode: "A <- A - E");
                public static Opcode SUB_H = new Opcode(OpcodeBytes.SUB_H, size: 1, instruction: "SUB H", cycles: 4, pseudocode: "A <- A - H");
                public static Opcode SUB_L = new Opcode(OpcodeBytes.SUB_L, size: 1, instruction: "SUB L", cycles: 4, pseudocode: "A <- A - L");
                public static Opcode SUB_M = new Opcode(OpcodeBytes.SUB_M, size: 1, instruction: "SUB M", cycles: 7, pseudocode: "A <- A - (HL)");
                public static Opcode SUB_A = new Opcode(OpcodeBytes.SUB_A, size: 1, instruction: "SUB A", cycles: 4, pseudocode: "A <- A - A");
            #endregion

            #region ANA - Logical AND register or memory with accumulator
                public static Opcode ANA_B = new Opcode(OpcodeBytes.ANA_B, size: 1, instruction: "ANA B", cycles: 4, pseudocode: "A <- A & B");
                public static Opcode ANA_C = new Opcode(OpcodeBytes.ANA_C, size: 1, instruction: "ANA C", cycles: 4, pseudocode: "A <- A & C");
                public static Opcode ANA_D = new Opcode(OpcodeBytes.ANA_D, size: 1, instruction: "ANA D", cycles: 4, pseudocode: "A <- A & D");
                public static Opcode ANA_E = new Opcode(OpcodeBytes.ANA_E, size: 1, instruction: "ANA E", cycles: 4, pseudocode: "A <- A & E");
                public static Opcode ANA_H = new Opcode(OpcodeBytes.ANA_H, size: 1, instruction: "ANA H", cycles: 4, pseudocode: "A <- A & H");
                public static Opcode ANA_L = new Opcode(OpcodeBytes.ANA_L, size: 1, instruction: "ANA L", cycles: 4, pseudocode: "A <- A & L");
                public static Opcode ANA_M = new Opcode(OpcodeBytes.ANA_M, size: 1, instruction: "ANA M", cycles: 7, pseudocode: "A <- A & (HL)");
                public static Opcode ANA_A = new Opcode(OpcodeBytes.ANA_A, size: 1, instruction: "ANA A", cycles: 4, pseudocode: "A <- A & A");
            #endregion

            #region ORA - Logical OR register or memory with accumulator
                public static Opcode ORA_B = new Opcode(OpcodeBytes.ORA_B, size: 1, instruction: "ORA B", cycles: 4, pseudocode: "A <- A | B");
                public static Opcode ORA_C = new Opcode(OpcodeBytes.ORA_C, size: 1, instruction: "ORA C", cycles: 4, pseudocode: "A <- A | C");
                public static Opcode ORA_D = new Opcode(OpcodeBytes.ORA_D, size: 1, instruction: "ORA D", cycles: 4, pseudocode: "A <- A | D");
                public static Opcode ORA_E = new Opcode(OpcodeBytes.ORA_E, size: 1, instruction: "ORA E", cycles: 4, pseudocode: "A <- A | E");
                public static Opcode ORA_H = new Opcode(OpcodeBytes.ORA_H, size: 1, instruction: "ORA H", cycles: 4, pseudocode: "A <- A | H");
                public static Opcode ORA_L = new Opcode(OpcodeBytes.ORA_L, size: 1, instruction: "ORA L", cycles: 4, pseudocode: "A <- A | L");
                public static Opcode ORA_M = new Opcode(OpcodeBytes.ORA_M, size: 1, instruction: "ORA M", cycles: 7, pseudocode: "A <- A | (HL)");
                public static Opcode ORA_A = new Opcode(OpcodeBytes.ORA_A, size: 1, instruction: "ORA A", cycles: 4, pseudocode: "A <- A | A");
            #endregion

            #region ADC - Add register or memory to accumulator with carry
                public static Opcode ADC_B = new Opcode(OpcodeBytes.ADC_B, size: 1, instruction: "ADC B", cycles: 4, pseudocode: "A <- A + B + CY");
                public static Opcode ADC_C = new Opcode(OpcodeBytes.ADC_C, size: 1, instruction: "ADC C", cycles: 4, pseudocode: "A <- A + C + CY");
                public static Opcode ADC_D = new Opcode(OpcodeBytes.ADC_D, size: 1, instruction: "ADC D", cycles: 4, pseudocode: "A <- A + D + CY");
                public static Opcode ADC_E = new Opcode(OpcodeBytes.ADC_E, size: 1, instruction: "ADC E", cycles: 4, pseudocode: "A <- A + E + CY");
                public static Opcode ADC_H = new Opcode(OpcodeBytes.ADC_H, size: 1, instruction: "ADC H", cycles: 4, pseudocode: "A <- A + H + CY");
                public static Opcode ADC_L = new Opcode(OpcodeBytes.ADC_L, size: 1, instruction: "ADC L", cycles: 4, pseudocode: "A <- A + L + CY");
                public static Opcode ADC_M = new Opcode(OpcodeBytes.ADC_M, size: 1, instruction: "ADC M", cycles: 7, pseudocode: "A <- A + (HL) + CY");
                public static Opcode ADC_A = new Opcode(OpcodeBytes.ADC_A, size: 1, instruction: "ADC A", cycles: 4, pseudocode: "A <- A + A + CY");
            #endregion

            #region SBB - Subtract register or memory from accumulator with borrow
                public static Opcode SBB_B = new Opcode(OpcodeBytes.SBB_B, size: 1, instruction: "SBB B", cycles: 4, pseudocode: "A <- A - B - CY");
                public static Opcode SBB_C = new Opcode(OpcodeBytes.SBB_C, size: 1, instruction: "SBB C", cycles: 4, pseudocode: "A <- A - C - CY");
                public static Opcode SBB_D = new Opcode(OpcodeBytes.SBB_D, size: 1, instruction: "SBB D", cycles: 4, pseudocode: "A <- A - D - CY");
                public static Opcode SBB_E = new Opcode(OpcodeBytes.SBB_E, size: 1, instruction: "SBB E", cycles: 4, pseudocode: "A <- A - E - CY");
                public static Opcode SBB_H = new Opcode(OpcodeBytes.SBB_H, size: 1, instruction: "SBB H", cycles: 4, pseudocode: "A <- A - H - CY");
                public static Opcode SBB_L = new Opcode(OpcodeBytes.SBB_L, size: 1, instruction: "SBB L", cycles: 4, pseudocode: "A <- A - L - CY");
                public static Opcode SBB_M = new Opcode(OpcodeBytes.SBB_M, size: 1, instruction: "SBB M", cycles: 7, pseudocode: "A <- A - (HL) - CY");
                public static Opcode SBB_A = new Opcode(OpcodeBytes.SBB_A, size: 1, instruction: "SBB A", cycles: 4, pseudocode: "A <- A - A - CY");
            #endregion

            #region XRA - Logical XOR register or memory with accumulator
                public static Opcode XRA_B = new Opcode(OpcodeBytes.XRA_B, size: 1, instruction: "XRA B", cycles: 4, pseudocode: "A <- A ^ B");
                public static Opcode XRA_C = new Opcode(OpcodeBytes.XRA_C, size: 1, instruction: "XRA C", cycles: 4, pseudocode: "A <- A ^ C");
                public static Opcode XRA_D = new Opcode(OpcodeBytes.XRA_D, size: 1, instruction: "XRA D", cycles: 4, pseudocode: "A <- A ^ D");
                public static Opcode XRA_E = new Opcode(OpcodeBytes.XRA_E, size: 1, instruction: "XRA E", cycles: 4, pseudocode: "A <- A ^ E");
                public static Opcode XRA_H = new Opcode(OpcodeBytes.XRA_H, size: 1, instruction: "XRA H", cycles: 4, pseudocode: "A <- A ^ H");
                public static Opcode XRA_L = new Opcode(OpcodeBytes.XRA_L, size: 1, instruction: "XRA L", cycles: 4, pseudocode: "A <- A ^ L");
                public static Opcode XRA_M = new Opcode(OpcodeBytes.XRA_M, size: 1, instruction: "XRA M", cycles: 7, pseudocode: "A <- A ^ (HL)");
                public static Opcode XRA_A = new Opcode(OpcodeBytes.XRA_A, size: 1, instruction: "XRA A", cycles: 4, pseudocode: "A <- A ^ A");
            #endregion

            #region CMP - Compare register or memory with accumulator
                public static Opcode CMP_B = new Opcode(OpcodeBytes.CMP_B, size: 1, instruction: "CMP B", cycles: 4, pseudocode: "A - B");
                public static Opcode CMP_C = new Opcode(OpcodeBytes.CMP_C, size: 1, instruction: "CMP C", cycles: 4, pseudocode: "A - C");
                public static Opcode CMP_D = new Opcode(OpcodeBytes.CMP_D, size: 1, instruction: "CMP D", cycles: 4, pseudocode: "A - D");
                public static Opcode CMP_E = new Opcode(OpcodeBytes.CMP_E, size: 1, instruction: "CMP E", cycles: 4, pseudocode: "A - E");
                public static Opcode CMP_H = new Opcode(OpcodeBytes.CMP_H, size: 1, instruction: "CMP H", cycles: 4, pseudocode: "A - H");
                public static Opcode CMP_L = new Opcode(OpcodeBytes.CMP_L, size: 1, instruction: "CMP L", cycles: 4, pseudocode: "A - L");
                public static Opcode CMP_M = new Opcode(OpcodeBytes.CMP_M, size: 1, instruction: "CMP M", cycles: 7, pseudocode: "A - (HL)");
                public static Opcode CMP_A = new Opcode(OpcodeBytes.CMP_A, size: 1, instruction: "CMP A", cycles: 4, pseudocode: "A - A");
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

            #region MVI - Move immediate data
                public static Opcode MVI_B = new Opcode(OpcodeBytes.MVI_B, size: 2, instruction: "MVI B,D8", cycles: 7, pseudocode: "B <- byte 2");
                public static Opcode MVI_C = new Opcode(OpcodeBytes.MVI_C, size: 2, instruction: "MVI C,D8", cycles: 7, pseudocode: "C <- byte 2");
                public static Opcode MVI_D = new Opcode(OpcodeBytes.MVI_D, size: 2, instruction: "MVI D,D8", cycles: 7, pseudocode: "D <- byte 2");
                public static Opcode MVI_E = new Opcode(OpcodeBytes.MVI_E, size: 2, instruction: "MVI E,D8", cycles: 7, pseudocode: "E <- byte 2");
                public static Opcode MVI_H = new Opcode(OpcodeBytes.MVI_H, size: 2, instruction: "MVI H,D8", cycles: 7, pseudocode: "L <- byte 2");
                public static Opcode MVI_L = new Opcode(OpcodeBytes.MVI_L, size: 2, instruction: "MVI L,D8", cycles: 7, pseudocode: "L <- byte 2");
                public static Opcode MVI_M = new Opcode(OpcodeBytes.MVI_M, size: 2, instruction: "MVI M,D8", cycles: 10, pseudocode: "(HL) <- byte 2");
                public static Opcode MVI_A = new Opcode(OpcodeBytes.MVI_A, size: 2, instruction: "MVI A,D8", cycles: 7, pseudocode: "A <- byte 2");
            #endregion

            #region LXI - Load register pair immediate
                public static Opcode LXI_B = new Opcode(OpcodeBytes.LXI_B, size: 3, instruction: "LXI B,D16", cycles: 10, pseudocode: "B <- byte 3, C <- byte 2");
                public static Opcode LXI_D = new Opcode(OpcodeBytes.LXI_D, size: 3, instruction: "LXI D,D16", cycles: 10, pseudocode: "D <- byte 3, E <- byte 2");
                public static Opcode LXI_H = new Opcode(OpcodeBytes.LXI_H, size: 3, instruction: "LXI H,D16", cycles: 10, pseudocode: "H <- byte 3, L <- byte 2");
                public static Opcode LXI_SP = new Opcode(OpcodeBytes.LXI_SP, size: 3, instruction: "LXI SP, D16", cycles: 10, pseudocode: "SP.hi <- byte 3, SP.lo <- byte 2");
            #endregion

            /** Add immediate to accumulator */
            public static Opcode ADI =new Opcode(OpcodeBytes.ADI, size: 2, instruction: "ADI D8", cycles: 7, pseudocode: "A <- A + byte");

            /** Add immediate to accumulator with carry */
            public static Opcode ACI =new Opcode(OpcodeBytes.ACI, size: 2, instruction: "ACI D8", cycles: 7, pseudocode: "A <- A + data + CY");

            /** Subtract immediate from accumulator */
            public static Opcode SUI =new Opcode(OpcodeBytes.SUI, size: 2, instruction: "SUI D8", cycles: 7, pseudocode: "A <- A - data");

            /** Subtract immediate from accumulator with borrow */
            public static Opcode SBI =new Opcode(OpcodeBytes.SBI, size: 2, instruction: "SBI D8", cycles: 7, pseudocode: "A <- A - data - CY");

            /** Logical AND immediate with accumulator */
            public static Opcode ANI =new Opcode(OpcodeBytes.ANI, size: 2, instruction: "ANI D8", cycles: 7, pseudocode: "A <- A & data");

            /** XOR immediate with accumulator */
            public static Opcode XRI =new Opcode(OpcodeBytes.XRI, size: 2, instruction: "XRI D8", cycles: 7, pseudocode: "A <- A ^ data");

            /** Logical OR immediate with accumulator */
            public static Opcode ORI =new Opcode(OpcodeBytes.ORI, size: 2, instruction: "ORI D8", cycles: 7, pseudocode: "A <- A | data");

            /** Compare immediate with accumulator */
            public static Opcode CPI =new Opcode(OpcodeBytes.CPI, size: 2, instruction: "CPI D8", cycles: 7, pseudocode: "A - data");

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
            public static Opcode PCHL = new Opcode(OpcodeBytes.PCHL, size: 1, instruction: "PCHL", cycles: 5, pseudocode: "PC.hi <- H; PC.lo <- L");

            /** Jump */
            public static Opcode JP = new Opcode(OpcodeBytes.JP, size: 3, instruction: "JMP adr", cycles: 10, pseudocode: "PC <= adr");

            /** Jump (duplicate) */
            public static Opcode JMP2 = new Opcode(OpcodeBytes.JMP2, size: 3, instruction: "JMP adr", cycles: 10, pseudocode: "PC <= adr");

            /** Jump if parity odd */
            public static Opcode JPO = new Opcode(OpcodeBytes.JPO, size: 3, instruction: "JPO adr", cycles: 10, pseudocode: "if PO, PC <- adr");

            /** Jump if parity even */
            public static Opcode JPE = new Opcode(OpcodeBytes.JPE, size: 3, instruction: "JPE adr", cycles: 10, pseudocode: "if PE, PC <- adr");

            /** Jump if plus/positive */
            public static Opcode JPP = new Opcode(OpcodeBytes.JPP, size: 3, instruction: "JP adr", cycles: 10, pseudocode: "if P=1 PC <- adr");

            /** Jump if zero */
            public static Opcode JZ = new Opcode(OpcodeBytes.JZ, size: 3, instruction: "JZ adr", cycles: 10, pseudocode: "if Z, PC <- adr");

            /** Jump if not zero */
            public static Opcode JNZ = new Opcode(OpcodeBytes.JNZ, size: 3, instruction: "JNZ adr", cycles: 10, pseudocode: "if NZ, PC <- adr");

            /** Jump if not carry */
            public static Opcode JNC = new Opcode(OpcodeBytes.JNC, size: 3, instruction: "JNC adr", cycles: 10, pseudocode: "if NCY, PC<-adr");

            /** Jump if carry */
            public static Opcode JC = new Opcode(OpcodeBytes.JC, size: 3, instruction: "JC adr", cycles: 10, pseudocode: "if CY, PC<-adr");

            /** Jump if minus/negative */
            public static Opcode JM = new Opcode(OpcodeBytes.JM, size: 3, instruction: "JM adr", cycles: 10, pseudocode: "if M, PC <- adr");

        #endregion

        #region Call subroutine instructions

            public static Opcode CALL = new Opcode(OpcodeBytes.CALL, size: 3, instruction: "CALL adr", cycles: 17, pseudocode: "(SP-1)<-PC.hi;(SP-2)<-PC.lo;SP<-SP-2;PC=adr");
            public static Opcode CALL2 = new Opcode(OpcodeBytes.CALL2, size: 3, instruction: "CALL adr", cycles: 17, pseudocode: "(SP-1)<-PC.hi;(SP-2)<-PC.lo;SP<-SP-2;PC=adr");
            public static Opcode CALL3 = new Opcode(OpcodeBytes.CALL3, size: 3, instruction: "CALL adr", cycles: 17, pseudocode: "(SP-1)<-PC.hi;(SP-2)<-PC.lo;SP<-SP-2;PC=adr");
            public static Opcode CALL4 = new Opcode(OpcodeBytes.CALL4, size: 3, instruction: "CALL adr", cycles: 17, pseudocode: "(SP-1)<-PC.hi;(SP-2)<-PC.lo;SP<-SP-2;PC=adr");

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
