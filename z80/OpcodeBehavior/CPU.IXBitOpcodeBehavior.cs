using System;

namespace JustinCredible.ZilogZ80
{
    public partial class CPU
    {
        private void ExecuteIXBitOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            incrementProgramCounter = true;
            useAlternateCycleCount = false;

            switch (opcode.Code)
            {
                #region Rotate

                    #region RLC (IX+n), r - Rotate left

                        case OpcodeBytes.RLC_IX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_IX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_IX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_IX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_IX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_IX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteRotate(value, left: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.RLC_IX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteRotate(value, left: true);
                            break;
                        }

                    #endregion

                    #region RRC (IX+n), r - Rotate right

                        case OpcodeBytes.RRC_IX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_IX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_IX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_IX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_IX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_IX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteRotate(value, left: false);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.RRC_IX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteRotate(value, left: false);
                            break;
                        }

                    #endregion

                    #region RL (IX+n), r - Rotate left through carry

                        case OpcodeBytes.RL_IX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_IX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_IX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_IX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_IX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_IX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.RL_IX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }

                    #endregion

                    #region RR (IX+n), r - Rotate right through carry

                        case OpcodeBytes.RR_IX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_IX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_IX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_IX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_IX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_IX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.RR_IX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }

                    #endregion

                #endregion

                #region Shift

                    #region SLA (IX+n), r - Shift left (arithmetic)
                        case OpcodeBytes.SLA_IX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_IX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_IX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_IX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_IX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_IX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteShiftArithmetic(value: value, left: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.SLA_IX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                    #endregion

                    #region SRA (IX+n), r - Shift right (arithmetic)
                        case OpcodeBytes.SRA_IX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_IX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_IX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_IX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_IX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_IX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteShiftArithmetic(value: value, left: false);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.SRA_IX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                    #endregion

                    // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
                    // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
                    #region SLL (IX+n), r - Shift left ?? (undocumented)
                        case OpcodeBytes.SLL_IX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_IX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_IX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_IX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_IX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_IX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteShiftLogical(value: value, left: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.SLL_IX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                    #endregion

                    #region SRL (IX+n), r - Shift right logical
                        case OpcodeBytes.SRL_IX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_IX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_IX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_IX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_IX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_IX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteShiftLogical(value: value, left: false);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.SRL_IX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                    #endregion

                #endregion

                #region Test Bit

                    case OpcodeBytes.BIT_0_IX_2:
                    case OpcodeBytes.BIT_0_IX_3:
                    case OpcodeBytes.BIT_0_IX_4:
                    case OpcodeBytes.BIT_0_IX_5:
                    case OpcodeBytes.BIT_0_IX_6:
                    case OpcodeBytes.BIT_0_IX_7:
                    case OpcodeBytes.BIT_0_IX:
                    case OpcodeBytes.BIT_0_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(0, value);
                            break;
                        }

                    case OpcodeBytes.BIT_1_IX_2:
                    case OpcodeBytes.BIT_1_IX_3:
                    case OpcodeBytes.BIT_1_IX_4:
                    case OpcodeBytes.BIT_1_IX_5:
                    case OpcodeBytes.BIT_1_IX_6:
                    case OpcodeBytes.BIT_1_IX_7:
                    case OpcodeBytes.BIT_1_IX:
                    case OpcodeBytes.BIT_1_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(1, value);
                            break;
                        }

                    case OpcodeBytes.BIT_2_IX_2:
                    case OpcodeBytes.BIT_2_IX_3:
                    case OpcodeBytes.BIT_2_IX_4:
                    case OpcodeBytes.BIT_2_IX_5:
                    case OpcodeBytes.BIT_2_IX_6:
                    case OpcodeBytes.BIT_2_IX_7:
                    case OpcodeBytes.BIT_2_IX:
                    case OpcodeBytes.BIT_2_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(2, value);
                            break;
                        }

                    case OpcodeBytes.BIT_3_IX_2:
                    case OpcodeBytes.BIT_3_IX_3:
                    case OpcodeBytes.BIT_3_IX_4:
                    case OpcodeBytes.BIT_3_IX_5:
                    case OpcodeBytes.BIT_3_IX_6:
                    case OpcodeBytes.BIT_3_IX_7:
                    case OpcodeBytes.BIT_3_IX:
                    case OpcodeBytes.BIT_3_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(3, value);
                            break;
                        }

                    case OpcodeBytes.BIT_4_IX_2:
                    case OpcodeBytes.BIT_4_IX_3:
                    case OpcodeBytes.BIT_4_IX_4:
                    case OpcodeBytes.BIT_4_IX_5:
                    case OpcodeBytes.BIT_4_IX_6:
                    case OpcodeBytes.BIT_4_IX_7:
                    case OpcodeBytes.BIT_4_IX:
                    case OpcodeBytes.BIT_4_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(4, value);
                            break;
                        }

                    case OpcodeBytes.BIT_5_IX_2:
                    case OpcodeBytes.BIT_5_IX_3:
                    case OpcodeBytes.BIT_5_IX_4:
                    case OpcodeBytes.BIT_5_IX_5:
                    case OpcodeBytes.BIT_5_IX_6:
                    case OpcodeBytes.BIT_5_IX_7:
                    case OpcodeBytes.BIT_5_IX:
                    case OpcodeBytes.BIT_5_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(5, value);
                            break;
                        }

                    case OpcodeBytes.BIT_6_IX_2:
                    case OpcodeBytes.BIT_6_IX_3:
                    case OpcodeBytes.BIT_6_IX_4:
                    case OpcodeBytes.BIT_6_IX_5:
                    case OpcodeBytes.BIT_6_IX_6:
                    case OpcodeBytes.BIT_6_IX_7:
                    case OpcodeBytes.BIT_6_IX:
                    case OpcodeBytes.BIT_6_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(6, value);
                            break;
                        }

                    case OpcodeBytes.BIT_7_IX_2:
                    case OpcodeBytes.BIT_7_IX_3:
                    case OpcodeBytes.BIT_7_IX_4:
                    case OpcodeBytes.BIT_7_IX_5:
                    case OpcodeBytes.BIT_7_IX_6:
                    case OpcodeBytes.BIT_7_IX_7:
                    case OpcodeBytes.BIT_7_IX:
                    case OpcodeBytes.BIT_7_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(7, value);
                            break;
                        }

                #endregion

                #region Reset Bit

                    case OpcodeBytes.RES_0_IX_2:
                    case OpcodeBytes.RES_0_IX_3:
                    case OpcodeBytes.RES_0_IX_4:
                    case OpcodeBytes.RES_0_IX_5:
                    case OpcodeBytes.RES_0_IX_6:
                    case OpcodeBytes.RES_0_IX_7:
                    case OpcodeBytes.RES_0_IX:
                    case OpcodeBytes.RES_0_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(0, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_1_IX_2:
                    case OpcodeBytes.RES_1_IX_3:
                    case OpcodeBytes.RES_1_IX_4:
                    case OpcodeBytes.RES_1_IX_5:
                    case OpcodeBytes.RES_1_IX_6:
                    case OpcodeBytes.RES_1_IX_7:
                    case OpcodeBytes.RES_1_IX:
                    case OpcodeBytes.RES_1_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(1, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_2_IX_2:
                    case OpcodeBytes.RES_2_IX_3:
                    case OpcodeBytes.RES_2_IX_4:
                    case OpcodeBytes.RES_2_IX_5:
                    case OpcodeBytes.RES_2_IX_6:
                    case OpcodeBytes.RES_2_IX_7:
                    case OpcodeBytes.RES_2_IX:
                    case OpcodeBytes.RES_2_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(2, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_3_IX_2:
                    case OpcodeBytes.RES_3_IX_3:
                    case OpcodeBytes.RES_3_IX_4:
                    case OpcodeBytes.RES_3_IX_5:
                    case OpcodeBytes.RES_3_IX_6:
                    case OpcodeBytes.RES_3_IX_7:
                    case OpcodeBytes.RES_3_IX:
                    case OpcodeBytes.RES_3_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(3, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_4_IX_2:
                    case OpcodeBytes.RES_4_IX_3:
                    case OpcodeBytes.RES_4_IX_4:
                    case OpcodeBytes.RES_4_IX_5:
                    case OpcodeBytes.RES_4_IX_6:
                    case OpcodeBytes.RES_4_IX_7:
                    case OpcodeBytes.RES_4_IX:
                    case OpcodeBytes.RES_4_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(4, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_5_IX_2:
                    case OpcodeBytes.RES_5_IX_3:
                    case OpcodeBytes.RES_5_IX_4:
                    case OpcodeBytes.RES_5_IX_5:
                    case OpcodeBytes.RES_5_IX_6:
                    case OpcodeBytes.RES_5_IX_7:
                    case OpcodeBytes.RES_5_IX:
                    case OpcodeBytes.RES_5_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(5, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_6_IX_2:
                    case OpcodeBytes.RES_6_IX_3:
                    case OpcodeBytes.RES_6_IX_4:
                    case OpcodeBytes.RES_6_IX_5:
                    case OpcodeBytes.RES_6_IX_6:
                    case OpcodeBytes.RES_6_IX_7:
                    case OpcodeBytes.RES_6_IX:
                    case OpcodeBytes.RES_6_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(6, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_7_IX_2:
                    case OpcodeBytes.RES_7_IX_3:
                    case OpcodeBytes.RES_7_IX_4:
                    case OpcodeBytes.RES_7_IX_5:
                    case OpcodeBytes.RES_7_IX_6:
                    case OpcodeBytes.RES_7_IX_7:
                    case OpcodeBytes.RES_7_IX:
                    case OpcodeBytes.RES_7_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(7, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                #endregion

                #region Set Bit

                    case OpcodeBytes.SET_0_IX_2:
                    case OpcodeBytes.SET_0_IX_3:
                    case OpcodeBytes.SET_0_IX_4:
                    case OpcodeBytes.SET_0_IX_5:
                    case OpcodeBytes.SET_0_IX_6:
                    case OpcodeBytes.SET_0_IX_7:
                    case OpcodeBytes.SET_0_IX:
                    case OpcodeBytes.SET_0_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(0, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_1_IX_2:
                    case OpcodeBytes.SET_1_IX_3:
                    case OpcodeBytes.SET_1_IX_4:
                    case OpcodeBytes.SET_1_IX_5:
                    case OpcodeBytes.SET_1_IX_6:
                    case OpcodeBytes.SET_1_IX_7:
                    case OpcodeBytes.SET_1_IX:
                    case OpcodeBytes.SET_1_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(1, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_2_IX_2:
                    case OpcodeBytes.SET_2_IX_3:
                    case OpcodeBytes.SET_2_IX_4:
                    case OpcodeBytes.SET_2_IX_5:
                    case OpcodeBytes.SET_2_IX_6:
                    case OpcodeBytes.SET_2_IX_7:
                    case OpcodeBytes.SET_2_IX:
                    case OpcodeBytes.SET_2_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(2, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_3_IX_2:
                    case OpcodeBytes.SET_3_IX_3:
                    case OpcodeBytes.SET_3_IX_4:
                    case OpcodeBytes.SET_3_IX_5:
                    case OpcodeBytes.SET_3_IX_6:
                    case OpcodeBytes.SET_3_IX_7:
                    case OpcodeBytes.SET_3_IX:
                    case OpcodeBytes.SET_3_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(3, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_4_IX_2:
                    case OpcodeBytes.SET_4_IX_3:
                    case OpcodeBytes.SET_4_IX_4:
                    case OpcodeBytes.SET_4_IX_5:
                    case OpcodeBytes.SET_4_IX_6:
                    case OpcodeBytes.SET_4_IX_7:
                    case OpcodeBytes.SET_4_IX:
                    case OpcodeBytes.SET_4_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(4, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_5_IX_2:
                    case OpcodeBytes.SET_5_IX_3:
                    case OpcodeBytes.SET_5_IX_4:
                    case OpcodeBytes.SET_5_IX_5:
                    case OpcodeBytes.SET_5_IX_6:
                    case OpcodeBytes.SET_5_IX_7:
                    case OpcodeBytes.SET_5_IX:
                    case OpcodeBytes.SET_5_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(5, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_6_IX_2:
                    case OpcodeBytes.SET_6_IX_3:
                    case OpcodeBytes.SET_6_IX_4:
                    case OpcodeBytes.SET_6_IX_5:
                    case OpcodeBytes.SET_6_IX_6:
                    case OpcodeBytes.SET_6_IX_7:
                    case OpcodeBytes.SET_6_IX:
                    case OpcodeBytes.SET_6_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(6, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_7_IX_2:
                    case OpcodeBytes.SET_7_IX_3:
                    case OpcodeBytes.SET_7_IX_4:
                    case OpcodeBytes.SET_7_IX_5:
                    case OpcodeBytes.SET_7_IX_6:
                    case OpcodeBytes.SET_7_IX_7:
                    case OpcodeBytes.SET_7_IX:
                    case OpcodeBytes.SET_7_IX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(7, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, Registers.PC));
            }
        }
    }
}
