using System;

namespace DotEukali.QthCalculator.Helpers
{
    internal static class EnumHelpers
    {
        internal static T Min<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) <= 0 ? a : b;
        }

        internal static T Min<T>(T a, T b, T c) where T : IComparable
        {
            return Min(Min(a, b), c);
        }
    }
}
