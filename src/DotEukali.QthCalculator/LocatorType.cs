namespace DotEukali.QthCalculator
{
    /// <summary>
    /// Enables Locator Type overrides in calculations
    /// </summary>
    public enum LocatorType
    {
        /// <summary>
        /// Maidenhead Field locator, eg 'AA'
        /// </summary>
        Field = 2,
        /// <summary>
        /// Maidenhead Square locator, eg 'AA00'
        /// </summary>
        Square = 4,
        /// <summary>
        /// Maidenhead SubSquare locator, eg 'AA00aa'
        /// </summary>
        SubSquare = 6,
        /// <summary>
        /// Maidenhead Extended Square locator, eg 'AA00aa00'
        /// </summary>
        ExtendedSquare = 8
    }
}
