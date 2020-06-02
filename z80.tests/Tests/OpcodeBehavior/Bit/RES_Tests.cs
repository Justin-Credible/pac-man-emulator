using System.Collections.Generic;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RES_Tests : BaseTest
    {
        public static IEnumerable<object[]> GetDataForRegisters()
        {
            var list = new List<object[]>();

            foreach (var register in RegistersClassData.StandardRegisters)
            {
                list.Add(new object[] { register, 0b00000000, 0, 0b00000000 });
                list.Add(new object[] { register, 0b00000000, 1, 0b00000000 });
                list.Add(new object[] { register, 0b00000000, 2, 0b00000000 });
                list.Add(new object[] { register, 0b00000000, 3, 0b00000000 });
                list.Add(new object[] { register, 0b00000000, 4, 0b00000000 });
                list.Add(new object[] { register, 0b00000000, 5, 0b00000000 });
                list.Add(new object[] { register, 0b00000000, 6, 0b00000000 });
                list.Add(new object[] { register, 0b00000000, 7, 0b00000000 });
                list.Add(new object[] { register, 0b11111111, 0, 0b11111110 });
                list.Add(new object[] { register, 0b11111111, 1, 0b11111101 });
                list.Add(new object[] { register, 0b11111111, 2, 0b11111011 });
                list.Add(new object[] { register, 0b11111111, 3, 0b11110111 });
                list.Add(new object[] { register, 0b11111111, 4, 0b11101111 });
                list.Add(new object[] { register, 0b11111111, 5, 0b11011111 });
                list.Add(new object[] { register, 0b11111111, 6, 0b10111111 });
                list.Add(new object[] { register, 0b11111111, 7, 0b01111111 });
                list.Add(new object[] { register, 0b11001010, 0, 0b11001010 });
                list.Add(new object[] { register, 0b11001010, 1, 0b11001000 });
                list.Add(new object[] { register, 0b11001010, 2, 0b11001010 });
                list.Add(new object[] { register, 0b11001010, 3, 0b11000010 });
                list.Add(new object[] { register, 0b11001010, 4, 0b11001010 });
                list.Add(new object[] { register, 0b11001010, 5, 0b11001010 });
                list.Add(new object[] { register, 0b11001010, 6, 0b10001010 });
                list.Add(new object[] { register, 0b11001010, 7, 0b01001010 });
            }

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForRegisters))]
        public void Test_RES_X_R(Register register, byte initialValue, int index, byte expectedValue)
        {
            var rom = AssembleSource($@"
                org 00h
                RES {index}, {register}
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
                    AuxCarry = true,
                    Carry = true,
                    Sign = true,
                    Parity = true,
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

            list.Add(new object[] { 0b00000000, 0, 0b00000000 });
            list.Add(new object[] { 0b00000000, 1, 0b00000000 });
            list.Add(new object[] { 0b00000000, 2, 0b00000000 });
            list.Add(new object[] { 0b00000000, 3, 0b00000000 });
            list.Add(new object[] { 0b00000000, 4, 0b00000000 });
            list.Add(new object[] { 0b00000000, 5, 0b00000000 });
            list.Add(new object[] { 0b00000000, 6, 0b00000000 });
            list.Add(new object[] { 0b00000000, 7, 0b00000000 });
            list.Add(new object[] { 0b11111111, 0, 0b11111110 });
            list.Add(new object[] { 0b11111111, 1, 0b11111101 });
            list.Add(new object[] { 0b11111111, 2, 0b11111011 });
            list.Add(new object[] { 0b11111111, 3, 0b11110111 });
            list.Add(new object[] { 0b11111111, 4, 0b11101111 });
            list.Add(new object[] { 0b11111111, 5, 0b11011111 });
            list.Add(new object[] { 0b11111111, 6, 0b10111111 });
            list.Add(new object[] { 0b11111111, 7, 0b01111111 });
            list.Add(new object[] { 0b11001010, 0, 0b11001010 });
            list.Add(new object[] { 0b11001010, 1, 0b11001000 });
            list.Add(new object[] { 0b11001010, 2, 0b11001010 });
            list.Add(new object[] { 0b11001010, 3, 0b11000010 });
            list.Add(new object[] { 0b11001010, 4, 0b11001010 });
            list.Add(new object[] { 0b11001010, 5, 0b11001010 });
            list.Add(new object[] { 0b11001010, 6, 0b10001010 });
            list.Add(new object[] { 0b11001010, 7, 0b01001010 });

            return list;
        }

        [Theory]
        [MemberData(nameof(GetDataForHLRegister))]
        public void Test_RES_X_MHL(byte initialValue, int index, byte expectedValue)
        {
            var rom = AssembleSource($@"
                org 00h
                RES {index}, (HL)
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
                    AuxCarry = true,
                    Carry = true,
                    Sign = true,
                    Parity = true,
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
