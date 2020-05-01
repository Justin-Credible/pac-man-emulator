using System.Collections;
using System.Collections.Generic;

namespace JustinCredible.ZilogZ80.Tests
{
    public class RegisterPermutationsClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object [] { Register.A, Register.A };
            yield return new object [] { Register.A, Register.B };
            yield return new object [] { Register.A, Register.C };
            yield return new object [] { Register.A, Register.D };
            yield return new object [] { Register.A, Register.E };
            yield return new object [] { Register.A, Register.H };
            yield return new object [] { Register.A, Register.L };
            yield return new object [] { Register.B, Register.A };
            yield return new object [] { Register.B, Register.B };
            yield return new object [] { Register.B, Register.C };
            yield return new object [] { Register.B, Register.D };
            yield return new object [] { Register.B, Register.E };
            yield return new object [] { Register.B, Register.H };
            yield return new object [] { Register.B, Register.L };
            yield return new object [] { Register.C, Register.A };
            yield return new object [] { Register.C, Register.B };
            yield return new object [] { Register.C, Register.C };
            yield return new object [] { Register.C, Register.D };
            yield return new object [] { Register.C, Register.E };
            yield return new object [] { Register.C, Register.H };
            yield return new object [] { Register.C, Register.L };
            yield return new object [] { Register.D, Register.A };
            yield return new object [] { Register.D, Register.B };
            yield return new object [] { Register.D, Register.C };
            yield return new object [] { Register.D, Register.D };
            yield return new object [] { Register.D, Register.E };
            yield return new object [] { Register.D, Register.H };
            yield return new object [] { Register.D, Register.L };
            yield return new object [] { Register.E, Register.A };
            yield return new object [] { Register.E, Register.B };
            yield return new object [] { Register.E, Register.C };
            yield return new object [] { Register.E, Register.D };
            yield return new object [] { Register.E, Register.E };
            yield return new object [] { Register.E, Register.H };
            yield return new object [] { Register.E, Register.L };
            yield return new object [] { Register.H, Register.A };
            yield return new object [] { Register.H, Register.B };
            yield return new object [] { Register.H, Register.C };
            yield return new object [] { Register.H, Register.D };
            yield return new object [] { Register.H, Register.E };
            yield return new object [] { Register.H, Register.H };
            yield return new object [] { Register.H, Register.L };
            yield return new object [] { Register.L, Register.A };
            yield return new object [] { Register.L, Register.B };
            yield return new object [] { Register.L, Register.C };
            yield return new object [] { Register.L, Register.D };
            yield return new object [] { Register.L, Register.E };
            yield return new object [] { Register.L, Register.H };
            yield return new object [] { Register.L, Register.L };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
