using System;
using System.Text.RegularExpressions;

namespace DotEukali.QthCalculator
{
    internal static class Validations
    {
        public static bool IsValidGranularity(this int granularity) => granularity > 0 && granularity <= 8 && granularity % 2 == 0;

        public static void ThrowIfInvalidGranularity(this int granularity)
        {
            if (!granularity.IsValidGranularity())
            {
                throw new Exception($"Granularity of {granularity} is invalid. Must be an even number between 2 and 8.");
            }
        }

        public static bool IsValidMaidenHead(this string maidenHead, bool strict = false)
        {
            Regex regex = new Regex(@"^[A-R]{2}([0-9]{2})?([a-x]{2})?([0-9]{2})?$", strict ? RegexOptions.None : RegexOptions.IgnoreCase);

            return regex.IsMatch(maidenHead);
        }
    }
}
