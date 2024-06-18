namespace DotEukali.QthCalculator
{
    public static class ValidationConstants
    {
        /// <summary>
        /// The regular expression pattern for validating Maidenhead references.
        /// </summary>
        public const string MaidenheadRegex = "^([A-R]{2})$|^([A-R]{2}[0-9]{2})$|^([A-R]{2}[0-9]{2}[a-x]{2})$|^([A-R]{2}[0-9]{2}[a-x]{2}[0-9]{2})$";

        /// <summary>
        /// The regular expression pattern for validating Maidenhead references in a case-insensitive manner.
        /// </summary>
        public const string MaidenheadRegexCaseInsensitive = "(?i)" + MaidenheadRegex;
    }
}
