using System;

namespace JustinCredible.PacEmu
{
    public static class StringExtensions
    {
        // https://stackoverflow.com/a/8055977
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new string[] { separator }, StringSplitOptions.None);
        }
    }
}
