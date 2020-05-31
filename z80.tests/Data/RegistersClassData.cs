using System.Collections;
using System.Collections.Generic;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RegistersClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object [] { Register.A };
            yield return new object [] { Register.B };
            yield return new object [] { Register.C };
            yield return new object [] { Register.D };
            yield return new object [] { Register.E };
            yield return new object [] { Register.H };
            yield return new object [] { Register.L };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static List<Register> StandardRegisters
        {
            get
            {
                return new List<Register>()
                {
                    Register.A,
                    Register.B,
                    Register.C,
                    Register.D,
                    Register.E,
                    Register.H,
                    Register.L,
                };
            }
        }
    }
}
