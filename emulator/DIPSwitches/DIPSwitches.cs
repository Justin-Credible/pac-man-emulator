
namespace JustinCredible.PacEmu
{
    public class DIPSwitches
    {
        public CoinsPerGame CoinsPerGame { get; set; } = CoinsPerGame.OneCoinOneGame;
        public LivesPerGame LivesPerGame { get; set; } = LivesPerGame.Three;
        public BonusScorePerExtraLife BonusScorePerExtraLife { get; set; } = BonusScorePerExtraLife.TenThousand;
        public Difficulty Difficulty { get; set; } = Difficulty.Normal;
        public GhostNames GhostNames { get; set; } = GhostNames.Normal;
        public CabinetMode CabinetMode { get; set; } = CabinetMode.Upright;
    }
}
