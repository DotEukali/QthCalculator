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
            MaidenHead maidenHead = new MaidenHead(maidenHeadString);

            maidenHead.IsValid().Should().BeTrue();

            maidenHead.Latitude(granularity).Should().Be(expectedLat);
            maidenHead.Longitude(granularity).Should().Be(expectedLong);
        }

        [Fact]
        public void Invalid_Granularity_Does_Throw()
        {
            MaidenHead maidenHead = new MaidenHead("AA00aa");

            maidenHead.IsValid().Should().BeTrue();

            FluentActions.Invoking(() => maidenHead.Latitude(3)).Should().Throw<Exception>();
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
            MaidenHead maidenHead = new MaidenHead(maidenheadString);

            maidenHead.IsValid(false).Should().Be(isValid);
            maidenHead.IsValid(true).Should().Be(isStrictValid);
        }
    }
}
