
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using JustinCredible.Z80Disassembler;
using JustinCredible.ZilogZ80;

namespace JustinCredible.PacEmu
{
    /**
     * An implementation of the Pac-Man game hardware for emulation; this includes the
     * Zilog Z80 CPU instance, TODO hardware, interrupts, debugger, and hardware loop.
     */
    public class PacManPCB : IMemory
    {
        #region Constants

        // The Zilog Z80 for the Pac-Man hardware is clocked at 3.072MHz.
        private const int CPU_MHZ = 3072000;

        // While the resolution is indeed 288x224, note that the monitor for this
        // game is portrait, not landscape. It is rotated -90 degrees (counterclockwise)
        // in the cabinet and therefore the resolution viewable to the user will be 224x256.
        // The framebuffer will need to be rotated before it is displayed to the end user.
        public const int RESOLUTION_WIDTH = 288;
        public const int RESOLUTION_HEIGHT = 224;

        // TODO
        // The frame buffer is 256 x 224, which is 57,344 pixels. Since the display is only
        // black and white, we only need one bit per pixel. Therefore we need 57,344 / 8
        // => 7,168 bytes or 7 KB for the frame buffer. This is pulled from the video RAM
        // portion which is at $2400-$3fff.
        // private const int FRAME_BUFFER_SIZE = 1024 * 7;

        #endregion

        #region Events

        // Fired when a frame is ready to be rendered.
        public delegate void RenderEvent(RenderEventArgs e);
        public event RenderEvent OnRender;
        private RenderEventArgs _renderEventArgs;

        // Fired when a sound effect should be played.
        // public delegate void SoundEvent(SoundEventArgs e);
        // public event SoundEvent OnSound;
        // private SoundEventArgs _soundEventArgs;

        #endregion

        #region Hardware

        // The thread on which we'll run the hardware emulation loop.
        private Thread _thread;

        // Indicates if a stop was requested via the Stop() method. Used to break out of the hardware
        // loop in the thread and stop execution.
        private bool _cancelled = false;

        // The configuration of the Zilog Z80 CPU specifically for the Pac-Man hardware.
        private static readonly CPUConfig _cpuConfig = new CPUConfig()
        {
            Registers = new CPURegisters()
            {
                PC = 0x0000,

                // TODO: Hardcoded to the top of the RAM.
                // Is this different for each game that runs on the Pac-Man hardware?
                SP = 0x4FEF,
            },

            // Interrupts are initially disabled, and will be enabled by the program ROM when ready.
            InterruptsEnabled = false,

            EnableDiagnosticsMode = false,
        };

        // Zilog Z80
        private CPU _cpu;

        // The game's video hardware runs at 60hz. It generates an interrupts @ 60hz (VBLANK).To simulate
        // this, we'll calculate the number of cycles we're expecting between each of these interrupts.
        // While this is not entirely accurate, it is close enough for the game to run as expected.
        private double _cyclesPerInterrupt = Math.Floor(Convert.ToDouble(CPU_MHZ / 60));
        private int _cyclesSinceLastInterrupt = 0;

        // To keep the emulated CPU from running too fast, we use a stopwatch and count cycles.
        private Stopwatch _cpuStopWatch = new Stopwatch();
        private int _cycleCount = 0;

        // Holds the last data written by the CPU to ports 0, which is used by the VBLANK interrupt.
        // Interrupt mode 2 uses this as the lower 8 bits with the I register as the upper 8 bits
        // to build a jump vector. Pac-Man's game code sets this to control where the code jumps to
        // after a VBLANK interrupt. See CPU::StepMaskableInterrupt() for more details.
        private byte _port0WriteLastData = 0x00;

        #endregion

        #region Memory Implementation

        /**
         * The first 20KB of address space is ROM & RAM:
         * 
         * 0000 - 3FFF     16 KB       Game ROM (4 x 1K ROMs)
         * 4000 - 43FF     1 KB        Video RAM (tile information)
         * 4400 - 47FF     1 KB        Video RAM (tile palettes)
         * 4800 - 4FEF     2032 B      RAM (just short of 1KB)
         * 4FF0 - 4FFF     16 B        Sprite #, x/y flip bits
         *
         * 5000 - 50FF                 Memory mapped registers
         */
        private byte[] _memory = null;

