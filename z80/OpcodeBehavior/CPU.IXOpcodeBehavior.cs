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

                #region Add

                    #region Add (Addresses)

                        case OpcodeBytes.ADD_IX_BC:
                            Registers.IX = ExecuteAdd16NonArithmetic(Registers.IX, Registers.BC);
                            break;
                        case OpcodeBytes.ADD_IX_DE:
                            Registers.IX = ExecuteAdd16NonArithmetic(Registers.IX, Registers.DE);
                            break;
                        case OpcodeBytes.ADD_IX_IX:
                            Registers.IX = ExecuteAdd16NonArithmetic(Registers.IX, Registers.IX);
                            break;
                        case OpcodeBytes.ADD_IX_SP:
                            Registers.IX = ExecuteAdd16NonArithmetic(Registers.IX, Registers.SP);
                            break;

                    #endregion

                    #region Add (Arithmetic)

                        case OpcodeBytes.ADD_A_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteAdd(Registers.A, value);
                            break;
                        }

                        case OpcodeBytes.ADD_A_IXH:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IXH);
                            break;

                        case OpcodeBytes.ADD_A_IXL:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IXL);
                            break;

                        case OpcodeBytes.ADC_A_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteAdd(Registers.A, value, addCarryFlag: true);
                            break;
                        }

                        case OpcodeBytes.ADC_A_IXH:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IXH, addCarryFlag: true);
                            break;
                        case OpcodeBytes.ADC_A_IXL:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IXL, addCarryFlag: true);
                            break;

                    #endregion

                #endregion

                #region Subtract

                    case OpcodeBytes.SUB_IX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IX + offset);
                        Registers.A = ExecuteSubtract(Registers.A, value);
                        break;
                    }

                    case OpcodeBytes.SUB_IXH:
                        Registers.A = ExecuteSubtract(Registers.A, Registers.IXH);
                        break;
                    case OpcodeBytes.SUB_IXL:
                        Registers.A = ExecuteSubtract(Registers.A, Registers.IXL);
                        break;

                    case OpcodeBytes.SBC_A_IX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IX + offset);
                        Registers.A = ExecuteSubtract(Registers.A, value, subtractCarryFlag: true);
                        break;
                    }

                    case OpcodeBytes.SBC_A_IXH:
                        Registers.A = ExecuteSubtract(Registers.A, Registers.IXH, subtractCarryFlag: true);
                        break;
                    case OpcodeBytes.SBC_A_IXL:
                        Registers.A = ExecuteSubtract(Registers.A, Registers.IXL, subtractCarryFlag: true);
                        break;

                #endregion

                #region Compare

                    case OpcodeBytes.CP_IX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IX + offset);
                        ExecuteSubtract(Registers.A, value);
                        break;
                    }

                    case OpcodeBytes.CP_IXH:
                        ExecuteSubtract(Registers.A, Registers.IXH);
                        break;
                    case OpcodeBytes.CP_IXL:
                        ExecuteSubtract(Registers.A, Registers.IXL);
                        break;

                #endregion

                #region Bitwise Operations

                    #region Bitwise AND

                        case OpcodeBytes.AND_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = (byte)(Registers.A & value);
                            SetFlags(result: Registers.A, carry: false, subtract: false, auxCarry: true);
                            break;
                        }

                        case OpcodeBytes.AND_IXH:
                            Registers.A = (byte)(Registers.A & Registers.IXH);
                            SetFlags(result: Registers.A, carry: false, subtract: false, auxCarry: true);
                            break;
                        case OpcodeBytes.AND_IXL:
                            Registers.A = (byte)(Registers.A & Registers.IXL);
                            SetFlags(result: Registers.A, carry: false, subtract: false, auxCarry: true);
                            break;

                    #endregion

                    #region Bitwise OR

                        case OpcodeBytes.OR_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = (byte)(Registers.A | value);
                            SetFlags(result: Registers.A, carry: false, subtract: false, auxCarry: false);
                            break;
                        }

                        case OpcodeBytes.OR_IXH:
                            Registers.A = (byte)(Registers.A | Registers.IXH);
                            SetFlags(result: Registers.A, carry: false, subtract: false, auxCarry: false);
                            break;
                        case OpcodeBytes.OR_IXL:
                            Registers.A = (byte)(Registers.A | Registers.IXL);
                            SetFlags(result: Registers.A, carry: false, subtract: false, auxCarry: false);
                            break;

                    #endregion

                    #region Bitwise XOR

                        case OpcodeBytes.XOR_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = (byte)(Registers.A ^ value);
                            SetFlags(result: Registers.A, carry: false, subtract: false, auxCarry: false);
                            break;
                        }

                        case OpcodeBytes.XOR_IXH:
                            Registers.A = (byte)(Registers.A ^ Registers.IXH);
                            SetFlags(result: Registers.A, carry: false, subtract: false, auxCarry: false);
                            break;
                        case OpcodeBytes.XOR_IXL:
                            Registers.A = (byte)(Registers.A ^ Registers.IXL);
                            SetFlags(result: Registers.A, carry: false, subtract: false, auxCarry: false);
                            break;

                    #endregion

                #endregion

                #region Load

                    case OpcodeBytes.LD_SP_IX:
                        Registers.SP = Registers.IX;
                        break;

                    case OpcodeBytes.LD_IX_NN:
                    {
                        var value = ReadMemory16(Registers.PC + 2);
                        Registers.IX = value;
                        break;
                    }

                    case OpcodeBytes.LD_MNN_IX:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        WriteMemory16(address, Registers.IX);
                        break;
                    }
                    case OpcodeBytes.LD_IX_MNN:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        Registers.IX = ReadMemory16(address);
                        break;
                    }

                    case OpcodeBytes.LD_MIX_N:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = Memory[Registers.PC + 3];
                        WriteMemory(Registers.IX + offset, value);
                        break;
                    }

                    case OpcodeBytes.LD_MIX_B:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IX + offset, Registers.B);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_C:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IX + offset, Registers.C);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_D:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IX + offset, Registers.D);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_E:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IX + offset, Registers.E);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_H:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IX + offset, Registers.H);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_L:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IX + offset, Registers.L);
                        break;
                    }
                    case OpcodeBytes.LD_MIX_A:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IX + offset, Registers.A);
                        break;
                    }

                    case OpcodeBytes.LD_B_MIX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.B = ReadMemory(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_C_MIX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.C = ReadMemory(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_D_MIX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.D = ReadMemory(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_E_MIX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.E = ReadMemory(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_H_MIX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.H = ReadMemory(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_L_MIX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.L = ReadMemory(Registers.IX + offset);
                        break;
                    }
                    case OpcodeBytes.LD_A_MIX:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.A = ReadMemory(Registers.IX + offset);
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
                        Registers.IXH = Memory[Registers.PC + 2];
                        break;
                    case OpcodeBytes.LD_IXL_N:
                        Registers.IXL = Memory[Registers.PC + 2];
                        break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode.Code, Registers.PC));
            }
        }
    }
}
