using System;

namespace JustinCredible.ZilogZ80
{
    /**
     * An emulated version of the Zilog Z80 CPU.
     */
    public partial class CPU
    {
        /**
         * Indicates the ROM has finished executing via a HALT opcode.
         * Step should not be called again without first calling Reset.
         */
        public bool Finished { get; private set; }

        /** The addressable memory implementation; can include RAM, ROM, memory mapped devices, etc. */
        public IMemory Memory { get; set; }

        /** The CPU registers (A B C D E H L I R IX IY PC SP) */
        public CPURegisters Registers { get; set; }

        /** The encapsulated condition/flags register (F) */
        public ConditionFlags Flags { get; set; }

        /** Indicates if interrupts are enabled or not: IFF1. */
        public bool InterruptsEnabled { get; set; }

        /** The previous value of the interrupts enabled flag (IFF1) when a non-maskable interrupt is used: IFF2. */
        public bool InterruptsEnabledPreviousValue { get; set; }

        /** The mode of interrupt the CPU is currently using. */
        public InterruptMode InterruptMode { get; set; }

        /** Configuration for the CPU; used to customize the CPU instance. */
        public CPUConfig Config { get; private set; }

        public delegate void CPUDiagDebugEvent(int eventID);

        /** Fired on CALL 0x05 when EnableCPUDiagMode is true. */
        public event CPUDiagDebugEvent OnCPUDiagDebugEvent;

        /**
         * Event handler for handling CPU writes to devices.
         * 
         * Indicates the ID of the device to write the given data to.
         */
        public delegate void DeviceWriteEvent(int deviceID, byte data);

        /** Fired when the OUT instruction is encountered. */
        public event DeviceWriteEvent OnDeviceWrite;

        /**
         * Event handler for handling CPU reads from devices.
         * 
         * Indicates the ID of the device to read from and should return
         * the data for that device.
         */
        public delegate byte DeviceReadEvent(int deviceID);

        /** Fired when the IN instruction is encountered. */
        public event DeviceReadEvent OnDeviceRead;

        #region Initialization

        public CPU(CPUConfig config)
        {
            Config = config;
            this.Reset();
        }

        public void Reset()
        {
            // Re-initialize the CPU based on configuration.
            Memory = Config.Memory;
            Registers = Config.Registers ?? new CPURegisters();
            Flags = Config.Flags ?? new ConditionFlags();
            InterruptsEnabled = Config.InterruptsEnabled;
            InterruptsEnabledPreviousValue = Config.InterruptsEnabledPreviousValue;
            InterruptMode = Config.InterruptMode;

            // Reset the flag that indicates that the ROM has finished executing.
            Finished = false;
        }

        #endregion

        #region Debugging

        public void PrintDebugSummary()
        {
            var opcodeByte = Memory.Read(Registers.PC);
            Opcode opcode = null;

            try
            {
                opcode = Opcodes.GetOpcode(Registers.PC, Memory);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetOpcode error: " + ex.Message);
            }

            var instruction = opcode == null ? "???" : opcode.Instruction;

            var opcodeFormatted = String.Format("0x{0:X2} {1}", opcodeByte, instruction);
            var pc = String.Format("0x{0:X4}", Registers.PC);
            var sp = String.Format("0x{0:X4}", Registers.SP);
            var regA = String.Format("0x{0:X2}", Registers.A);
            var regB = String.Format("0x{0:X2}", Registers.B);
            var regC = String.Format("0x{0:X2}", Registers.C);
            var regD = String.Format("0x{0:X2}", Registers.D);
            var regE = String.Format("0x{0:X2}", Registers.E);
            var regH = String.Format("0x{0:X2}", Registers.H);
            var regL = String.Format("0x{0:X2}", Registers.L);

            var valueAtDE = String.Format("0x{0:X2}", Memory.Read(Registers.DE));
            var valueAtHL = String.Format("0x{0:X2}", Memory.Read(Registers.HL));

            Console.WriteLine($"Opcode: {opcodeFormatted}");
            Console.WriteLine();
            Console.WriteLine($"PC: {pc}\tSP: {sp}");
            Console.WriteLine();
            Console.WriteLine($"A: {regA}\t\tB: {regB}\t\tC: {regC}");
            Console.WriteLine($"D: {regD}\t\tE: {regE}\t\tH: {regH}\t\tL: {regL}");
            Console.WriteLine($"(DE): {valueAtHL}\t\t\t(HL): {valueAtHL}");
            Console.WriteLine();
            Console.WriteLine($"Sign: {Flags.Sign}\tZero: {Flags.Zero}\tSign: {Flags.Sign}\tParity: {Flags.ParityOverflow}\tCarry: {Flags.Carry}\tHalf Carry: {Flags.HalfCarry}");
        }

