using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DotEukali.QthCalculator.Extensions
{
    internal static class InternalExtensions
    {
        internal static string FormatMaidenheadReference(this string location)
        {
            if (!location.IsValidMaidenheadReference())
                return location;

            var formattedLocation = new StringBuilder();

            for (var i = 0; i < location.Length; i++)
            {
                switch (i)
                {
                    case 0:
                    case 1:
                        formattedLocation.Append(char.ToUpper(location[i]));
                        break;
                    case 4:
                    case 5:
                        formattedLocation.Append(char.ToLower(location[i]));
                        break;
                    default:
                        formattedLocation.Append(location[i]);
                        break;
                }
            }

            return formattedLocation.ToString();
        }

        internal static bool IsValidMaidenheadReference(this string maidenHead, bool strict = false)
        {
            Regex regex = new Regex(ValidationConstants.MaidenheadRegex, strict ? RegexOptions.None : RegexOptions.IgnoreCase,
                TimeSpan.FromSeconds(1));

            return regex.IsMatch(maidenHead);
        }

        internal static bool IsValidLatitude(this double latitude) => latitude >= -90D && latitude <= 90D;

        internal static bool IsValidLongitude(this double longitude) => longitude >= -180D && longitude <= 180D;

    }
}
