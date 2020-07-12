
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace JustinCredible.PacEmu.Tests
{
    public class TileRendererTests
    {
        [Theory]
        [InlineData(1, "render-all-tiles-palette-1.bmp")]
        [InlineData(3, "render-all-tiles-palette-3.bmp")]
        [InlineData(24, "render-all-tiles-palette-24.bmp")]
        public void TestRenderTileRendersAllTiles(int paletteIndex, string fileToCompare)
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = VideoHardwareTestData.COLOR_ROM;
            romData.Data[ROMs.PAC_MAN_PALETTE.FileName] = VideoHardwareTestData.PALETTE_ROM;
            romData.Data[ROMs.PAC_MAN_TILE.FileName] = null;
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = null;

            var video = new VideoHardware(romData);
            video.InitializeColors();
            video.InitializePalettes();

            var tileRenderer = new TileRenderer(VideoHardwareTestData.TILE_ROM, video._palettes);

            // There are 256 tiles, so we can render a grid of 16x16 tiles.
            // Each tile is 8x8 pixels.
            var width = 16 * 8;
            var height = 16 * 8;

            // Holds the (x, y) coordinates of the origin (top/left) of the location
            // to render the next tile.
            var tileOriginX = 0;
            var tileOriginY = 0;

            // The image we'll be rendering all the tiles to.
            var image = new Image<Rgba32>(width, height);

            // Render each of the 256 tiles.
            for (var tileIndex = 0; tileIndex < 256; tileIndex++)
            {
                // Render the tile with the given color palette.
                var tile = tileRenderer.RenderTile(tileIndex, paletteIndex);

                // Copy the rendered tile over into the full image.
                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        image[tileOriginX + x, tileOriginY + y] = tile[x, y];
                    }
                }

                if ((tileIndex + 1) % 16 == 0)
                {
                    // Row is finished, wrap back around.
                    tileOriginX = 0;
                    tileOriginY += 8;
                }
                else
                {
                    // Next column.
                    tileOriginX += 8;
                }
            }

            // Assert: the rendered image should be the same as the reference image.

            byte[] actualBytes = null;

            using (var steam = new MemoryStream())
            {
                image.Save(steam, new BmpEncoder());
                actualBytes = steam.ToArray();
            }

            var expectedBytes = File.ReadAllBytes($"../../../ReferenceData/{fileToCompare}");

            Assert.Equal(expectedBytes, actualBytes);
        }
    }
}
