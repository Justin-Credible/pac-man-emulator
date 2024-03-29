
using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace JustinCredible.PacEmu.Tests
{
    public class SpriteRendererTests
    {
        [Theory]
        [InlineData(1, "render-all-sprites-palette-1.bmp")]
        [InlineData(3, "render-all-sprites-palette-3.bmp")]
        [InlineData(24, "render-all-sprites-palette-24.bmp")]
        public void TestRenderSpriteRendersAllSprites(int paletteIndex, string fileToCompare)
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = VideoHardwareTestData.COLOR_ROM;
            romData.Data[ROMs.PAC_MAN_PALETTE.FileName] = VideoHardwareTestData.PALETTE_ROM;
            romData.Data[ROMs.PAC_MAN_TILE.FileName] = null;
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = null;

            var video = new VideoHardware(romData);
            video.InitializeColors();
            video.InitializePalettes();

            var spriteRenderer = new SpriteRenderer(VideoHardwareTestData.SPRITE_ROM, video._palettes);

            // There are 64 sprites, so we can render a grid of 8x8 sprites.
            // Each sprite is 16x16 pixels.
            var width = 8 * 16;
            var height = 8 * 16;

            // Holds the (x, y) coordinates of the origin (top/left) of the location
            // to render the next sprite.
            var spriteOriginX = 0;
            var spriteOriginY = 0;

            // The image we'll be rendering all the sprites to.
            var image = new Image<Rgba32>(width, height);

            // Render each of the 64 sprites.
            for (var spriteIndex = 0; spriteIndex < 64; spriteIndex++)
            {
                // Render the sprite with the given color palette.
                var sprite = spriteRenderer.RenderSprite(spriteIndex, paletteIndex);

                // Copy the rendered sprite over into the full image.
                for (var y = 0; y < 16; y++)
                {
                    for (var x = 0; x < 16; x++)
                    {
                        image[spriteOriginX + x, spriteOriginY + y] = sprite[x, y];
                    }
                }

                if ((spriteIndex + 1) % 8 == 0)
                {
                    // Row is finished, wrap back around.
                    spriteOriginX = 0;
                    spriteOriginY += 16;
                }
                else
                {
                    // Next column.
                    spriteOriginX += 16;
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

        [Fact]
        public void TestRenderSpriteRendersAllSpritesWithTransparency()
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = VideoHardwareTestData.COLOR_ROM;
            romData.Data[ROMs.PAC_MAN_PALETTE.FileName] = VideoHardwareTestData.PALETTE_ROM;
            romData.Data[ROMs.PAC_MAN_TILE.FileName] = null;
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = null;

            var video = new VideoHardware(romData);
            video.InitializeColors();
            video.InitializePalettes();

            var spriteRenderer = new SpriteRenderer(VideoHardwareTestData.SPRITE_ROM, video._palettes);

            // There are 64 sprites, so we can render a grid of 8x8 sprites.
            // Each sprite is 16x16 pixels.
            var width = 8 * 16;
            var height = 8 * 16;

            // Holds the (x, y) coordinates of the origin (top/left) of the location
            // to render the next sprite.
            var spriteOriginX = 0;
            var spriteOriginY = 0;

            // The image we'll be rendering all the sprites to.
            var image = new Image<Rgba32>(width, height, new Rgba32() { R = 255, G = 0, B = 255, A = 255 });

            // Render each of the 64 sprites.
            for (var spriteIndex = 0; spriteIndex < 64; spriteIndex++)
            {
                // Render the sprite with the given color palette.
                var sprite = spriteRenderer.RenderSprite(spriteIndex, 5);

                // Copy the rendered sprite over into the full image.
                for (var y = 0; y < 16; y++)
                {
                    for (var x = 0; x < 16; x++)
                    {
                        var pixel = sprite[x, y];
                        var isTransparent = pixel.A == 0;

                        if (!isTransparent)
                            image[spriteOriginX + x, spriteOriginY + y] = sprite[x, y];
                    }
                }

                if ((spriteIndex + 1) % 8 == 0)
                {
                    // Row is finished, wrap back around.
                    spriteOriginX = 0;
                    spriteOriginY += 16;
                }
                else
                {
                    // Next column.
                    spriteOriginX += 16;
                }
            }

            // Assert: the rendered image should be the same as the reference image.

            byte[] actualBytes = null;

            using (var steam = new MemoryStream())
            {
                image.Save(steam, new BmpEncoder());
                actualBytes = steam.ToArray();
            }

            var expectedBytes = File.ReadAllBytes("../../../ReferenceData/render-all-sprites-with-transparency.bmp");

            Assert.Equal(expectedBytes, actualBytes);
        }

        [Fact]
        public void TestRenderSpriteRendersAllSpritesWithRotations()
        {
            var romData = new ROMData();
            romData.Data[ROMs.PAC_MAN_COLOR.FileName] = VideoHardwareTestData.COLOR_ROM;
            romData.Data[ROMs.PAC_MAN_PALETTE.FileName] = VideoHardwareTestData.PALETTE_ROM;
            romData.Data[ROMs.PAC_MAN_TILE.FileName] = null;
            romData.Data[ROMs.PAC_MAN_SPRITE.FileName] = null;

            var video = new VideoHardware(romData);
            video.InitializeColors();
            video.InitializePalettes();

            var spriteRenderer = new SpriteRenderer(VideoHardwareTestData.SPRITE_ROM, video._palettes);

            // There are 64 sprites, so we can render a grid of 8x8 sprites.
            // Each sprite is 16x16 pixels.
            var width = 8 * 16;
            var height = 8 * 16;

            // Holds the (x, y) coordinates of the origin (top/left) of the location
            // to render the next sprite.
            var spriteOriginX = 0;
            var spriteOriginY = 0;

            // The image we'll be rendering all the sprites to.
            var image = new Image<Rgba32>(width, height, new Rgba32() { R = 0, G = 51, B = 102, A = 255});

            // Alternate between not flipping, flipping only X and only Y, and both.

            var flipIndex = 0;

            var flipValues = new List<Tuple<bool, bool>>()
            {
                new Tuple<bool, bool>(false, false),
                new Tuple<bool, bool>(true, false),
                new Tuple<bool, bool>(false, true),
                new Tuple<bool, bool>(true, true),
            };

            // Render each of the 64 sprites.
            for (var spriteIndex = 0; spriteIndex < 64; spriteIndex++)
            {
                // Determine how we'll flip this sprite.
                var flip = flipValues[flipIndex];
                var flipX = flip.Item1;
                var flipY = flip.Item2;

                // Render the sprite with the given color palette.
                var sprite = spriteRenderer.RenderSprite(spriteIndex, 1, flipX, flipY);

                // Increment the value so we flip differently on the next sprite.

                flipIndex++;

                if (flipIndex == 4)
                    flipIndex = 0;

                // Copy the rendered sprite over into the full image.
                for (var y = 0; y < 16; y++)
                {
                    for (var x = 0; x < 16; x++)
                    {
                        var pixel = sprite[x, y];
                        var isTransparent = pixel.A == 0;

                        if (!isTransparent)
                            image[spriteOriginX + x, spriteOriginY + y] = sprite[x, y];
                    }
                }

                if ((spriteIndex + 1) % 8 == 0)
                {
                    // Row is finished, wrap back around.
                    spriteOriginX = 0;
                    spriteOriginY += 16;
                }
                else
                {
                    // Next column.
                    spriteOriginX += 16;
                }
            }

            // Assert: the rendered image should be the same as the reference image.

            byte[] actualBytes = null;

            using (var steam = new MemoryStream())
            {
                image.Save(steam, new BmpEncoder());
                actualBytes = steam.ToArray();
            }

            var expectedBytes = File.ReadAllBytes("../../../ReferenceData/render-all-sprites-with-rotations.bmp");

            Assert.Equal(expectedBytes, actualBytes);
        }
    }
}
