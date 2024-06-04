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
            Regex regex = new Regex(ValidationConstants.MaidenHeadRegex, strict ? RegexOptions.None : RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1));

            return regex.IsMatch(maidenHead);
        }
    }
}