        public byte Read(int address)
        {
            if (address >= 0x0000 && address <= 0x4FFF)
            {
                // Includes ROM, Video RAM, RAM, and sprites.
                return _memory[address];
            }
            else if (address >= 0x5000 && address <= 0x503F)
            {
                // IN0 (joystick and coin slot) (each byte returns same value)
                // TODO: Pass joystick/button state here.
                return 0x00;
            }
            else if (address == 0x5003)
            {
                // Flip screen (bit 0: 0 = normal, 1 = flipped)
                // TODO: If supporting the cocktail mode screen flip dip switch.
                return 0x00;
            }
            else if (address == 0x5004)
            {
                // 1 player start lamp (bit 0: 0 = on, 1 = off)
                // I don't believe the Pac-Man arcade cabinet had support for blinking start buttons.
                // Other games on the same platform might though.
                return 0x01;
            }
            else if (address == 0x5005)
            {
                // 2 player start lamp (bit 0: 0 = on, 1 = off)
                // I don't believe the Pac-Man arcade cabinet had support for blinking start buttons.
                // Other games on the same platform might though.
                return 0x01;
            }
            else if (address == 0x5006)
            {
                // Coin lockout (bit 0: 0 = unlocked, 1 = locked)
                // I don't believe the Pac-Man arcade cabinet had a coin lockout mechanism.
                return 0x00;
            }
            else if (address == 0x5007)
            {
                // Coin counter (trigger by changing bit 0 from 0 to 1)
                return 0x00;
            }
            else if (address >= 0x5040 && address <= 0x507F)
            {
                // IN1 (joystick and start buttons) (each byte returns same value)
                // TODO: Pass joystick/button state here.
                return 0x00;
            }
            else if (address >= 0x5080 && address <= 0x50BF)
            {
                // Dip Switch Settings (each byte returns same value)
                // TODO: Pass dip switch values here.
                return 0x00;
            }
            else if (address < 0x00)
            {
                throw new Exception(String.Format("Invalid read memory address (< 0x0000): 0x{0:X4}", address));
            }
            else
            {
                // TODO: I may need to remove and/or relax this restriction. Adding an exception for now
                // so I can troubleshoot while getting things running.
                throw new Exception(String.Format("Unexpected read memory address: 0x{0:X4}", address));
            }
        }

        public ushort Read16(int address)
        {
            var lower = Read(address);
            var upper = Read(address + 1) << 8;
            return (UInt16)(upper | lower);
        }

