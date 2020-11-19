using System;
using System.Collections.Generic;
using System.Text;
using JustinCredible.ZilogZ80;

namespace JustinCredible.Z80Disassembler
{
    public static class Disassembler
    {
        public static readonly string CURRENT_LINE_MARKER = "---->";

        public static string Disassemble(IMemory rom, bool emitAddresses = false, bool emitPseudocode = false)
        {
            var disassembly = new StringBuilder();

            UInt16 address = 0x0000;

            while (true)
            {
                var instructionSize = 0;
                var instruction = Disassemble(rom, address, out instructionSize, emitAddresses, emitPseudocode);
                disassembly.AppendLine(instruction);

                address += (UInt16)instructionSize;

                try
                {
                    var value = rom.Read(address);
                }
                catch
                {
                    // If we couldn't read any more then we probably reached the end.
                    break;
                }
            }

            return disassembly.ToString();
        }

        public static string Disassemble(IMemory rom, UInt16 address, out int instructionSize, bool emitAddress = false, bool emitPseudocode = false)
        {
            var opcode = Opcodes.GetOpcode(address, rom);

            var disassembly = new StringBuilder();

            if (emitAddress)
                disassembly.AppendFormat("0x{0:X4}\t", address);

            disassembly.Append(opcode.Instruction);

            instructionSize = opcode.Size;

            if (opcode.Size == 2)
            {
                var next = rom.Read(address + 1);
                var dataFormatted = String.Format("0x{0:X2}", next);
                disassembly.Replace("D8", dataFormatted);
            }
            else if (opcode.Size == 3)
            {
                var upper = rom.Read(address + 2) << 8;
                var lower = rom.Read(address + 1);
                var dataOrAddress = (UInt16)(upper | lower);

                var dataOrAddressFormatted = String.Format("0x{0:X4}", dataOrAddress);

                disassembly.Replace("D16", dataOrAddressFormatted);
                disassembly.Replace("adr", dataOrAddressFormatted);
            }
            else if (opcode.Size == 4)
            {
                // TODO: If IX/IY Bit operations, first two bytes are prefixes, fourth is the opcode
                // and the third byte is the relative address offset. In that case parse it out so
                // it can be shown in the disassembly. The follow code assumes the third and fourth
                // bytes are always inline data or an address.

                var upper = rom.Read(address + 3) << 8;
                var lower = rom.Read(address + 2);
                var dataOrAddress = (UInt16)(upper | lower);

                var dataOrAddressFormatted = String.Format("0x{0:X4}", dataOrAddress);

                disassembly.Replace("D16", dataOrAddressFormatted);
                disassembly.Replace("adr", dataOrAddressFormatted);
            }
            else if (opcode.Size != 1)
                throw new Exception($"Unexpected opcode size for opcode: {opcode.Instruction}");

            if (emitPseudocode && !String.IsNullOrWhiteSpace(opcode.Pseudocode))
                disassembly.Append($"\t\t; {opcode.Pseudocode}");

            return disassembly.ToString();
        }

        /**
         * Used to build a visual disassembly of memory locations before and after the given address.
         * Useful for display to an end user inside of a debugger or similar.
         */
        public static string FormatDisassemblyForDisplay(UInt16 address, IMemory memory, int beforeCount = 10, int afterCount = 10, Dictionary<UInt16, String> annotations = null)
        {
            var output = new StringBuilder();

            // Ensure the start and end locations are within range.
            var start = (address - beforeCount < 0) ? 0 : (address - beforeCount);
            // var end = (address + afterCount >= _memory.Length) ? _memory.Length - 1 : (address + afterCount);
            // This could go out of range; so we'll have to watch for IndexOutOfRangeException below and bail early.
            var end = address + afterCount;

            for (var i = start; i < end; i++)
            {
                var addressIndex = (UInt16)i;

                // If this is the current address location, add an arrow pointing to it.
                output.Append(address == addressIndex ? CURRENT_LINE_MARKER : "\t");

                // If we're showing annotations, then don't show the pseudocode.
                var emitPseudocode = annotations == null;

                try
                {
                    // Disasemble the opcode and print it.
                    var instruction = Disassembler.Disassemble(memory, addressIndex, out int instructionLength, true, emitPseudocode);
                    output.Append(instruction);

                    // If we're showing annotations, attempt to look up the annotation for this address.
                    if (annotations != null)
                    {
                        var annotation = annotations.ContainsKey(addressIndex) ? annotations[addressIndex] : null;
                        output.Append("\t\t; ");
                        output.Append(annotation == null ? "???" : annotation);
                    }

                    output.AppendLine();

                    // If the opcode is larger than a single byte, we don't want to print subsequent
                    // bytes as opcodes, so here we print the next address locations as the byte value
                    // in parentheses, and then increment so we can skip disassembly of the data.
                    if (instructionLength == 3)
                    {
                        var upper = memory.Read(addressIndex + 2) << 8;
                        var lower = memory.Read(addressIndex + 1);
                        var combined = (UInt16)(upper | lower);
                        var dataFormatted = String.Format("0x{0:X4}", combined);
                        var address1Formatted = String.Format("0x{0:X4}", addressIndex+1);
                        var address2Formatted = String.Format("0x{0:X4}", addressIndex+2);
                        output.AppendLine($"\t{address1Formatted}\t(D16: {dataFormatted})");
                        output.AppendLine($"\t{address2Formatted}\t");
                        i += 2;
                    }
                    else if (instructionLength == 2)
                    {
                        var dataFormatted = String.Format("0x{0:X2}", memory.Read(addressIndex+1));
                        var addressFormatted = String.Format("0x{0:X4}", addressIndex+1);
                        output.AppendLine($"\t{addressFormatted}\t(D8: {dataFormatted})");
                        i++;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            return output.ToString();
        }
    }
}
