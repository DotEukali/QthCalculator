using System;

namespace DotEukali.QthCalculator.Exceptions
{
    public class InvalidLatLongException : Exception
    {
        public InvalidLatLongException(double latitude, double longitude)
            : base($"The provided latitude ({latitude}) and/or longitude ({longitude}) is invalid.")
        {
        }
    }
}
