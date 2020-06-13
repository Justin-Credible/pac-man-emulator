using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SLA_R_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForRegisters()
        {
            var list = new List<object[]>();

            foreach (var register in RegistersClassData.StandardRegisters)
            {
                list.Add(new object[] { register, 0b11001001, 0b10010010, new ConditionFlags() { Carry = true, Zero = false, Sign = true, ParityOverflow = false } });
                list.Add(new object[] { register, 0b10010010, 0b00100100, new ConditionFlags() { Carry = true, Zero = false, Sign = false, ParityOverflow = true } });
                list.Add(new object[] { register, 0b00100100, 0b01001000, new ConditionFlags() { Carry = false, Zero = false, Sign = false, ParityOverflow = true } });
                list.Add(new object[] { register, 0b10000000, 0b00000000, new ConditionFlags() { Carry = true, Zero = true, Sign = false, ParityOverflow = true } });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForRegisters))]
        public void Test_SLA_R(Register register, byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                SLA {register}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    [register] = initialValue,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = !expectedFlags.Carry,
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    ParityOverflow = !expectedFlags.ParityOverflow,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers[register]);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        public static IEnumerable<object[]> GetDataForHLRegister()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0b11001001, 0b10010010, new ConditionFlags() { Carry = true, Zero = false, Sign = true, ParityOverflow = false } });
            list.Add(new object[] { 0b10010010, 0b00100100, new ConditionFlags() { Carry = true, Zero = false, Sign = false, ParityOverflow = true } });
            list.Add(new object[] { 0b00100100, 0b01001000, new ConditionFlags() { Carry = false, Zero = false, Sign = false, ParityOverflow = true } });
            list.Add(new object[] { 0b10000000, 0b00000000, new ConditionFlags() { Carry = true, Zero = true, Sign = false, ParityOverflow = true } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForHLRegister))]
        public void Test_SLA_MHL(byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                SLA (HL)
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234] = initialValue;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    HL = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Carry = !expectedFlags.Carry,
                    Sign = !expectedFlags.Sign,
                    Zero = !expectedFlags.Zero,
                    ParityOverflow = !expectedFlags.ParityOverflow,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(expectedValue, state.Memory[0x2234]);

            // Should be affected.
            Assert.Equal(expectedFlags.Carry, state.Flags.Carry);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
