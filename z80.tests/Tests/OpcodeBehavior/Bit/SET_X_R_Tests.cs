using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class SET_X_R_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForRegisters()
        {
            var list = new List<object[]>();

            foreach (var register in RegistersClassData.StandardRegisters)
            {
                list.Add(new object[] { register, 0b00000000, 0, 0b00000001 });
                list.Add(new object[] { register, 0b00000000, 1, 0b00000010 });
                list.Add(new object[] { register, 0b00000000, 2, 0b00000100 });
                list.Add(new object[] { register, 0b00000000, 3, 0b00001000 });
                list.Add(new object[] { register, 0b00000000, 4, 0b00010000 });
                list.Add(new object[] { register, 0b00000000, 5, 0b00100000 });
                list.Add(new object[] { register, 0b00000000, 6, 0b01000000 });
                list.Add(new object[] { register, 0b00000000, 7, 0b10000000 });
                list.Add(new object[] { register, 0b11111111, 0, 0b11111111 });
                list.Add(new object[] { register, 0b11111111, 1, 0b11111111 });
                list.Add(new object[] { register, 0b11111111, 2, 0b11111111 });
                list.Add(new object[] { register, 0b11111111, 3, 0b11111111 });
                list.Add(new object[] { register, 0b11111111, 4, 0b11111111 });
                list.Add(new object[] { register, 0b11111111, 5, 0b11111111 });
                list.Add(new object[] { register, 0b11111111, 6, 0b11111111 });
                list.Add(new object[] { register, 0b11111111, 7, 0b11111111 });
                list.Add(new object[] { register, 0b11001010, 0, 0b11001011 });
                list.Add(new object[] { register, 0b11001010, 1, 0b11001010 });
                list.Add(new object[] { register, 0b11001010, 2, 0b11001110 });
                list.Add(new object[] { register, 0b11001010, 3, 0b11001010 });
                list.Add(new object[] { register, 0b11001010, 4, 0b11011010 });
                list.Add(new object[] { register, 0b11001010, 5, 0b11101010 });
                list.Add(new object[] { register, 0b11001010, 6, 0b11001010 });
                list.Add(new object[] { register, 0b11001010, 7, 0b11001010 });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForRegisters))]
        public void Test_SET_X_R(Register register, byte initialValue, int index, byte expectedValue)
        {
            var rom = AssembleSource($@"
                org 00h
                SET {index}, {register}
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
                    // Should be unaffected.
                    Zero = true,
                    Subtract = true,
                    HalfCarry = true,
                    Carry = true,
                    Sign = true,
                    ParityOverflow = true,
                }
            };

            var state = Execute(rom, initialState);

            Assert.Equal(expectedValue, state.Registers[register]);

            // Should be unaffected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 8, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }

        public static IEnumerable<object[]> GetDataForHLRegister()
        {
            var list = new List<object[]>();

            list.Add(new object[] { 0b00000000, 0, 0b00000001 });
            list.Add(new object[] { 0b00000000, 1, 0b00000010 });
            list.Add(new object[] { 0b00000000, 2, 0b00000100 });
            list.Add(new object[] { 0b00000000, 3, 0b00001000 });
            list.Add(new object[] { 0b00000000, 4, 0b00010000 });
            list.Add(new object[] { 0b00000000, 5, 0b00100000 });
            list.Add(new object[] { 0b00000000, 6, 0b01000000 });
            list.Add(new object[] { 0b00000000, 7, 0b10000000 });
            list.Add(new object[] { 0b11111111, 0, 0b11111111 });
            list.Add(new object[] { 0b11111111, 1, 0b11111111 });
            list.Add(new object[] { 0b11111111, 2, 0b11111111 });
            list.Add(new object[] { 0b11111111, 3, 0b11111111 });
            list.Add(new object[] { 0b11111111, 4, 0b11111111 });
            list.Add(new object[] { 0b11111111, 5, 0b11111111 });
            list.Add(new object[] { 0b11111111, 6, 0b11111111 });
            list.Add(new object[] { 0b11111111, 7, 0b11111111 });
            list.Add(new object[] { 0b11001010, 0, 0b11001011 });
            list.Add(new object[] { 0b11001010, 1, 0b11001010 });
            list.Add(new object[] { 0b11001010, 2, 0b11001110 });
            list.Add(new object[] { 0b11001010, 3, 0b11001010 });
            list.Add(new object[] { 0b11001010, 4, 0b11011010 });
            list.Add(new object[] { 0b11001010, 5, 0b11101010 });
            list.Add(new object[] { 0b11001010, 6, 0b11001010 });
            list.Add(new object[] { 0b11001010, 7, 0b11001010 });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForHLRegister))]
        public void Test_SET_X_MHL(byte initialValue, int index, byte expectedValue)
        {
            var rom = AssembleSource($@"
                org 00h
                SET {index}, (HL)
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
                    // Should be unaffected.
                    Zero = true,
                    Subtract = true,
                    HalfCarry = true,
                    Carry = true,
                    Sign = true,
                    ParityOverflow = true,
                }
            };

            var state = Execute(rom, memory, initialState);

            Assert.Equal(0x2234, state.Registers.HL);
            Assert.Equal(expectedValue, state.Memory[0x2234]);

            // Should be affected.
            AssertFlagsSame(initialState, state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(4 + 15, state.Cycles);
            Assert.Equal(0x02, state.Registers.PC);
        }
    }
}