        #endregion

        #region Step / Execute Opcode

        /** Used to signal a non-maskable interrupt and call to 0x0066 */
        public int StepNonMaskableInterrupt()
        {
            // Save the previous state of the flag and then disable interrupts.
            InterruptsEnabledPreviousValue = InterruptsEnabled;
            InterruptsEnabled = false;

            ExecuteCall(0x0066, Registers.PC);

            // TODO: Guessing here for cycle count.
            return Opcodes.CALL.Cycles;
        }

        /** Used to signal an interrupt and execute interrupt behavior based on the current interrupt mode. */
        public int StepMaskableInterrupt(byte dataBusValue = 0x00)
        {
            // TODO: How to support daisy-chained peripheral interrupt scheme?

            if (!InterruptsEnabled)
                return 0;

            switch (InterruptMode)
            {
                case InterruptMode.Zero:
                {
                    switch (dataBusValue)
                    {
                        case OpcodeBytes.RST_00:
                            ExecuteCall(0x000, Registers.PC);
                            return Opcodes.RST_00.Cycles;
                        case OpcodeBytes.RST_08:
                            ExecuteCall(0x0008, Registers.PC);
                            return Opcodes.RST_08.Cycles;
                        case OpcodeBytes.RST_10:
                            ExecuteCall(0x0010, Registers.PC);
                            return Opcodes.RST_10.Cycles;
                        case OpcodeBytes.RST_18:
                            ExecuteCall(0x0018, Registers.PC);
                            return Opcodes.RST_18.Cycles;
                        case OpcodeBytes.RST_20:
                            ExecuteCall(0x0020, Registers.PC);
                            return Opcodes.RST_20.Cycles;
                        case OpcodeBytes.RST_28:
                            ExecuteCall(0x0028, Registers.PC);
                            return Opcodes.RST_28.Cycles;
                        case OpcodeBytes.RST_30:
                            ExecuteCall(0x0030, Registers.PC);
                            return Opcodes.RST_30.Cycles;
                        case OpcodeBytes.RST_38:
                            ExecuteCall(0x0038, Registers.PC);
                            return Opcodes.RST_38.Cycles;
                        default:
                            // TODO: Implement accepting arbitrary opcode instruction bytes as per:
                            // http://www.z80.info/zip/z80-interrupts_rewritten.pdf
                            //   The instruction is normally a Restart (RST) instruction since this is an efficient one byte call to
                            //   any one of eight subroutines located in the first 64 bytes of memory. (Each subroutine is 8
                            //    byte long.) However, any instruction may be given to the Z80­CPU.
                            throw new NotImplementedException(String.Format("Only RST 0 - RST 7 instructions for interrupt mode 0 are currently supported; value given: {0:X2}", dataBusValue));
                    }
                }

                case InterruptMode.One:
                {
                    ExecuteCall(0x0038, Registers.PC);
                    return Opcodes.RST_38.Cycles; // TODO: Seems the same as a RST 38h... same cycle count as well?
                }

                case InterruptMode.Two:
                {
                    // The MSB bits are from the interrupt vector while the LSB are from the data bus.
                    var address = (Registers.I << 8) | dataBusValue;
                    ExecuteCall((UInt16)address, Registers.PC);

                    // TODO: Guessing here for cycle count.
                    return Opcodes.CALL.Cycles;
                }

                default:
                    throw new NotImplementedException($"The given interrupt mode {InterruptMode} is not implemented.");
            }
        }

