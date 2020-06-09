using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_R_MIY_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                foreach (var register in RegistersClassData.StandardRegisters)
                {
                    list.Add(new object[] { offset, register });
                }
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_LD_R_MIY(int offset, Register register)
        {
            var rom = AssembleSource($@"
                org 00h
                LD {register}, (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = 0x42;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be unaffected.
                    Carry = true,
                    Sign = true,
                    Zero = true,
                    Subtract = true,
                    AuxCarry = true,
                    Parity = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x42, state.Registers[register]);
            Assert.Equal(0x42, state.Memory[0x2234 + offset]);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
