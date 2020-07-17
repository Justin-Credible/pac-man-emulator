using Color = System.Drawing.Color;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using Xunit;

namespace JustinCredible.PacEmu.Tests
{
    public class VideoHardwareTests
    {
        [Fact]
        public void TestInitializeColorsParsesRomColorsCorrectly()
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
        public void TestInitializePalettesParsesPaletteRomAndBuildsTableCorrectly()
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

        [Fact]
        public void TestRenderTiles()
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = VideoHardwareTestData.COLOR_ROM;
            romData.Data[ROMs.PAC_MAN_PALETTE.FileName] = VideoHardwareTestData.PALETTE_ROM;
            romData.Data[ROMs.PAC_MAN_TILE.FileName] = VideoHardwareTestData.TILE_ROM;
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = VideoHardwareTestData.SPRITE_ROM;

            var video = new VideoHardware(romData);
            video.InitializeColors();
            video.InitializePalettes();
            video.InitializeTiles();
            video.InitializeSprites();

            // Arrange: Setup the tiles and specify the palettes for each.

            var rawMemory = new byte[0xFFFF];
            var memory = new SimpleMemory(rawMemory);

            VideoHardwareTestData.SetupBackgroundTiles(memory);

            // Act: Render the image based on the tiles and palettes in memory.

            var spriteCoordinates = new byte[16];
            var image = video.Render(memory, spriteCoordinates);

            // Assert: the rendered image should be the same as the reference image.

            byte[] actualBytes = null;

            using (var steam = new MemoryStream())
            {
                image.Save(steam, new BmpEncoder());
                actualBytes = steam.ToArray();
            }

            var expectedBytes = File.ReadAllBytes($"../../../ReferenceData/render-playfield-tiles.bmp");

            Assert.Equal(expectedBytes, actualBytes);
        }

        [Fact]
        public void TestRenderSpriteOverBackgroundTiles()
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = VideoHardwareTestData.COLOR_ROM;
            romData.Data[ROMs.PAC_MAN_PALETTE.FileName] = VideoHardwareTestData.PALETTE_ROM;
            romData.Data[ROMs.PAC_MAN_TILE.FileName] = VideoHardwareTestData.TILE_ROM;
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = VideoHardwareTestData.SPRITE_ROM;

            var video = new VideoHardware(romData);
            video.InitializeColors();
            video.InitializePalettes();
            video.InitializeTiles();
            video.InitializeSprites();

            // Arrange: Setup the tiles and specify the palettes for each.

            var rawMemory = new byte[0xFFFF];
            var memory = new SimpleMemory(rawMemory);

            VideoHardwareTestData.SetupBackgroundTiles(memory);

            memory.Write(0x5060, 31); // x
            memory.Write(0x5061, 16); // y

            // Pac-Man facing left sprite (46), no flip X/Y (0x00).
            var flags = (byte)((46 << 2) | (0x00));

            memory.Write(0x4FF0, flags);
            memory.Write(0x4FF1, 9); // Palette.

            // Act: Render the image based on the tiles and palettes in memory.

            var spriteCoordinates = new byte[16];
            var image = video.Render(memory, spriteCoordinates);

            // Assert: the rendered image should be the same as the reference image.

            byte[] actualBytes = null;

            using (var steam = new MemoryStream())
            {
                image.Save(steam, new BmpEncoder());
                actualBytes = steam.ToArray();
            }

            var expectedBytes = File.ReadAllBytes($"../../../ReferenceData/render-sprites-on-playfield.bmp");

            Assert.Equal(expectedBytes, actualBytes);
        }

        [Theory]
        [InlineData("boot-screen.vram", "render-boot-screen.bmp")]
        [InlineData("attract-screen.vram", "render-attract-screen.bmp")]
        [InlineData("maze-1.vram", "render-maze-1.bmp")]
        public void TestRenderScreen(string vramFile, string expectedBitmapFile)
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = VideoHardwareTestData.COLOR_ROM;
            romData.Data[ROMs.PAC_MAN_PALETTE.FileName] = VideoHardwareTestData.PALETTE_ROM;
            romData.Data[ROMs.PAC_MAN_TILE.FileName] = VideoHardwareTestData.TILE_ROM;
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = VideoHardwareTestData.SPRITE_ROM;

            var video = new VideoHardware(romData);
            video.InitializeColors();
            video.InitializePalettes();
            video.InitializeTiles();
            video.InitializeSprites();

            // Arrange: Load a VRAM dump which contains tiles positions and palettes for each tile.

            var rawMemory = new byte[0xFFFF];

            var vram = File.ReadAllBytes($"../../../TestData/{vramFile}");
            Array.Copy(vram, 0, rawMemory, 0x4000, 0x0800);

            var memory = new SimpleMemory(rawMemory);

            // Act: Render the image based on the tiles and palettes in memory.

            var spriteCoordinates = new byte[16];
            var image = video.Render(memory, spriteCoordinates);

            // Assert: the rendered image should be the same as the reference image.

            byte[] actualBytes = null;

            using (var steam = new MemoryStream())
            {
                image.Save(steam, new BmpEncoder());
                actualBytes = steam.ToArray();
            }

            var expectedBytes = File.ReadAllBytes($"../../../ReferenceData/{expectedBitmapFile}");

            Assert.Equal(expectedBytes, actualBytes);
        }
    }
}
