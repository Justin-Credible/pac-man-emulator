using System;

namespace JustinCredible.ZilogZ80
{
    // TODO: Update mnemonics on opcode method calls.
    public partial class CPU
    {
        private void ExecuteStandardOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            incrementProgramCounter = true;
            useAlternateCycleCount = false;

            switch (opcode.Code)
            {
                case OpcodeBytes.HALT:
                    Finished = true;
                    incrementProgramCounter = false;
                    break;

                case OpcodeBytes.NOP:
                    break;

                #region Carry bit instructions

                    case OpcodeBytes.CCF:
                        Flags.Carry = !Flags.Carry;
                        break;

                    case OpcodeBytes.SCF:
                        Flags.Carry = true;
                        break;

                #endregion

                #region Single register instructions

                    #region INC r - Increment Register or Memory

                        case OpcodeBytes.INC_B:
                            Registers.B++;
                            SetFlags(carry: false, result: Registers.B, subtract: false);
                            break;
                        case OpcodeBytes.INC_C:
                            Registers.C++;
                            SetFlags(carry: false, result: Registers.C, subtract: false);
                            break;
                        case OpcodeBytes.INC_D:
                            Registers.D++;
                            SetFlags(carry: false, result: Registers.D, subtract: false);
                            break;
                        case OpcodeBytes.INC_E:
                            Registers.E++;
                            SetFlags(carry: false, result: Registers.E, subtract: false);
                            break;
                        case OpcodeBytes.INC_H:
                            Registers.H++;
                            SetFlags(carry: false, result: Registers.H, subtract: false);
                            break;
                        case OpcodeBytes.INC_L:
                            Registers.L++;
                            SetFlags(carry: false, result: Registers.L, subtract: false);
                            break;
                        case OpcodeBytes.INC_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value++;
                            WriteMemory(Registers.HL, value);
                            SetFlags(carry: false, result: ReadMemory(Registers.HL), subtract: false);
                            break;
                        }
                        case OpcodeBytes.INC_A:
                            Registers.A++;
                            SetFlags(carry: false, result: Registers.A, subtract: false);
                            break;

                    #endregion

                    #region DEC r - Decrement Register or Memory

                        case OpcodeBytes.DEC_B:
                            Registers.B--;
                            SetFlags(carry: false, result: Registers.B, subtract: true);
                            break;
                        case OpcodeBytes.DEC_C:
                            Registers.C--;
                            SetFlags(carry: false, result: Registers.C, subtract: true);
                            break;
                        case OpcodeBytes.DEC_D:
                            Registers.D--;
                            SetFlags(carry: false, result: Registers.D, subtract: true);
                            break;
                        case OpcodeBytes.DEC_E:
                            Registers.E--;
                            SetFlags(carry: false, result: Registers.E, subtract: true);
                            break;
                        case OpcodeBytes.DEC_H:
                            Registers.H--;
                            SetFlags(carry: false, result: Registers.H, subtract: true);
                            break;
                        case OpcodeBytes.DEC_L:
                            Registers.L--;
                            SetFlags(carry: false, result: Registers.L, subtract: true);
                            break;
                        case OpcodeBytes.DEC_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value--;
                            WriteMemory(Registers.HL, value);
                            SetFlags(carry: false, result: ReadMemory(Registers.HL), subtract: true);
                            break;
                        }
                        case OpcodeBytes.DEC_A:
                            Registers.A--;
                            SetFlags(carry: false, result: Registers.A, subtract: true);
                            break;

                    #endregion

                    /** Compliment Accumulator */
                    case OpcodeBytes.CPL:
                        Registers.A = (byte)~Registers.A;
                        break;

                    /** Decimal Adjust Accumulator */
                    case OpcodeBytes.DAA:
                        ExecuteDAA();
                        break;

                #endregion

                #region Data transfer instructions

                    #region LD (rr), A - Store accumulator

                        case OpcodeBytes.LD_MBC_A:
                            WriteMemory(Registers.BC, Registers.A);
                            break;
                        case OpcodeBytes.LD_MDE_A:
                            WriteMemory(Registers.DE, Registers.A);
                            break;

                    #endregion

                    #region LD A, (rr) - Load accumulator

