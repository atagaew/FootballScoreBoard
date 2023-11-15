using System.Security.Cryptography.X509Certificates;
using FluentAssertions;

namespace SportRadar.FootballScoreBoard.Tests
{
    public class MatchTests
    {
        [Fact]
        public void WhenCreatingNewMatch_Then_ItShouldReturnAllProperties()
        {
            // Arrange
            var homeScore = Faker.RandomNumber.Next();
            var awayScore = Faker.RandomNumber.Next();

            var homeTeam = Faker.Company.Name();
            var awayTeam = Faker.Company.Name();
            var createdTime = DateTimeOffset.Now;

            // Action
            var match = new Match(homeTeam, awayTeam, homeScore, awayScore);

            // Assert
            match.HomeScore.Should().Be(homeScore);
            match.AwayScore.Should().Be(awayScore);
            match.HomeTeam.Should().Be(homeTeam);
            match.AwayTeam.Should().Be(awayTeam);

            match.CreatedTime.Should().BeCloseTo(createdTime, TimeSpan.FromSeconds(1));
            match.StartTime.Should().Be(match.CreatedTime);
            match.EndTime.Should().BeNull();
        }
    }
}