using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_R_R_IY_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { Register.A, Register.IYH });
            list.Add(new object[] { Register.B, Register.IYH });
            list.Add(new object[] { Register.C, Register.IYH });
            list.Add(new object[] { Register.D, Register.IYH });
            list.Add(new object[] { Register.E, Register.IYH });

            list.Add(new object[] { Register.A, Register.IYL });
            list.Add(new object[] { Register.B, Register.IYL });
            list.Add(new object[] { Register.C, Register.IYL });
            list.Add(new object[] { Register.D, Register.IYL });
            list.Add(new object[] { Register.E, Register.IYL });

            list.Add(new object[] { Register.IYH, Register.A });
            list.Add(new object[] { Register.IYH, Register.B });
            list.Add(new object[] { Register.IYH, Register.C });
            list.Add(new object[] { Register.IYH, Register.D });
            list.Add(new object[] { Register.IYH, Register.E });

            list.Add(new object[] { Register.IYL, Register.A });
            list.Add(new object[] { Register.IYL, Register.B });
            list.Add(new object[] { Register.IYL, Register.C });
            list.Add(new object[] { Register.IYL, Register.D });
            list.Add(new object[] { Register.IYL, Register.E });

            list.Add(new object[] { Register.IYH, Register.IYH });
            list.Add(new object[] { Register.IYH, Register.IYL });
            list.Add(new object[] { Register.IYL, Register.IYL });
            list.Add(new object[] { Register.IYL, Register.IYH });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_LD_R_R(Register destination, Register source)
        {
            var rom = AssembleSource($@"
                org 00h
                LD {destination}, {source}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    [source] = 0x42,
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

            var state = Execute(rom, initialState);

            Assert.Equal(0x42, state.Registers[destination]);
            Assert.Equal(0x42, state.Registers[source]);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_IYH_IYL()
        {
            var rom = AssembleSource($@"
                org 00h
                LD IYH, IYL
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x1234,
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

            var state = Execute(rom, initialState);

            Assert.Equal(0x3434, state.Registers.IY);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_IYL_IYH()
        {
            var rom = AssembleSource($@"
                org 00h
                LD IYL, IYH
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x1234,
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

            var state = Execute(rom, initialState);

            Assert.Equal(0x1212, state.Registers.IY);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_SP_IY()
        {
            var rom = AssembleSource($@"
                org 00h
                LD SP, IY
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IY = 0x1234,
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

            var state = Execute(rom, initialState);

            Assert.Equal(0x1234, state.Registers.IY);
            Assert.Equal(0x1234, state.Registers.SP);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
