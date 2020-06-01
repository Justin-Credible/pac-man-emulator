using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class BIT_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForRegisters()
        {
            var list = new List<object[]>();

            foreach (var register in RegistersClassData.StandardRegisters)
            {
                list.Add(new object[] { register, 0b10010010, 0, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { register, 0b10010010, 1, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { register, 0b10010010, 2, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { register, 0b10010010, 3, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { register, 0b10010010, 4, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { register, 0b10010010, 5, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { register, 0b10010010, 6, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { register, 0b10010010, 7, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { register, 0b01101101, 0, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { register, 0b01101101, 1, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { register, 0b01101101, 2, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { register, 0b01101101, 3, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { register, 0b01101101, 4, new ConditionFlags() { Zero = true } });
                list.Add(new object[] { register, 0b01101101, 5, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { register, 0b01101101, 6, new ConditionFlags() { Zero = false } });
                list.Add(new object[] { register, 0b01101101, 7, new ConditionFlags() { Zero = true } });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForRegisters))]
        public void Test_BIT_X_R(Register register, byte value, int index, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                BIT {index}, {register}
                HALT
            ");

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    [register] = value,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Zero = !expectedFlags.Zero,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,

                    // Should be unaffected.
                    Carry = true,
                    Sign = true,
                    Parity = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(value, state.Registers[register]);

            // Should be affected.
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            // Should be unaffected
            Assert.True(state.Flags.Carry);
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Parity);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        public static IEnumerable<object[]> GetDataForHLRegister()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0b10010010, 0, new ConditionFlags() { Zero = true } });
            list.Add(new object[] { 0b10010010, 1, new ConditionFlags() { Zero = false } });
            list.Add(new object[] { 0b10010010, 2, new ConditionFlags() { Zero = true } });
            list.Add(new object[] { 0b10010010, 3, new ConditionFlags() { Zero = true } });
            list.Add(new object[] { 0b10010010, 4, new ConditionFlags() { Zero = false } });
            list.Add(new object[] { 0b10010010, 5, new ConditionFlags() { Zero = true } });
            list.Add(new object[] { 0b10010010, 6, new ConditionFlags() { Zero = true } });
            list.Add(new object[] { 0b10010010, 7, new ConditionFlags() { Zero = false } });
            list.Add(new object[] { 0b01101101, 0, new ConditionFlags() { Zero = false } });
            list.Add(new object[] { 0b01101101, 1, new ConditionFlags() { Zero = true } });
            list.Add(new object[] { 0b01101101, 2, new ConditionFlags() { Zero = false } });
            list.Add(new object[] { 0b01101101, 3, new ConditionFlags() { Zero = false } });
            list.Add(new object[] { 0b01101101, 4, new ConditionFlags() { Zero = true } });
            list.Add(new object[] { 0b01101101, 5, new ConditionFlags() { Zero = false } });
            list.Add(new object[] { 0b01101101, 6, new ConditionFlags() { Zero = false } });
            list.Add(new object[] { 0b01101101, 7, new ConditionFlags() { Zero = true } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForHLRegister))]
        public void Test_BIT_X_MHL(byte value, int index, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                BIT {index}, (HL)
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234] = value;

            var initialState = new CPUConfig()
            {
                Registers = new CPURegisters()
                {
                    HL = 0x2234,
                },
                Flags = new ConditionFlags()
                {
                    // Should be affected.
                    Zero = !expectedFlags.Zero,

                    // Should be reset.
                    Subtract = true,
                    AuxCarry = true,

                    // Should be unaffected.
                    Carry = true,
                    Sign = true,
                    Parity = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(value, state.Memory[0x2234]);

            // Should be affected.
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be reset.
            Assert.False(state.Flags.AuxCarry);
            Assert.False(state.Flags.Subtract);

            // Should be unaffected
            Assert.True(state.Flags.Carry);
            Assert.True(state.Flags.Sign);
            Assert.True(state.Flags.Parity);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 12, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
