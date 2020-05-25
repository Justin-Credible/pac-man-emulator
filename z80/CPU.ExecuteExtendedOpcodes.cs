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
                    // Device[C] <- (HL); HL++; B--; if B != 0, repeat(); 
                    case OpcodeBytes.OTIR:
                    {
                        OnDeviceWrite?.Invoke(Registers.C, ReadMemory(Registers.HL));
                        Registers.HL++;
                        Registers.B--;
                        Flags.Zero = Registers.B == 0;
                        Flags.Subtract = true;

                        if (opcode.Code == OpcodeBytes.OTIR)
                        {
                            if (Registers.B == 0)
                                useAlternateCycleCount = true;
                            else
                                incrementProgramCounter = false; // repeat operation
                        }

                        break;
                    }

                    // Device[C] <- (HL); HL--; B--;
                    case OpcodeBytes.OUTD:
                    // Device[C] <- (HL); HL--; B--; if B != 0, repeat(); 
                    case OpcodeBytes.OTDR:
                    {
                        OnDeviceWrite?.Invoke(Registers.C, ReadMemory(Registers.HL));
                        Registers.HL--;
                        Registers.B--;
                        Flags.Zero = Registers.B == 0;
                        Flags.Subtract = true;

                        if (opcode.Code == OpcodeBytes.OTDR)
                        {
                            if (Registers.B == 0)
                                useAlternateCycleCount = true;
                            else
                                incrementProgramCounter = false; // repeat operation
                        }

                        break;
                    }

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
                    // (HL) <- Device[C]; HL++; B--; if B != 0, repeat();
                    case OpcodeBytes.INIR:
                    {
                        WriteMemory(Registers.HL, OnDeviceRead?.Invoke(Registers.C) ?? 0);
                        Registers.HL++;
                        Registers.B--;
                        Flags.Zero = Registers.B == 0;
                        Flags.Subtract = true;

                        if (opcode.Code == OpcodeBytes.INIR)
                        {
                            if (Registers.B == 0)
                                useAlternateCycleCount = true;
                            else
                                incrementProgramCounter = false; // repeat operation
                        }

                        break;
                    }

                    // (HL) <- Device[C]; HL--; B--;
                    case OpcodeBytes.IND:
                    // (HL) <- Device[C]; HL--; B--; if B != 0, repeat();
                    case OpcodeBytes.INDR:
                    {
                        WriteMemory(Registers.HL, OnDeviceRead?.Invoke(Registers.C) ?? 0);
                        Registers.HL--;
                        Registers.B--;
                        Flags.Zero = Registers.B == 0;
                        Flags.Subtract = true;

                        if (opcode.Code == OpcodeBytes.INDR)
                        {
                            if (Registers.B == 0)
                                useAlternateCycleCount = true;
                            else
                                incrementProgramCounter = false; // repeat operation
                        }

                        break;
                    }

                #endregion

                #region Compare

                    /* A - (HL); HL++; BC--; */
                    case OpcodeBytes.CPI:
                    /* A - (HL); HL++; BC--; if BC != 0 && !Z, repeat(); */
                    case OpcodeBytes.CPIR:
                    {
                        var memValue = ReadMemory(Registers.HL);

                        var borrowOccurred = memValue > Registers.A;

                        var result = Registers.A - memValue;

                        if (borrowOccurred)
                            result = 256 + result;

                        SetFlags(result: (byte)result, subtract: true);

                        Registers.HL++;
                        Registers.BC--;

                        if (opcode.Code == OpcodeBytes.CPIR)
                        {
                            if (Registers.BC != 0 && !Flags.Zero)
                                incrementProgramCounter = false; // repeat operation
                            else
                                useAlternateCycleCount = true;
                        }

                        break;
                    }

                    /* A - (HL); HL--; BC--; */
                    case OpcodeBytes.CPD:
                    /* A - (HL); HL--; BC--; if BC != 0 && !Z, repeat(); */
                    case OpcodeBytes.CPDR:
                    {
                        var memValue = ReadMemory(Registers.HL);

                        var borrowOccurred = memValue > Registers.A;

                        var result = Registers.A - memValue;

                        if (borrowOccurred)
                            result = 256 + result;

                        SetFlags(result: (byte)result, subtract: true);

                        Registers.HL--;
                        Registers.BC--;

                        if (opcode.Code == OpcodeBytes.CPDR)
                        {
                            if (Registers.BC != 0 && !Flags.Zero)
                                incrementProgramCounter = false; // repeat operation
                            else
                                useAlternateCycleCount = true;
                        }

                        break;
                    }


                #endregion

                #region Load

                    /* (DE) <- (HL); HL++; DE++; BC--; */
                    case OpcodeBytes.LDI:
                    /* (DE) <- (HL); HL++; DE++; BC--; if BC !=0, repeat(); */
                    case OpcodeBytes.LDIR:
                    /* (DE) <- (HL); HL--; DE--; BC--; */
                    case OpcodeBytes.LDD:
                    /* (DE) <- (HL); HL--; DE--; BC--;  if BC !=0, repeat(); */
                    case OpcodeBytes.LDDR:
                    {
                        WriteMemory(Registers.DE, ReadMemory(Registers.HL));
                        Registers.BC--;

                        if (opcode.Code == OpcodeBytes.LDI || opcode.Code == OpcodeBytes.LDIR)
                        {
                            Registers.HL++;
                            Registers.DE++;
                        }
                        else if (opcode.Code == OpcodeBytes.LDD || opcode.Code == OpcodeBytes.LDDR)
                        {
                            Registers.HL--;
                            Registers.DE--;
                        }

                        if (opcode.Code == OpcodeBytes.LDIR || opcode.Code == OpcodeBytes.LDDR)
                        {
                            if (Registers.BC != 0)
                                incrementProgramCounter = false; // repeat operation
                            else
                                useAlternateCycleCount = true;
                        }

                        SetFlags(null, subtract: false, auxCarry: false);

                        if (opcode.Code == OpcodeBytes.LDDR)
                            Flags.Parity = false;
                        else
                            Flags.Parity = Registers.BC - 1 != 0 ? true : false;

                        break;
                    }

                    /* (NN) <- BC */
                    case OpcodeBytes.LD_MNN_BC:
                    {
                        var address = ReadMemory16(ProgramCounter + 2);
                        WriteMemory16(address, Registers.BC);
                        break;
                    }

                    /* (NN) <- DE */
                    case OpcodeBytes.LD_MNN_DE:
                    {
                        var address = ReadMemory16(ProgramCounter + 2);
                        WriteMemory16(address, Registers.DE);
                        break;
                    }

                    /* (NN) <- HL */
                    case OpcodeBytes.LD_MNN_HL_2:
                    {
                        var address = ReadMemory16(ProgramCounter + 2);
                        WriteMemory16(address, Registers.HL);
                        break;
                    }

                    /* (NN) <- SP */
                    case OpcodeBytes.LD_MNN_SP:
                    {
                        var address = ReadMemory16(ProgramCounter + 2);
                        WriteMemory16(address, StackPointer);
                        break;
                    }

                    /* BC <- (NN) */
                    case OpcodeBytes.LD_BC_MNN:
                    {
                        var address = ReadMemory16(ProgramCounter + 2);
                        Registers.BC = ReadMemory16(address);
                        break;
                    }

                    /* DE <- (NN) */
                    case OpcodeBytes.LD_DE_MNN:
                    {
                        var address = ReadMemory16(ProgramCounter + 2);
                        Registers.DE = ReadMemory16(address);
                        break;
                    }

                    /* HL <- (NN) */
                    case OpcodeBytes.LD_HL_MNN_2:
                    {
                        var address = ReadMemory16(ProgramCounter + 2);
                        Registers.HL = ReadMemory16(address);
                        break;
                    }

                    /* SP <- (NN) */
                    case OpcodeBytes.LD_SP_MNN:
                    {
                        var address = ReadMemory16(ProgramCounter + 2);
                        StackPointer = ReadMemory16(address);
                        break;
                    }

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

                #region Return from Interrupt

                    case OpcodeBytes.RETI:
                    {
                        ExecuteRET();

                        // TODO: Support daisy-chainable interrupts; signal here to allow the
                        // next interrupt to occur (if any).

                        // Don't increment the program counter because we just updated it to
                        // the given address.
                        incrementProgramCounter = false;

                        break;
                    }

                #endregion

                #region Shift and Rotate

                    case OpcodeBytes.RRD:
                    {
                        // tmp <- (HL).lo; (HL).lo <- (HL).hi; (HL).hi <- A.lo; A.lo <- tmp;
                        var memValue = ReadMemory(Registers.HL); // (HL)
                        var tmp = memValue & 0x0F; // tmp <- (HL).lo
                        memValue = (byte)(((memValue & 0xF0) >> 4) | (memValue & 0xF0)); // (HL).lo <- (HL).hi
                        memValue = (byte)(((Registers.A & 0x0F) << 4) | (memValue & 0x0F)); // (HL).hi <- A.lo
                        Registers.A = (byte)((Registers.A & 0xF0) | tmp); //  A.lo <- tmp
                        WriteMemory(Registers.HL, memValue);
                        SetFlags(Registers.A, auxCarry: false, subtract: false);
                        break;
                    }

                    case OpcodeBytes.RLD:
                    {
                        // tmp <- (HL).lo; (HL).lo <- A.lo; A.lo <- (HL).hi; (HL).hi <- tmp;
                        var memValue = ReadMemory(Registers.HL); // (HL)
                        var tmp = memValue & 0x0F; // tmp <- (HL).lo
                        memValue = (byte)((Registers.A & 0x0F) | (memValue & 0xF0)); // (HL).lo <- A.lo
                        Registers.A = (byte)(((memValue & 0xF0) >> 4) | (Registers.A & 0xF0)); // A.lo <- (HL).hi
                        memValue = (byte)((tmp << 4) | (memValue & 0x0F)); // (HL).hi <- tmp
                        WriteMemory(Registers.HL, memValue);
                        SetFlags(Registers.A, auxCarry: false, subtract: false);
                        break;
                    }

                #endregion

                #region ADC HL, rr - Add register or memory to HL with carry

                    case OpcodeBytes.ADC_HL_BC:
                        Execute_ADC_HL(Registers.BC);
                        break;
                    case OpcodeBytes.ADC_HL_DE:
                        Execute_ADC_HL(Registers.DE);
                        break;
                    case OpcodeBytes.ADC_HL_HL:
                        Execute_ADC_HL(Registers.HL);
                        break;
                    case OpcodeBytes.ADC_HL_SP:
                        Execute_ADC_HL(StackPointer);
                        break;

                #endregion

                #region SBC HL, rr - Subtract register or memory from HL with borrow

                    case OpcodeBytes.SBC_HL_BC:
                        Execute_SBC_HL(Registers.BC);
                        break;
                    case OpcodeBytes.SBC_HL_DE:
                        Execute_SBC_HL(Registers.DE);
                        break;
                    case OpcodeBytes.SBC_HL_HL:
                        Execute_SBC_HL(Registers.HL);
                        break;
                    case OpcodeBytes.SBC_HL_SP:
                        Execute_SBC_HL(StackPointer);
                        break;

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode, ProgramCounter));
            }
        }

        #region Opcodes - Additional Implementations

        private void Execute_ADC_HL(UInt16 value)
        {
            var result = Registers.HL + value;

            if (Flags.Carry)
                result += 1;

            var carryOccurred = result > 65535;

            if (carryOccurred)
                result = result - 65536;

            SetFlags(carry: carryOccurred, result: (UInt16)result, subtract: false);

            Registers.HL = (UInt16)result;
        }

        private void Execute_SBC_HL(UInt16 value)
        {
            var borrowOccurred = Flags.Carry
                ? value >= Registers.HL // Account for the extra minus one from the carry flag subtraction.
                : value > Registers.HL;

            var result = Registers.HL - value;

            if (Flags.Carry)
                result -= 1;

            if (borrowOccurred)
                result = 65536 + result;

            SetFlags(carry: borrowOccurred, result: (byte)result, subtract: true);

            Registers.HL = (UInt16)result;
        }

        #endregion

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
