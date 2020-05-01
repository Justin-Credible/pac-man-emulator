
namespace JustinCredible.ZilogZ80
{
    /**
     * Represents the CPU flag register with named properties to access each bit.
     *
     * Bit layout:
     * 7 6 5 4 3 2 1 0
     * S Z 0 A 0 P 1 C
     *
     * Bits 5, 3, and 1 are not used and always remain the same.
     */
    public class ConditionFlags
    {
        /**
         * Z (zero) set to 1 when the result is equal to zero
         */
        public bool Zero;

        /**
         * S (sign) set to 1 when bit 7 (the most significant bit or MSB) of the math instruction is set
         */
        public bool Sign;

        /**
         * P (parity) is  set if the number of 1 bits in the result is even.
         */
        public bool Parity;

        /**
         * CY (carry) set to 1 when the instruction resulted in a carry out or borrow into the high order bit
         */
        public bool Carry;

        /**
         * AC (auxillary carry) is used mostly for BCD (binary coded decimal) math. Read the data book for more details, Space Invaders doesn't use it.
         */
        public bool AuxCarry;

        public byte ToByte()
        {
            /**
             * 7 6 5 4 3 2 1 0
             * S Z 0 A 0 P 1 C
             */
            var flags = 0b00000010;

            if (Sign)
                flags = flags | 0b10000000;

            if (Zero)
                flags = flags | 0b01000000;

            if (AuxCarry)
                flags = flags | 0b00010000;

            if (Parity)
                flags = flags | 0b00000100;

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
            Carry = (flags & 0b00000001) == 0b00000001;
        }
    }
}
