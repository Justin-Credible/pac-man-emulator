using JustinCredible.ZilogZ80;

namespace JustinCredible.PacEmu
{
    public class EmulatorState
    {
        public CPURegisters Registers { get; set; }
        public ConditionFlags Flags { get; set; }
        public bool Halted { get; set; }
        public bool InterruptsEnabled { get; set; }
        public InterruptMode InterruptMode { get; set; }
        public byte[] Memory { get; set; }
        public byte[] SpriteCoordinates { get; set; }
        public int TotalCycles { get; set; }
        public int TotalSteps { get; set; }
        public int CyclesSinceLastInterrupt { get; set; }
        public AudioHardwareState AudioHardwareState { get; set; }

        public int? LastCyclesExecuted { get; set; }
    }
}
