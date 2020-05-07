
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class BaseTest
    {
        protected CPUConfig GetCPUConfig()
        {
            // Uses the defaults from the "overridden" unit test CPUConfig.
            return new CPUConfig();
        }

        protected static byte[] AssembleSource(string source)
        {
            var tempFilePath = Path.GetTempFileName();
            var sourceFilePath = tempFilePath;
            var romFilePath = Path.Combine(Path.GetDirectoryName(tempFilePath), Path.GetFileNameWithoutExtension(tempFilePath) + ".rom");

            File.WriteAllText(sourceFilePath, source);

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = GetAssemblerBinaryPath();
            startInfo.Arguments = $"--z80 \"{sourceFilePath}\"";
            startInfo.RedirectStandardError = true;

            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            var stdErr = process.StandardError.ReadToEnd();
            var exitCode = process.ExitCode;

            if (exitCode != 0)
                throw new Exception($"Error assembling Zilog Z80 source code; non-zero exit code: {exitCode}; stdErr: {stdErr}");

            return File.ReadAllBytes(romFilePath);
        }

        private static String GetAssemblerBinaryPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "../../../../assembler/zasm-4.2.4-Linux64/zasm";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "../../../../assembler/zasm-4.2.4-macos10.12/zasm";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new Exception("The zasm assembler is not available for Windows; try using the Windows Subsystem for Linux (WSL).");
            else
                throw new Exception("The zasm assembler is not available this platform.");
        }

        protected CPUResults Execute(byte[] rom, byte[] memory, CPUConfig cpuConfig = null)
        {
            // Map the ROM into the memory space.
            Array.Copy(rom, memory, rom.Length);

            return Execute(memory, cpuConfig);
        }

        protected CPUResults Execute(byte[] memory, CPUConfig cpuConfig = null)
        {
            var cpu = new CPU(cpuConfig ?? GetCPUConfig());

            return Execute(memory, cpu);
        }

        protected CPUResults Execute(byte[] rom, byte[] memory, CPU cpu)
        {
            // Map the ROM into the memory space.
            Array.Copy(rom, memory, rom.Length);

            return Execute(memory, cpu);
        }

        protected CPUResults Execute(byte[] memory, CPU cpu)
        {
            cpu.LoadMemory(memory);

            // Record the number of iterations (instructions), CPU cycles, and the address of the
            // program counter after each instruction is executed. This allows tests to assert each
            // of these values in addition to the CPU state.
            var iterations = 0;
            var cycles = 0;
            var pcAddresses = new List<UInt16>();

            while (!cpu.Finished)
            {
                // Ensure we don't have a run away program.
                if (iterations > 100)
                    throw new Exception("More than 100 iterations occurred.");

                pcAddresses.Add(cpu.ProgramCounter);

                cycles += cpu.Step();

                iterations++;
            }

            // Return the state of the CPU so tests can do verification.

            var results = new CPUResults()
            {
                Iterations = iterations,
                Cycles = cycles,
                ProgramCounterAddresses = pcAddresses,
                Memory = cpu.Memory,
                Registers = cpu.Registers,
                ShadowRegisters = cpu.ShadowRegisters,
                Flags = cpu.Flags,
                ShadowFlags = cpu.ShadowFlags,
                MemoryRefresh = cpu.MemoryRefresh, // TODO
                IndexIX = cpu.IndexIX, // TODO
                IndexIY = cpu.IndexIY, // TODO
                ProgramCounter = cpu.ProgramCounter,
                StackPointer = cpu.StackPointer,
                InterruptsEnabled = cpu.InterruptsEnabled,
            };

            return results;
        }

        protected void AssertFlagsFalse(CPUResults results)
        {
            Assert.False(results.Flags.Sign);
            Assert.False(results.Flags.Zero);
            Assert.False(results.Flags.AuxCarry);
            Assert.False(results.Flags.Parity);
            Assert.False(results.Flags.Subtract);
            Assert.False(results.Flags.Carry);
        }

        protected void AssertFlagsSame(CPUConfig initialState, CPUResults results)
        {
            Assert.Equal(initialState.Flags.Sign, results.Flags.Sign);
            Assert.Equal(initialState.Flags.Zero, results.Flags.Zero);
            Assert.Equal(initialState.Flags.AuxCarry, results.Flags.AuxCarry);
            Assert.Equal(initialState.Flags.Parity, results.Flags.Parity);
            Assert.Equal(initialState.Flags.Subtract, results.Flags.Subtract);
            Assert.Equal(initialState.Flags.Carry, results.Flags.Carry);
        }
    }
}
