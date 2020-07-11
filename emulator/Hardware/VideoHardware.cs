
using System;
using System.Drawing;

namespace JustinCredible.PacEmu
{
    /**
     * An implementation of the Pac-Man game video hardware for emulation.
     */
    public class VideoHardware
    {
        private byte[] _colorROM = null;
        private byte[] _paletteROM = null;
        private byte[] _tileROM = null;
        private byte[] _spriteROM = null;

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
    }
}
