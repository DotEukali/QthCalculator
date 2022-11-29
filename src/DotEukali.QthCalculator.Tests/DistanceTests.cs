﻿using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DotEukali.QthCalculator.Tests
{
    public class DistanceTests
    {
        [Theory]
        [MemberData(nameof(DistanceData))]
        public void Distance_Calculates_Successfully(string a, string b, int kms, int miles, int mtrs, int yards)
        {
            MaidenHead start = MaidenHead.Build(a);
            MaidenHead finish = MaidenHead.Build(b);

            start.DistanceTo(finish).Should().Be(kms);
            start.DistanceTo(finish, UnitOfMeasure.Meters).Should().Be(mtrs);
            start.DistanceTo(finish, UnitOfMeasure.Miles).Should().Be(miles);
            start.DistanceTo(finish, UnitOfMeasure.Yards).Should().Be(yards);
        }

        public static IEnumerable<object[]> DistanceData()
        {
            yield return new object[] { "QG62ek22", "QG62ek98", 6, 4, 6387, 6985 };
            yield return new object[] { "QG62ek", "JN68kb", 15860, 9855, 15859601, 17344270 };
            yield return new object[] { "QG62", "FI09", 13657, 8486, 13657314, 14935820 };
            yield return new object[] { "QG", "FI", 14556, 9045, 14555885, 15918510 };
        }
    }
}
