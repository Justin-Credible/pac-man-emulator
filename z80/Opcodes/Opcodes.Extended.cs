
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

            /* Device[C] <- (HL); HL++; B--; if B != 0, repeat(); */
            public static Opcode OTIR = new Opcode(OpcodeBytes.OTIR, instructionSet: InstructionSet.Extended, size: 2, instruction: "OTIR", cycles: 21, alternateCycles: 16, pseudocode: "Device[C] <- (HL); HL++; B--; if B != 0, repeat();");

            /* Device[C] <- (HL); HL--; B--; */
            public static Opcode OUTD = new Opcode(OpcodeBytes.OUTD, instructionSet: InstructionSet.Extended, size: 2, instruction: "OUTD", cycles: 16, pseudocode: "Device[C] <- (HL); HL--; B--;");

            /* Device[C] <- (HL); HL--; B--; if B != 0, repeat(); */
            public static Opcode OTDR = new Opcode(OpcodeBytes.OTDR, instructionSet: InstructionSet.Extended, size: 2, instruction: "OTDR", cycles: 21, alternateCycles: 16, pseudocode: "Device[C] <- (HL); HL--; B--; if B != 0, repeat();");

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

            /* (HL) <- Device[C]; HL++; B--; if B != 0, repeat(); */
            public static Opcode INIR = new Opcode(OpcodeBytes.INIR, instructionSet: InstructionSet.Extended, size: 2, instruction: "INIR", cycles: 21, alternateCycles: 16, pseudocode: "(HL) <- Device[C]; HL++; B--; if B != 0, repeat();");

            /* (HL) <- Device[C]; HL--; B--; */
            public static Opcode IND = new Opcode(OpcodeBytes.IND, instructionSet: InstructionSet.Extended, size: 2, instruction: "IND", cycles: 16, pseudocode: "(HL) <- Device[C]; HL--; B--;");

            /* (HL) <- Device[C]; HL--; B--; if B != 0, repeat(); */
            public static Opcode INDR = new Opcode(OpcodeBytes.INDR, instructionSet: InstructionSet.Extended, size: 2, instruction: "INDR", cycles: 21, alternateCycles: 16, pseudocode: "(HL) <- Device[C]; HL--; B--; if B != 0, repeat();");

        #endregion

        #region Compare

            /* A - (HL); HL++; BC--; */
            public static Opcode CPI = new Opcode(OpcodeBytes.CPI, instructionSet: InstructionSet.Extended, size: 2, instruction: "CPI", cycles: 16, pseudocode: "A - (HL); HL++; BC--;");

            /* A - (HL); HL++; BC--; if BC != 0 && !Z, repeat(); */
            public static Opcode CPIR = new Opcode(OpcodeBytes.CPIR, instructionSet: InstructionSet.Extended, size: 2, instruction: "CPIR", cycles: 21, alternateCycles: 16, pseudocode: "A - (HL); HL++; BC--; if BC != 0 && !Z, repeat();");

            /* A - (HL); HL--; BC--; */
            public static Opcode CPD = new Opcode(OpcodeBytes.CPD, instructionSet: InstructionSet.Extended, size: 2, instruction: "CPD", cycles: 16, pseudocode: "A - (HL); HL--; BC--;");

            /* A - (HL); HL--; BC--; if BC != 0 && !Z, repeat(); */
            public static Opcode CPDR = new Opcode(OpcodeBytes.CPDR, instructionSet: InstructionSet.Extended, size: 2, instruction: "CPDR", cycles: 21, alternateCycles: 16, pseudocode: "A - (HL); HL--; BC--; if BC != 0 && !Z, repeat();");

        #endregion

        #region Load

            /* (DE) <- (HL); HL++; DE++; BC--; */
            public static Opcode LDI = new Opcode(OpcodeBytes.LDI, instructionSet: InstructionSet.Extended, size: 2, instruction: "LDI", cycles: 16, pseudocode: "(DE) <- (HL); HL++; DE++; BC--;");

            /* (DE) <- (HL); HL++; DE++; BC--; if BC !=0, repeat(); */
            public static Opcode LDIR = new Opcode(OpcodeBytes.LDIR, instructionSet: InstructionSet.Extended, size: 2, instruction: "LDIR", cycles: 21, alternateCycles: 16, pseudocode: "(DE) <- (HL); HL++; DE++; BC--; if BC !=0, repeat();");

            /* (DE) <- (HL); HL--; DE--; BC--; */
            public static Opcode LDD = new Opcode(OpcodeBytes.LDD, instructionSet: InstructionSet.Extended, size: 2, instruction: "LDD", cycles: 16, pseudocode: "(DE) <- (HL); HL--; DE--; BC--;");

            /* (DE) <- (HL); HL--; DE--; BC--;  if BC !=0, repeat(); */
            public static Opcode LDDR = new Opcode(OpcodeBytes.LDDR, instructionSet: InstructionSet.Extended, size: 2, instruction: "LDDR", cycles: 21, alternateCycles: 16, pseudocode: "(DE) <- (HL); HL--; DE--; BC--;  if BC !=0, repeat();");

            /* (NN) <- BC */
            public static Opcode LD_MNN_BC = new Opcode(OpcodeBytes.LD_MNN_BC, instructionSet: InstructionSet.Extended, size: 4, instruction: "LD (NN), BC", cycles: 20, pseudocode: "(NN) <- BC");

            /* (NN) <- DE */
            public static Opcode LD_MNN_DE = new Opcode(OpcodeBytes.LD_MNN_DE, instructionSet: InstructionSet.Extended, size: 4, instruction: "LD (NN), DE", cycles: 20, pseudocode: "(NN) <- DE");

            /* (NN) <- HL */
            public static Opcode LD_MNN_HL_2 = new Opcode(OpcodeBytes.LD_MNN_HL_2, instructionSet: InstructionSet.Extended, size: 4, instruction: "LD (NN), HL", cycles: 20, pseudocode: "(NN) <- HL");

            /* (NN) <- SP */
            public static Opcode LD_MNN_SP = new Opcode(OpcodeBytes.LD_MNN_SP, instructionSet: InstructionSet.Extended, size: 4, instruction: "LD (NN), SP", cycles: 20, pseudocode: "(NN) <- SP");

            /* BC <- (NN) */
            public static Opcode LD_BC_MNN = new Opcode(OpcodeBytes.LD_BC_MNN, instructionSet: InstructionSet.Extended, size: 4, instruction: "LD BC, (NN)", cycles: 20, pseudocode: "BC <- (NN)");

            /* DE <- (NN) */
            public static Opcode LD_DE_MNN = new Opcode(OpcodeBytes.LD_DE_MNN, instructionSet: InstructionSet.Extended, size: 4, instruction: "LD DE, (NN)", cycles: 20, pseudocode: "DE <- (NN)");

            /* HL <- (NN) */
            public static Opcode LD_HL_MNN_2 = new Opcode(OpcodeBytes.LD_HL_MNN_2, instructionSet: InstructionSet.Extended, size: 4, instruction: "LD HL, (NN)", cycles: 20, pseudocode: "HL <- (NN)");

            /* SP <- (NN) */
            public static Opcode LD_SP_MNN = new Opcode(OpcodeBytes.LD_SP_MNN, instructionSet: InstructionSet.Extended, size: 4, instruction: "LD SP, (NN)", cycles: 20, pseudocode: "SP <- (NN)");

        #endregion

        #region Set Interrupt Mode

            /** Set Interrupt Mode 0 */
            public static Opcode IM0 = new Opcode(OpcodeBytes.IM0, instructionSet: InstructionSet.Extended, size: 2, instruction: "IM0", cycles: 8, pseudocode: "InterruptMode = 0");
            public static Opcode IM0_2 = new Opcode(OpcodeBytes.IM0_2, instructionSet: InstructionSet.Extended, size: 2, instruction: "IM0_2", cycles: 8, pseudocode: "InterruptMode = 0");

            // Undocumented IM0 equivalents, see:
            // https://stackoverflow.com/questions/39436351/what-does-im0-1-mean-in-z80-info-decoding#comment66229644_39436440
            public static Opcode IM0_3 = new Opcode(OpcodeBytes.IM0_3, instructionSet: InstructionSet.Extended, size: 2, instruction: "IM0_3", cycles: 8, pseudocode: "InterruptMode = 0");
            public static Opcode IM0_4 = new Opcode(OpcodeBytes.IM0_4, instructionSet: InstructionSet.Extended, size: 2, instruction: "IM0_4", cycles: 8, pseudocode: "InterruptMode = 0");

            /** Set Interrupt Mode 1 */
            public static Opcode IM1 = new Opcode(OpcodeBytes.IM1, instructionSet: InstructionSet.Extended, size: 2, instruction: "IM1", cycles: 8, pseudocode: "InterruptMode = 1");
            public static Opcode IM1_2 = new Opcode(OpcodeBytes.IM1_2, instructionSet: InstructionSet.Extended, size: 2, instruction: "IM1_2", cycles: 8, pseudocode: "InterruptMode = 1");

            /** Set Interrupt Mode 2 */
            public static Opcode IM2 = new Opcode(OpcodeBytes.IM2, instructionSet: InstructionSet.Extended, size: 2, instruction: "IM2", cycles: 8, pseudocode: "InterruptMode = 2");
            public static Opcode IM2_2 = new Opcode(OpcodeBytes.IM2_2, instructionSet: InstructionSet.Extended, size: 2, instruction: "IM2_2", cycles: 8, pseudocode: "InterruptMode = 2");

        #endregion

        #region Return from Non-maskable Interrupt

            public static Opcode RETN = new Opcode(OpcodeBytes.RETN, instructionSet: InstructionSet.Extended, size: 2, instruction: "RETN", cycles: 14, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2; IFF1 <- IFF2;");
            public static Opcode RETN_2 = new Opcode(OpcodeBytes.RETN_2, instructionSet: InstructionSet.Extended, size: 2, instruction: "RETN_2", cycles: 14, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2; IFF1 <- IFF2;");
            public static Opcode RETN_3 = new Opcode(OpcodeBytes.RETN_3, instructionSet: InstructionSet.Extended, size: 2, instruction: "RETN_3", cycles: 14, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2; IFF1 <- IFF2;");
            public static Opcode RETN_4 = new Opcode(OpcodeBytes.RETN_4, instructionSet: InstructionSet.Extended, size: 2, instruction: "RETN_4", cycles: 14, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2; IFF1 <- IFF2;");
            public static Opcode RETN_5 = new Opcode(OpcodeBytes.RETN_5, instructionSet: InstructionSet.Extended, size: 2, instruction: "RETN_5", cycles: 14, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2; IFF1 <- IFF2;");
            public static Opcode RETN_6 = new Opcode(OpcodeBytes.RETN_6, instructionSet: InstructionSet.Extended, size: 2, instruction: "RETN_6", cycles: 14, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2; IFF1 <- IFF2;");
            public static Opcode RETN_7 = new Opcode(OpcodeBytes.RETN_7, instructionSet: InstructionSet.Extended, size: 2, instruction: "RETN_7", cycles: 14, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2; IFF1 <- IFF2;");

        #endregion

        #region Return from Interrupt

            public static Opcode RETI = new Opcode(OpcodeBytes.RETI, instructionSet: InstructionSet.Extended, size: 2, instruction: "RETI", cycles: 14, pseudocode: "PC.lo <- (sp); PC.hi<-(sp+1); SP <- SP+2; NextInterrupt();");

        #endregion

        #region Rotate and Shift

            public static Opcode RRD = new Opcode(OpcodeBytes.RRD, instructionSet: InstructionSet.Extended, size: 2, instruction: "RRD", cycles: 18, pseudocode: "tmp <- (HL).lo; (HL).lo <- (HL).hi; (HL).hi <- A.lo; A.lo <- tmp;");
            public static Opcode RLD = new Opcode(OpcodeBytes.RLD, instructionSet: InstructionSet.Extended, size: 2, instruction: "RLD", cycles: 18, pseudocode: "tmp <- (HL).lo; (HL).lo <- A.lo; A.lo <- (HL).hi; (HL).hi <- tmp;");

        #endregion

        #region ADC HL, r - Add register or memory to accumulator with carry

            public static Opcode ADC_HL_BC = new Opcode(OpcodeBytes.ADC_HL_BC, instructionSet: InstructionSet.Extended, size: 2, instruction: "ADC HL, BC", cycles: 15, pseudocode: "HL <- HL + BC + CY");
            public static Opcode ADC_HL_DE = new Opcode(OpcodeBytes.ADC_HL_DE, instructionSet: InstructionSet.Extended, size: 2, instruction: "ADC HL, DE", cycles: 15, pseudocode: "HL <- HL + DE + CY");
            public static Opcode ADC_HL_HL = new Opcode(OpcodeBytes.ADC_HL_HL, instructionSet: InstructionSet.Extended, size: 2, instruction: "ADC HL, HL", cycles: 15, pseudocode: "HL <- HL + HL + CY");
            public static Opcode ADC_HL_SP = new Opcode(OpcodeBytes.ADC_HL_SP, instructionSet: InstructionSet.Extended, size: 2, instruction: "ADC HL, SP", cycles: 15, pseudocode: "HL <- HL + SP + CY");

        #endregion

        #region SBC HL, r - Subtract register or memory from accumulator with borrow

            public static Opcode SBC_HL_BC = new Opcode(OpcodeBytes.SBC_HL_BC, instructionSet: InstructionSet.Extended, size: 2, instruction: "SBC HL, BC", cycles: 15, pseudocode: "HL <- HL - BC - CY");
            public static Opcode SBC_HL_DE = new Opcode(OpcodeBytes.SBC_HL_DE, instructionSet: InstructionSet.Extended, size: 2, instruction: "SBC HL, DE", cycles: 15, pseudocode: "HL <- HL - DE - CY");
            public static Opcode SBC_HL_HL = new Opcode(OpcodeBytes.SBC_HL_HL, instructionSet: InstructionSet.Extended, size: 2, instruction: "SBC HL, HL", cycles: 15, pseudocode: "HL <- HL - HL - CY");
            public static Opcode SBC_HL_SP = new Opcode(OpcodeBytes.SBC_HL_SP, instructionSet: InstructionSet.Extended, size: 2, instruction: "SBC HL, SP", cycles: 15, pseudocode: "HL <- HL - SP - CY");

        #endregion

        #region Negate Accumulator

            public static Opcode NEG = new Opcode(OpcodeBytes.NEG, instructionSet: InstructionSet.Extended, size: 2, instruction: "NEG", cycles: 8, pseudocode: "A = ~A + 1");
            public static Opcode NEG_2 = new Opcode(OpcodeBytes.NEG_2, instructionSet: InstructionSet.Extended, size: 2, instruction: "NEG", cycles: 8, pseudocode: "A = ~A + 1");
            public static Opcode NEG_3 = new Opcode(OpcodeBytes.NEG_3, instructionSet: InstructionSet.Extended, size: 2, instruction: "NEG", cycles: 8, pseudocode: "A = ~A + 1");
            public static Opcode NEG_4 = new Opcode(OpcodeBytes.NEG_4, instructionSet: InstructionSet.Extended, size: 2, instruction: "NEG", cycles: 8, pseudocode: "A = ~A + 1");
            public static Opcode NEG_5 = new Opcode(OpcodeBytes.NEG_5, instructionSet: InstructionSet.Extended, size: 2, instruction: "NEG", cycles: 8, pseudocode: "A = ~A + 1");
            public static Opcode NEG_6 = new Opcode(OpcodeBytes.NEG_6, instructionSet: InstructionSet.Extended, size: 2, instruction: "NEG", cycles: 8, pseudocode: "A = ~A + 1");
            public static Opcode NEG_7 = new Opcode(OpcodeBytes.NEG_7, instructionSet: InstructionSet.Extended, size: 2, instruction: "NEG", cycles: 8, pseudocode: "A = ~A + 1");
            public static Opcode NEG_8 = new Opcode(OpcodeBytes.NEG_8, instructionSet: InstructionSet.Extended, size: 2, instruction: "NEG", cycles: 8, pseudocode: "A = ~A + 1");

        #endregion
    }
}