        public void Write(int address, byte value)
        {
            if (address >= 0x0000 && address <= 0x3FFF)
            {
                // Prevent writes to ROM address space.
                // TODO: I may need to remove and/or relax this restriction (a no-op perhaps?). Adding
                // an exception for now so I can troubleshoot while getting things running.
                throw new Exception(String.Format("Unexpected write to ROM region (0x0000 - 0x3FFF): {0:X4}", address));
            }
            else if (address >= 0x4000 && address <= 0x4FFF)
            {
                // Video RAM, RAM, and sprites.
                _memory[address] = value;
            }
            else if (address == 0x5000)
            {
                // Interrupt enable (bit 0: 0 = disabled, 1 = enabled)
                _cpu.InterruptsEnabled = (value & 0x01) == 0x01;
            }
            else if (address == 0x5001)
            {
                // Sound enable (bit 0: 0 = disabled, 1 = enabled)

                // TODO: Implement.
                // soundEnabled = (value & 0x01) == 0x01;;
            }
            else if (address == 0x5002)
            {
                // ??? Aux board enable?

                // TODO: May need to remove/relax this sanity check.
                if (value != 0x00 && value != 0x01)
                    throw new Exception(String.Format("Unexpected value when writing to memory address location 0x5000 (aux board enable) with value: {0:X2}. Expected either 0x00 (disabled) or 0x01 (enabled).", value));

                // TODO: Implement?
                // ??? = value == 0x01;
            }
            else if (address == 0x5003)
            {
                // Flip screen (bit 0: 0 = normal, 1 = flipped)
                // TODO: If supporting the cocktail mode screen flip dip switch.
                return; // no-op
            }
            else if (address == 0x5004)
            {
                // 1 player start lamp (bit 0: 0 = on, 1 = off)
                // I don't believe the Pac-Man arcade cabinet had support for blinking start buttons.
                // Other games on the same platform might though.
                return; // no-op
            }
            else if (address == 0x5005)
            {
                // 2 player start lamp (bit 0: 0 = on, 1 = off)
                // I don't believe the Pac-Man arcade cabinet had support for blinking start buttons.
                // Other games on the same platform might though.
                return; // no-op
            }
            else if (address == 0x5006)
            {
                // Coin lockout (bit 0: 0 = unlocked, 1 = locked)
                // I don't believe the Pac-Man arcade cabinet had a coin lockout mechanism.
                return; // no-op
            }
            else if (address == 0x5007)
            {
                // Coin counter (trigger by changing bit 0 from 0 to 1)
                return; // no-op
            }
            else if (address >= 0x5040 && address <= 0x5045)
            {
                // Sound voice 1
                // 5040-5044 – accumulator (low nibbles, used by H/W only)
                // 5045 – waveform (low nibble)
                // TODO: Implement.
                return; // no-op
            }
            else if (address >= 0x5046 && address <= 0x504A)
            {
                // Sound voice 2, laid out like voice 1, missing low accumulator nibble
                // TODO: Implement.
                return; // no-op
            }
            else if (address >= 0x504B && address <= 0x504F)
            {
                // Sound voice 3, laid out like voice 1, missing low accumulator nibble
                // TODO: Implement.
                return; // no-op
            }
            else if (address >= 0x5050 && address <= 0x5054)
            {
                // Voice 1 frequency (low nibbles)
                // TODO: Implement.
                return; // no-op
            }
            else if (address == 0x5055)
            {
                // Voice 1 volume (low nibble)
                // TODO: Implement.
                return; // no-op
            }
            else if (address >= 0x5056 && address <= 0x505A)
            {
                // Voice 2 frequency and volume, laid out like voice 1
                // TODO: Implement.
                return; // no-op
            }
            else if (address >= 0x505B && address <= 0x505F)
            {
                // Voice 3 frequency and volume, laid out like voice 1
                // TODO: Implement.
                return; // no-op
            }
            else if (address >= 0x5060 && address <= 0x506F)
            {
                // Sprite x, y coordinates
                // TODO: Implement.
                return; // no-op
            }
            else if (address >= 0x50C0 && address <= 0x50FF)
            {
                // Watchdog reset (each byte has the same function)
                // TODO: Implement.
                return; // no-op
            }
            else
            {
                // TODO: I may need to remove and/or relax this restriction. Adding an exception for now
                // so I can troubleshoot while getting things running.
                throw new Exception(String.Format("Unexpected write to memory address: 0x{0:X4} with value: 0x{1:X2}", address, value));
            }
        }

        public void Write16(int address, ushort value)
        {
            var lower = (byte)(value & 0x00FF);
            var upper = (byte)((value & 0xFF00) >> 8);
            Write(address, lower);
            Write(address + 1, upper);
        }

        #endregion

        #region Dip Switches

        // public StartingShipsSetting StartingShips { get; set; } = StartingShipsSetting.Three;
        // public ExtraShipAtSetting ExtraShipAt { get; set; } = ExtraShipAtSetting.Points1000;

        #endregion

        #region Button State

        public bool ButtonP1Left { get; set; } = false;
        public bool ButtonP1Right { get; set; } = false;
        public bool ButtonP1Up { get; set; } = false;
        public bool ButtonP1Down { get; set; } = false;
        public bool ButtonP2Left { get; set; } = false;
        public bool ButtonP2Right { get; set; } = false;
        public bool ButtonP2Up { get; set; } = false;
        public bool ButtonP2Down { get; set; } = false;
        public bool ButtonStart1P { get; set; } = false;
        public bool ButtonStart2P { get; set; } = false;
        public bool ButtonCredit { get; set; } = false;

        #endregion

        #region Debugging Features

        private static readonly int MAX_ADDRESS_HISTORY = 100;
        private static readonly int MAX_REWIND_HISTORY = 20;

        private int _totalCycles = 0;
        private int _totalSteps = 0;

        /**
         * Enables debugging statistics and features.
         */
        public bool Debug { get; set; } = false;

        /**
         * When Debug=true, stores the last MAX_ADDRESS_HISTORY values of the program counter.
         */
        private List<UInt16> _addressHistory = new List<UInt16>();

        /**
         * When Debug=true, the program will break at these addresses and allow the user to perform
         * interactive debugging via the console.
         */
        public List<UInt16> BreakAtAddresses { get; set; }

        /**
         * When Debug=true, allows for single reverse-stepping in the interactive debugging console.
         */
        public bool RewindEnabled { get; set; } = false;

        /**
         * When Debug=true and RewindEnabled=true, stores sufficient state of the CPU and emulator
         * to allow for stepping backwards to each state of the system.
         */
        private List<EmulatorState> _executionHistory = new List<EmulatorState>();

