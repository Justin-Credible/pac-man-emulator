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
                #region RLC, r - Rotate left

                    case OpcodeBytes.RLC_B:
                        Registers.B = Rotate(Registers.B, left: true);
                        break;
                    case OpcodeBytes.RLC_C:
                        Registers.C = Rotate(Registers.C, left: true);
                        break;
                    case OpcodeBytes.RLC_D:
                        Registers.D = Rotate(Registers.D, left: true);
                        break;
                    case OpcodeBytes.RLC_E:
                        Registers.E = Rotate(Registers.E, left: true);
                        break;
                    case OpcodeBytes.RLC_H:
                        Registers.H = Rotate(Registers.H, left: true);
                        break;
                    case OpcodeBytes.RLC_L:
                        Registers.L = Rotate(Registers.L, left: true);
                        break;
                    case OpcodeBytes.RLC_HL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = Rotate(value, left: true);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RLC_A:
                        Registers.A = Rotate(Registers.A, left: true);
                        break;

                #endregion

                #region RRC, r - Rotate right

                    case OpcodeBytes.RRC_B:
                        Registers.B = Rotate(Registers.B, left: false);
                        break;
                    case OpcodeBytes.RRC_C:
                        Registers.C = Rotate(Registers.C, left: false);
                        break;
                    case OpcodeBytes.RRC_D:
                        Registers.D = Rotate(Registers.D, left: false);
                        break;
                    case OpcodeBytes.RRC_E:
                        Registers.E = Rotate(Registers.E, left: false);
                        break;
                    case OpcodeBytes.RRC_H:
                        Registers.H = Rotate(Registers.H, left: false);
                        break;
                    case OpcodeBytes.RRC_L:
                        Registers.L = Rotate(Registers.L, left: false);
                        break;
                    case OpcodeBytes.RRC_HL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = Rotate(value, left: false);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RRC_A:
                        Registers.A = Rotate(Registers.A, left: false);
                        break;

                #endregion

                #region RL, r - Rotate left through carry

                    case OpcodeBytes.RL_B:
                        Registers.B = Rotate(Registers.B, left: true, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RL_C:
                        Registers.C = Rotate(Registers.C, left: true, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RL_D:
                        Registers.D = Rotate(Registers.D, left: true, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RL_E:
                        Registers.E = Rotate(Registers.E, left: true, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RL_H:
                        Registers.H = Rotate(Registers.H, left: true, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RL_L:
                        Registers.L = Rotate(Registers.L, left: true, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RL_HL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = Rotate(value, left: true, rotateThroughCarry: true);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RL_A:
                        Registers.A = Rotate(Registers.A, left: true, rotateThroughCarry: true);
                        break;

                #endregion

                #region RR, r - Rotate right through carry

                    case OpcodeBytes.RR_B:
                        Registers.B = Rotate(Registers.B, left: false, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RR_C:
                        Registers.C = Rotate(Registers.C, left: false, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RR_D:
                        Registers.D = Rotate(Registers.D, left: false, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RR_E:
                        Registers.E = Rotate(Registers.E, left: false, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RR_H:
                        Registers.H = Rotate(Registers.H, left: false, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RR_L:
                        Registers.L = Rotate(Registers.L, left: false, rotateThroughCarry: true);
                        break;
                    case OpcodeBytes.RR_HL:
                    {
                        var value = ReadMemory(Registers.HL);
                        value = Rotate(value, left: false, rotateThroughCarry: true);
                        WriteMemory(Registers.HL, value);
                        break;
                    }
                    case OpcodeBytes.RR_A:
                        Registers.A = Rotate(Registers.A, left: false, rotateThroughCarry: true);
                        break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, Registers.PC));
            }
        }

        private void ExecuteIXBitOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            // TODO
            throw new NotImplementedException();
        }

        private void ExecuteIYBitOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            // TODO
            throw new NotImplementedException();
        }

        #region Additional Opcode Implementation Logic

        private byte Rotate(byte value, bool left, bool rotateThroughCarry = false, bool setAllFlags = true)
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

        #endregion
    }
}
