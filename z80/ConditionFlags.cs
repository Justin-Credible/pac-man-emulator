
namespace JustinCredible.ZilogZ80
{
    /**
     * Represents the CPU flag register (F) with named properties to access each bit.
     *
     * Bit layout:
     * 7 6 5 4 3  2  1 0
     * S Z - H - P/V N C
     *
     * Bits 5 and 3 are not used and always remain the same (zero).
     */
    public class ConditionFlags
    {
        /**
         * S (sign) set to 1 when bit 7 (the most significant bit or MSB) of the math instruction is set.
         */
        public bool Sign;

        /**
         * Z (zero) set to 1 when the result is equal to zero.
         */
        public bool Zero;

        /**
         * H (half carry) is used mostly for BCD (binary coded decimal) math.
         */
        public bool HalfCarry;

        /**
         * P/V (Parity/Overflow) is set if the number of 1 bits in the result is even.
         */
        public bool ParityOverflow;

        /**
         * N (Add/Subtract) is set to 1 if the operation was a subtract, 0 if it was an addition, or unmodified if neither.
         */
        public bool Subtract;

        /**
         * C (carry) set to 1 when the instruction resulted in a carry out or borrow into the high order bit.
         */
        public bool Carry;

        /**
         * The shadow/alternate value of the F register known as F'. Used by the swap method.
         */
        public byte Shadow;

        // Need parameterless constructor for JSON deserialization.
        public ConditionFlags()
        {
        }

        public ConditionFlags(byte? registerValue = null)
        {
            if (registerValue.HasValue)
                SetFromByte(registerValue.Value);
        }

        public void SwapShadowRegister()
        {
            // Save off shadow register values.
            byte shadowF = Shadow;

            // Copy regular register values to shadow registers.
            Shadow = ToByte();

            // Copy shadow register values to regular registers.
            SetFromByte(shadowF);
        }

        public byte ToByte()
        {
            /**
             * 7 6 5 4 3  2  1 0
             * S Z - H - P/V N C
             */
            var flags = 0b00000000;

            if (Sign)
                flags = flags | 0b10000000;

            if (Zero)
                flags = flags | 0b01000000;

            if (HalfCarry)
                flags = flags | 0b00010000;

            if (ParityOverflow)
                flags = flags | 0b00000100;

            if (Subtract)
                flags = flags | 0b00000010;

            if (Carry)
                flags = flags | 0b00000001;

            return (byte)flags;
        }

        public void SetFromByte(byte flags)
        {
            Sign = (flags & 0b10000000) == 0b10000000;
            Zero = (flags & 0b01000000) == 0b01000000;
            HalfCarry = (flags & 0b00010000) == 0b00010000;
            ParityOverflow = (flags & 0b00000100) == 0b00000100;
            Subtract = (flags & 0b00000010) == 0b00000010;
            Carry = (flags & 0b00000001) == 0b00000001;
        }
    }
}
