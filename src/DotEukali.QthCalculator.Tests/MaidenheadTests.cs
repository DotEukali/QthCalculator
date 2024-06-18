using System;
using FluentAssertions;
using Xunit;

namespace DotEukali.QthCalculator.Tests
{
    public class MaidenheadTests
    {
        [Theory]
        [InlineData("JN68kb36", 2, 45.0, 10.0)]
        [InlineData("JN68kb", 2, 45.0, 10.0)]
        [InlineData("JN68", 2, 45.0, 10.0)]
        [InlineData("JN", 2, 45.0, 10.0)]
        [InlineData("JN68kb36", 4, 48.5, 13.0)]
        [InlineData("JN68kb", 4, 48.5, 13.0)]
        [InlineData("JN68", 4, 48.5, 13.0)]
        public void Granularity_Works(string maidenHeadString, int granularity, double expectedLat, double expectedLong)
        {
            Maidenhead maidenhead = MaidenheadCalculator.GetMaidenhead(maidenHeadString);

            maidenhead.Latitude((LocatorType)granularity).Should().Be(expectedLat);
            maidenhead.Longitude((LocatorType)granularity).Should().Be(expectedLong);
        }

        [Theory]
        [InlineData("JN68kb", true, true)]
        [InlineData("Jn68kB", true, false)]
        [InlineData("JZ68kb", false, false)]
        [InlineData("A", false, false)]
        [InlineData("AA", true, true)]
        [InlineData("AAA", false, false)]
        [InlineData("AA11", true, true)]
        [InlineData("AA11aa", true, true)]
        [InlineData("AAaa", false, false)]
        [InlineData("AA11aa11", true, true)]
        [InlineData("AA1111", false, false)]
        [InlineData("AA11a", false, false)]
        [InlineData("AA11a1", false, false)]
        public void Maidenhead_Validation_Works(string maidenheadString, bool isValid, bool isStrictValid)
        {
            MaidenheadCalculator.IsValidMaidenheadReference(maidenheadString, false).Should().Be(isValid);
            MaidenheadCalculator.IsValidMaidenheadReference(maidenheadString, true).Should().Be(isStrictValid);

            MaidenheadCalculator.TryGetMaidenhead(maidenheadString, out _).Should().Be(isValid);

            if (isValid)
            {
                FluentActions.Invoking(() => MaidenheadCalculator.GetMaidenhead(maidenheadString)).Should().NotThrow();
            }
            else
            {
                FluentActions.Invoking(() => MaidenheadCalculator.GetMaidenhead(maidenheadString)).Should().Throw<Exception>();
            }

        }

        [Fact]
        public void Maidenheads_Objects_With_Equal_Values_Are_Equal()
        {
            Maidenhead mh1 = MaidenheadCalculator.GetMaidenhead("AA00aa00");
            Maidenhead mh2 = MaidenheadCalculator.GetMaidenhead("aa00aa00");

            mh1.Equals(mh2).Should().BeTrue();
        }

        [Fact]
        public void ToString_Returns_Maidenhead_String()
        {
            Maidenhead mh1 = MaidenheadCalculator.GetMaidenhead("AA00aa00");

            mh1.ToString().Should().Be("AA00aa00");
        }
    }
}
