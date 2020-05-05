
# Z80 Instruction Set Excerciser

This directory contains diagnostics programs used by the [`CPUIntegrationTest`](../Tests/CPUIntegrationTest.cs) unit test. These are Z80 programs that exercises all of the opcodes.

> ZEX is the ‘Z80 instruction set exerciser’, a hallowed ancient CP/M* program written by one Mr. Frank D. Cringle in 1994 to run through (nearly) all Z80 instructions and compare the resulting CPU state against a real Z80 CPU. ZEXDOC is masking out the 2 undocumented flag bits XF and YF, while ZEXALL is testing all flag bits.
> 
> *CP/M was the dominating business operating system for 8-bit machines in the 70’s, before there was MS-DOS.

Description is from: https://floooh.github.io/2016/07/12/z80-rust-ms1.html

Source and binary files are from:
* https://github.com/anotherlin/z80emu/tree/master/testfiles
* http://mdfs.net/Software/Z80/Exerciser/
