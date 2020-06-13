using System;

namespace JustinCredible.ZilogZ80
{
    public partial class CPU
    {
        private void ExecuteBitOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            incrementProgramCounter = true;
            useAlternateCycleCount = false;

            switch (opcode.Code)
            {
                #region Rotate

                    #region RLC r - Rotate left

                        case OpcodeBytes.RLC_B:
                            Registers.B = ExecuteRotate(Registers.B, left: true);
                            break;
                        case OpcodeBytes.RLC_C:
                            Registers.C = ExecuteRotate(Registers.C, left: true);
                            break;
                        case OpcodeBytes.RLC_D:
                            Registers.D = ExecuteRotate(Registers.D, left: true);
                            break;
                        case OpcodeBytes.RLC_E:
                            Registers.E = ExecuteRotate(Registers.E, left: true);
                            break;
                        case OpcodeBytes.RLC_H:
                            Registers.H = ExecuteRotate(Registers.H, left: true);
                            break;
                        case OpcodeBytes.RLC_L:
                            Registers.L = ExecuteRotate(Registers.L, left: true);
                            break;
                        case OpcodeBytes.RLC_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value = ExecuteRotate(value, left: true);
                            WriteMemory(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.RLC_A:
                            Registers.A = ExecuteRotate(Registers.A, left: true);
                            break;

                    #endregion

                    #region RRC r - Rotate right

                        case OpcodeBytes.RRC_B:
                            Registers.B = ExecuteRotate(Registers.B, left: false);
                            break;
                        case OpcodeBytes.RRC_C:
                            Registers.C = ExecuteRotate(Registers.C, left: false);
                            break;
                        case OpcodeBytes.RRC_D:
                            Registers.D = ExecuteRotate(Registers.D, left: false);
                            break;
                        case OpcodeBytes.RRC_E:
                            Registers.E = ExecuteRotate(Registers.E, left: false);
                            break;
                        case OpcodeBytes.RRC_H:
                            Registers.H = ExecuteRotate(Registers.H, left: false);
                            break;
                        case OpcodeBytes.RRC_L:
                            Registers.L = ExecuteRotate(Registers.L, left: false);
                            break;
                        case OpcodeBytes.RRC_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value = ExecuteRotate(value, left: false);
                            WriteMemory(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.RRC_A:
                            Registers.A = ExecuteRotate(Registers.A, left: false);
                            break;

                    #endregion

                    #region RL r - Rotate left through carry

                        case OpcodeBytes.RL_B:
                            Registers.B = ExecuteRotate(Registers.B, left: true, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RL_C:
                            Registers.C = ExecuteRotate(Registers.C, left: true, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RL_D:
                            Registers.D = ExecuteRotate(Registers.D, left: true, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RL_E:
                            Registers.E = ExecuteRotate(Registers.E, left: true, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RL_H:
                            Registers.H = ExecuteRotate(Registers.H, left: true, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RL_L:
                            Registers.L = ExecuteRotate(Registers.L, left: true, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RL_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value = ExecuteRotate(value, left: true, rotateThroughCarry: true);
                            WriteMemory(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.RL_A:
                            Registers.A = ExecuteRotate(Registers.A, left: true, rotateThroughCarry: true);
                            break;

                    #endregion

                    #region RR r - Rotate right through carry

                        case OpcodeBytes.RR_B:
                            Registers.B = ExecuteRotate(Registers.B, left: false, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RR_C:
                            Registers.C = ExecuteRotate(Registers.C, left: false, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RR_D:
                            Registers.D = ExecuteRotate(Registers.D, left: false, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RR_E:
                            Registers.E = ExecuteRotate(Registers.E, left: false, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RR_H:
                            Registers.H = ExecuteRotate(Registers.H, left: false, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RR_L:
                            Registers.L = ExecuteRotate(Registers.L, left: false, rotateThroughCarry: true);
                            break;
                        case OpcodeBytes.RR_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value = ExecuteRotate(value, left: false, rotateThroughCarry: true);
                            WriteMemory(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.RR_A:
                            Registers.A = ExecuteRotate(Registers.A, left: false, rotateThroughCarry: true);
                            break;

                    #endregion

                #endregion

                #region Shift

                    #region SLA r - Shift left (arithmetic)
                        case OpcodeBytes.SLA_B:
                            Registers.B = ExecuteShiftArithmetic(value: Registers.B, left: true);
                            break;
                        case OpcodeBytes.SLA_C:
                            Registers.C = ExecuteShiftArithmetic(value: Registers.C, left: true);
                            break;
                        case OpcodeBytes.SLA_D:
                            Registers.D = ExecuteShiftArithmetic(value: Registers.D, left: true);
                            break;
                        case OpcodeBytes.SLA_E:
                            Registers.E = ExecuteShiftArithmetic(value: Registers.E, left: true);
                            break;
                        case OpcodeBytes.SLA_H:
                            Registers.H = ExecuteShiftArithmetic(value: Registers.H, left: true);
                            break;
                        case OpcodeBytes.SLA_L:
                            Registers.L = ExecuteShiftArithmetic(value: Registers.L, left: true);
                            break;
                        case OpcodeBytes.SLA_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value = ExecuteShiftArithmetic(value: value, left: true);
                            WriteMemory(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.SLA_A:
                            Registers.A = ExecuteShiftArithmetic(value: Registers.A, left: true);
                            break;
                    #endregion

                    #region SRA r - Shift right (arithmetic)
                        case OpcodeBytes.SRA_B:
                            Registers.B = ExecuteShiftArithmetic(value: Registers.B, left: false);
                            break;
                        case OpcodeBytes.SRA_C:
                            Registers.C = ExecuteShiftArithmetic(value: Registers.C, left: false);
                            break;
                        case OpcodeBytes.SRA_D:
                            Registers.D = ExecuteShiftArithmetic(value: Registers.D, left: false);
                            break;
                        case OpcodeBytes.SRA_E:
                            Registers.E = ExecuteShiftArithmetic(value: Registers.E, left: false);
                            break;
                        case OpcodeBytes.SRA_H:
                            Registers.H = ExecuteShiftArithmetic(value: Registers.H, left: false);
                            break;
                        case OpcodeBytes.SRA_L:
                            Registers.L = ExecuteShiftArithmetic(value: Registers.L, left: false);
                            break;
                        case OpcodeBytes.SRA_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value = ExecuteShiftArithmetic(value: value, left: false);
                            WriteMemory(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.SRA_A:
                            Registers.A = ExecuteShiftArithmetic(value: Registers.A, left: false);
                            break;
                    #endregion

                    // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
                    // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
                    #region SLL r - Shift left ?? (undocumented)
                        case OpcodeBytes.SLL_B:
                            Registers.B = ExecuteShiftLogical(value: Registers.B, left: true);
                            break;
                        case OpcodeBytes.SLL_C:
                            Registers.C = ExecuteShiftLogical(value: Registers.C, left: true);
                            break;
                        case OpcodeBytes.SLL_D:
                            Registers.D = ExecuteShiftLogical(value: Registers.D, left: true);
                            break;
                        case OpcodeBytes.SLL_E:
                            Registers.E = ExecuteShiftLogical(value: Registers.E, left: true);
                            break;
                        case OpcodeBytes.SLL_H:
                            Registers.H = ExecuteShiftLogical(value: Registers.H, left: true);
                            break;
                        case OpcodeBytes.SLL_L:
                            Registers.L = ExecuteShiftLogical(value: Registers.L, left: true);
                            break;
                        case OpcodeBytes.SLL_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value = ExecuteShiftLogical(value: value, left: true);
                            WriteMemory(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.SLL_A:
                            Registers.A = ExecuteShiftLogical(value: Registers.A, left: true);
                            break;
                    #endregion

                    #region SRL r - Shift right logical
                        case OpcodeBytes.SRL_B:
                            Registers.B = ExecuteShiftLogical(value: Registers.B, left: false);
                            break;
                        case OpcodeBytes.SRL_C:
                            Registers.C = ExecuteShiftLogical(value: Registers.C, left: false);
                            break;
                        case OpcodeBytes.SRL_D:
                            Registers.D = ExecuteShiftLogical(value: Registers.D, left: false);
                            break;
                        case OpcodeBytes.SRL_E:
                            Registers.E = ExecuteShiftLogical(value: Registers.E, left: false);
                            break;
                        case OpcodeBytes.SRL_H:
                            Registers.H = ExecuteShiftLogical(value: Registers.H, left: false);
                            break;
                        case OpcodeBytes.SRL_L:
                            Registers.L = ExecuteShiftLogical(value: Registers.L, left: false);
                            break;
                        case OpcodeBytes.SRL_MHL:
                        {
                            var value = ReadMemory(Registers.HL);
                            value = ExecuteShiftLogical(value: value, left: false);
                            WriteMemory(Registers.HL, value);
                            break;
                        }
                        case OpcodeBytes.SRL_A:
                            Registers.A = ExecuteShiftLogical(value: Registers.A, left: false);
                            break;
                    #endregion

                #endregion

                #region Test Bit

                    case OpcodeBytes.BIT_0_B:
                        ExecuteTestBit(0, Registers.B);
                        break;
                    case OpcodeBytes.BIT_0_C:
                        ExecuteTestBit(0, Registers.C);
                        break;
                    case OpcodeBytes.BIT_0_D:
                        ExecuteTestBit(0, Registers.D);
                        break;
                    case OpcodeBytes.BIT_0_E:
                        ExecuteTestBit(0, Registers.E);
                        break;
                    case OpcodeBytes.BIT_0_H:
                        ExecuteTestBit(0, Registers.H);
                        break;
                    case OpcodeBytes.BIT_0_L:
                        ExecuteTestBit(0, Registers.L);
                        break;
                    case OpcodeBytes.BIT_0_MHL:
                        ExecuteTestBit(0, ReadMemory(Registers.HL));
                        break;
                    case OpcodeBytes.BIT_0_A:
                        ExecuteTestBit(0, Registers.A);
                        break;

                    case OpcodeBytes.BIT_1_B:
                        ExecuteTestBit(1, Registers.B);
                        break;
                    case OpcodeBytes.BIT_1_C:
                        ExecuteTestBit(1, Registers.C);
                        break;
                    case OpcodeBytes.BIT_1_D:
                        ExecuteTestBit(1, Registers.D);
                        break;
                    case OpcodeBytes.BIT_1_E:
                        ExecuteTestBit(1, Registers.E);
                        break;
                    case OpcodeBytes.BIT_1_H:
                        ExecuteTestBit(1, Registers.H);
                        break;
                    case OpcodeBytes.BIT_1_L:
                        ExecuteTestBit(1, Registers.L);
                        break;
                    case OpcodeBytes.BIT_1_MHL:
                        ExecuteTestBit(1, ReadMemory(Registers.HL));
                        break;
                    case OpcodeBytes.BIT_1_A:
                        ExecuteTestBit(1, Registers.A);
                        break;

                    case OpcodeBytes.BIT_2_B:
                        ExecuteTestBit(2, Registers.B);
                        break;
                    case OpcodeBytes.BIT_2_C:
                        ExecuteTestBit(2, Registers.C);
                        break;
                    case OpcodeBytes.BIT_2_D:
                        ExecuteTestBit(2, Registers.D);
                        break;
                    case OpcodeBytes.BIT_2_E:
                        ExecuteTestBit(2, Registers.E);
                        break;
                    case OpcodeBytes.BIT_2_H:
                        ExecuteTestBit(2, Registers.H);
                        break;
                    case OpcodeBytes.BIT_2_L:
                        ExecuteTestBit(2, Registers.L);
                        break;
                    case OpcodeBytes.BIT_2_MHL:
                        ExecuteTestBit(2, ReadMemory(Registers.HL));
                        break;
                    case OpcodeBytes.BIT_2_A:
                        ExecuteTestBit(2, Registers.A);
                        break;

                    case OpcodeBytes.BIT_3_B:
                        ExecuteTestBit(3, Registers.B);
                        break;
                    case OpcodeBytes.BIT_3_C:
                        ExecuteTestBit(3, Registers.C);
                        break;
                    case OpcodeBytes.BIT_3_D:
                        ExecuteTestBit(3, Registers.D);
                        break;
                    case OpcodeBytes.BIT_3_E:
                        ExecuteTestBit(3, Registers.E);
                        break;
                    case OpcodeBytes.BIT_3_H:
                        ExecuteTestBit(3, Registers.H);
                        break;
                    case OpcodeBytes.BIT_3_L:
                        ExecuteTestBit(3, Registers.L);
                        break;
                    case OpcodeBytes.BIT_3_MHL:
                        ExecuteTestBit(3, ReadMemory(Registers.HL));
                        break;
                    case OpcodeBytes.BIT_3_A:
                        ExecuteTestBit(3, Registers.A);
                        break;

                    case OpcodeBytes.BIT_4_B:
                        ExecuteTestBit(4, Registers.B);
                        break;
                    case OpcodeBytes.BIT_4_C:
                        ExecuteTestBit(4, Registers.C);
                        break;
                    case OpcodeBytes.BIT_4_D:
                        ExecuteTestBit(4, Registers.D);
                        break;
                    case OpcodeBytes.BIT_4_E:
                        ExecuteTestBit(4, Registers.E);
                        break;
                    case OpcodeBytes.BIT_4_H:
                        ExecuteTestBit(4, Registers.H);
                        break;
                    case OpcodeBytes.BIT_4_L:
                        ExecuteTestBit(4, Registers.L);
                        break;
                    case OpcodeBytes.BIT_4_MHL:
                        ExecuteTestBit(4, ReadMemory(Registers.HL));
                        break;
                    case OpcodeBytes.BIT_4_A:
                        ExecuteTestBit(4, Registers.A);
                        break;

                    case OpcodeBytes.BIT_5_B:
                        ExecuteTestBit(5, Registers.B);
                        break;
                    case OpcodeBytes.BIT_5_C:
                        ExecuteTestBit(5, Registers.C);
                        break;
                    case OpcodeBytes.BIT_5_D:
                        ExecuteTestBit(5, Registers.D);
                        break;
                    case OpcodeBytes.BIT_5_E:
                        ExecuteTestBit(5, Registers.E);
                        break;
                    case OpcodeBytes.BIT_5_H:
                        ExecuteTestBit(5, Registers.H);
                        break;
                    case OpcodeBytes.BIT_5_L:
                        ExecuteTestBit(5, Registers.L);
                        break;
                    case OpcodeBytes.BIT_5_MHL:
                        ExecuteTestBit(5, ReadMemory(Registers.HL));
                        break;
                    case OpcodeBytes.BIT_5_A:
                        ExecuteTestBit(5, Registers.A);
                        break;

                    case OpcodeBytes.BIT_6_B:
                        ExecuteTestBit(6, Registers.B);
                        break;
                    case OpcodeBytes.BIT_6_C:
                        ExecuteTestBit(6, Registers.C);
                        break;
                    case OpcodeBytes.BIT_6_D:
                        ExecuteTestBit(6, Registers.D);
                        break;
                    case OpcodeBytes.BIT_6_E:
                        ExecuteTestBit(6, Registers.E);
                        break;
                    case OpcodeBytes.BIT_6_H:
                        ExecuteTestBit(6, Registers.H);
                        break;
                    case OpcodeBytes.BIT_6_L:
                        ExecuteTestBit(6, Registers.L);
                        break;
                    case OpcodeBytes.BIT_6_MHL:
                        ExecuteTestBit(6, ReadMemory(Registers.HL));
                        break;
                    case OpcodeBytes.BIT_6_A:
                        ExecuteTestBit(6, Registers.A);
                        break;

                    case OpcodeBytes.BIT_7_B:
                        ExecuteTestBit(7, Registers.B);
                        break;
                    case OpcodeBytes.BIT_7_C:
                        ExecuteTestBit(7, Registers.C);
                        break;
                    case OpcodeBytes.BIT_7_D:
                        ExecuteTestBit(7, Registers.D);
                        break;
                    case OpcodeBytes.BIT_7_E:
                        ExecuteTestBit(7, Registers.E);
                        break;
                    case OpcodeBytes.BIT_7_H:
                        ExecuteTestBit(7, Registers.H);
                        break;
                    case OpcodeBytes.BIT_7_L:
                        ExecuteTestBit(7, Registers.L);
                        break;
                    case OpcodeBytes.BIT_7_MHL:
                        ExecuteTestBit(7, ReadMemory(Registers.HL));
                        break;
                    case OpcodeBytes.BIT_7_A:
                        ExecuteTestBit(7, Registers.A);
                        break;

                #endregion

                #region Reset Bit

                    case OpcodeBytes.RES_0_B:
                        Registers.B = ExecuteResetBit(0, Registers.B);
                        break;
                    case OpcodeBytes.RES_0_C:
                        Registers.C = ExecuteResetBit(0, Registers.C);
                        break;
                    case OpcodeBytes.RES_0_D:
                        Registers.D = ExecuteResetBit(0, Registers.D);
                        break;
                    case OpcodeBytes.RES_0_E:
                        Registers.E = ExecuteResetBit(0, Registers.E);
                        break;
                    case OpcodeBytes.RES_0_H:
                        Registers.H = ExecuteResetBit(0, Registers.H);
                        break;
                    case OpcodeBytes.RES_0_L:
                        Registers.L = ExecuteResetBit(0, Registers.L);
                        break;
                    case OpcodeBytes.RES_0_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteResetBit(0, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RES_0_A:
                        Registers.A = ExecuteResetBit(0, Registers.A);
                        break;

                    case OpcodeBytes.RES_1_B:
                        Registers.B = ExecuteResetBit(1, Registers.B);
                        break;
                    case OpcodeBytes.RES_1_C:
                        Registers.C = ExecuteResetBit(1, Registers.C);
                        break;
                    case OpcodeBytes.RES_1_D:
                        Registers.D = ExecuteResetBit(1, Registers.D);
                        break;
                    case OpcodeBytes.RES_1_E:
                        Registers.E = ExecuteResetBit(1, Registers.E);
                        break;
                    case OpcodeBytes.RES_1_H:
                        Registers.H = ExecuteResetBit(1, Registers.H);
                        break;
                    case OpcodeBytes.RES_1_L:
                        Registers.L = ExecuteResetBit(1, Registers.L);
                        break;
                    case OpcodeBytes.RES_1_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteResetBit(1, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RES_1_A:
                        Registers.A = ExecuteResetBit(1, Registers.A);
                        break;

                    case OpcodeBytes.RES_2_B:
                        Registers.B = ExecuteResetBit(2, Registers.B);
                        break;
                    case OpcodeBytes.RES_2_C:
                        Registers.C = ExecuteResetBit(2, Registers.C);
                        break;
                    case OpcodeBytes.RES_2_D:
                        Registers.D = ExecuteResetBit(2, Registers.D);
                        break;
                    case OpcodeBytes.RES_2_E:
                        Registers.E = ExecuteResetBit(2, Registers.E);
                        break;
                    case OpcodeBytes.RES_2_H:
                        Registers.H = ExecuteResetBit(2, Registers.H);
                        break;
                    case OpcodeBytes.RES_2_L:
                        Registers.L = ExecuteResetBit(2, Registers.L);
                        break;
                    case OpcodeBytes.RES_2_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteResetBit(2, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RES_2_A:
                        Registers.A = ExecuteResetBit(2, Registers.A);
                        break;

                    case OpcodeBytes.RES_3_B:
                        Registers.B = ExecuteResetBit(3, Registers.B);
                        break;
                    case OpcodeBytes.RES_3_C:
                        Registers.C = ExecuteResetBit(3, Registers.C);
                        break;
                    case OpcodeBytes.RES_3_D:
                        Registers.D = ExecuteResetBit(3, Registers.D);
                        break;
                    case OpcodeBytes.RES_3_E:
                        Registers.E = ExecuteResetBit(3, Registers.E);
                        break;
                    case OpcodeBytes.RES_3_H:
                        Registers.H = ExecuteResetBit(3, Registers.H);
                        break;
                    case OpcodeBytes.RES_3_L:
                        Registers.L = ExecuteResetBit(3, Registers.L);
                        break;
                    case OpcodeBytes.RES_3_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteResetBit(3, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RES_3_A:
                        Registers.A = ExecuteResetBit(3, Registers.A);
                        break;

                    case OpcodeBytes.RES_4_B:
                        Registers.B = ExecuteResetBit(4, Registers.B);
                        break;
                    case OpcodeBytes.RES_4_C:
                        Registers.C = ExecuteResetBit(4, Registers.C);
                        break;
                    case OpcodeBytes.RES_4_D:
                        Registers.D = ExecuteResetBit(4, Registers.D);
                        break;
                    case OpcodeBytes.RES_4_E:
                        Registers.E = ExecuteResetBit(4, Registers.E);
                        break;
                    case OpcodeBytes.RES_4_H:
                        Registers.H = ExecuteResetBit(4, Registers.H);
                        break;
                    case OpcodeBytes.RES_4_L:
                        Registers.L = ExecuteResetBit(4, Registers.L);
                        break;
                    case OpcodeBytes.RES_4_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteResetBit(4, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RES_4_A:
                        Registers.A = ExecuteResetBit(4, Registers.A);
                        break;

                    case OpcodeBytes.RES_5_B:
                        Registers.B = ExecuteResetBit(5, Registers.B);
                        break;
                    case OpcodeBytes.RES_5_C:
                        Registers.C = ExecuteResetBit(5, Registers.C);
                        break;
                    case OpcodeBytes.RES_5_D:
                        Registers.D = ExecuteResetBit(5, Registers.D);
                        break;
                    case OpcodeBytes.RES_5_E:
                        Registers.E = ExecuteResetBit(5, Registers.E);
                        break;
                    case OpcodeBytes.RES_5_H:
                        Registers.H = ExecuteResetBit(5, Registers.H);
                        break;
                    case OpcodeBytes.RES_5_L:
                        Registers.L = ExecuteResetBit(5, Registers.L);
                        break;
                    case OpcodeBytes.RES_5_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteResetBit(5, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RES_5_A:
                        Registers.A = ExecuteResetBit(5, Registers.A);
                        break;

                    case OpcodeBytes.RES_6_B:
                        Registers.B = ExecuteResetBit(6, Registers.B);
                        break;
                    case OpcodeBytes.RES_6_C:
                        Registers.C = ExecuteResetBit(6, Registers.C);
                        break;
                    case OpcodeBytes.RES_6_D:
                        Registers.D = ExecuteResetBit(6, Registers.D);
                        break;
                    case OpcodeBytes.RES_6_E:
                        Registers.E = ExecuteResetBit(6, Registers.E);
                        break;
                    case OpcodeBytes.RES_6_H:
                        Registers.H = ExecuteResetBit(6, Registers.H);
                        break;
                    case OpcodeBytes.RES_6_L:
                        Registers.L = ExecuteResetBit(6, Registers.L);
                        break;
                    case OpcodeBytes.RES_6_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteResetBit(6, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RES_6_A:
                        Registers.A = ExecuteResetBit(6, Registers.A);
                        break;

                    case OpcodeBytes.RES_7_B:
                        Registers.B = ExecuteResetBit(7, Registers.B);
                        break;
                    case OpcodeBytes.RES_7_C:
                        Registers.C = ExecuteResetBit(7, Registers.C);
                        break;
                    case OpcodeBytes.RES_7_D:
                        Registers.D = ExecuteResetBit(7, Registers.D);
                        break;
                    case OpcodeBytes.RES_7_E:
                        Registers.E = ExecuteResetBit(7, Registers.E);
                        break;
                    case OpcodeBytes.RES_7_H:
                        Registers.H = ExecuteResetBit(7, Registers.H);
                        break;
                    case OpcodeBytes.RES_7_L:
                        Registers.L = ExecuteResetBit(7, Registers.L);
                        break;
                    case OpcodeBytes.RES_7_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteResetBit(7, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RES_7_A:
                        Registers.A = ExecuteResetBit(7, Registers.A);
                        break;

                #endregion

                #region Set Bit

                    case OpcodeBytes.SET_0_B:
                        Registers.B = ExecuteSetBit(0, Registers.B);
                        break;
                    case OpcodeBytes.SET_0_C:
                        Registers.C = ExecuteSetBit(0, Registers.C);
                        break;
                    case OpcodeBytes.SET_0_D:
                        Registers.D = ExecuteSetBit(0, Registers.D);
                        break;
                    case OpcodeBytes.SET_0_E:
                        Registers.E = ExecuteSetBit(0, Registers.E);
                        break;
                    case OpcodeBytes.SET_0_H:
                        Registers.H = ExecuteSetBit(0, Registers.H);
                        break;
                    case OpcodeBytes.SET_0_L:
                        Registers.L = ExecuteSetBit(0, Registers.L);
                        break;
                    case OpcodeBytes.SET_0_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteSetBit(0, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.SET_0_A:
                        Registers.A = ExecuteSetBit(0, Registers.A);
                        break;

                    case OpcodeBytes.SET_1_B:
                        Registers.B = ExecuteSetBit(1, Registers.B);
                        break;
                    case OpcodeBytes.SET_1_C:
                        Registers.C = ExecuteSetBit(1, Registers.C);
                        break;
                    case OpcodeBytes.SET_1_D:
                        Registers.D = ExecuteSetBit(1, Registers.D);
                        break;
                    case OpcodeBytes.SET_1_E:
                        Registers.E = ExecuteSetBit(1, Registers.E);
                        break;
                    case OpcodeBytes.SET_1_H:
                        Registers.H = ExecuteSetBit(1, Registers.H);
                        break;
                    case OpcodeBytes.SET_1_L:
                        Registers.L = ExecuteSetBit(1, Registers.L);
                        break;
                    case OpcodeBytes.SET_1_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteSetBit(1, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.SET_1_A:
                        Registers.A = ExecuteSetBit(1, Registers.A);
                        break;

                    case OpcodeBytes.SET_2_B:
                        Registers.B = ExecuteSetBit(2, Registers.B);
                        break;
                    case OpcodeBytes.SET_2_C:
                        Registers.C = ExecuteSetBit(2, Registers.C);
                        break;
                    case OpcodeBytes.SET_2_D:
                        Registers.D = ExecuteSetBit(2, Registers.D);
                        break;
                    case OpcodeBytes.SET_2_E:
                        Registers.E = ExecuteSetBit(2, Registers.E);
                        break;
                    case OpcodeBytes.SET_2_H:
                        Registers.H = ExecuteSetBit(2, Registers.H);
                        break;
                    case OpcodeBytes.SET_2_L:
                        Registers.L = ExecuteSetBit(2, Registers.L);
                        break;
                    case OpcodeBytes.SET_2_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteSetBit(2, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.SET_2_A:
                        Registers.A = ExecuteSetBit(2, Registers.A);
                        break;

                    case OpcodeBytes.SET_3_B:
                        Registers.B = ExecuteSetBit(3, Registers.B);
                        break;
                    case OpcodeBytes.SET_3_C:
                        Registers.C = ExecuteSetBit(3, Registers.C);
                        break;
                    case OpcodeBytes.SET_3_D:
                        Registers.D = ExecuteSetBit(3, Registers.D);
                        break;
                    case OpcodeBytes.SET_3_E:
                        Registers.E = ExecuteSetBit(3, Registers.E);
                        break;
                    case OpcodeBytes.SET_3_H:
                        Registers.H = ExecuteSetBit(3, Registers.H);
                        break;
                    case OpcodeBytes.SET_3_L:
                        Registers.L = ExecuteSetBit(3, Registers.L);
                        break;
                    case OpcodeBytes.SET_3_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteSetBit(3, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.SET_3_A:
                        Registers.A = ExecuteSetBit(3, Registers.A);
                        break;

                    case OpcodeBytes.SET_4_B:
                        Registers.B = ExecuteSetBit(4, Registers.B);
                        break;
                    case OpcodeBytes.SET_4_C:
                        Registers.C = ExecuteSetBit(4, Registers.C);
                        break;
                    case OpcodeBytes.SET_4_D:
                        Registers.D = ExecuteSetBit(4, Registers.D);
                        break;
                    case OpcodeBytes.SET_4_E:
                        Registers.E = ExecuteSetBit(4, Registers.E);
                        break;
                    case OpcodeBytes.SET_4_H:
                        Registers.H = ExecuteSetBit(4, Registers.H);
                        break;
                    case OpcodeBytes.SET_4_L:
                        Registers.L = ExecuteSetBit(4, Registers.L);
                        break;
                    case OpcodeBytes.SET_4_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteSetBit(4, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.SET_4_A:
                        Registers.A = ExecuteSetBit(4, Registers.A);
                        break;

                    case OpcodeBytes.SET_5_B:
                        Registers.B = ExecuteSetBit(5, Registers.B);
                        break;
                    case OpcodeBytes.SET_5_C:
                        Registers.C = ExecuteSetBit(5, Registers.C);
                        break;
                    case OpcodeBytes.SET_5_D:
                        Registers.D = ExecuteSetBit(5, Registers.D);
                        break;
                    case OpcodeBytes.SET_5_E:
                        Registers.E = ExecuteSetBit(5, Registers.E);
                        break;
                    case OpcodeBytes.SET_5_H:
                        Registers.H = ExecuteSetBit(5, Registers.H);
                        break;
                    case OpcodeBytes.SET_5_L:
                        Registers.L = ExecuteSetBit(5, Registers.L);
                        break;
                    case OpcodeBytes.SET_5_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteSetBit(5, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.SET_5_A:
                        Registers.A = ExecuteSetBit(5, Registers.A);
                        break;

                    case OpcodeBytes.SET_6_B:
                        Registers.B = ExecuteSetBit(6, Registers.B);
                        break;
                    case OpcodeBytes.SET_6_C:
                        Registers.C = ExecuteSetBit(6, Registers.C);
                        break;
                    case OpcodeBytes.SET_6_D:
                        Registers.D = ExecuteSetBit(6, Registers.D);
                        break;
                    case OpcodeBytes.SET_6_E:
                        Registers.E = ExecuteSetBit(6, Registers.E);
                        break;
                    case OpcodeBytes.SET_6_H:
                        Registers.H = ExecuteSetBit(6, Registers.H);
                        break;
                    case OpcodeBytes.SET_6_L:
                        Registers.L = ExecuteSetBit(6, Registers.L);
                        break;
                    case OpcodeBytes.SET_6_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteSetBit(6, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.SET_6_A:
                        Registers.A = ExecuteSetBit(6, Registers.A);
                        break;

                    case OpcodeBytes.SET_7_B:
                        Registers.B = ExecuteSetBit(7, Registers.B);
                        break;
                    case OpcodeBytes.SET_7_C:
                        Registers.C = ExecuteSetBit(7, Registers.C);
                        break;
                    case OpcodeBytes.SET_7_D:
                        Registers.D = ExecuteSetBit(7, Registers.D);
                        break;
                    case OpcodeBytes.SET_7_E:
                        Registers.E = ExecuteSetBit(7, Registers.E);
                        break;
                    case OpcodeBytes.SET_7_H:
                        Registers.H = ExecuteSetBit(7, Registers.H);
                        break;
                    case OpcodeBytes.SET_7_L:
                        Registers.L = ExecuteSetBit(7, Registers.L);
                        break;
                    case OpcodeBytes.SET_7_MHL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = ExecuteSetBit(7, value);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.SET_7_A:
                        Registers.A = ExecuteSetBit(7, Registers.A);
                        break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode.Code, Registers.PC));
            }
        }

        #region Additional Opcode Implementation Logic

        private byte ExecuteRotate(byte value, bool left, bool rotateThroughCarry = false, bool setAllFlags = true)
        {
            var previousHighOrderBitSet = (value & 0x80) == 0x80;
            var previousLowOrderBitSet = (value & 0x01) == 0x01;
            var previousCarryFlagSet = Flags.Carry;

            int result = value;

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

            Flags.Subtract = false;
            Flags.HalfCarry = false;

            if (setAllFlags)
            {
                Flags.Zero = result == 0;
                Flags.Sign = (result & 0b10000000) == 0b10000000;
                Flags.ParityOverflow = CalculateParityBit((byte)result);
            }

            // The standard RLA/RRA/RLCA/RRCA opcodes don't set the Z/S/P flags
            // but the bit opcodes RR/RL/RLC/RRC do set those flags. In both cases
            // the N and H flags are always reset.
            SetFlags(result: setAllFlags ? (byte?)result : (byte?)null, subtract: false, halfCarry: false);

            return (byte)result;
        }

        public byte ExecuteShiftArithmetic(byte value, bool left)
        {
            var previousHighOrderBitSet = (value & 0x80) == 0x80;
            var previousLowOrderBitSet = (value & 0x01) == 0x01;

            int result = value;

            if (left)
            {
                result = result << 1;
                Flags.Carry = previousHighOrderBitSet;
            }
            else
            {
                result = result >> 1;
                Flags.Carry = previousLowOrderBitSet;

                if (previousHighOrderBitSet)
                    result = result | 0x80;
            }

            SetFlags((byte)result, subtract: false, halfCarry: false);

            return (byte)result;
        }

        public byte ExecuteShiftLogical(byte value, bool left)
        {
            var previousHighOrderBitSet = (value & 0x80) == 0x80;
            var previousLowOrderBitSet = (value & 0x01) == 0x01;

            int result = value;

            if (left)
            {
                // Despite the SLL mnemonic, it's not really a "shift left logical" operation.
                // See: https://spectrumcomputing.co.uk/forums/viewtopic.php?p=9956&sid=f2072dd8da32b1a27a449433d387ebe6#p9956
                result = result << 1;
                Flags.Carry = previousHighOrderBitSet;
                result = result | 0x01; // Here's the wonky part; a one is put into bit zero.
            }
            else
            {
                result = result >> 1;
                Flags.Carry = previousLowOrderBitSet;
            }

            SetFlags((byte)result, subtract: false, halfCarry: false);

            return (byte)result;
        }

        public void ExecuteTestBit(int index, byte value)
        {
            if (index < 0 || index > 7)
                throw new Exception("Expected index to be 0-7.");

            int mask = 0x01 << index;

            Flags.Zero = (value & mask) == mask ? false : true;

            Flags.Subtract = false;
            Flags.HalfCarry = false;
        }

        public byte ExecuteResetBit(int index, byte value)
        {
            if (index < 0 || index > 7)
                throw new Exception("Expected index to be 0-7.");

            int mask = ~(0x01 << index);

            return (byte)(value & mask);
        }

        public byte ExecuteSetBit(int index, byte value)
        {
            if (index < 0 || index > 7)
                throw new Exception("Expected index to be 0-7.");

            int mask = 0x01 << index;

            return (byte)(value | mask);
        }

        #endregion
    }
}
