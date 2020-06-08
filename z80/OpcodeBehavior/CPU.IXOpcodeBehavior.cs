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
                #region Add

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

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, Registers.PC));
            }
        }
    }
}
