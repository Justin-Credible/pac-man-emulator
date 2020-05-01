using System;

namespace JustinCredible.ZilogZ80
{
    /**
     * An emulated version of the Zilog Z80 CPU.
     */
    public class CPU
    {
        /**
         * Indicates the ROM has finished executing via a HLT opcode.
         * Step should not be called again without first calling Reset.
         */
        public bool Finished { get; private set; }

        /** The addressable memory; can include RAM and ROM. See CPUConfig. */
        public byte[] Memory { get; set; }

        /** The primary CPU registers (A B C D E H L) */
        public CPURegisters Registers { get; set; }

        /** Alternative register set (A' B' C' D' E' H' L') */
        public CPURegisters AlternateRegisters { get; set; } // TODO

        /** The encapsulated condition/flags register (F) */
        public ConditionFlags Flags { get; set; }

        /** Alternative flag register (F') */
        public ConditionFlags AlternativeFlags { get; set; } // TODO

        /** The interrupt vector register (I) */
        public UInt16 InterruptVector { get; set; } // TODO

        /** The memory refresh register (R) */
        public UInt16 MemoryRefresh { get; set; } // TODO

        /** The index register (IX) */
        public UInt16 IndexIX { get; set; } // TODO

        /** The index register (IY) */
        public UInt16 IndexIY { get; set; } // TODO

        /** Program Counter; 16-bits */
        public UInt16 ProgramCounter { get; set; }

        /** Stack Pointer; 16-bits */
        public UInt16 StackPointer { get; set; }

        // TODO: Interrupt modes
        /** Indicates if interrupts are enabled or not. */
        public bool InterruptsEnabled { get; set; }

        /** Configuration for the CPU; used to customize the CPU instance. */
        public CPUConfig Config { get; private set; }

        // public delegate void CPUDiagDebugEvent(int eventID);

        /** Fired on CALL 0x05 when EnableCPUDiagMode is true. */
        // public event CPUDiagDebugEvent OnCPUDiagDebugEvent;

        // TODO: Is device I/O handled differently on Z80?

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
            ProgramCounter = Config.ProgramCounter;
            StackPointer = Config.StackPointer;
            InterruptsEnabled = Config.InterruptsEnabled;

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
            var opcodeByte = ReadMemory(ProgramCounter);
            var opcodeInstruction = Opcodes.Lookup[opcodeByte].Instruction;

            var opcode = String.Format("0x{0:X2} {1}", opcodeByte, opcodeInstruction);
            var pc = String.Format("0x{0:X4}", ProgramCounter);
            var sp = String.Format("0x{0:X4}", StackPointer);
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
            Console.WriteLine($"Zero: {Flags.Zero}\tSign: {Flags.Sign}\tParity: {Flags.Parity}\tCarry: {Flags.Carry}\tAux Carry: {Flags.AuxCarry}");
        }

        #endregion

        #region Step

        /** Executes the given interrupt RST instruction and returns the number of cycles it took to execute. */
        public int StepInterrupt(Interrupt id)
        {
            switch (id)
            {
                case Interrupt.Zero:
                    ExecuteCALL(0x000, ProgramCounter);
                    return Opcodes.RST_0.Cycles;
                case Interrupt.One:
                    ExecuteCALL(0x0008, ProgramCounter);
                    return Opcodes.RST_1.Cycles;
                case Interrupt.Two:
                    ExecuteCALL(0x0010, ProgramCounter);
                    return Opcodes.RST_2.Cycles;
                case Interrupt.Three:
                    ExecuteCALL(0x0018, ProgramCounter);
                    return Opcodes.RST_3.Cycles;
                case Interrupt.Four:
                    ExecuteCALL(0x0020, ProgramCounter);
                    return Opcodes.RST_4.Cycles;
                case Interrupt.Five:
                    ExecuteCALL(0x0028, ProgramCounter);
                    return Opcodes.RST_5.Cycles;
                case Interrupt.Six:
                    ExecuteCALL(0x0030, ProgramCounter);
                    return Opcodes.RST_6.Cycles;
                case Interrupt.Seven:
                    ExecuteCALL(0x0038, ProgramCounter);
                    return Opcodes.RST_7.Cycles;
                default:
                    throw new Exception($"Unhandled interrupt ID: {id}");
            }
        }

        /** Executes the next instruction and returns the number of cycles it took to execute. */
        public int Step()
        {
            // Fetch the next opcode to be executed, as indicated by the program counter.
            var opcodeByte = ReadMemory(ProgramCounter);

            return Step(opcodeByte);
        }

