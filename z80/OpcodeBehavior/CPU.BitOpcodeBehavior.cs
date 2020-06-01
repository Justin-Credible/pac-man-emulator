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
                        case OpcodeBytes.RLC_HL:
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
                        case OpcodeBytes.RRC_HL:
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
                        case OpcodeBytes.RL_HL:
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
                        case OpcodeBytes.RR_HL:
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
                        case OpcodeBytes.SLA_HL:
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
                        case OpcodeBytes.SRA_HL:
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
                        case OpcodeBytes.SLL_HL:
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
                        case OpcodeBytes.SRL_HL:
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

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, Registers.PC));
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
            Flags.AuxCarry = false;

            if (setAllFlags)
            {
                Flags.Zero = result == 0;
                Flags.Sign = (result & 0b10000000) == 0b10000000;
                Flags.Parity = CalculateParityBit((byte)result);
            }

            // The standard RLA/RRA/RLCA/RRCA opcodes don't set the Z/S/P flags
            // but the bit opcodes RR/RL/RLC/RRC do set those flags. In both cases
            // the N and H flags are always reset.
            SetFlags(result: setAllFlags ? (byte?)result : (byte?)null, subtract: false, auxCarry: false);

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

            SetFlags((byte)result, subtract: false, auxCarry: false);

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

            SetFlags((byte)result, subtract: false, auxCarry: false);

            return (byte)result;
        }

        #endregion
    }
}
