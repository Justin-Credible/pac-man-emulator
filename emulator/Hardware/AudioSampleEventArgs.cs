using System;

namespace JustinCredible.PacEmu
{
    public class AudioSampleEventArgs : EventArgs
    {
        /**
         * An array of three audio samples, one for each of the voices.
         * Only the lower nibble of each byte is used. The samples are
         * 8-bit, signed.
         */
        public byte[] Samples { get; set; }
    }
}
