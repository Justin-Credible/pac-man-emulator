
namespace JustinCredible.ZilogZ80
{
    /**
     * Represents the CPU flag register with named properties to access each bit.
     *
     * Bit layout:
     * 7 6 5 4 3 2 1 0
     * S Z - H - P N C
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
         * TODO: I didn't implement this for the 8080 because it wasn't used by Space Invaders.
         */
        public bool AuxCarry;

        /**
         * P (Parity/Overflow) is set if the number of 1 bits in the result is even.
         */
        public bool Parity;

        /**
         * N (Add/Subtract) is set to 1 if the operation was a subtract, 0 if it was an addition, or unmodified if neither.
         * TODO: Add to all unit tests?
         */
        public bool Subtract;

        /**
         * C (carry) set to 1 when the instruction resulted in a carry out or borrow into the high order bit.
         */
        public bool Carry;

        public byte ToByte()
        {
            /**
             * 7 6 5 4 3 2 1 0
             * S Z - H - P N C
             */
            var flags = 0b00000000;

            if (Sign)
                flags = flags | 0b10000000;

            if (Zero)
                flags = flags | 0b01000000;

            if (AuxCarry)
                flags = flags | 0b00010000;

            if (Parity)
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
            AuxCarry = (flags & 0b00010000) == 0b00010000;
            Parity = (flags & 0b00000100) == 0b00000100;
            Subtract = (flags & 0b00000010) == 0b00000010;
            Carry = (flags & 0b00000001) == 0b00000001;
        }
    }
}
