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

                    // r <- Device[C]
                    case OpcodeBytes.IN_A_MC:
                        Registers.A = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlagsFromDeviceReadOperation(Registers.A);
                        break;
                    case OpcodeBytes.IN_B_MC:
                        Registers.B = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlagsFromDeviceReadOperation(Registers.B);
                        break;
                    case OpcodeBytes.IN_C_MC:
                        Registers.C = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlagsFromDeviceReadOperation(Registers.C);
                        break;
                    case OpcodeBytes.IN_D_MC:
                        Registers.D = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlagsFromDeviceReadOperation(Registers.D);
                        break;
                    case OpcodeBytes.IN_E_MC:
                        Registers.E = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlagsFromDeviceReadOperation(Registers.E);
                        break;
                    case OpcodeBytes.IN_H_MC:
                        Registers.H = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlagsFromDeviceReadOperation(Registers.H);
                        break;
                    case OpcodeBytes.IN_L_MC:
                        Registers.L = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlagsFromDeviceReadOperation(Registers.L);
                        break;
                    case OpcodeBytes.IN_MC:
                        var value = OnDeviceRead?.Invoke(Registers.C) ?? 0;
                        SetFlagsFromDeviceReadOperation(value);
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
                    /* A - (HL); HL--; BC--; */
                    case OpcodeBytes.CPD:
                    /* A - (HL); HL--; BC--; if BC != 0 && !Z, repeat(); */
                    case OpcodeBytes.CPDR:
                    {
                        var memValue = ReadMemory(Registers.HL);

                        Execute8BitSubtraction(minuend: Registers.A, subtrahend: memValue, subtractCarryFlag: false, affectsCarryFlag: false);

                        // CPI/CPIR increments HL while CPD/CPDR decrements HL.
                        if (opcode.Code == OpcodeBytes.CPI || opcode.Code == OpcodeBytes.CPIR)
                            Registers.HL++;
                        else if (opcode.Code == OpcodeBytes.CPD || opcode.Code == OpcodeBytes.CPDR)
                            Registers.HL--;
                        else
                            throw new Exception($"Sanity check failed: unhandled case for opcode: {opcode.Instruction}");

                        Registers.BC--;

                        // Special handling of the P/V flag for these opcodes; see programmers manual.
                        Flags.ParityOverflow = Registers.BC != 0;

                        // The "repeat" opcode variants have the potential to repeat until a condition is met.
                        if (opcode.Code == OpcodeBytes.CPIR || opcode.Code == OpcodeBytes.CPDR)
                        {
                            // If either BC goes to zero OR the comparison was zero then we can bail out.
                            // Otherwise the instruction will continue to repeat until this condition is met.
                            if (Registers.BC == 0 || Flags.Zero)
                            {
                                // We can stop repeating this operation.
                                // When breaking out, we use the alternate cycle count.
                                incrementProgramCounter = true;
                                useAlternateCycleCount = true;
                            }
                            else
                            {
                                // We need to continue performing this operation until BC == 0 or Flags.Zero is set.
                                // We achieve this by leaving the program counter set to the current vaule.
                                // When repeating the operation we use the regular cycle count.
                                incrementProgramCounter = false;
                                useAlternateCycleCount = false;
                            }
                        }

                        break;
                    }

                #endregion

                #region Load

                    case OpcodeBytes.LD_I_A:
                        Registers.I = Registers.A;
                        break;

                    case OpcodeBytes.LD_A_I:
                        Registers.A = Registers.I;
                        break;

                    case OpcodeBytes.LD_R_A:
                        Registers.R = Registers.A;
                        break;

                    case OpcodeBytes.LD_A_R:
                        Registers.A = Registers.R;
                        break;

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

                        Flags.HalfCarry = false;
                        Flags.Subtract = false;

                        if (opcode.Code == OpcodeBytes.LDDR || opcode.Code == OpcodeBytes.LDIR)
                            Flags.ParityOverflow = false;
                        else
                            Flags.ParityOverflow = Registers.BC != 0;

                        break;
                    }

                    /* (NN) <- BC */
                    case OpcodeBytes.LD_MNN_BC:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        WriteMemory16(address, Registers.BC);
                        break;
                    }

                    /* (NN) <- DE */
                    case OpcodeBytes.LD_MNN_DE:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        WriteMemory16(address, Registers.DE);
                        break;
                    }

                    /* (NN) <- HL */
                    case OpcodeBytes.LD_MNN_HL_2:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        WriteMemory16(address, Registers.HL);
                        break;
                    }

                    /* (NN) <- SP */
                    case OpcodeBytes.LD_MNN_SP:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        WriteMemory16(address, Registers.SP);
                        break;
                    }

                    /* BC <- (NN) */
                    case OpcodeBytes.LD_BC_MNN:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        Registers.BC = ReadMemory16(address);
                        break;
                    }

                    /* DE <- (NN) */
                    case OpcodeBytes.LD_DE_MNN:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        Registers.DE = ReadMemory16(address);
                        break;
                    }

                    /* HL <- (NN) */
                    case OpcodeBytes.LD_HL_MNN_2:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        Registers.HL = ReadMemory16(address);
                        break;
                    }

                    /* SP <- (NN) */
                    case OpcodeBytes.LD_SP_MNN:
                    {
                        var address = ReadMemory16(Registers.PC + 2);
                        Registers.SP = ReadMemory16(address);
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
                        ExecuteReturn();

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
                        ExecuteReturn();

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
                        SetFlags(Registers.A, halfCarry: false, subtract: false);
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
                        SetFlags(Registers.A, halfCarry: false, subtract: false);
                        break;
                    }

                #endregion

                #region ADC HL, rr - Add register or memory to HL with carry

                    case OpcodeBytes.ADC_HL_BC:
                        Registers.HL = Execute16BitAddition(Registers.HL, Registers.BC, addCarryFlag: true);
                        break;
                    case OpcodeBytes.ADC_HL_DE:
                        Registers.HL = Execute16BitAddition(Registers.HL, Registers.DE, addCarryFlag: true);
                        break;
                    case OpcodeBytes.ADC_HL_HL:
                        Registers.HL = Execute16BitAddition(Registers.HL, Registers.HL, addCarryFlag: true);
                        break;
                    case OpcodeBytes.ADC_HL_SP:
                        Registers.HL = Execute16BitAddition(Registers.HL, Registers.SP, addCarryFlag: true);
                        break;

                #endregion

                #region SBC HL, rr - Subtract register or memory from HL with borrow

                    case OpcodeBytes.SBC_HL_BC:
                        Registers.HL = Execute16BitSubtraction(Registers.HL, Registers.BC, subtractCarryFlag: true);
                        break;
                    case OpcodeBytes.SBC_HL_DE:
                        Registers.HL = Execute16BitSubtraction(Registers.HL, Registers.DE, subtractCarryFlag: true);
                        break;
                    case OpcodeBytes.SBC_HL_HL:
                        Registers.HL = Execute16BitSubtraction(Registers.HL, Registers.HL, subtractCarryFlag: true);
                        break;
                    case OpcodeBytes.SBC_HL_SP:
                        Registers.HL = Execute16BitSubtraction(Registers.HL, Registers.SP, subtractCarryFlag: true);
                        break;

                #endregion

                #region Negate Accumulator

                    case OpcodeBytes.NEG:
                    case OpcodeBytes.NEG_2:
                    case OpcodeBytes.NEG_3:
                    case OpcodeBytes.NEG_4:
                    case OpcodeBytes.NEG_5:
                    case OpcodeBytes.NEG_6:
                    case OpcodeBytes.NEG_7:
                    case OpcodeBytes.NEG_8:
                    {
                        Registers.A = Execute8BitSubtraction(0, Registers.A, false);
                        break;
                    }

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{1:X4}", opcode.Code, Registers.PC));
            }
        }

        #region Utilities

        /**
         * A helper method used to encapsulate the logic for the setting of the condition flags during
         * an 8-bit device read operation. This method sets all six of the condition flags based on
         * the following:
         * 
         * • Zero (Z) is set if result is 0; otherwise, it is reset.
         * • Sign (S) is set if result is negative; otherwise, it is reset.
         * • Half Carry (H) is reset.
         * • Parity (P/V) is set if parity even; otherwise, it is reset.
         * • Subract (N) is reset.
         * • Carry (C) is not affected.
         */
        private void SetFlagsFromDeviceReadOperation(byte result)
        {
            Flags.Sign = (0x80 & result) == 0x80;
            Flags.Zero = result == 0;
            Flags.ParityOverflow = CalculateParityBit(result);

            // These two are always reset.
            Flags.Subtract = false;
            Flags.HalfCarry = false;
        }

        #endregion
    }
}
