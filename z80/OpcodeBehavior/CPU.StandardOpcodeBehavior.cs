using System;

namespace JustinCredible.ZilogZ80
{
    public partial class CPU
    {
        private void ExecuteStandardOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            incrementProgramCounter = true;
            useAlternateCycleCount = false;

            switch (opcode.Code)
            {
                case OpcodeBytes.HALT:
                    Halted = true;
                    break;

                case OpcodeBytes.NOP:
                    break;

                #region Carry bit instructions

                    case OpcodeBytes.CCF:
                        Flags.HalfCarry = Flags.Carry;
                        Flags.Carry = !Flags.Carry;
                        Flags.Subtract = false;
                        break;

                    case OpcodeBytes.SCF:
                        Flags.HalfCarry = false;
                        Flags.Carry = true;
                        Flags.Subtract = false;
                        break;

                #endregion

                #region Single register instructions

                    #region INC r - Increment Register or Memory

                        case OpcodeBytes.INC_B:
                            SetFlagsFrom8BitAddition(addend: Registers.B, augend: 1, false, false);
                            Registers.B++;
                            break;
                        case OpcodeBytes.INC_C:
                            SetFlagsFrom8BitAddition(addend: Registers.C, augend: 1, false, false);
                            Registers.C++;
                            break;
                        case OpcodeBytes.INC_D:
                            SetFlagsFrom8BitAddition(addend: Registers.D, augend: 1, false, false);
                            Registers.D++;
                            break;
                        case OpcodeBytes.INC_E:
                            SetFlagsFrom8BitAddition(addend: Registers.E, augend: 1, false, false);
                            Registers.E++;
                            break;
                        case OpcodeBytes.INC_H:
                            SetFlagsFrom8BitAddition(addend: Registers.H, augend: 1, false, false);
                            Registers.H++;
                            break;
                        case OpcodeBytes.INC_L:
                            SetFlagsFrom8BitAddition(addend: Registers.L, augend: 1, false, false);
                            Registers.L++;
                            break;
                        case OpcodeBytes.INC_MHL:
                        {
                            var value = Memory.Read(Registers.HL);
                            SetFlagsFrom8BitAddition(addend: value, augend: 1, false, false);
                            value++;
                            Memory.Write(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.INC_A:
                            SetFlagsFrom8BitAddition(addend: Registers.A, augend: 1, false, false);
                            Registers.A++;
                            break;

                    #endregion

                    #region DEC r - Decrement Register or Memory

                        case OpcodeBytes.DEC_B:
                            SetFlagsFrom8BitSubtraction(minuend: Registers.B, subtrahend: 1, false, false);
                            Registers.B--;
                            break;
                        case OpcodeBytes.DEC_C:
                            SetFlagsFrom8BitSubtraction(minuend: Registers.C, subtrahend: 1, false, false);
                            Registers.C--;
                            break;
                        case OpcodeBytes.DEC_D:
                            SetFlagsFrom8BitSubtraction(minuend: Registers.D, subtrahend: 1, false, false);
                            Registers.D--;
                            break;
                        case OpcodeBytes.DEC_E:
                            SetFlagsFrom8BitSubtraction(minuend: Registers.E, subtrahend: 1, false, false);
                            Registers.E--;
                            break;
                        case OpcodeBytes.DEC_H:
                            SetFlagsFrom8BitSubtraction(minuend: Registers.H, subtrahend: 1, false, false);
                            Registers.H--;
                            break;
                        case OpcodeBytes.DEC_L:
                            SetFlagsFrom8BitSubtraction(minuend: Registers.L, subtrahend: 1, false, false);
                            Registers.L--;
                            break;
                        case OpcodeBytes.DEC_MHL:
                        {
                            var value = Memory.Read(Registers.HL);
                            SetFlagsFrom8BitSubtraction(minuend: value, subtrahend: 1, false, false);
                            value--;
                            Memory.Write(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.DEC_A:
                            SetFlagsFrom8BitSubtraction(minuend: Registers.A, subtrahend: 1, false, false);
                            Registers.A--;
                            break;

                    #endregion

                    /** Compliment Accumulator */
                    case OpcodeBytes.CPL:
                        Registers.A = (byte)~Registers.A;
                        Flags.HalfCarry = true;
                        Flags.Subtract = true;
                        break;

                    /** Decimal Adjust Accumulator */
                    case OpcodeBytes.DAA:
                        Execute_DAA();
                        break;

                #endregion

                #region Data transfer instructions

                    #region LD (rr), A - Store accumulator

                        case OpcodeBytes.LD_MBC_A:
                            Memory.Write(Registers.BC, Registers.A);
                            break;
                        case OpcodeBytes.LD_MDE_A:
                            Memory.Write(Registers.DE, Registers.A);
                            break;

                    #endregion

                    #region LD A, (rr) - Load accumulator

                        case OpcodeBytes.LD_A_MBC:
                            Registers.A = Memory.Read(Registers.BC);
                            break;
                        case OpcodeBytes.LD_A_MDE:
                            Registers.A = Memory.Read(Registers.DE);
                            break;

                    #endregion

                    #region LD - Load (copy) data

                        #region LD r, r (from register to register)

                        case OpcodeBytes.LD_B_B:
                            // NOP
                            break;
                        case OpcodeBytes.LD_B_C:
                            Registers.B = Registers.C;
                            break;
                        case OpcodeBytes.LD_B_D:
                            Registers.B = Registers.D;
                            break;
                        case OpcodeBytes.LD_B_E:
                            Registers.B = Registers.E;
                            break;
                        case OpcodeBytes.LD_B_H:
                            Registers.B = Registers.H;
                            break;
                        case OpcodeBytes.LD_B_L:
                            Registers.B = Registers.L;
                            break;
                        case OpcodeBytes.LD_B_A:
                            Registers.B = Registers.A;
                            break;
                        case OpcodeBytes.LD_C_B:
                            Registers.C = Registers.B;
                            break;
                        case OpcodeBytes.LD_C_C:
                            // NOP
                            break;
                        case OpcodeBytes.LD_C_D:
                            Registers.C = Registers.D;
                            break;
                        case OpcodeBytes.LD_C_E:
                            Registers.C = Registers.E;
                            break;
                        case OpcodeBytes.LD_C_H:
                            Registers.C = Registers.H;
                            break;
                        case OpcodeBytes.LD_C_L:
                            Registers.C = Registers.L;
                            break;
                        case OpcodeBytes.LD_C_A:
                            Registers.C = Registers.A;
                            break;
                        case OpcodeBytes.LD_D_B:
                            Registers.D = Registers.B;
                            break;
                        case OpcodeBytes.LD_D_C:
                            Registers.D = Registers.C;
                            break;
                        case OpcodeBytes.LD_D_D:
                            // NOP
                            break;
                        case OpcodeBytes.LD_D_E:
                            Registers.D = Registers.E;
                            break;
                        case OpcodeBytes.LD_D_H:
                            Registers.D = Registers.H;
                            break;
                        case OpcodeBytes.LD_D_L:
                            Registers.D = Registers.L;
                            break;
                        case OpcodeBytes.LD_D_A:
                            Registers.D = Registers.A;
                            break;
                        case OpcodeBytes.LD_E_B:
                            Registers.E = Registers.B;
                            break;
                        case OpcodeBytes.LD_E_C:
                            Registers.E = Registers.C;
                            break;
                        case OpcodeBytes.LD_E_D:
                            Registers.E = Registers.D;
                            break;
                        case OpcodeBytes.LD_E_E:
                            // NOP
                            break;
                        case OpcodeBytes.LD_E_H:
                            Registers.E = Registers.H;
                            break;
                        case OpcodeBytes.LD_E_L:
                            Registers.E = Registers.L;
                            break;
                        case OpcodeBytes.LD_E_A:
                            Registers.E = Registers.A;
                            break;
                        case OpcodeBytes.LD_H_B:
                            Registers.H = Registers.B;
                            break;
                        case OpcodeBytes.LD_H_C:
                            Registers.H = Registers.C;
                            break;
                        case OpcodeBytes.LD_H_D:
                            Registers.H = Registers.D;
                            break;
                        case OpcodeBytes.LD_H_E:
                            Registers.H = Registers.E;
                            break;
                        case OpcodeBytes.LD_H_H:
                            // NOP
                            break;
                        case OpcodeBytes.LD_H_L:
                            Registers.H = Registers.L;
                            break;
                        case OpcodeBytes.LD_H_A:
                            Registers.H = Registers.A;
                            break;
                        case OpcodeBytes.LD_L_B:
                            Registers.L = Registers.B;
                            break;
                        case OpcodeBytes.LD_L_C:
                            Registers.L = Registers.C;
                            break;
                        case OpcodeBytes.LD_L_D:
                            Registers.L = Registers.D;
                            break;
                        case OpcodeBytes.LD_L_E:
                            Registers.L = Registers.E;
                            break;
                        case OpcodeBytes.LD_L_H:
                            Registers.L = Registers.H;
                            break;
                        case OpcodeBytes.LD_L_L:
                            // NOP
                            break;
                        case OpcodeBytes.LD_L_A:
                            Registers.L = Registers.A;
                            break;
                        case OpcodeBytes.LD_A_B:
                            Registers.A = Registers.B;
                            break;
                        case OpcodeBytes.LD_A_C:
                            Registers.A = Registers.C;
                            break;
                        case OpcodeBytes.LD_A_D:
                            Registers.A = Registers.D;
                            break;
                        case OpcodeBytes.LD_A_E:
                            Registers.A = Registers.E;
                            break;
                        case OpcodeBytes.LD_A_H:
                            Registers.A = Registers.H;
                            break;
                        case OpcodeBytes.LD_A_L:
                            Registers.A = Registers.L;
                            break;
                        case OpcodeBytes.LD_A_A:
                            // NOP
                            break;

                        #endregion

                        #region LD r, (HL) (from memory to register)

                        case OpcodeBytes.LD_B_MHL:
                            Registers.B = Memory.Read(Registers.HL);
                            break;
                        case OpcodeBytes.LD_C_MHL:
                            Registers.C = Memory.Read(Registers.HL);
                            break;
                        case OpcodeBytes.LD_D_MHL:
                            Registers.D = Memory.Read(Registers.HL);
                            break;
                        case OpcodeBytes.LD_E_MHL:
                            Registers.E = Memory.Read(Registers.HL);
                            break;
                        case OpcodeBytes.LD_H_MHL:
                            Registers.H = Memory.Read(Registers.HL);
                            break;
                        case OpcodeBytes.LD_L_MHL:
                            Registers.L = Memory.Read(Registers.HL);
                            break;
                        case OpcodeBytes.LD_A_MHL:
                            Registers.A = Memory.Read(Registers.HL);
                            break;

                        #endregion

                        #region LD (HL), r (from register to memory)

                        case OpcodeBytes.LD_MHL_B:
                            Memory.Write(Registers.HL, Registers.B);
                            break;
                        case OpcodeBytes.LD_MHL_C:
                            Memory.Write(Registers.HL, Registers.C);
                            break;
                        case OpcodeBytes.LD_MHL_D:
                            Memory.Write(Registers.HL, Registers.D);
                            break;
                        case OpcodeBytes.LD_MHL_E:
                            Memory.Write(Registers.HL, Registers.E);
                            break;
                        case OpcodeBytes.LD_MHL_H:
                            Memory.Write(Registers.HL, Registers.H);
                            break;
                        case OpcodeBytes.LD_MHL_L:
                            Memory.Write(Registers.HL, Registers.L);
                            break;
                        case OpcodeBytes.LD_MHL_A:
                            Memory.Write(Registers.HL, Registers.A);
                            break;

                        #endregion

                    #endregion

                #endregion

                #region Register or memory to accumulator instructions

                    #region ADD r - Add register or memory to accumulator

                        case OpcodeBytes.ADD_A_B:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.B);
                            break;
                        case OpcodeBytes.ADD_A_C:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.C);
                            break;
                        case OpcodeBytes.ADD_A_D:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.D);
                            break;
                        case OpcodeBytes.ADD_A_E:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.E);
                            break;
                        case OpcodeBytes.ADD_A_H:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.H);
                            break;
                        case OpcodeBytes.ADD_A_L:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.L);
                            break;
                        case OpcodeBytes.ADD_A_MHL:
                            Registers.A = Execute8BitAddition(Registers.A, Memory.Read(Registers.HL));
                            break;
                        case OpcodeBytes.ADD_A_A:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.A);
                            break;

                    #endregion

                    #region SUB r - Subtract register or memory from accumulator

                        case OpcodeBytes.SUB_B:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.B);
                            break;
                        case OpcodeBytes.SUB_C:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.C);
                            break;
                        case OpcodeBytes.SUB_D:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.D);
                            break;
                        case OpcodeBytes.SUB_E:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.E);
                            break;
                        case OpcodeBytes.SUB_H:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.H);
                            break;
                        case OpcodeBytes.SUB_L:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.L);
                            break;
                        case OpcodeBytes.SUB_MHL:
                            Registers.A = Execute8BitSubtraction(Registers.A, Memory.Read(Registers.HL));
                            break;
                        case OpcodeBytes.SUB_A:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.A);
                            break;

                    #endregion

                    #region AND r - Logical AND register or memory with accumulator

                        case OpcodeBytes.AND_B:
                            Registers.A = (byte)(Registers.A & Registers.B);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        case OpcodeBytes.AND_C:
                            Registers.A = (byte)(Registers.A & Registers.C);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        case OpcodeBytes.AND_D:
                            Registers.A = (byte)(Registers.A & Registers.D);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        case OpcodeBytes.AND_E:
                            Registers.A = (byte)(Registers.A & Registers.E);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        case OpcodeBytes.AND_H:
                            Registers.A = (byte)(Registers.A & Registers.H);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        case OpcodeBytes.AND_L:
                            Registers.A = (byte)(Registers.A & Registers.L);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        case OpcodeBytes.AND_MHL:
                            Registers.A = (byte)(Registers.A & Memory.Read(Registers.HL));
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        case OpcodeBytes.AND_A:
                            Registers.A = (byte)(Registers.A & Registers.A);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;

                    #endregion

                    #region OR r - Logical OR register or memory with accumulator

                        case OpcodeBytes.OR_B:
                            Registers.A = (byte)(Registers.A | Registers.B);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.OR_C:
                            Registers.A = (byte)(Registers.A | Registers.C);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.OR_D:
                            Registers.A = (byte)(Registers.A | Registers.D);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.OR_E:
                            Registers.A = (byte)(Registers.A | Registers.E);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.OR_H:
                            Registers.A = (byte)(Registers.A | Registers.H);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.OR_L:
                            Registers.A = (byte)(Registers.A | Registers.L);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.OR_MHL:
                            Registers.A = (byte)(Registers.A | Memory.Read(Registers.HL));
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.OR_A:
                            Registers.A = (byte)(Registers.A | Registers.A);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;

                    #endregion

                    #region ADC A, r - Add register or memory to accumulator with carry

                        case OpcodeBytes.ADC_A_B:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.B, true);
                            break;
                        case OpcodeBytes.ADC_A_C:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.C, true);
                            break;
                        case OpcodeBytes.ADC_A_D:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.D, true);
                            break;
                        case OpcodeBytes.ADC_A_E:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.E, true);
                            break;
                        case OpcodeBytes.ADC_A_H:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.H, true);
                            break;
                        case OpcodeBytes.ADC_A_L:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.L, true);
                            break;
                        case OpcodeBytes.ADC_A_MHL:
                            Registers.A = Execute8BitAddition(Registers.A, Memory.Read(Registers.HL), true);
                            break;
                        case OpcodeBytes.ADC_A_A:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.A, true);
                            break;

                    #endregion

                    #region SBC A, r - Subtract register or memory from accumulator with borrow

                        case OpcodeBytes.SBC_A_B:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.B, true);
                            break;
                        case OpcodeBytes.SBC_A_C:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.C, true);
                            break;
                        case OpcodeBytes.SBC_A_D:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.D, true);
                            break;
                        case OpcodeBytes.SBC_A_E:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.E, true);
                            break;
                        case OpcodeBytes.SBC_A_H:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.H, true);
                            break;
                        case OpcodeBytes.SBC_A_L:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.L, true);
                            break;
                        case OpcodeBytes.SBC_A_MHL:
                            Registers.A = Execute8BitSubtraction(Registers.A, Memory.Read(Registers.HL), true);
                            break;
                        case OpcodeBytes.SBC_A_A:
                            Registers.A = Execute8BitSubtraction(Registers.A, Registers.A, true);
                            break;

                    #endregion

                    #region XOR r - Logical XOR register or memory with accumulator

                        case OpcodeBytes.XOR_B:
                            Registers.A = (byte)(Registers.A ^ Registers.B);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.XOR_C:
                            Registers.A = (byte)(Registers.A ^ Registers.C);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.XOR_D:
                            Registers.A = (byte)(Registers.A ^ Registers.D);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.XOR_E:
                            Registers.A = (byte)(Registers.A ^ Registers.E);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.XOR_H:
                            Registers.A = (byte)(Registers.A ^ Registers.H);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.XOR_L:
                            Registers.A = (byte)(Registers.A ^ Registers.L);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.XOR_MHL:
                            Registers.A = (byte)(Registers.A ^ Memory.Read(Registers.HL));
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.XOR_A:
                            Registers.A = (byte)(Registers.A ^ Registers.A);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;

                    #endregion

                    #region CP r - Compare register or memory with accumulator

                        case OpcodeBytes.CP_B:
                            Execute8BitSubtraction(Registers.A, Registers.B, false);
                            break;
                        case OpcodeBytes.CP_C:
                            Execute8BitSubtraction(Registers.A, Registers.C, false);
                            break;
                        case OpcodeBytes.CP_D:
                            Execute8BitSubtraction(Registers.A, Registers.D, false);
                            break;
                        case OpcodeBytes.CP_E:
                            Execute8BitSubtraction(Registers.A, Registers.E, false);
                            break;
                        case OpcodeBytes.CP_H:
                            Execute8BitSubtraction(Registers.A, Registers.H, false);
                            break;
                        case OpcodeBytes.CP_L:
                            Execute8BitSubtraction(Registers.A, Registers.L, false);
                            break;
                        case OpcodeBytes.CP_MHL:
                            Execute8BitSubtraction(Registers.A, Memory.Read(Registers.HL), false);
                            break;
                        case OpcodeBytes.CP_A:
                            Execute8BitSubtraction(Registers.A, Registers.A, false);
                            break;

                    #endregion

                #endregion

            #region Exchange registers

                /** Exchange BC, DE, HL <-> BC', DE', HL' */
                case OpcodeBytes.EXX:
                    Registers.SwapShadowRegistersBCDEHL();
                    break;

                /** Exchange A, F <-> A', F' */
                case OpcodeBytes.EX_AF_AFP:
                    Registers.SwapShadowRegisterA();
                    Flags.SwapShadowRegister();
                    break;

            #endregion

                #region Rotate accumulator instructions

                    // Rotate accumulator left
                    // A = A << 1; bit 0 = prev bit 7; CY = prev bit 7
                    case OpcodeBytes.RLCA:
                        Registers.A = ExecuteRotate(value: Registers.A, left: true, setAllFlags: false);
                        break;

                    // Rotate accumulator right
                    // A = A >> 1; bit 7 = prev bit 0; CY = prev bit 0
                    case OpcodeBytes.RRCA:
                        Registers.A = ExecuteRotate(value: Registers.A, left: false, setAllFlags: false);
                        break;

                    // Rotate accumulator left through carry
                    // A = A << 1; bit 0 = prev CY; CY = prev bit 7
                    case OpcodeBytes.RLA:
                        Registers.A = ExecuteRotate(value: Registers.A, left: true, rotateThroughCarry: true, setAllFlags: false);
                        break;

                    // Rotate accumulator right through carry
                    // A = A >> 1; bit 7 = prev CY; CY = prev bit 0
                    case OpcodeBytes.RRA:
                        Registers.A = ExecuteRotate(value: Registers.A, left: false, rotateThroughCarry: true, setAllFlags: false);
                        break;

                #endregion

                #region Register pair instructions

                    #region INC rr - Increment register pair

                        case OpcodeBytes.INC_BC:
                            Registers.BC++;
                            break;
                        case OpcodeBytes.INC_DE:
                            Registers.DE++;
                            break;
                        case OpcodeBytes.INC_HL:
                            Registers.HL++;
                            break;
                        case OpcodeBytes.INC_SP:
                            Registers.SP++;
                            break;

                    #endregion

                    #region DEC rr - Decrement register pair

                        case OpcodeBytes.DEC_BC:
                            Registers.BC--;
                            break;
                        case OpcodeBytes.DEC_DE:
                            Registers.DE--;
                            break;
                        case OpcodeBytes.DEC_HL:
                            Registers.HL--;
                            break;
                        case OpcodeBytes.DEC_SP:
                            Registers.SP--;
                            break;

                    #endregion

                    #region PUSH rr - Push data onto the stack

                        case OpcodeBytes.PUSH_BC:
                            Memory.Write(Registers.SP - 1, Registers.B);
                            Memory.Write(Registers.SP - 2, Registers.C);
                            Registers.SP = (UInt16)(Registers.SP - 2);
                            break;
                        case OpcodeBytes.PUSH_DE:
                            Memory.Write(Registers.SP - 1, Registers.D);
                            Memory.Write(Registers.SP - 2, Registers.E);
                            Registers.SP = (UInt16)(Registers.SP - 2);
                            break;
                        case OpcodeBytes.PUSH_HL:
                            Memory.Write(Registers.SP - 1, Registers.H);
                            Memory.Write(Registers.SP - 2, Registers.L);
                            Registers.SP = (UInt16)(Registers.SP - 2);
                            break;
                        case OpcodeBytes.PUSH_AF:
                            Memory.Write(Registers.SP - 1, Registers.A);
                            Memory.Write(Registers.SP - 2, Flags.ToByte());
                            Registers.SP = (UInt16)(Registers.SP - 2);
                            break;

                    #endregion

                    #region POP rr - Pop data off of the stack

                        case OpcodeBytes.POP_BC:
                            Registers.B = Memory.Read(Registers.SP + 1);
                            Registers.C = Memory.Read(Registers.SP);
                            Registers.SP = (UInt16)(Registers.SP + 2);
                            break;
                        case OpcodeBytes.POP_DE:
                            Registers.D = Memory.Read(Registers.SP + 1);
                            Registers.E = Memory.Read(Registers.SP);
                            Registers.SP = (UInt16)(Registers.SP + 2);
                            break;
                        case OpcodeBytes.POP_HL:
                            Registers.H = Memory.Read(Registers.SP + 1);
                            Registers.L = Memory.Read(Registers.SP);
                            Registers.SP = (UInt16)(Registers.SP + 2);
                            break;
                        case OpcodeBytes.POP_AF:
                            Registers.A = Memory.Read(Registers.SP + 1);
                            Flags.SetFromByte(Memory.Read(Registers.SP));
                            Registers.SP = (UInt16)(Registers.SP + 2);
                            break;

                    #endregion

                    #region ADD HL, rr - Double (16-bit) add

                        case OpcodeBytes.ADD_HL_BC:
                            Registers.HL = Execute16BitAddition(Registers.HL, Registers.BC);
                            break;
                        case OpcodeBytes.ADD_HL_DE:
                            Registers.HL = Execute16BitAddition(Registers.HL, Registers.DE);
                            break;
                        case OpcodeBytes.ADD_HL_HL:
                            Registers.HL = Execute16BitAddition(Registers.HL, Registers.HL);
                            break;
                        case OpcodeBytes.ADD_HL_SP:
                            Registers.HL = Execute16BitAddition(Registers.HL, Registers.SP);
                            break;

                    #endregion

                    // Load SP from H and L
                    case OpcodeBytes.LD_SP_HL:
                        Registers.SP = Registers.HL;
                        break;

                    // Exchange stack
                    //  L <-> (SP); H <-> (SP+1)
                    case OpcodeBytes.EX_MSP_HL:
                    {
                        var oldL = Registers.L;
                        var oldH = Registers.H;
                        Registers.L = Memory.Read(Registers.SP);
                        Memory.Write(Registers.SP, oldL);
                        Registers.H = Memory.Read(Registers.SP+1);
                        Memory.Write(Registers.SP+1, oldH);
                        break;
                    }

                    // Exchange registers
                    // H <-> D; L <-> E
                    case OpcodeBytes.EX_DE_HL:
                    {
                        var oldH = Registers.H;
                        var oldL = Registers.L;
                        Registers.H = Registers.D;
                        Registers.D = oldH;
                        Registers.L = Registers.E;
                        Registers.E = oldL;
                        break;
                    }

                #endregion

                #region Immediate instructions

                    #region LD r, n - Load immediate data

                        case OpcodeBytes.LD_B_N:
                            Registers.B = Memory.Read(Registers.PC + 1);
                            break;
                        case OpcodeBytes.LD_C_N:
                            Registers.C = Memory.Read(Registers.PC + 1);
                            break;
                        case OpcodeBytes.LD_D_N:
                            Registers.D = Memory.Read(Registers.PC + 1);
                            break;
                        case OpcodeBytes.LD_E_N:
                            Registers.E = Memory.Read(Registers.PC + 1);
                            break;
                        case OpcodeBytes.LD_H_N:
                            Registers.H = Memory.Read(Registers.PC + 1);
                            break;
                        case OpcodeBytes.LD_L_N:
                            Registers.L = Memory.Read(Registers.PC + 1);
                            break;
                        case OpcodeBytes.LD_MHL_N:
                            Memory.Write(Registers.HL, Memory.Read(Registers.PC + 1));
                            break;
                        case OpcodeBytes.LD_A_N:
                            Registers.A = Memory.Read(Registers.PC + 1);
                            break;

                    #endregion

                    #region LD rr, nn - Load register pair immediate

                        case OpcodeBytes.LD_BC_NN:
                            Registers.B = Memory.Read(Registers.PC + 2);
                            Registers.C = Memory.Read(Registers.PC + 1);
                            break;
                        case OpcodeBytes.LD_DE_NN:
                            Registers.D = Memory.Read(Registers.PC + 2);
                            Registers.E = Memory.Read(Registers.PC + 1);
                            break;
                        case OpcodeBytes.LD_HL_NN:
                            Registers.H = Memory.Read(Registers.PC + 2);
                            Registers.L = Memory.Read(Registers.PC + 1);
                            break;
                        case OpcodeBytes.LD_SP_NN:
                        {
                            var upper = Memory.Read(Registers.PC + 2) << 8;
                            var lower = Memory.Read(Registers.PC + 1);
                            var address = upper | lower;
                            Registers.SP = (UInt16)address;
                            break;
                        }

                    #endregion

                    // Add immediate to accumulator
                    // A <- A + byte
                    case OpcodeBytes.ADD_A_N:
                        Registers.A = Execute8BitAddition(Registers.A, Memory.Read(Registers.PC+1));
                        break;

                    // Add immediate to accumulator with carry
                    // A <- A + data + CY
                    case OpcodeBytes.ADC_A_N:
                        Registers.A = Execute8BitAddition(Registers.A, Memory.Read(Registers.PC+1), true);
                        break;

                    // Subtract immediate from accumulator
                    // A <- A - data
                    case OpcodeBytes.SUB_N:
                        Registers.A = Execute8BitSubtraction(Registers.A, Memory.Read(Registers.PC+1));
                        break;

                    // Subtract immediate from accumulator with borrow
                    // A <- A - data - CY
                    case OpcodeBytes.SBC_A_N:
                        Registers.A = Execute8BitSubtraction(Registers.A, Memory.Read(Registers.PC+1), true);
                        break;

                    // Logical AND immediate with accumulator
                    // A <- A & data
                    case OpcodeBytes.AND_N:
                        Registers.A = (byte)(Registers.A & Memory.Read(Registers.PC+1));
                        SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                        break;

                    // XOR immediate with accumulator
                    // A <- A ^ data
                    case OpcodeBytes.XOR_N:
                        Registers.A = (byte)(Registers.A ^ Memory.Read(Registers.PC+1));
                        SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                        break;

                    // Logical OR immediate with accumulator
                    // A <- A | data
                    case OpcodeBytes.OR_N:
                        Registers.A = (byte)(Registers.A | Memory.Read(Registers.PC+1));
                        SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                        break;

                    // Compare immediate with accumulator
                    // A - data
                    case OpcodeBytes.CP_N:
                        Execute8BitSubtraction(Registers.A, Memory.Read(Registers.PC+1), false);
                        break;

                #endregion

                #region Direct addressing instructions

                    // Store accumulator direct
                    case OpcodeBytes.LD_MNN_A:
                    {
                        var upper = Memory.Read(Registers.PC + 2) << 8;
                        var lower = Memory.Read(Registers.PC + 1);
                        var address = upper | lower;
                        Memory.Write(address, Registers.A);
                        break;
                    }

                    // Load accumulator direct
                    case OpcodeBytes.LD_A_MNN:
                    {
                        var upper = Memory.Read(Registers.PC + 2) << 8;
                        var lower = Memory.Read(Registers.PC + 1);
                        var address = upper | lower;
                        Registers.A = Memory.Read(address);
                        break;
                    }

                    // Store H and L direct
                    case OpcodeBytes.LD_MNN_HL:
                    {
                        var upper = Memory.Read(Registers.PC + 2) << 8;
                        var lower = Memory.Read(Registers.PC + 1);
                        var address = upper | lower;
                        Memory.Write(address, Registers.L);
                        Memory.Write(address + 1, Registers.H);
                        break;
                    }

                    // Load H and L direct
                    case OpcodeBytes.LD_HL_MNN:
                    {
                        var upper = Memory.Read(Registers.PC + 2) << 8;
                        var lower = Memory.Read(Registers.PC + 1);
                        var address = upper | lower;
                        Registers.L = Memory.Read(address);
                        Registers.H = Memory.Read(address + 1);
                        break;
                    }

                #endregion

                #region Jump instructions

                    // Load program counter
                    case OpcodeBytes.JP_HL:
                        ExecuteJump(Registers.HL);
                        incrementProgramCounter = false;
                        break;

                    // Jump
                    case OpcodeBytes.JP:
                    {
                        ExecuteJump();

                        // Don't increment the program counter because we just updated it to
                        // the given address.
                        incrementProgramCounter = false;

                        break;
                    }

                    // Jump if parity odd
                    case OpcodeBytes.JP_PO:
                    {
                        if (!Flags.ParityOverflow)
                        {
                            ExecuteJump();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if parity even
                    case OpcodeBytes.JP_PE:
                    {
                        if (Flags.ParityOverflow)
                        {
                            ExecuteJump();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if plus/positive
                    case OpcodeBytes.JP_P:
                    {
                        if (!Flags.Sign)
                        {
                            ExecuteJump();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if zero
                    case OpcodeBytes.JP_Z:
                    {
                        if (Flags.Zero)
                        {
                            ExecuteJump();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if not zero
                    case OpcodeBytes.JP_NZ:
                    {
                        if (!Flags.Zero)
                        {
                            ExecuteJump();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if not carry
                    case OpcodeBytes.JP_NC:
                    {
                        if (!Flags.Carry)
                        {
                            ExecuteJump();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if carry
                    case OpcodeBytes.JP_C:
                    {
                        if (Flags.Carry)
                        {
                            ExecuteJump();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if minus/negative
                    case OpcodeBytes.JP_M:
                    {
                        if (Flags.Sign)
                        {
                            ExecuteJump();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Relative jump
                    case OpcodeBytes.JR:
                    {
                        ExecuteRelativeJump((sbyte)Memory.Read(Registers.PC + 1));
                        incrementProgramCounter = false;
                        break;
                    }

                    // Relative jump if zero
                    case OpcodeBytes.JR_Z:
                    {
                        if (Flags.Zero)
                        {
                            ExecuteRelativeJump((sbyte)Memory.Read(Registers.PC + 1));
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Relative jump if not zero
                    case OpcodeBytes.JR_NZ:
                    {
                        if (!Flags.Zero)
                        {
                            ExecuteRelativeJump((sbyte)(sbyte)Memory.Read(Registers.PC + 1));
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Relative jump if carry
                    case OpcodeBytes.JR_C:
                    {
                        if (Flags.Carry)
                        {
                            ExecuteRelativeJump((sbyte)Memory.Read(Registers.PC + 1));
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Relative jump if not carry
                    case OpcodeBytes.JR_NC:
                    {
                        if (!Flags.Carry)
                        {
                            ExecuteRelativeJump((sbyte)Memory.Read(Registers.PC + 1));
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Decrement B and relative jump if not zero
                    case OpcodeBytes.DJNZ:
                    {
                        Registers.B--;

                        if (Registers.B != 0)
                        {
                            ExecuteRelativeJump((sbyte)Memory.Read(Registers.PC + 1));
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                #endregion

                #region Call subroutine instructions

                    case OpcodeBytes.CALL:
                    {
                        ExecuteCall(opcode);

                        // Don't increment the program counter because we just updated it to
                        // the given address.
                        incrementProgramCounter = false;

                        break;
                    }

                    // Call if minus/negative
                    case OpcodeBytes.CALL_M:
                    {
                        if (Flags.Sign)
                        {
                            ExecuteCall(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if party even
                    case OpcodeBytes.CALL_PE:
                    {
                        if (Flags.ParityOverflow)
                        {
                            ExecuteCall(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if carry
                    case OpcodeBytes.CALL_C:
                    {
                        if (Flags.Carry)
                        {
                            ExecuteCall(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if zero
                    case OpcodeBytes.CALL_Z:
                    {
                        if (Flags.Zero)
                        {
                            ExecuteCall(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if plus/positive
                    case OpcodeBytes.CALL_P:
                    {
                        if (!Flags.Sign)
                        {
                            ExecuteCall(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if party odd
                    case OpcodeBytes.CALL_PO:
                    {
                        if (!Flags.ParityOverflow)
                        {
                            ExecuteCall(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if no carry
                    case OpcodeBytes.CALL_NC:
                    {
                        if (!Flags.Carry)
                        {
                            ExecuteCall(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if not zero
                    case OpcodeBytes.CALL_NZ:
                    {
                        if (!Flags.Zero)
                        {
                            ExecuteCall(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                #endregion

                #region Return from subroutine instructions

                    // Return from subroutine
                    case OpcodeBytes.RET:
                    {
                        ExecuteReturn();

                        // Don't increment the program counter because we just updated it to
                        // the given address.
                        incrementProgramCounter = false;

                        break;
                    }

                    // Return if not zero
                    case OpcodeBytes.RET_NZ:
                    {
                        if (!Flags.Zero)
                        {
                            ExecuteReturn();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if zero
                    case OpcodeBytes.RET_Z:
                    {
                        if (Flags.Zero)
                        {
                            ExecuteReturn();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if no carry
                    case OpcodeBytes.RET_NC:
                    {
                        if (!Flags.Carry)
                        {
                            ExecuteReturn();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if carry
                    case OpcodeBytes.RET_C:
                    {
                        if (Flags.Carry)
                        {
                            ExecuteReturn();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if parity odd
                    case OpcodeBytes.RET_PO:
                    {
                        if (!Flags.ParityOverflow)
                        {
                            ExecuteReturn();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if parity even
                    case OpcodeBytes.RET_PE:
                    {
                        if (Flags.ParityOverflow)
                        {
                            ExecuteReturn();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if plus/positive
                    case OpcodeBytes.RET_P:
                    {
                        if (!Flags.Sign)
                        {
                            ExecuteReturn();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if minus/negative
                    case OpcodeBytes.RET_M:
                    {
                        if (Flags.Sign)
                        {
                            ExecuteReturn();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                #endregion

                #region Restart (interrupt handlers) instructions

                    case OpcodeBytes.RST_00:
                    {
                        var returnAddress = (UInt16)(Registers.PC + opcode.Size);
                        ExecuteCall(0x0000, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_08:
                    {
                        var returnAddress = (UInt16)(Registers.PC + opcode.Size);
                        ExecuteCall(0x0008, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_10:
                    {
                        var returnAddress = (UInt16)(Registers.PC + opcode.Size);
                        ExecuteCall(0x0010, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_18:
                    {
                        var returnAddress = (UInt16)(Registers.PC + opcode.Size);
                        ExecuteCall(0x0018, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_20:
                    {
                        var returnAddress = (UInt16)(Registers.PC + opcode.Size);
                        ExecuteCall(0x0020, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_28:
                    {
                        var returnAddress = (UInt16)(Registers.PC + opcode.Size);
                        ExecuteCall(0x0028, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_30:
                    {
                        var returnAddress = (UInt16)(Registers.PC + opcode.Size);
                        ExecuteCall(0x0030, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_38:
                    {
                        var returnAddress = (UInt16)(Registers.PC + opcode.Size);
                        ExecuteCall(0x0038, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                #endregion

                #region Interrupt flip-flop instructions

                    // Enable interrupts
                    case OpcodeBytes.EI:
                        InterruptsEnabled = true;
                        InterruptsEnabledPreviousValue = true;
                        break;

                    // Disable interrupts
                    case OpcodeBytes.DI:
                        InterruptsEnabled = false;
                        InterruptsEnabledPreviousValue = false;
                        break;

                #endregion

                #region Input/Output Instructions

                    // Output accumulator to given device number
                    case OpcodeBytes.OUT_MN_A:
                        OnDeviceWrite?.Invoke(Memory.Read(Registers.PC + 1), Registers.A);
                        break;

                    // Retrieve input from given device number and populate accumulator
                    case OpcodeBytes.IN_A_MN:
                        Registers.A = OnDeviceRead?.Invoke(Memory.Read(Registers.PC + 1)) ?? 0;
                        break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode.Code, Registers.PC));
            }
        }

        #region Additional Opcode Implementation Logic

        private void Execute_DAA()
        {
            // Decimal Adjust Accumulator
            // Used to adjust the accumulator after an addition or subtraction operation
            // to get the binary coded decimal (BCD) value. For example, consider:
            //      LD A, 0x32
            //      LD B, 0x12
            //      ADD A
            //      DAA
            // Hex 32 (dec 50) added to hex 12 (dec 18) would be hex 44 (dec 68) for a
            // normal addition. But for a BCD addition, we're looking at dec 32 + dec 12
            // which is dec 44. After the DAA operation the A register will contain 0x44.

            // NOTE: I originally re-used my implementation from the Intel 8080 CPU core
            // which was implemented based on the 8080 Programmers Manual. While working
            // on the Z80 core I found discrepencies between other emulators (both zemu
            // and 8bitworkshop) as well as the Z80 CPU User Manual. I scrapped my version
            // for one based on 8bitworkshop, which passes the ZEX integration tests.
            // However, I don't think it is completely accurate; still still differs from
            // zemu and I don't think it's 100% accurate with respect to the Z80 Manual.

            var result = Registers.A;

            if (Flags.HalfCarry || ((Registers.A & 0x0F) > 9))
                result = (byte)(Flags.Subtract ? result - 0x06 : result + 0x06);

            if (Flags.Carry || (Registers.A > 0x99))
                result = (byte)(Flags.Subtract ? result - 0x60 : result + 0x60);

            SetSignZeroAndParityFlags(result);
            Flags.HalfCarry = ((Registers.A & 0x10) ^ (result & 0x10)) > 0 ? true : false;
            Flags.Carry = (Flags.Carry || (Registers.A > 0x99)) ? true : false;

            Registers.A = result;
        }

        private void ExecuteJump()
        {
            var upper = Memory.Read(Registers.PC + 2) << 8;
            var lower = Memory.Read(Registers.PC + 1);
            var address = (UInt16)(upper | lower);

            ExecuteJump(address);
        }

        private void ExecuteJump(UInt16 address)
        {
            #region CPU Diagnostics Debugging Mode

            // This is special case code only for running the CPU diagnostic program.
            // See the EnableDiagnosticsMode flag for more details.
            if (Config.EnableDiagnosticsMode && address == 0x00)
            {
                // This is a CALL 0x00 which returns control to CP/M.
                this.Halted = true;
                return;
            }

            #endregion

            Registers.PC = address;
        }

        private void ExecuteRelativeJump(sbyte relativeAddress)
        {
            // Calculate the address to jump to using the given relative address which
            // is signed.
            int offset = (int)unchecked(relativeAddress);
            var address = Registers.PC + offset;

            // The assembler adds the size of the opcode to the offset address during
            // assembly, attempting to account for the fact that normal hardware would
            // increment the program counter this amount. However, in this emulator we
            // did do not increment the program counter until after the execute loop,
            // and we don't even do it for call/jump scenarios. Therefore, account for
            // the assembler's adjustment and add the opcode size back in.
            address += Opcodes.JR.Size;

            ExecuteJump((UInt16)address);
        }

        private void ExecuteCall(Opcode opcode)
        {
            var returnAddress = (UInt16)(Registers.PC + opcode.Size);

            var upper = Memory.Read(Registers.PC + 2) << 8;
            var lower = Memory.Read(Registers.PC + 1);
            var address = (UInt16)(upper | lower);

            ExecuteCall(address, returnAddress);
        }

        private void ExecuteCall(UInt16 address, UInt16 returnAddress)
        {
            // We need to break this into two bytes so we can push it onto the stack.
            var returnAddressUpper = (byte)((returnAddress & 0xFF00) >> 8);
            var returnAddressLower = (byte)(returnAddress & 0x00FF);

            // Push the return address onto the stack.
            Memory.Write(Registers.SP - 1, returnAddressUpper);
            Memory.Write(Registers.SP - 2, returnAddressLower);
            Registers.SP--;
            Registers.SP--;

            #region CPU Diagnostics Debugging Mode

            // This is special case code only for running the CPU diagnostic program.
            // See the EnableDiagnosticsMode flag for more details.

            if (Config.EnableDiagnosticsMode && address == 0x05)
            {
                // This is a CALL 0x05 which is a CP/M call.
                // Register C is a flag, a value of 9 indicates a string print using
                // the value of register pair DE as a pointer to the string to print
                // which is terminated by the $ character. If register C is a value
                // of 2 it is a character print based on the value of register A.
                if (OnCPUDiagDebugEvent != null)
                    OnCPUDiagDebugEvent(Registers.C);

                ExecuteReturn();
                return;
            }

            #endregion

            // Jump to the given address.
            Registers.PC = address;
        }

        private void ExecuteReturn()
        {
            // Pop the return address off of the stack.
            var returnAddressUpper = Memory.Read(Registers.SP + 1) << 8;
            var returnAddressLower = Memory.Read(Registers.SP);
            var returnAddress = (UInt16)(returnAddressUpper | returnAddressLower);
            Registers.SP++;
            Registers.SP++;

            Registers.PC = returnAddress;
        }

        private byte Execute8BitAddition(byte addend, byte augend, bool addCarryFlag = false)
        {
            var sum = addend + augend;

            if (addCarryFlag && Flags.Carry)
                sum += 1;

            var carryOccurred = sum > 255;

            if (carryOccurred)
                sum = sum - 256;

            SetFlagsFrom8BitAddition(addend, augend, addCarryFlag, affectsCarryFlag: true);

            return (byte)sum;
        }

        private UInt16 Execute16BitAddition(UInt16 addend, UInt16 augend, bool addCarryFlag = false)
        {
            var sum = addend + augend;

            var carryOccurred = sum > 65535;

            if (addCarryFlag && Flags.Carry)
                sum += 1;

            if (carryOccurred)
                sum = sum - 65536;

            // This is odd, but for 16-bit additions the only time we set all six flags is we we're doing addition
            // for the "add with carry" opcodes. Otherwise, we only set three flags (N/C/H). This is documented
            // in the Z80 CPU user manual as well as here: http://clrhome.org/table/#add
            var setAllFlags = addCarryFlag;

            SetFlagsFrom16BitAddition(addend, augend, addCarryFlag, setAllFlags);

            return (UInt16)sum;
        }

        private byte Execute8BitSubtraction(byte minuend, byte subtrahend, bool subtractCarryFlag = false, bool affectsCarryFlag = true)
        {
            var borrowOccurred = (subtractCarryFlag && Flags.Carry)
                ? subtrahend >= minuend // Account for the extra minus one from the carry flag subtraction.
                : subtrahend > minuend;

            var difference = minuend - subtrahend;

            if (subtractCarryFlag && Flags.Carry)
                difference -= 1;

            if (borrowOccurred)
                difference = 256 + difference;

            SetFlagsFrom8BitSubtraction(minuend, subtrahend, subtractCarryFlag, affectsCarryFlag: affectsCarryFlag);

            return (byte)difference;
        }

        private UInt16 Execute16BitSubtraction(UInt16 minuend, UInt16 subtrahend, bool subtractCarryFlag = false)
        {
            var borrowOccurred = (subtractCarryFlag && Flags.Carry)
                ? subtrahend >= minuend // Account for the extra minus one from the carry flag subtraction.
                : subtrahend > minuend;

            var difference = minuend - subtrahend;

            if (subtractCarryFlag && Flags.Carry)
                difference -= 1;

            if (borrowOccurred)
                difference = 65536 + difference;

            SetFlagsFrom16BitSubtraction(minuend, subtrahend, subtractCarryFlag);

            return (UInt16)difference;
        }

        #endregion
    }
}
