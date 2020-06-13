using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RR_R_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>();

            foreach (var register in RegistersClassData.StandardRegisters)
            {
                list.Add(new object[] { register, 0b01001001, false, 0b00100100, true, true, false });
                list.Add(new object[] { register, 0b01001001, true, 0b10100100, true, false, true });
                list.Add(new object[] { register, 0b01001000, false, 0b00100100, false, true, false });
                list.Add(new object[] { register, 0b01001000, true, 0b10100100, false, false, true });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_RR_R(Register register, byte initialValue, bool initialCarryFlag, byte expectedValue, bool expectedCarryFlag, bool expectedPartyFlag, bool expectedSignFlag)
        {
            var rom = AssembleSource($@"
                org 00h
                RR {register}
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
                    Carry = initialCarryFlag,
                    Zero = true,
                    Sign = !expectedSignFlag,
                    ParityOverflow = !expectedPartyFlag,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers[register]);

            // Should be affected.
            Assert.Equal(expectedCarryFlag, state.Flags.Carry);
            Assert.Equal(expectedSignFlag, state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.Equal(expectedPartyFlag, state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        [Theory]
        [InlineData(0b01001001, false, 0b00100100, true, true, false)]
        [InlineData(0b01001001, true, 0b10100100, true, false, true)]
        [InlineData(0b01001000, false, 0b00100100, false, true, false)]
        [InlineData(0b01001000, true, 0b10100100, false, false, true)]
        public void Test_RR_MHL(byte initialValue, bool initialCarryFlag, byte expectedValue, bool expectedCarryFlag, bool expectedPartyFlag, bool expectedSignFlag)
        {
            var rom = AssembleSource($@"
                org 00h
                RR (HL)
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
                    Carry = initialCarryFlag,
                    Zero = true,
                    Sign = !expectedSignFlag,
                    ParityOverflow = !expectedPartyFlag,

                    // Should be reset.
                    Subtract = true,
                    HalfCarry = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(expectedValue, state.Memory[0x2234]);

            // Should be affected.
            Assert.Equal(expectedCarryFlag, state.Flags.Carry);
            Assert.Equal(expectedSignFlag, state.Flags.Sign);
            Assert.False(state.Flags.Zero);
            Assert.Equal(expectedPartyFlag, state.Flags.ParityOverflow);

            // Should be reset.
            Assert.False(state.Flags.HalfCarry);
            Assert.False(state.Flags.Subtract);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
