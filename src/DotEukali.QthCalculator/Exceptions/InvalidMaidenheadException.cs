using System;

namespace DotEukali.QthCalculator.Exceptions
{
    public class InvalidMaidenheadException : Exception
    {
        internal InvalidMaidenheadException(string maidenheadLocator)
            : base($"Maidenhead locator '{maidenheadLocator}' is invalid.")
        { }
    }
}
