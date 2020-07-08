using System;

namespace JustinCredible.ZilogZ80
{
    public partial class CPU
    {
        private void ExecuteIYOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            incrementProgramCounter = true;
            useAlternateCycleCount = false;

            switch (opcode.Code)
            {
                case OpcodeBytes.JP_IY:
                    ExecuteJump(Registers.IY);
                    incrementProgramCounter = false;
                    break;

                #region Increment/Decrement

                    case OpcodeBytes.INC_IY:
                        Registers.IY++;
                        break;
                    case OpcodeBytes.DEC_IY:
                        Registers.IY--;
                        break;

                    case OpcodeBytes.INC_IYH:
                        SetFlagsFrom8BitAddition(addend: Registers.IYH, augend: 1, false, false);
                        Registers.IYH++;
                        break;
                    case OpcodeBytes.DEC_IYH:
                        SetFlagsFrom8BitSubtraction(minuend: Registers.IYH, subtrahend: 1, false, false);
                        Registers.IYH--;
                        break;

                    case OpcodeBytes.INC_IYL:
                        SetFlagsFrom8BitAddition(addend: Registers.IYL, augend: 1, false, false);
                        Registers.IYL++;
                        break;
                    case OpcodeBytes.DEC_IYL:
                        SetFlagsFrom8BitSubtraction(minuend: Registers.IYL, subtrahend: 1, false, false);
                        Registers.IYL--;
                        break;

                    case OpcodeBytes.INC_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IY + offset);
                        SetFlagsFrom8BitAddition(addend: value, augend: 1, false, false);
                        value++;
                        Memory.Write(Registers.IY + offset, value);
                        break;
                    }
                    case OpcodeBytes.DEC_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IY + offset);
                        SetFlagsFrom8BitSubtraction(minuend: value, subtrahend: 1, false, false);
                        value--;
                        Memory.Write(Registers.IY + offset, value);
                        break;
                    }

                #endregion

                #region Stack Operations

                    case OpcodeBytes.POP_IY:
                        Registers.IYH = Memory.Read(Registers.SP + 1);
                        Registers.IYL = Memory.Read(Registers.SP);
                        Registers.SP = (UInt16)(Registers.SP + 2);
                        break;

                    case OpcodeBytes.PUSH_IY:
                        Memory.Write(Registers.SP - 1, Registers.IYH);
                        Memory.Write(Registers.SP - 2, Registers.IYL);
                        Registers.SP = (UInt16)(Registers.SP - 2);
                        break;

                    // Exchange stack
                    //  IYL <-> (SP); IYH <-> (SP+1)
                    case OpcodeBytes.EX_MSP_IY:
                    {
                        var oldIYL = Registers.IYL;
                        var oldIYH = Registers.IYH;
                        Registers.IYL = Memory.Read(Registers.SP);
                        Memory.Write(Registers.SP, oldIYL);
                        Registers.IYH = Memory.Read(Registers.SP+1);
                        Memory.Write(Registers.SP+1, oldIYH);
                        break;
                    }

                #endregion

                #region Add

                    #region Add (Addresses)

                        case OpcodeBytes.ADD_IY_BC:
                            Registers.IY = Execute16BitAddition(Registers.IY, Registers.BC);
                            break;
                        case OpcodeBytes.ADD_IY_DE:
                            Registers.IY = Execute16BitAddition(Registers.IY, Registers.DE);
                            break;
                        case OpcodeBytes.ADD_IY_IY:
                            Registers.IY = Execute16BitAddition(Registers.IY, Registers.IY);
                            break;
                        case OpcodeBytes.ADD_IY_SP:
                            Registers.IY = Execute16BitAddition(Registers.IY, Registers.SP);
                            break;

                    #endregion

                    #region Add (Arithmetic)

                        case OpcodeBytes.ADD_A_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = Execute8BitAddition(Registers.A, value);
                            break;
                        }

                        case OpcodeBytes.ADD_A_IYH:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.IYH);
                            break;

                        case OpcodeBytes.ADD_A_IYL:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.IYL);
                            break;

                        case OpcodeBytes.ADC_A_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = Execute8BitAddition(Registers.A, value, addCarryFlag: true);
                            break;
                        }

                        case OpcodeBytes.ADC_A_IYH:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.IYH, addCarryFlag: true);
                            break;
                        case OpcodeBytes.ADC_A_IYL:
                            Registers.A = Execute8BitAddition(Registers.A, Registers.IYL, addCarryFlag: true);
                            break;

                    #endregion

                #endregion

                #region Subtract

                    case OpcodeBytes.SUB_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IY + offset);
                        Registers.A = Execute8BitSubtraction(Registers.A, value);
                        break;
                    }

                    case OpcodeBytes.SUB_IYH:
                        Registers.A = Execute8BitSubtraction(Registers.A, Registers.IYH);
                        break;
                    case OpcodeBytes.SUB_IYL:
                        Registers.A = Execute8BitSubtraction(Registers.A, Registers.IYL);
                        break;

                    case OpcodeBytes.SBC_A_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IY + offset);
                        Registers.A = Execute8BitSubtraction(Registers.A, value, subtractCarryFlag: true);
                        break;
                    }

                    case OpcodeBytes.SBC_A_IYH:
                        Registers.A = Execute8BitSubtraction(Registers.A, Registers.IYH, subtractCarryFlag: true);
                        break;
                    case OpcodeBytes.SBC_A_IYL:
                        Registers.A = Execute8BitSubtraction(Registers.A, Registers.IYL, subtractCarryFlag: true);
                        break;

                #endregion

                #region Compare

                    case OpcodeBytes.CP_IY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.IY + offset);
                        Execute8BitSubtraction(Registers.A, value);
                        break;
                    }

                    case OpcodeBytes.CP_IYH:
                        Execute8BitSubtraction(Registers.A, Registers.IYH);
                        break;
                    case OpcodeBytes.CP_IYL:
                        Execute8BitSubtraction(Registers.A, Registers.IYL);
                        break;

                #endregion

                #region Bitwise Operations

                    #region Bitwise AND

                        case OpcodeBytes.AND_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = (byte)(Registers.A & value);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        }

                        case OpcodeBytes.AND_IYH:
                            Registers.A = (byte)(Registers.A & Registers.IYH);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;
                        case OpcodeBytes.AND_IYL:
                            Registers.A = (byte)(Registers.A & Registers.IYL);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: true);
                            break;

                    #endregion

                    #region Bitwise OR

                        case OpcodeBytes.OR_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = (byte)(Registers.A | value);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        }

                        case OpcodeBytes.OR_IYH:
                            Registers.A = (byte)(Registers.A | Registers.IYH);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.OR_IYL:
                            Registers.A = (byte)(Registers.A | Registers.IYL);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;

                    #endregion

                    #region Bitwise XOR

                        case OpcodeBytes.XOR_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = (byte)(Registers.A ^ value);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        }

                        case OpcodeBytes.XOR_IYH:
                            Registers.A = (byte)(Registers.A ^ Registers.IYH);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;
                        case OpcodeBytes.XOR_IYL:
                            Registers.A = (byte)(Registers.A ^ Registers.IYL);
                            SetFlagsFrom8BitLogicalOperation(Registers.A, isAND: false);
                            break;

                    #endregion

                #endregion

                #region Load

                    case OpcodeBytes.LD_SP_IY:
                        Registers.SP = Registers.IY;
                        break;

                    case OpcodeBytes.LD_IY_NN:
                    {
                        var value = Memory.Read16(Registers.PC + 2);
                        Registers.IY = value;
                        break;
                    }

                    case OpcodeBytes.LD_MNN_IY:
                    {
                        var address = Memory.Read16(Registers.PC + 2);
                        Memory.Write16(address, Registers.IY);
                        break;
                    }
                    case OpcodeBytes.LD_IY_MNN:
                    {
                        var address = Memory.Read16(Registers.PC + 2);
                        Registers.IY = Memory.Read16(address);
                        break;
                    }

                    case OpcodeBytes.LD_MIY_N:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        var value = Memory.Read(Registers.PC + 3);
                        Memory.Write(Registers.IY + offset, value);
                        break;
                    }

                    case OpcodeBytes.LD_MIY_B:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IY + offset, Registers.B);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_C:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IY + offset, Registers.C);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_D:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IY + offset, Registers.D);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_E:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IY + offset, Registers.E);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_H:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IY + offset, Registers.H);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_L:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IY + offset, Registers.L);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_A:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Memory.Write(Registers.IY + offset, Registers.A);
                        break;
                    }

                    case OpcodeBytes.LD_B_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.B = Memory.Read(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_C_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.C = Memory.Read(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_D_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.D = Memory.Read(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_E_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.E = Memory.Read(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_H_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.H = Memory.Read(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_L_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.L = Memory.Read(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_A_MIY:
                    {
                        var offset = (sbyte)Memory.Read(Registers.PC + 2);
                        Registers.A = Memory.Read(Registers.IY + offset);
                        break;
                    }

                    case OpcodeBytes.LD_A_IYH:
                        Registers.A = Registers.IYH;
                        break;
                    case OpcodeBytes.LD_B_IYH:
                        Registers.B = Registers.IYH;
                        break;
                    case OpcodeBytes.LD_C_IYH:
                        Registers.C = Registers.IYH;
                        break;
                    case OpcodeBytes.LD_D_IYH:
                        Registers.D = Registers.IYH;
                        break;
                    case OpcodeBytes.LD_E_IYH:
                        Registers.E = Registers.IYH;
                        break;

                    case OpcodeBytes.LD_A_IYL:
                        Registers.A = Registers.IYL;
                        break;
                    case OpcodeBytes.LD_B_IYL:
                        Registers.B = Registers.IYL;
                        break;
                    case OpcodeBytes.LD_C_IYL:
                        Registers.C = Registers.IYL;
                        break;
                    case OpcodeBytes.LD_D_IYL:
                        Registers.D = Registers.IYL;
                        break;
                    case OpcodeBytes.LD_E_IYL:
                        Registers.E = Registers.IYL;
                        break;

                    case OpcodeBytes.LD_IYH_A:
                        Registers.IYH = Registers.A;
                        break;
                    case OpcodeBytes.LD_IYH_B:
                        Registers.IYH = Registers.B;
                        break;
                    case OpcodeBytes.LD_IYH_C:
                        Registers.IYH = Registers.C;
                        break;
                    case OpcodeBytes.LD_IYH_D:
                        Registers.IYH = Registers.D;
                        break;
                    case OpcodeBytes.LD_IYH_E:
                        Registers.IYH = Registers.E;
                        break;

                    case OpcodeBytes.LD_IYL_A:
                        Registers.IYL = Registers.A;
                        break;
                    case OpcodeBytes.LD_IYL_B:
                        Registers.IYL = Registers.B;
                        break;
                    case OpcodeBytes.LD_IYL_C:
                        Registers.IYL = Registers.C;
                        break;
                    case OpcodeBytes.LD_IYL_D:
                        Registers.IYL = Registers.D;
                        break;
                    case OpcodeBytes.LD_IYL_E:
                        Registers.IYL = Registers.E;
                        break;

                    case OpcodeBytes.LD_IYH_IYH:
                        // NOP
                        break;
                    case OpcodeBytes.LD_IYH_IYL:
                        Registers.IYH = Registers.IYL;
                        break;
                    case OpcodeBytes.LD_IYL_IYH:
                        Registers.IYL = Registers.IYH;
                        break;
                    case OpcodeBytes.LD_IYL_IYL:
                        // NOP
                        break;

                    case OpcodeBytes.LD_IYH_N:
                        Registers.IYH = Memory.Read(Registers.PC + 2);
                        break;
                    case OpcodeBytes.LD_IYL_N:
                        Registers.IYL = Memory.Read(Registers.PC + 2);
                        break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode.Code, Registers.PC));
            }
        }
    }
}
