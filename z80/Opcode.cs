
using System.Collections.Generic;

namespace JustinCredible.ZilogZ80
{
    /**
     * Describes an opcode for the Zilog Z80 CPU.
     */
    public class Opcode
    {
        public byte Code { get; }
        public int Size { get; }
        public string Instruction { get; }
        public int Cycles { get; }
        public int? AlternateCycles { get; }
        public string Pseudocode { get; }
        public InstructionSet InstructionSet { get; }

        // The constructor is marked internal because when instantiated this class
        // will insert the opcode instance into a lookup table internally. This is
        // only meant to be used to define opcodes internally in this assembly. By
        // using internal instead of private, we can access it via the unit tests.
        internal Opcode(byte code, int size, string instruction, int cycles, int? alternateCycles = null, string pseudocode = null, InstructionSet instructionSet = InstructionSet.Standard)
        {
            Code = code;
            Size = size;
            Instruction = instruction;
            Cycles = cycles;
            AlternateCycles = alternateCycles;
            Pseudocode = pseudocode;
            InstructionSet = instructionSet;

            // Insert this opcode into the correct lookup table.
            switch (instructionSet)
            {
                case InstructionSet.Standard:
                    if (Opcodes.Standard == null) Opcodes.Standard = new Dictionary<byte, Opcode>();
                    Opcodes.Standard[code] = this;
                    break;
                case InstructionSet.Extended:
                    if (Opcodes.Extended == null) Opcodes.Extended = new Dictionary<byte, Opcode>();
                    Opcodes.Extended[code] = this;
                    break;
                case InstructionSet.Bit:
                    if (Opcodes.Bit == null) Opcodes.Bit = new Dictionary<byte, Opcode>();
                    Opcodes.Bit[code] = this;
                    break;
                case InstructionSet.IX:
                    if (Opcodes.IX == null) Opcodes.IX = new Dictionary<byte, Opcode>();
                    Opcodes.IX[code] = this;
                    break;
                case InstructionSet.IXBit:
                    if (Opcodes.IXBit == null) Opcodes.IXBit = new Dictionary<byte, Opcode>();
                    Opcodes.IXBit[code] = this;
                    break;
                case InstructionSet.IY:
                    if (Opcodes.IY == null) Opcodes.IY = new Dictionary<byte, Opcode>();
                    Opcodes.IY[code] = this;
                    break;
                case InstructionSet.IYBit:
                    if (Opcodes.IYBit == null) Opcodes.IYBit = new Dictionary<byte, Opcode>();
                    Opcodes.IYBit[code] = this;
                    break;
            }
        }
    }
}
