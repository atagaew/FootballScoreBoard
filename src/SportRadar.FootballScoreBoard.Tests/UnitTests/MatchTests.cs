using FluentAssertions;

namespace SportRadar.FootballScoreBoard.Tests.UnitTests
{
    public partial class MatchTests
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
            var sut = new Match(homeTeam, awayTeam, homeScore, awayScore);

            // Assert
            sut.HomeScore.Should().Be(homeScore);
            sut.AwayScore.Should().Be(awayScore);
            sut.HomeTeam.Should().Be(homeTeam);
            sut.AwayTeam.Should().Be(awayTeam);

            sut.CreatedTime.Should().BeCloseTo(createdTime, TimeSpan.FromSeconds(1));
            sut.StartTime.Should().Be(sut.CreatedTime);
            sut.EndTime.Should().BeNull();
            sut.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void WhenCreatingNewMatch_Then_ItShouldBothTeamsShouldHaveZeroScore()
        {
            // Arrange
            var homeTeam = Faker.Company.Name();
            var awayTeam = Faker.Company.Name();
            var createdTime = DateTimeOffset.Now;

            // Action
            var sut = new Match(homeTeam, awayTeam);

            // Assert
            sut.HomeScore.Should().Be(0);
            sut.AwayScore.Should().Be(0);
            sut.HomeTeam.Should().Be(homeTeam);
            sut.AwayTeam.Should().Be(awayTeam);

            sut.CreatedTime.Should().BeCloseTo(createdTime, TimeSpan.FromSeconds(1));
            sut.StartTime.Should().Be(sut.CreatedTime);
            sut.EndTime.Should().BeNull();
            sut.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void WhenCreatingDifferentMatches_Then_IdShouldBeDifferent()
        {
            // Arrange
            var homeTeam = Faker.Company.Name();
            var awayTeam = Faker.Company.Name();

            // Action
            var sut1 = new Match(homeTeam, awayTeam);
            var sut2 = new Match(homeTeam, awayTeam);

            // Assert
            sut1.Id.Should().NotBe(sut2.Id);
        }

        public static IEnumerable<object[]> TestNameData()
        {
            yield return new object[] { string.Empty, string.Empty };
            yield return new object[] { null, null };
            yield return new object[] { Faker.Company.Name(), null };
            yield return new object[] { null, Faker.Company.Name() };
        }

        [Theory]
        [MemberData(nameof(TestNameData))]
        public void WhenCreatingAMatchWithEmptyTeamName_Then_ThereShouldBeAnException(string homeTeamName, string awayTeamName)
        {
            // Arrange

            // Action
            Action action = () => new Match(homeTeamName, awayTeamName);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}