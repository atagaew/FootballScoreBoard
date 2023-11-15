using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace SportRadar.FootballScoreBoard.Tests.IntegrationTests;

public class MatchReportServiceTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _testFixture;

    public MatchReportServiceTests(TestFixture testFixture)
    {
        _testFixture = testFixture;
    }

    [Fact]
    public void WhenCreatingNewMatch_Then_ItShouldAppearInTheListOfActiveMatches()
    {
        // Arrange
        var matchService = _testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchReportService = _testFixture.Scope.ServiceProvider.GetService<IMatchReportService>();
        var homeTeam = Faker.Country.Name();
        var awayTeam = Faker.Country.Name();
        var matchInfo = new Match(homeTeam, awayTeam);

        // Action
        matchService.Create(matchInfo);

        // Assert
        matchReportService.Summary().Where(f => f.Id == matchInfo.Id).Should().NotBeNull();
    }
}
