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

        /** The addressable memory; can include RAM and ROM. See CPUConfig. */
        public byte[] Memory { get; set; }

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
            Memory = new byte[Config.MemorySize];
            Registers = Config.Registers ?? new CPURegisters();
            Flags = Config.Flags ?? new ConditionFlags();
            InterruptsEnabled = Config.InterruptsEnabled;
            InterruptsEnabledPreviousValue = Config.InterruptsEnabledPreviousValue;
            InterruptMode = Config.InterruptMode;

            // Reset the flag that indicates that the ROM has finished executing.
            Finished = false;
        }

        public void LoadMemory(byte[] memory)
        {
            // Ensure the memory data is not larger than we can load.
            if (memory.Length > Config.MemorySize)
                throw new Exception($"Memory cannot exceed {Config.MemorySize} bytes.");

            if (memory.Length != Config.MemorySize)
            {
                // If the memory given is less than the configured memory size, then
                // ensure that the rest of the memory array is zeroed out.
                Memory = new byte[Config.MemorySize];
                Array.Copy(memory, Memory, memory.Length);
            }
            else
                Memory = memory;
        }

        #endregion

        #region Debugging

        public void PrintDebugSummary()
        {
            var opcodeByte = ReadMemory(Registers.PC);
            var opcodeInstruction = Opcodes.GetOpcode(Registers.PC, Memory).Instruction;

            var opcode = String.Format("0x{0:X2} {1}", opcodeByte, opcodeInstruction);
            var pc = String.Format("0x{0:X4}", Registers.PC);
            var sp = String.Format("0x{0:X4}", Registers.SP);
            var regA = String.Format("0x{0:X2}", Registers.A);
            var regB = String.Format("0x{0:X2}", Registers.B);
            var regC = String.Format("0x{0:X2}", Registers.C);
            var regD = String.Format("0x{0:X2}", Registers.D);
            var regE = String.Format("0x{0:X2}", Registers.E);
            var regH = String.Format("0x{0:X2}", Registers.H);
            var regL = String.Format("0x{0:X2}", Registers.L);

            var valueAtDE = Registers.DE >= Memory.Length ? "N/A" : String.Format("0x{0:X2}", ReadMemory(Registers.DE));
            var valueAtHL = Registers.HL >= Memory.Length ? "N/A" : String.Format("0x{0:X2}", ReadMemory(Registers.HL));

            Console.WriteLine($"Opcode: {opcode}");
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
                throw new Exception(String.Format("Unable to fetch opcode structure for byte 0x{0:X2} at memory address 0x{1:X4}.", Memory[Registers.PC], Registers.PC));

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
         * Out parameters indicate if the program counter should be executed and which cycle count to use.
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

        private void SetFlagsFrom8BitAddition(byte addend, byte augend, bool addCarryFlag = false)
        {
            var originalCarryFlagSet = Flags.Carry;

            var sum = addend + augend + (addCarryFlag && originalCarryFlagSet ? 1 : 0);

            // Check if the result is zero; only consider 8-bits for the case
            // of an overflow (e.g. 128 + 128 = 256 => 1 0000 0000 & 0xFF => 0).
            Flags.Zero = (sum & 0xFF) == 0;

            // The highest order bit is used for 2's complement to indicate a
            // negative number if it is set.
            Flags.Sign = (sum & 0x80) == 0x80;

            // Additions will always reset this flag.
            Flags.Subtract = false;

            // If the sum is over the maximum value for an 8-bit number (255)
            // then we know a carry out of the seventh bit occurred.
            Flags.Carry = sum > 255;

            // If the third bit (highest bit of the nibble) of either is set
            // and sum of lower nibble is over the maximum value for a 4-bit
            // number (15) then we know a carry out of the third bit occurred.
            Flags.HalfCarry =
                (((addend & 0x08) == 0x08) || ((augend & 0x08) == 0x08))
                &&
                (((addend & 0x0F) + (augend & 0x0F)) > 15);

            // In the case of Add With Carry, if the carry flag was set and added
            // and we didn't detect a half carry yet, we need to also add the carry
            // flag and then re-detect for a half carry.
            if (addCarryFlag && originalCarryFlagSet & !Flags.HalfCarry)
            {
                Flags.HalfCarry =
                    ((((addend + augend) & 0x08) == 0x08))
                    &&
                    ((((addend + augend) & 0x0F) + 1) > 15);
            }

            // Overflow is calculated by performing signed addition using 2's
            // complement. If the result is outside of the max/min values of
            // 127 and -128, then it is counted as an overflow. Note that we
            // cast the addends to signed bytes and then perform the addition.
            // var signedSum = ((sbyte)addend) + ((sbyte)augend);
            var signedSum = ((sbyte)addend) + ((sbyte)augend) + (addCarryFlag && originalCarryFlagSet ? 1 : 0);
            Flags.ParityOverflow = signedSum > 127 || signedSum < -128;
        }

        private void SetFlagsFrom16BitAddition(UInt16 addend, UInt16 augend, bool addCarryFlag = false)
        {
            var originalCarryFlagSet = Flags.Carry;

            var sum = addend + augend + (addCarryFlag && originalCarryFlagSet ? 1 : 0);

            // TODO
            // if (addCarryFlag)
            // {
            //     // Check if the result is zero; only consider 8-bits for the case
            //     // of an overflow (e.g. 128 + 128 = 256 => 1 0000 0000 & 0xFF => 0).
            //     Flags.Zero = (sum & 0xFF) == 0;

            //     // The highest order bit is used for 2's complement to indicate a
            //     // negative number if it is set.
            //     Flags.Sign = (sum & 0x80) == 0x80;
            // }

            // Additions will always reset this flag.
            Flags.Subtract = false;

            // If the sum is over the maximum value for an 16-bit number (65535)
            // then we know a carry out of the seventh bit occurred.
            Flags.Carry = sum > 65535;

            // If the 11th bit (highest bit of the nibble) of either is set
            // and sum of lower nibble is over the maximum value for a 12-bit
            // number (4095) then we know a carry out of the third bit occurred.
            Flags.HalfCarry =
                (((addend & 0x0800) == 0x0800) || ((augend & 0x0800) == 0x0800))
                &&
                (((addend & 0x0FFF) + (augend & 0x0FFF)) > 4095);

            // TODO
            // // In the case of Add With Carry, if the carry flag was set and added
            // // and we didn't detect a half carry yet, we need to also add the carry
            // // flag and then re-detect for a half carry.
            // if (addCarryFlag && originalCarryFlagSet & !Flags.HalfCarry)
            // {
            //     Flags.HalfCarry =
            //         ((((addend + augend) & 0x08) == 0x08))
            //         &&
            //         ((((addend + augend) & 0x0F) + 1) > 15);
            // }

            // TODO
            // if (addCarryFlag)
            // {
            //     // Overflow is calculated by performing signed addition using 2's
            //     // complement. If the result is outside of the max/min values of
            //     // 127 and -128, then it is counted as an overflow. Note that we
            //     // cast the addends to signed bytes and then perform the addition.
            //     // var signedSum = ((sbyte)addend) + ((sbyte)augend);
            //     var signedSum = ((sbyte)addend) + ((sbyte)augend) + (addCarryFlag && originalCarryFlagSet ? 1 : 0);
            //     Flags.ParityOverflow = signedSum > 127 || signedSum < -128;
            // }
        }

        private void SetFlags(byte? result = null, bool? carry = null, bool? halfCarry = false, bool? subtract = null)
        {
            if (result != null)
            {
                Flags.Zero = result == 0;
                Flags.Sign = (result & 0b10000000) == 0b10000000;
                Flags.ParityOverflow = CalculateParityBit((byte)result);
            }

            SetFlags(carry: carry, halfCarry: halfCarry, subtract: subtract);
        }

        private void SetFlags(UInt16? result = null, bool? carry = null, bool? halfCarry = false, bool? subtract = null)
        {
            if (result != null)
            {
                Flags.Zero = result == 0;
                Flags.Sign = (result & 0b1000000000000000) == 0b1000000000000000;
                Flags.ParityOverflow = CalculateParityBit((UInt16)result);
            }

            SetFlags(carry: carry, halfCarry: halfCarry, subtract: subtract);
        }

        private void SetFlags(bool? carry = null, bool? halfCarry = false, bool? subtract = null)
        {
            if (carry != null)
                Flags.Carry = carry.Value;

            // TODO: This keeps the old 8080 behavior of always resetting the flag for all operations.
            // I believe this is wrong for Z80, there are many cases where it remains unmodified.
            Flags.HalfCarry = halfCarry == null ? false : halfCarry.Value;

            // This flag isn't modified in all cases. If not provided, then don't modify.
            if (subtract != null)
                Flags.Subtract = subtract.Value;
        }

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

        private bool CalculateParityBit(UInt16 value)
        {
            var setBits = 0;

            for (var i = 0; i < 16; i++)
            {
                if ((value & 0x01) == 1)
                    setBits++;

                value = (byte)(value >> 1);
            }

            // Parity bit is set if number of bits is even.
            return setBits == 0 || setBits % 2 == 0;
        }

        private byte ReadMemory(int address)
        {
            var mirroringEnabled = Config.MirrorMemoryStart != 0 && Config.MirrorMemoryEnd != 0;
            var error = false;

            byte? result = null;

            if (address < 0)
            {
                error = true;
            }
            else if (address < Config.MemorySize)
            {
                result = Memory[address];
            }
            else if (mirroringEnabled && address >= Config.MirrorMemoryStart && address <= Config.MirrorMemoryEnd)
            {
                var translated = address - (Config.MirrorMemoryEnd - Config.MirrorMemoryStart + 1);

                if (translated < 0 || translated >= Config.MemorySize)
                {
                    error = true;
                }
                else
                {
                    result = Memory[translated];
                }
            }
            else
            {
                error = true;
            }

            if (error)
            {
                var programCounterFormatted = String.Format("0x{0:X4}", Registers.PC);
                var addressFormatted = String.Format("0x{0:X4}", address);
                var startAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryStart);
                var endAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryEnd);
                var mirrorEndAddressFormatted = String.Format("0x{0:X4}", Config.MirrorMemoryEnd);
                throw new Exception($"Illegal memory address ({addressFormatted}) specified for read memory operation at address {programCounterFormatted}; expected address to be between {startAddressFormatted} and {(mirroringEnabled ? mirrorEndAddressFormatted : endAddressFormatted)} inclusive.");
            }

            if (result == null)
                throw new Exception("Failed sanity check; result should be set.");

            return result.Value;
        }

        private UInt16 ReadMemory16(int address)
        {
            var lower = ReadMemory(address);
            var upper = ReadMemory(address + 1) << 8;
            var value = (UInt16)(upper | lower);
            return value;
        }

        private void WriteMemory(int address, byte value)
        {
            // Determine if we should allow the write to memory based on the address
            // if the configuration has specified a restricted writeable range.
            var enforceWriteBoundsCheck = Config.WriteableMemoryStart != 0 && Config.WriteableMemoryEnd != 0;
            var mirroringEnabled = Config.MirrorMemoryStart != 0 && Config.MirrorMemoryEnd != 0;
            var allowWrite = true;
            var error = false;

            if (enforceWriteBoundsCheck)
                allowWrite = address >= Config.WriteableMemoryStart && address <= Config.WriteableMemoryEnd;

            if (allowWrite)
            {
                Memory[address] = value;
            }
            else if (mirroringEnabled && address >= Config.MirrorMemoryStart && address <= Config.MirrorMemoryEnd)
            {
                var translated = address - (Config.MirrorMemoryEnd - Config.MirrorMemoryStart + 1);

                if (translated < 0 || translated >= Config.MemorySize)
                {
                    error = true;
                }
                else
                {
                    Memory[translated] = value;
                }
            }
            else
            {
                error = true;
            }

            if (error)
            {
                var programCounterFormatted = String.Format("0x{0:X4}", Registers.PC);
                var addressFormatted = String.Format("0x{0:X4}", address);
                var startAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryStart);
                var endAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryEnd);
                var mirrorEndAddressFormatted = String.Format("0x{0:X4}", Config.MirrorMemoryEnd);
                throw new Exception($"Illegal memory address ({addressFormatted}) specified for write memory operation at address {programCounterFormatted}; expected address to be between {startAddressFormatted} and {(mirroringEnabled ? mirrorEndAddressFormatted : endAddressFormatted)} inclusive.");
            }
        }

        private void WriteMemory16(int address, UInt16 value)
        {
            var lower = (byte)(value & 0x00FF);
            var upper = (byte)((value & 0xFF00) >> 8);
            WriteMemory(address, lower);
            WriteMemory(address + 1, upper);
        }

        #endregion
    }
}
