namespace DotEukali.QthCalculator
{
    public static class ValidationConstants
    {
        public const string MaidenHeadRegex = "^([A-R]{2})$|^([A-R]{2}[0-9]{2})$|^([A-R]{2}[0-9]{2}[a-x]{2})$|^([A-R]{2}[0-9]{2}[a-x]{2}[0-9]{2})$";
        public const string MaidenHeadRegexCaseInsensitive = "(?i)" + MaidenHeadRegex;
    }
}