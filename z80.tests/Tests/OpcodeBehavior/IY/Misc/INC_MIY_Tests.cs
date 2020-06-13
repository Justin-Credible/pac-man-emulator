using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class INC_MIY_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0 });
            list.Add(new object[] { 1 });
            list.Add(new object[] { 2 });
            list.Add(new object[] { 27 });
            list.Add(new object[] { -33 });
            list.Add(new object[] { -62 });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_INC_MIY_NoFlags(int offset)
        {
            var rom = AssembleSource($@"
                org 00h
                INC (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var registers = new CPURegisters()
            {
                IY = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477 + offset] = 0x42;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x43, state.Memory[0x2477 + offset]);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }

        // TODO: This should detect overflow and not parity
        // TODO: Also detect Half-carry.
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestINR_MIY_ParityFlag(int offset)
        {
            var rom = AssembleSource($@"
                org 00h
                INC (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var registers = new CPURegisters()
            {
                IY = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477 + offset] = 0x43;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x44, state.Memory[0x2477 + offset]);

            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestINR_MIY_SignFlag(int offset)
        {
            var rom = AssembleSource($@"
                org 00h
                INC (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var registers = new CPURegisters()
            {
                IY = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477 + offset] = 0x7F;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x80, state.Memory[0x2477 + offset]);

            Assert.True(state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestINR_MIY_ZeroButNoCarryFlag(int offset)
        {
            var rom = AssembleSource($@"
                org 00h
                INC (IY {(offset < 0 ? '-' : '+')} {Math.Abs(offset)})
                HALT
            ");

            var registers = new CPURegisters()
            {
                IY = 0x2477,
            };

            var memory = new byte[16384];
            memory[0x2477 + offset] = 0xFF;

            var initialState = new CPUConfig()
            {
                Registers = registers,
                MemorySize = memory.Length,
                Flags = new ConditionFlags()
                {
                    Subtract = true,
                },
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x00, state.Memory[0x2477 + offset]);

            Assert.False(state.Flags.Sign);
            Assert.True(state.Flags.Zero);
            Assert.False(state.Flags.HalfCarry);
            Assert.True(state.Flags.ParityOverflow);
            Assert.False(state.Flags.Subtract);
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 23, state.Cycles);
            Assert.Equal(0x03, state.Registers.PC);
        }
    }
}