        /**
         * Indicates if we're stingle stepping through opcodes/instructions using the interactive
         * debugger when Debug=true.
         */
        private bool _singleStepping = false;

        /**
         * For use by the interactive debugger when Debug=true. If true, indicates that the disassembly
         * should be annotated with the values in the Annotations dictionary. If false, the diassembler
         * will annotate each line with a pseudocode comment instead.
         */
        private bool _showAnnotatedDisassembly = false;

        /**
         * The annotations to be used when Debug=true and _showAnnotatedDisassembly=true. It is a map
         * of memory addresses to string annotation values.
         */
        public Dictionary<UInt16, String> Annotations { get; set; }

        #endregion

        #region Public Methods

        /**
         * Used to start execution of the CPU with the given ROM and optional emulator state.
         * The emulator's hardware loop will run on a spereate thread, and therefore, this method
         * is non-blocking.
         */
        public void Start(ROMData romData, EmulatorState state = null)
        {
            if (_thread != null)
                throw new Exception("Emulator cannot be started because it was already running.");

            if (romData == null || romData.Data == null || romData.Data.Count == 0)
                throw new Exception("romData is required.");

            // Save off the ROM data for use by the video and sound hardware. Only the code ROMs are mapped
            // into the CPU's address space; the other ROMs are accessed directly by other hardware.
            // TODO: Pass relevant data into the video/sound hardware instead of saving a reference?
            // _romData = romData;

            _cyclesSinceLastInterrupt = 0;

            _cpu = new CPU(_cpuConfig);

            _cpu.OnDeviceRead += CPU_OnDeviceRead;
            _cpu.OnDeviceWrite += CPU_OnDeviceWrite;

            // Fetch the ROM data; we trust the contents were validated with a CRC32 check elsewhere, but
            // since the CRC check can be bypassed, we at least need to ensure the file sizes are correct
            // since this classes' implementation of IMemory is expecting certain addreses.

            var codeRom1 = romData.Data[ROMs.PAC_MAN_CODE_1.FileName];
            var codeRom2 = romData.Data[ROMs.PAC_MAN_CODE_2.FileName];
            var codeRom3 = romData.Data[ROMs.PAC_MAN_CODE_3.FileName];
            var codeRom4 = romData.Data[ROMs.PAC_MAN_CODE_4.FileName];

            if (codeRom1.Length != 4096 || codeRom2.Length != 4096 || codeRom3.Length != 4096 || codeRom4.Length != 4096)
                throw new Exception("All code ROMs must be exactly 4KB in size.");

            // Define our addressable memory space, which includes the game code ROMS and RAM.

            var addressableMemorySize =
                codeRom1.Length     // Code ROM 1
                + codeRom2.Length   // Code ROM 2
                + codeRom3.Length   // Code ROM 3
                + codeRom4.Length   // Code ROM 4
                + 1024              // Video RAM (tile information)
                + 1024              // Video RAM (tile palettes)
                + 2032              // RAM
                + 16;               // Sprite numbers

            _memory = new byte[addressableMemorySize];

            // Map the code ROM into the lower 16K of the memory space.
            Array.Copy(codeRom1, 0, _memory, 0, codeRom1.Length);
            Array.Copy(codeRom2, 0, _memory, codeRom1.Length, codeRom2.Length);
            Array.Copy(codeRom3, 0, _memory, codeRom1.Length + codeRom2.Length, codeRom3.Length);
            Array.Copy(codeRom4, 0, _memory, codeRom1.Length + codeRom2.Length + codeRom3.Length, codeRom4.Length);

            // This class implements the IMemory interface, which the CPU needs to determine how to read and
            // write data. We set the reference to this class instance (whose implementation uses _memory).
            _cpu.Memory = this;

            _renderEventArgs = new RenderEventArgs()
            {
                ShouldRender = false,

                // TODO: This is a placeholder for now; the FrameBuffer will need to be a bitmap or similar.
                FrameBuffer = new byte[RESOLUTION_WIDTH * RESOLUTION_HEIGHT],
            };

            // TODO: Initialize emulated video and sound hardware here?

            if (state != null)
                LoadState(state);

            _cancelled = false;
            _thread = new Thread(new ThreadStart(HardwareLoop));
            _thread.Name = "Emulator: Hardware Loop";
            _thread.Start();
        }