                        case OpcodeBytes.LD_A_MBC:
                            Registers.A = ReadMemory(Registers.BC);
                            break;
                        case OpcodeBytes.LD_A_MDE:
                            Registers.A = ReadMemory(Registers.DE);
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
                            Registers.B = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.LD_C_MHL:
                            Registers.C = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.LD_D_MHL:
                            Registers.D = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.LD_E_MHL:
                            Registers.E = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.LD_H_MHL:
                            Registers.H = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.LD_L_MHL:
                            Registers.L = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.LD_A_MHL:
                            Registers.A = ReadMemory(Registers.HL);
                            break;

                        #endregion

                        #region LD (HL), r (from register to memory)

                        case OpcodeBytes.LD_MHL_B:
                            WriteMemory(Registers.HL, Registers.B);
                            break;
                        case OpcodeBytes.LD_MHL_C:
                            WriteMemory(Registers.HL, Registers.C);
                            break;
                        case OpcodeBytes.LD_MHL_D:
                            WriteMemory(Registers.HL, Registers.D);
                            break;
                        case OpcodeBytes.LD_MHL_E:
                            WriteMemory(Registers.HL, Registers.E);
                            break;
                        case OpcodeBytes.LD_MHL_H:
                            WriteMemory(Registers.HL, Registers.H);
                            break;
                        case OpcodeBytes.LD_MHL_L:
                            WriteMemory(Registers.HL, Registers.L);
                            break;
                        case OpcodeBytes.LD_MHL_A:
                            WriteMemory(Registers.HL, Registers.A);
                            break;

                        #endregion

                    #endregion

                #endregion

                #region Register or memory to accumulator instructions

                    #region ADD r - Add register or memory to accumulator

                        case OpcodeBytes.ADD_A_B:
                            ExecuteADD(Registers.B);
                            break;
                        case OpcodeBytes.ADD_A_C:
                            ExecuteADD(Registers.C);
                            break;
                        case OpcodeBytes.ADD_A_D:
                            ExecuteADD(Registers.D);
                            break;
                        case OpcodeBytes.ADD_A_E:
                            ExecuteADD(Registers.E);
                            break;
                        case OpcodeBytes.ADD_A_H:
                            ExecuteADD(Registers.H);
                            break;
                        case OpcodeBytes.ADD_A_L:
                            ExecuteADD(Registers.L);
                            break;
                        case OpcodeBytes.ADD_A_MHL:
                            ExecuteADD(ReadMemory(Registers.HL));
                            break;
                        case OpcodeBytes.ADD_A_A:
                            ExecuteADD(Registers.A);
                            break;

                    #endregion

                    #region SUB r - Subtract register or memory from accumulator

                        case OpcodeBytes.SUB_B:
                            ExecuteSUB(Registers.B);
                            break;
                        case OpcodeBytes.SUB_C:
                            ExecuteSUB(Registers.C);
                            break;
                        case OpcodeBytes.SUB_D:
                            ExecuteSUB(Registers.D);
                            break;
                        case OpcodeBytes.SUB_E:
                            ExecuteSUB(Registers.E);
                            break;
                        case OpcodeBytes.SUB_H:
                            ExecuteSUB(Registers.H);
                            break;
                        case OpcodeBytes.SUB_L:
                            ExecuteSUB(Registers.L);
                            break;
                        case OpcodeBytes.SUB_MHL:
                            ExecuteSUB(ReadMemory(Registers.HL));
                            break;
                        case OpcodeBytes.SUB_A:
                            ExecuteSUB(Registers.A);
                            break;

                    #endregion

                    #region AND r - Logical AND register or memory with accumulator

                        case OpcodeBytes.AND_B:
                            Registers.A = (byte)(Registers.A & Registers.B);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.AND_C:
                            Registers.A = (byte)(Registers.A & Registers.C);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.AND_D:
                            Registers.A = (byte)(Registers.A & Registers.D);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.AND_E:
                            Registers.A = (byte)(Registers.A & Registers.E);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.AND_H:
                            Registers.A = (byte)(Registers.A & Registers.H);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.AND_L:
                            Registers.A = (byte)(Registers.A & Registers.L);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.AND_MHL:
                            Registers.A = (byte)(Registers.A & ReadMemory(Registers.HL));
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.AND_A:
                            Registers.A = (byte)(Registers.A & Registers.A);
                            SetFlags(carry: false, result: Registers.A);
                            break;

