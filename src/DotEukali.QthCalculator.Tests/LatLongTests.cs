using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace DotEukali.QthCalculator.Tests
{
    public class LatLongTests
    {
        [Theory]
        [MemberData(nameof(LatLongData))]
        public void Lat_n_Long_Calculates_Successfully(string maidenHeadString, double expectedLatEdge, double expectedLongEdge, double expectedLatCenter, double expectedLongCenter)
        {
            MaidenHead maidenHead = new MaidenHead(maidenHeadString);

            maidenHead.IsValid().Should().BeTrue();

            maidenHead.Latitude(false).Should().Be(expectedLatEdge);
            maidenHead.Longitude(false).Should().Be(expectedLongEdge);

            maidenHead.Latitude(true).Should().Be(expectedLatCenter);
            maidenHead.Longitude(true).Should().Be(expectedLongCenter);
        }
        
        public static IEnumerable<object[]> LatLongData()
        {
            yield return new object[] { "QG62ek22", -27.575, 152.35, -27.572917, 152.354167 };
            yield return new object[] { "QG62ek98", -27.55, 152.408333, -27.547917, 152.4125 };
            yield return new object[] { "QG62ek", -27.583333, 152.333333, -27.5625, 152.375 };
            yield return new object[] { "QG62", -28.0, 152.0, -27.5, 153.0 };
            yield return new object[] { "QG", -30.0, 140.0, -25.0, 150.0 };
            yield return new object[] { "JN68kb36", 48.066667, 12.858333, 48.06875, 12.8625 };
            yield return new object[] { "JN68kb", 48.041667, 12.833333, 48.0625, 12.875 };
            yield return new object[] { "JN68", 48, 12.0, 48.5, 13.0 };
            yield return new object[] { "JN", 40.0, 0.0, 45.0, 10.0 };
            yield return new object[] { "FI09st64", -0.191667, -78.45, -0.189583, -78.445833 };
            yield return new object[] { "FI09st", -0.208333, -78.5, -0.1875, -78.458333 };
            yield return new object[] { "FI09", -1.0, -80.0, -0.5, -79.0 };
            yield return new object[] { "FI", -10.0, -80.0, -5.0, -70.0 };

            yield return new object[] { "IA80", -90.0, -4.0, -89.5, -3.0 };
            yield return new object[] { "IA", -90.0, -20.0, -85.0, -10.0 };

            yield return new object[] { "AR09", 89.0, -180.0, 89.5, -179.0 };
            yield return new object[] { "AR", 80.0, -180.0, 85.0, -170.0 };
        }
    }
}
