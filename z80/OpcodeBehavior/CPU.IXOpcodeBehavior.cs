using System;

namespace JustinCredible.ZilogZ80
{
    public partial class CPU
    {
        private void ExecuteIXOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            incrementProgramCounter = true;
            useAlternateCycleCount = false;

            switch (opcode.Code)
            {
                case OpcodeBytes.JP_IX:
                    ExecuteJump(Registers.IX);
                    incrementProgramCounter = false;
                    break;

                #region Increment/Decrement

                    case OpcodeBytes.INC_IX:
                        Registers.IX++;
                        break;
                    case OpcodeBytes.DEC_IX:
                        Registers.IX--;
                        break;

                    case OpcodeBytes.INC_IXH:
                        SetFlagsFrom8BitAddition(addend: Registers.IXH, augend: 1, false, false);
                        Registers.IXH++;
                        break;
                    case OpcodeBytes.DEC_IXH:
                        SetFlagsFrom8BitSubtraction(minuend: Registers.IXH, subtrahend: 1, false, false);
                        Registers.IXH--;
                        break;

                    case OpcodeBytes.INC_IXL:
                        SetFlagsFrom8BitAddition(addend: Registers.IXL, augend: 1, false, false);
                        Registers.IXL++;
                        break;
                    case OpcodeBytes.DEC_IXL:
                        SetFlagsFrom8BitSubtraction(minuend: Registers.IXL, subtrahend: 1, false, false);
                        Registers.IXL--;
                        break;

                    case OpcodeBytes.INC_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IX + offset);
                        SetFlagsFrom8BitAddition(addend: value, augend: 1, false, false);
                        value++;
                        Memory.Write(Registers.IX + offset, value);
                        break;
                    }
                    case OpcodeBytes.DEC_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IX + offset);
                        SetFlagsFrom8BitSubtraction(minuend: value, subtrahend: 1, false, false);
                        value--;
                        Memory.Write(Registers.IX + offset, value);
                        break;
                    }

                #endregion

                #region Stack Operations

                    case OpcodeBytes.POP_IX:
                        Registers.IXH = Memory.Read(Registers.SP + 1);
                        Registers.IXL = Memory.Read(Registers.SP);
                        Registers.SP = (UInt16)(Registers.SP + 2);
                        break;

                    case OpcodeBytes.PUSH_IX:
                        Memory.Write(Registers.SP - 1, Registers.IXH);
                        Memory.Write(Registers.SP - 2, Registers.IXL);
                        Registers.SP = (UInt16)(Registers.SP - 2);
                        break;

                    // Exchange stack
                    //  IXL <-> (SP); IXH <-> (SP+1)
                    case OpcodeBytes.EX_MSP_IX:
                    {
                        var oldIXL = Registers.IXL;
                        var oldIXH = Registers.IXH;
                        Registers.IXL = Memory.Read(Registers.SP);
                        Memory.Write(Registers.SP, oldIXL);
                        Registers.IXH = Memory.Read(Registers.SP+1);
                        Memory.Write(Registers.SP+1, oldIXH);
                        break;
                    }

                #endregion

                #region Add

                    #region Add (Addresses)

                        case OpcodeBytes.ADD_IX_BC:
                            Registers.IX = Execute16BitAddition(Registers.IX, Registers.BC);
                            break;
                        case OpcodeBytes.ADD_IX_DE:
                            Registers.IX = Execute16BitAddition(Registers.IX, Registers.DE);
                            break;
                        case OpcodeBytes.ADD_IX_IX:
                            Registers.IX = Execute16BitAddition(Registers.IX, Registers.IX);
                            break;
                        case OpcodeBytes.ADD_IX_SP:
                            Registers.IX = Execute16BitAddition(Registers.IX, Registers.SP);
                            break;

                    #endregion

                    #region Add (Arithmetic)

                        case OpcodeBytes.ADD_A_MIX:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IX + offset);
                            Registers.A = Execute8BitAddition(Registers.A, value);
                            break;
                        }

                        case OpcodeBytes.ADD_A_IXH:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.IXH);
                            break;

                        case OpcodeBytes.ADD_A_IXL:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.IXL);
                            break;

                        case OpcodeBytes.ADC_A_MIX:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IX + offset);
                            Registers.A = Execute8BitAddition(Registers.A, value, addCarryFlag: true);
                            break;
                        }

                        case OpcodeBytes.ADC_A_IXH:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.IXH, addCarryFlag: true);
                            break;
                        case OpcodeBytes.ADC_A_IXL:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.IXL, addCarryFlag: true);
                            break;

                    #endregion

                #endregion

                #region Subtract

                    case OpcodeBytes.SUB_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IX + offset);
                        Registers.A = Execute8BitSubtraction(Registers.A, value);
                        break;
                    }

                    case OpcodeBytes.SUB_IXH:
                        Registers.A = Execute8BitSubtraction(Registers.A, Registers.IXH);
                        break;
                    case OpcodeBytes.SUB_IXL:
                        Registers.A = Execute8BitSubtraction(Registers.A, Registers.IXL);
                        break;

                    case OpcodeBytes.SBC_A_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IX + offset);
                        Registers.A = Execute8BitSubtraction(Registers.A, value, subtractCarryFlag: true);
                        break;
                    }

                    case OpcodeBytes.SBC_A_IXH:
                        Registers.A = Execute8BitSubtraction(Registers.A, Registers.IXH, subtractCarryFlag: true);
                        break;
                    case OpcodeBytes.SBC_A_IXL:
                        Registers.A = Execute8BitSubtraction(Registers.A, Registers.IXL, subtractCarryFlag: true);
                        break;

                #endregion

                #region Compare

                    case OpcodeBytes.CP_IX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IX + offset);
                        Execute8BitSubtraction(Registers.A, value);
                        break;
                    }

                    case OpcodeBytes.CP_IXH:
                        Execute8BitSubtraction(Registers.A, Registers.IXH);
                        break;
                    case OpcodeBytes.CP_IXL:
                        Execute8BitSubtraction(Registers.A, Registers.IXL);
                        break;

                #endregion

                #region Bitwise Operations

                    #region Bitwise AND

                        case OpcodeBytes.AND_MIX:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IX + offset);
                            Registers.A = (byte)(Registers.A & value);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        }

                        case OpcodeBytes.AND_IXH:
                            Registers.A = (byte)(Registers.A & Registers.IXH);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        case OpcodeBytes.AND_IXL:
                            Registers.A = (byte)(Registers.A & Registers.IXL);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;

                    #endregion

                    #region Bitwise OR

                        case OpcodeBytes.OR_MIX:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IX + offset);
                            Registers.A = (byte)(Registers.A | value);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        }

                        case OpcodeBytes.OR_IXH:
                            Registers.A = (byte)(Registers.A | Registers.IXH);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.OR_IXL:
                            Registers.A = (byte)(Registers.A | Registers.IXL);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;

                    #endregion

                    #region Bitwise XOR

                        case OpcodeBytes.XOR_MIX:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IX + offset);
                            Registers.A = (byte)(Registers.A ^ value);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        }

                        case OpcodeBytes.XOR_IXH:
                            Registers.A = (byte)(Registers.A ^ Registers.IXH);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.XOR_IXL:
                            Registers.A = (byte)(Registers.A ^ Registers.IXL);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;

                    #endregion

                #endregion

                #region Load

                    case OpcodeBytes.LD_SP_IX:
                        Registers.SP = Registers.IX;
                        break;

                    case OpcodeBytes.LD_IX_NN:
                    {
                        var value = Memory.Read16(Registers.PC + 2);
                        Registers.IX = value;
                        break;
                    }

                    case OpcodeBytes.LD_MNN_IX:
                    {
                        var address = Memory.Read16(Registers.PC + 2);
                        Memory.Write16(address, Registers.IX);
                        break;
                    }
                    case OpcodeBytes.LD_IX_MNN:
                    {
                        var address = Memory.Read16(Registers.PC + 2);
                        Registers.IX = Memory.Read16(address);
                        break;
                    }

                    case OpcodeBytes.LD_MIX_N:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.PC + 3);
                        Memory.Write(Registers.IX + offset, value);
                        break;
                    }

                    case OpcodeBytes.LD_MIX_B:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IX + offset, Registers.B);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_C:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IX + offset, Registers.C);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_D:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IX + offset, Registers.D);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_E:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IX + offset, Registers.E);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_H:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IX + offset, Registers.H);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_L:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IX + offset, Registers.L);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_A:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IX + offset, Registers.A);
                        break;
                    }

                    case OpcodeBytes.LD_B_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.B = Memory.Read(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_C_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.C = Memory.Read(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_D_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.D = Memory.Read(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_E_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.E = Memory.Read(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_H_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.H = Memory.Read(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_L_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.L = Memory.Read(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_A_MIX:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.A = Memory.Read(Registers.IX + offset);
                        break;
                    }

                    case OpcodeBytes.LD_A_IXH:
                        Registers.A = Registers.IXH;
                        break;
                    case OpcodeBytes.LD_B_IXH:
                        Registers.B = Registers.IXH;
                        break;
                    case OpcodeBytes.LD_C_IXH:
                        Registers.C = Registers.IXH;
                        break;
                    case OpcodeBytes.LD_D_IXH:
                        Registers.D = Registers.IXH;
                        break;
                    case OpcodeBytes.LD_E_IXH:
                        Registers.E = Registers.IXH;
                        break;

                    case OpcodeBytes.LD_A_IXL:
                        Registers.A = Registers.IXL;
                        break;
                    case OpcodeBytes.LD_B_IXL:
                        Registers.B = Registers.IXL;
                        break;
                    case OpcodeBytes.LD_C_IXL:
                        Registers.C = Registers.IXL;
                        break;
                    case OpcodeBytes.LD_D_IXL:
                        Registers.D = Registers.IXL;
                        break;
                    case OpcodeBytes.LD_E_IXL:
                        Registers.E = Registers.IXL;
                        break;

                    case OpcodeBytes.LD_IXH_A:
                        Registers.IXH = Registers.A;
                        break;
                    case OpcodeBytes.LD_IXH_B:
                        Registers.IXH = Registers.B;
                        break;
                    case OpcodeBytes.LD_IXH_C:
                        Registers.IXH = Registers.C;
                        break;
                    case OpcodeBytes.LD_IXH_D:
                        Registers.IXH = Registers.D;
                        break;
                    case OpcodeBytes.LD_IXH_E:
                        Registers.IXH = Registers.E;
                        break;

                    case OpcodeBytes.LD_IXL_A:
                        Registers.IXL = Registers.A;
                        break;
                    case OpcodeBytes.LD_IXL_B:
                        Registers.IXL = Registers.B;
                        break;
                    case OpcodeBytes.LD_IXL_C:
                        Registers.IXL = Registers.C;
                        break;
                    case OpcodeBytes.LD_IXL_D:
                        Registers.IXL = Registers.D;
                        break;
                    case OpcodeBytes.LD_IXL_E:
                        Registers.IXL = Registers.E;
                        break;

                    case OpcodeBytes.LD_IXH_IXH:
                        // NOP
                        break;
                    case OpcodeBytes.LD_IXH_IXL:
                        Registers.IXH = Registers.IXL;
                        break;
                    case OpcodeBytes.LD_IXL_IXH:
                        Registers.IXL = Registers.IXH;
                        break;
                    case OpcodeBytes.LD_IXL_IXL:
                        // NOP
                        break;

                    case OpcodeBytes.LD_IXH_N:
                        Registers.IXH = Memory.Read(Registers.PC + 2);
                        break;
                    case OpcodeBytes.LD_IXL_N:
                        Registers.IXL = Memory.Read(Registers.PC + 2);
                        break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode.Code, Registers.PC));
            }
        }
    }
}
