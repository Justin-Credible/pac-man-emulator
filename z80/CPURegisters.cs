
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
        // Main Registers
        public byte A; // Accumulator
        public byte B;
        public byte C;
        public byte D;
        public byte E;
        public byte H;
        public byte L;

        // Alternate/Shadow Registers
        public byte Shadow_A;
        public byte Shadow_B;
        public byte Shadow_C;
        public byte Shadow_D;
        public byte Shadow_E;
        public byte Shadow_H;
        public byte Shadow_L;

        /** Interrupt Vector */
        public UInt16 I { get; set; } // TODO

        /** DRAM refresh counter */
        public UInt16 R { get; set; } // TODO

        /** Index/Base Register */
        public UInt16 IX { get; set; } // TODO

        /** Index/Base Register */
        public UInt16 IY { get; set; } // TODO

        /** Program Counter */
        public UInt16 PC { get; set; }

        /** Stack Pointer */
        public UInt16 SP { get; set; }

        public void SwapShadowRegisterA()
        {
            // Save off shadow register values.
            byte shadowA = Shadow_A;

            // Copy regular register values to shadow registers.
            Shadow_A = A;

            // Copy shadow register values to regular registers.
            A = shadowA;
        }

        public void SwapShadowRegistersBCDEHL()
        {
            // Save off shadow register values.
            byte shadowB = Shadow_B;
            byte shadowC = Shadow_C;
            byte shadowD = Shadow_D;
            byte shadowE = Shadow_E;
            byte shadowH = Shadow_H;
            byte shadowL = Shadow_L;

            // Copy regular register values to shadow registers.
            Shadow_B = B;
            Shadow_C = C;
            Shadow_D = D;
            Shadow_E = E;
            Shadow_H = H;
            Shadow_L = L;

            // Copy shadow register values to regular registers.
            B = shadowB;
            C = shadowC;
            D = shadowD;
            E = shadowE;
            H = shadowH;
            L = shadowL;
        }

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
                    case RegisterPair.I:
                        return I;
                    case RegisterPair.R:
                        return R;
                    case RegisterPair.IX:
                        return IX;
                    case RegisterPair.IY:
                        return IY;
                    case RegisterPair.PC:
                        return PC;
                    case RegisterPair.SP:
                        return SP;
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
                    case RegisterPair.I:
                        I = value;
                        break;
                    case RegisterPair.R:
                        R = value;
                        break;
                    case RegisterPair.IX:
                        IX = value;
                        break;
                    case RegisterPair.IY:
                        IY = value;
                        break;
                    case RegisterPair.PC:
                        PC = value;
                        break;
                    case RegisterPair.SP:
                        SP = value;
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