        /**
         * Used to stop execution of the CPU and halt the thread.
         */
        public void Stop()
        {
            if (_thread == null)
                throw new Exception("Emulator cannot be stopped because it wasn't running.");

            _cancelled = true;
        }

        /**
         * Used to stop CPU execution and enable single stepping through opcodes via the interactive
         * debugger (only when Debug = true).
         */
        public void Break()
        {
            if (Debug)
                _singleStepping = true;
        }

        #endregion

        #region CPU Event Handlers

        /**
         * Used to handle the CPU's IN instruction; read value from given device ID.
         */
        private byte CPU_OnDeviceRead(int deviceID)
        {
            // I don't believe the Pac-Man game code reads from any external devices.
            switch (deviceID)
            {
                default:
                    Console.WriteLine($"WARNING: An IN/Read for port {deviceID} is not implemented.");
                    return 0x00;
            }
        }

        /**
         * Used to handle the CPU's OUT instruction; write value to given device ID.
         */
        private void CPU_OnDeviceWrite(int deviceID, byte data)
        {
            switch (deviceID)
            {
                // The Pac-Man game code writes data to port zero, which is used as the lower 8 bits of an
                // address when using interrupt mode 2 (IM2). We save this value so we can use it when triggering
                // the VBLANK interrupt.
                case 0x00:
                    _port0WriteLastData = data;
                    break;

                default:
                    Console.WriteLine($"WARNING: An OUT/Write for port {deviceID} (value: {data}) is not implemented.");
                    break;
            }
        }

        #endregion

        #region Private Methods: Hardware Loop

