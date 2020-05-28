using System;
using Xunit;

namespace JustinCredible.ZilogZ80.Tests
{
    public class CPUTests : BaseTest
    {
        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void TestStepNonMaskableInterrupt(bool initialIFF1, bool initialIFF2)
        {
            var initialState = new CPUConfig()
            {
                InterruptsEnabled = initialIFF1,
                InterruptsEnabledPreviousValue = initialIFF2,
                ProgramCounter = 0x1234,
                StackPointer = 0x3FFF,
            };

            var cpu = new CPU(initialState);

            var cycles = cpu.StepNonMaskableInterrupt();

            Assert.Equal(17, cycles);

            // We should've jumped to 0x0066.
            Assert.Equal(0x0066, cpu.ProgramCounter);

            // Interrupts should be disabled (IFF1).
            Assert.False(cpu.InterruptsEnabled);

            // The previous value of IFF1 should have been preserved in IFF2.
            Assert.Equal(initialIFF1, cpu.InterruptsEnabledPreviousValue);

            // Ensure the previous program counter value was pushed onto the stack.
            Assert.Equal(0x3FFD, cpu.StackPointer);
            Assert.Equal(0x12, cpu.Memory[0x3FFE]);
            Assert.Equal(0x34, cpu.Memory[0x3FFD]);
        }

        [Fact]
        public void TestStepMaskableInterrupt_DoesNothingWhenInterruptsDisabled()
        {
            var initialState = new CPUConfig()
            {
                InterruptsEnabled = false,
                ProgramCounter = 0x1234,
                StackPointer = 0x3FFF,
            };

            var cpu = new CPU(initialState);

            var cycles = cpu.StepMaskableInterrupt(0x00);

            Assert.Equal(0, cycles);
            Assert.False(cpu.InterruptsEnabled);
            Assert.Equal(0x1234, cpu.ProgramCounter);
            Assert.Equal(0x3FFF, cpu.StackPointer);
        }

        [Theory]
        [InlineData(OpcodeBytes.RST_00, 0x0000)]
        [InlineData(OpcodeBytes.RST_08, 0x0008)]
        [InlineData(OpcodeBytes.RST_10, 0x0010)]
        [InlineData(OpcodeBytes.RST_18, 0x0018)]
        [InlineData(OpcodeBytes.RST_20, 0x0020)]
        [InlineData(OpcodeBytes.RST_28, 0x0028)]
        [InlineData(OpcodeBytes.RST_30, 0x0030)]
        [InlineData(OpcodeBytes.RST_38, 0x0038)]
        public void TestStepMaskableInterrupt_Mode0(byte dataBusValue, UInt16 expectedProgramCounter)
        {
            var initialState = new CPUConfig()
            {
                InterruptsEnabled = true,
                InterruptMode = InterruptMode.Zero,
                ProgramCounter = 0x1234,
                StackPointer = 0x3FFF,
            };

            var cpu = new CPU(initialState);

            var cycles = cpu.StepMaskableInterrupt(dataBusValue);

            Assert.Equal(11, cycles);

            // Interrupts should still be enabled (IFF1).
            Assert.True(cpu.InterruptsEnabled);

            // We should've jumped somewhere.
            Assert.Equal(expectedProgramCounter, cpu.ProgramCounter);

            // Ensure the previous program counter value was pushed onto the stack.
            Assert.Equal(0x3FFD, cpu.StackPointer);
            Assert.Equal(0x12, cpu.Memory[0x3FFE]);
            Assert.Equal(0x34, cpu.Memory[0x3FFD]);
        }

        [Fact]
        public void TestStepMaskableInterrupt_Mode1()
        {
            var initialState = new CPUConfig()
            {
                InterruptsEnabled = true,
                InterruptMode = InterruptMode.One,
                ProgramCounter = 0x1234,
                StackPointer = 0x3FFF,
            };

            var cpu = new CPU(initialState);

            var cycles = cpu.StepMaskableInterrupt(0x00);

            Assert.Equal(11, cycles);

            // Interrupts should still be enabled (IFF1).
            Assert.True(cpu.InterruptsEnabled);

            // We should've jumped here.
            Assert.Equal(0x0038, cpu.ProgramCounter);

            // Ensure the previous program counter value was pushed onto the stack.
            Assert.Equal(0x3FFD, cpu.StackPointer);
            Assert.Equal(0x12, cpu.Memory[0x3FFE]);
            Assert.Equal(0x34, cpu.Memory[0x3FFD]);
        }

        [Theory]
        [InlineData(0x2211, 0x25, 0x2225)]
        [InlineData(0x0512, 0xF3, 0x05F3)]
        public void TestStepMaskableInterrupt_Mode2(UInt16 interruptVector, byte dataBusValue, UInt16 expectedProgramCounter)
        {
            var initialState = new CPUConfig()
            {
                InterruptsEnabled = true,
                InterruptMode = InterruptMode.Two,
                ProgramCounter = 0x1234,
                StackPointer = 0x3FFF,
                Registers = new CPURegisters()
                {
                    I = interruptVector,
                },
            };

            var cpu = new CPU(initialState);

            var cycles = cpu.StepMaskableInterrupt(dataBusValue);

            Assert.Equal(17, cycles);

            // Interrupts should still be enabled (IFF1).
            Assert.True(cpu.InterruptsEnabled);

            // We should've jumped here; MSB from interrupt vector and LSB from data bus combine to form address.
            Assert.Equal(expectedProgramCounter, cpu.ProgramCounter);

            // Ensure the previous program counter value was pushed onto the stack.
            Assert.Equal(0x3FFD, cpu.StackPointer);
            Assert.Equal(0x12, cpu.Memory[0x3FFE]);
            Assert.Equal(0x34, cpu.Memory[0x3FFD]);
        }
    }
}