                    #endregion

                    #region OR r - Logical OR register or memory with accumulator

                        case OpcodeBytes.OR_B:
                            Registers.A = (byte)(Registers.A | Registers.B);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.OR_C:
                            Registers.A = (byte)(Registers.A | Registers.C);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.OR_D:
                            Registers.A = (byte)(Registers.A | Registers.D);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.OR_E:
                            Registers.A = (byte)(Registers.A | Registers.E);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.OR_H:
                            Registers.A = (byte)(Registers.A | Registers.H);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.OR_L:
                            Registers.A = (byte)(Registers.A | Registers.L);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.OR_MHL:
                            Registers.A = (byte)(Registers.A | ReadMemory(Registers.HL));
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.OR_A:
                            Registers.A = (byte)(Registers.A | Registers.A);
                            SetFlags(carry: false, result: Registers.A);
                            break;

                    #endregion

                    #region ADC A, r - Add register or memory to accumulator with carry

                        case OpcodeBytes.ADC_A_B:
                            ExecuteADD(Registers.B, true);
                            break;
                        case OpcodeBytes.ADC_A_C:
                            ExecuteADD(Registers.C, true);
                            break;
                        case OpcodeBytes.ADC_A_D:
                            ExecuteADD(Registers.D, true);
                            break;
                        case OpcodeBytes.ADC_A_E:
                            ExecuteADD(Registers.E, true);
                            break;
                        case OpcodeBytes.ADC_A_H:
                            ExecuteADD(Registers.H, true);
                            break;
                        case OpcodeBytes.ADC_A_L:
                            ExecuteADD(Registers.L, true);
                            break;
                        case OpcodeBytes.ADC_A_MHL:
                            ExecuteADD(ReadMemory(Registers.HL), true);
                            break;
                        case OpcodeBytes.ADC_A_A:
                            ExecuteADD(Registers.A, true);
                            break;

                    #endregion

                    #region SBC A, r - Subtract register or memory from accumulator with borrow

                        case OpcodeBytes.SBC_A_B:
                            ExecuteSUB(Registers.B, true);
                            break;
                        case OpcodeBytes.SBC_A_C:
                            ExecuteSUB(Registers.C, true);
                            break;
                        case OpcodeBytes.SBC_A_D:
                            ExecuteSUB(Registers.D, true);
                            break;
                        case OpcodeBytes.SBC_A_E:
                            ExecuteSUB(Registers.E, true);
                            break;
                        case OpcodeBytes.SBC_A_H:
                            ExecuteSUB(Registers.H, true);
                            break;
                        case OpcodeBytes.SBC_A_L:
                            ExecuteSUB(Registers.L, true);
                            break;
                        case OpcodeBytes.SBC_A_MHL:
                            ExecuteSUB(ReadMemory(Registers.HL), true);
                            break;
                        case OpcodeBytes.SBC_A_A:
                            ExecuteSUB(Registers.A, true);
                            break;

                    #endregion

                    #region XOR r - Logical XOR register or memory with accumulator

                        case OpcodeBytes.XOR_B:
                            Registers.A = (byte)(Registers.A ^ Registers.B);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.XOR_C:
                            Registers.A = (byte)(Registers.A ^ Registers.C);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.XOR_D:
                            Registers.A = (byte)(Registers.A ^ Registers.D);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.XOR_E:
                            Registers.A = (byte)(Registers.A ^ Registers.E);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.XOR_H:
                            Registers.A = (byte)(Registers.A ^ Registers.H);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.XOR_L:
                            Registers.A = (byte)(Registers.A ^ Registers.L);
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.XOR_MHL:
                            Registers.A = (byte)(Registers.A ^ ReadMemory(Registers.HL));
                            SetFlags(carry: false, result: Registers.A);
                            break;
                        case OpcodeBytes.XOR_A:
                            Registers.A = (byte)(Registers.A ^ Registers.A);
                            SetFlags(carry: false, result: Registers.A);
                            break;

                    #endregion

                    #region CP r - Compare register or memory with accumulator

