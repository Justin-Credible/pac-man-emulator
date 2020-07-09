using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

namespace JustinCredible.Z80Disassembler
{
    class Program
    {
        private static CommandLineApplication _app;

        public static void Main(string[] args)
        {
            var version = Utilities.AppVersion;

            _app = new CommandLineApplication();
            _app.Name = "Z80disasm";
            _app.Description = "Z80 Disassembler";
            _app.HelpOption("-?|-h|--help");

            _app.VersionOption("-v|--version",

                // Used for HelpOption() header
                $"{_app.Name} {version}",

                // Used for output of --version option.
                version
            );

            // When launched without any commands or options.
            _app.OnExecute(() =>
            {
                _app.ShowHelp();
                return 0;
            });

            _app.Command("disassemble", Disassemble);

            _app.Execute(args);
        }

        private static void Disassemble(CommandLineApplication command)
        {
            command.Description = "Disassembles the given ROM file.";
            command.HelpOption("-?|-h|--help");

            // TODO: Update for Pac-Man ROM file names.
            var romPathArg = command.Argument("[ROM path]", "The path to the ROM file to disassemble (or directory containing invaders.e through .h).");

            var outputOption = command.Option("-o|--output", "The path to the to output file.", CommandOptionType.SingleValue);
            var includeAddressOption = command.Option("-a|--address", "Include addresses in the disassembly.", CommandOptionType.NoValue);
            var includePseudocodeOption = command.Option("-p|--pseudocode", "Include pseudocode in the disassembly (a comment on each line).", CommandOptionType.NoValue);

            command.OnExecute(() =>
            {
                if (String.IsNullOrWhiteSpace(romPathArg.Value))
                    throw new Exception("A ROM path is required.");

                byte[] rom = null;

                if (File.Exists(romPathArg.Value))
                {
                    Console.WriteLine($"Reading ROM file: {romPathArg.Value}");

                    rom = File.ReadAllBytes(romPathArg.Value);
                }
                else if (Directory.Exists(romPathArg.Value))
                {
                    // TODO: Update for Pac-Man ROM file names.
                    Console.WriteLine("$Reading Pac-Man ROM files from directory: {romPathArg.Value}");
                    Console.WriteLine($"• invaders.e");
                    Console.WriteLine($"• invaders.f");
                    Console.WriteLine($"• invaders.g");
                    Console.WriteLine($"• invaders.h");

                    rom = ReadRomFiles(romPathArg.Value);
                }
                else
                    throw new Exception($"Could not locate a ROM file (or directory containing invaders.e though .h) at the path {romPathArg.Value}");

                Console.WriteLine("Disassembling ROM...");

                var wrappedRom = new SimpleMemory(rom);
                var disassembly = Disassembler.Disassemble(wrappedRom, includeAddressOption.HasValue(), includePseudocodeOption.HasValue());

                Console.WriteLine("Disassembly complete!");

                if (outputOption.HasValue())
                {
                    Console.WriteLine($"Writing output to: {outputOption.Value()}");
                    File.WriteAllText(outputOption.Value(), disassembly);
                }
                else
                {
                    Console.WriteLine(String.Empty);
                    Console.WriteLine(String.Empty);

                    Console.WriteLine(disassembly);

                    Console.WriteLine(String.Empty);
                    Console.WriteLine(String.Empty);
                }

                return 0;
            });
        }

        private static byte[] ReadRomFiles(string directoryPath)
        {
            // TODO: Update for Pac-Man ROM file names.
            var hPath = Path.Join(directoryPath, "invaders.h");
            var gPath = Path.Join(directoryPath, "invaders.g");
            var fPath = Path.Join(directoryPath, "invaders.f");
            var ePath = Path.Join(directoryPath, "invaders.e");

            if (!File.Exists(hPath))
                throw new Exception($"Could not locate {hPath}");

            if (!File.Exists(gPath))
                throw new Exception($"Could not locate {gPath}");

            if (!File.Exists(fPath))
                throw new Exception($"Could not locate {fPath}");

            if (!File.Exists(ePath))
                throw new Exception($"Could not locate {ePath}");

            // TODO: Checksums?

            var bytes = new List<byte>();

            bytes.AddRange(File.ReadAllBytes(hPath));
            bytes.AddRange(File.ReadAllBytes(gPath));
            bytes.AddRange(File.ReadAllBytes(fPath));
            bytes.AddRange(File.ReadAllBytes(ePath));

            return bytes.ToArray();
        }
    }
}
