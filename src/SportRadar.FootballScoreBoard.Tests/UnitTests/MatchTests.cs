using FluentAssertions;

namespace SportRadar.FootballScoreBoard.Tests.UnitTests
{
    public partial class MatchTests
    {
        [Fact]
        public void WhenCreatingNewMatch_Then_ItShouldReturnAllProperties()
        {
            // Arrange
            var homeScore = Faker.RandomNumber.Next(0, 200);
            var awayScore = Faker.RandomNumber.Next(0, 200);

            var homeTeam = Faker.Company.Name();
            var awayTeam = Faker.Company.Name();
            var createdTime = DateTimeOffset.Now;

            // Action
            var sut = new Match(homeTeam, awayTeam, homeScore, awayScore);

            // Assert
            sut.HomeTeamScore.Should().Be(homeScore);
            sut.AwayTeamScore.Should().Be(awayScore);
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
            sut.HomeTeamScore.Should().Be(0);
            sut.AwayTeamScore.Should().Be(0);
            sut.HomeTeam.Should().Be(homeTeam);
            sut.AwayTeam.Should().Be(awayTeam);

            sut.CreatedTime.Should().BeCloseTo(createdTime, TimeSpan.FromSeconds(1));
            sut.StartTime.Should().Be(sut.CreatedTime);
            sut.EndTime.Should().BeNull();
            sut.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void WhenCreatingNewMatch_Then_ItShouldBePossibleToSetStartTimeOfTheMatch()
        {
            // Arrange
            var homeScore = Faker.RandomNumber.Next(0, 200);
            var awayScore = Faker.RandomNumber.Next(0, 200);

            var homeTeam = Faker.Company.Name();
            var awayTeam = Faker.Company.Name();
            var createdTime = DateTimeOffset.Now;

            // Action
            var customStartTime = DateTimeOffset.Now.AddDays(Faker.RandomNumber.Next(-1, -200));
            var sut = new Match(homeTeam, awayTeam, homeScore, awayScore, customStartTime);

            // Assert
            sut.HomeTeamScore.Should().Be(homeScore);
            sut.AwayTeamScore.Should().Be(awayScore);
            sut.HomeTeam.Should().Be(homeTeam);
            sut.AwayTeam.Should().Be(awayTeam);

            sut.CreatedTime.Should().BeCloseTo(createdTime, TimeSpan.FromSeconds(1));
            sut.StartTime.Should().Be(customStartTime);
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

        public static IEnumerable<object[]> TestNegativeScoreData()
        {
            yield return new object[] { -1, 0 };
            yield return new object[] { -1, 1 };
            yield return new object[] { 0, -1 };
            yield return new object[] { 1, -1 };
            yield return new object[] { -1, -1 };
        }

        [Theory]
        [MemberData(nameof(TestNegativeScoreData))]
        public void WhenCreatingNewMatch_AndScoreIsNegative_Then_ThereShouldBeAnException(int homeTeamScore, int awayTeamScore)
        {
            // Arrange

            // Action
            Action action = () => new Match(Faker.Country.Name(), Faker.Country.Name(), homeTeamScore, awayTeamScore);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("Score cannot be less than zero.*");
        }

        [Theory]
        [MemberData(nameof(TestNegativeScoreData))]
        public void WhenUpdateMatch_AndScoreIsNegative_Then_ThereShouldBeAnException(int homeTeamScore, int awayTeamScore)
        {
            // Arrange
            var match = new Match(Faker.Country.Name(), Faker.Country.Name(), Faker.RandomNumber.Next(0, 200), Faker.RandomNumber.Next(0, 200));

            // Action
            Action action = () =>
            {
                match.UpdateScore(homeTeamScore, awayTeamScore);
            };

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("Score cannot be less than zero.*");
        }

        [Fact]
        public void WhenUpdateMatch_ItShouldReturnNewInstanceOfTheMatch_AndInformationFieldsShouldBeTheSame()
        {
            // Arrange
            var match = new Match(Faker.Country.Name(), Faker.Country.Name(), Faker.RandomNumber.Next(0, 200), Faker.RandomNumber.Next(0, 200));

            // Action
            var homeTeamUpdatedScore = Faker.RandomNumber.Next(0, 200);
            var awayTeamUpdatedScore = Faker.RandomNumber.Next(0, 200);
            var updatedMatch = match.UpdateScore(homeTeamUpdatedScore, awayTeamUpdatedScore);

            // Assert
            updatedMatch.Should().NotBeEquivalentTo(match);
            updatedMatch.HomeTeamScore.Should().Be(homeTeamUpdatedScore);
            updatedMatch.AwayTeamScore.Should().Be(awayTeamUpdatedScore);
            updatedMatch.Id.Should().Be(match.Id);
            updatedMatch.HomeTeam.Should().Be(match.HomeTeam);
            updatedMatch.AwayTeam.Should().Be(match.AwayTeam);
            updatedMatch.CreatedTime.Should().Be(match.CreatedTime);
            updatedMatch.StartTime.Should().Be(match.StartTime);
            updatedMatch.EndTime.Should().Be(match.EndTime);
        }

        [Fact]
        public void WhenFinishMatch_Then_ItShouldReturnNewInstanceOfTheMatch_AndInformationFieldsShouldBeTheSame_AndEndTimeEndTimeShouldBeUpdated()
        {
            // Arrange
            var match = new Match(Faker.Country.Name(), Faker.Country.Name(), Faker.RandomNumber.Next(0, 200), Faker.RandomNumber.Next(0, 200));

            // Action
            var homeTeamFinalScore = Faker.RandomNumber.Next(0, 200);
            var awayTeamFinalScore = Faker.RandomNumber.Next(0, 200);
            var finishedMatch = match.FinishMatch(homeTeamFinalScore, awayTeamFinalScore);

            // Assert
            finishedMatch.Should().NotBeEquivalentTo(match);
            finishedMatch.HomeTeamScore.Should().Be(homeTeamFinalScore);
            finishedMatch.AwayTeamScore.Should().Be(awayTeamFinalScore);
            finishedMatch.Id.Should().Be(match.Id);
            finishedMatch.HomeTeam.Should().Be(match.HomeTeam);
            finishedMatch.AwayTeam.Should().Be(match.AwayTeam);
            finishedMatch.CreatedTime.Should().Be(match.CreatedTime);
            finishedMatch.StartTime.Should().Be(match.StartTime);
            finishedMatch.EndTime.Should().NotBeNull();
            finishedMatch.EndTime.Should().NotBeNull().And.Subject!.Value.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void WhenFinishMatch_AndThereCustomEndDate_AndThereIsCustomFinishDate_Then_ItShouldUpdateTheFinishDate()
        {
            // Arrange
            var customStartDate = DateTimeOffset.Now.AddDays(Faker.RandomNumber.Next(-100, -200));
            var customEndDate = DateTimeOffset.Now.AddDays(Faker.RandomNumber.Next(-1, -99));
            var match = new Match(Faker.Country.Name(), Faker.Country.Name(), Faker.RandomNumber.Next(0, 200), Faker.RandomNumber.Next(0, 200), customStartDate);

            // Action
            var homeTeamFinalScore = Faker.RandomNumber.Next(0, 200);
            var awayTeamFinalScore = Faker.RandomNumber.Next(0, 200);
            var finishedMatch = match.FinishMatch(homeTeamFinalScore, awayTeamFinalScore, customEndDate);

            // Assert
            finishedMatch.Should().NotBeEquivalentTo(match);
            finishedMatch.HomeTeamScore.Should().Be(homeTeamFinalScore);
            finishedMatch.AwayTeamScore.Should().Be(awayTeamFinalScore);
            finishedMatch.Id.Should().Be(match.Id);
            finishedMatch.HomeTeam.Should().Be(match.HomeTeam);
            finishedMatch.AwayTeam.Should().Be(match.AwayTeam);
            finishedMatch.CreatedTime.Should().Be(match.CreatedTime);
            finishedMatch.StartTime.Should().Be(match.StartTime);
            finishedMatch.EndTime.Should().NotBeNull();
            finishedMatch.EndTime.Should().NotBeNull().And.Subject!.Value.Should().Be(customEndDate);
        }

        [Fact]
        public void WhenFinishMatch_AndThereCustomEndDate_AndFinishDateLessThanStartDate_Then_ItShouldThrowAnException()
        {
            // Arrange
            var customStartDate = DateTimeOffset.Now.AddDays(Faker.RandomNumber.Next(-100, -200));
            var customEndDate = DateTimeOffset.Now.AddDays(Faker.RandomNumber.Next(-201, -401));
            var match = new Match(Faker.Country.Name(), Faker.Country.Name(), Faker.RandomNumber.Next(0, 200), Faker.RandomNumber.Next(0, 200), customStartDate);
            var homeTeamFinalScore = Faker.RandomNumber.Next(0, 200);
            var awayTeamFinalScore = Faker.RandomNumber.Next(0, 200);

            // Action
            Action action = () =>
            {
                match.FinishMatch(homeTeamFinalScore, awayTeamFinalScore, customEndDate);
            };

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("End time cannot be less than start time.*");
        }

        [Fact]
        public void WhenFinishMatch_AndTheMatchIsAlreadyFinished_Then_ItShouldThrowAnException()
        {
            // Arrange
            var customStartDate = DateTimeOffset.Now.AddDays(Faker.RandomNumber.Next(-100, -200));
            var customEndDate = DateTimeOffset.Now.AddDays(Faker.RandomNumber.Next(-1, -99));
            var match = new Match(Faker.Country.Name(), Faker.Country.Name(), Faker.RandomNumber.Next(0, 200), Faker.RandomNumber.Next(0, 200), customStartDate);
            var homeTeamFinalScore = Faker.RandomNumber.Next(0, 200);
            var awayTeamFinalScore = Faker.RandomNumber.Next(0, 200);
            var finishedMatch = match.FinishMatch(homeTeamFinalScore, awayTeamFinalScore, customEndDate);

            // Action
            Action action = () =>
            {
                finishedMatch.FinishMatch(homeTeamFinalScore, awayTeamFinalScore, customEndDate);
            };

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("Can't finish the match that's alrady finished.*");
        }

        [Theory]
        [MemberData(nameof(TestNegativeScoreData))]
        public void WhenFinishMatch_AndScoreIsNegative_Then_ThereShouldBeAnException(int homeTeamScore, int awayTeamScore)
        {
            // Arrange
            var match = new Match(Faker.Country.Name(), Faker.Country.Name(), Faker.RandomNumber.Next(0, 200), Faker.RandomNumber.Next(0, 200));

            // Action
            Action action = () =>
            {
                match.FinishMatch(homeTeamScore, awayTeamScore);
            };

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("Score cannot be less than zero.*");
        }
    }
}