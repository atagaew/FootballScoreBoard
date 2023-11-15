using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SportRadar.FootballScoreBoard.Tests.IntegrationTests;

namespace SportRadar.FootballScoreBoard.Tests.UnitTests;

public class MatchInMemoryRepositoryTests : IClassFixture<TestFixture>
{
    [Fact]
    public async Task WhenAdd_Then_ItShouldBeStoredInternally()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture();
        await testFixture.InitializeAsync();
        var sut = testFixture.Scope.ServiceProvider.GetService<IMatchRepository>();
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        // Action
        sut.Add(matchInfo);

        // Assert
        sut.GetAll().Should().Contain(matchInfo);
    }
}