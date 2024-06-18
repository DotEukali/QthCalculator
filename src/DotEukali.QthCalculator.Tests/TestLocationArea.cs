using System;

namespace DotEukali.QthCalculator.Tests;

public record TestLocationArea(string Maidenhead)
{
    public double LatitudeBottom { get; init; }
    public double LatitudeTop { get; init; }

    public double LongitudeLeft { get; init; }
    public double LongitudeRight { get; init; }

    public double LatitudeMiddle => Math.Round((LatitudeTop + LatitudeBottom) / 2, 6);
    public double LongitudeMiddle => Math.Round((LongitudeLeft + LongitudeRight) / 2, 6);
}
