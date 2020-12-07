using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;

namespace JustinCredible.PacEmu.CLI
{
    /**
     * The main entry point into the desktop version of the emulator which handles
     * parsing CLI arguments and passing them to the emulator library.
     */
    public class Program
    {
        private static CommandLineApplication _app;

        private static string AppVersion
        {
            get
            {
                return Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            }
        }

        public static void Main(string[] args)
        {
            var version = Program.AppVersion;

            _app = new CommandLineApplication();
            _app.Name = "pacemu";
            _app.Description = "Pac-Man Arcade Hardware Emulator";
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

            _app.Command("run", Run);

            _app.Execute(args);
        }

        private static void Run(CommandLineApplication command)
        {
            command.Description = "Runs the emulator using the given ROM files.";
            command.HelpOption("-?|-h|--help");

            var romPathArg = command.Argument("[ROM path]", "The path to a directory containing the ROM set to load.");

            var romsetOption = command.Option("-rs|--rom-set", "The name of an alternative ROM set and/or PCB configuration to use; pacman or mspacman; defaults to pacman", CommandOptionType.SingleValue);
            var dipSwitchesOption = command.Option("-dw|--dip-switches", "The path to a JSON file containing DIP switch settings; defaults to dip-switches.json in CWD.", CommandOptionType.SingleValue);
            var loadStateOption = command.Option("-l|--load-state", "Loads an emulator save state from the given path.", CommandOptionType.SingleValue);
            var skipChecksumsOption = command.Option("-sc|--skip-checksums", "Allow running a ROM with invalid checksums.", CommandOptionType.NoValue);
            var writableRomOption = command.Option("-wr|--writable-rom", "Allow memory writes to the ROM address space.", CommandOptionType.NoValue);
            var debugOption = command.Option("-d|--debug", "Run in debug mode; enables internal statistics and logs useful when debugging.", CommandOptionType.NoValue);
            var breakOption = command.Option("-b|--break", "Used with debug, will break at the given address and allow single stepping opcode execution (e.g. --break 0x0248)", CommandOptionType.MultipleValue);
            var reverseStepOption = command.Option("-rvs|--reverse-step", "Used with debug, allows for single stepping in reverse to rewind opcode execution.", CommandOptionType.NoValue);
            var annotationsPathOption = command.Option("-a|--annotations", "Used with debug, a path to a text file containing memory address annotations for interactive debugging (line format: 0x1234 .... ; Annotation)", CommandOptionType.SingleValue);

            command.OnExecute(() =>
            {
                var config = new EmulatorConfig();

                if (String.IsNullOrWhiteSpace(romPathArg.Value))
                    throw new Exception("A directory containing Pac-Man arcade hardware compatible ROM files is required.");

                if (!Directory.Exists(romPathArg.Value))
                    throw new Exception($"Could not locate a directory at path {romPathArg.Value}");

                config.RomPath = romPathArg.Value;
                config.RomSet = ROMSet.PacMan;

                if (romsetOption.HasValue())
                {
                    if (romsetOption.Value() == "pacman")
                        config.RomSet = ROMSet.PacMan;
                    else if (romsetOption.Value() == "mspacman")
                        config.RomSet = ROMSet.MsPacMan;
                    else
                        throw new ArgumentException($"Unexpected ROM set: {romsetOption.Value()}");
                }

                config.DipSwitchesConfigPath = dipSwitchesOption.HasValue() ? dipSwitchesOption.Value() : null;
                config.LoadStateFilePath = loadStateOption.HasValue() ? loadStateOption.Value() : null;
                config.SkipChecksums = skipChecksumsOption.HasValue();
                config.WritableRom = writableRomOption.HasValue();
                config.Debug = debugOption.HasValue();

                if (config.Debug)
                {
                    if (breakOption.HasValue())
                    {
                        var addresses = new List<UInt16>();

                        foreach (var addressString in breakOption.Values)
                        {
                            UInt16 address = Convert.ToUInt16(addressString, 16);
                            addresses.Add(address);
                        }

                        config.Breakpoints = addresses;
                    }

                    config.ReverseStep = reverseStepOption.HasValue();
                    config.AnnotationsFilePath = annotationsPathOption.HasValue() ? annotationsPathOption.Value() : null;
                }

                Emulator.Start(config);

                return 0;
            });
        }
    }
}
