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

                    // Device[C] <- R
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

                    // Device[C] <- (HL); HL++; B--;
                    case OpcodeBytes.OUTI:
                        OnDeviceWrite?.Invoke(Registers.C, ReadMemory(Registers.HL));
                        Registers.HL++;
                        Registers.B--;
                        Flags.Zero = Registers.B == 0;
                        Flags.Subtract = true;
                        break;

                    // Device[C] <- (HL); HL--; B--;
                    case OpcodeBytes.OUTD:
                        OnDeviceWrite?.Invoke(Registers.C, ReadMemory(Registers.HL));
                        Registers.HL--;
                        Registers.B--;
                        Flags.Zero = Registers.B == 0;
                        Flags.Subtract = true;
                        break;

                    // R <- Device[C]
                    case OpcodeBytes.IN_A_MC:
                        Registers.A = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlags(Registers.A, subtract: false, auxCarry: false);
                        break;
                    case OpcodeBytes.IN_B_MC:
                        Registers.B = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlags(Registers.B, subtract: false, auxCarry: false);
                        break;
                    case OpcodeBytes.IN_C_MC:
                        Registers.C = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlags(Registers.C, subtract: false, auxCarry: false);
                        break;
                    case OpcodeBytes.IN_D_MC:
                        Registers.D = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlags(Registers.D, subtract: false, auxCarry: false);
                        break;
                    case OpcodeBytes.IN_E_MC:
                        Registers.E = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlags(Registers.E, subtract: false, auxCarry: false);
                        break;
                    case OpcodeBytes.IN_H_MC:
                        Registers.H = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlags(Registers.H, subtract: false, auxCarry: false);
                        break;
                    case OpcodeBytes.IN_L_MC:
                        Registers.L = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlags(Registers.L, subtract: false, auxCarry: false);
                        break;
                    case OpcodeBytes.IN_MC:
                        var value = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlags(value, subtract: false, auxCarry: false);
                        break;

                    // (HL) <- Device[C]; HL++; B--;
                    case OpcodeBytes.INI:
                        WriteMemory(Registers.HL, OnDeviceRead?.Invoke(Registers.C) ?? 0);
                        Registers.HL++;
                        Registers.B--;
                        Flags.Zero = Registers.B == 0;
                        Flags.Subtract = true;
                        break;

                    // (HL) <- Device[C]; HL--; B--;
                    case OpcodeBytes.IND:
                        WriteMemory(Registers.HL, OnDeviceRead?.Invoke(Registers.C) ?? 0);
                        Registers.HL--;
                        Registers.B--;
                        Flags.Zero = Registers.B == 0;
                        Flags.Subtract = true;
                        break;

                #endregion

                #region Set Interrupt Mode

                    case OpcodeBytes.IM0:
                    case OpcodeBytes.IM0_2:
                    case OpcodeBytes.IM0_3:
                    case OpcodeBytes.IM0_4:
                        InterruptMode = InterruptMode.Zero;
                        break;

                    case OpcodeBytes.IM1:
                    case OpcodeBytes.IM1_2:
                        InterruptMode = InterruptMode.One;
                        break;

                    case OpcodeBytes.IM2:
                    case OpcodeBytes.IM2_2:
                        InterruptMode = InterruptMode.Two;
                        break;

                #endregion

                #region Return from Non-maskable Interrupt

                    case OpcodeBytes.RETN:
                    case OpcodeBytes.RETN_2:
                    case OpcodeBytes.RETN_3:
                    case OpcodeBytes.RETN_4:
                    case OpcodeBytes.RETN_5:
                    case OpcodeBytes.RETN_6:
                    case OpcodeBytes.RETN_7:
                    {
                        ExecuteRET();

                        // Copy IFF2 back to IFF1; this is the only difference between RET and RETN.
                        InterruptsEnabled = InterruptsEnabledPreviousValue;

                        // Don't increment the program counter because we just updated it to
                        // the given address.
                        incrementProgramCounter = false;

                        break;
                    }

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
