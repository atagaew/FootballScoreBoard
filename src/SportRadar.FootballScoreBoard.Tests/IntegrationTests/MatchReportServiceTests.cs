using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace SportRadar.FootballScoreBoard.Tests.IntegrationTests;

public class MatchReportServiceTests : IClassFixture<TestFixture>
{
    [Fact]
    public async Task WhenCreatingNewMatch_Then_ItShouldAppearInTheListOfActiveMatches()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture();
        await testFixture.InitializeAsync();

        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchReportService = testFixture.Scope.ServiceProvider.GetService<IMatchReportService>();
        var homeTeam = Faker.Country.Name();
        var awayTeam = Faker.Country.Name();
        var matchInfo = new Match(homeTeam, awayTeam);

        // Action
        matchService.Create(matchInfo);

        // Assert
        matchReportService.Summary().Where(f => f.Id == matchInfo.Id).Should().NotBeNull();
    }

    [Fact]
    public async Task WhenThereAreManyInProgressMatches_Then_ItShouldBeOrderedByTotalScore()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture();
        await testFixture.InitializeAsync();

        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchReportService = testFixture.Scope.ServiceProvider.GetService<IMatchReportService>();
        var mexicoCanada = new Match("Mexico", "Canada", 0, 5);
        var spainBrasil = new Match("Spain", "Brasil", 10, 2);
        var germanyFrance = new Match("Germany", "France", 2, 2);
        var uruguayItaly = new Match("Uruguay", "Italy", 6, 6);
        var argentinaAustralia = new Match("Argentina", "Australia", 3, 1);
        matchService.Create(mexicoCanada);
        matchService.Create(spainBrasil);
        matchService.Create(germanyFrance);
        matchService.Create(uruguayItaly);
        matchService.Create(argentinaAustralia);

        // Action
        var summaryReport = matchReportService.Summary().Select(f => f.Id).ToArray();

        // Assert
        var expectedOrder = new[]
        {
            uruguayItaly.Id,
            spainBrasil.Id,
            mexicoCanada.Id,
            argentinaAustralia.Id,
            germanyFrance.Id
        };
        summaryReport.Should().Equal(expectedOrder);
    }

    [Fact]
    public async Task WhenTheMatchFinish_Then_ItShouldBeRemovedFromSummary()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture();
        await testFixture.InitializeAsync();

        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchReportService = testFixture.Scope.ServiceProvider.GetService<IMatchReportService>();
        var mexicoCanada = new Match("Mexico", "Canada", 0, 5);
        var spainBrasil = new Match("Spain", "Brasil", 10, 2);
        var germanyFrance = new Match("Germany", "France", 2, 2);
        var uruguayItaly = new Match("Uruguay", "Italy", 6, 6);
        var argentinaAustralia = new Match("Argentina", "Australia", 3, 1);
        matchService.Create(mexicoCanada);
        matchService.Create(spainBrasil);
        matchService.Create(germanyFrance);
        matchService.Create(uruguayItaly);
        matchService.Create(argentinaAustralia);

        // Action
        spainBrasil = spainBrasil.FinishMatch(spainBrasil.HomeTeamScore, spainBrasil.AwayTeamScore);
        matchService.Finish(spainBrasil);

        var summaryReport = matchReportService.Summary().Select(f => f.Id).ToArray();

        // Assert
        var expectedOrder = new[]
        {
            uruguayItaly.Id,
            mexicoCanada.Id,
            argentinaAustralia.Id,
            germanyFrance.Id
        };

        summaryReport.Should().Equal(expectedOrder);
    }

    [Fact]
    public async Task WhenGetTeamScoresForHomeTeam_AndMatchIsInProgress_Then_ItShouldReturnTheRightScoreOfHomeTeam()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture();
        await testFixture.InitializeAsync();

        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchReportService = testFixture.Scope.ServiceProvider.GetService<IMatchReportService>();
        var homeTeam = "Spain";
        var expectedScore = 10;
        var spainBrasil = new Match(homeTeam, "Brasil", expectedScore, 2);

        // Action
        matchService.Create(spainBrasil);

        var score = matchReportService.GetTeamScores(homeTeam);

        // Assert
        score.Should().Be(expectedScore);
    }

    [Fact]
    public async Task WhenGetTeamScoresForAwayTeam_AndMatchIsInProgress_Then_ItShouldReturnTheRightScoreOfAwayTeam()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture();
        await testFixture.InitializeAsync();

        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchReportService = testFixture.Scope.ServiceProvider.GetService<IMatchReportService>();
        var awayTeam = "Brasil";
        var expectedScore = 2;
        var spainBrasil = new Match("Spain", awayTeam, 10, expectedScore);

        // Action
        matchService.Create(spainBrasil);

        var score = matchReportService.GetTeamScores(awayTeam);

        // Assert
        score.Should().Be(expectedScore);
    }

    [Fact]
    public async Task WhenGetTeamScores_AndMatchFinished_Then_ItShouldReturnNull()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture();
        await testFixture.InitializeAsync();

        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchReportService = testFixture.Scope.ServiceProvider.GetService<IMatchReportService>();
        var homeTeam = "Spain";
        var expectedScore = 10;
        var spainBrasil = new Match(homeTeam, "Brasil", expectedScore, 2);
        matchService.Create(spainBrasil);

        // Action
        spainBrasil = spainBrasil.FinishMatch(spainBrasil.HomeTeamScore, spainBrasil.AwayTeamScore);
        matchService.Finish(spainBrasil);

        var score = matchReportService.GetTeamScores(homeTeam);

        // Assert
        score.Should().BeNull();
    }

    [Fact]
    public async Task WhenGetTeamScores_AndOneMatchFinished_ButSecondIsInProgress_Then_ItShouldReturnScoreOfInProgressMatch()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture();
        await testFixture.InitializeAsync();

        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchReportService = testFixture.Scope.ServiceProvider.GetService<IMatchReportService>();
        var homeTeam = "Spain";
        var expectedScore = 11;
        var spainBrasil = new Match(homeTeam, "Brasil", Faker.RandomNumber.Next(0, 10), Faker.RandomNumber.Next(0, 10));
        matchService.Create(spainBrasil);
        spainBrasil = spainBrasil.FinishMatch(spainBrasil.HomeTeamScore, spainBrasil.AwayTeamScore);
        matchService.Finish(spainBrasil);

        // Action
        var spainGermany = new Match(homeTeam, "Germany", expectedScore, Faker.RandomNumber.Next(0, 10));
        matchService.Create(spainGermany);
        var score = matchReportService.GetTeamScores(homeTeam);

        // Assert
        score.Should().Be(expectedScore);
    }
}
