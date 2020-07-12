using Color = System.Drawing.Color;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;
using System;
using System.IO;
using SixLabors.ImageSharp.Formats.Bmp;

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
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = null;

            var video = new VideoHardware(romData);
            video.InitializeColors();
            video.InitializePalettes();
            video.InitializeTiles();

            // Arrange: Setup the tiles and specify the palettes for each.

            var rawMemory = new byte[0xFFFF];
            var memory = new SimpleMemory(rawMemory);

            // 1UP

            memory.Write(0x4000 + 0x3DA, (byte)(16 * 3 + 1)); // 1
            memory.Write(0x4000 + 0x3D9, (byte)(16 * 5 + 5)); // U
            memory.Write(0x4000 + 0x3D8, (byte)(16 * 5 + 0)); // P

            memory.Write(0x4400 + 0x3DA, (byte)15);
            memory.Write(0x4400 + 0x3D9, (byte)15);
            memory.Write(0x4400 + 0x3D8, (byte)15);

            // 1UP score (220)

            memory.Write(0x4000 + 0x3F9, (byte)2); // 2
            memory.Write(0x4000 + 0x3F8, (byte)2); // 2
            memory.Write(0x4000 + 0x3F7, (byte)0); // 0

            memory.Write(0x4400 + 0x3F9, (byte)15);
            memory.Write(0x4400 + 0x3F8, (byte)15);
            memory.Write(0x4400 + 0x3F7, (byte)15);

            // HIGH SCORE

            memory.Write(0x4000 + 0x3D4, (byte)(16 * 4 + 8)); // H
            memory.Write(0x4000 + 0x3D3, (byte)(16 * 4 + 9)); // I
            memory.Write(0x4000 + 0x3D2, (byte)(16 * 4 + 7)); // G
            memory.Write(0x4000 + 0x3D1, (byte)(16 * 4 + 8)); // H
            memory.Write(0x4000 + 0x3D0, (byte)(16 * 1 + 6)); // (space)
            memory.Write(0x4000 + 0x3CF, (byte)(16 * 5 + 3)); // S
            memory.Write(0x4000 + 0x3CE, (byte)(16 * 4 + 3)); // C
            memory.Write(0x4000 + 0x3CD, (byte)(16 * 4 + 15)); // O
            memory.Write(0x4000 + 0x3CC, (byte)(16 * 5 + 2)); // R
            memory.Write(0x4000 + 0x3CB, (byte)(16 * 4 + 5)); // E

            memory.Write(0x4400 + 0x3D4, (byte)15);
            memory.Write(0x4400 + 0x3D3, (byte)15);
            memory.Write(0x4400 + 0x3D2, (byte)15);
            memory.Write(0x4400 + 0x3D1, (byte)15);
            memory.Write(0x4400 + 0x3D0, (byte)15);
            memory.Write(0x4400 + 0x3CF, (byte)15);
            memory.Write(0x4400 + 0x3CE, (byte)15);
            memory.Write(0x4400 + 0x3CD, (byte)15);
            memory.Write(0x4400 + 0x3CC, (byte)15);
            memory.Write(0x4400 + 0x3CB, (byte)15);

            // High score value (100)

            memory.Write(0x4000 + 0x3F0, (byte)(16 * 3 + 1)); // 1
            memory.Write(0x4000 + 0x3EF, (byte)(16 * 3 + 0)); // 0
            memory.Write(0x4000 + 0x3EE, (byte)(16 * 3 + 0)); // 0
            memory.Write(0x4000 + 0x3ED, (byte)(16 * 3 + 0)); // 0

            memory.Write(0x4400 + 0x3F0, (byte)15);
            memory.Write(0x4400 + 0x3EF, (byte)15);
            memory.Write(0x4400 + 0x3EE, (byte)15);
            memory.Write(0x4400 + 0x3ED, (byte)15);

            // 2UP

            memory.Write(0x4000 + 0x3C7, (byte)(16 * 3 + 2)); // 2
            memory.Write(0x4000 + 0x3C6, (byte)(16 * 5 + 5)); // U
            memory.Write(0x4000 + 0x3C5, (byte)(16 * 5 + 0)); // P

            memory.Write(0x4400 + 0x3C7, (byte)15);
            memory.Write(0x4400 + 0x3C6, (byte)15);
            memory.Write(0x4400 + 0x3C5, (byte)15);

            // 2UP score (290)

            memory.Write(0x400 + 0x3E6, (byte)2); // 2
            memory.Write(0x400 + 0x3E5, (byte)9); // 9
            memory.Write(0x400 + 0x3E4, (byte)0); // 0

            memory.Write(0x4400 + 0x3E6, (byte)15);
            memory.Write(0x4400 + 0x3E5, (byte)15);
            memory.Write(0x4400 + 0x3E4, (byte)15);

            // Populate the palette for the bottom two rows (lives and level counter)

            // Pac-Man Life Icon 1

            memory.Write(0x4000 + 0x01B, (byte)(16 * 2 + 1));
            memory.Write(0x4000 + 0x01A, (byte)(16 * 2 + 0));
            memory.Write(0x4000 + 0x03B, (byte)(16 * 2 + 3));
            memory.Write(0x4000 + 0x03A, (byte)(16 * 2 + 2));

            memory.Write(0x4400 + 0x01B, (byte)9);
            memory.Write(0x4400 + 0x01A, (byte)9);
            memory.Write(0x4400 + 0x03B, (byte)9);
            memory.Write(0x4400 + 0x03A, (byte)9);

            // Pac-Man Life Icon 2

            memory.Write(0x4000 + 0x019, (byte)(16 * 2 + 1));
            memory.Write(0x4000 + 0x018, (byte)(16 * 2 + 0));
            memory.Write(0x4000 + 0x039, (byte)(16 * 2 + 3));
            memory.Write(0x4000 + 0x038, (byte)(16 * 2 + 2));

            memory.Write(0x4400 + 0x019, (byte)9);
            memory.Write(0x4400 + 0x018, (byte)9);
            memory.Write(0x4400 + 0x039, (byte)9);
            memory.Write(0x4400 + 0x038, (byte)9);

            // Cherry Level Icon

            memory.Write(0x4000 + 0x005, (byte)(16 * 9 + 1));
            memory.Write(0x4000 + 0x004, (byte)(16 * 9 + 0));
            memory.Write(0x4000 + 0x025, (byte)(16 * 9 + 3));
            memory.Write(0x4000 + 0x024, (byte)(16 * 9 + 2));

            memory.Write(0x4400 + 0x005, (byte)20);
            memory.Write(0x4400 + 0x004, (byte)20);
            memory.Write(0x4400 + 0x025, (byte)20);
            memory.Write(0x4400 + 0x024, (byte)20);

            // Dots

            foreach (var dotLocation in VideoHardwareTestData.DOT_TILE_LOCATIONS)
            {
                memory.Write(0x4000 + dotLocation, (byte)(16 * 1 + 0));
                memory.Write(0x4400 + dotLocation, (byte)1);
            }

            // Power Pellets

            foreach (var dotLocation in VideoHardwareTestData.POWER_PELLET_TILE_LOCATIONS)
            {
                memory.Write(0x4000 + dotLocation, (byte)(16 * 1 + 4));
                memory.Write(0x4400 + dotLocation, (byte)1);
            }

            // Act: Render the image based on the tiles and palettes in memory.

            var image = new Image<Rgba32>(VideoHardware.RESOLUTION_WIDTH, VideoHardware.RESOLUTION_HEIGHT);
            video.RenderTiles(memory, image);

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
    }
}
