
using System;
using Color = System.Drawing.Color;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using JustinCredible.ZilogZ80;

namespace JustinCredible.PacEmu
{
    /**
     * An implementation of the Pac-Man game video hardware for emulation.
     */
    public class VideoHardware
    {
        // 32 Tiles Wide * 8 Pixels = 256
        // 36 Tiles Tall * 8 Pixels = 288
        internal const int RESOLUTION_WIDTH = 256;
        internal const int RESOLUTION_HEIGHT = 288;

        private byte[] _colorROM = null;
        private byte[] _paletteROM = null;
        private byte[] _tileROM = null;
        private byte[] _spriteROM = null;

        // TODO: Use SixLabors.ImageSharp.Color instead?
        internal Color[] _colors = null;
        internal Color[][] _palettes = null;

        private TileRenderer _tileRenderer = null;

        public VideoHardware(ROMData romData)
        {
            _colorROM = romData.Data[ROMs.PAC_MAN_COLOR.FileName];
            _paletteROM = romData.Data[ROMs.PAC_MAN_PALETTE.FileName];
            _tileROM = romData.Data[ROMs.PAC_MAN_TILE.FileName];
            _spriteROM = romData.Data[ROMs.PAC_MAN_SPRITE.FileName];
        }

        public void Initialize()
        {
            InitializeColors();
            InitializePalettes();
            InitializeTiles();
            InitializeSprites();
        }

        public void InitializeColors()
        {
            if (_colorROM == null)
                throw new ArgumentException("Color ROM is required.");

            _colors = new Color[_colorROM.Length];

            for (var i = 0; i < _colorROM.Length; i++)
            {
                var colorByte = _colorROM[i];

                // Compute the RGB color value by using the color value weights as listed in
                // Chris Lomont's Pac-Man Emulation Guide v0.1, page 5, table 3.
                var red = 0;
                var green = 0;
                var blue = 0;

                // Bit 0: Red (least amount); weight: 0x21 (33 dec).
                if ((colorByte & 0x01) == 0x01)
                    red += 0x21;

                // Bit 1: Red; weight: 0x47 (71 dec).
                if ((colorByte & 0x02) == 0x02)
                    red += 0x47;

                // Bit 2: Red (most amount); weight: 0x97 (151 dec).
                if ((colorByte & 0x04) == 0x04)
                    red += 0x97;

                // Bit 3: Green (least amount); weight: 0x21 (33 dec).
                if ((colorByte & 0x08) == 0x08)
                    green += 0x21;

                // Bit 4: Green; weight: 0x47 (71 dec).
                if ((colorByte & 0x10) == 0x10)
                    green += 0x47;

                // Bit 5: Green (most amount); weight: 0x97 (151 dec).
                if ((colorByte & 0x20) == 0x20)
                    green += 0x97;

                // Bit 6: Blue; weight: 0x51 (81 dec).
                if ((colorByte & 0x40) == 0x40)
                    blue += 0x51;

                // Bit 7: Green (most amount); weight: 0xAE (174 dec).
                if ((colorByte & 0x80) == 0x80)
                    blue += 0xAE;

                _colors[i] = Color.FromArgb(red, green, blue);
            }
        }

        public void InitializePalettes()
        {
            if (_paletteROM == null)
                throw new ArgumentException("Palette ROM is required.");

            // Built palette table so we can lookup each color. Information from
            // Chris Lomont's Pac-Man Emulation Guide v0.1, page 6, figure 3.

            // Each palette is 4 bytes and consists of 4 colors.
            _palettes = new Color[_paletteROM.Length / 4][];

            var paletteIndex = 0;

            // Step over each palette entry, which is four bytes...
            for (var i = 0; i < _paletteROM.Length; i += 4)
            {
                // Each byte is an index into the color ROM values.
                var color0Number = _paletteROM[i];
                var color1Number = _paletteROM[i + 1];
                var color2Number = _paletteROM[i + 2];
                var color3Number = _paletteROM[i + 3];

                // Grab each of the colors for this palette.
                var color0 = _colors[color0Number];
                var color1 = _colors[color1Number];
                var color2 = _colors[color2Number];
                var color3 = _colors[color3Number];

                // Create an entry in the palette table.
                _palettes[paletteIndex] = new Color[4]
                {
                    color0,
                    color1,
                    color2,
                    color3,
                };

                paletteIndex++;
            }
        }

        public void InitializeTiles()
        {
            if (_tileROM == null)
                throw new ArgumentException("Tile ROM is required.");

            _tileRenderer = new TileRenderer(_tileROM, _palettes);
            _tileRenderer.PreRenderAllTiles();
        }

