namespace SportRadar.FootballScoreBoard;

/// <summary>
/// Returns actual matches.
/// </summary>
internal class MatchReportService : IMatchReportService
{
    private readonly IMatchRepository _matchRepository;

    public MatchReportService(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    /// <summary>
    /// Returns summary report of inprogress matches.
    /// </summary>
    public IEnumerable<Match> Summary()
    {
        return _matchRepository
            .GetAll()
            .Where(f => !f.EndTime.HasValue)
            .OrderByDescending(k=>k.AwayTeamScore+k.HomeTeamScore).
            ThenByDescending(k=>k.StartTime);
    }
}