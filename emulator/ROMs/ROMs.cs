using System.Collections.Generic;

namespace JustinCredible.PacEmu
{
    /**
     * Used to encapsulate each of the ROMs for each game that runs on the Pac-Man hardware.
     */
    public class ROMs
    {
        public static readonly ROMFile PAC_MAN_CODE_1 = new ROMFile() { FileName = "pacman.6e", Size = 4096, CRC32 = "C1E6AB10", Description = "Code 1 (4KB)" };
        public static readonly ROMFile PAC_MAN_CODE_2 = new ROMFile() { FileName = "pacman.6f", Size = 4096, CRC32 = "1A6FB2D4", Description = "Code 2 (4KB)" };
        public static readonly ROMFile PAC_MAN_CODE_3 = new ROMFile() { FileName = "pacman.6h", Size = 4096, CRC32 = "BCDD1BEB", Description = "Code 3 (4KB)" };
        public static readonly ROMFile PAC_MAN_CODE_4 = new ROMFile() { FileName = "pacman.6j", Size = 4096, CRC32 = "817D94E3", Description = "Code 4 (4KB)" };
        public static readonly ROMFile PAC_MAN_COLOR = new ROMFile() { FileName = "82s123.7f", Size = 32, CRC32 = "2FC650BD", Description = "Color (32B) (32 one-byte colors)" };
        public static readonly ROMFile PAC_MAN_PALETTE = new ROMFile() { FileName = "82s126.4a", Size = 256, CRC32 = "3EB3A8E4", Description = "Palette (256B) (64 four-byte pallets)" };
        public static readonly ROMFile PAC_MAN_TILE = new ROMFile() { FileName = "pacman.5e", Size = 4096, CRC32 = "0C944964", Description = "Tile (4KB) (256 8x8 pixel tile images)" };
        public static readonly ROMFile PAC_MAN_SPRITE = new ROMFile() { FileName = "pacman.5f", Size = 4096, CRC32 = "958FEDF9", Description = "Sprite (4KB) (64 16x16 sprite images)" };
        public static readonly ROMFile PAC_MAN_SOUND_1 = new ROMFile() { FileName = "82s126.1m", Size = 256, CRC32 = "A9CC86BF", Description = "Sound 1 (256B) (8 waveforms)" };
        public static readonly ROMFile PAC_MAN_SOUND_2 = new ROMFile() { FileName = "82s126.3m", Size = 256, CRC32 = "77245B66", Description = "Sound 2 (256B) (8 waveforms)" };

        public static readonly ROMFile MS_PAC_MAN_TILE = new ROMFile() { FileName = "5e", Size = 4096, CRC32 = "5C281D01", Description = "Tile (4KB) (256 8x8 pixel tile images)" };
        public static readonly ROMFile MS_PAC_MAN_SPRITE = new ROMFile() { FileName = "5f", Size = 4096, CRC32 = "615AF909", Description = "Sprite (4KB) (64 16x16 sprite images)" };
        public static readonly ROMFile MS_PAC_MAN_AUX_U5 = new ROMFile() { FileName = "u5", Size = 2048, CRC32 = "F45FBBCD", Description = "Aux Board ROM (2KB)" };
        public static readonly ROMFile MS_PAC_MAN_AUX_U6 = new ROMFile() { FileName = "u6", Size = 4096, CRC32 = "A90E7000", Description = "Aux Board ROM (4KB)" };
        public static readonly ROMFile MS_PAC_MAN_AUX_U7 = new ROMFile() { FileName = "u7", Size = 4096, CRC32 = "C82CD714", Description = "Aux Board ROM (4KB)" };

        public static readonly List<ROMFile> PAC_MAN = new List<ROMFile>()
        {
            PAC_MAN_CODE_1,
            PAC_MAN_CODE_2,
            PAC_MAN_CODE_3,
            PAC_MAN_CODE_4,
            PAC_MAN_COLOR,
            PAC_MAN_PALETTE,
            PAC_MAN_TILE,
            PAC_MAN_SPRITE,
            PAC_MAN_SOUND_1,
            PAC_MAN_SOUND_2,
        };

        public static readonly List<ROMFile> MS_PAC_MAN = new List<ROMFile>()
        {
            PAC_MAN_CODE_1,
            PAC_MAN_CODE_2,
            PAC_MAN_CODE_3,
            PAC_MAN_CODE_4,
            PAC_MAN_COLOR,
            PAC_MAN_PALETTE,
            MS_PAC_MAN_TILE,
            MS_PAC_MAN_SPRITE,
            PAC_MAN_SOUND_1,
            PAC_MAN_SOUND_2,
            MS_PAC_MAN_AUX_U5,
            MS_PAC_MAN_AUX_U6,
            MS_PAC_MAN_AUX_U7,
        };
    }
}
