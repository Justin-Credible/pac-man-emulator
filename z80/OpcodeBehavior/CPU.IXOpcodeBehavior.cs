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
                            Registers.IX = ExecuteAdd16(Registers.IX, Registers.BC);
                            break;
                        case OpcodeBytes.ADD_IX_DE:
                            Registers.IX = ExecuteAdd16(Registers.IX, Registers.DE);
                            break;
                        case OpcodeBytes.ADD_IX_IX:
                            Registers.IX = ExecuteAdd16(Registers.IX, Registers.IX);
                            break;
                        case OpcodeBytes.ADD_IX_SP:
                            Registers.IX = ExecuteAdd16(Registers.IX, Registers.SP);
                            break;

                    #endregion

                    #region ADD A, (IX+n)

                        case OpcodeBytes.ADD_A_IX:
                        {
                            var offset = (sbyte)Memory[Registers.PC + 2];
                            var value = ReadMemory(Registers.IX + offset);
                            Registers.A = ExecuteAdd(Registers.A, value);
                            break;
                        }

                    #endregion

                    #region #region ADD A, IX.hi/low

                        case OpcodeBytes.ADD_A_IXH:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IXH);
                            break;

                        case OpcodeBytes.ADD_A_IXL:
                            Registers.A = ExecuteAdd(Registers.A, Registers.IXL);
                            break;

                    #endregion

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, Registers.PC));
            }
        }
    }
}
