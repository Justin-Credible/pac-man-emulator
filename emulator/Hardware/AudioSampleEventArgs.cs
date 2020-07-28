using System;

namespace JustinCredible.PacEmu
{
    public class AudioSampleEventArgs : EventArgs
    {
        /**
         * An array of audio samples to be played. Each entry in the array
         * is a group of three audio samples, one for each of the voices for
         * a given moment in time. Only the lower nibble of each byte is used.
         * The samples are 8-bit, signed.
         */
        public byte[][] Samples { get; set; }
    }
}
