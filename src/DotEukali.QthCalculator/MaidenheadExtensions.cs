using System;
using DotEukali.QthCalculator.Helpers;

namespace DotEukali.QthCalculator
{
    /// <summary>
    /// Extensions for calculating latitude and longitude from a Maidenhead object.
    /// </summary>
    public static class MaidenheadExtensions
    {
        /// <summary>
        /// Calculates the latitude coordinate of a Maidenhead locator.
        /// </summary>
        /// <param name="maidenhead">The Maidenhead locator.</param>
        /// <param name="locatorType">The type of locator system to use (Field, Square, SubSquare, or ExtendedSquare).</param>
        /// <returns>The latitude coordinate of middle of the Maidenhead locator.</returns>
        public static double Latitude(this Maidenhead maidenhead, LocatorType locatorType) =>
            maidenhead.Latitude(LatitudePoint.Middle, locatorType);

        /// <summary>
        /// Calculates the latitude coordinate of a Maidenhead locator.
        /// </summary>
        /// <param name="maidenhead">The Maidenhead locator.</param>
        /// <param name="latitudePoint">The latitude point to calculate (North, Middle, or South). Default is Middle.</param>
        /// <param name="locatorType">The type of locator system to use (Field, Square, SubSquare, or ExtendedSquare). Default is ExtendedSquare.</param>
        /// <returns>The latitude coordinate of the Maidenhead location.</returns>
        public static double Latitude(this Maidenhead maidenhead,
            LatitudePoint latitudePoint = LatitudePoint.Middle,
            LocatorType locatorType = LocatorType.ExtendedSquare)
        {
            locatorType = EnumHelpers.Min(maidenhead.LocatorType, locatorType);
            string location = maidenhead.Location;

            double latitude = (location[1] - 'A') * 10D;

            if (locatorType >= LocatorType.Square)
                latitude += int.Parse(location.Substring(3, 1));

            if (locatorType >= LocatorType.SubSquare)
                latitude += (location[5] - 'a') / 24D;

            if (locatorType >= LocatorType.ExtendedSquare)
                latitude += int.Parse(location.Substring(7, 1)) / 240D;

            if (latitudePoint == LatitudePoint.North)
            {
                if (locatorType == LocatorType.Field)
                    latitude += 10D;
                else if (locatorType == LocatorType.Square)
                    latitude += 1D;
                else if (locatorType == LocatorType.SubSquare)
                    latitude += 1D / 24D;
                else if (locatorType == LocatorType.ExtendedSquare)
                    latitude += 1D / 240D;
            }
            else if (latitudePoint == LatitudePoint.Middle)
            {
                if (locatorType == LocatorType.Field)
                    latitude += 5D;
                else if (locatorType == LocatorType.Square)
                    latitude += 0.5D;
                else if (locatorType == LocatorType.SubSquare)
                    latitude += 1D / 48D;
                else if (locatorType == LocatorType.ExtendedSquare)
                    latitude += 1D / 480D;
            }

            return Math.Round(latitude - 90, 6);
        }

        /// <summary>
        /// Calculates the longitude coordinate of a Maidenhead locator.
        /// </summary>
        /// <param name="maidenhead">The Maidenhead locator.</param>
        /// <param name="locatorType">The type of locator system to use (Field, Square, SubSquare, or ExtendedSquare).</param>
        /// <returns>The longitude coordinate of middle of the Maidenhead locator.</returns>
        public static double Longitude(this Maidenhead maidenhead, LocatorType locatorType) =>
            maidenhead.Longitude(LongitudePoint.Middle, locatorType);

        /// <summary>
        /// Calculates the longitude coordinate of a Maidenhead location.
        /// </summary>
        /// <param name="maidenhead">The Maidenhead locator.</param>
        /// <param name="longitudePoint">The longitude point to calculate (West, Middle, or East). Default is Middle.</param>
        /// <param name="locatorType">The type of locator system to use (Field, Square, SubSquare, or ExtendedSquare). Default is ExtendedSquare.</param>
        /// <returns>The longitude coordinate of the Maidenhead location.</returns>
        public static double Longitude(this Maidenhead maidenhead,
            LongitudePoint longitudePoint = LongitudePoint.Middle,
            LocatorType locatorType = LocatorType.ExtendedSquare)
        {

            locatorType = EnumHelpers.Min(maidenhead.LocatorType, locatorType);
            string location = maidenhead.Location;

            double longitude = (location[0] - 'A') * 20;

            if (locatorType >= LocatorType.Square)
                longitude += int.Parse(location.Substring(2, 1)) * 2D;
            if (locatorType >= LocatorType.SubSquare)
                longitude += (location[4] - 'a') / 12D;
            if (locatorType >= LocatorType.ExtendedSquare)
                longitude += int.Parse(location.Substring(6, 1)) / 120D;

            if (longitudePoint == LongitudePoint.Middle)
            {
                if (locatorType == LocatorType.Field)
                    longitude += 10D;
                else if (locatorType == LocatorType.Square)
                    longitude += 1D;
                else if (locatorType == LocatorType.SubSquare)
                    longitude += 1D / 24D;
                else if (locatorType == LocatorType.ExtendedSquare)
                    longitude += 1D / 240D;
            }
            else if (longitudePoint == LongitudePoint.East)
            {
                if (locatorType == LocatorType.Field)
                    longitude += 20D;
                else if (locatorType == LocatorType.Square)
                    longitude += 2D;
                else if (locatorType == LocatorType.SubSquare)
                    longitude += 1D / 12D;
                else if (locatorType == LocatorType.ExtendedSquare)
                    longitude += 1D / 120D;
            }

            return Math.Round(longitude - 180, 6);
        }
    }
}
