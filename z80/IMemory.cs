using System;

namespace JustinCredible.ZilogZ80
{
    public interface IMemory
    {
        void Write(int address, byte value);
        void Write16(int address, UInt16 value);
        byte Read(int address);
        UInt16 Read16(int address);
    }
}
