using System;
using System.Collections.Generic;

namespace JustinCredible.PacEmu
{
    public class EmulatorConfig
    {
        public string RomPath { get; set; }
        public ROMSet RomSet { get; set; }
        public string DipSwitchesConfigPath { get; set; }
        public string LoadStateFilePath { get; set; }
        public bool SkipChecksums { get; set; }
        public bool WritableRom { get; set; }
        public bool Debug { get; set; }
        public List<UInt16> Breakpoints { get; set; }
        public bool ReverseStep { get; set; }
        public string AnnotationsFilePath { get; set; }
    }
}
