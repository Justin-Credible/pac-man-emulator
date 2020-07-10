using System.Drawing;
using Xunit;

namespace JustinCredible.PacEmu.Tests
{
    public class VideoHardwareTests
    {
        private readonly byte[] COLOR_ROM = new byte[]
        {
            0x00, 0x07, 0x66, 0xef, 0x00, 0xf8, 0xea, 0x6f, 0x00, 0x3f, 0x00, 0xc9, 0x38, 0xaa, 0xaf, 0xf6,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        };

        [Fact]
        public void InitializeColorsParsesRomColorsCorrectly()
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = COLOR_ROM;
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
    }
}
