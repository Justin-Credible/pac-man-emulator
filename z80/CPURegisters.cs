
using System;

namespace JustinCredible.ZilogZ80
{
    /**
     * Represents the collection registers available on the Zilog Z80 CPU.
     * 
     * http://www.z80.info/gfx/z80block.gif
     * http://www.z80.info/z80arki.htm
     */
    public class CPURegisters
    {
        /** Accumulator */
        public byte A;

        public byte B;
        public byte C;
        public byte D;
        public byte E;
        public byte H;
        public byte L;

        #region Register Pair Getter/Setters

        public UInt16 BC
        {
            get
            {
                var upper = this.B << 8;
                var lower = this.C;
                var address = upper | lower;
                return (UInt16)address;
            }
            set
            {
                var upper = (value & 0xFF00) >> 8;
                var lower = (byte)value;
                this.B = (byte)upper;
                this.C = (byte)lower;
            }
        }

        public UInt16 DE
        {
            get
            {
                var upper = this.D << 8;
                var lower = this.E;
                var address = upper | lower;
                return (UInt16)address;
            }
            set
            {
                var upper = (value & 0xFF00) >> 8;
                var lower = (byte)value;
                this.D = (byte)upper;
                this.E = (byte)lower;
            }
        }

        public UInt16 HL
        {
            get
            {
                var upper = this.H << 8;
                var lower = this.L;
                var address = upper | lower;
                return (UInt16)address;
            }
            set
            {
                var upper = (value & 0xFF00) >> 8;
                var lower = (byte)value;
                this.H = (byte)upper;
                this.L = (byte)lower;
            }
        }

        #endregion

        #region Register Pair Indexer

        public UInt16 this[RegisterPair pair]
        {
            get
            {
                switch (pair)
                {
                    case RegisterPair.BC:
                        return this.BC;
                    case RegisterPair.DE:
                        return this.DE;
                    case RegisterPair.HL:
                        return this.HL;
                    default:
                        throw new System.NotImplementedException("Unhandled register pair: " + pair);
                }
            }
            set
            {
                switch (pair)
                {
                    case RegisterPair.BC:
                        this.BC = value;
                        break;
                    case RegisterPair.DE:
                        this.DE = value;
                        break;
                    case RegisterPair.HL:
                        this.HL = value;
                        break;
                    default:
                        throw new System.NotImplementedException("Unhandled register pair: " + pair);
                }
            }
        }

        #endregion

        #region Individual Register Indexer

        public byte this[Register registerID]
        {
            get
            {
                switch (registerID)
                {
                    case Register.A:
                        return A;
                    case Register.B:
                        return B;
                    case Register.C:
                        return C;
                    case Register.D:
                        return D;
                    case Register.E:
                        return E;
                    case Register.H:
                        return H;
                    case Register.L:
                        return L;
                    default:
                        throw new NotImplementedException("Unandled register: " + registerID);
                }
            }
            set
            {
                switch (registerID)
                {
                    case Register.A:
                        A = value;
                        break;
                    case Register.B:
                        B = value;
                        break;
                    case Register.C:
                        C = value;
                        break;
                    case Register.D:
                        D = value;
                        break;
                    case Register.E:
                        E = value;
                        break;
                    case Register.H:
                        H = value;
                        break;
                    case Register.L:
                        L = value;
                        break;
                    default:
                        throw new NotImplementedException("Unandled register: " + registerID);
                }
            }
        }

        #endregion
    }
}
