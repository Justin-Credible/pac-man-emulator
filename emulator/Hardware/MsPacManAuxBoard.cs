
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using JustinCredible.Z80Disassembler;
using JustinCredible.ZilogZ80;

namespace JustinCredible.PacEmu
{
    /**
     * Ms. Pac-Man is simply a daughterboard plugged in via a ribbon cable to the original
     * Pac-Man PCB at the Z80 CPU socket. This handles some of the daughterboard's logic.
     */
    public class MsPacManAuxBoard
    {
        public byte[] AuxROMs = null;

        public void LoadAuxROMs(ROMData romData)
        {
            // Decrypt Ms. Pac-Man aux board ROMs U5, U6, U7.

            var u5 = romData.Data[ROMs.MS_PAC_MAN_AUX_U5.FileName];
            var u6 = romData.Data[ROMs.MS_PAC_MAN_AUX_U6.FileName];
            var u7 = romData.Data[ROMs.MS_PAC_MAN_AUX_U7.FileName];

            // Original ROMs: 16K
            // New ROMs: 10K
            AuxROMs = new byte[(16 + 10) * 1024];

            for (var i = 0; i < 0x1000; i++)
            {
                AuxROMs[DecryptAddress1((uint)i)+0x4000] = (byte)DecryptData(u7[i]);
                AuxROMs[DecryptAddress1((uint)i)+0x5000] = (byte)DecryptData(u6[i]);
            }

            for (var i = 0; i < 0x0800; i++)
                AuxROMs[DecryptAddress2((uint)i)+0x6000] = (byte)DecryptData(u5[i]);

            // Copy the original Pac-Man ROM, expect for code ROM 4 (6J) which is replaced
            // completely by aux ROM U7. We'll also be applying patches to these below.

            var codeRom1 = romData.Data[ROMs.PAC_MAN_CODE_1.FileName];
            var codeRom2 = romData.Data[ROMs.PAC_MAN_CODE_2.FileName];
            var codeRom3 = romData.Data[ROMs.PAC_MAN_CODE_3.FileName];

            Array.Copy(codeRom1, 0, AuxROMs, 0x0000, 0x1000);
            Array.Copy(codeRom2, 0, AuxROMs, 0x1000, 0x1000);
            Array.Copy(codeRom3, 0, AuxROMs, 0x2000, 0x1000);
            Array.Copy(AuxROMs, 0x4000, AuxROMs, 0x3000, 0x1000);

            // The U5 ROM contains patches to the original Pac-Man ROMs 1-3 (6E/6F/6H).
            // This list of patch locations was determined by looking at MAME and PIE.
            // https://www.walkofmind.com/programming/pie/pie.htm
            // https://github.com/mamedev/mame/blob/master/src/mame/drivers/pacman.cpp#L7366

            // Technically the aux board has latches that "overlay" reads from these addresses
            // however, it's good enough to just replace the bytes in our case.

            for (var i = 0; i < 8; i++)
            {
                AuxROMs[0x0410+i] = AuxROMs[0x6008+i];
                AuxROMs[0x08E0+i] = AuxROMs[0x61D8+i];
                AuxROMs[0x0A30+i] = AuxROMs[0x6118+i];
                AuxROMs[0x0BD0+i] = AuxROMs[0x60D8+i];
                AuxROMs[0x0C20+i] = AuxROMs[0x6120+i];
                AuxROMs[0x0E58+i] = AuxROMs[0x6168+i];
                AuxROMs[0x0EA8+i] = AuxROMs[0x6198+i];

                AuxROMs[0x1000+i] = AuxROMs[0x6020+i];
                AuxROMs[0x1008+i] = AuxROMs[0x6010+i];
                AuxROMs[0x1288+i] = AuxROMs[0x6098+i];
                AuxROMs[0x1348+i] = AuxROMs[0x6048+i];
                AuxROMs[0x1688+i] = AuxROMs[0x6088+i];
                AuxROMs[0x16B0+i] = AuxROMs[0x6188+i];
                AuxROMs[0x16D8+i] = AuxROMs[0x60C8+i];
                AuxROMs[0x16F8+i] = AuxROMs[0x61C8+i];
                AuxROMs[0x19A8+i] = AuxROMs[0x60A8+i];
                AuxROMs[0x19B8+i] = AuxROMs[0x61A8+i];

                AuxROMs[0x2060+i] = AuxROMs[0x6148+i];
                AuxROMs[0x2108+i] = AuxROMs[0x6018+i];
                AuxROMs[0x21A0+i] = AuxROMs[0x61A0+i];
                AuxROMs[0x2298+i] = AuxROMs[0x60A0+i];
                AuxROMs[0x23E0+i] = AuxROMs[0x60E8+i];
                AuxROMs[0x2418+i] = AuxROMs[0x6000+i];
                AuxROMs[0x2448+i] = AuxROMs[0x6058+i];
                AuxROMs[0x2470+i] = AuxROMs[0x6140+i];
                AuxROMs[0x2488+i] = AuxROMs[0x6080+i];
                AuxROMs[0x24B0+i] = AuxROMs[0x6180+i];
                AuxROMs[0x24D8+i] = AuxROMs[0x60C0+i];
                AuxROMs[0x24F8+i] = AuxROMs[0x61C0+i];
                AuxROMs[0x2748+i] = AuxROMs[0x6050+i];
                AuxROMs[0x2780+i] = AuxROMs[0x6090+i];
                AuxROMs[0x27B8+i] = AuxROMs[0x6190+i];
                AuxROMs[0x2800+i] = AuxROMs[0x6028+i];
                AuxROMs[0x2B20+i] = AuxROMs[0x6100+i];
                AuxROMs[0x2B30+i] = AuxROMs[0x6110+i];
                AuxROMs[0x2BF0+i] = AuxROMs[0x61D0+i];
                AuxROMs[0x2CC0+i] = AuxROMs[0x60D0+i];
                AuxROMs[0x2CD8+i] = AuxROMs[0x60E0+i];
                AuxROMs[0x2CF0+i] = AuxROMs[0x61E0+i];
                AuxROMs[0x2D60+i] = AuxROMs[0x6160+i];
            }
        }

        #region Decryption Algorithms

        // The following are from https://www.walkofmind.com/programming/pie/pie.htm

        private uint DecryptData(uint e)
        {
            uint d;

            d  = (e & 0xC0) >> 3;
            d = d | (e & 0x10) << 2;
            d = d | (e & 0x0E) >> 1;
            d = d | (e & 0x01) << 7;
            d = d | (e & 0x20);

            return d;
        }

        private uint DecryptAddress1(uint e)
        {
            uint d;

            d  = (e & 0x807);
            d = d | (e & 0x400) >> 7;
            d = d | (e & 0x200) >> 2;
            d = d | (e & 0x080) << 3;
            d = d | (e & 0x040) << 2;
            d = d | (e & 0x138) << 1;

            return d;
        }

        private uint DecryptAddress2(uint e)
        {
            uint d;

            d  = (e & 0x807);
            d = d | (e & 0x040) << 4;
            d = d | (e & 0x100) >> 3;
            d = d | (e & 0x080) << 2;
            d = d | (e & 0x600) >> 2;
            d = d | (e & 0x028) << 1;
            d = d | (e & 0x010) >> 1;

            return d;
        }

        #endregion
    }
}
