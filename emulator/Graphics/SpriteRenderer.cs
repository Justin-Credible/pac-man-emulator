
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = System.Drawing.Color;

namespace JustinCredible.PacEmu
{
    public class SpriteRenderer
    {
        // The raw sprite ROM chip data; used to build all of the sprites using the color palette.
        private byte[] _spriteROM;

        // This holds the colors of each of the palettes; the first dimension is the palette
        // index and the second dimension is the index of the color.
        private Color[][] _palettes;

        // The sprite cache is a two dimensional array, where the first dimension is the index
        // of the color palette and the second dimension is the index of the sprite.
        private Image<Rgba32>[][] _spriteCache;
        private Image<Rgba32>[][] _spriteCacheFlipX;
        private Image<Rgba32>[][] _spriteCacheFlipY;
        private Image<Rgba32>[][] _spriteCacheFlipBoth;

        public SpriteRenderer(byte[] spriteROM, Color[][] palettes)
        {
            _spriteROM = spriteROM;
            _palettes = palettes;
            _spriteCache = new Image<Rgba32>[palettes.Length][];
            _spriteCacheFlipX = new Image<Rgba32>[palettes.Length][];
            _spriteCacheFlipY = new Image<Rgba32>[palettes.Length][];
            _spriteCacheFlipBoth = new Image<Rgba32>[palettes.Length][];
        }

        /**
         * Renders and caches every sprite in each of the color palettes to eliminate having to
         * re-build each sprite from scratch for each call to RenderSprite().
         */
        public void PreRenderAllSprites()
        {
            // Each sprite is 64 bytes, so the number of sprites in the ROM can be calculated.
            // For Pac-Man, this is 4096 / 64 = 64 sprites.
            var spriteCount = _spriteROM.Length / 64;

            // For each possible color palette...
            for (int paletteIndex = 0; paletteIndex < _palettes.Length; paletteIndex++)
            {
                _spriteCache[paletteIndex] = new Image<Rgba32>[spriteCount];
                _spriteCacheFlipX[paletteIndex] = new Image<Rgba32>[spriteCount];
                _spriteCacheFlipY[paletteIndex] = new Image<Rgba32>[spriteCount];
                _spriteCacheFlipBoth[paletteIndex] = new Image<Rgba32>[spriteCount];

                // ... render each sprite in each palette and orientation and then cache it.
                for (int spriteIndex = 0; spriteIndex < spriteCount; spriteIndex++)
                {
                    _spriteCache[paletteIndex][spriteIndex] = RenderSprite(spriteIndex, paletteIndex, false, false);
                    _spriteCacheFlipX[paletteIndex][spriteIndex] = RenderSprite(spriteIndex, paletteIndex, flipX: true, flipY: false);
                    _spriteCacheFlipY[paletteIndex][spriteIndex] = RenderSprite(spriteIndex, paletteIndex, flipX: false, flipY: true);
                    _spriteCacheFlipBoth[paletteIndex][spriteIndex] = RenderSprite(spriteIndex, paletteIndex, flipX: true, flipY: true);
                }
            }
        }

