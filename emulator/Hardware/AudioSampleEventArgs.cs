using System;

namespace JustinCredible.PacEmu
{
    public class AudioSampleEventArgs : EventArgs
    {
        /**
         * An array of audio samples to be played. Each entry in the array
         * is a group of three audio samples, one for each of the voices for
         * a given moment in time. The samples are 8-bit signed values.
         */
        public sbyte[][] Samples { get; set; }
    }
}