                        case OpcodeBytes.CP_B:
                            ExecuteSUB(Registers.B, false, false);
                            break;
                        case OpcodeBytes.CP_C:
                            ExecuteSUB(Registers.C, false, false);
                            break;
                        case OpcodeBytes.CP_D:
                            ExecuteSUB(Registers.D, false, false);
                            break;
                        case OpcodeBytes.CP_E:
                            ExecuteSUB(Registers.E, false, false);
                            break;
                        case OpcodeBytes.CP_H:
                            ExecuteSUB(Registers.H, false, false);
                            break;
                        case OpcodeBytes.CP_L:
                            ExecuteSUB(Registers.L, false, false);
                            break;
                        case OpcodeBytes.CP_MHL:
                            ExecuteSUB(ReadMemory(Registers.HL), false, false);
                            break;
                        case OpcodeBytes.CP_A:
                            ExecuteSUB(Registers.A, false, false);
                            break;

                    #endregion

                #endregion

            #region Exchange registers

                /** Exchange BC, DE, HL <-> BC', DE', HL' */
                case OpcodeBytes.EXX:
                {
                    // Save off shadow register values.
                    byte shadowB = ShadowRegisters.B;
                    byte shadowC = ShadowRegisters.C;
                    byte shadowD = ShadowRegisters.D;
                    byte shadowE = ShadowRegisters.E;
                    byte shadowH = ShadowRegisters.H;
                    byte shadowL = ShadowRegisters.L;

                    // Copy regular register values to shadow registers.
                    ShadowRegisters.B = Registers.B;
                    ShadowRegisters.C = Registers.C;
                    ShadowRegisters.D = Registers.D;
                    ShadowRegisters.E = Registers.E;
                    ShadowRegisters.H = Registers.H;
                    ShadowRegisters.L = Registers.L;

                    // Copy shadow register values to regular registers.
                    Registers.B = shadowB;
                    Registers.C = shadowC;
                    Registers.D = shadowD;
                    Registers.E = shadowE;
                    Registers.H = shadowH;
                    Registers.L = shadowL;

                    break;
                }

                /** Exchange A, F <-> A', F' */
                case OpcodeBytes.EX_AF_AFP:
                {
                    // Save off shadow register values.
                    byte shadowA = ShadowRegisters.A;
                    byte shadowF = ShadowFlags.ToByte();

                    // Copy regular register values to shadow registers.
                    ShadowRegisters.A = Registers.A;
                    ShadowFlags.SetFromByte(Flags.ToByte());

                    // Copy shadow register values to regular registers.
                    Registers.A = shadowA;
                    Flags.SetFromByte(shadowF);

                    break;
                }

            #endregion

                #region Rotate accumulator instructions

                    // Rotate accumulator left
                    // A = A << 1; bit 0 = prev bit 7; CY = prev bit 7
                    case OpcodeBytes.RLCA:
                        ExecuteRotateAccumulator(left: true);
                        break;

                    // Rotate accumulator right
                    // A = A >> 1; bit 7 = prev bit 0; CY = prev bit 0
                    case OpcodeBytes.RRCA:
                        ExecuteRotateAccumulator(left: false);
                        break;

                    // Rotate accumulator left through carry
                    // A = A << 1; bit 0 = prev CY; CY = prev bit 7
                    case OpcodeBytes.RLA:
                        ExecuteRotateAccumulator(left: true, rotateThroughCarry: true);
                        break;

                    // Rotate accumulator right through carry
                    // A = A >> 1; bit 7 = prev CY; CY = prev bit 0
                    case OpcodeBytes.RRA:
                        ExecuteRotateAccumulator(left: false, rotateThroughCarry: true);
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
                            StackPointer++;
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
                            StackPointer--;
                            break;

                    #endregion

                    #region PUSH rr - Push data onto the stack

                        case OpcodeBytes.PUSH_BC:
                            WriteMemory(StackPointer - 1, Registers.B);
                            WriteMemory(StackPointer - 2, Registers.C);
                            StackPointer = (UInt16)(StackPointer - 2);
                            break;
                        case OpcodeBytes.PUSH_DE:
                            WriteMemory(StackPointer - 1, Registers.D);
                            WriteMemory(StackPointer - 2, Registers.E);
                            StackPointer = (UInt16)(StackPointer - 2);
                            break;
                        case OpcodeBytes.PUSH_HL:
                            WriteMemory(StackPointer - 1, Registers.H);
                            WriteMemory(StackPointer - 2, Registers.L);
                            StackPointer = (UInt16)(StackPointer - 2);
                            break;
                        case OpcodeBytes.PUSH_AF:
                            WriteMemory(StackPointer - 1, Registers.A);
                            WriteMemory(StackPointer - 2, Flags.ToByte());
                            StackPointer = (UInt16)(StackPointer - 2);
                            break;

