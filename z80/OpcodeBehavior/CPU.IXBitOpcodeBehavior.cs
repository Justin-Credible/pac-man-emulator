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

                        case OpcodeBytes.RLC_MIX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteRotate(value, left: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.RLC_MIX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteRotate(value, left: true);
                            break;
                        }

                    #endregion

                    #region RRC (IX+n), r - Rotate right

                        case OpcodeBytes.RRC_MIX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteRotate(value, left: false);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.RRC_MIX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteRotate(value, left: false);
                            break;
                        }

                    #endregion

                    #region RL (IX+n), r - Rotate left through carry

                        case OpcodeBytes.RL_MIX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.RL_MIX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }

                    #endregion

                    #region RR (IX+n), r - Rotate right through carry

                        case OpcodeBytes.RR_MIX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.RR_MIX_A:
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
                        case OpcodeBytes.SLA_MIX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteShiftArithmetic(value: value, left: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.SLA_MIX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                    #endregion

                    #region SRA (IX+n), r - Shift right (arithmetic)
                        case OpcodeBytes.SRA_MIX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteShiftArithmetic(value: value, left: false);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.SRA_MIX_A:
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
                        case OpcodeBytes.SLL_MIX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteShiftLogical(value: value, left: true);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.SLL_MIX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                    #endregion

                    #region SRL (IX+n), r - Shift right logical
                        case OpcodeBytes.SRL_MIX_B:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.B = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIX_C:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.C = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIX_D:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.D = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIX_E:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.E = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIX_H:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.H = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIX_L:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.L = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteShiftLogical(value: value, left: false);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }
                        case OpcodeBytes.SRL_MIX_A:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                    #endregion

                #endregion

                #region Test Bit

                    case OpcodeBytes.BIT_0_MIX_2:
                    case OpcodeBytes.BIT_0_MIX_3:
                    case OpcodeBytes.BIT_0_MIX_4:
                    case OpcodeBytes.BIT_0_MIX_5:
                    case OpcodeBytes.BIT_0_MIX_6:
                    case OpcodeBytes.BIT_0_MIX_7:
                    case OpcodeBytes.BIT_0_MIX:
                    case OpcodeBytes.BIT_0_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(0, value);
                            break;
                        }

                    case OpcodeBytes.BIT_1_MIX_2:
                    case OpcodeBytes.BIT_1_MIX_3:
                    case OpcodeBytes.BIT_1_MIX_4:
                    case OpcodeBytes.BIT_1_MIX_5:
                    case OpcodeBytes.BIT_1_MIX_6:
                    case OpcodeBytes.BIT_1_MIX_7:
                    case OpcodeBytes.BIT_1_MIX:
                    case OpcodeBytes.BIT_1_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(1, value);
                            break;
                        }

                    case OpcodeBytes.BIT_2_MIX_2:
                    case OpcodeBytes.BIT_2_MIX_3:
                    case OpcodeBytes.BIT_2_MIX_4:
                    case OpcodeBytes.BIT_2_MIX_5:
                    case OpcodeBytes.BIT_2_MIX_6:
                    case OpcodeBytes.BIT_2_MIX_7:
                    case OpcodeBytes.BIT_2_MIX:
                    case OpcodeBytes.BIT_2_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(2, value);
                            break;
                        }

                    case OpcodeBytes.BIT_3_MIX_2:
                    case OpcodeBytes.BIT_3_MIX_3:
                    case OpcodeBytes.BIT_3_MIX_4:
                    case OpcodeBytes.BIT_3_MIX_5:
                    case OpcodeBytes.BIT_3_MIX_6:
                    case OpcodeBytes.BIT_3_MIX_7:
                    case OpcodeBytes.BIT_3_MIX:
                    case OpcodeBytes.BIT_3_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(3, value);
                            break;
                        }

                    case OpcodeBytes.BIT_4_MIX_2:
                    case OpcodeBytes.BIT_4_MIX_3:
                    case OpcodeBytes.BIT_4_MIX_4:
                    case OpcodeBytes.BIT_4_MIX_5:
                    case OpcodeBytes.BIT_4_MIX_6:
                    case OpcodeBytes.BIT_4_MIX_7:
                    case OpcodeBytes.BIT_4_MIX:
                    case OpcodeBytes.BIT_4_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(4, value);
                            break;
                        }

                    case OpcodeBytes.BIT_5_MIX_2:
                    case OpcodeBytes.BIT_5_MIX_3:
                    case OpcodeBytes.BIT_5_MIX_4:
                    case OpcodeBytes.BIT_5_MIX_5:
                    case OpcodeBytes.BIT_5_MIX_6:
                    case OpcodeBytes.BIT_5_MIX_7:
                    case OpcodeBytes.BIT_5_MIX:
                    case OpcodeBytes.BIT_5_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(5, value);
                            break;
                        }

                    case OpcodeBytes.BIT_6_MIX_2:
                    case OpcodeBytes.BIT_6_MIX_3:
                    case OpcodeBytes.BIT_6_MIX_4:
                    case OpcodeBytes.BIT_6_MIX_5:
                    case OpcodeBytes.BIT_6_MIX_6:
                    case OpcodeBytes.BIT_6_MIX_7:
                    case OpcodeBytes.BIT_6_MIX:
                    case OpcodeBytes.BIT_6_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(6, value);
                            break;
                        }

                    case OpcodeBytes.BIT_7_MIX_2:
                    case OpcodeBytes.BIT_7_MIX_3:
                    case OpcodeBytes.BIT_7_MIX_4:
                    case OpcodeBytes.BIT_7_MIX_5:
                    case OpcodeBytes.BIT_7_MIX_6:
                    case OpcodeBytes.BIT_7_MIX_7:
                    case OpcodeBytes.BIT_7_MIX:
                    case OpcodeBytes.BIT_7_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            ExecuteTestBit(7, value);
                            break;
                        }

                #endregion

                #region Reset Bit

                    case OpcodeBytes.RES_0_MIX_2:
                    case OpcodeBytes.RES_0_MIX_3:
                    case OpcodeBytes.RES_0_MIX_4:
                    case OpcodeBytes.RES_0_MIX_5:
                    case OpcodeBytes.RES_0_MIX_6:
                    case OpcodeBytes.RES_0_MIX_7:
                    case OpcodeBytes.RES_0_MIX:
                    case OpcodeBytes.RES_0_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(0, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_1_MIX_2:
                    case OpcodeBytes.RES_1_MIX_3:
                    case OpcodeBytes.RES_1_MIX_4:
                    case OpcodeBytes.RES_1_MIX_5:
                    case OpcodeBytes.RES_1_MIX_6:
                    case OpcodeBytes.RES_1_MIX_7:
                    case OpcodeBytes.RES_1_MIX:
                    case OpcodeBytes.RES_1_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(1, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_2_MIX_2:
                    case OpcodeBytes.RES_2_MIX_3:
                    case OpcodeBytes.RES_2_MIX_4:
                    case OpcodeBytes.RES_2_MIX_5:
                    case OpcodeBytes.RES_2_MIX_6:
                    case OpcodeBytes.RES_2_MIX_7:
                    case OpcodeBytes.RES_2_MIX:
                    case OpcodeBytes.RES_2_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(2, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_3_MIX_2:
                    case OpcodeBytes.RES_3_MIX_3:
                    case OpcodeBytes.RES_3_MIX_4:
                    case OpcodeBytes.RES_3_MIX_5:
                    case OpcodeBytes.RES_3_MIX_6:
                    case OpcodeBytes.RES_3_MIX_7:
                    case OpcodeBytes.RES_3_MIX:
                    case OpcodeBytes.RES_3_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(3, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_4_MIX_2:
                    case OpcodeBytes.RES_4_MIX_3:
                    case OpcodeBytes.RES_4_MIX_4:
                    case OpcodeBytes.RES_4_MIX_5:
                    case OpcodeBytes.RES_4_MIX_6:
                    case OpcodeBytes.RES_4_MIX_7:
                    case OpcodeBytes.RES_4_MIX:
                    case OpcodeBytes.RES_4_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(4, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_5_MIX_2:
                    case OpcodeBytes.RES_5_MIX_3:
                    case OpcodeBytes.RES_5_MIX_4:
                    case OpcodeBytes.RES_5_MIX_5:
                    case OpcodeBytes.RES_5_MIX_6:
                    case OpcodeBytes.RES_5_MIX_7:
                    case OpcodeBytes.RES_5_MIX:
                    case OpcodeBytes.RES_5_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(5, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_6_MIX_2:
                    case OpcodeBytes.RES_6_MIX_3:
                    case OpcodeBytes.RES_6_MIX_4:
                    case OpcodeBytes.RES_6_MIX_5:
                    case OpcodeBytes.RES_6_MIX_6:
                    case OpcodeBytes.RES_6_MIX_7:
                    case OpcodeBytes.RES_6_MIX:
                    case OpcodeBytes.RES_6_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(6, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_7_MIX_2:
                    case OpcodeBytes.RES_7_MIX_3:
                    case OpcodeBytes.RES_7_MIX_4:
                    case OpcodeBytes.RES_7_MIX_5:
                    case OpcodeBytes.RES_7_MIX_6:
                    case OpcodeBytes.RES_7_MIX_7:
                    case OpcodeBytes.RES_7_MIX:
                    case OpcodeBytes.RES_7_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteResetBit(7, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                #endregion

                #region Set Bit

                    case OpcodeBytes.SET_0_MIX_2:
                    case OpcodeBytes.SET_0_MIX_3:
                    case OpcodeBytes.SET_0_MIX_4:
                    case OpcodeBytes.SET_0_MIX_5:
                    case OpcodeBytes.SET_0_MIX_6:
                    case OpcodeBytes.SET_0_MIX_7:
                    case OpcodeBytes.SET_0_MIX:
                    case OpcodeBytes.SET_0_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(0, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_1_MIX_2:
                    case OpcodeBytes.SET_1_MIX_3:
                    case OpcodeBytes.SET_1_MIX_4:
                    case OpcodeBytes.SET_1_MIX_5:
                    case OpcodeBytes.SET_1_MIX_6:
                    case OpcodeBytes.SET_1_MIX_7:
                    case OpcodeBytes.SET_1_MIX:
                    case OpcodeBytes.SET_1_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(1, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_2_MIX_2:
                    case OpcodeBytes.SET_2_MIX_3:
                    case OpcodeBytes.SET_2_MIX_4:
                    case OpcodeBytes.SET_2_MIX_5:
                    case OpcodeBytes.SET_2_MIX_6:
                    case OpcodeBytes.SET_2_MIX_7:
                    case OpcodeBytes.SET_2_MIX:
                    case OpcodeBytes.SET_2_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(2, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_3_MIX_2:
                    case OpcodeBytes.SET_3_MIX_3:
                    case OpcodeBytes.SET_3_MIX_4:
                    case OpcodeBytes.SET_3_MIX_5:
                    case OpcodeBytes.SET_3_MIX_6:
                    case OpcodeBytes.SET_3_MIX_7:
                    case OpcodeBytes.SET_3_MIX:
                    case OpcodeBytes.SET_3_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(3, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_4_MIX_2:
                    case OpcodeBytes.SET_4_MIX_3:
                    case OpcodeBytes.SET_4_MIX_4:
                    case OpcodeBytes.SET_4_MIX_5:
                    case OpcodeBytes.SET_4_MIX_6:
                    case OpcodeBytes.SET_4_MIX_7:
                    case OpcodeBytes.SET_4_MIX:
                    case OpcodeBytes.SET_4_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(4, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_5_MIX_2:
                    case OpcodeBytes.SET_5_MIX_3:
                    case OpcodeBytes.SET_5_MIX_4:
                    case OpcodeBytes.SET_5_MIX_5:
                    case OpcodeBytes.SET_5_MIX_6:
                    case OpcodeBytes.SET_5_MIX_7:
                    case OpcodeBytes.SET_5_MIX:
                    case OpcodeBytes.SET_5_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(5, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_6_MIX_2:
                    case OpcodeBytes.SET_6_MIX_3:
                    case OpcodeBytes.SET_6_MIX_4:
                    case OpcodeBytes.SET_6_MIX_5:
                    case OpcodeBytes.SET_6_MIX_6:
                    case OpcodeBytes.SET_6_MIX_7:
                    case OpcodeBytes.SET_6_MIX:
                    case OpcodeBytes.SET_6_MIX_8:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            value = ExecuteSetBit(6, value);
                            WriteMemory(Registers.IX + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_7_MIX_2:
                    case OpcodeBytes.SET_7_MIX_3:
                    case OpcodeBytes.SET_7_MIX_4:
                    case OpcodeBytes.SET_7_MIX_5:
                    case OpcodeBytes.SET_7_MIX_6:
                    case OpcodeBytes.SET_7_MIX_7:
                    case OpcodeBytes.SET_7_MIX:
                    case OpcodeBytes.SET_7_MIX_8:
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