        public void InitializeSprites()
        {
            if (_spriteROM == null)
                throw new ArgumentException("Sprite ROM is required.");

            // TODO: ???
        }

        public Image<Rgba32> Render(IMemory memory)
        {
            var image = new Image<Rgba32>(RESOLUTION_WIDTH, RESOLUTION_HEIGHT);

            // Render background; this includes the playfield (maze, dots, power pelletes),
            // top bar (scores), and bottom bar (lives and level counter).
            RenderTiles(memory, image);

            // TODO: Render sprites.
            // RenderSprites(memory, image);

            return image;
        }

        public void RenderTiles(IMemory memory, Image<Rgba32> image)
        {
            #region Render top strip of tiles (scores).

            // First row uses addresses 3DF through 3C0, decreasing from left to right.

            var originX = 0;  // Column 0
            var originY = 0;  // Row 0

            for (var i = 0x3DF; i >= 0x3C0; i--)
            {
                var tileAddress = 0x4000 + i;
                var paletteAddress = 0x4400 + i;

                var tileIndex = memory.Read(tileAddress);
                var paletteIndex = memory.Read(paletteAddress);

                var tile = _tileRenderer.RenderTile(tileIndex, paletteIndex);

                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        image[originX + x, originY + y] = tile[x, y];
                    }
                }

                // Next column.
                originX += 8;
            }

            // Second row uses addresses 3FF through 3E0, decreasing from left to right.

            originX = 0;  // Column 0
            originY = 1 * 8;  // Row 1 (0 zero-indexed)

            for (var i = 0x3FF; i >= 0x3E0; i--)
            {
                var tileAddress = 0x4000 + i;
                var paletteAddress = 0x4400 + i;

                var tileIndex = memory.Read(tileAddress);
                var paletteIndex = memory.Read(paletteAddress);

                var tile = _tileRenderer.RenderTile(tileIndex, paletteIndex);

                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        image[originX + x, originY + y] = tile[x, y];
                    }
                }

                // Next column.
                originX += 8;
            }

            #endregion

            #region Render the playfield background tiles

            // The playfield uses addresses 040 through 3BF, increasing from top to bottom,
            // right to left, starting at the top right corner of the playfield.

            originX = 29 * 8; // Column 30 (29 zero-indexed)
            originY = 2 * 8; // Row 3 (2 zero-indexed)

            var playfieldRow = 1;

            for (var i = 0x040; i <= 0x3BF; i++)
            {
                var tileAddress = 0x4000 + i;
                var paletteAddress = 0x4400 + i;

                var tileIndex = memory.Read(tileAddress);
                var paletteIndex = memory.Read(paletteAddress);

                var tile = _tileRenderer.RenderTile(tileIndex, paletteIndex);

                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        image[originX + x, originY + y] = tile[x, y];
                    }
                }

                if (playfieldRow == 32)
                {
                    // Next column (to the left) and back to the top.
                    originX -= 8;
                    originY = 2 * 8; // Row 3 (2 zero-indexed)
                    playfieldRow = 1;
                }
                else
                {
                    // Next row.
                    originY += 8;
                    playfieldRow++;
                }
            }

            #endregion

            #region Render bottom strip of tiles (lives, stage counter)

            // First row uses addresses 01F through 000, decreasing from left to right.

            originX = 0; // Column 0
            originY = 34 * 8; // Row 35 (34 zero-indexed)

            for (var i = 0x01F; i >= 0x000; i--)
            {
                var tileAddress = 0x4000 + i;
                var paletteAddress = 0x4400 + i;

                var tileIndex = memory.Read(tileAddress);
                var paletteIndex = memory.Read(paletteAddress);

                var tile = _tileRenderer.RenderTile(tileIndex, paletteIndex);

                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        image[originX + x, originY + y] = tile[x, y];
                    }
                }

                // Next column.
                originX += 8;
            }

            // Second row uses addresses 03F through 020, decreasing from left to right.

            originX = 0; // Column 0
            originY = 35 * 8; // Row 36 (35 zero-indexed)

            for (var i = 0x03F; i >= 0x020; i--)
            {
                var tileAddress = 0x4000 + i;
                var paletteAddress = 0x4400 + i;

                var tileIndex = memory.Read(tileAddress);
                var paletteIndex = memory.Read(paletteAddress);

                var tile = _tileRenderer.RenderTile(tileIndex, paletteIndex);

                for (var y = 0; y < 8; y++)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        image[originX + x, originY + y] = tile[x, y];
                    }
                }

                // Next column.
                originX += 8;
            }

            #endregion
        }
    }
}