        /**
         * Handles stepping the CPU to execute instructions as well as firing interrupts.
         */
        private void HardwareLoop()
        {
            _cpuStopWatch.Start();
            _cycleCount = 0;

            try
            {
                while (!_cancelled)
                {
                    // Handle all the debug tasks that need to happen before we execute an instruction.
                    if (Debug)
                        HandleDebugFeaturesPreStep();

                    // Step the CPU to execute the next instruction.
                    var cycles = _cpu.Step();

                    // Keep track of the number of cycles to see if we need to throttle the CPU.
                    _cycleCount += cycles;

                    // Handle all the debug tasks that need to happen after we execute an instruction.
                    if (Debug)
                        HandleDebugFeaturesPostStep(cycles);

                    // Throttle the CPU emulation if needed.
                    if (_cycleCount >= (CPU_MHZ/60))
                    {
                        _cpuStopWatch.Stop();

                        if (_cpuStopWatch.Elapsed.TotalMilliseconds < 16.6)
                        {
                            var sleepForMs = 16.6 - _cpuStopWatch.Elapsed.TotalMilliseconds;

                            if (sleepForMs >= 1)
                                System.Threading.Thread.Sleep((int)sleepForMs);
                        }

                        _cycleCount = 0;
                        _cpuStopWatch.Restart();
                    }

                    // See if it's time to fire a CPU interrupt or not.
                    HandleInterrupts(cycles);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("An exception occurred during emulation!");
                PrintDebugSummary(_showAnnotatedDisassembly);
                Console.WriteLine("-------------------------------------------------------------------");
                throw exception;
            }

            _cpu = null;
            _thread = null;
        }

        /**
         * Pac-Man sends a single maskable interrupt, which is driven by the video hardware.
         * We can use the number of CPU cycles elapsed to roughtly estimate when this interrupt
         * should fire which is roughtly 60hz (also known as vblank; when the electron beam reaches
         * end of the screen).
         */
        private void HandleInterrupts(int cyclesElapsed)
        {
            // Keep track of the number of cycles since the last interrupt occurred.
            _cyclesSinceLastInterrupt += cyclesElapsed;

            // Determine if it's time for the video hardware to fire an interrupt.
            if (_cyclesSinceLastInterrupt < _cyclesPerInterrupt)
                return;

            // If interrupts are enabled, then handle them, otherwise do nothing.
            if (_cpu.InterruptsEnabled)
            {
                // If we're going to run an interrupt handler, ensure interrupts are disabled.
                // This ensures we don't interrupt the interrupt handler. The program ROM will
                // re-enable the interrupts manually.
                // TODO: Is this comment and behavior still accurate for the Z80?
                _cpu.InterruptsEnabled = false;

                // Execute the handler for the interrupt.
                _cpu.StepMaskableInterrupt(_port0WriteLastData);

                // CRT electron beam reached the end (V-Blank).
                if (OnRender != null)
                {
                    // TODO: Delegate to emulated video rendering hardware here to generate a bitmap...
                    // OR pass along the already rendered bitmap? Is this the correct point at which to
                    // generate the entire bitmap/framebuffer??
                    // Array.Copy(_cpu.Memory, 0x2400, _renderEventArgs.FrameBuffer, 0, FRAME_BUFFER_SIZE);
                    OnRender(_renderEventArgs);
                }
            }

            // Reset the count so we can count up again.
            _cyclesSinceLastInterrupt = 0;
        }

        /**
         * This method handles all the work that needs to be done when debugging is enabled right before
         * the CPU executes an opcode. This includes recording CPU stats, address history, and CPU breakpoints,
         * as well as implements the interactive debugger.
         */
        private void HandleDebugFeaturesPreStep()
        {
            // Record the current address.

            _addressHistory.Add(_cpu.Registers.PC);

            if (_addressHistory.Count >= MAX_ADDRESS_HISTORY)
                _addressHistory.RemoveAt(0);

            // See if we need to break based on a given address.
            if (BreakAtAddresses != null && BreakAtAddresses.Contains(_cpu.Registers.PC))
                _singleStepping = true;

            // If we need to break, print out the CPU state and wait for a keypress.
            if (_singleStepping)
            {
                // Print debug information and wait for user input via the console (key press).
                while (true)
                {
                    Console.WriteLine("-------------------------------------------------------------------");
                    PrintDebugSummary(_showAnnotatedDisassembly);

                    var rewindPrompt = "";

                    if (RewindEnabled && _executionHistory.Count > 0)
                        rewindPrompt = "F9 = Step Backward    ";

                    Console.WriteLine($"  F1 = Save State    F2 = Load State    F4 = Edit Breakpoints");
                    Console.WriteLine($"  F5 = Continue    {rewindPrompt}F10 = Step");
                    Console.WriteLine("  F11 = Toggle Annotated Disassembly    F12 = Print Last 30 Opcodes");
                    var key = Console.ReadKey(); // Blocking

                    // Handle console input.
                    if (key.Key == ConsoleKey.F1) // Save State
                    {
                        var state = SaveState();
                        var json = JsonSerializer.Serialize<EmulatorState>(state);

                        Console.WriteLine(" Enter file name/path to write save state...");
                        var filename = Console.ReadLine();

                        File.WriteAllText(filename, json);

                        Console.WriteLine("  State Saved!");
                    }
                    else if (key.Key == ConsoleKey.F2) // Load State
                    {
                        Console.WriteLine(" Enter file name/path to read save state...");
                        var filename = Console.ReadLine();

                        var json = File.ReadAllText(filename);

                        var state = JsonSerializer.Deserialize<EmulatorState>(json);
                        LoadState(state);

                        Console.WriteLine("  State Loaded!");
                    }
                    else if (key.Key == ConsoleKey.F4) // Edit Breakpoints
                    {
                        if (BreakAtAddresses == null)
                            BreakAtAddresses = new List<ushort>();

                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Current break point addressess:");

                            if (BreakAtAddresses.Count == 0)
                            {
                                Console.Write(" (none)");
                            }
                            else
                            {
                                foreach (var breakAtAddress in BreakAtAddresses)
                                    Console.WriteLine(String.Format(" • 0x{0:X4}", breakAtAddress));
                            }

                            Console.WriteLine("  Enter an address to toggle breakpoint (e.g. '0x1234<ENTER>') or leave empty and press <ENTER> to stop editing breakpoints...");
                            var addressString = Console.ReadLine();

                            if (String.IsNullOrWhiteSpace(addressString))
                                break; // Break out of input loop

                            var address = Convert.ToUInt16(addressString, 16);

                            if (BreakAtAddresses.Contains(address))
                                BreakAtAddresses.Remove(address);
                            else
                                BreakAtAddresses.Add(address);
                        }
                    }
                    else if (key.Key == ConsoleKey.F5) // Continue
                    {
                        _singleStepping = false;
                        break; // Break out of input loop
                    }
                    else if (RewindEnabled && _executionHistory.Count > 0 && key.Key == ConsoleKey.F9) // Step Backward
                    {
                        var state = _executionHistory[_executionHistory.Count - 1];
                        _executionHistory.RemoveAt(_executionHistory.Count - 1);

                        LoadState(state);
                        _cyclesSinceLastInterrupt -= state.LastCyclesExecuted.Value;
                    }
                    else if (key.Key == ConsoleKey.F10) // Step
                    {
                        break; // Break out of input loop
                    }
                    else if (key.Key == ConsoleKey.F11) // Toggle Annotated Disassembly
                    {
                        _showAnnotatedDisassembly = !_showAnnotatedDisassembly;
                    }
                    else if (key.Key == ConsoleKey.F12) // Print List 10 Opcodes
                    {
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine(" Last 30 Opcodes: ");
                        Console.WriteLine();
                        PrintRecentInstructions(30);
                    }
                }
            }
        }

