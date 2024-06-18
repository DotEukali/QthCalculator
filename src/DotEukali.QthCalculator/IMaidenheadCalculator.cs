namespace DotEukali.QthCalculator
{
    /// <inheritdoc cref="MaidenheadCalculator" />
    public interface IMaidenheadCalculator
    {
        /// <inheritdoc cref="MaidenheadCalculator" />
        Maidenhead GetMaidenhead(string maidenheadReference);

        /// <inheritdoc cref="MaidenheadCalculator" />
        bool TryGetMaidenhead(string maidenheadReference, out Maidenhead? result);

        /// <inheritdoc cref="MaidenheadCalculator" />
        Maidenhead GetMaidenhead(double latitude, double longitude, LocatorType locatorType = LocatorType.SubSquare);

        /// <inheritdoc cref="MaidenheadCalculator" />
        double GetDistance(Maidenhead from, Maidenhead to, UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kilometers, LocatorType locatorType = LocatorType.ExtendedSquare);

        /// <inheritdoc cref="MaidenheadCalculator" />
        bool IsValidMaidenheadReference(string maidenheadReference, bool strict = false);
    }
}
