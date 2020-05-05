using System;
using System.IO;
using System.Text;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CPUIntegrationTest : BaseTest
    {
        private const int MAX_INSTRUCTIONS = 10000;

        // TODO: Uncomment once we're ready to pass the test.
        // [Theory]
        //[InlineData("zexdoc.com")]
        //[InlineData("zexall.com")]
        public void IntegrationTest(string binaryPath)
        {
            var originalBinary = File.ReadAllBytes($"../../../ZEX/{binaryPath}");

            // The assembled version is expected to be loaded at 0x100. So here
            // we prepend 256 empty bytes by copying into a new array.
            var memory = new byte[0x100 + originalBinary.Length];
            Array.Copy(originalBinary, 0, memory, 0x100, originalBinary.Length);

            var cpuConfig = GetCPUConfig();

            // Ensure we're running the CPU in a special diagnostics mode.
            // This allows for special behavior as if CP/M was running our
            // program (e.g. JMP 0x00 exits and CALL 0x05 prints a message).
            cpuConfig.EnableDiagnosticsMode = true;

            // Disable memory write protection.
            cpuConfig.WriteableMemoryStart = 0;
            cpuConfig.WriteableMemoryEnd = 0;

            // The ZEX program is assembled with it's first instruction at $100.
            cpuConfig.ProgramCounter = 0x100;

            // Values as per: https://floooh.github.io/2016/07/12/z80-rust-ms1.html
            cpuConfig.MemorySize = 64 * 1024;
            // cpuConfig.StackPointer = 0xF000;

            // Initialize the CPU with the configuration we just built.
            var cpu = new CPU(cpuConfig);

            /**
             * Both zexall and zexdoc attempt to load the stack pointer with the value
             * of memory location 6 immediately after jumping to the start of the program.
             *
             *	org	100h
             *	jp	start
             *	start:	ld	hl,(6)
             *	ld	sp,hl
             *
             * Therefore initializing the stack pointer to 0xF000 above is useless. We'll
             * just inject the 0xF000 value into memory location 6 here so it will be used.
             */
            memory[6] = 0x00;
            memory[7] = 0xF0;

            // Load the CPU's memory with the test program.
            cpu.LoadMemory(memory);

            var passed = false;
            string consoleOutput = "";

            // This event handler will fire when CALL 0x05 is executed.
            // Based on the output string from the program we know if the test
            // suite passed or not.
            cpu.OnCPUDiagDebugEvent += (int eventID) =>
            {
                if (eventID == 2)
                {
                    // This appears to be a character printing routine using the value
                    // of the A register. On failures, this appends an exit code to the
                    // console output.
                    consoleOutput += (char)cpu.Registers.A;
                }
                else if (eventID == 9)
                {
                    // Apparently the string to be printed by CP/M is pointed at by
                    // the address stored in the DE register pair. Here we fetch it.
                    consoleOutput = FetchString(cpu, cpu.Registers.DE);
                    Console.WriteLine(consoleOutput);

                    // The cpudiag.bin program prints this text if the tests pass.
                    // TODO: This was for the 8080 CPUDiag program; update for whatever
                    // the success text is for ZEX.
                    if (consoleOutput.Contains("CPU IS OPERATIONAL"))
                        passed = true;
                }
            };

            // Keep track of how many instructions we've executed so we can
            // attempt to detect an infinite loop or other anomalies.
            var instructionCount = 0;

            // Now run the program!
            while (instructionCount < MAX_INSTRUCTIONS)
            {
                cpu.Step();
                instructionCount++;

                if (cpu.Finished)
                    break;
            }

            Assert.True(instructionCount < MAX_INSTRUCTIONS, "Sanity check to ensure we didn't get in an infinite loop.");
            Assert.True(cpu.Finished);
            Assert.True(passed, $"ZEX console output was: '{consoleOutput}'");
        }

        private string FetchString(CPU cpu, UInt16 ptr)
        {
            var sb = new StringBuilder();

            while (true)
            {
                var c = (char)cpu.Memory[ptr];

                // The ZEX program terminates strings with the dollar sign.
                if (c == '$')
                    break;
                else
                    sb.Append(c);

                ptr++;
            }

            var result = sb.ToString();

            // \f\r\n Seems to be the prefix to all assembled strings?
            // We'll just strip them out here.
            if (result.StartsWith("\f\r\n"))
                result = result.Substring(3);

            return result;
        }
    }
}