        /**
         * This method handles all the work that needs to be done when debugging is enabled right after
         * the CPU executes an opcode. This includes recording CPU stats and rewind history.
         */
        private void HandleDebugFeaturesPostStep(int cyclesElapsed)
        {
            // Keep track of the total number of steps (instructions) and cycles ellapsed.
            _totalSteps++;
            _totalCycles += cyclesElapsed;

            // Used to slow down the emulation to watch the renderer.
            // if (_totalCycles % 1000 == 0)
            //     System.Threading.Thread.Sleep(10);

            if (RewindEnabled)
            {
                if (_executionHistory.Count >= MAX_REWIND_HISTORY)
                    _executionHistory.RemoveAt(0);

                var state = SaveState();
                state.LastCyclesExecuted = cyclesElapsed;

                _executionHistory.Add(state);
            }
        }

        #endregion

        #region Private Methods: Save/Load State

        /**
         * Used to dump the state of the CPU and all fields needed to restore the emulator's
         * state in order to continue at this execution point later on.
         */
        private EmulatorState SaveState()
        {
            return new EmulatorState()
            {
                // TODO: Why not just pass the instance through to be serialized? Risk missing a field here.
                Registers = new CPURegisters()
                {
                    A = _cpu.Registers.A,
                    B = _cpu.Registers.B,
                    C = _cpu.Registers.C,
                    D = _cpu.Registers.D,
                    E = _cpu.Registers.E,
                    H = _cpu.Registers.H,
                    L = _cpu.Registers.L,
                    Shadow_A = _cpu.Registers.Shadow_A,
                    Shadow_B = _cpu.Registers.Shadow_B,
                    Shadow_C = _cpu.Registers.Shadow_C,
                    Shadow_D = _cpu.Registers.Shadow_D,
                    Shadow_E = _cpu.Registers.Shadow_E,
                    Shadow_H = _cpu.Registers.Shadow_H,
                    Shadow_L = _cpu.Registers.Shadow_L,
                    I = _cpu.Registers.I,
                    R = _cpu.Registers.R,
                    IX = _cpu.Registers.IX,
                    IY = _cpu.Registers.IY,
                    PC = _cpu.Registers.PC,
                    SP = _cpu.Registers.SP,
                },
                Flags = new ConditionFlags()
                {
                    Zero = _cpu.Flags.Zero,
                    Sign = _cpu.Flags.Sign,
                    ParityOverflow = _cpu.Flags.ParityOverflow,
                    Carry = _cpu.Flags.Carry,
                    HalfCarry = _cpu.Flags.HalfCarry,
                    Shadow = _cpu.Flags.Shadow,
                },
                InterruptsEnabled = _cpu.InterruptsEnabled,
                InterruptMode = _cpu.InterruptMode,
                Memory = _memory,
                TotalCycles = _totalCycles,
                TotalSteps = _totalSteps,
                CyclesSinceLastInterrupt = _cyclesSinceLastInterrupt,
            };
        }

        /**
         * Used to restore the state of the CPU and restore all fields to allow the emulator
         * to continue execution from a previously saved state.
         */
        private void LoadState(EmulatorState state)
        {
            _cpu.Registers = state.Registers;
            _cpu.Flags = state.Flags;
            _cpu.InterruptsEnabled = state.InterruptsEnabled;
            _cpu.InterruptMode = state.InterruptMode;
            _memory = state.Memory;
            _totalCycles = state.TotalCycles;
            _totalSteps = state.TotalSteps;
            _cyclesSinceLastInterrupt = state.CyclesSinceLastInterrupt;
        }

        #endregion

        #region Private Methods: Debugging & Diagnostics

        private void PrintDebugSummary(bool showAnnotatedDisassembly = false)
        {
            Console.WriteLine("-------------------------------------------------------------------");

            if (Debug)
                Console.WriteLine($" Total Steps/Opcodes: {_totalSteps}\tCPU Cycles: {_totalCycles}");

            Console.WriteLine();
            _cpu.PrintDebugSummary();
            Console.WriteLine();
            PrintCurrentExecution(showAnnotatedDisassembly);
            Console.WriteLine();
        }

