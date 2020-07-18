
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = System.Drawing.Color;

namespace JustinCredible.PacEmu
{
    public class TileRenderer
    {
        // The raw tile ROM chip data; used to build all of the tiles using the color palette.
        private byte[] _tileROM;

        // This holds the colors of each of the palettes; the first dimension is the palette
        // index and the second dimension is the index of the color.
        private Color[][] _palettes;

        // The tile cache is a two dimensional array, where the first dimension is the index
        // of the color palette and the second dimension is the index of the tile.
        private Image<Rgba32>[][] _tileCache;

        public TileRenderer(byte[] tileROM, Color[][] palettes)
        {
            _tileROM = tileROM;
            _palettes = palettes;
            _tileCache = new Image<Rgba32>[palettes.Length][];
        }

        /**
         * Renders and caches every tile in each of the color palettes to eliminate having to
         * re-build each tile from scratch for each call to RenderTile().
         */
        public void PreRenderAllTiles()
        {
            // Each tile is 16 bytes, so the number of tiles in the ROM can be calculated.
            // For Pac-Man, this is 4096 / 16 = 256 tiles.
            var tileCount = _tileROM.Length / 16;

            // For each possible color palette...
            for (int paletteIndex = 0; paletteIndex < _palettes.Length; paletteIndex++)
            {
                _tileCache[paletteIndex] = new Image<Rgba32>[tileCount];

                // ... render each tile in said color and then cache it.
                for (int tileIndex = 0; tileIndex < tileCount; tileIndex++)
                {
                    _tileCache[paletteIndex][tileIndex] = RenderTile(tileIndex, paletteIndex);
                }
            }
        }

        /**
         * Renders the given tile with the given color palette.
         */
        public Image<Rgba32> RenderTile(int tileIndex, int paletteIndex)
        {
            var tile = _tileCache?[paletteIndex]?[tileIndex];

            if (tile != null)
                return tile;

            var palette = _palettes[paletteIndex];

            // See: Chris Lomont's Pac-Man Emulation Guide v0.1, page 6.

            // Each tile is 8x8 pixels.
            tile = new Image<Rgba32>(8, 8);

            // Each tile is 16 bytes long; calculate the start and stop index into the tile
            // ROM so we can get the 16 bytes for each tile.
            var tileByteStartIndex = tileIndex * 16;
            var tileByteEndIndex = tileByteStartIndex + 16;

            // The starting origin of each column of pixels we'll render.
            var originX = 0;
            var originY = 0;

            // Loop over the 16 bytes of data that will make up the pixels for the 8x8 tile.
            // Apparently the last byte in the sequence of 16 contains the first row of pixels;
            // this is why we're looping backwards here.
            for (var i = tileByteEndIndex - 1; i >= tileByteStartIndex; i--)
            {
                // Grab a byte.
                var tileByte = _tileROM[i];

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
                var pixel1ColorIndex = ((tileByte & 0x10) >> 3) | (tileByte & 0x01);
                var pixel2ColorIndex = ((tileByte & 0x20) >> 4) | ((tileByte & 0x02) >> 1);
                var pixel3ColorIndex = ((tileByte & 0x40) >> 5) | ((tileByte & 0x04) >> 2);
                var pixel4ColorIndex = ((tileByte & 0x80) >> 6) | ((tileByte & 0x08) >> 3);

                // Using the index, fetch the color for each pixel.
                var pixel1Color = palette[pixel1ColorIndex];
                var pixel2Color = palette[pixel2ColorIndex];
                var pixel3Color = palette[pixel3ColorIndex];
                var pixel4Color = palette[pixel4ColorIndex];

                // Place each pixel in the tile using the correct color.
                // Note that the bottom of the column is pixel #1 and the top is pixel #4.
                tile[originX, originY + 0] = new Rgba32() { R = pixel4Color.R, G = pixel4Color.G, B = pixel4Color.B, A = 255 };
                tile[originX, originY + 1] = new Rgba32() { R = pixel3Color.R, G = pixel3Color.G, B = pixel3Color.B, A = 255 };
                tile[originX, originY + 2] = new Rgba32() { R = pixel2Color.R, G = pixel2Color.G, B = pixel2Color.B, A = 255 };
                tile[originX, originY + 3] = new Rgba32() { R = pixel1Color.R, G = pixel1Color.G, B = pixel1Color.B, A = 255 };

                if (originX == 7)
                {
                    // Once we reach the last column, wrap around so we can render the bottom.
                    originX = 0;
                    originY = 4;
                }
                else
                {
                    // Next column.
                    originX++;
                }
            }

            return tile;
        }
    }
}
