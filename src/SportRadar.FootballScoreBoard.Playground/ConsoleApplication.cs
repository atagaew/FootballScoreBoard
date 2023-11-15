using SportRadar.FootballScoreBoard;

public class ConsoleApplication
{
    private readonly IMatchService _matchService;

    public ConsoleApplication(IMatchService matchService)
    {
        _matchService = matchService;
    }

    public void Run()
    {
        _matchService.Create(new Match("Mexico", "Canada", 0, 0));
        Console.WriteLine("Created");
    }
}