using System;
using System.Text;
using JustinCredible.ZilogZ80;

namespace JustinCredible.Z80Disassembler
{
    public static class Disassembler
    {

        public static string Disassemble(byte[] rom, bool emitAddresses = false, bool emitPseudocode = false)
        {
            var disassembly = new StringBuilder();

            UInt16 address = 0x0000;

            while (true)
            {
                var instructionSize = 0;
                var instruction = Disassemble(rom, address, out instructionSize, emitAddresses, emitPseudocode);
                disassembly.AppendLine(instruction);

                address += (UInt16)instructionSize;

                if (address >= rom.Length)
                    break;
            }

            return disassembly.ToString();
        }

        public static string Disassemble(byte[] rom, UInt16 address, out int instructionSize, bool emitAddress = false, bool emitPseudocode = false)
        {
            var opcode = Opcodes.GetOpcode(address, rom);

            var disassembly = new StringBuilder();

            if (emitAddress)
                disassembly.AppendFormat("0x{0:X4}\t", address);

            disassembly.Append(opcode.Instruction);

            instructionSize = opcode.Size;

            if (opcode.Size == 2)
            {
                var next = rom[address + 1];
                var dataFormatted = String.Format("0x{0:X2}", next);
                disassembly.Replace("D8", dataFormatted);
            }
            else if (opcode.Size == 3)
            {
                var upper = rom[address + 2] << 8;
                var lower = rom[address + 1];
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
    }
}
