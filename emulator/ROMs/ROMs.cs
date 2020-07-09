using System.Collections.Generic;

namespace JustinCredible.PacEmu
{
    /**
     * Used to encapsulate each of the ROMs for each game that runs on the Pac-Man hardware.
     */
    public class ROMs
    {
        public static readonly ROMFile PAC_MAN_CODE_1 = new ROMFile() { FileName = "pacman.6e", CRC32 = "C1E6AB10", Description = "Code 1 (4KB)" };
        public static readonly ROMFile PAC_MAN_CODE_2 = new ROMFile() { FileName = "pacman.6f", CRC32 = "1A6FB2D4", Description = "Code 2 (4KB)" };
        public static readonly ROMFile PAC_MAN_CODE_3 = new ROMFile() { FileName = "pacman.6h", CRC32 = "BCDD1BEB", Description = "Code 3 (4KB)" };
        public static readonly ROMFile PAC_MAN_CODE_4 = new ROMFile() { FileName = "pacman.6h", CRC32 = "817D94E3", Description = "Code 4 (4KB)" };
        public static readonly ROMFile PAC_MAN_COLOR = new ROMFile() { FileName = "82s123.7f", CRC32 = "2FC650BD", Description = "Color (32B) (32 one-byte colors)" };
        public static readonly ROMFile PAC_MAN_PALETTE = new ROMFile() { FileName = "82s126.4a", CRC32 = "3EB3A8E4", Description = "Palette (256B) (64 four-byte pallets)" };
        public static readonly ROMFile PAC_MAN_TILE = new ROMFile() { FileName = "pacman.5e", CRC32 = "0C944964", Description = "Tile (4KB) (256 8x8 pixel tile images)" };
        public static readonly ROMFile PAC_MAN_SPRITE= new ROMFile() { FileName = "pacman.5f", CRC32 = "958FEDF9", Description = "Sprite (4KB) (64 16x16 sprite images)" };
        public static readonly ROMFile PAC_MAN_SOUND_1 = new ROMFile() { FileName = "82s126.1m", CRC32 = "A9CC86BF", Description = "Sound 1 (256B) (8 waveforms)" };
        public static readonly ROMFile PAC_MAN_SOUND_2 = new ROMFile() { FileName = "82s126.3m", CRC32 = "77245B66", Description = "Sound 2 (256B) (8 waveforms)" };

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
    }
}
