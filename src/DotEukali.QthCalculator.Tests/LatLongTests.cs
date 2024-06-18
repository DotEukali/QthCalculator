using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DotEukali.QthCalculator.Tests
{
    public class LatLongTests
    {
        [Theory]
        [MemberData(nameof(LocationData))]
        public void Extreme_Locations_Resolve_To_Maidenhead_Location(TestLocationArea location)
        {
            double minStep = 0.000001;
            double latitudeBottom = location.LatitudeBottom + minStep;
            double latitudeTop = location.LatitudeTop - minStep;
            double longitudeLeft = location.LongitudeLeft + minStep;
            double longitudeRight = location.LongitudeRight - minStep;
            LocatorType locatorType = (LocatorType)location.Maidenhead.Length;

            List<Maidenhead> maidenheadPoints =
            [
                MaidenheadCalculator.GetMaidenhead(latitudeBottom, longitudeLeft, locatorType),
                MaidenheadCalculator.GetMaidenhead(latitudeTop, longitudeLeft, locatorType),
                MaidenheadCalculator.GetMaidenhead(latitudeTop, longitudeRight, locatorType),
                MaidenheadCalculator.GetMaidenhead(latitudeBottom, longitudeRight, locatorType),
                MaidenheadCalculator.GetMaidenhead(location.LatitudeMiddle, location.LongitudeMiddle, locatorType)
            ];

            maidenheadPoints.All(x => x.Location == location.Maidenhead).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(LocationData))]
        public void Lat_n_Long_Calculates_Successfully(TestLocationArea location)
        {
            Maidenhead maidenhead = MaidenheadCalculator.GetMaidenhead(location.Maidenhead);

            maidenhead.Latitude(LatitudePoint.South).Should().Be(location.LatitudeBottom);
            maidenhead.Latitude(LatitudePoint.North).Should().Be(location.LatitudeTop);
            Math.Round(maidenhead.Latitude(LatitudePoint.Middle), 5).Should().Be(Math.Round(location.LatitudeMiddle, 5));
            maidenhead.Longitude(LongitudePoint.West).Should().Be(location.LongitudeLeft);
            maidenhead.Longitude(LongitudePoint.East).Should().Be(location.LongitudeRight);
            Math.Round(maidenhead.Longitude(LongitudePoint.Middle), 5).Should().Be(Math.Round(location.LongitudeMiddle, 5));
        }

        public static TheoryData<TestLocationArea> LocationData =>
            new TheoryData<TestLocationArea>
            {
                new TestLocationArea("QG62ek22")
                {
                    LatitudeBottom = -27.575,
                    LatitudeTop = -27.570833,
                    LongitudeLeft = 152.35,
                    LongitudeRight = 152.358333
                },
                new TestLocationArea("QG62ek98")
                {
                    LatitudeBottom = -27.55,
                    LatitudeTop = -27.545833,
                    LongitudeLeft = 152.408333,
                    LongitudeRight = 152.416667
                },
                new TestLocationArea("QG62ek")
                {
                    LatitudeBottom = -27.583333,
                    LatitudeTop = -27.541667,
                    LongitudeLeft = 152.333333,
                    LongitudeRight = 152.416667
                },
                new TestLocationArea("QG62")
                {
                    LatitudeBottom = -28.0,
                    LatitudeTop = -27.0,
                    LongitudeLeft = 152.0,
                    LongitudeRight = 154.0
                },
                new TestLocationArea("QG")
                {
                    LatitudeBottom = -30.0,
                    LatitudeTop = -20.0,
                    LongitudeLeft = 140.0,
                    LongitudeRight = 160.0
                },
                new TestLocationArea("QH")
                {
                    LatitudeBottom = -20.0,
                    LatitudeTop = -10.0,
                    LongitudeLeft = 140.0,
                    LongitudeRight = 160
                },
                new TestLocationArea("JN68kb36")
                {
                    LatitudeBottom = 48.066667,
                    LatitudeTop = 48.070833,
                    LongitudeLeft = 12.858333,
                    LongitudeRight = 12.866667
                },
                new TestLocationArea("JN68kb")
                {
                    LatitudeBottom = 48.041667,
                    LatitudeTop = 48.083333,
                    LongitudeLeft = 12.833333,
                    LongitudeRight = 12.916667
                },
                new TestLocationArea("JN68")
                {
                    LatitudeBottom = 48.0,
                    LatitudeTop = 49.0,
                    LongitudeLeft = 12.0,
                    LongitudeRight = 14.0
                },
                new TestLocationArea("JN")
                {
                    LatitudeBottom = 40.0,
                    LatitudeTop = 50.0,
                    LongitudeLeft = 0.0,
                    LongitudeRight = 20.0
                },
                new TestLocationArea("FI09st64")
                {
                    LatitudeBottom = -0.191667,
                    LatitudeTop = -0.1875,
                    LongitudeLeft = -78.45,
                    LongitudeRight = -78.441667
                },
                new TestLocationArea("FI09st")
                {
                    LatitudeBottom = -0.208333,
                    LatitudeTop = -0.166667,
                    LongitudeLeft = -78.5,
                    LongitudeRight = -78.416667
                },
                new TestLocationArea("FI09")
                {
                    LatitudeBottom = -1.0,
                    LatitudeTop = 0.0,
                    LongitudeLeft = -80.0,
                    LongitudeRight = -78.0
                },
                new TestLocationArea("FI")
                {
                    LatitudeBottom = -10.0,
                    LatitudeTop = 0.0,
                    LongitudeLeft = -80.0,
                    LongitudeRight = -60.0
                },
                new TestLocationArea("IA80")
                {
                    LatitudeBottom = -90.0,
                    LatitudeTop = -89.0,
                    LongitudeLeft = -4.0,
                    LongitudeRight = -2.0
                },
                new TestLocationArea("IA")
                {
                    LatitudeBottom = -90.0,
                    LatitudeTop = -80.0,
                    LongitudeLeft = -20.0,
                    LongitudeRight = 0
                },
                new TestLocationArea("AR09")
                {
                    LatitudeBottom = 89.0,
                    LatitudeTop = 90.0,
                    LongitudeLeft = -180.0,
                    LongitudeRight = -178
                },
                new TestLocationArea("AR")
                {
                    LatitudeBottom = 80.0,
                    LatitudeTop = 90.0,
                    LongitudeLeft = -180.0,
                    LongitudeRight = -160.0
                }
            };
    }
}
