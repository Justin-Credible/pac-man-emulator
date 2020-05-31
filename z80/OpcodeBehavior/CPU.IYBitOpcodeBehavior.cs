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
                #region RLC (IY+n), r - Rotate left

                    case OpcodeBytes.RLC_IY_B:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.B = Rotate(value, left: true);
                        break;
                    }
                    case OpcodeBytes.RLC_IY_C:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.C = Rotate(value, left: true);
                        break;
                    }
                    case OpcodeBytes.RLC_IY_D:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.D = Rotate(value, left: true);
                        break;
                    }
                    case OpcodeBytes.RLC_IY_E:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.E = Rotate(value, left: true);
                        break;
                    }
                    case OpcodeBytes.RLC_IY_H:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.H = Rotate(value, left: true);
                        break;
                    }
                    case OpcodeBytes.RLC_IY_L:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.L = Rotate(value, left: true);
                        break;
                    }
                    case OpcodeBytes.RLC_IY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        value = Rotate(value, left: true);
                        WriteMemory(Registers.IY + offset, value);
                        break;
                    }
                    case OpcodeBytes.RLC_IY_A:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.A = Rotate(value, left: true);
                        break;
                    }

                #endregion

                #region RRC (IY+n), r - Rotate right

                    case OpcodeBytes.RRC_IY_B:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.B = Rotate(value, left: false);
                        break;
                    }
                    case OpcodeBytes.RRC_IY_C:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.C = Rotate(value, left: false);
                        break;
                    }
                    case OpcodeBytes.RRC_IY_D:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.D = Rotate(value, left: false);
                        break;
                    }
                    case OpcodeBytes.RRC_IY_E:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.E = Rotate(value, left: false);
                        break;
                    }
                    case OpcodeBytes.RRC_IY_H:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.H = Rotate(value, left: false);
                        break;
                    }
                    case OpcodeBytes.RRC_IY_L:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.L = Rotate(value, left: false);
                        break;
                    }
                    case OpcodeBytes.RRC_IY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        value = Rotate(value, left: false);
                        WriteMemory(Registers.IY + offset, value);
                        break;
                    }
                    case OpcodeBytes.RRC_IY_A:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.A = Rotate(value, left: false);
                        break;
                    }

                #endregion

                #region RL (IY+n), r - Rotate left through carry

                    case OpcodeBytes.RL_IY_B:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.B = Rotate(value, left: true, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RL_IY_C:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.C = Rotate(value, left: true, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RL_IY_D:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.D = Rotate(value, left: true, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RL_IY_E:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.E = Rotate(value, left: true, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RL_IY_H:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.H = Rotate(value, left: true, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RL_IY_L:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.L = Rotate(value, left: true, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RL_IY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        value = Rotate(value, left: true, rotateThroughCarry: true);
                        WriteMemory(Registers.IY + offset, value);
                        break;
                    }
                    case OpcodeBytes.RL_IY_A:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.A = Rotate(value, left: true, rotateThroughCarry: true);
                        break;
                    }

                #endregion

                #region RR (IY+n), r - Rotate right through carry

                    case OpcodeBytes.RR_IY_B:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.B = Rotate(value, left: false, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RR_IY_C:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.C = Rotate(value, left: false, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RR_IY_D:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.D = Rotate(value, left: false, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RR_IY_E:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.E = Rotate(value, left: false, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RR_IY_H:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.H = Rotate(value, left: false, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RR_IY_L:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.L = Rotate(value, left: false, rotateThroughCarry: true);
                        break;
                    }
                    case OpcodeBytes.RR_IY:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        value = Rotate(value, left: false, rotateThroughCarry: true);
                        WriteMemory(Registers.IY + offset, value);
                        break;
                    }
                    case OpcodeBytes.RR_IY_A:
                    {
                        var offset = (sbyte)Memory[Registers.PC + 2];
                        var value = ReadMemory(Registers.IY + offset);
                        Registers.A = Rotate(value, left: false, rotateThroughCarry: true);
                        break;
                    }

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, Registers.PC));
            }
        }
    }
}
