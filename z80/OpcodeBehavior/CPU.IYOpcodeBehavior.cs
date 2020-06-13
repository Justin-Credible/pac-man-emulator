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
                        Registers.IYH++;
                        SetFlags(subtract: false, result: Registers.IY);
                        break;
                    case OpcodeBytes.DEC_IYH:
                        Registers.IYH--;
                        SetFlags(subtract: true, result: Registers.IY);
                        break;

                    case OpcodeBytes.INC_IYL:
                        Registers.IYL++;
                        SetFlags(subtract: false, result: Registers.IY);
                        break;
                    case OpcodeBytes.DEC_IYL:
                        Registers.IYL--;
                        SetFlags(subtract: true, result: Registers.IY);
                        break;

                    case OpcodeBytes.INC_MIY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        value++;
                        WriteMemory(Registers.IY + offset, value);
                        SetFlags(carry: false, result: value, subtract: false);
                        break;
                    }
                    case OpcodeBytes.DEC_MIY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        value--;
                        WriteMemory(Registers.IY + offset, value);
                        SetFlags(carry: false, result: value, subtract: true);
                        break;
                    }

                #endregion

                #region Stack Operations

                    case OpcodeBytes.POP_IY:
                        Registers.IYH = ReadMemory(Registers.SP + 1);
                        Registers.IYL = ReadMemory(Registers.SP);
                        Registers.SP = (UInt16)(Registers.SP + 2);
                        break;

                    case OpcodeBytes.PUSH_IY:
                        WriteMemory(Registers.SP - 1, Registers.IYH);
                        WriteMemory(Registers.SP - 2, Registers.IYL);
                        Registers.SP = (UInt16)(Registers.SP - 2);
                        break;

                    // Exchange stack
                    //  IYL <-> (SP); IYH <-> (SP+1)
                    case OpcodeBytes.EX_MSP_IY:
                    {
                        var oldIYL = Registers.IYL;
                        var oldIYH = Registers.IYH;
                        Registers.IYL = ReadMemory(Registers.SP);
                        WriteMemory(Registers.SP, oldIYL);
                        Registers.IYH = ReadMemory(Registers.SP+1);
                        WriteMemory(Registers.SP+1, oldIYH);
                        break;
                    }

                #endregion

                #region Add

                    #region Add (Addresses)

                        case OpcodeBytes.ADD_IY_BC:
                            Registers.IY = ExecuteAdd16NonArithmetic(Registers.IY, Registers.BC);
                            break;
                        case OpcodeBytes.ADD_IY_DE:
                            Registers.IY = ExecuteAdd16NonArithmetic(Registers.IY, Registers.DE);
                            break;
                        case OpcodeBytes.ADD_IY_IY:
                            Registers.IY = ExecuteAdd16NonArithmetic(Registers.IY, Registers.IY);
                            break;
                        case OpcodeBytes.ADD_IY_SP:
                            Registers.IY = ExecuteAdd16NonArithmetic(Registers.IY, Registers.SP);
                            break;

                    #endregion

                    #region Add (Arithmetic)

                        case OpcodeBytes.ADD_A_IY:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IY + offset);
                            Registers.A = ExecuteAdd(Registers.A, value);
                            break;
                        }

                        case OpcodeBytes.ADD_A_IYH:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IYH);
                            break;

                        case OpcodeBytes.ADD_A_IYL:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IYL);
                            break;

                        case OpcodeBytes.ADC_A_IY:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IY + offset);
                            Registers.A = ExecuteAdd(Registers.A, value, addCarryFlag: true);
                            break;
                        }

                        case OpcodeBytes.ADC_A_IYH:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IYH, addCarryFlag: true);
                            break;
                        case OpcodeBytes.ADC_A_IYL:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IYL, addCarryFlag: true);
                            break;

                    #endregion

                #endregion

                #region Subtract

                    case OpcodeBytes.SUB_IY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.A = ExecuteSubtract(Registers.A, value);
                        break;
                    }

                    case OpcodeBytes.SUB_IYH:
                        Registers.A = ExecuteSubtract(Registers.A, Registers.IYH);
                        break;
                    case OpcodeBytes.SUB_IYL:
                        Registers.A = ExecuteSubtract(Registers.A, Registers.IYL);
                        break;

                    case OpcodeBytes.SBC_A_IY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.A = ExecuteSubtract(Registers.A, value, subtractCarryFlag: true);
                        break;
                    }

                    case OpcodeBytes.SBC_A_IYH:
                        Registers.A = ExecuteSubtract(Registers.A, Registers.IYH, subtractCarryFlag: true);
                        break;
                    case OpcodeBytes.SBC_A_IYL:
                        Registers.A = ExecuteSubtract(Registers.A, Registers.IYL, subtractCarryFlag: true);
                        break;

                #endregion

                #region Compare

                    case OpcodeBytes.CP_IY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        ExecuteSubtract(Registers.A, value);
                        break;
                    }

                    case OpcodeBytes.CP_IYH:
                        ExecuteSubtract(Registers.A, Registers.IYH);
                        break;
                    case OpcodeBytes.CP_IYL:
                        ExecuteSubtract(Registers.A, Registers.IYL);
                        break;

                #endregion

                #region Bitwise Operations

                    #region Bitwise AND

                        case OpcodeBytes.AND_IY:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IY + offset);
                            Registers.A = (byte)(Registers.A & value);
                            SetFlags(result: Registers.A, carry: false, subtract: false, halfCarry: true);
                            break;
                        }

                        case OpcodeBytes.AND_IYH:
                            Registers.A = (byte)(Registers.A & Registers.IYH);
                            SetFlags(result: Registers.A, carry: false, subtract: false, halfCarry: true);
                            break;
                        case OpcodeBytes.AND_IYL:
                            Registers.A = (byte)(Registers.A & Registers.IYL);
                            SetFlags(result: Registers.A, carry: false, subtract: false, halfCarry: true);
                            break;

                    #endregion

                    #region Bitwise OR

                        case OpcodeBytes.OR_IY:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IY + offset);
                            Registers.A = (byte)(Registers.A | value);
                            SetFlags(result: Registers.A, carry: false, subtract: false, halfCarry: false);
                            break;
                        }

                        case OpcodeBytes.OR_IYH:
                            Registers.A = (byte)(Registers.A | Registers.IYH);
                            SetFlags(result: Registers.A, carry: false, subtract: false, halfCarry: false);
                            break;
                        case OpcodeBytes.OR_IYL:
                            Registers.A = (byte)(Registers.A | Registers.IYL);
                            SetFlags(result: Registers.A, carry: false, subtract: false, halfCarry: false);
                            break;

                    #endregion

                    #region Bitwise XOR

                        case OpcodeBytes.XOR_IY:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IY + offset);
                            Registers.A = (byte)(Registers.A ^ value);
                            SetFlags(result: Registers.A, carry: false, subtract: false, halfCarry: false);
                            break;
                        }

                        case OpcodeBytes.XOR_IYH:
                            Registers.A = (byte)(Registers.A ^ Registers.IYH);
                            SetFlags(result: Registers.A, carry: false, subtract: false, halfCarry: false);
                            break;
                        case OpcodeBytes.XOR_IYL:
                            Registers.A = (byte)(Registers.A ^ Registers.IYL);
                            SetFlags(result: Registers.A, carry: false, subtract: false, halfCarry: false);
                            break;

                    #endregion

                #endregion

                #region Load

                    case OpcodeBytes.LD_SP_IY:
                        Registers.SP = Registers.IY;
                        break;

                    case OpcodeBytes.LD_IY_NN:
                    {
                        var value = ReadMemory16(Registers.PC + 2);
                        Registers.IY = value;
                        break;
                    }

                    case OpcodeBytes.LD_MNN_IY:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        WriteMemory16(address, Registers.IY);
                        break;
                    }
                    case OpcodeBytes.LD_IY_MNN:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        Registers.IY = ReadMemory16(address);
                        break;
                    }

                    case OpcodeBytes.LD_MIY_N:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = Memory[Registers.PC + 3];
                        WriteMemory(Registers.IY + offset, value);
                        break;
                    }

                    case OpcodeBytes.LD_MIY_B:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IY + offset, Registers.B);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_C:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IY + offset, Registers.C);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_D:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IY + offset, Registers.D);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_E:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IY + offset, Registers.E);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_H:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IY + offset, Registers.H);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_L:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IY + offset, Registers.L);
                        break;
                    }
                    case OpcodeBytes.LD_MIY_A:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        WriteMemory(Registers.IY + offset, Registers.A);
                        break;
                    }

                    case OpcodeBytes.LD_B_MIY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.B = ReadMemory(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_C_MIY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.C = ReadMemory(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_D_MIY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.D = ReadMemory(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_E_MIY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.E = ReadMemory(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_H_MIY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.H = ReadMemory(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_L_MIY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.L = ReadMemory(Registers.IY + offset);
                        break;
                    }
                    case OpcodeBytes.LD_A_MIY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        Registers.A = ReadMemory(Registers.IY + offset);
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
                        Registers.IYH = Memory[Registers.PC + 2];
                        break;
                    case OpcodeBytes.LD_IYL_N:
                        Registers.IYL = Memory[Registers.PC + 2];
                        break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode.Code, Registers.PC));
            }
        }
    }
}
