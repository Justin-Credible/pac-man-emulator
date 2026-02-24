
using System;

namespace JustinCredible.PacEmu
{
    /**
     * An implementation of the Pac-Man game audio hardware: Namco Waveform Sound Generator 3 (WSG3)
     * for emulation.
     */
    public class AudioHardware
    {
        private byte[] _soundROMs = null;
        private ROMSet _romset = ROMSet.PacMan;

        public byte Voice1Waveform { get; set; } = 0x00;
        public byte[] Voice1Frequency { get; set; } = new byte[5] { 0x00, 0x00, 0x00, 0x00, 0x00, };
        public byte Voice1Volume { get; set; } = 0x00;
        private int Voice1Accumulator { get; set; } = 0x00000; // 20-bit

        public byte Voice2Waveform { get; set; } = 0x00;
        public byte[] Voice2Frequency { get; set; } = new byte[4] { 0x00, 0x00, 0x00, 0x00, };
        public byte Voice2Volume { get; set; } = 0x00;
        private int Voice2Accumulator { get; set; } = 0x0000; // 16-bit

        public byte Voice3Waveform { get; set; } = 0x00;
        public byte[] Voice3Frequency { get; set; } = new byte[4] { 0x00, 0x00, 0x00, 0x00, };
        public byte Voice3Volume { get; set; } = 0x00;
        private int Voice3Accumulator { get; set; } = 0x0000; // 16-bit

        public AudioHardware(ROMData romData, ROMSet romset = ROMSet.PacMan)
        {
            if (romset != ROMSet.PacMan && romset != ROMSet.MsPacMan)
                throw new ArgumentException($"Unexpected ROM set: {romset}");

            _romset = romset;

            var rom1 = romData.Data[ROMs.PAC_MAN_SOUND_1.FileName];
            var rom2 = romData.Data[ROMs.PAC_MAN_SOUND_2.FileName];

            _soundROMs = new byte[512];
            Array.Copy(rom1, 0, _soundROMs, 0, 256);
            Array.Copy(rom2, 0, _soundROMs, 256, 256);
        }

        /**
         * To be called for each clock cycle of the chip. For Pac-Man this should be at 96 kHz.
         * This handles updating the internal registers and creating the audio samples for each
         * of the three voices. It returns three bytes, each a 8-bit, signed audio sample.
         */
        public byte[] Tick()
        {
            // First, lets convert the frequency bytes into the 20-bit or 16-bit values from the array
            // of bytes that the game code sets; each byte only uses the lower nibble, so we need to
            // extract those and combine them to get the full value for each.

            var v1Freq = ((Voice1Frequency[0] & 0x0F) << 0)
                | ((Voice1Frequency[1] & 0x0F) << 4)
                | ((Voice1Frequency[2] & 0x0F) << 8)
                | ((Voice1Frequency[3] & 0x0F) << 12)
                | ((Voice1Frequency[4] & 0x0F) << 16);

            var v2Freq = ((Voice2Frequency[0] & 0x0F) << 0)
                | ((Voice2Frequency[1] & 0x0F) << 4)
                | ((Voice2Frequency[2] & 0x0F) << 8)
                | ((Voice2Frequency[3] & 0x0F) << 12);

            var v3Freq = ((Voice3Frequency[0] & 0x0F) << 0)
                | ((Voice3Frequency[1] & 0x0F) << 4)
                | ((Voice3Frequency[2] & 0x0F) << 8)
                | ((Voice3Frequency[3] & 0x0F) << 12);

            // At each clock and for each voice the hardware performs the following steps:

            // 1. Add the 20-bit (or 16-bit) voice frequency to the 20-bit (or 16-bit) accumulator.

            Voice1Accumulator = (0xFFFFF) & (Voice1Accumulator + v1Freq);
            Voice2Accumulator = (0xFFFF) & (Voice2Accumulator + v2Freq);
            Voice3Accumulator = (0xFFFF) & (Voice3Accumulator + v3Freq);

            // 2. Use the waveform 0-7 to lookup a 32-byte sample in the Sound ROM.

            var v1Sample = new byte[32];
            Array.Copy(_soundROMs, (0x07 & Voice1Waveform) * 32, v1Sample, 0, 32);

            var v2Sample = new byte[32];
            Array.Copy(_soundROMs, (0x07 & Voice2Waveform) * 32, v2Sample, 0, 32);

            var v3Sample = new byte[32];
            Array.Copy(_soundROMs, (0x07 & Voice3Waveform) * 32, v3Sample, 0, 32);

            // 3. Take the top 5 bits of the accumulator to look up a nibble 0-31 in that sample.

            var v1SampleNibble = v1Sample[(Voice1Accumulator & 0xF8000) >> 15];
            var v2SampleNibble = v2Sample[(Voice2Accumulator & 0xF800) >> 11];
            var v3SampleNibble = v3Sample[(Voice3Accumulator & 0xF800) >> 11];

            // 4. Multiply that nibble by the volume nibble 0-15.
            // The nibble is centered around 0 by subtracting 8 (range -8 to +7) before
            // multiplying, producing a signed sample stored in a byte (two's complement).

            v1SampleNibble = (byte)(((v1SampleNibble & 0x0F) - 8) * (Voice1Volume & 0x0F));
            v2SampleNibble = (byte)(((v2SampleNibble & 0x0F) - 8) * (Voice2Volume & 0x0F));
            v3SampleNibble = (byte)(((v3SampleNibble & 0x0F) - 8) * (Voice3Volume & 0x0F));

            // 5. Send the result to the amplifier for output.

            return new byte[3] { v1SampleNibble, v2SampleNibble, v3SampleNibble };
        }

        public AudioHardwareState SaveState()
        {
            return new AudioHardwareState()
            {
                Voice1Waveform = this.Voice1Waveform,
                Voice1Frequency = this.Voice1Frequency,
                Voice1Volume = this.Voice1Volume,
                Voice1Accumulator = this.Voice1Accumulator,

                Voice2Waveform = this.Voice2Waveform,
                Voice2Frequency = this.Voice2Frequency,
                Voice2Volume = this.Voice2Volume,
                Voice2Accumulator = this.Voice2Accumulator,

                Voice3Waveform = this.Voice3Waveform,
                Voice3Frequency = this.Voice3Frequency,
                Voice3Volume = this.Voice3Volume,
                Voice3Accumulator = this.Voice3Accumulator,
            };
        }

        public void LoadState(AudioHardwareState state)
        {
            Voice1Waveform = state.Voice1Waveform;
            Voice1Frequency = state.Voice1Frequency;
            Voice1Volume = state.Voice1Volume;
            Voice1Accumulator = state.Voice1Accumulator;

            Voice2Waveform = state.Voice2Waveform;
            Voice2Frequency = state.Voice2Frequency;
            Voice2Volume = state.Voice2Volume;
            Voice2Accumulator = state.Voice2Accumulator;

            Voice3Waveform = state.Voice3Waveform;
            Voice3Frequency = state.Voice3Frequency;
            Voice3Volume = state.Voice3Volume;
            Voice3Accumulator = state.Voice3Accumulator;
        }
    }
}
