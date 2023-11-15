using Microsoft.Extensions.DependencyInjection;
using Moq;
using SportRadar.FootballScoreBoard.Tests.IntegrationTests;

namespace SportRadar.FootballScoreBoard.Tests.UnitTests;

public class MatchServiceTests : IClassFixture<TestFixture>
{
    [Fact]
    public async Task WhenCreatingNewMatch_Then_ItShouldCallStoreOfTheMatchRepository()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture() { MockMatchRepository = new Mock<IMatchRepository>() };
        await testFixture.InitializeAsync();
        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        // Action
        matchService.Create(matchInfo);

        // Assert
        testFixture.MockMatchRepository.Verify(f => f.Add(It.Is<Match>(m => m.Equals(matchInfo))));
    }

    [Fact]
    public async Task WhenUpdatingMatch_Then_ItShouldCallStoreOfTheMatchRepository()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture() { MockMatchRepository = new Mock<IMatchRepository>() };
        await testFixture.InitializeAsync();
        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        // Action
        matchService.Update(matchInfo);

        // Assert
        testFixture.MockMatchRepository.Verify(f => f.Update(It.Is<Match>(m => m.Equals(matchInfo))));
    }
}
