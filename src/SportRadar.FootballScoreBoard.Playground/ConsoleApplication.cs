using SportRadar.FootballScoreBoard;

public class ConsoleApplication
{
    private readonly IMatchService _matchService;
    private readonly IMatchReportService _matchReportService;

    public ConsoleApplication(IMatchService matchService, IMatchReportService matchReportService)
    {
        _matchService = matchService;
        _matchReportService = matchReportService;
    }

    public void Run()
    {
        var mexicoCanada = new Match("Mexico", "Canada", 0, 5);
        var spainBrasil = new Match("Spain", "Brasil", 10, 2);
        var germanyFrance = new Match("Germany", "France", 2, 2);
        var uruguayItaly = new Match("Uruguay", "Italy", 6, 6);
        var argentinaAustralia = new Match("Argentina", "Australia", 3, 1);
        var ukraineRussia = new Match("Ukraine", "Russia", 5, 0);
        _matchService.Create(mexicoCanada);
        _matchService.Create(spainBrasil);
        _matchService.Create(germanyFrance);
        _matchService.Create(uruguayItaly);
        _matchService.Create(argentinaAustralia);

        _matchService.Create(ukraineRussia);
        _matchService.Update(ukraineRussia.UpdateScore(6, 0));
        _matchService.Finish(ukraineRussia.FinishMatch(9, 0));

        var summaryReport = _matchReportService.Summary().ToArray();
        foreach (var match in summaryReport)
        {
            Console.WriteLine(match);
        }
    }
}