        /**
         * Prints last n instructions that were executed up to MAX_ADDRESS_HISTORY.
         * Useful when a debugger is attached. Only works when Debug is true.
         */
        private void PrintRecentInstructions(int count = 10)
        {
            if (!Debug)
                return;

            var output = new StringBuilder();

            if (count > _addressHistory.Count)
                count = _addressHistory.Count;

            var startIndex = _addressHistory.Count - count;

            for (var i = startIndex; i < _addressHistory.Count; i++)
            {
                var address = _addressHistory[i];

                // Edge case for being able to print instruction history when we've jumped outside
                // of the allowable memory locations.
                if (address >= _memory.Length)
                {
                    var addressDisplay = String.Format("0x{0:X4}", address);
                    output.AppendLine($"[IndexOutOfRange: {addressDisplay}]");
                    continue;
                }

                var instruction = Disassembler.Disassemble(_cpu.Memory, address, out _, true, true);
                output.AppendLine(instruction);
            }

            Console.WriteLine(output.ToString());
        }

        /**
         * Used to print the disassembly of memory locations before and after the given address.
         * Useful when a debugger is attached.
         */
        private void PrintMemory(UInt16 address, bool annotate = false, int beforeCount = 10, int afterCount = 10)
        {
            var output = new StringBuilder();

            // Ensure the start and end locations are within range.
            // TODO: Should probably use the IMemory implementation here... but if this is just for looking at
            // disassembly of the ROM regions, direct usage of _memory should be okay, at least for now.
            var start = (address - beforeCount < 0) ? 0 : (address - beforeCount);
            var end = (address + afterCount >= _memory.Length) ? _memory.Length - 1 : (address + afterCount);

            for (var i = start; i < end; i++)
            {
                var addressIndex = (UInt16)i;

                // If this is the current address location, add an arrow pointing to it.
                output.Append(address == addressIndex ? "---->\t" : "\t");

                // If we're showing annotations, then don't show the pseudocode.
                var emitPseudocode = !_showAnnotatedDisassembly;

                // Disasemble the opcode and print it.
                var instruction = Disassembler.Disassemble(_cpu.Memory, addressIndex, out int instructionLength, true, emitPseudocode);
                output.Append(instruction);

                // If we're showing annotations, attempt to look up the annotation for this address.
                if (_showAnnotatedDisassembly && Annotations != null)
                {
                    var annotation = Annotations.ContainsKey(addressIndex) ? Annotations[addressIndex] : null;
                    output.Append("\t\t; ");
                    output.Append(annotation == null ? "???" : annotation);
                }

                output.AppendLine();

                // If the opcode is larger than a single byte, we don't want to print subsequent
                // bytes as opcodes, so here we print the next address locations as the byte value
                // in parentheses, and then increment so we can skip disassembly of the data.
                if (instructionLength == 3)
                {
                    var upper = _cpu.Memory.Read(addressIndex + 2) << 8;
                    var lower = _cpu.Memory.Read(addressIndex + 1);
                    var combined = (UInt16)(upper | lower);
                    var dataFormatted = String.Format("0x{0:X4}", combined);
                    var address1Formatted = String.Format("0x{0:X4}", addressIndex+1);
                    var address2Formatted = String.Format("0x{0:X4}", addressIndex+2);
                    output.AppendLine($"\t{address1Formatted}\t(D16: {dataFormatted})");
                    output.AppendLine($"\t{address2Formatted}\t");
                    i += 2;
                }
                else if (instructionLength == 2)
                {
                    var dataFormatted = String.Format("0x{0:X2}", _cpu.Memory.Read(addressIndex+1));
                    var addressFormatted = String.Format("0x{0:X4}", addressIndex+1);
                    output.AppendLine($"\t{addressFormatted}\t(D8: {dataFormatted})");
                    i++;
                }
            }

            Console.WriteLine(output.ToString());
        }

        /**
         * Used to print the disassembly of the memory locations around where the program counter is pointing.
         * Useful when a debugger is attached.
         */
        private void PrintCurrentExecution(bool annotate = false, int beforeCount = 10, int afterCount = 10)
        {
            PrintMemory(_cpu.Registers.PC, annotate, beforeCount, afterCount);
        }

        #endregion
    }
}
