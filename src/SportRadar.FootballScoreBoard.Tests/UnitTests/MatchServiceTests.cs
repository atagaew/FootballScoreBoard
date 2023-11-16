using FluentAssertions;
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
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        testFixture.MockMatchRepository.Setup(f => f.GetMatch(It.Is<Guid>(i => i == matchInfo.Id))).Returns(matchInfo);

        await testFixture.InitializeAsync();
        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();

        // Action
        matchService.Update(matchInfo);

        // Assert
        testFixture.MockMatchRepository.Verify(f => f.Update(It.Is<Match>(m => m.Equals(matchInfo))));
    }

    [Fact]
    public async Task WhenUpdatingMatch_AndItIsNotExisting_Then_ItShouldThrowAnException()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture() { MockMatchRepository = new Mock<IMatchRepository>() };
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        testFixture.MockMatchRepository.Setup(f => f.GetMatch(It.Is<Guid>(i => i == matchInfo.Id))).Returns((Match)null);

        await testFixture.InitializeAsync();
        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();

        // Action
        Action act = () => matchService.Update(matchInfo);

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Match with id * not found");
    }

    [Fact]
    public async Task WhenUpdatingMatch_AndTheMatchIsAlreadyFinished_Then_ItShouldThrowAnException()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture() { MockMatchRepository = new Mock<IMatchRepository>() };
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        testFixture.MockMatchRepository.Setup(f => f.GetMatch(It.Is<Guid>(i => i == matchInfo.Id))).Returns(matchInfo.FinishMatch(0, 0));

        await testFixture.InitializeAsync();
        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();

        // Action
        Action act = () => matchService.Update(matchInfo);

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Match with id * not found");
    }

    [Fact]
    public async Task WhenFinishingMatch_Then_ItShoudCallStoreOfTheMatchRepository()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture() { MockMatchRepository = new Mock<IMatchRepository>() };
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        testFixture.MockMatchRepository.Setup(f => f.GetMatch(It.Is<Guid>(i => i == matchInfo.Id))).Returns(matchInfo);

        await testFixture.InitializeAsync();
        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();
        matchService.Create(matchInfo);
        matchInfo = matchInfo.FinishMatch(matchInfo.HomeTeamScore, matchInfo.AwayTeamScore);

        // Action
        matchService.Finish(matchInfo);

        // Assert
        testFixture.MockMatchRepository.Verify(f => f.Update(It.Is<Match>(m => m.Equals(matchInfo))));
    }

    [Fact]
    public async Task WhenFinishingMatch_AndItIsNotExisting_Then_ItShouldThrowAnException()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture() { MockMatchRepository = new Mock<IMatchRepository>() };
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        testFixture.MockMatchRepository.Setup(f => f.GetMatch(It.Is<Guid>(i => i == matchInfo.Id))).Returns((Match)null);

        await testFixture.InitializeAsync();
        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();

        // Action
        Action act = () => matchService.Finish(matchInfo);

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Match with id * not found");
    }

    [Fact]
    public async Task WhenFinishingMatch_AndTheMatchIsAlreadyFinished_Then_ItShouldThrowAnException()
    {
        // Arrange
        await using TestFixture testFixture = new TestFixture() { MockMatchRepository = new Mock<IMatchRepository>() };
        var matchInfo = new Match(Faker.Country.Name(), Faker.Country.Name());

        testFixture.MockMatchRepository.Setup(f => f.GetMatch(It.Is<Guid>(i => i == matchInfo.Id))).Returns(matchInfo.FinishMatch(0, 0));

        await testFixture.InitializeAsync();
        var matchService = testFixture.Scope.ServiceProvider.GetService<IMatchService>();

        // Action
        Action act = () => matchService.Finish(matchInfo);

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Match with id * not found");
    }
}
