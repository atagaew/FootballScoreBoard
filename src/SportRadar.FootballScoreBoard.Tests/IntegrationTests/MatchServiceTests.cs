using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace SportRadar.FootballScoreBoard.Tests.IntegrationTests;

public class MatchServiceTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _testFixture;

    public MatchServiceTests(TestFixture testFixture)
    {
        _testFixture = testFixture;
    }

    [Fact]
    public void WhenCreatingNewMatch_Then_ItShouldAppearInTheListOfActiveMatches()
    {
        // Arrange
        var matchService = _testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchRepository = _testFixture.Scope.ServiceProvider.GetService<IMatchRepository>();
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        // Action
        matchService.Create(matchInfo);

        // Assert
        matchRepository.GetAll().Where(f => f.Id == matchInfo.Id).Should().NotBeNull();
    }

    [Fact]
    public void WhenUpdateExistingMatch_Then_ItShouldAppearInTheListOfActiveMatches()
    {
        // Arrange
        var matchService = _testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchRepository = _testFixture.Scope.ServiceProvider.GetService<IMatchRepository>();
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());
        matchService.Create(matchInfo);

        // Action
        matchInfo.UpdateScore(1, 1);
        matchService.Update(matchInfo);

        // Assert
        matchRepository.GetAll().Where(f => f.Id == matchInfo.Id).Should().NotBeNull();
    }

    [Fact]
    public void WhenUpdateExistingMatch_AndSuchMatchDoesNotExist_ThenItShouldThrowNotFoundException()
    {
        // Arrange
        var matchService = _testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        // Action
        Action act = () => matchService.Update(matchInfo);

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Match with id * not found");
    }
}