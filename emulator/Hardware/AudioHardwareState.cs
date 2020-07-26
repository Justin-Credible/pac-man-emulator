
using System;

namespace JustinCredible.PacEmu
{
    /**
     * For keeping track of the state of the audio hardware's registers.
     */
    public class AudioHardwareState
    {
        public byte Voice1Waveform { get; set; }
        public byte[] Voice1Frequency { get; set; }
        public byte Voice1Volume { get; set; }
        public int Voice1Accumulator { get; set; }

        public byte Voice2Waveform { get; set; }
        public byte[] Voice2Frequency { get; set; }
        public byte Voice2Volume { get; set; }
        public int Voice2Accumulator { get; set; }

        public byte Voice3Waveform { get; set; }
        public byte[] Voice3Frequency { get; set; }
        public byte Voice3Volume { get; set; }
        public int Voice3Accumulator { get; set; }
    }
}