        private int Step(byte opcodeByte)
        {
            // Sanity check.
            if (Finished)
                throw new Exception("Program has finished execution; Reset() must be invoked before invoking Step() again.");

            // Fetch the opcode metadata.
            var opcode = Opcodes.Lookup[opcodeByte];

            // Indicates if we should increment the program counter after executing the instruction.
            // This is almost always the case, but there are a few cases where we don't want to.
            var incrementProgramCounter = true;

            // Some instructions have an alternate cycle count depending on the outcome of the
            // operation. This indicates if we should count the regular or alternate cycle count
            // when returning the number of cycles that the instruction took to execute.
            var useAlternateCycleCount = false;

            #region Opcode Execution

            // Execute the opcode.
            switch (opcodeByte)
            {
                case OpcodeBytes.HLT:
                    Finished = true;
                    incrementProgramCounter = false;
                    break;

                #region NOP - No operation

                    case OpcodeBytes.NOP:
                    case OpcodeBytes.NOP2:
                    case OpcodeBytes.NOP3:
                    case OpcodeBytes.NOP4:
                    case OpcodeBytes.NOP5:
                    case OpcodeBytes.NOP6:
                    case OpcodeBytes.NOP7:
                    case OpcodeBytes.NOP8:
                        break;

                #endregion

                #region Carry bit instructions

                    case OpcodeBytes.CMC:
                        Flags.Carry = !Flags.Carry;
                        break;

                    case OpcodeBytes.STC:
                        Flags.Carry = true;
                        break;

                #endregion

                #region Single register instructions

                    #region INR - Increment Register or Memory

                        case OpcodeBytes.INR_B:
                            Registers.B++;
                            SetFlags(false, Registers.B);
                            break;
                        case OpcodeBytes.INR_C:
                            Registers.C++;
                            SetFlags(false, Registers.C);
                            break;
                        case OpcodeBytes.INR_D:
                            Registers.D++;
                            SetFlags(false, Registers.D);
                            break;
                        case OpcodeBytes.INR_E:
                            Registers.E++;
                            SetFlags(false, Registers.E);
                            break;
                        case OpcodeBytes.INR_H:
                            Registers.H++;
                            SetFlags(false, Registers.H);
                            break;
                        case OpcodeBytes.INR_L:
                            Registers.L++;
                            SetFlags(false, Registers.L);
                            break;
                        case OpcodeBytes.INR_M:
                        {
                            var value = ReadMemory(Registers.HL);
                            value++;
                            WriteMemory(Registers.HL, value);
                            SetFlags(false, ReadMemory(Registers.HL));
                            break;
                        }
                        case OpcodeBytes.INR_A:
                            Registers.A++;
                            SetFlags(false, Registers.A);
                            break;

                    #endregion

                    #region DCR - Decrement Register or Memory

                        case OpcodeBytes.DCR_B:
                            Registers.B--;
                            SetFlags(false, Registers.B);
                            break;
                        case OpcodeBytes.DCR_C:
                            Registers.C--;
                            SetFlags(false, Registers.C);
                            break;
                        case OpcodeBytes.DCR_D:
                            Registers.D--;
                            SetFlags(false, Registers.D);
                            break;
                        case OpcodeBytes.DCR_E:
                            Registers.E--;
                            SetFlags(false, Registers.E);
                            break;
                        case OpcodeBytes.DCR_H:
                            Registers.H--;
                            SetFlags(false, Registers.H);
                            break;
                        case OpcodeBytes.DCR_L:
                            Registers.L--;
                            SetFlags(false, Registers.L);
                            break;
                        case OpcodeBytes.DCR_M:
                        {
                            var value = ReadMemory(Registers.HL);
                            value--;
                            WriteMemory(Registers.HL, value);
                            SetFlags(false, ReadMemory(Registers.HL));
                            break;
                        }
                        case OpcodeBytes.DCR_A:
                            Registers.A--;
                            SetFlags(false, Registers.A);
                            break;

                    #endregion

                    /** Compliment Accumulator */
                    case OpcodeBytes.CMA:
                        Registers.A = (byte)~Registers.A;
                        break;

                    /** Decimal Adjust Accumulator */
                    case OpcodeBytes.DAA:
                        ExecuteDAA();
                        break;

                #endregion

                #region Data transfer instructions

                    #region STAX - Store accumulator

                        case OpcodeBytes.STAX_B:
                            WriteMemory(Registers.BC, Registers.A);
                            break;
                        case OpcodeBytes.STAX_D:
                            WriteMemory(Registers.DE, Registers.A);
                            break;

                    #endregion

                    #region LDAX - Load accumulator

                        case OpcodeBytes.LDAX_B:
                            Registers.A = ReadMemory(Registers.BC);
                            break;
                        case OpcodeBytes.LDAX_D:
                            Registers.A = ReadMemory(Registers.DE);
                            break;

                    #endregion

                    #region MOV - Move (copy) data

                        #region MOV X, X (from register to register)

                        case OpcodeBytes.MOV_B_B:
                            // NOP
                            break;
                        case OpcodeBytes.MOV_B_C:
                            Registers.B = Registers.C;
                            break;
                        case OpcodeBytes.MOV_B_D:
                            Registers.B = Registers.D;
                            break;
                        case OpcodeBytes.MOV_B_E:
                            Registers.B = Registers.E;
                            break;
                        case OpcodeBytes.MOV_B_H:
                            Registers.B = Registers.H;
                            break;
                        case OpcodeBytes.MOV_B_L:
                            Registers.B = Registers.L;
                            break;
                        case OpcodeBytes.MOV_B_A:
                            Registers.B = Registers.A;
                            break;
                        case OpcodeBytes.MOV_C_B:
                            Registers.C = Registers.B;
                            break;
                        case OpcodeBytes.MOV_C_C:
                            // NOP
                            break;
                        case OpcodeBytes.MOV_C_D:
                            Registers.C = Registers.D;
                            break;
                        case OpcodeBytes.MOV_C_E:
                            Registers.C = Registers.E;
                            break;
                        case OpcodeBytes.MOV_C_H:
                            Registers.C = Registers.H;
                            break;
                        case OpcodeBytes.MOV_C_L:
                            Registers.C = Registers.L;
                            break;
                        case OpcodeBytes.MOV_C_A:
                            Registers.C = Registers.A;
                            break;
                        case OpcodeBytes.MOV_D_B:
                            Registers.D = Registers.B;
                            break;
                        case OpcodeBytes.MOV_D_C:
                            Registers.D = Registers.C;
                            break;
                        case OpcodeBytes.MOV_D_D:
                            // NOP
                            break;
                        case OpcodeBytes.MOV_D_E:
                            Registers.D = Registers.E;
                            break;
                        case OpcodeBytes.MOV_D_H:
                            Registers.D = Registers.H;
                            break;
                        case OpcodeBytes.MOV_D_L:
                            Registers.D = Registers.L;
                            break;
                        case OpcodeBytes.MOV_D_A:
                            Registers.D = Registers.A;
                            break;
                        case OpcodeBytes.MOV_E_B:
                            Registers.E = Registers.B;
                            break;
                        case OpcodeBytes.MOV_E_C:
                            Registers.E = Registers.C;
                            break;
                        case OpcodeBytes.MOV_E_D:
                            Registers.E = Registers.D;
                            break;
                        case OpcodeBytes.MOV_E_E:
                            // NOP
                            break;
                        case OpcodeBytes.MOV_E_H:
                            Registers.E = Registers.H;
                            break;
                        case OpcodeBytes.MOV_E_L:
                            Registers.E = Registers.L;
                            break;
                        case OpcodeBytes.MOV_E_A:
                            Registers.E = Registers.A;
                            break;
                        case OpcodeBytes.MOV_H_B:
                            Registers.H = Registers.B;
                            break;
                        case OpcodeBytes.MOV_H_C:
                            Registers.H = Registers.C;
                            break;
                        case OpcodeBytes.MOV_H_D:
                            Registers.H = Registers.D;
                            break;
                        case OpcodeBytes.MOV_H_E:
                            Registers.H = Registers.E;
                            break;
                        case OpcodeBytes.MOV_H_H:
                            // NOP
                            break;
                        case OpcodeBytes.MOV_H_L:
                            Registers.H = Registers.L;
                            break;
                        case OpcodeBytes.MOV_H_A:
                            Registers.H = Registers.A;
                            break;
                        case OpcodeBytes.MOV_L_B:
                            Registers.L = Registers.B;
                            break;
                        case OpcodeBytes.MOV_L_C:
                            Registers.L = Registers.C;
                            break;
                        case OpcodeBytes.MOV_L_D:
                            Registers.L = Registers.D;
                            break;
                        case OpcodeBytes.MOV_L_E:
                            Registers.L = Registers.E;
                            break;
                        case OpcodeBytes.MOV_L_H:
                            Registers.L = Registers.H;
                            break;
                        case OpcodeBytes.MOV_L_L:
                            // NOP
                            break;
                        case OpcodeBytes.MOV_L_A:
                            Registers.L = Registers.A;
                            break;
                        case OpcodeBytes.MOV_A_B:
                            Registers.A = Registers.B;
                            break;
                        case OpcodeBytes.MOV_A_C:
                            Registers.A = Registers.C;
                            break;
                        case OpcodeBytes.MOV_A_D:
                            Registers.A = Registers.D;
                            break;
                        case OpcodeBytes.MOV_A_E:
                            Registers.A = Registers.E;
                            break;
                        case OpcodeBytes.MOV_A_H:
                            Registers.A = Registers.H;
                            break;
                        case OpcodeBytes.MOV_A_L:
                            Registers.A = Registers.L;
                            break;
                        case OpcodeBytes.MOV_A_A:
                            // NOP
                            break;

                        #endregion

                        #region MOV X, M (from memory to register)

                        case OpcodeBytes.MOV_B_M:
                            Registers.B = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.MOV_C_M:
                            Registers.C = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.MOV_D_M:
                            Registers.D = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.MOV_E_M:
                            Registers.E = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.MOV_H_M:
                            Registers.H = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.MOV_L_M:
                            Registers.L = ReadMemory(Registers.HL);
                            break;
                        case OpcodeBytes.MOV_A_M:
                            Registers.A = ReadMemory(Registers.HL);
                            break;

                        #endregion

                        #region MOV M, X (from register to memory)

                        case OpcodeBytes.MOV_M_B:
                            WriteMemory(Registers.HL, Registers.B);
                            break;
                        case OpcodeBytes.MOV_M_C:
                            WriteMemory(Registers.HL, Registers.C);
                            break;
                        case OpcodeBytes.MOV_M_D:
                            WriteMemory(Registers.HL, Registers.D);
                            break;
                        case OpcodeBytes.MOV_M_E:
                            WriteMemory(Registers.HL, Registers.E);
                            break;
                        case OpcodeBytes.MOV_M_H:
                            WriteMemory(Registers.HL, Registers.H);
                            break;
                        case OpcodeBytes.MOV_M_L:
                            WriteMemory(Registers.HL, Registers.L);
                            break;
                        case OpcodeBytes.MOV_M_A:
                            WriteMemory(Registers.HL, Registers.A);
                            break;

                        #endregion

                    #endregion

                #endregion

                #region Register or memory to accumulator instructions

                    #region ADD - Add register or memory to accumulator

                        case OpcodeBytes.ADD_B:
                            ExecuteADD(Registers.B);
                            break;
                        case OpcodeBytes.ADD_C:
                            ExecuteADD(Registers.C);
                            break;
                        case OpcodeBytes.ADD_D:
                            ExecuteADD(Registers.D);
                            break;
                        case OpcodeBytes.ADD_E:
                            ExecuteADD(Registers.E);
                            break;
                        case OpcodeBytes.ADD_H:
                            ExecuteADD(Registers.H);
                            break;
                        case OpcodeBytes.ADD_L:
                            ExecuteADD(Registers.L);
                            break;
                        case OpcodeBytes.ADD_M:
                            ExecuteADD(ReadMemory(Registers.HL));
                            break;
                        case OpcodeBytes.ADD_A:
                            ExecuteADD(Registers.A);
                            break;

                    #endregion

                    #region SUB - Subtract register or memory from accumulator

                        case OpcodeBytes.SUB_B:
                            ExecuteSUB(Registers.B);
                            break;
                        case OpcodeBytes.SUB_C:
                            ExecuteSUB(Registers.C);
                            break;
                        case OpcodeBytes.SUB_D:
                            ExecuteSUB(Registers.D);
                            break;
                        case OpcodeBytes.SUB_E:
                            ExecuteSUB(Registers.E);
                            break;
                        case OpcodeBytes.SUB_H:
                            ExecuteSUB(Registers.H);
                            break;
                        case OpcodeBytes.SUB_L:
                            ExecuteSUB(Registers.L);
                            break;
                        case OpcodeBytes.SUB_M:
                            ExecuteSUB(ReadMemory(Registers.HL));
                            break;
                        case OpcodeBytes.SUB_A:
                            ExecuteSUB(Registers.A);
                            break;

                    #endregion

                    #region ANA - Logical AND register or memory with accumulator

                        case OpcodeBytes.ANA_B:
                            Registers.A = (byte)(Registers.A & Registers.B);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ANA_C:
                            Registers.A = (byte)(Registers.A & Registers.C);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ANA_D:
                            Registers.A = (byte)(Registers.A & Registers.D);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ANA_E:
                            Registers.A = (byte)(Registers.A & Registers.E);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ANA_H:
                            Registers.A = (byte)(Registers.A & Registers.H);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ANA_L:
                            Registers.A = (byte)(Registers.A & Registers.L);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ANA_M:
                            Registers.A = (byte)(Registers.A & ReadMemory(Registers.HL));
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ANA_A:
                            Registers.A = (byte)(Registers.A & Registers.A);
                            SetFlags(false, Registers.A);
                            break;

                    #endregion

                    #region ORA - Logical OR register or memory with accumulator

                        case OpcodeBytes.ORA_B:
                            Registers.A = (byte)(Registers.A | Registers.B);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ORA_C:
                            Registers.A = (byte)(Registers.A | Registers.C);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ORA_D:
                            Registers.A = (byte)(Registers.A | Registers.D);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ORA_E:
                            Registers.A = (byte)(Registers.A | Registers.E);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ORA_H:
                            Registers.A = (byte)(Registers.A | Registers.H);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ORA_L:
                            Registers.A = (byte)(Registers.A | Registers.L);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ORA_M:
                            Registers.A = (byte)(Registers.A | ReadMemory(Registers.HL));
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.ORA_A:
                            Registers.A = (byte)(Registers.A | Registers.A);
                            SetFlags(false, Registers.A);
                            break;

                    #endregion

                    #region ADC - Add register or memory to accumulator with carry

                        case OpcodeBytes.ADC_B:
                            ExecuteADD(Registers.B, true);
                            break;
                        case OpcodeBytes.ADC_C:
                            ExecuteADD(Registers.C, true);
                            break;
                        case OpcodeBytes.ADC_D:
                            ExecuteADD(Registers.D, true);
                            break;
                        case OpcodeBytes.ADC_E:
                            ExecuteADD(Registers.E, true);
                            break;
                        case OpcodeBytes.ADC_H:
                            ExecuteADD(Registers.H, true);
                            break;
                        case OpcodeBytes.ADC_L:
                            ExecuteADD(Registers.L, true);
                            break;
                        case OpcodeBytes.ADC_M:
                            ExecuteADD(ReadMemory(Registers.HL), true);
                            break;
                        case OpcodeBytes.ADC_A:
                            ExecuteADD(Registers.A, true);
                            break;

                    #endregion

                    #region SBB - Subtract register or memory from accumulator with borrow

                        case OpcodeBytes.SBB_B:
                            ExecuteSUB(Registers.B, true);
                            break;
                        case OpcodeBytes.SBB_C:
                            ExecuteSUB(Registers.C, true);
                            break;
                        case OpcodeBytes.SBB_D:
                            ExecuteSUB(Registers.D, true);
                            break;
                        case OpcodeBytes.SBB_E:
                            ExecuteSUB(Registers.E, true);
                            break;
                        case OpcodeBytes.SBB_H:
                            ExecuteSUB(Registers.H, true);
                            break;
                        case OpcodeBytes.SBB_L:
                            ExecuteSUB(Registers.L, true);
                            break;
                        case OpcodeBytes.SBB_M:
                            ExecuteSUB(ReadMemory(Registers.HL), true);
                            break;
                        case OpcodeBytes.SBB_A:
                            ExecuteSUB(Registers.A, true);
                            break;

                    #endregion

                    #region XRA - Logical XOR register or memory with accumulator

                        case OpcodeBytes.XRA_B:
                            Registers.A = (byte)(Registers.A ^ Registers.B);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.XRA_C:
                            Registers.A = (byte)(Registers.A ^ Registers.C);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.XRA_D:
                            Registers.A = (byte)(Registers.A ^ Registers.D);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.XRA_E:
                            Registers.A = (byte)(Registers.A ^ Registers.E);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.XRA_H:
                            Registers.A = (byte)(Registers.A ^ Registers.H);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.XRA_L:
                            Registers.A = (byte)(Registers.A ^ Registers.L);
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.XRA_M:
                            Registers.A = (byte)(Registers.A ^ ReadMemory(Registers.HL));
                            SetFlags(false, Registers.A);
                            break;
                        case OpcodeBytes.XRA_A:
                            Registers.A = (byte)(Registers.A ^ Registers.A);
                            SetFlags(false, Registers.A);
                            break;

                    #endregion

                    #region CMP - Compare register or memory with accumulator

                        case OpcodeBytes.CMP_B:
                            ExecuteSUB(Registers.B, false, false);
                            break;
                        case OpcodeBytes.CMP_C:
                            ExecuteSUB(Registers.C, false, false);
                            break;
                        case OpcodeBytes.CMP_D:
                            ExecuteSUB(Registers.D, false, false);
                            break;
                        case OpcodeBytes.CMP_E:
                            ExecuteSUB(Registers.E, false, false);
                            break;
                        case OpcodeBytes.CMP_H:
                            ExecuteSUB(Registers.H, false, false);
                            break;
                        case OpcodeBytes.CMP_L:
                            ExecuteSUB(Registers.L, false, false);
                            break;
                        case OpcodeBytes.CMP_M:
                            ExecuteSUB(ReadMemory(Registers.HL), false, false);
                            break;
                        case OpcodeBytes.CMP_A:
                            ExecuteSUB(Registers.A, false, false);
                            break;

                    #endregion

                #endregion

                #region Rotate accumulator instructions

                    // Rotate accumulator left
                    // A = A << 1; bit 0 = prev bit 7; CY = prev bit 7
                    case OpcodeBytes.RLC:
                        ExecuteRotateAccumulator(left: true);
                        break;

                    // Rotate accumulator right
                    // A = A >> 1; bit 7 = prev bit 0; CY = prev bit 0
                    case OpcodeBytes.RRC:
                        ExecuteRotateAccumulator(left: false);
                        break;

                    // Rotate accumulator left through carry
                    // A = A << 1; bit 0 = prev CY; CY = prev bit 7
                    case OpcodeBytes.RAL:
                        ExecuteRotateAccumulator(left: true, rotateThroughCarry: true);
                        break;

                    // Rotate accumulator right through carry
                    // A = A >> 1; bit 7 = prev CY; CY = prev bit 0
                    case OpcodeBytes.RAR:
                        ExecuteRotateAccumulator(left: false, rotateThroughCarry: true);
                        break;

                #endregion

                #region Register pair instructions

                    #region INX - Increment register pair

                        case OpcodeBytes.INX_B:
                            Registers.BC++;
                            break;
                        case OpcodeBytes.INX_D:
                            Registers.DE++;
                            break;
                        case OpcodeBytes.INX_H:
                            Registers.HL++;
                            break;
                        case OpcodeBytes.INX_SP:
                            StackPointer++;
                            break;

                    #endregion

                    #region DCX - Decrement register pair

                        case OpcodeBytes.DCX_B:
                            Registers.BC--;
                            break;
                        case OpcodeBytes.DCX_D:
                            Registers.DE--;
                            break;
                        case OpcodeBytes.DCX_H:
                            Registers.HL--;
                            break;
                        case OpcodeBytes.DCX_SP:
                            StackPointer--;
                            break;

                    #endregion

                    #region PUSH - Push data onto the stack

                        case OpcodeBytes.PUSH_B:
                            WriteMemory(StackPointer - 1, Registers.B);
                            WriteMemory(StackPointer - 2, Registers.C);
                            StackPointer = (UInt16)(StackPointer - 2);
                            break;
                        case OpcodeBytes.PUSH_D:
                            WriteMemory(StackPointer - 1, Registers.D);
                            WriteMemory(StackPointer - 2, Registers.E);
                            StackPointer = (UInt16)(StackPointer - 2);
                            break;
                        case OpcodeBytes.PUSH_H:
                            WriteMemory(StackPointer - 1, Registers.H);
                            WriteMemory(StackPointer - 2, Registers.L);
                            StackPointer = (UInt16)(StackPointer - 2);
                            break;
                        case OpcodeBytes.PUSH_PSW:
                            WriteMemory(StackPointer - 1, Registers.A);
                            WriteMemory(StackPointer - 2, Flags.ToByte());
                            StackPointer = (UInt16)(StackPointer - 2);
                            break;

                    #endregion

                    #region POP - Pop data off of the stack

                        case OpcodeBytes.POP_B:
                            Registers.B = ReadMemory(StackPointer + 1);
                            Registers.C = ReadMemory(StackPointer);
                            StackPointer = (UInt16)(StackPointer + 2);
                            break;
                        case OpcodeBytes.POP_D:
                            Registers.D = ReadMemory(StackPointer + 1);
                            Registers.E = ReadMemory(StackPointer);
                            StackPointer = (UInt16)(StackPointer + 2);
                            break;
                        case OpcodeBytes.POP_H:
                            Registers.H = ReadMemory(StackPointer + 1);
                            Registers.L = ReadMemory(StackPointer);
                            StackPointer = (UInt16)(StackPointer + 2);
                            break;
                        case OpcodeBytes.POP_PSW:
                            Registers.A = ReadMemory(StackPointer + 1);
                            Flags.SetFromByte(ReadMemory(StackPointer));
                            StackPointer = (UInt16)(StackPointer + 2);
                            break;

                    #endregion

                    #region DAD - Double (16-bit) add

                        case OpcodeBytes.DAD_B:
                            ExecuteDAD(Registers.BC);
                            break;
                        case OpcodeBytes.DAD_D:
                            ExecuteDAD(Registers.DE);
                            break;
                        case OpcodeBytes.DAD_H:
                            ExecuteDAD(Registers.HL);
                            break;
                        case OpcodeBytes.DAD_SP:
                            ExecuteDAD(StackPointer);
                            break;

                    #endregion

                    // Load SP from H and L
                    case OpcodeBytes.SPHL:
                        StackPointer = Registers.HL;
                        break;

                    // Exchange stack
                    //  L <-> (SP); H <-> (SP+1)
                    case OpcodeBytes.XTHL:
                    {
                        var oldL = Registers.L;
                        var oldH = Registers.H;
                        Registers.L = ReadMemory(StackPointer);
                        WriteMemory(StackPointer, oldL);
                        Registers.H = ReadMemory(StackPointer+1);
                        WriteMemory(StackPointer+1, oldH);
                        break;
                    }

                    // Exchange registers
                    // H <-> D; L <-> E
                    case OpcodeBytes.XCHG:
                    {
                        var oldH = Registers.H;
                        var oldL = Registers.L;
                        Registers.H = Registers.D;
                        Registers.D = oldH;
                        Registers.L = Registers.E;
                        Registers.E = oldL;
                        break;
                    }

                #endregion

                #region Immediate instructions

                    #region MVI - Move immediate data

                        case OpcodeBytes.MVI_B:
                            Registers.B = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.MVI_C:
                            Registers.C = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.MVI_D:
                            Registers.D = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.MVI_E:
                            Registers.E = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.MVI_H:
                            Registers.H = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.MVI_L:
                            Registers.L = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.MVI_M:
                            WriteMemory(Registers.HL, ReadMemory(ProgramCounter + 1));
                            break;
                        case OpcodeBytes.MVI_A:
                            Registers.A = ReadMemory(ProgramCounter + 1);
                            break;

                    #endregion

                    #region LXI - Load register pair immediate

                        case OpcodeBytes.LXI_B:
                            Registers.B = ReadMemory(ProgramCounter + 2);
                            Registers.C = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LXI_D:
                            Registers.D = ReadMemory(ProgramCounter + 2);
                            Registers.E = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LXI_H:
                            Registers.H = ReadMemory(ProgramCounter + 2);
                            Registers.L = ReadMemory(ProgramCounter + 1);
                            break;
                        case OpcodeBytes.LXI_SP:
                        {
                            var upper = ReadMemory(ProgramCounter + 2) << 8;
                            var lower = ReadMemory(ProgramCounter + 1);
                            var address = upper | lower;
                            StackPointer = (UInt16)address;
                            break;
                        }

                    #endregion

                    // Add immediate to accumulator
                    // A <- A + byte
                    case OpcodeBytes.ADI:
                        ExecuteADD(ReadMemory(ProgramCounter+1));
                        break;

                    // Add immediate to accumulator with carry
                    // A <- A + data + CY
                    case OpcodeBytes.ACI:
                        ExecuteADD(ReadMemory(ProgramCounter+1), true);
                        break;

                    // Subtract immediate from accumulator
                    // A <- A - data
                    case OpcodeBytes.SUI:
                        ExecuteSUB(ReadMemory(ProgramCounter+1));
                        break;

                    // Subtract immediate from accumulator with borrow
                    // A <- A - data - CY
                    case OpcodeBytes.SBI:
                        ExecuteSUB(ReadMemory(ProgramCounter+1), true);
                        break;

                    // Logical AND immediate with accumulator
                    // A <- A & data
                    case OpcodeBytes.ANI:
                        Registers.A = (byte)(Registers.A & ReadMemory(ProgramCounter+1));
                        SetFlags(false, Registers.A);
                        break;

                    // XOR immediate with accumulator
                    // A <- A ^ data
                    case OpcodeBytes.XRI:
                        Registers.A = (byte)(Registers.A ^ ReadMemory(ProgramCounter+1));
                        SetFlags(false, Registers.A);
                        break;

                    // Logical OR immediate with accumulator
                    // A <- A | data
                    case OpcodeBytes.ORI:
                        Registers.A = (byte)(Registers.A | ReadMemory(ProgramCounter+1));
                        SetFlags(false, Registers.A);
                        break;

                    // Compare immediate with accumulator
                    // A - data
                    case OpcodeBytes.CPI:
                        ExecuteSUB(ReadMemory(ProgramCounter+1), false, false);
                        break;

                #endregion

                #region Direct addressing instructions

                    // Store accumulator direct
                    case OpcodeBytes.STA:
                    {
                        var upper = ReadMemory(ProgramCounter + 2) << 8;
                        var lower = ReadMemory(ProgramCounter + 1);
                        var address = upper | lower;
                        WriteMemory(address, Registers.A);
                        break;
                    }

                    // Load accumulator direct
                    case OpcodeBytes.LDA:
                    {
                        var upper = ReadMemory(ProgramCounter + 2) << 8;
                        var lower = ReadMemory(ProgramCounter + 1);
                        var address = upper | lower;
                        Registers.A = ReadMemory(address);
                        break;
                    }

                    // Store H and L direct
                    case OpcodeBytes.SHLD:
                    {
                        var upper = ReadMemory(ProgramCounter + 2) << 8;
                        var lower = ReadMemory(ProgramCounter + 1);
                        var address = upper | lower;
                        WriteMemory(address, Registers.L);
                        WriteMemory(address + 1, Registers.H);
                        break;
                    }

                    // Load H and L direct
                    case OpcodeBytes.LHLD:
                    {
                        var upper = ReadMemory(ProgramCounter + 2) << 8;
                        var lower = ReadMemory(ProgramCounter + 1);
                        var address = upper | lower;
                        Registers.L = ReadMemory(address);
                        Registers.H = ReadMemory(address + 1);
                        break;
                    }

                #endregion

                #region Jump instructions

                    // Load program counter
                    case OpcodeBytes.PCHL:
                        ExecuteJMP(Registers.HL);
                        incrementProgramCounter = false;
                        break;

                    // Jump
                    case OpcodeBytes.JMP:
                    // Jump (duplicate)
                    case OpcodeBytes.JMP2:
                    {
                        ExecuteJMP();

                        // Don't increment the program counter because we just updated it to
                        // the given address.
                        incrementProgramCounter = false;

                        break;
                    }

                    // Jump if parity odd
                    case OpcodeBytes.JPO:
                    {
                        if (!Flags.Parity)
                        {
                            ExecuteJMP();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if parity even
                    case OpcodeBytes.JPE:
                    {
                        if (Flags.Parity)
                        {
                            ExecuteJMP();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if plus/positive
                    case OpcodeBytes.JP:
                    {
                        if (!Flags.Sign)
                        {
                            ExecuteJMP();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if zero
                    case OpcodeBytes.JZ:
                    {
                        if (Flags.Zero)
                        {
                            ExecuteJMP();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if not zero
                    case OpcodeBytes.JNZ:
                    {
                        if (!Flags.Zero)
                        {
                            ExecuteJMP();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if not carry
                    case OpcodeBytes.JNC:
                    {
                        if (!Flags.Carry)
                        {
                            ExecuteJMP();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if carry
                    case OpcodeBytes.JC:
                    {
                        if (Flags.Carry)
                        {
                            ExecuteJMP();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                    // Jump if minus/negative
                    case OpcodeBytes.JM:
                    {
                        if (Flags.Sign)
                        {
                            ExecuteJMP();
                            incrementProgramCounter = false;
                        }
                        break;
                    }

                #endregion

                #region Call subroutine instructions

                    case OpcodeBytes.CALL:
                    case OpcodeBytes.CALL2:
                    case OpcodeBytes.CALL3:
                    case OpcodeBytes.CALL4:
                    {
                        ExecuteCALL(opcode);

                        // Don't increment the program counter because we just updated it to
                        // the given address.
                        incrementProgramCounter = false;

                        break;
                    }

                    // Call if minus/negative
                    case OpcodeBytes.CM:
                    {
                        if (Flags.Sign)
                        {
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if party even
                    case OpcodeBytes.CPE:
                    {
                        if (Flags.Parity)
                        {
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if carry
                    case OpcodeBytes.CC:
                    {
                        if (Flags.Carry)
                        {
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if zero
                    case OpcodeBytes.CZ:
                    {
                        if (Flags.Zero)
                        {
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if plus/positive
                    case OpcodeBytes.CP:
                    {
                        if (!Flags.Sign)
                        {
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if party odd
                    case OpcodeBytes.CPO:
                    {
                        if (!Flags.Parity)
                        {
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if no carry
                    case OpcodeBytes.CNC:
                    {
                        if (!Flags.Carry)
                        {
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Call if not zero
                    case OpcodeBytes.CNZ:
                    {
                        if (!Flags.Zero)
                        {
                            ExecuteCALL(opcode);
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                #endregion

                #region Return from subroutine instructions

                    // Return from subroutine
                    case OpcodeBytes.RET:
                    // Return from subroutine (duplicate)
                    case OpcodeBytes.RET2:
                    {
                        ExecuteRET();

                        // Don't increment the program counter because we just updated it to
                        // the given address.
                        incrementProgramCounter = false;

                        break;
                    }

                    // Return if not zero
                    case OpcodeBytes.RNZ:
                    {
                        if (!Flags.Zero)
                        {
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if zero
                    case OpcodeBytes.RZ:
                    {
                        if (Flags.Zero)
                        {
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if no carry
                    case OpcodeBytes.RNC:
                    {
                        if (!Flags.Carry)
                        {
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if carry
                    case OpcodeBytes.RC:
                    {
                        if (Flags.Carry)
                        {
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if parity odd
                    case OpcodeBytes.RPO:
                    {
                        if (!Flags.Parity)
                        {
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if parity even
                    case OpcodeBytes.RPE:
                    {
                        if (Flags.Parity)
                        {
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if plus/positive
                    case OpcodeBytes.RP:
                    {
                        if (!Flags.Sign)
                        {
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                    // Return if minus/negative
                    case OpcodeBytes.RM:
                    {
                        if (Flags.Sign)
                        {
                            ExecuteRET();
                            incrementProgramCounter = false;
                        }
                        else
                            useAlternateCycleCount = true;

                        break;
                    }

                #endregion

                #region Restart (interrupt handlers) instructions

                    case OpcodeBytes.RST_0:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0000, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_1:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0008, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_2:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0010, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_3:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0018, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_4:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0020, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_5:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0028, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_6:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0030, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                    case OpcodeBytes.RST_7:
                    {
                        var returnAddress = (UInt16)(ProgramCounter + opcode.Size);
                        ExecuteCALL(0x0038, returnAddress);
                        incrementProgramCounter = false;
                        break;
                    }

                #endregion

                #region Interrupt flip-flop instructions

                    // Enable interrupts
                    case OpcodeBytes.EI:
                        InterruptsEnabled = true;
                        break;

                    // Disable interrupts
                    case OpcodeBytes.DI:
                        InterruptsEnabled = false;
                        break;

                #endregion

                #region Input/Output Instructions

                    // Output accumulator to given device number
                    case OpcodeBytes.OUT:
                    {
                        if (OnDeviceWrite != null)
                            OnDeviceWrite(ReadMemory(ProgramCounter + 1), Registers.A);

                        break;
                    }

                    // Retrieve input from given device number and populate accumulator
                    case OpcodeBytes.IN:
                    {
                        if (OnDeviceRead != null)
                            Registers.A = OnDeviceRead(ReadMemory(ProgramCounter + 1));

                        break;
                    }

                #endregion

                default:
                    throw new NotImplementedException(String.Format("Attempted to execute unknown opcode 0x{0:X2} at memory address 0x{0:X4}", opcode, ProgramCounter));
            }

            #endregion

            // Determine how many cycles the instruction took.

            var elapsedCycles = (UInt16)opcode.Cycles;

            if (useAlternateCycleCount)
            {
                // Sanity check; if this fails an opcode definition or implementation is invalid.
                if (opcode.AlternateCycles == null)
                    throw new Exception(String.Format("The implementation for opcode 0x{0:X2} at memory address 0x{0:X4} indicated the alternate number of cycles should be used, but was not defined.", opcode, ProgramCounter));

                elapsedCycles = (UInt16)opcode.AlternateCycles;
            }

            // Increment the program counter.
            if (incrementProgramCounter)
               ProgramCounter += (UInt16)opcode.Size;

            return elapsedCycles;
        }

        #endregion

        #region Opcodes - Additional Implementations

        private void ExecuteDAA()
        {
            // Decimal Adjust Accumulator
            // This occurs in two steps below. Step descriptions are taken directly
            // from the Z80 Programmers Manual. See the manual for more details on
            // the instruction and how it can be used.

            // Step 1:
            // If the least significant four bits of the accumulator represents a number
            // greater than 9, or if the Auxiliary Carry bit is equal to one, the accumulator
            // is incremented by six. Otherwise, no incrementing occurs.

            // If a carry out of the least significant four bits occurs during Step (1),
            // the Auxiliary Carry bit is set; otherwise it is reset.

            var newAuxCarryValue = false;

            int lsb = Registers.A & 0x0F;

            if (lsb > 9 || Flags.AuxCarry)
            {
                Registers.A += 6;
                newAuxCarryValue = lsb >= 9;
            }

            // Step 2:
            // If the most significant four bits of the accumulator now represent a number
            // greater than 9, or if the normal carry bit is equal to one, the most significant
            // four bits of the accumulator are incremented by six. Otherwise, no incrementing occurs.

            // If a carry out of the most significant four bits occurs during Step (2). the normal
            // Carry bit is set; otherwise, it is unaffected.

            var newCarryValue = false;

            int msb = (Registers.A & 0xF0) >> 4;

            if (msb > 9 || Flags.Carry)
            {
                newCarryValue = msb >= 9;
                msb += 6;

                var newValue = ((msb << 4) & 0xF0) | (Registers.A & 0x0F);
                Registers.A = (byte)newValue;
            }

            // Update the condition flags.
            SetFlags(newCarryValue, Registers.A, newAuxCarryValue);
        }

        private void ExecuteJMP()
        {
            var upper = ReadMemory(ProgramCounter + 2) << 8;
            var lower = ReadMemory(ProgramCounter + 1);
            var address = (UInt16)(upper | lower);

            ExecuteJMP(address);
        }

        private void ExecuteJMP(UInt16 address)
        {
            #region CPU Diagnostics Debugging Mode

            // This is special case code only for running the CPU diagnostic program.
            // See the EnableDiagnosticsMode flag for more details.
            /* TODO: This was for the Intel 8080 test program; remove?
            if (Config.EnableDiagnosticsMode && address == 0x00)
            {
                // This is a CALL 0x00 which returns control to CP/M.
                this.Finished = true;
                return;
            }
            */

            #endregion

            ProgramCounter = address;
        }

        private void ExecuteCALL(Opcode opcode)
        {
            var returnAddress = (UInt16)(ProgramCounter + opcode.Size);

            var upper = ReadMemory(ProgramCounter + 2) << 8;
            var lower = ReadMemory(ProgramCounter + 1);
            var address = (UInt16)(upper | lower);

            ExecuteCALL(address, returnAddress);
        }

        private void ExecuteCALL(UInt16 address, UInt16 returnAddress)
        {
            // We need to break this into two bytes so we can push it onto the stack.
            var returnAddressUpper = (byte)((returnAddress & 0xFF00) >> 8);
            var returnAddressLower = (byte)(returnAddress & 0x00FF);

            // Push the return address onto the stack.
            WriteMemory(StackPointer - 1, returnAddressUpper);
            WriteMemory(StackPointer - 2, returnAddressLower);
            StackPointer--;
            StackPointer--;

            #region CPU Diagnostics Debugging Mode

            // This is special case code only for running the CPU diagnostic program.
            // See the EnableDiagnosticsMode flag for more details.

            /* TODO: This was for the Intel 8080 test program; remove?
            if (Config.EnableDiagnosticsMode && address == 0x05)
            {
                // This is a CALL 0x05 which is a CP/M call.
                // Register C is a flag, a value of 9 indicates a string print using
                // the value of register pair DE as a pointer to the string to print
                // which is terminated by the $ character. If register C is a value
                // of 2 it is a character print based on the value of register A.
                if (OnCPUDiagDebugEvent != null)
                    OnCPUDiagDebugEvent(Registers.C);

                ExecuteRET();
                return;
            }
            */

            #endregion

            // Jump to the given address.
            ProgramCounter = address;
        }

        private void ExecuteRET()
        {
            // Pop the return address off of the stack.
            var returnAddressUpper = ReadMemory(StackPointer + 1) << 8;
            var returnAddressLower = ReadMemory(StackPointer);
            var returnAddress = (UInt16)(returnAddressUpper | returnAddressLower);
            StackPointer++;
            StackPointer++;

            ProgramCounter = returnAddress;
        }

        private void ExecuteMOV(Register dest, Register source)
        {
            Registers[dest] = Registers[source];
        }

        // private void ExecuteMOVFromRegisterToMemory(Register source)
        // {
        //     var address = Registers.HL;

        //     // Determine if we should allow the write to memory based on the address
        //     // if the configuration has specified a restricted writeable range.
        //     var enforceWriteBoundsCheck = Config.WriteableMemoryStart != 0 && Config.WriteableMemoryEnd != 0;
        //     var allowWrite = true;

        //     if (enforceWriteBoundsCheck)
        //         allowWrite = address >= Config.WriteableMemoryStart && address <= Config.WriteableMemoryEnd;

        //     if (allowWrite)
        //         WriteMemory(address, Registers[source]);
        //     else
        //     {
        //         var programCounterFormatted = String.Format("0x{0:X4}", ProgramCounter);
        //         var addressFormatted = String.Format("0x{0:X4}", address);
        //         var startAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryStart);
        //         var endAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryEnd);
        //         throw new Exception($"Illegal memory address ({addressFormatted}) specified for 'MOV M, {source}' operation at address {programCounterFormatted}; expected address to be between {startAddressFormatted} and {endAddressFormatted} inclusive.");
        //     }
        // }

        // private void ExecuteMOVIToMemory(byte data)
        // {
        //     var address = Registers.HL;

        //     // Determine if we should allow the write to memory based on the address
        //     // if the configuration has specified a restricted writeable range.
        //     var enforceWriteBoundsCheck = Config.WriteableMemoryStart != 0 && Config.WriteableMemoryEnd != 0;
        //     var allowWrite = true;

        //     if (enforceWriteBoundsCheck)
        //         allowWrite = address >= Config.WriteableMemoryStart && address <= Config.WriteableMemoryEnd;

        //     if (allowWrite)
        //         WriteMemory(address, data);
        //     else
        //     {
        //         var programCounterFormatted = String.Format("0x{0:X4}", ProgramCounter);
        //         var addressFormatted = String.Format("0x{0:X4}", address);
        //         var startAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryStart);
        //         var endAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryEnd);
        //         throw new Exception($"Illegal memory address ({addressFormatted}) specified for 'MVI M, d8' operation at address {programCounterFormatted}; expected address to be between {startAddressFormatted} and {endAddressFormatted} inclusive.");
        //     }
        // }

        private void ExecuteADD(byte value, bool addCarryFlag = false)
        {
            var result = Registers.A + value;

            if (addCarryFlag && Flags.Carry)
                result += 1;

            var carryOccurred = result > 255;

            if (carryOccurred)
                result = result - 256;

            SetFlags(carryOccurred, (byte)result);

            Registers.A = (byte)result;
        }

        private void ExecuteSUB(byte value, bool subtractCarryFlag = false, bool updateAccumulator = true)
        {
            var borrowOccurred = (subtractCarryFlag && Flags.Carry)
                ? value >= Registers.A // Account for the extra minus one from the carry flag subtraction.
                : value > Registers.A;

            var result = Registers.A - value;

            if (subtractCarryFlag && Flags.Carry)
                result -= 1;

            if (borrowOccurred)
                result = 256 + result;

            SetFlags(borrowOccurred, (byte)result);

            if (updateAccumulator)
                Registers.A = (byte)result;
        }

        private void ExecuteDAD(UInt16 value)
        {
            var result = Registers.HL + value;

            var carryOccurred = result > 65535;

            if (carryOccurred)
                result = result - 65536;

            Registers.HL = (UInt16)result;

            Flags.Carry = carryOccurred;
        }

        /**
         * Encapsulates the rotate accumulator left/right (RRC and RLC) and the rotate
         * accumulator left/right though carry (RAL and RAR) instruction behavior. The Intel
         * Z80 programmers manual has excellent examples and diagrams of each instruction.
         */
        private void ExecuteRotateAccumulator(bool left, bool rotateThroughCarry = false)
        {
            var previousHighOrderBitSet = (Registers.A & 0x80) == 0x80;
            var previousLowOrderBitSet = (Registers.A & 0x01) == 0x01;
            var previousCarryFlagSet = Flags.Carry;

            int result = Registers.A;

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

            Registers.A = (byte)result;
        }

        #endregion

        #region Utilities

        private void SetFlags(bool carry, byte result, bool auxCarry = false)
        {
            Flags.Carry = carry;
            Flags.Zero = result == 0;
            Flags.Sign = (result & 0b10000000) == 0b10000000;
            Flags.Parity = CalculateParityBit((byte)result);
            Flags.AuxCarry = auxCarry;
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
                var programCounterFormatted = String.Format("0x{0:X4}", ProgramCounter);
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
                var programCounterFormatted = String.Format("0x{0:X4}", ProgramCounter);
                var addressFormatted = String.Format("0x{0:X4}", address);
                var startAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryStart);
                var endAddressFormatted = String.Format("0x{0:X4}", Config.WriteableMemoryEnd);
                var mirrorEndAddressFormatted = String.Format("0x{0:X4}", Config.MirrorMemoryEnd);
                throw new Exception($"Illegal memory address ({addressFormatted}) specified for write memory operation at address {programCounterFormatted}; expected address to be between {startAddressFormatted} and {(mirroringEnabled ? mirrorEndAddressFormatted : endAddressFormatted)} inclusive.");
            }
        }

        #endregion
    }
}
