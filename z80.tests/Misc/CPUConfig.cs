
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    /**
     * A convenience class that "overrides" references to the real CPUConfig
     * for all usages in the unit test namespace. This allows us to instantiate
     * an instance with property setters while still getting the defaults.
     */
    public class CPUConfig : JustinCredible.ZilogZ80.CPUConfig
    {
        public CPUConfig()
        {
            MemorySize = 16 * 1024;
            WriteableMemoryStart = 0x2000;
            WriteableMemoryEnd = 0x3FFF;
            MirrorMemoryStart = 0x4000;
            MirrorMemoryEnd = 0x5FFF;
            EnableDiagnosticsMode = false;
        }
    }
}
