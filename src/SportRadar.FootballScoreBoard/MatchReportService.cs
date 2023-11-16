namespace SportRadar.FootballScoreBoard;

internal class MatchReportService : IMatchReportService
{
    private readonly IMatchRepository _matchRepository;

    public MatchReportService(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public IEnumerable<Match> Summary()
    {
        return _matchRepository
            .GetAll()
            .Where(f => !f.EndTime.HasValue)
            .OrderByDescending(k=>k.AwayTeamScore+k.HomeTeamScore).
            ThenByDescending(k=>k.StartTime);
    }
}