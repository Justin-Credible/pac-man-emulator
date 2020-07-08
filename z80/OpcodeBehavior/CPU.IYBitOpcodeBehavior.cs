using System;

namespace JustinCredible.ZilogZ80
{
    public partial class CPU
    {
        private void ExecuteIYBitOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            incrementProgramCounter = true;
            useAlternateCycleCount = false;

            switch (opcode.Code)
            {
                #region Rotate

                    #region RLC (IY+n), r - Rotate left

                        case OpcodeBytes.RLC_MIY_B:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.B = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIY_C:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.C = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIY_D:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.D = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIY_E:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.E = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIY_H:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.H = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIY_L:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.L = ExecuteRotate(value, left: true);
                            break;
                        }
                        case OpcodeBytes.RLC_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteRotate(value, left: true);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }
                        case OpcodeBytes.RLC_MIY_A:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = ExecuteRotate(value, left: true);
                            break;
                        }

                    #endregion

                    #region RRC (IY+n), r - Rotate right

                        case OpcodeBytes.RRC_MIY_B:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.B = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIY_C:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.C = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIY_D:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.D = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIY_E:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.E = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIY_H:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.H = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIY_L:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.L = ExecuteRotate(value, left: false);
                            break;
                        }
                        case OpcodeBytes.RRC_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteRotate(value, left: false);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }
                        case OpcodeBytes.RRC_MIY_A:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = ExecuteRotate(value, left: false);
                            break;
                        }

                    #endregion

                    #region RL (IY+n), r - Rotate left through carry

                        case OpcodeBytes.RL_MIY_B:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.B = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIY_C:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.C = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIY_D:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.D = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIY_E:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.E = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIY_H:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.H = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIY_L:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.L = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RL_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }
                        case OpcodeBytes.RL_MIY_A:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            break;
                        }

                    #endregion

                    #region RR (IY+n), r - Rotate right through carry

                        case OpcodeBytes.RR_MIY_B:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.B = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIY_C:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.C = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIY_D:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.D = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIY_E:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.E = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIY_H:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.H = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIY_L:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.L = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }
                        case OpcodeBytes.RR_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }
                        case OpcodeBytes.RR_MIY_A:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            break;
                        }

                    #endregion

                #endregion

                #region Shift

                    #region SLA (IY+n), r - Shift left (arithmetic)
                        case OpcodeBytes.SLA_MIY_B:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.B = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIY_C:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.C = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIY_D:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.D = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIY_E:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.E = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIY_H:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.H = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIY_L:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.L = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLA_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteShiftArithmetic(value: value, left: true);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }
                        case OpcodeBytes.SLA_MIY_A:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = ExecuteShiftArithmetic(value: value, left: true);
                            break;
                        }
                    #endregion

                    #region SRA (IY+n), r - Shift right (arithmetic)
                        case OpcodeBytes.SRA_MIY_B:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.B = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIY_C:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.C = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIY_D:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.D = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIY_E:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.E = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIY_H:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.H = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIY_L:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.L = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRA_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteShiftArithmetic(value: value, left: false);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }
                        case OpcodeBytes.SRA_MIY_A:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = ExecuteShiftArithmetic(value: value, left: false);
                            break;
                        }
                    #endregion

                    // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
                    // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
                    #region SLL (IY+n), r - Shift left ?? (undocumented)
                        case OpcodeBytes.SLL_MIY_B:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.B = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIY_C:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.C = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIY_D:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.D = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIY_E:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.E = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIY_H:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.H = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIY_L:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.L = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                        case OpcodeBytes.SLL_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteShiftLogical(value: value, left: true);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }
                        case OpcodeBytes.SLL_MIY_A:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = ExecuteShiftLogical(value: value, left: true);
                            break;
                        }
                    #endregion

                    #region SRL (IY+n), r - Shift right logical
                        case OpcodeBytes.SRL_MIY_B:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.B = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIY_C:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.C = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIY_D:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.D = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIY_E:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.E = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIY_H:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.H = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIY_L:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.L = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                        case OpcodeBytes.SRL_MIY:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteShiftLogical(value: value, left: false);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }
                        case OpcodeBytes.SRL_MIY_A:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            Registers.A = ExecuteShiftLogical(value: value, left: false);
                            break;
                        }
                    #endregion

                #endregion

                #region Test Bit

                    case OpcodeBytes.BIT_0_MIY_2:
                    case OpcodeBytes.BIT_0_MIY_3:
                    case OpcodeBytes.BIT_0_MIY_4:
                    case OpcodeBytes.BIT_0_MIY_5:
                    case OpcodeBytes.BIT_0_MIY_6:
                    case OpcodeBytes.BIT_0_MIY_7:
                    case OpcodeBytes.BIT_0_MIY:
                    case OpcodeBytes.BIT_0_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            ExecuteTestBit(0, value);
                            break;
                        }

                    case OpcodeBytes.BIT_1_MIY_2:
                    case OpcodeBytes.BIT_1_MIY_3:
                    case OpcodeBytes.BIT_1_MIY_4:
                    case OpcodeBytes.BIT_1_MIY_5:
                    case OpcodeBytes.BIT_1_MIY_6:
                    case OpcodeBytes.BIT_1_MIY_7:
                    case OpcodeBytes.BIT_1_MIY:
                    case OpcodeBytes.BIT_1_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            ExecuteTestBit(1, value);
                            break;
                        }

                    case OpcodeBytes.BIT_2_MIY_2:
                    case OpcodeBytes.BIT_2_MIY_3:
                    case OpcodeBytes.BIT_2_MIY_4:
                    case OpcodeBytes.BIT_2_MIY_5:
                    case OpcodeBytes.BIT_2_MIY_6:
                    case OpcodeBytes.BIT_2_MIY_7:
                    case OpcodeBytes.BIT_2_MIY:
                    case OpcodeBytes.BIT_2_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            ExecuteTestBit(2, value);
                            break;
                        }

                    case OpcodeBytes.BIT_3_MIY_2:
                    case OpcodeBytes.BIT_3_MIY_3:
                    case OpcodeBytes.BIT_3_MIY_4:
                    case OpcodeBytes.BIT_3_MIY_5:
                    case OpcodeBytes.BIT_3_MIY_6:
                    case OpcodeBytes.BIT_3_MIY_7:
                    case OpcodeBytes.BIT_3_MIY:
                    case OpcodeBytes.BIT_3_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            ExecuteTestBit(3, value);
                            break;
                        }

                    case OpcodeBytes.BIT_4_MIY_2:
                    case OpcodeBytes.BIT_4_MIY_3:
                    case OpcodeBytes.BIT_4_MIY_4:
                    case OpcodeBytes.BIT_4_MIY_5:
                    case OpcodeBytes.BIT_4_MIY_6:
                    case OpcodeBytes.BIT_4_MIY_7:
                    case OpcodeBytes.BIT_4_MIY:
                    case OpcodeBytes.BIT_4_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            ExecuteTestBit(4, value);
                            break;
                        }

                    case OpcodeBytes.BIT_5_MIY_2:
                    case OpcodeBytes.BIT_5_MIY_3:
                    case OpcodeBytes.BIT_5_MIY_4:
                    case OpcodeBytes.BIT_5_MIY_5:
                    case OpcodeBytes.BIT_5_MIY_6:
                    case OpcodeBytes.BIT_5_MIY_7:
                    case OpcodeBytes.BIT_5_MIY:
                    case OpcodeBytes.BIT_5_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            ExecuteTestBit(5, value);
                            break;
                        }

                    case OpcodeBytes.BIT_6_MIY_2:
                    case OpcodeBytes.BIT_6_MIY_3:
                    case OpcodeBytes.BIT_6_MIY_4:
                    case OpcodeBytes.BIT_6_MIY_5:
                    case OpcodeBytes.BIT_6_MIY_6:
                    case OpcodeBytes.BIT_6_MIY_7:
                    case OpcodeBytes.BIT_6_MIY:
                    case OpcodeBytes.BIT_6_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            ExecuteTestBit(6, value);
                            break;
                        }

                    case OpcodeBytes.BIT_7_MIY_2:
                    case OpcodeBytes.BIT_7_MIY_3:
                    case OpcodeBytes.BIT_7_MIY_4:
                    case OpcodeBytes.BIT_7_MIY_5:
                    case OpcodeBytes.BIT_7_MIY_6:
                    case OpcodeBytes.BIT_7_MIY_7:
                    case OpcodeBytes.BIT_7_MIY:
                    case OpcodeBytes.BIT_7_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            ExecuteTestBit(7, value);
                            break;
                        }

                #endregion

                #region Reset Bit

                    case OpcodeBytes.RES_0_MIY_2:
                    case OpcodeBytes.RES_0_MIY_3:
                    case OpcodeBytes.RES_0_MIY_4:
                    case OpcodeBytes.RES_0_MIY_5:
                    case OpcodeBytes.RES_0_MIY_6:
                    case OpcodeBytes.RES_0_MIY_7:
                    case OpcodeBytes.RES_0_MIY:
                    case OpcodeBytes.RES_0_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteResetBit(0, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_1_MIY_2:
                    case OpcodeBytes.RES_1_MIY_3:
                    case OpcodeBytes.RES_1_MIY_4:
                    case OpcodeBytes.RES_1_MIY_5:
                    case OpcodeBytes.RES_1_MIY_6:
                    case OpcodeBytes.RES_1_MIY_7:
                    case OpcodeBytes.RES_1_MIY:
                    case OpcodeBytes.RES_1_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteResetBit(1, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_2_MIY_2:
                    case OpcodeBytes.RES_2_MIY_3:
                    case OpcodeBytes.RES_2_MIY_4:
                    case OpcodeBytes.RES_2_MIY_5:
                    case OpcodeBytes.RES_2_MIY_6:
                    case OpcodeBytes.RES_2_MIY_7:
                    case OpcodeBytes.RES_2_MIY:
                    case OpcodeBytes.RES_2_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteResetBit(2, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_3_MIY_2:
                    case OpcodeBytes.RES_3_MIY_3:
                    case OpcodeBytes.RES_3_MIY_4:
                    case OpcodeBytes.RES_3_MIY_5:
                    case OpcodeBytes.RES_3_MIY_6:
                    case OpcodeBytes.RES_3_MIY_7:
                    case OpcodeBytes.RES_3_MIY:
                    case OpcodeBytes.RES_3_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteResetBit(3, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_4_MIY_2:
                    case OpcodeBytes.RES_4_MIY_3:
                    case OpcodeBytes.RES_4_MIY_4:
                    case OpcodeBytes.RES_4_MIY_5:
                    case OpcodeBytes.RES_4_MIY_6:
                    case OpcodeBytes.RES_4_MIY_7:
                    case OpcodeBytes.RES_4_MIY:
                    case OpcodeBytes.RES_4_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteResetBit(4, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_5_MIY_2:
                    case OpcodeBytes.RES_5_MIY_3:
                    case OpcodeBytes.RES_5_MIY_4:
                    case OpcodeBytes.RES_5_MIY_5:
                    case OpcodeBytes.RES_5_MIY_6:
                    case OpcodeBytes.RES_5_MIY_7:
                    case OpcodeBytes.RES_5_MIY:
                    case OpcodeBytes.RES_5_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteResetBit(5, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_6_MIY_2:
                    case OpcodeBytes.RES_6_MIY_3:
                    case OpcodeBytes.RES_6_MIY_4:
                    case OpcodeBytes.RES_6_MIY_5:
                    case OpcodeBytes.RES_6_MIY_6:
                    case OpcodeBytes.RES_6_MIY_7:
                    case OpcodeBytes.RES_6_MIY:
                    case OpcodeBytes.RES_6_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteResetBit(6, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.RES_7_MIY_2:
                    case OpcodeBytes.RES_7_MIY_3:
                    case OpcodeBytes.RES_7_MIY_4:
                    case OpcodeBytes.RES_7_MIY_5:
                    case OpcodeBytes.RES_7_MIY_6:
                    case OpcodeBytes.RES_7_MIY_7:
                    case OpcodeBytes.RES_7_MIY:
                    case OpcodeBytes.RES_7_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteResetBit(7, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                #endregion

                #region Set Bit

                    case OpcodeBytes.SET_0_MIY_2:
                    case OpcodeBytes.SET_0_MIY_3:
                    case OpcodeBytes.SET_0_MIY_4:
                    case OpcodeBytes.SET_0_MIY_5:
                    case OpcodeBytes.SET_0_MIY_6:
                    case OpcodeBytes.SET_0_MIY_7:
                    case OpcodeBytes.SET_0_MIY:
                    case OpcodeBytes.SET_0_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteSetBit(0, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_1_MIY_2:
                    case OpcodeBytes.SET_1_MIY_3:
                    case OpcodeBytes.SET_1_MIY_4:
                    case OpcodeBytes.SET_1_MIY_5:
                    case OpcodeBytes.SET_1_MIY_6:
                    case OpcodeBytes.SET_1_MIY_7:
                    case OpcodeBytes.SET_1_MIY:
                    case OpcodeBytes.SET_1_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteSetBit(1, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_2_MIY_2:
                    case OpcodeBytes.SET_2_MIY_3:
                    case OpcodeBytes.SET_2_MIY_4:
                    case OpcodeBytes.SET_2_MIY_5:
                    case OpcodeBytes.SET_2_MIY_6:
                    case OpcodeBytes.SET_2_MIY_7:
                    case OpcodeBytes.SET_2_MIY:
                    case OpcodeBytes.SET_2_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteSetBit(2, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_3_MIY_2:
                    case OpcodeBytes.SET_3_MIY_3:
                    case OpcodeBytes.SET_3_MIY_4:
                    case OpcodeBytes.SET_3_MIY_5:
                    case OpcodeBytes.SET_3_MIY_6:
                    case OpcodeBytes.SET_3_MIY_7:
                    case OpcodeBytes.SET_3_MIY:
                    case OpcodeBytes.SET_3_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteSetBit(3, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_4_MIY_2:
                    case OpcodeBytes.SET_4_MIY_3:
                    case OpcodeBytes.SET_4_MIY_4:
                    case OpcodeBytes.SET_4_MIY_5:
                    case OpcodeBytes.SET_4_MIY_6:
                    case OpcodeBytes.SET_4_MIY_7:
                    case OpcodeBytes.SET_4_MIY:
                    case OpcodeBytes.SET_4_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteSetBit(4, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_5_MIY_2:
                    case OpcodeBytes.SET_5_MIY_3:
                    case OpcodeBytes.SET_5_MIY_4:
                    case OpcodeBytes.SET_5_MIY_5:
                    case OpcodeBytes.SET_5_MIY_6:
                    case OpcodeBytes.SET_5_MIY_7:
                    case OpcodeBytes.SET_5_MIY:
                    case OpcodeBytes.SET_5_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteSetBit(5, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_6_MIY_2:
                    case OpcodeBytes.SET_6_MIY_3:
                    case OpcodeBytes.SET_6_MIY_4:
                    case OpcodeBytes.SET_6_MIY_5:
                    case OpcodeBytes.SET_6_MIY_6:
                    case OpcodeBytes.SET_6_MIY_7:
                    case OpcodeBytes.SET_6_MIY:
                    case OpcodeBytes.SET_6_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteSetBit(6, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                    case OpcodeBytes.SET_7_MIY_2:
                    case OpcodeBytes.SET_7_MIY_3:
                    case OpcodeBytes.SET_7_MIY_4:
                    case OpcodeBytes.SET_7_MIY_5:
                    case OpcodeBytes.SET_7_MIY_6:
                    case OpcodeBytes.SET_7_MIY_7:
                    case OpcodeBytes.SET_7_MIY:
                    case OpcodeBytes.SET_7_MIY_8:
                        {
                            var offset = (sbyte)Memory.Read(Registers.PC + 2);
                            var value = Memory.Read(Registers.IY + offset);
                            value = ExecuteSetBit(7, value);
                            Memory.Write(Registers.IY + offset, value);
                            break;
                        }

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode.Code, Registers.PC));
            }
        }
    }
}
