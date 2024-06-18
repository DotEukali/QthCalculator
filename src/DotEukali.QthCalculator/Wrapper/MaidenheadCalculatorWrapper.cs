namespace DotEukali.QthCalculator.Wrapper
{
    internal sealed class MaidenheadCalculatorWrapper : IMaidenheadCalculator
    {
        /// <inheritdoc />
        public Maidenhead GetMaidenhead(string maidenheadReference) =>
            MaidenheadCalculator.GetMaidenhead(maidenheadReference);

        /// <inheritdoc />
        public bool TryGetMaidenhead(string maidenheadReference, out Maidenhead? result) =>
            MaidenheadCalculator.TryGetMaidenhead(maidenheadReference, out result);

        /// <inheritdoc />
        public Maidenhead GetMaidenhead(double latitude, double longitude, LocatorType locatorType = LocatorType.SubSquare) =>
            MaidenheadCalculator.GetMaidenhead(latitude, longitude, locatorType);

        /// <inheritdoc />
        public double GetDistance(Maidenhead from, Maidenhead to, UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kilometers,
            LocatorType locatorType = LocatorType.ExtendedSquare) =>
            MaidenheadCalculator.GetDistance(from, to, unitOfMeasure, locatorType);

        /// <inheritdoc />
        public bool IsValidMaidenheadReference(string maidenheadReference, bool strict = false) =>
            MaidenheadCalculator.IsValidMaidenheadReference(maidenheadReference, strict);
    }
}
