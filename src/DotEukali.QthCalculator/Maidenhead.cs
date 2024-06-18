using DotEukali.QthCalculator.Exceptions;
using DotEukali.QthCalculator.Extensions;

namespace DotEukali.QthCalculator
{
    /// <summary>
    /// A Maidenhead locator, also known as a grid locator or a QTH locator.
    /// </summary>
    public readonly struct Maidenhead
    {
        internal Maidenhead(string location)
        {
            if (!MaidenheadCalculator.IsValidMaidenheadReference(location))
            {
                throw new InvalidMaidenheadException(location);
            }

            Location = location.FormatMaidenheadReference();
            LocatorType = (LocatorType)Location.Length;
        }

        /// <summary>
        /// The Maidenhead grid reference.
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// The Maidenhead Locator type, eg, Field, Square, SubSquare, Extended Square
        /// </summary>
        public LocatorType LocatorType { get; }

        /// <summary>
        /// Overrides ToString to return the Maidenhead locator reference
        /// </summary>
        /// <returns>The Maidenhead locator reference</returns>
        public override string ToString() => Location;
    }
}
