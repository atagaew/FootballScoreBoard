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
}