                    #endregion

                    #region POP rr - Pop data off of the stack

                        case OpcodeBytes.POP_BC:
                            Registers.B = ReadMemory(StackPointer + 1);
                            Registers.C = ReadMemory(StackPointer);
                            StackPointer = (UInt16)(StackPointer + 2);
                            break;
                        case OpcodeBytes.POP_DE:
                            Registers.D = ReadMemory(StackPointer + 1);
                            Registers.E = ReadMemory(StackPointer);
                            StackPointer = (UInt16)(StackPointer + 2);
                            break;
                        case OpcodeBytes.POP_HL:
                            Registers.H = ReadMemory(StackPointer + 1);
                            Registers.L = ReadMemory(StackPointer);
                            StackPointer = (UInt16)(StackPointer + 2);
                            break;
                        case OpcodeBytes.POP_AF:
                            Registers.A = ReadMemory(StackPointer + 1);
                            Flags.SetFromByte(ReadMemory(StackPointer));
                            StackPointer = (UInt16)(StackPointer + 2);
                            break;

                    #endregion

                    #region ADD HL, rr - Double (16-bit) add

                        case OpcodeBytes.ADD_HL_BC:
                            ExecuteDAD(Registers.BC);
                            break;
                        case OpcodeBytes.ADD_HL_DE:
                            ExecuteDAD(Registers.DE);
                            break;
                        case OpcodeBytes.ADD_HL_HL:
                            ExecuteDAD(Registers.HL);
                            break;
                        case OpcodeBytes.ADD_HL_SP:
                            ExecuteDAD(StackPointer);
                            break;

                    #endregion

                    // Load SP from H and L
                    case OpcodeBytes.LD_SP_HL:
                        StackPointer = Registers.HL;
                        break;

