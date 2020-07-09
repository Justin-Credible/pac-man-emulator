using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CPUIntegrationTest : BaseTest
    {
        public static IEnumerable<object[]> GetTestDataForAllDocumentedBehaviors()
        {
            return new List<object[]>()
            {
                new object[] { false, null, "ALL", "ALL" }
            };
        }

        public static IEnumerable<object[]> GetTestDataForAllUndocumentedBehaviors()
        {
            return new List<object[]>()
            {
                new object[] { true, null, "ALL", "ALL" }
            };
        }

        public static IEnumerable<object[]> GetTestDataForIndividualDocumentedBehaviors()
        {
            var list = new List<object[]>();
            list.AddRange(GetIndividualTestCases(false));
            return list;
        }

        public static IEnumerable<object[]> GetTestDataForIndividualUndocumentedBehaviors()
        {
            var list = new List<object[]>();
            list.AddRange(GetIndividualTestCases(true));
            return list;
        }

        public static IEnumerable<object[]> GetIndividualTestCases(bool testUndocumented)
        {
            var list = new List<object[]>();

            list.Add(new object[] { testUndocumented, 0, "adc16", "<adc,sbc> hl,<bc,de,hl,sp>...." });
            list.Add(new object[] { testUndocumented, 1, "add16", "add hl,<bc,de,hl,sp>" });
            list.Add(new object[] { testUndocumented, 2, "add16x", "add ix,<bc,de,ix,sp>" });
            list.Add(new object[] { testUndocumented, 3, "add16y", "add iy,<bc,de,iy,sp>" });
            list.Add(new object[] { testUndocumented, 4, "alu8i", "aluop a,nn" });
            list.Add(new object[] { testUndocumented, 5, "alu8r", "aluop a,<b,c,d,e,h,l,(hl),a>" });
            list.Add(new object[] { testUndocumented, 6, "alu8rx", "aluop a,<ixh,ixl,iyh,iyl>" });
            list.Add(new object[] { testUndocumented, 7, "alu8x", "aluop a,(<ix,iy>+1)" });
            list.Add(new object[] { testUndocumented, 8, "bitx", "bit n,(<ix,iy>+1)" });
            list.Add(new object[] { testUndocumented, 9, "bitz80", "bit n,<b,c,d,e,h,l,(hl),a>" });
            list.Add(new object[] { testUndocumented, 10, "cpd1", "cpd<r>" });
            list.Add(new object[] { testUndocumented, 11, "cpi1", "cpi<r>" });
            list.Add(new object[] { testUndocumented, 12, "daa", "<daa,cpl,scf,ccf>" });
            list.Add(new object[] { testUndocumented, 13, "inca", "<inc,dec> a" });
            list.Add(new object[] { testUndocumented, 14, "incb", "<inc,dec> b" });
            list.Add(new object[] { testUndocumented, 15, "incbc", "<inc,dec> bc" });
            list.Add(new object[] { testUndocumented, 16, "incc", "<inc,dec> c" });
            list.Add(new object[] { testUndocumented, 17, "incd", "<inc,dec> d" });
            list.Add(new object[] { testUndocumented, 18, "incde", "<inc,dec> de" });
            list.Add(new object[] { testUndocumented, 19, "ince", "<inc,dec> e" });
            list.Add(new object[] { testUndocumented, 20, "inch", "<inc,dec> h" });
            list.Add(new object[] { testUndocumented, 21, "inchl", "<inc,dec> hl" });
            list.Add(new object[] { testUndocumented, 22, "incix", "<inc,dec> ix" });
            list.Add(new object[] { testUndocumented, 23, "inciy", "<inc,dec> iy" });
            list.Add(new object[] { testUndocumented, 24, "incl", "<inc,dec> l" });
            list.Add(new object[] { testUndocumented, 25, "incm", "<inc,dec> (hl)" });
            list.Add(new object[] { testUndocumented, 26, "incsp", "<inc,dec> sp" });
            list.Add(new object[] { testUndocumented, 27, "incx", "<inc,dec> (<ix,iy>+1)" });
            list.Add(new object[] { testUndocumented, 28, "incxh", "<inc,dec> ixh" });
            list.Add(new object[] { testUndocumented, 29, "incxl", "<inc,dec> ixl" });
            list.Add(new object[] { testUndocumented, 30, "incyh", "<inc,dec> iyh" });
            list.Add(new object[] { testUndocumented, 31, "incyl", "<inc,dec> iyl" });
            list.Add(new object[] { testUndocumented, 32, "ld161", "ld <bc,de>,(nnnn)" });
            list.Add(new object[] { testUndocumented, 33, "ld162", "ld hl,(nnnn)" });
            list.Add(new object[] { testUndocumented, 34, "ld163", "ld sp,(nnnn)" });
            list.Add(new object[] { testUndocumented, 35, "ld164", "ld <ix,iy>,(nnnn)" });
            list.Add(new object[] { testUndocumented, 36, "ld165", "ld (nnnn),<bc,de>" });
            list.Add(new object[] { testUndocumented, 37, "ld166", "ld (nnnn),hl" });
            list.Add(new object[] { testUndocumented, 38, "ld167", "ld (nnnn),sp" });
            list.Add(new object[] { testUndocumented, 39, "ld168", "ld (nnnn),<ix,iy>" });
            list.Add(new object[] { testUndocumented, 40, "ld16im", "ld <bc,de,hl,sp>,nnnn" });
            list.Add(new object[] { testUndocumented, 41, "ld16ix", "ld <ix,iy>,nnnn" });
            list.Add(new object[] { testUndocumented, 42, "ld8bd", "ld a,<(bc),(de)>" });
            list.Add(new object[] { testUndocumented, 43, "ld8im", "ld <b,c,d,e,h,l,(hl),a>,nn" });
            list.Add(new object[] { testUndocumented, 44, "ld8imx", "ld (<ix,iy>+1),nn" });
            list.Add(new object[] { testUndocumented, 45, "ld8ix1", "ld <b,c,d,e>,(<ix,iy>+1)" });
            list.Add(new object[] { testUndocumented, 46, "ld8ix2", "ld <h,l>,(<ix,iy>+1)" });
            list.Add(new object[] { testUndocumented, 47, "ld8ix3", "ld a,(<ix,iy>+1)" });
            list.Add(new object[] { testUndocumented, 48, "ld8ixy", "ld <ixh,ixl,iyh,iyl>,nn" });
            list.Add(new object[] { testUndocumented, 49, "ld8rr", "ld <bcdehla>,<bcdehla>" });
            // list.Add(new object[] { testUndocumented, 50, "ld8rrx", "ld <bcdexya>,<bcdexya>" }); // TODO: ERROR: Missing opcodebyte DD 40? Investigate once interactive debugger is working.
            list.Add(new object[] { testUndocumented, 51, "lda", "ld a,(nnnn) / ld (nnnn),a" });
            list.Add(new object[] { testUndocumented, 52, "ldd1", "ldd<r> (1)" });
            list.Add(new object[] { testUndocumented, 53, "ldd2", "ldd<r> (2)" });
            list.Add(new object[] { testUndocumented, 54, "ldi1", "ldi<r> (1)" });
            list.Add(new object[] { testUndocumented, 55, "ldi2", "ldi<r> (2)" });
            list.Add(new object[] { testUndocumented, 56, "neg", "neg" });
            list.Add(new object[] { testUndocumented, 57, "rld", "<rrd,rld>" });
            list.Add(new object[] { testUndocumented, 58, "rot8080", "<rlca,rrca,rla,rra>" });
            list.Add(new object[] { testUndocumented, 59, "rotxy", "shf/rot (<ix,iy>+1)" });
            list.Add(new object[] { testUndocumented, 60, "rotz80", "shf/rot <b,c,d,e,h,l,(hl),a>" });
            list.Add(new object[] { testUndocumented, 61, "srz80", "<set,res> n,<bcdehl(hl)a>" });
            list.Add(new object[] { testUndocumented, 62, "srzx", "<set,res> n,(<ix,iy>+1)" });
            list.Add(new object[] { testUndocumented, 63, "st8ix1", "ld (<ix,iy>+1),<b,c,d,e>" });
            list.Add(new object[] { testUndocumented, 64, "st8ix2", "ld (<ix,iy>+1),<h,l>" });
            list.Add(new object[] { testUndocumented, 65, "st8ix3", "ld (<ix,iy>+1),a" });
            list.Add(new object[] { testUndocumented, 66, "stabd", "ld (<bc,de>),a" });

            return list;
        }

        [Theory]

        // All tests, run together (only documented opcodes and documented behaviors)
        // [MemberData(nameof(GetTestDataForAllDocumentedBehaviors))]

        // All tests, run together (includes undocumented opcodes and undocumented behaviors)
        // [MemberData(nameof(GetTestDataForAllUndocumentedBehaviors))]

        // All tests, run individually (only documented opcodes and documented behaviors)
        [MemberData(nameof(GetTestDataForIndividualDocumentedBehaviors))]

        // All tests, run individually (only undocumented opcodes and undocumented behaviors)
        // [MemberData(nameof(GetTestDataForIndividualUndocumentedBehaviors))]

        public void IntegrationTest(bool testUndocumented, int? testIndex, string testCaseSourceLabel, string testCaseDescription)
        {
            if (System.Diagnostics.Debugger.IsAttached)
                Console.WriteLine($"Starting test: {testIndex} {testCaseSourceLabel}: {testCaseDescription}");

            // Grab the ROM and its hash based on if we're testing the documented or undocumented behavior.
            var fileName = GetTestProgramFileName(testUndocumented);
            var expectedHash = GetHashForTestProgram(testUndocumented);

            // Read and calculate the MD5 hash to ensure we have the expected ROM. This is important because
            // we may need to modify the ROM based on the given text index to run a single test.
            var testProgramBinary = File.ReadAllBytes($"../../../ZEX/{fileName}");
            var actualHash = ComputeMD5Hash(testProgramBinary);

            if (actualHash != expectedHash)
                throw new Exception($"Expected the Z80 Instruction Exerciser ({fileName}) program to have an MD5 hash of {expectedHash} but saw {actualHash}");

            var cpuConfig = new CPUConfig();

            // Ensure we're running the CPU in a special diagnostics mode.
            // This allows for special behavior as if CP/M was running our
            // program (e.g. JMP 0x00 exits and CALL 0x05 prints a message).
            cpuConfig.EnableDiagnosticsMode = true;

            // The ZEX program is assembled with it's first instruction at $100.
            cpuConfig.Registers.PC = 0x100;

            // Values as per: https://floooh.github.io/2016/07/12/z80-rust-ms1.html
            var memory = new byte[64 * 1024];
            // cpuConfig.StackPointer = 0xF000;

            // The assembled version is expected to be loaded at 0x100. So here
            // we prepend 256 empty bytes by copying into a new array.
            Array.Copy(testProgramBinary, 0, memory, 0x100, testProgramBinary.Length);

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

            // If a text index was supplied, this indicates we only want to run one of the test
            // cases instead of the whole suite. The test suite has 66 different test groups, each
            // identified by a jump vector starting at the tests label. Once the program has reached
            // a pair of 0x00 bytes, the loop will stop and the tests will complete. So if we want
            // to run only a single test, we'll grab the address for that test group, set it as the
            // first address in the jump vector list and then immeidiately set a pair of 0x00 bytes
            // as the second address in the list.
            if (testIndex != null)
            {
                // Sanity check.
                if (testIndex.Value < 0 || testIndex.Value > 66)
                    throw new Exception("The testIndex must be between 0 and 66 (inclusive).");

                // Calculate the address of the first item in the tests jump vector list.
                // The tests jump vector list is 58 bytes into the ROM. I found this by looking for
                // the assembled jp 0 instruction (C3 00 00) which immediately proceeds the tests label.
                var testsVectorListOffset = 0x100 + 58;

                // Calculate the address of the test we want to run. Since each address is two bytes we
                // need to multiply the index by 2 and then add the offset.
                var singleTestVectorListOffset = testsVectorListOffset + (testIndex.Value * 2);

                // Now fetch the upper and lower bytes for the address (little endian).
                var lowerByte = memory[singleTestVectorListOffset];
                var upperByte = memory[singleTestVectorListOffset + 1];

                // Put the address for this single test as the first one in the list.
                memory[testsVectorListOffset] = lowerByte;
                memory[testsVectorListOffset + 1] = upperByte;

                // To ensure we only run a single test, ensure the next address is 0x0000 to stop the loop.
                memory[testsVectorListOffset + 2] = 0x00;
                memory[testsVectorListOffset + 3] = 0x00;
            }

            cpuConfig.Memory = new SimpleMemory(memory);

            // Initialize the CPU with the configuration we just built.
            var cpu = new CPU(cpuConfig);

            // var testsPassed = 0;
            // var testsFailed = 0;
            var testOutputBuilder = new StringBuilder();

            // This event handler will fire when CALL 0x05 is executed.
            // Based on the output string from the program we know if the test
            // suite passed or not.
            cpu.OnCPUDiagDebugEvent += (int eventID) =>
            {
                if (eventID == 2)
                {
                    // This appears to be a character printing routine using the value
                    // of the A register.
                    testOutputBuilder.Append((char)cpu.Registers.A);
                }
                else if (eventID == 9)
                {
                    // Apparently the string to be printed by CP/M is pointed at by
                    // the address stored in the DE register pair. Here we fetch it.
                    var str = FetchString(cpu, cpu.Registers.DE);
                    testOutputBuilder.Append(str);
                }
            };

            // Now run the program!
            while (true)
            {
                cpu.Step();

                if (cpu.Halted)
                    break;
            }

            var testOutput = testOutputBuilder.ToString();

            if (System.Diagnostics.Debugger.IsAttached)
                Console.WriteLine(Environment.NewLine + testOutput);

            var foundSuccess = false;
            var foundError = false;

            if (testOutput.Contains("  OK"))
                foundSuccess = true;

            if (testOutput.Contains("  ERROR"))
                foundError = true;

            Assert.True(cpu.Halted);
            Assert.True(foundSuccess, $"Should have found a success; ZEX console output was: '{testOutput.ToString()}'");
            Assert.False(foundError, $"Should not have found any errors; ZEX console output was: '{testOutput.ToString()}'");
        }

        private string ComputeMD5Hash(byte[] data)
        {
            MD5 md5Hash = MD5.Create();
            var hash = md5Hash.ComputeHash(data);

            var builder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
                builder.Append(hash[i].ToString("x2"));

            return builder.ToString();
        }

        private string GetTestProgramFileName(bool undocumented)
        {
            return undocumented ? "zexall.com" : "zexdoc.com";
        }

        private string GetHashForTestProgram(bool undocumented)
        {
            return undocumented ? "4a32f2a6691b3da5213d7688dda59822" : "be3691b6b910d2c2e05e194cd529e45c";
        }

        private string FetchString(CPU cpu, UInt16 ptr)
        {
            var sb = new StringBuilder();

            while (true)
            {
                var c = (char)cpu.Memory.Read(ptr);

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
