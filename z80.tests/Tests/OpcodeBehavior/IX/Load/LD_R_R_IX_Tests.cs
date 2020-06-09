using System;
using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class LD_R_R_IX : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            list.Add(new object[] { Register.A, Register.IXH });
            list.Add(new object[] { Register.B, Register.IXH });
            list.Add(new object[] { Register.C, Register.IXH });
            list.Add(new object[] { Register.D, Register.IXH });
            list.Add(new object[] { Register.E, Register.IXH });

            list.Add(new object[] { Register.A, Register.IXL });
            list.Add(new object[] { Register.B, Register.IXL });
            list.Add(new object[] { Register.C, Register.IXL });
            list.Add(new object[] { Register.D, Register.IXL });
            list.Add(new object[] { Register.E, Register.IXL });

            list.Add(new object[] { Register.IXH, Register.A });
            list.Add(new object[] { Register.IXH, Register.B });
            list.Add(new object[] { Register.IXH, Register.C });
            list.Add(new object[] { Register.IXH, Register.D });
            list.Add(new object[] { Register.IXH, Register.E });

            list.Add(new object[] { Register.IXL, Register.A });
            list.Add(new object[] { Register.IXL, Register.B });
            list.Add(new object[] { Register.IXL, Register.C });
            list.Add(new object[] { Register.IXL, Register.D });
            list.Add(new object[] { Register.IXL, Register.E });

            list.Add(new object[] { Register.IXH, Register.IXH });
            list.Add(new object[] { Register.IXH, Register.IXL });
            list.Add(new object[] { Register.IXL, Register.IXL });
            list.Add(new object[] { Register.IXL, Register.IXH });

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
        public void Test_LD_IXH_IXL()
        {
            var rom = AssembleSource($@"
                org 00h
                LD IXH, IXL
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IX = 0x1234,
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

            Assert.Equal(0x3434, state.Registers.IX);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_IXL_IXH()
        {
            var rom = AssembleSource($@"
                org 00h
                LD IXL, IXH
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IX = 0x1234,
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

            Assert.Equal(0x1212, state.Registers.IX);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Fact]
        public void Test_LD_SP_IX()
        {
            var rom = AssembleSource($@"
                org 00h
                LD SP, IX
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    IX = 0x1234,
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

            Assert.Equal(0x1234, state.Registers.IX);
            Assert.Equal(0x1234, state.Registers.SP);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 10, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
