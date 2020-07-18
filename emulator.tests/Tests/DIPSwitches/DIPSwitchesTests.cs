
using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace JustinCredible.PacEmu.Tests
{
    public class DIPSwitchesTests
    {
        [Theory]
        [InlineData(CoinsPerGame.FreePlay, LivesPerGame.One, BonusScorePerExtraLife.TenThousand, Difficulty.Hard, GhostNames.Alternate, 0b00000000)]
        [InlineData(CoinsPerGame.OneCoinOneGame, LivesPerGame.One, BonusScorePerExtraLife.TenThousand, Difficulty.Hard, GhostNames.Alternate, 0b00000001)]
        [InlineData(CoinsPerGame.OneCoinOneGame, LivesPerGame.Two, BonusScorePerExtraLife.TenThousand, Difficulty.Hard, GhostNames.Alternate, 0b00000101)]
        [InlineData(CoinsPerGame.OneCoinOneGame, LivesPerGame.Two, BonusScorePerExtraLife.FifteenThousand, Difficulty.Hard, GhostNames.Alternate, 0b00010101)]
        [InlineData(CoinsPerGame.OneCoinOneGame, LivesPerGame.Two, BonusScorePerExtraLife.FifteenThousand, Difficulty.Normal, GhostNames.Alternate, 0b01010101)]
        [InlineData(CoinsPerGame.OneCoinOneGame, LivesPerGame.Two, BonusScorePerExtraLife.FifteenThousand, Difficulty.Normal, GhostNames.Normal, 0b11010101)]
        [InlineData(CoinsPerGame.TwoCoinsOneGame, LivesPerGame.Five, BonusScorePerExtraLife.None, Difficulty.Normal, GhostNames.Normal, 0b11111111)]
        [InlineData(CoinsPerGame.TwoCoinsOneGame, LivesPerGame.Three, BonusScorePerExtraLife.None, Difficulty.Normal, GhostNames.Normal, 0b11111011)]
        [InlineData(CoinsPerGame.OneCoinTwoGames, LivesPerGame.Three, BonusScorePerExtraLife.TwentyThousand, Difficulty.Normal, GhostNames.Normal, 0b11101010)]
        public void TestGetByte(CoinsPerGame coinsPerGame, LivesPerGame livesPerGame, BonusScorePerExtraLife bonusScorePerExtraLife, Difficulty difficulty, GhostNames ghostNames, byte expectedByte)
        {
            var dipSwitches = new DIPSwitches()
            {
                CoinsPerGame = coinsPerGame,
                LivesPerGame = livesPerGame,
                BonusScorePerExtraLife = bonusScorePerExtraLife,
                Difficulty = difficulty,
                GhostNames = ghostNames,
            };

            Assert.Equal(expectedByte, dipSwitches.GetByte());
        }
    }
}
