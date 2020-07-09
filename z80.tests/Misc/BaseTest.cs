
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
        protected static byte[] AssembleSource(string source)
        {
            var tempFilePath = Path.GetTempFileName();
            var sourceFilePath = tempFilePath;
            var romFilePath = Path.Combine(Path.GetDirectoryName(tempFilePath), Path.GetFileNameWithoutExtension(tempFilePath) + ".rom");

            File.WriteAllText(sourceFilePath, source);

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = GetAssemblerBinaryPath();
            startInfo.Arguments = $"--z80 \"{sourceFilePath}\" --ixcbr2";
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

        #region Execute Overloads

        // These are overloads for the Execute(CPU) method. They are very messy! These have all came about
        // over time as the codebase has evolved from re-use for each emulator project the tests have had
        // to support (CHIP-8 => Intel 8080 => Zilog 80). They exist primarily so that I don't have go and
        // refactor hundreds of test cases. I concede this is probably confusing and messy... but it's not
        // worth my time to fix; I'm here to learn how to write emulators, not spend time refactoring legacy
        // unit test cases.

        protected CPUResults Execute(byte[] rom, byte[] memory, CPUConfig cpuConfig = null)
        {
            // Map the ROM into the memory space.
            Array.Copy(rom, memory, rom.Length);

            return Execute(memory, cpuConfig);
        }

        protected CPUResults Execute(byte[] memory, CPUConfig cpuConfig = null)
        {
            var cpu = new CPU(cpuConfig ?? new CPUConfig());

            var memoryImplementation = cpu.Memory as SimpleMemory;

            // Map the ROM into the memory space.
            if (memory.Length > memoryImplementation.Memory.Length)
            {
                memoryImplementation = new SimpleMemory(memory);
            }
            else
                Array.Copy(memory, memoryImplementation.Memory, memory.Length);

            return Execute(memoryImplementation.Memory, cpu);
        }

        protected CPUResults Execute(byte[] rom, byte[] memory, CPU cpu)
        {
            // Map the ROM into the memory space.
            Array.Copy(rom, memory, rom.Length);

            return Execute(memory, cpu);
        }

        protected CPUResults Execute(byte[] memory, CPU cpu)
        {
            var memoryImplementation = new SimpleMemory(memory);
            cpu.Memory = memoryImplementation;

            return Execute(cpu);
        }

        #endregion

        /**
         * Used to start a unit test run with the given CPU instance. The instance is expected to
         * have a memory implementation loaded with a ROM and registers (program counter etc) set
         * to appropriate values. After the execution completes, a results object will be returned
         * which contains the state of the CPU that can be used for assertions.
         */
        protected CPUResults Execute(CPU cpu)
        {
            // Record the number of iterations (instructions), CPU cycles, and the address of the
            // program counter after each instruction is executed. This allows tests to assert each
            // of these values in addition to the CPU state.
            var iterations = 0;
            var cycles = 0;
            var pcAddresses = new List<UInt16>();

            while (!cpu.Halted)
            {
                // Ensure we don't have a run away program.
                if (iterations > 100)
                    throw new Exception("More than 100 iterations occurred.");

                pcAddresses.Add(cpu.Registers.PC);

                cycles += cpu.Step();

                iterations++;
            }

            // Return the state of the CPU so tests can do verification.

            var results = new CPUResults()
            {
                Iterations = iterations,
                Cycles = cycles,
                ProgramCounterAddresses = pcAddresses,
                Memory = (cpu.Memory as SimpleMemory).Memory,
                Registers = cpu.Registers,
                Flags = cpu.Flags,
                ProgramCounter = cpu.Registers.PC,
                StackPointer = cpu.Registers.SP,
                InterruptsEnabled = cpu.InterruptsEnabled,
                InterruptsEnabledPreviousValue = cpu.InterruptsEnabledPreviousValue,
                InterruptMode = cpu.InterruptMode,
            };

            return results;
        }

        protected void AssertFlagsFalse(CPUResults results)
        {
            Assert.False(results.Flags.Sign);
            Assert.False(results.Flags.Zero);
            Assert.False(results.Flags.HalfCarry);
            Assert.False(results.Flags.ParityOverflow);
            Assert.False(results.Flags.Subtract);
            Assert.False(results.Flags.Carry);
        }

        protected void AssertFlagsSame(CPUConfig initialState, CPUResults results)
        {
            Assert.Equal(initialState.Flags.Sign, results.Flags.Sign);
            Assert.Equal(initialState.Flags.Zero, results.Flags.Zero);
            Assert.Equal(initialState.Flags.HalfCarry, results.Flags.HalfCarry);
            Assert.Equal(initialState.Flags.ParityOverflow, results.Flags.ParityOverflow);
            Assert.Equal(initialState.Flags.Subtract, results.Flags.Subtract);
            Assert.Equal(initialState.Flags.Carry, results.Flags.Carry);
        }
    }
}
