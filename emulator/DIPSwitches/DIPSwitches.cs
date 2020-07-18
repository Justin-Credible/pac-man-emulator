
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

        // TODO: Unit tests.
        public byte GetByte()
        {
            var coinsPerGame = (byte)CoinsPerGame; // Bits 0-1
            var livesPerGame = (byte)((byte)LivesPerGame << 2); // Bits 2-3
            var bonusScorePerExtraLife = (byte)((byte)BonusScorePerExtraLife << 4); // Bits 4-5
            var difficulty = (byte)((byte)Difficulty << 6); // Bit 6
            var ghostNames = (byte)((byte)GhostNames << 7); // Bit 7

            return (byte)(coinsPerGame | livesPerGame | bonusScorePerExtraLife | difficulty | ghostNames);
        }
    }
}
