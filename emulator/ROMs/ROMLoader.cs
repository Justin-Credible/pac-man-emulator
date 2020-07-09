using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;

namespace JustinCredible.PacEmu
{
    /**
     * Used to load a set of ROMs into memory.
     */
    public class ROMLoader
    {
        // TODO: Use an enum to identify which ROM set to load for other games.
        public static ROMData LoadFromDisk(string directoryPath, bool enforceValidChecksum = true)
        {
            var romData = new ROMData();

            foreach (var romFile in ROMs.PAC_MAN)
            {
                var path = Path.Join(directoryPath, romFile.FileName);
                var alternatePath = Path.Join(directoryPath, romFile.AlternateFileName);

                byte[] rom = null;

                // Attempt to load the ROM file data from the primary and secondary file names.

                if (File.Exists(path))
                    rom = File.ReadAllBytes(path);

                if (File.Exists(alternatePath))
                    rom = File.ReadAllBytes(alternatePath);

                if (rom == null)
                {
                    var alternateFileNameMessage = romFile.AlternateFileName == null ? "" : $"(or alternate name '{romFile.AlternateFileName}')";
                    throw new Exception($"Could not locate the '{romFile.Description}' ROM file '{romFile.FileName}'{alternateFileNameMessage} with CRC32 of '{romFile.CRC32}' at the location: {path}");
                }

                // Perform a quick checksum to determine if we got the correct file.

                var crc32 = new CRC32();
                var checksum = crc32.Get(rom).ToString("X");

                if (checksum != romFile.CRC32)
                {
                    var alternateFileNameMessage = romFile.AlternateFileName == null ? "" : $"(or alternate name '{romFile.AlternateFileName}')";
                    var message = $"The CRC32 checksum for '{romFile.Description}' ROM file '{romFile.FileName}'{alternateFileNameMessage} at the location: {path} was calculated as '{checksum}', but we are expecting '{romFile.CRC32}'.";

                    if (enforceValidChecksum)
                        throw new Exception(message);
                    else
                        Console.WriteLine("[WARNING] {message}");
                }

                // Add the binary ROM data to the set indexed by file name.
                romData.Data[romFile.FileName] = rom;
            }

            return romData;
        }
    }
}
