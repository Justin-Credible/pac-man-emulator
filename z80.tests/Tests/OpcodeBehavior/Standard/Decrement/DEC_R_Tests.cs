using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class DEC_R_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetData()
        {
            var registers = RegistersClassData.StandardRegisters.GetRange(0, 1);
            var list = new List<object[]>();

            foreach (var register in registers)
            {
                list.Add(new object[] { register, 0x01, 0x00, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = true, Sign = false } });
                list.Add(new object[] { register, 0xFF, 0xFE, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });
                list.Add(new object[] { register, 0x10, 0x0F, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
                list.Add(new object[] { register, 0x7E, 0x7D, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = false } });
                list.Add(new object[] { register, 0x00, 0xFF, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });
                list.Add(new object[] { register, 0x80, 0x7F, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = false } });
            }

            return list;
        }

        public static IEnumerable<object[]> GetDataForHL()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0x01, 0x00, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = true, Sign = false } });
            list.Add(new object[] { 0xFF, 0xFE, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0x10, 0x0F, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0x7E, 0x7D, new ConditionFlags() { HalfCarry = false, ParityOverflow = false, Zero = false, Sign = false } });
            list.Add(new object[] { 0x00, 0xFF, new ConditionFlags() { HalfCarry = true, ParityOverflow = false, Zero = false, Sign = true } });
            list.Add(new object[] { 0x80, 0x7F, new ConditionFlags() { HalfCarry = true, ParityOverflow = true, Zero = false, Sign = false } });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test_DEC_R(Register register, byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC {register}
                HALT
            ");

            var registers = new CPURegisters();
            registers[register] = initialValue;

            var flags = new ConditionFlags()
            {
                // Should be affected.
                HalfCarry = !expectedFlags.HalfCarry,
                ParityOverflow = !expectedFlags.ParityOverflow,
                Zero = !expectedFlags.Zero,
                Sign = !expectedFlags.Sign,

                // Should be set.
                Subtract = false,

                // Should be unaffected.
                Carry = false,
            };

            var initialState = new CPUConfig()
            {
                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers[register]);

            // Should be affected.
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be set.
            Assert.True(state.Flags.Subtract);

            // Should be unaffected.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 4, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }

        [Theory]
        [MemberData(nameof(GetDataForHL))]
        public void Test_INC_MHL(byte initialValue, byte expectedValue, ConditionFlags expectedFlags)
        {
            var rom = AssembleSource($@"
                org 00h
                DEC (HL)
                HALT
            ");

            var memory = new byte[16*1024];
            memory[0x2234] = initialValue;

            var registers = new CPURegisters()
            {
                HL = 0x2234,
            };

            var flags = new ConditionFlags()
            {
                // Should be affected.
                HalfCarry = !expectedFlags.HalfCarry,
                ParityOverflow = !expectedFlags.ParityOverflow,
                Zero = !expectedFlags.Zero,
                Sign = !expectedFlags.Sign,

                // Should be set.
                Subtract = false,

                // Should be unaffected.
                Carry = false,
            };

            var initialState = new CPUConfig()
            {                Registers = registers,
                Flags = flags,
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(expectedValue, state.Memory[0x2234]);

            // Should be affected.
            Assert.Equal(expectedFlags.HalfCarry, state.Flags.HalfCarry);
            Assert.Equal(expectedFlags.Sign, state.Flags.Sign);
            Assert.Equal(expectedFlags.ParityOverflow, state.Flags.ParityOverflow);
            Assert.Equal(expectedFlags.Zero, state.Flags.Zero);

            // Should be set.
            Assert.True(state.Flags.Subtract);

            // Should be unaffected.
            Assert.False(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 11, state.Cycles);
            Assert.Equal(0x01, state.Registers.PC);
        }
    }
}
