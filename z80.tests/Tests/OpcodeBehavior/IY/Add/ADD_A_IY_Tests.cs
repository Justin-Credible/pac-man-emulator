using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class ADD_A_IY_Tests : BaseTest
    {
        [Fact]
        public void Test_ADD_A_IY_ExampleFromManual()
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, (IY + 5h)
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x1005] = 0x22;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = 0x11,
                    IY = 0x1000,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x33, state.Registers.A);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }

        public static IEnumerable<object[]> GetData()
        {
            var offsets = new List<int>() { 0, 1, 2, 27, -33, -62 };
            var list = new List<object[]>();

            foreach (var offset in offsets)
            {
                list.Add(new object[] { offset, 0x42, 0x19, 0x5B, new ConditionFlags() { Carry = false, Zero = false, Sign = false } });
                list.Add(new object[] { offset, 0x42, 0x4A, 0x8C, new ConditionFlags() { Carry = false, Zero = false, Sign = true } });
                list.Add(new object[] { offset, 0xEE, 0x1D, 0x0B, new ConditionFlags() { Carry = true, Zero = false, Sign = false } });
                list.Add(new object[] { offset, 0xEE, 0x12, 0x00, new ConditionFlags() { Carry = true, Zero = true, Sign = false } });
            }

            return list;
        }

        // TODO: Test Parity/Overflow and AuxCarry flags.
        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_ADD_A_IY(int offset, byte initialValue, byte valueToAdd, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                ADD A, (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234 + offset] = valueToAdd;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    A = initialValue,
                    IY = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = !expectedFlags.Carry,
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    // Parity = !expectedFlags.Parity,

                    // Should be reset.
                    Subtract = true,

                    // AuxCarry = ???
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(expectedValue, state.Registers.A);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            // Assert.Equal(expectedFlags.Parity, state.Flags.Parity);

            // Should be reset.
            Assert.False(state.Flags.Subtract);

            // Assert.False(state.Flags.AuxCarry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 19, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
