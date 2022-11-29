using System;

namespace DotEukali.QthCalculator
{
    public static class MaidenHeadExtensions
    {
        public static double Latitude(this MaidenHead maidenHead, int granularity = 8) => maidenHead.Latitude(true, granularity);
        public static double Latitude(this MaidenHead maidenHead, bool getCenter = true, int granularity = 8)
        {
            granularity.ThrowIfInvalidGranularity();

            if (!maidenHead.IsValid())
                return 0D;

            int length = Math.Min(maidenHead.Length, granularity);
            string location = maidenHead.Location.ToUpper();

            double latitude = (location[1] - 65) * 10;

            if (length >= 4) latitude += int.Parse(location.Substring(3, 1));
            if (length >= 6) latitude += (location[5] - 65D) / 24D;
            if (length >= 8) latitude += int.Parse(location.Substring(7, 1)) / 240D;

            if (!getCenter)
                return Math.Round(latitude - 90, 6);

            if (length == 2) latitude += 5D;
            else if (length == 4) latitude += ('L' - 65D + 0.5D) / 24D + 1D / 48D;
            else if (length == 6) latitude += 1D / 48D;
            else if (length == 8) latitude += 1D / 480D;

            return Math.Round(latitude - 90, 6);
        }

        public static double Longitude(this MaidenHead maidenHead, int granularity = 8) => maidenHead.Longitude(true, granularity);
        public static double Longitude(this MaidenHead maidenHead, bool getCenter = true, int granularity = 8)
        {
            granularity.ThrowIfInvalidGranularity();

            if (!maidenHead.IsValid())
                return 0D;

            int length = Math.Min(maidenHead.Length, granularity);
            string location = maidenHead.Location.ToUpper();

            double longitude = (location[0] - 65) * 20;

            if (length >= 4) longitude += int.Parse(location.Substring(2, 1)) * 2D;
            if (length >= 6) longitude += (location[4] - 65) / 12D;
            if (length >= 8) longitude += int.Parse(location.Substring(6, 1)) / 120D;

            if (!getCenter)
                return Math.Round(longitude - 180, 6);

            if (length == 2) longitude += 10D;
            else if (length == 4) longitude += ('L' - 65 + 0.5D) / 12D + 1D / 24D;
            else if (length == 6) longitude += 1D / 24D;
            else if (length == 8) longitude += 1D / 240D;

            return Math.Round(longitude - 180, 6);
        }

        public static int DistanceTo(this MaidenHead from, MaidenHead to, UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kilometers, int granularity = 8)
        {
            double fromLat = from.Latitude(granularity);
            double fromLong = from.Longitude(granularity);

            double toLat = to.Latitude(granularity);
            double toLong = to.Longitude(granularity);

            double dLatRadians = GetRadians(toLat - fromLat);
            double dLongRadians = GetRadians(toLong - fromLong);

            double a = Math.Sin(dLatRadians / 2) * Math.Sin(dLatRadians / 2)
                     + Math.Cos(GetRadians(fromLat)) * Math.Cos(GetRadians(toLat))
                     * Math.Sin(dLongRadians / 2) * Math.Sin(dLongRadians / 2);

            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return AsUnitOfMeasure(angle * ConstantValues.EarthRadiusKm, unitOfMeasure);
        }

        private static double GetRadians(double deg) => deg * (Math.PI / 180D);

        private static int AsUnitOfMeasure(double distanceInKm, UnitOfMeasure unitOfMeasure) =>
            (int)Math.Round(unitOfMeasure switch
            {
                UnitOfMeasure.Kilometers => distanceInKm,
                UnitOfMeasure.Meters => distanceInKm * ConstantValues.KmToMeters,
                UnitOfMeasure.Miles => distanceInKm * ConstantValues.KmToMiles,
                UnitOfMeasure.Yards => distanceInKm * ConstantValues.KmToYards,
                _ => throw new ArgumentOutOfRangeException(nameof(unitOfMeasure), unitOfMeasure, null)
            }, 0);
    }
}
