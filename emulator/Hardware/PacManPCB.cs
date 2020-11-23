
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
     * Zilog Z80 CPU instance, video & sound hardware, interrupts, debugger, and hardware loop.
     */
    public class PacManPCB : IMemory
    {
        // The thread on which we'll run the hardware emulation loop.
        private Thread _thread;

        // Indicates if a stop was requested via the Stop() method. Used to break out of the hardware
        // loop in the thread and stop execution.
        private bool _cancelled = false;

        // Indicates if writes should be allowed to the ROM addres space.
        public bool AllowWritableROM { get; set; } = false;

        // The ROM set the PCB should be configured for.
        public ROMSet ROMSet { get; set; } = ROMSet.PacMan;

        #region Events/Delegates

        // Fired when a frame is ready to be rendered.
        public delegate void RenderEvent(RenderEventArgs e);
        public event RenderEvent OnRender;
        private RenderEventArgs _renderEventArgs = new RenderEventArgs();

        // Fired when an audio sample should be played.
        public delegate void AudioSampleEvent(AudioSampleEventArgs e);
        public event AudioSampleEvent OnAudioSample;
        private AudioSampleEventArgs _audioSampleEventArgs = new AudioSampleEventArgs();

        // Fired when a breakpoint is hit and the CPU is paused (only when Debug = true).
        public delegate void BreakpointHitEvent();
        public event BreakpointHitEvent OnBreakpointHitEvent;

        #endregion

        #region Hardware: Components

        // The Zilog Z80 for the Pac-Man hardware is clocked at 3.072MHz.
        private const int CPU_MHZ = 3072000;

        // The Namcom Waveform Sound Generator (WSG3) runs at CPU_MHZ/32 = 96 kHz.
        private const int WSG3_MHZ = CPU_MHZ / 32;

        internal CPU _cpu; // Zilog Z80
        private VideoHardware _video;
        private AudioHardware _audio;
        public DIPSwitches DIPSwitchState { get; set; } = new DIPSwitches();
        public Buttons ButtonState { get; set; } = new Buttons();

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

        /**
         * Ms. Pac-Man is simply a Pac-Man PCB which an auxiliary daughterboard attached via
         * a ribbon cable to the original Z80 CPU slot. It is dormant until a 1 is written to
         * the 0x5002 address, at which point it becomes enabled. The daughterboard handles
         * intercepting reads/writes to certain addresses so it can patch areas of the original
         * Pac-Man ROM with jumps to the extra Ms. Pac-Man ROMs on the daughterboard.
         */
        private bool _auxBoardEnabled = false;
        private MsPacManAuxBoard _auxBoard = null;

        /**
         * Used to enable or disable all voices on the audio hardware.
         */
        private bool _soundEnabled = false;

        /**
         * Each pair of bytes is an (x, y) coordinate for each of the 8 hardware sprites.
         * These can be written to via the addresses 0x5060 - 0x506F and is used by the
         * video hardware to position the sprites on the screen.
         */
        private byte[] _spriteCoordinates = new byte[16];

        /**
         * Keeps track of if the screen should be flipped or not. This is used if the game is
         * configured for cocktail mode when it is the second player's turn. In upright mode
         * the screen will not be flipped.
         */
        private bool _flipScreen = false;

        #endregion

        #region Hardware: Interrupts

        // The game's video hardware runs at 60hz. It generates an interrupts @ 60hz (VBLANK).To simulate
        // this, we'll calculate the number of cycles we're expecting between each of these interrupts.
        // While this is not entirely accurate, it is close enough for the game to run as expected.
        private int _cyclesPerInterrupt = CPU_MHZ / 60;
        private int _cyclesSinceLastInterrupt = 0;

        // The game's WSG3 audio hardware runs at a CPU clock / 32. e.g. 3.072 MHz / 32 = 96 kHz
        // Ticking this clock at 96 kHz is too performance intensive and results in distorted audio.
        // Instead, we'll calculate the approximate number of samples per we need to generate per
        // "frame" assuming 60 Hz. e.g. (3.072 MHz / 60) / (3.072 MHz / 96 kHz) = 1600
        // Therefore we need to tick the audio hardware ~1600 times every 1/60 of a second (16.6 ms).
        private int _audioSamplesPerFrame = (CPU_MHZ / 60) / (CPU_MHZ / WSG3_MHZ);

        // To keep the emulated CPU from running too fast, we use a stopwatch and count cycles.
        private Stopwatch _cpuStopWatch = new Stopwatch();
        private int _cycleCount = 0;

        // Holds the last data written by the CPU to ports 0, which is used by the VBLANK interrupt.
        // Interrupt mode 2 uses this as the lower 8 bits with the I register as the upper 8 bits
        // to build a jump vector. Pac-Man's game code sets this to control where the code jumps to
        // after a VBLANK interrupt. See CPU::StepMaskableInterrupt() for more details.
        private byte _port0WriteLastData = 0x00;

        #endregion

        #region Debugging Features

        private static readonly int MAX_ADDRESS_HISTORY = 50;
        private static readonly int MAX_REVERSE_STEP_HISTORY = 20;

        internal long _totalCycles = 0;
        internal long _totalOpcodes = 0;

        /**
         * Enables debugging statistics and features.
         */
        public bool Debug { get; set; } = false;

        /**
         * When Debug=true, stores the last MAX_ADDRESS_HISTORY values of the program counter.
         */
        internal List<UInt16> _addressHistory = new List<UInt16>();

        /**
         * When Debug=true, the program will break at these addresses and allow the user to perform
         * interactive debugging via the console.
         */
        public List<UInt16> BreakAtAddresses { get; set; } = new List<ushort>();

        /**
         * When Debug=true, allows for single reverse-stepping in the interactive debugging console.
         */
        public bool ReverseStepEnabled { get; set; } = false;

        /**
         * When Debug=true and ReverseStepEnabled=true, stores sufficient state of the CPU and emulator
         * to allow for stepping backwards to each state of the system.
         */
        internal List<EmulatorState> _executionHistory = new List<EmulatorState>();

        /**
         * Indicates if the thread is in a busy/lazy wait for the user to submit a command via the
         * interactive debugger.
         */
        private bool _isWaitingForInteractiveDebugger = false;

        /**
         * Indicates if we're stingle stepping through opcodes/instructions using the interactive
         * debugger when Debug=true.
         */
        private bool _singleStepping = false;

        /**
         * The disassembly annotations to be used by the interactive debugger when Debug=true. It is
         * a map of memory addresses to string annotation values.
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

            if (ROMSet != ROMSet.PacMan && ROMSet != ROMSet.MsPacMan)
                throw new ArgumentException($"Unexpected ROM set: {ROMSet}");

            // The initial configuration of the CPU.
            var cpuConfig = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    PC = 0x0000,

                    // Hardcode the stackpointer to the top of the RAM.
                    // TODO: Is this different for each game that runs on the Pac-Man hardware?
                    SP = 0x4FEF,
                },

                // Interrupts are initially disabled, and will be enabled by the program ROM when ready.
                InterruptsEnabled = false,

                // Diagnostics is only for unit tests.
                EnableDiagnosticsMode = false,
            };

            // Initialize the CPU and subscribe to device events.
            _cpu = new CPU(cpuConfig);
            _cpu.OnDeviceRead += CPU_OnDeviceRead;
            _cpu.OnDeviceWrite += CPU_OnDeviceWrite;
            _cyclesSinceLastInterrupt = 0;

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

            // Load and decrypt the Ms. Pac-Man daughterboard ROMs and apply patches to the base Pac-Man ROMs.
            // The patches will be applied to a seperate memory instance because we still need the original,
            // unmodified Pac-Man code ROMs present to boot and pass the self-test. See PacManPCB::Read().
            if (ROMSet == ROMSet.MsPacMan)
            {
                _auxBoard = new MsPacManAuxBoard();
                _auxBoard.LoadAuxROMs(romData);
            }

            // This class implements the IMemory interface, which the CPU needs to determine how to read and
            // write data. We set the reference to this class instance (whose implementation uses _memory).
            _cpu.Memory = this;

            // Initialize video hardware.
            _video = new VideoHardware(romData, ROMSet);
            _video.Initialize();

            // Initialize audio hardware.
            _audio = new AudioHardware(romData, ROMSet);

            if (state != null)
                LoadState(state);

            _cancelled = false;
            _thread = new Thread(new ThreadStart(HardwareLoop));
            _thread.Name = "Pac-Man Hardware";
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
            if (!Debug)
                return;

            _singleStepping = true;

            if (OnBreakpointHitEvent != null)
            {
                _isWaitingForInteractiveDebugger = true;
                OnBreakpointHitEvent.Invoke();
            }
        }

        /**
         * Used to continue CPU execution (only when Debug = true).
         * If the user only wants to single step, true can be passed here.
         */
        public void Continue(bool singleStep = false)
        {
            if (!Debug || !_isWaitingForInteractiveDebugger)
                return;

            // Handle continue vs single step; if we're continuing then we want to release
            // the single step mode and continue until the next conditional breakpoint.
            if (!singleStep)
                _singleStepping = false;

            // Release the thread from busy/lazy waiting.
            _isWaitingForInteractiveDebugger = false;
        }

        /**
         * Used to reverse step backwards a single step, effectively reverting the CPU to the state
         * prior to the last opcode executing. This requires Debug=true and ReverseStepEnabled=true.
         */
        public void ReverseStep()
        {
            if (!Debug && !ReverseStepEnabled)
                throw new Exception("Debug feature: reverse stepping is not enabled.");

            var state = _executionHistory[_executionHistory.Count - 1];
            _executionHistory.RemoveAt(_executionHistory.Count - 1);

            // The first time we step backwards in the debugger, the most recent saved execution state
            // will be the same as the current state. In that case, pop again to get the last state.
            if (state.Registers.PC == _cpu.Registers.PC && _executionHistory.Count > 0)
            {
                state = _executionHistory[_executionHistory.Count - 1];
                _executionHistory.RemoveAt(_executionHistory.Count - 1);
            }

            LoadState(state);
            _cyclesSinceLastInterrupt -= state.LastCyclesExecuted.Value;
            OnBreakpointHitEvent.Invoke();
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

        #region Memory Read/Write Implementation (IMemory)

        public byte Read(int address)
        {
            // Special behavior for when the Ms. Pac-Man daughterboard is enabled.
            if (_auxBoardEnabled)
            {
                if (address <= 0x4000)
                {
                    // For all original ROM areas, read our patched version.
                    return _auxBoard.AuxROMs[address];
                }
                else if (address >= 0x8000 && address < 0x8800)
                {
                    // Aux ROM U5
                    return _auxBoard.AuxROMs[address - 0x8000 + 0x6000];
                }
                else if (address >= 0x8800 && address < 0xA000)
                {
                    // Aux ROM U6
                    return _auxBoard.AuxROMs[(address & 0xFFF) + 0x5000];
                }

                // else fall through to original memory read behaviors.
            }

            if (address >= 0x0000 && address <= 0x4FFF)
            {
                // Includes ROM, Video RAM, RAM, and sprites.
                return _memory[address];
            }
            else if (address >= 0x5000 && address <= 0x503F)
            {
                // IN0 (player 1 joystick, credit switches, and rack advance button) (each byte returns same value).
                return ButtonState.GetPortIN0();
            }
            else if (address == 0x5003)
            {
                // Flip screen (bit 0: 0 = normal, 1 = flipped)
                return (byte)(_flipScreen == true ? 1 : 0);
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
                // IN1 (player 2 joystick, start buttons, board test and cabinet mode switches) (each byte returns same value).
                // Note that bit 7 is the cabinet mode setting which comes from a DIP switch.
                var cabinetMode = (byte)((DIPSwitchState.CabinetMode == CabinetMode.Upright ? 1 : 0) << 7);
                return (byte)(ButtonState.GetPortIN1() | cabinetMode);
            }
            else if (address >= 0x5080 && address <= 0x50BF)
            {
                // Dip Switch Settings (each byte returns same value).
                return DIPSwitchState.GetByte();
            }
            else if (address < 0x00)
            {
                throw new IndexOutOfRangeException(String.Format("Invalid read memory address (< 0x0000): 0x{0:X4}", address));
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
                if (AllowWritableROM)
                    _memory[address] = value;
                else
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
                _soundEnabled = (value & 0x01) == 0x01;;
            }
            else if (address == 0x5002)
            {
                // When the Ms. Pac-Man auxiliary board is connected on Pac-Man hardware in the Z80 slot,
                // writing a 1 to this address will enable the additional functionality provided by the
                // board, which is special behavior when reading memory locations.

                if (ROMSet == ROMSet.MsPacMan)
                {
                    if (value != 0x00 && value != 0x01)
                        throw new Exception(String.Format("Unexpected value when writing to memory address location 0x5002 (aux board enable) with value: {0:X2}. Expected either 0x00 (disabled) or 0x01 (enabled).", value));

                    if ((value & 0x01) == 0x01)
                        _auxBoardEnabled = true;
                }
            }
            else if (address == 0x5003)
            {
                // Flip screen (bit 0: 0 = normal, 1 = flipped)
                _flipScreen = value == 1 ? true : false;
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

            else if (address >= 0x5040 && address <= 0x5044)
            {
                // Sound voice 1
                // 5040-5044 – accumulator (low nibbles, used by H/W only)
                if (value != 0)
                    throw new Exception("Not expecting game code to adjust voice 1 accumulator values to be set by game code.");
            }
            else if (address == 0x5045)
            {
                // Sound voice 1
                // 5045 – waveform (low nibble)
                _audio.Voice1Waveform = value;
                return;
            }

            else if (address >= 0x5046 && address <= 0x5049)
            {
                // Sound voice 2
                // 5046-5049 – accumulator (low nibbles, used by H/W only)
                if (value != 0)
                    throw new Exception("Not expecting game code to adjust voice 2 accumulator values to be set by game code.");
            }
            else if (address == 0x504A)
            {
                // Sound voice 1
                // 504A – waveform (low nibble)
                _audio.Voice2Waveform = value;
                return;
            }

            else if (address >= 0x504B && address <= 0x504E)
            {
                // Sound voice 3
                // 504B-504E – accumulator (low nibbles, used by H/W only)
                if (value != 0)
                    throw new Exception("Not expecting game code to adjust voice 3 accumulator values to be set by game code.");
            }
            else if (address == 0x504F)
            {
                // Sound voice 3
                // 504F – waveform (low nibble)
                _audio.Voice3Waveform = value;
                return;
            }

            else if (address == 0x5050)
            {
                // Voice 1 frequency (low nibble) byte 0
                _audio.Voice1Frequency[0] = value;
                return;
            }
            else if (address == 0x5051)
            {
                // Voice 1 frequency (low nibble) byte 1
                _audio.Voice1Frequency[1] = value;
                return;
            }
            else if (address == 0x5052)
            {
                // Voice 1 frequency (low nibble) byte 2
                _audio.Voice1Frequency[2] = value;
                return;
            }
            else if (address == 0x5053)
            {
                // Voice 1 frequency (low nibble) byte 3
                _audio.Voice1Frequency[3] = value;
                return;
            }
            else if (address == 0x5054)
            {
                // Voice 1 frequency (low nibble) byte 4
                _audio.Voice1Frequency[4] = value;
                return;
            }
            else if (address == 0x5055)
            {
                // Voice 1 volume (low nibble)
                _audio.Voice1Volume = value;
                return;
            }
            else if (address == 0x5056)
            {
                // Voice 2 frequency (low nibble) byte 0
                _audio.Voice2Frequency[0] = value;
                return;
            }
            else if (address == 0x5057)
            {
                // Voice 2 frequency (low nibble) byte 1
                _audio.Voice2Frequency[1] = value;
                return;
            }
            else if (address == 0x5058)
            {
                // Voice 2 frequency (low nibble) byte 2
                _audio.Voice2Frequency[2] = value;
                return;
            }
            else if (address == 0x5059)
            {
                // Voice 2 frequency (low nibble) byte 3
                _audio.Voice2Frequency[3] = value;
                return;
            }
            else if (address == 0x505A)
            {
                // Voice 2 volume (low nibble)
                _audio.Voice2Volume = value;
                return;
            }
            else if (address == 0x505B)
            {
                // Voice 3 frequency (low nibble) byte 0
                _audio.Voice3Frequency[0] = value;
                return;
            }
            else if (address == 0x505C)
            {
                // Voice 3 frequency (low nibble) byte 1
                _audio.Voice3Frequency[1] = value;
                return;
            }
            else if (address == 0x505D)
            {
                // Voice 3 frequency (low nibble) byte 2
                _audio.Voice3Frequency[2] = value;
                return;
            }
            else if (address == 0x505E)
            {
                // Voice 3 frequency (low nibble) byte 3
                _audio.Voice3Frequency[3] = value;
                return;
            }
            else if (address == 0x505F)
            {
                // Voice 3 volume (low nibble)
                _audio.Voice3Volume = value;
                return;
            }
            else if (address >= 0x5060 && address <= 0x506F)
            {
                // Sprite x, y coordinates.
                // Convert the address to fit in the byte array that holds the 16 coordinate values.
                _spriteCoordinates[address - 0x5060] = value;
            }
            else if (address >= 0x50C0 && address <= 0x50FF)
            {
                // Watchdog reset (each byte has the same function)
                // This would write values that the watchdog would look for to determine if the game
                // code had locked up or not. Since I'm not implementing the watchdog hardware I don't
                // need to implement this.
                return; // no-op
            }
            else
            {
                // Writing to any other locations will do nothing.
                // NOTE: A bunch of these writes occur during bootup / self-test.
                // Console.WriteLine(String.Format("Unexpected write to memory address: 0x{0:X4} with value: 0x{1:X2}", address, value));
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

        #region Private Methods: Hardware Loop

        /**
         * Handles stepping the CPU to execute instructions as well as firing interrupts.
         */
        private void HardwareLoop()
        {
            _cpuStopWatch.Start();
            _cycleCount = 0;

#if !DEBUG
            try
            {
#endif
                while (!_cancelled)
                {
                    // Handle all the debug tasks that need to happen before we execute an instruction.
                    if (Debug)
                    {
                        HandleDebugFeaturesPreStep();

                        // If the interactive debugger is active, wait for the user to single step, continue,
                        // or perform another operation.
                        while(_isWaitingForInteractiveDebugger)
                            Thread.Sleep(250);
                    }

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

                    // Fire a CPU interrupt if it's time to do so.
                    HandleInterrupts(cycles);
                }
#if !DEBUG
            }
            catch (Exception exception)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("An exception occurred during emulation!");
                _cpu.PrintDebugSummary();
                Console.WriteLine("-------------------------------------------------------------------");
                throw exception;
            }
#endif

            _cpu = null;
            _thread = null;
        }

        /**
         * Pac-Man sends a single maskable interrupt, which is driven by the video hardware.
         * We can use the number of CPU cycles elapsed to roughly estimate when this interrupt
         * should fire which is roughly 60hz (also known as vblank; when the electron beam reaches
         * end of the screen). Returns true if it was time to fire an interrupt and false otherwise.
         * Note that a true return value does not mean that an interrupt fired (interrupts can be
         * disabled), only that it was time to fire one.
         */
        private bool HandleInterrupts(int cyclesElapsed)
        {
            // Keep track of the number of cycles since the last interrupt occurred.
            _cyclesSinceLastInterrupt += cyclesElapsed;

            // Determine if it's time for the video hardware to fire an interrupt.
            if (_cyclesSinceLastInterrupt < _cyclesPerInterrupt)
                return false;

            // CRT electron beam reached the end (V-Blank).

            // If interrupts are enabled, then handle them, otherwise do nothing.
            if (_cpu.InterruptsEnabled)
            {
                // If we're going to run an interrupt handler, ensure interrupts are disabled.
                // This ensures we don't interrupt the interrupt handler. The program ROM will
                // re-enable the interrupts manually.
                _cpu.InterruptsEnabled = false;

                // Execute the handler for the interrupt.
                _cpu.StepMaskableInterrupt(_port0WriteLastData);

                // Every 1/60 of a second is a good time for us to generate a video frame as
                // well as all of the audio samples that need to be queued up to play.
                HandleRenderVideoFrame();
                HandleRenderAudioSamples();
            }

            // Reset the count so we can count up again.
            _cyclesSinceLastInterrupt = 0;

            return true;
        }

        private void HandleRenderVideoFrame()
        {
            // Render the screen into an image.
            var image = _video.Render(this, _spriteCoordinates, _flipScreen);

            // Convert the image into a bitmap format.

            byte[] bitmap = null;

            using (var steam = new MemoryStream())
            {
                image.Save(steam, new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder());
                bitmap = steam.ToArray();
            }

            // Delegate to the render event, passing the framebuffer to be rendered.
            _renderEventArgs.FrameBuffer = bitmap;
            OnRender?.Invoke(_renderEventArgs);
        }

        private void HandleRenderAudioSamples()
        {
            var samples = new byte[_audioSamplesPerFrame][];

            // Generate the number of audio samples that we need for a given "frame".
            for (var i = 0; i < _audioSamplesPerFrame; i++)
            {
                var sample = _audio.Tick();
                samples[i] = sample;
            }

            // Delegate to the event, passing the audio samples to be played.
            _audioSampleEventArgs.Samples = samples;
            OnAudioSample?.Invoke(_audioSampleEventArgs);
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
            if (BreakAtAddresses.Contains(_cpu.Registers.PC))
                _singleStepping = true;

            // If we need to break, print out the CPU state and wait for a keypress.
            if (_singleStepping)
            {
                Break();
                return;
            }
        }

        /**
         * This method handles all the work that needs to be done when debugging is enabled right after
         * the CPU executes an opcode. This includes recording CPU stats and reverse step history.
         */
        private void HandleDebugFeaturesPostStep(int cyclesElapsed)
        {
            // Keep track of the total number of steps (instructions) and cycles ellapsed.
            _totalOpcodes++;
            _totalCycles += cyclesElapsed;

            // Used to slow down the emulation to watch the renderer.
            // if (_totalCycles % 1000 == 0)
            //     System.Threading.Thread.Sleep(10);

            if (ReverseStepEnabled)
            {
                if (_executionHistory.Count >= MAX_REVERSE_STEP_HISTORY)
                    _executionHistory.RemoveAt(0);

                var state = SaveState();
                state.LastCyclesExecuted = cyclesElapsed;

                _executionHistory.Add(state);
            }
        }

        #endregion

        #region Public Methods: Save/Load State

        /**
         * Used to dump the state of the CPU and all fields needed to restore the emulator's
         * state in order to continue at this execution point later on.
         */
        public EmulatorState SaveState()
        {
            return new EmulatorState()
            {
                Registers = _cpu.Registers,
                Flags = _cpu.Flags,
                Halted = _cpu.Halted,
                InterruptsEnabled = _cpu.InterruptsEnabled,
                InterruptsEnabledPreviousValue = _cpu.InterruptsEnabledPreviousValue,
                InterruptMode = _cpu.InterruptMode,
                Memory = _memory,
                SpriteCoordinates = _spriteCoordinates,
                TotalCycles = _totalCycles,
                TotalOpcodes = _totalOpcodes,
                CyclesSinceLastInterrupt = _cyclesSinceLastInterrupt,
                AudioHardwareState = _audio.SaveState(),
            };
        }

        /**
         * Used to restore the state of the CPU and restore all fields to allow the emulator
         * to continue execution from a previously saved state.
         */
        public void LoadState(EmulatorState state)
        {
            _cpu.Registers = state.Registers;
            _cpu.Flags = state.Flags;
            _cpu.Halted = state.Halted;
            _cpu.InterruptsEnabled = state.InterruptsEnabled;
            _cpu.InterruptsEnabledPreviousValue = state.InterruptsEnabledPreviousValue;
            _cpu.InterruptMode = state.InterruptMode;
            _memory = state.Memory;
            _spriteCoordinates = state.SpriteCoordinates;
            _totalCycles = state.TotalCycles;
            _totalOpcodes = state.TotalOpcodes;
            _cyclesSinceLastInterrupt = state.CyclesSinceLastInterrupt;
            _audio.LoadState(state.AudioHardwareState);
        }

        #endregion
    }
}
