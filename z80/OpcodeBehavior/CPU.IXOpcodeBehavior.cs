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

                    #region ADD IX, rr

                        case OpcodeBytes.ADD_IX_BC:
                            Registers.IX = ExecuteAdd(Registers.IX, Registers.BC);
                            break;
                        case OpcodeBytes.ADD_IX_DE:
                            Registers.IX = ExecuteAdd(Registers.IX, Registers.DE);
                            break;
                        case OpcodeBytes.ADD_IX_IX:
                            Registers.IX = ExecuteAdd(Registers.IX, Registers.IX);
                            break;
                        case OpcodeBytes.ADD_IX_SP:
                            Registers.IX = ExecuteAdd(Registers.IX, Registers.SP);
                            break;

                    #endregion

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, Registers.PC));
            }
        }
    }
}