        /**
         * Renders the given sprite with the given color palette.
         */
        public Image<Rgba32> RenderSprite(int spriteIndex, int paletteIndex, bool flipX = false, bool flipY = false)
        {
            Image<Rgba32> sprite = null;

            if (flipX && flipY)
                sprite = _spriteCacheFlipBoth?[paletteIndex]?[spriteIndex];
            else if (flipX)
                sprite = _spriteCacheFlipX?[paletteIndex]?[spriteIndex];
            else if (flipY)
                sprite = _spriteCacheFlipY?[paletteIndex]?[spriteIndex];
            else
                sprite = _spriteCache?[paletteIndex]?[spriteIndex];

            if (sprite != null)
                return sprite;

            var palette = _palettes[paletteIndex];

            // See: Chris Lomont's Pac-Man Emulation Guide v0.1, page 6-7.

            // Each sprite is 16x16 pixels.
            // Note that we mark all the background pixels as transparent here.
            sprite = new Image<Rgba32>(16, 16, new Rgba32() { A = 0 });

            // Each 8x4 strip of pixels is a "group" defined by 8 bytes.
            // Each strip goes into one of the follow location "slots"
            // 0  1
            // 2  3
            // 4  5
            // 6  7

            // The starting location for each 8 byte "group" is:
            // 5  1
            // 6  2
            // 7  3
            // 4  0

            // For convenience, we'll iterate over the bytes in this group order so we can more
            // easily draw from left to right, top to bottom using x, y coordinates.
            var groupIndexOrder = new int[] { 5, 1, 6, 2, 7, 3, 4, 0 };

            // The starting origin of each column of pixels we'll render.
            var originX = 0;
            var originY = 0;

            // Loop over the 64 bytes of data that will make up the pixels for the 8x8 sprite.
            foreach (var groupIndex in groupIndexOrder)
            {
                // Each sprite is 64 bytes long; calculate the start and stop index into the sprite
                // for each 8x4 group of pixels.
                var spriteByteStartIndex = (spriteIndex * 64) + (groupIndex * 8);
                var spriteByteEndIndex = (spriteIndex * 64) + (groupIndex * 8) + 8;

                // Apparently the last byte in the sequence of 8 contains the first row of pixels;
                // this is why we're looping backwards here.
                for (var i = spriteByteEndIndex - 1; i >= spriteByteStartIndex; i--)
                {
                    // Grab a byte.
                    var spriteByte = _spriteROM[i];

                    // Each pixel is made up of two bits. These two bits make up a number (0-4)
                    // that is an index into the current color palette which determines the pixel's
                    // color. Therfore, each byte holds 4 pixels. The two bits for each pixel are
                    // stored in bitplanes, like so:

                    // Bit     Usage
                    // 0       Bit 0 of pixel #1
                    // 1       Bit 0 of pixel #2
                    // 2       Bit 0 of pixel #3
                    // 3       Bit 0 of pixel #4
                    // 4       Bit 1 of pixel #1
                    // 5       Bit 1 of pixel #2
                    // 6       Bit 1 of pixel #3
                    // 7       Bit 1 of pixel #4

                    // Combine the two bits for each pixel to get the color index.
                    var pixel1ColorIndex = ((spriteByte & 0x10) >> 3) | (spriteByte & 0x01);
                    var pixel2ColorIndex = ((spriteByte & 0x20) >> 4) | ((spriteByte & 0x02) >> 1);
                    var pixel3ColorIndex = ((spriteByte & 0x40) >> 5) | ((spriteByte & 0x04) >> 2);
                    var pixel4ColorIndex = ((spriteByte & 0x80) >> 6) | ((spriteByte & 0x08) >> 3);

                    // Using the index, fetch the color for each pixel.
                    var pixel1Color = palette[pixel1ColorIndex];
                    var pixel2Color = palette[pixel2ColorIndex];
                    var pixel3Color = palette[pixel3ColorIndex];
                    var pixel4Color = palette[pixel4ColorIndex];

                    // Place each pixel in the sprite using the correct color.
                    // Note that the bottom of the column is pixel #1 and the top is pixel #4.
                    // Pixels using palette 0 OR using color index 0 of any palette are treated as transparent.
                    sprite[originX, originY + 0] = new Rgba32() { R = pixel4Color.R, G = pixel4Color.G, B = pixel4Color.B, A = (byte)(paletteIndex == 0 || pixel4ColorIndex == 0 ? 0 : 255) };
                    sprite[originX, originY + 1] = new Rgba32() { R = pixel3Color.R, G = pixel3Color.G, B = pixel3Color.B, A = (byte)(paletteIndex == 0 || pixel3ColorIndex == 0 ? 0 : 255) };
                    sprite[originX, originY + 2] = new Rgba32() { R = pixel2Color.R, G = pixel2Color.G, B = pixel2Color.B, A = (byte)(paletteIndex == 0 || pixel2ColorIndex == 0 ? 0 : 255) };
                    sprite[originX, originY + 3] = new Rgba32() { R = pixel1Color.R, G = pixel1Color.G, B = pixel1Color.B, A = (byte)(paletteIndex == 0 || pixel1ColorIndex == 0 ? 0 : 255) };

                    if (originX == 15)
                    {
                        // Once we reach the last column, wrap around so we can render the bottom.
                        originX = 0;
                        originY += 4;
                    }
                    else
                    {
                        // Next column.
                        originX++;
                    }
                }
            }

            if (flipX)
                sprite.Mutate(x => x.Flip(FlipMode.Horizontal));

            if (flipY)
                sprite.Mutate(x => x.Flip(FlipMode.Vertical));

            return sprite;
        }
    }
}
