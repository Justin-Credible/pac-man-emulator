using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace JustinCredible.PacEmu
{
    public class Annotations
    {
        /**
         * Used to parse a plain text file containing annotations of the diassembly in the format:
         *     0x1234 .... ; Annotation text
         *
         * Example: https://github.com/BleuLlama/GameDocs/blob/master/disassemble/mspac.asm
         *
         * Returns a dictionary of addresses and the annotation for each address (if any).
         */
        public static Dictionary<UInt16, String> ParseFile(string path)
        {
            var annotations = new Dictionary<UInt16, String>();

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (line.Length < 5)
                    continue;

                var addressRegEx = new Regex("^[0-9A-F]{4}$");

                try
                {
                    if (line.Length < 4)
                        continue;

                    var addressString = line.Substring(0, 4).ToUpper();

                    if (!addressRegEx.IsMatch(addressString))
                        continue;

                    var address = Convert.ToUInt16(addressString, 16);

                    var parts = line.Split(';');

                    if (parts.Length < 2)
                        continue;

                    annotations.Add(address, parts[1]);
                }
                catch
                {
                    // Do nothing; the annotation file can vary wildly with new lines,
                    // lines that are just comments, labels, etc. We only care about a
                    // parsable memory address and if that line has a comment at the end.
                }
            }

            return annotations;
        }
    }
}
