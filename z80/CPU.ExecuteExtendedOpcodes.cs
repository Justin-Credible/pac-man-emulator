using System;

namespace JustinCredible.ZilogZ80
{
    public partial class CPU
    {
        private void ExecuteExtendedOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            incrementProgramCounter = true;
            useAlternateCycleCount = false;

            switch (opcode.Code)
            {
                #region Input/Output Instructions

                case OpcodeBytes.OUT_MC_A:
                    OnDeviceWrite?.Invoke(Registers.C, Registers.A);
                    break;
                case OpcodeBytes.OUT_MC_B:
                    OnDeviceWrite?.Invoke(Registers.C, Registers.B);
                    break;
                case OpcodeBytes.OUT_MC_C:
                    OnDeviceWrite?.Invoke(Registers.C, Registers.C);
                    break;
                case OpcodeBytes.OUT_MC_D:
                    OnDeviceWrite?.Invoke(Registers.C, Registers.D);
                    break;
                case OpcodeBytes.OUT_MC_E:
                    OnDeviceWrite?.Invoke(Registers.C, Registers.E);
                    break;
                case OpcodeBytes.OUT_MC_H:
                    OnDeviceWrite?.Invoke(Registers.C, Registers.H);
                    break;
                case OpcodeBytes.OUT_MC_L:
                    OnDeviceWrite?.Invoke(Registers.C, Registers.L);
                    break;
                case OpcodeBytes.OUT_MC_0:
                    OnDeviceWrite?.Invoke(Registers.C, 0);
                    break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, ProgramCounter));
            }
        }

        private void ExecuteBitOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            // TODO
            throw new NotImplementedException();
        }

        private void ExecuteIXOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            // TODO
            throw new NotImplementedException();
        }

        private void ExecuteIYOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            // TODO
            throw new NotImplementedException();
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
    }
}