        /** Executes the next instruction and returns the number of cycles it took to execute. */
        public int Step()
        {
            // Sanity check.
            if (Finished)
                throw new Exception("Program has finished execution; Reset() must be invoked before invoking Step() again.");

            // Fetch the next opcode to be executed, as indicated by the program counter.
            var opcode = Opcodes.GetOpcode(Registers.PC, Memory);

            // Sanity check: unimplemented opcode?
            if (opcode == null)
                throw new Exception(String.Format("Unable to fetch opcode structure for byte 0x{0:X2} at memory address 0x{1:X4}.", Memory.Read(Registers.PC), Registers.PC));

            // Indicates if we should increment the program counter after executing the instruction.
            // This is almost always the case, but there are a few cases where we don't want to.
            var incrementProgramCounter = true;

            // Some instructions have an alternate cycle count depending on the outcome of the
            // operation. This indicates if we should count the regular or alternate cycle count
            // when returning the number of cycles that the instruction took to execute.
            var useAlternateCycleCount = false;

            ExecuteOpcode(opcode, out incrementProgramCounter, out useAlternateCycleCount);

            // Determine how many cycles the instruction took.

            var elapsedCycles = opcode.Cycles;

            if (useAlternateCycleCount)
            {
                // Sanity check; if this fails an opcode definition or implementation is invalid.
                if (opcode.AlternateCycles == null)
                    throw new Exception(String.Format("The implementation for opcode 0x{0:X2} at memory address 0x{1:X4} indicated the alternate number of cycles should be used, but was not defined.", opcode.Code, Registers.PC));

                elapsedCycles = opcode.AlternateCycles.Value;
            }

            // Increment the program counter.
            if (incrementProgramCounter)
               Registers.PC += (UInt16)opcode.Size;

            return elapsedCycles;
        }

        /**
         * Encapsulates the logic for executing opcodes.
         * Out parameters indicate if the program counter should be incremented and which cycle count to use.
         */
        private void ExecuteOpcode(Opcode opcode, out bool incrementProgramCounter, out bool useAlternateCycleCount)
        {
            incrementProgramCounter = true;
            useAlternateCycleCount = false;

            switch (opcode.InstructionSet)
            {
                case InstructionSet.Standard:
                    ExecuteStandardOpcode(opcode, out incrementProgramCounter, out useAlternateCycleCount);
                    break;
                case InstructionSet.Extended:
                    ExecuteExtendedOpcode(opcode, out incrementProgramCounter, out useAlternateCycleCount);
                    break;
                case InstructionSet.Bit:
                    ExecuteBitOpcode(opcode, out incrementProgramCounter, out useAlternateCycleCount);
                    break;
                case InstructionSet.IX:
                    ExecuteIXOpcode(opcode, out incrementProgramCounter, out useAlternateCycleCount);
                    break;
                case InstructionSet.IY:
                    ExecuteIYOpcode(opcode, out incrementProgramCounter, out useAlternateCycleCount);
                    break;
                case InstructionSet.IXBit:
                    ExecuteIXBitOpcode(opcode, out incrementProgramCounter, out useAlternateCycleCount);
                    break;
                case InstructionSet.IYBit:
                    ExecuteIYBitOpcode(opcode, out incrementProgramCounter, out useAlternateCycleCount);
                    break;
                default:
                    throw new Exception(String.Format("Encountered an unhandled InstructionSet type {0} at memory address 0x{1:X4} when attempting to grab an opcode.", opcode.InstructionSet));
            }
        }

        #endregion

        #region Utilities

        /**
         * A helper method used to encapsulate the logic for the setting of the Sign, Zero and
         * Parity flags based on the given value. This method sets three of the condition flags
         * based on the following, and leaves the other flags unmodified.
         *
         * • Zero (Z) is set if result is 0; otherwise, it is reset.
         * • Sign (S) is set if result is negative; otherwise, it is reset.
         * • Parity (P/V) is set if parity even; otherwise, it is reset.
         */
        private void SetSignZeroAndParityFlags(byte result)
        {
            Flags.Sign = (0x80 & result) == 0x80;
            Flags.Zero = result == 0;
            Flags.ParityOverflow = CalculateParityBit(result);
        }

        /**
         * A helper method used to encapsulate the logic for the setting of the condition flags during
         * an 8-bit logical operation (AND/OR/XOR). This method sets all six of the condition flags
         * based on the following:
         * 
         * • Zero (Z) is set if result is 0; otherwise, it is reset.
         * • Sign (S) is set if result is negative; otherwise, it is reset.
         * • Half Carry (H) is set for AND operations; otherwise, it is reset.
         * • Parity (P/V) is set if parity even; otherwise, it is reset.
         * • Subract (N) is reset.
         * • Carry (C) is reset.
         */
        private void SetFlagsFrom8BitLogicalOperation(byte result, bool isAND)
        {
            // An AND operation sets the flag, while XOR/OR reset it.
            Flags.HalfCarry = isAND;

            // These two are always reset.
            Flags.Subtract = false;
            Flags.Carry = false;

            SetSignZeroAndParityFlags(result);
        }

