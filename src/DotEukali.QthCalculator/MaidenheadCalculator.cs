using System;
using System.Linq;
using DotEukali.QthCalculator.Exceptions;
using DotEukali.QthCalculator.Extensions;
using DotEukali.QthCalculator.Helpers;

namespace DotEukali.QthCalculator
{
    /// <summary>
    /// Provides methods for calculating Maidenhead grid squares and distances between grid squares.
    /// </summary>
    public static class MaidenheadCalculator
    {
        /// <summary>
        /// Returns the Maidenhead object for the given Maidenhead reference.
        /// </summary>
        /// <param name="maidenheadReference">The Maidenhead reference to calculate the Maidenhead object for.</param>
        /// <returns>The Maidenhead object for the given Maidenhead reference.</returns>
        /// <exception cref="InvalidMaidenheadException">Thrown when the Maidenhead reference is invalid.</exception>
        public static Maidenhead GetMaidenhead(string maidenheadReference) =>
            new Maidenhead(maidenheadReference);

        /// <summary>
        /// Tries to get the Maidenhead object for the given Maidenhead reference.
        /// </summary>
        /// <param name="maidenheadReference">The Maidenhead reference to calculate the Maidenhead object for.</param>
        /// <param name="result">The resulting Maidenhead object if the calculation is successful, or null if the calculation fails.</param>
        /// <returns>true if the Maidenhead object is successfully calculated, false otherwise.</returns>
        public static bool TryGetMaidenhead(string maidenheadReference, out Maidenhead? result)
        {
            try
            {
                result = GetMaidenhead(maidenheadReference);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Calculates and returns the Maidenhead object for the given geographic coordinates.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        /// <param name="locatorType">The locatorType of the Maidenhead grid to generate (default value: LocatorType.SubSquare).</param>
        /// <returns>The Maidenhead object representing the location on the Maidenhead grid.</returns>
        /// <exception cref="InvalidLatLongException">Thrown when the latitude or longitude is invalid.</exception>
        public static Maidenhead GetMaidenhead(double latitude, double longitude,
            LocatorType locatorType = LocatorType.SubSquare)
        {
            if (!latitude.IsValidLatitude() || !longitude.IsValidLongitude())
            {
                throw new InvalidLatLongException(latitude, longitude);
            }

            char[] maidenhead = new char[(int)locatorType];

            longitude += 180;
            latitude += 90;

            maidenhead[0] = (char)((int)(longitude / 20) + 'A');
            maidenhead[1] = (char)((int)(latitude / 10) + 'A');

            if (locatorType == LocatorType.Field)
                return Result();

            maidenhead[2] = (char)(int)((longitude % 20 / 2) + '0');
            maidenhead[3] = (char)(int)((latitude % 10 / 1) + '0');

            if (locatorType == LocatorType.Square)
                return Result();

            maidenhead[4] = (char)(longitude % 2 / (1D / 12D) + 'a');
            maidenhead[5] = (char)(latitude % 1 / (1D / 24D) + 'a');

            if (locatorType == LocatorType.SubSquare)
                return Result();

            maidenhead[6] = (char)(longitude / (1D / 120D) % 10 + '0');
            maidenhead[7] = (char)(latitude / (1D / 240D) % 10 + '0');

            return Result();

            Maidenhead Result()
            {
                return GetMaidenhead(new string(maidenhead));
            }
        }

        /// <summary>
        /// Calculates the distance between two Maidenhead objects.
        /// </summary>
        /// <param name="from">The Maidenhead object representing the starting point.</param>
        /// <param name="to">The Maidenhead object representing the destination point.</param>
        /// <param name="unitOfMeasure">The unit of measure to use for the distance calculation. Default is Kilometers.</param>
        /// <param name="locatorType">The locatorType of the Maidenhead grid to use for the calculation. Default is ExtendedSquare.</param>
        /// <returns>The calculated distance between the two Maidenhead objects in the specified unit of measure.</returns>
        public static double GetDistance(Maidenhead from, Maidenhead to,
            UnitOfMeasure unitOfMeasure = UnitOfMeasure.Kilometers,
            LocatorType locatorType = LocatorType.ExtendedSquare)
        {
            locatorType = EnumHelpers.Min(from.LocatorType, to.LocatorType, locatorType);

            double fromLat = from.Latitude(locatorType);
            double fromLong = from.Longitude(locatorType);

            double toLat = to.Latitude(locatorType);
            double toLong = to.Longitude(locatorType);

            double dLatRadians = GetRadians(toLat - fromLat);
            double dLongRadians = GetRadians(toLong - fromLong);

            double a = Math.Sin(dLatRadians / 2) * Math.Sin(dLatRadians / 2)
                       + Math.Cos(GetRadians(fromLat)) * Math.Cos(GetRadians(toLat))
                                                       * Math.Sin(dLongRadians / 2) * Math.Sin(dLongRadians / 2);

            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return AsUnitOfMeasure(angle * Constants.EarthRadiusKm, unitOfMeasure);
        }

        /// <summary>
        /// Checks whether the given Maidenhead reference is valid.
        /// </summary>
        /// <param name="maidenHeadReference">The Maidenhead reference to validate.</param>
        /// <param name="strict">Indicates whether strict validation should be performed.
        /// If set to true, the reference must match the exact Maidenhead format including case,
        /// otherwise the match is case-insensitive. Default is false.</param>
        /// <returns>True if the Maidenhead reference is valid; otherwise, false.</returns>
        public static bool IsValidMaidenheadReference(string maidenHeadReference, bool strict = false) =>
            maidenHeadReference.IsValidMaidenheadReference(strict);

        private static double GetRadians(double deg) => deg * (Math.PI / 180D);

        private static int AsUnitOfMeasure(double distanceInKm, UnitOfMeasure unitOfMeasure) =>
            (int)Math.Round(unitOfMeasure switch
            {
                UnitOfMeasure.Kilometers => distanceInKm,
                UnitOfMeasure.Meters => distanceInKm * Constants.KmToMeters,
                UnitOfMeasure.Miles => distanceInKm * Constants.KmToMiles,
                UnitOfMeasure.Yards => distanceInKm * Constants.KmToYards,
                _ => throw new ArgumentOutOfRangeException(nameof(unitOfMeasure), unitOfMeasure, null)
            }, 0);
    }
}
