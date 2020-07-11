using System.Drawing;
using Xunit;

namespace JustinCredible.PacEmu.Tests
{
    public class VideoHardwareTests
    {
        [Fact]
        public void InitializeColorsParsesRomColorsCorrectly()
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = VideoHardwareTestData.COLOR_ROM;
            romData.Data[ROMs.PAC_MAN_PALETTE.FileName] = null;
            romData.Data[ROMs.PAC_MAN_TILE.FileName] = null;
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = null;

            var video = new VideoHardware(romData);
            video.InitializeColors();

            // Expected RGB color values as listed in Chris Lomont's Pac-Man Emulation
            // Guide v0.1, page 5, table 3.
            var expectedColors = new Color[]
            {
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(255, 0, 0),
                Color.FromArgb(222, 151, 81),
                Color.FromArgb(255, 184, 255),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 255, 255),
                Color.FromArgb(71, 184, 255),
                Color.FromArgb(255, 184, 81),

                Color.FromArgb(0, 0, 0),
                Color.FromArgb(255, 255, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(33, 33, 255),
                Color.FromArgb(0, 255, 0),
                Color.FromArgb(71, 184, 174),
                Color.FromArgb(255, 184, 174),
                Color.FromArgb(222, 222, 255),

                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),

                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(0, 0, 0),
            };

            Assert.Equal(32, video._colors.Length);

            for (var i = 0; i < 32; i++)
            {
                Assert.True(expectedColors[i].R == video._colors[i].R, $"The Red value for color #{i} was expected to be {expectedColors[i].R} but was actually {video._colors[i].R}");
                Assert.True(expectedColors[i].G == video._colors[i].G, $"The Green value for color #{i} was expected to be {expectedColors[i].G} but was actually {video._colors[i].G}");
                Assert.True(expectedColors[i].B == video._colors[i].B, $"The Blue value for color #{i} was expected to be {expectedColors[i].B} but was actually {video._colors[i].B}");
            }
        }

        [Fact]
        public void InitializePalettesParsesPaletteRomAndBuildsTableCorrectly()
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = VideoHardwareTestData.COLOR_ROM;
            romData.Data[ROMs.PAC_MAN_PALETTE.FileName] = VideoHardwareTestData.PALETTE_ROM;
            romData.Data[ROMs.PAC_MAN_TILE.FileName] = null;
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = null;

            var video = new VideoHardware(romData);
            video.InitializeColors();
            video.InitializePalettes();

            // Expected palette colors values as listed in Chris Lomont's Pac-Man Emulation
            // Guide v0.1, page 6, figure 3.
            var expectedPalettes = new Color[][]
            {
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.FromArgb(222,222,255), Color.FromArgb(33,33,255), Color.Red, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.FromArgb(222,222,255), Color.FromArgb(33,33,255), Color.FromArgb(255,184,255), },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.FromArgb(222,222,255), Color.FromArgb(33,33,255), Color.Aqua, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.FromArgb(222,222,255), Color.FromArgb(33,33,255), Color.FromArgb(255,184,81), },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.FromArgb(33,33,255), Color.Red, Color.Yellow, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.FromArgb(222,222,255), Color.Black, Color.FromArgb(255,184,174), },
                new Color[] { Color.Black, Color.Red, Color.Lime, Color.FromArgb(222,222,255), },
                new Color[] { Color.Black, Color.FromArgb(255,184,174), Color.Black, Color.FromArgb(33,33,255), },
                new Color[] { Color.Black, Color.Lime, Color.FromArgb(33,33,255), Color.FromArgb(255,184,174), },
                new Color[] { Color.Black, Color.Lime, Color.FromArgb(222,222,255), Color.Red, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Red, Color.FromArgb(222,151,81), Color.FromArgb(222,222,255), },
                new Color[] { Color.Black, Color.FromArgb(255,184,81), Color.Lime, Color.FromArgb(222,151,81), },
                new Color[] { Color.Black, Color.Yellow, Color.FromArgb(71,184,255), Color.FromArgb(222,222,255), },
                new Color[] { Color.Black, Color.FromArgb(71,184,174), Color.Lime, Color.FromArgb(222,222,255), },
                new Color[] { Color.Black, Color.Aqua, Color.FromArgb(255,184,255), Color.Yellow, },
                new Color[] { Color.Black, Color.FromArgb(222,222,255), Color.FromArgb(33,33,255), Color.Black, },
                new Color[] { Color.Black, Color.FromArgb(255,184,174), Color.Black, Color.FromArgb(33,33,255), },
                new Color[] { Color.Black, Color.FromArgb(255,184,174), Color.Black, Color.FromArgb(33,33,255), },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.FromArgb(222,222,255), Color.FromArgb(255,184,174), Color.Red },
                new Color[] { Color.Black, Color.FromArgb(222,222,255), Color.FromArgb(33,33,255), Color.FromArgb(255,184,174) },
                new Color[] { Color.Black, Color.FromArgb(255,184,174), Color.Black, Color.FromArgb(222,222,255) },


                // The last 32 palettes aren't used; they're all just black.
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
                new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, },
            };

            Assert.Equal(64, video._palettes.Length);
            Assert.Equal(64, expectedPalettes.Length);

            for (var i = 0; i < expectedPalettes.Length; i++)
            {
                var actualPalette = video._palettes[i];
                var expectedPalette = expectedPalettes[i];

                Assert.True(actualPalette.Length == 4, $"The palette at index {i} should have exactly 4 color entries, but found {actualPalette.Length}");

                for (var j = 0; j < 4; j++)
                {
                    var actualColor = actualPalette[j];
                    var expectedColor = expectedPalette[j];

                    Assert.True(expectedColor.R == actualColor.R, $"The Red value for color at index {j} for the palette at index {i} was expected to be {expectedColor.R} but was actually {actualColor.R}");
                    Assert.True(expectedColor.G == actualColor.G, $"The Green value for color at index {j} for the palette at index {i} was expected to be {expectedColor.G} but was actually {actualColor.G}");
                    Assert.True(expectedColor.B == actualColor.B, $"The Blue value for color at index {j} for the palette at index {i} was expected to be {expectedColor.B} but was actually {actualColor.B}");
                }
            }
        }
    }
}