        /**
         * A helper method used to encapsulate the logic for the setting of the condition flags during
         * an 8-bit addition operation. This includes the add with carry opcode variations. This method
         * sets all six of the condition flags based on the following:
         * 
         * • Zero (Z) is set if the result (ignoring a carry from bit 7) is 0x00; otherwise it is reset.
         * • Sign (S) is set if the result is negative (e.g. bit 7 is set); otherwise, it is reset.
         * • Half Carry (H) is set if carry from bit 3; otherwise, it is reset.
         * • Overflow (P/V) is set if overflow (e.g. sum > 127 || sum < -128); otherwise, it is reset.
         * • Subtract (N) is reset.
         * • Carry (C) is set if carry from bit 7; otherwise, it is reset.
         */
        private void SetFlagsFrom8BitAddition(byte addend, byte augend, bool addCarryFlag = false, bool affectsCarryFlag = true)
        {
            var originalCarryFlagSet = Flags.Carry;

            var sum = addend + augend + (addCarryFlag && originalCarryFlagSet ? 1 : 0);

            // Check if the result is zero; only consider 8-bits for the case
            // of a carry out of bit 7 by masking off the bits. For example:
            // 128 + 128 = 256 => 1 0000 0000 & 0xFF => 0
            Flags.Zero = (sum & 0xFF) == 0;

            // The highest order bit is used for 2's complement to indicate a
            // negative number if it is set.
            Flags.Sign = (sum & 0x80) == 0x80;

            // Additions will always reset this flag.
            Flags.Subtract = false;

            // If the sum is over the maximum value for an 8-bit number (255)
            // then we know a carry out of the seventh bit occurred.
            if (affectsCarryFlag)
                Flags.Carry = sum > 255;

            // A half carry occurs when bit 4 is set via a carry from the addition of the
            // lower 4 bits. So here we mask off the lower four bits and add them and see
            // if bit 4 is set or not.
            // (lower 4 bits of addend) + (lower 4 bits of augend) + (another 1 if ADC and C flag is set)
            Flags.HalfCarry = (((addend & 0x0F) + (augend & 0x0F) + (addCarryFlag && originalCarryFlagSet ? 1 : 0))
                                & 0x10) > 0 ? true : false;

            // Overflow is calculated by performing signed addition using 2's
            // complement. If the result is outside of the max/min values of
            // 127 and -128, then it is counted as an overflow. Note that we
            // cast the addends to signed bytes and then perform the addition.
            var signedSum = ((sbyte)addend) + ((sbyte)augend) + (addCarryFlag && originalCarryFlagSet ? 1 : 0);
            Flags.ParityOverflow = signedSum > 127 || signedSum < -128;
        }

