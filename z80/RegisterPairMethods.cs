
namespace JustinCredible.ZilogZ80
{
    /**
     * Extension methods for the RegisterPair enumeration.
     */
    public static class RegisterPairMethods
    {
        public static Register GetUpperRegister(this RegisterPair pair)
        {
            switch (pair)
            {
                case RegisterPair.BC:
                    return Register.B;
                case RegisterPair.DE:
                    return Register.D;
                case RegisterPair.HL:
                    return Register.H;
                default:
                    throw new System.NotImplementedException("Unhandled register pair: " + pair);
            }
        }

        public static Register GetLowerRegister(this RegisterPair pair)
        {
            switch (pair)
            {
                case RegisterPair.BC:
                    return Register.C;
                case RegisterPair.DE:
                    return Register.E;
                case RegisterPair.HL:
                    return Register.L;
                default:
                    throw new System.NotImplementedException("Unhandled register pair: " + pair);
            }
        }
    }
}
