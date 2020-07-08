
using System;
using JustinCredible.ZilogZ80;

namespace JustinCredible.Z80Disassembler
{
    public class SimpleMemory : IMemory
    {
        public byte[] Memory = null;

        public SimpleMemory(int size)
        {
            Memory = new byte[size];
        }

        public SimpleMemory(byte[] memory)
        {
            Memory = memory;
        }

        public byte Read(int address)
        {
            return Memory[address];
        }

        public ushort Read16(int address)
        {
            var lower = Read(address);
            var upper = Read(address + 1) << 8;
            return (UInt16)(upper | lower);
        }

        public void Write(int address, byte value)
        {
            Memory[address] = value;
        }

        public void Write16(int address, ushort value)
        {
            var lower = (byte)(value & 0x00FF);
            var upper = (byte)((value & 0xFF00) >> 8);
            Write(address, lower);
            Write(address + 1, upper);
        }
    }
}