        /**
         * A helper method used to encapsulate the logic for the setting of the condition flags during
         * an 16-bit addition operation. Note that the Z80 will only set all six flags for the add with
         * carry opcode variants; for the standard add (without carry) variants only three flags are set.
         * Therefore the setAllFlags can be used to control if all flags are set or not.
         *
         * If setAllFlags is false, only the following three condition flags will be set:
         * • Subtract (N) is reset.
         * • Carry (C) is set if carry from bit 15; otherwise, it is reset.
         * • Half Carry (H) is set if carry from bit 11; otherwise, it is reset.
         *
         * If setAllFlags is true, the following three condition flags will be set, in addition to the
         * afformentioned flags:
         * • Zero (Z) is set if the result (ignoring a carry from bit 15) is 0x0000; otherwise it is reset.
         * • Sign (S) is set if the result is negative (e.g. bit 15 is set); otherwise, it is reset.
         * • Overflow (P/V) is set if overflow (e.g. sum > 127 || sum < -128); otherwise, it is reset.
         */
        private void SetFlagsFrom16BitAddition(UInt16 addend, UInt16 augend, bool addCarryFlag = false, bool setAllFlags = true)
        {
            var originalCarryFlagSet = Flags.Carry;

            var sum = addend + augend + (addCarryFlag && originalCarryFlagSet ? 1 : 0);

            if (setAllFlags)
            {
                // Check if the result is zero; only consider 16-bits for the case
                // of a carry out of bit 7 by masking off the bits. For example:
                // 32768 + 32768 = 65536 => 1 0000 0000 0000 0000 & 0xFFFF => 0
                Flags.Zero = (sum & 0xFFFF) == 0;

                // The highest order bit is used for 2's complement to indicate a
                // negative number if it is set.
                Flags.Sign = (sum & 0x8000) == 0x8000;
            }

            // Additions will always reset this flag.
            Flags.Subtract = false;

            // If the sum is over the maximum value for an 16-bit number (65535)
            // then we know a carry out of the seventh bit occurred.
            Flags.Carry = sum > 65535;

            // A half carry occurs when bit 12 is set via a carry from the addition of the
            // lower 12 bits. So here we mask off the lower 12 bits and add them and see
            // if bit 12 is set or not.
            // (lower 12 bits of addend) + (lower 12 bits of augend) + (another 1 if ADC and C flag is set)
            Flags.HalfCarry = (((addend & 0x0FFF) + (augend & 0x0FFF) + (addCarryFlag && originalCarryFlagSet ? 1 : 0))
                                & 0x1000) > 0 ? true : false;

            if (setAllFlags)
            {
                // Overflow is calculated by performing signed addition using 2's
                // complement. If the result is outside of the max/min values of
                // 32767 and -32768, then it is counted as an overflow. Note that we
                // cast the addends to signed bytes and then perform the addition.

                // C# doesn't have a 16-bit equivalent type to sbyte, so we can't just do
                // a type cast, instead we have to convert manually; use two's complimement
                // to convert the UInt16 values to signed integer values.
                var signedAddend = ((addend & 0x8000) == 0x8000) ? (addend - 65536) : addend;
                var signedAugend = ((augend & 0x8000) == 0x8000) ? (augend - 65536) : augend;

                var signedSum = signedAddend + signedAugend + (addCarryFlag && originalCarryFlagSet ? 1 : 0);
                Flags.ParityOverflow = signedSum > 32767 || signedSum < -32768;
            }
        }

        /**
         * A helper method used to encapsulate the logic for the setting of the condition flags during
         * an 8-bit subtraction operation. This includes the subtract with carry opcode variations. This
         * method sets all six of the condition flags based on the following:
         * 
         * • Zero (Z) is set if the result is 0x00; otherwise it is reset.
         * • Sign (S) is set if the result is negative (e.g. bit 7 is set); otherwise, it is reset.
         * • Half Carry (H) is set if borrow from bit 4; otherwise, it is reset.
         * • Overflow (P/V) is set if overflow (e.g. difference > 127 || difference < -128); otherwise, it is reset.
         * • Subtract (N) is set.
         * • Carry (C) is set if borrow; otherwise, it is reset.
         */
        private void SetFlagsFrom8BitSubtraction(byte minuend, byte subtrahend, bool subtractCarryFlag = false, bool affectsCarryFlag = true)
        {
            var originalCarryFlagSet = Flags.Carry;

            var difference = minuend - subtrahend - (subtractCarryFlag && originalCarryFlagSet ? 1 : 0);

            // Check if the result is zero.
            Flags.Zero = (difference & 0xFF) == 0;

            // The highest order bit is used for 2's complement to indicate a
            // negative number if it is set.
            Flags.Sign = (difference & 0x80) == 0x80;

            // Subtractions will always set this flag.
            Flags.Subtract = true;

            // If the subtrahend is greater than the minuend then we know a borrow occurred.
            if (affectsCarryFlag)
            {
                var borrowOccurred = (subtractCarryFlag && originalCarryFlagSet)
                    ? subtrahend >= minuend // Account for the extra minus one from the carry flag subtraction.
                    : subtrahend > minuend;

                Flags.Carry = borrowOccurred;
            }

            // A half borrow occurs when bit 4 is set via a borrow from the subtraction of the
            // lower 4 bits. So here we mask off the lower four bits and subtract them and see
            // if bit 4 is set or not.
            // (lower 4 bits of minuend) - (lower 4 bits of subtrahend) - (another 1 if ADC and C flag is set)
            Flags.HalfCarry = (((minuend & 0x0F) - (subtrahend & 0x0F) - (subtractCarryFlag && originalCarryFlagSet ? 1 : 0))
                                & 0x10) > 0 ? true : false;

            // Overflow is calculated by performing signed subtraction using 2's
            // complement. If the result is outside of the max/min values of
            // 127 and -128, then it is counted as an overflow. Note that we
            // cast the addends to signed bytes and then perform the subtraction.
            var signedDifference = ((sbyte)minuend) - ((sbyte)subtrahend) - (subtractCarryFlag && originalCarryFlagSet ? 1 : 0);
            Flags.ParityOverflow = signedDifference > 127 || signedDifference < -128;
        }

