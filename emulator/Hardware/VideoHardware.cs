
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

            // TODO: Pre-build palette colors from palette ROM.
        }

        public void InitializeTiles()
        {
            if (_tileROM == null)
                throw new ArgumentException("Tile ROM is required.");

            // TODO: ???
        }

        public void InitializeSprites()
        {
            if (_spriteROM == null)
                throw new ArgumentException("Sprite ROM is required.");

            // TODO: ???
        }
    }
}