                    // Exchange stack
                    //  L <-> (SP); H <-> (SP+1)
                    case OpcodeBytes.EX_MSP_HL:
                    {
                        var oldL = Registers.L;
                        var oldH = Registers.H;
                        Registers.L = ReadMemory(StackPointer);
                        WriteMemory(StackPointer, oldL);
                        Registers.H = ReadMemory(StackPointer+1);
                        WriteMemory(StackPointer+1, oldH);
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
                            Registers.B = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LD_C_N:
                            Registers.C = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LD_D_N:
                            Registers.D = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LD_E_N:
                            Registers.E = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LD_H_N:
                            Registers.H = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LD_L_N:
                            Registers.L = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LD_MHL_N:
                            WriteMemory(Registers.HL, ReadMemory(ProgramCounter + 1));
                            break;
                        case OpcodeBytes.LD_A_N:
                            Registers.A = ReadMemory(ProgramCounter + 1);
                            break;

                    #endregion

                    #region LD rr, nn - Load register pair immediate

                        case OpcodeBytes.LD_BC_NN:
                            Registers.B = ReadMemory(ProgramCounter + 2);
                            Registers.C = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LD_DE_NN:
                            Registers.D = ReadMemory(ProgramCounter + 2);
                            Registers.E = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LD_HL_NN:
                            Registers.H = ReadMemory(ProgramCounter + 2);
                            Registers.L = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LD_SP_NN:
                        {
                            var upper = ReadMemory(ProgramCounter + 2) << 8;
                            var lower = ReadMemory(ProgramCounter + 1);
                            var address = upper | lower;
                            StackPointer = (UInt16)address;
                            break;
                        }

                    #endregion

                    // Add immediate to accumulator
                    // A <- A + byte
                    case OpcodeBytes.ADD_A_N:
                        ExecuteADD(ReadMemory(ProgramCounter+1));
                        break;

                    // Add immediate to accumulator with carry
                    // A <- A + data + CY
                    case OpcodeBytes.ADC_A_N:
                        ExecuteADD(ReadMemory(ProgramCounter+1), true);
                        break;

                    // Subtract immediate from accumulator
                    // A <- A - data
                    case OpcodeBytes.SUB_N:
                        ExecuteSUB(ReadMemory(ProgramCounter+1));
                        break;

                    // Subtract immediate from accumulator with borrow
                    // A <- A - data - CY
                    case OpcodeBytes.SBC_A_N:
                        ExecuteSUB(ReadMemory(ProgramCounter+1), true);
                        break;

                    // Logical AND immediate with accumulator
                    // A <- A & data
                    case OpcodeBytes.AND_N:
                        Registers.A = (byte)(Registers.A & ReadMemory(ProgramCounter+1));
                        SetFlags(carry: false, result: Registers.A);
                        break;

                    // XOR immediate with accumulator
                    // A <- A ^ data
                    case OpcodeBytes.XOR_N:
                        Registers.A = (byte)(Registers.A ^ ReadMemory(ProgramCounter+1));
                        SetFlags(carry: false, result: Registers.A);
                        break;

                    // Logical OR immediate with accumulator
                    // A <- A | data
                    case OpcodeBytes.OR_N:
                        Registers.A = (byte)(Registers.A | ReadMemory(ProgramCounter+1));
                        SetFlags(carry: false, result: Registers.A);
                        break;

                    // Compare immediate with accumulator
                    // A - data
                    case OpcodeBytes.CP_N:
                        ExecuteSUB(ReadMemory(ProgramCounter+1), false, false);
                        break;

                #endregion

                #region Direct addressing instructions

                    // Store accumulator direct
                    case OpcodeBytes.LD_MNN_A:
                    {
                        var upper = ReadMemory(ProgramCounter + 2) << 8;
                        var lower = ReadMemory(ProgramCounter + 1);
                        var address = upper | lower;
                        WriteMemory(address, Registers.A);
                        break;
                    }

                    // Load accumulator direct
                    case OpcodeBytes.LD_A_MNN:
                    {
                        var upper = ReadMemory(ProgramCounter + 2) << 8;
                        var lower = ReadMemory(ProgramCounter + 1);
                        var address = upper | lower;
                        Registers.A = ReadMemory(address);
                        break;
                    }

                    // Store H and L direct
                    case OpcodeBytes.LD_MNN_HL:
                    {
                        var upper = ReadMemory(ProgramCounter + 2) << 8;
                        var lower = ReadMemory(ProgramCounter + 1);
                        var address = upper | lower;
                        WriteMemory(address, Registers.L);
                        WriteMemory(address + 1, Registers.H);
                        break;
                    }

                    // Load H and L direct
                    case OpcodeBytes.LD_HL_MNN:
                    {
                        var upper = ReadMemory(ProgramCounter + 2) << 8;
                        var lower = ReadMemory(ProgramCounter + 1);
                        var address = upper | lower;
                        Registers.L = ReadMemory(address);
                        Registers.H = ReadMemory(address + 1);
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
                        if (!Flags.Parity)
                        {
                            ExecuteJump();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if parity even
                    case OpcodeBytes.JP_PE:
                    {
                        if (Flags.Parity)
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
                        ExecuteRelativeJump((sbyte)ReadMemory(ProgramCounter + 1));
                        incrementProgramCounter = false;
                        break;
                    }

                    // Relative jump if zero
                    case OpcodeBytes.JR_Z:
                    {
                        if (Flags.Zero)
                        {
                            ExecuteRelativeJump((sbyte)ReadMemory(ProgramCounter + 1));
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
                            ExecuteRelativeJump((sbyte)(sbyte)ReadMemory(ProgramCounter + 1));
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
                            ExecuteRelativeJump((sbyte)ReadMemory(ProgramCounter + 1));
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
                            ExecuteRelativeJump((sbyte)ReadMemory(ProgramCounter + 1));
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
                            ExecuteRelativeJump((sbyte)ReadMemory(ProgramCounter + 1));
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
                        ExecuteCALL(opcode);

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
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if party even
                    case OpcodeBytes.CALL_PE:
                    {
                        if (Flags.Parity)
                        {
                            ExecuteCALL(opcode);
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
                            ExecuteCALL(opcode);
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
                            ExecuteCALL(opcode);
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
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if party odd
                    case OpcodeBytes.CALL_PO:
                    {
                        if (!Flags.Parity)
                        {
                            ExecuteCALL(opcode);
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
                            ExecuteCALL(opcode);
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
                            ExecuteCALL(opcode);
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
                        ExecuteRET();

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
                            ExecuteRET();
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
                            ExecuteRET();
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
                            ExecuteRET();
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
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if parity odd
                    case OpcodeBytes.RET_PO:
                    {
                        if (!Flags.Parity)
                        {
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if parity even
                    case OpcodeBytes.RET_PE:
                    {
                        if (Flags.Parity)
                        {
                            ExecuteRET();
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
                            ExecuteRET();
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
                            ExecuteRET();
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
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0000, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_08:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0008, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_10:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0010, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_18:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0018, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_20:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0020, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_28:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0028, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_30:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0030, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_38:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0038, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                #endregion

                #region Interrupt flip-flop instructions

                    // Enable interrupts
                    case OpcodeBytes.EI:
                        InterruptsEnabled = true;
                        break;

                    // Disable interrupts
                    case OpcodeBytes.DI:
                        InterruptsEnabled = false;
                        break;

                #endregion

                #region Input/Output Instructions

                    // Output accumulator to given device number
                    case OpcodeBytes.OUT_MN_A:
                        OnDeviceWrite?.Invoke(ReadMemory(ProgramCounter + 1), Registers.A);
                        break;

                    // Retrieve input from given device number and populate accumulator
                    case OpcodeBytes.IN_A_MN:
                        Registers.A = (byte)OnDeviceRead?.Invoke(ReadMemory(ProgramCounter + 1));
                        break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, ProgramCounter));
            }
        }

        #region Opcodes - Additional Implementations

        private void ExecuteDAA()
        {
            // Decimal Adjust Accumulator
            // This occurs in two steps below. Step descriptions are taken directly
            // from the Z80 Programmers Manual. See the manual for more details on
            // the instruction and how it can be used.

            // Step 1:
            // If the least significant four bits of the accumulator represents a number
            // greater than 9, or if the Auxiliary Carry bit is equal to one, the accumulator
            // is incremented by six. Otherwise, no incrementing occurs.

            // If a carry out of the least significant four bits occurs during Step (1),
            // the Auxiliary Carry bit is set; otherwise it is reset.

            var newAuxCarryValue = false;

            int lsb = Registers.A & 0x0F;

            if (lsb > 9 || Flags.AuxCarry)
            {
                Registers.A += 6;
                newAuxCarryValue = lsb >= 9;
            }

            // Step 2:
            // If the most significant four bits of the accumulator now represent a number
            // greater than 9, or if the normal carry bit is equal to one, the most significant
            // four bits of the accumulator are incremented by six. Otherwise, no incrementing occurs.

            // If a carry out of the most significant four bits occurs during Step (2). the normal
            // Carry bit is set; otherwise, it is unaffected.

            var newCarryValue = false;

            int msb = (Registers.A & 0xF0) >> 4;

            if (msb > 9 || Flags.Carry)
            {
                newCarryValue = msb >= 9;
                msb += 6;

                var newValue = ((msb << 4) & 0xF0) | (Registers.A & 0x0F);
                Registers.A = (byte)newValue;
            }

            // Update the condition flags.
            SetFlags(carry: newCarryValue, result: Registers.A, auxCarry: newAuxCarryValue);
        }

        private void ExecuteJump()
        {
            var upper = ReadMemory(ProgramCounter + 2) << 8;
            var lower = ReadMemory(ProgramCounter + 1);
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
                this.Finished = true;
                return;
            }

            #endregion

            ProgramCounter = address;
        }

        private void ExecuteRelativeJump(sbyte relativeAddress)
        {
            // Calculate the address to jump to using the given relative address which
            // is signed.
            int offset = (int)unchecked(relativeAddress);
            var address = ProgramCounter + offset;

            // The assembler adds the size of the opcode to the offset address during
            // assembly, attempting to account for the fact that normal hardware would
            // increment the program counter this amount. However, in this emulator we
            // did do not increment the program counter until after the execute loop,
            // and we don't even do it for call/jump scenarios. Therefore, account for
            // the assembler's adjustment and add the opcode size back in.
            address += Opcodes.JR.Size;

            ExecuteJump((UInt16)address);
        }

        private void ExecuteCALL(Opcode opcode)
        {
            var returnAddress = (UInt16)(ProgramCounter + opcode.Size);

            var upper = ReadMemory(ProgramCounter + 2) << 8;
            var lower = ReadMemory(ProgramCounter + 1);
            var address = (UInt16)(upper | lower);

            ExecuteCALL(address, returnAddress);
        }

        private void ExecuteCALL(UInt16 address, UInt16 returnAddress)
        {
            // We need to break this into two bytes so we can push it onto the stack.
            var returnAddressUpper = (byte)((returnAddress & 0xFF00) >> 8);
            var returnAddressLower = (byte)(returnAddress & 0x00FF);

            // Push the return address onto the stack.
            WriteMemory(StackPointer - 1, returnAddressUpper);
            WriteMemory(StackPointer - 2, returnAddressLower);
            StackPointer--;
            StackPointer--;

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

                ExecuteRET();
                return;
            }

            #endregion

            // Jump to the given address.
            ProgramCounter = address;
        }

        private void ExecuteRET()
        {
            // Pop the return address off of the stack.
            var returnAddressUpper = ReadMemory(StackPointer + 1) << 8;
            var returnAddressLower = ReadMemory(StackPointer);
            var returnAddress = (UInt16)(returnAddressUpper | returnAddressLower);
            StackPointer++;
            StackPointer++;

            ProgramCounter = returnAddress;
        }

        private void ExecuteLD(Register dest, Register source)
        {
            Registers[dest] = Registers[source];
        }

        private void ExecuteADD(byte value, bool addCarryFlag = false)
        {
            var result = Registers.A + value;

            if (addCarryFlag && Flags.Carry)
                result += 1;

            var carryOccurred = result > 255;

            if (carryOccurred)
                result = result - 256;

            SetFlags(carry: carryOccurred, result: (byte)result, subtract: false);

            Registers.A = (byte)result;
        }

        private void ExecuteSUB(byte value, bool subtractCarryFlag = false, bool updateAccumulator = true)
        {
            var borrowOccurred = (subtractCarryFlag && Flags.Carry)
                ? value >= Registers.A // Account for the extra minus one from the carry flag subtraction.
                : value > Registers.A;

            var result = Registers.A - value;

            if (subtractCarryFlag && Flags.Carry)
                result -= 1;

            if (borrowOccurred)
                result = 256 + result;

            SetFlags(carry: borrowOccurred, result: (byte)result, subtract: true);

            if (updateAccumulator)
                Registers.A = (byte)result;
        }

        private void ExecuteDAD(UInt16 value)
        {
            var result = Registers.HL + value;

            var carryOccurred = result > 65535;

            if (carryOccurred)
                result = result - 65536;

            Registers.HL = (UInt16)result;

            Flags.Carry = carryOccurred;
            Flags.Subtract = false;

            // TODO: Set H flag
        }

        /**
         * Encapsulates the rotate accumulator left/right (RRC and RLC) and the rotate
         * accumulator left/right though carry (RAL and RAR) instruction behavior. The Intel
         * Z80 programmers manual has excellent examples and diagrams of each instruction.
         */
        private void ExecuteRotateAccumulator(bool left, bool rotateThroughCarry = false)
        {
            var previousHighOrderBitSet = (Registers.A & 0x80) == 0x80;
            var previousLowOrderBitSet = (Registers.A & 0x01) == 0x01;
            var previousCarryFlagSet = Flags.Carry;

            int result = Registers.A;

            if (left)
            {
                result = result << 1;

                if (rotateThroughCarry)
                {
                    if (previousCarryFlagSet)
                        result = result | 0x01;

                    Flags.Carry = previousHighOrderBitSet;
                }
                else
                {
                    if (previousHighOrderBitSet)
                        result = result | 0x01;

                    Flags.Carry = previousHighOrderBitSet;
                }
            }
            else
            {
                result = result >> 1;

                if (rotateThroughCarry)
                {
                    if (previousCarryFlagSet)
                        result = result | 0x80;

                    Flags.Carry = previousLowOrderBitSet;
                }
                else
                {
                    if (previousLowOrderBitSet)
                        result = result | 0x80;

                    Flags.Carry = previousLowOrderBitSet;
                }
            }

            Registers.A = (byte)result;
        }

        #endregion
    }
}