        /**
         * A helper method used to encapsulate the logic for the setting of the condition flags during
         * an 16-bit subtraction operation. This includes the subtract with carry opcode variations. This
         * method sets all six of the condition flags based on the following:
         * 
         * • Zero (Z) is set if the result is 0x00; otherwise it is reset.
         * • Sign (S) is set if the result is negative (e.g. bit 15 is set); otherwise, it is reset.
         * • Half Carry (H) is set if borrow from bit 12; otherwise, it is reset.
         * • Overflow (P/V) is set if overflow (e.g. difference > 127 || difference < -128); otherwise, it is reset.
         * • Subtract (N) is set.
         * • Carry (C) is set if borrow; otherwise, it is reset.
         */
        private void SetFlagsFrom16BitSubtraction(UInt16 minuend, UInt16 subtrahend, bool subtractCarryFlag = false, bool affectsCarryFlag = true)
        {
            var originalCarryFlagSet = Flags.Carry;

            var difference = minuend - subtrahend - (subtractCarryFlag && originalCarryFlagSet ? 1 : 0);

            // Check if the result is zero.
            Flags.Zero = (difference & 0xFFFF) == 0;

            // The highest order bit is used for 2's complement to indicate a
            // negative number if it is set.
            Flags.Sign = (difference & 0x8000) == 0x8000;

            // Subtractions will always set this flag.
            Flags.Subtract = true;

            // If the subtrahend is greater than the minuend then we know a borrow occurred.
            if (affectsCarryFlag)
            {
                var borrowOccurred = (subtractCarryFlag && originalCarryFlagSet)
                    ? subtrahend >= minuend // Account for the extra minus one from the carry flag subtraction.
                    : subtrahend > minuend;

                Flags.Carry = borrowOccurred;
            }

            // A half borrow occurs when bit 12 is set via a borrow from the subtraction of the
            // lower 12 bits. So here we mask off the lower twelve bits and subtract them and see
            // if bit 12 is set or not.
            // (lower 12 bits of minuend) - (lower 12 bits of subtrahend) - (another 1 if SBC and C flag is set)
            Flags.HalfCarry = (((minuend & 0x0FFF) - (subtrahend & 0x0FFF) - (subtractCarryFlag && originalCarryFlagSet ? 1 : 0))
                                & 0x1000) > 0 ? true : false;

            // Overflow is calculated by performing signed subtraction using 2's
            // complement. If the result is outside of the max/min values of
            // 32767 and -32768, then it is counted as an overflow. Note that we
            // cast the addends to signed bytes and then perform the subtraction.

            // C# doesn't have a 16-bit equivalent type to sbyte, so we can't just do
            // a type cast, instead we have to convert manually; use two's complimement
            // to convert the UInt16 values to signed integer values.
            var signedMinuend = ((minuend & 0x8000) == 0x8000) ? (minuend - 65536) : minuend;
            var signedSubtrahend = ((subtrahend & 0x8000) == 0x8000) ? (subtrahend - 65536) : subtrahend;

            var signedDifference = signedMinuend - signedSubtrahend - (subtractCarryFlag && originalCarryFlagSet ? 1 : 0);
            Flags.ParityOverflow = signedDifference > 32767 || signedDifference < -32768;
        }

        /**
         * A helper method for calculating the parity bit for the P/V flag; returns true (bit set)
         * if the number of bites in the given value is even, otherwise false (bit reset).
         */
        private bool CalculateParityBit(byte value)
        {
            var setBits = 0;

            for (var i = 0; i < 8; i++)
            {
                if ((value & 0x01) == 1)
                    setBits++;

                value = (byte)(value >> 1);
            }

            // Parity bit is set if number of bits is even.
            return setBits == 0 || setBits % 2 == 0;
        }

        #endregion
    }
}
