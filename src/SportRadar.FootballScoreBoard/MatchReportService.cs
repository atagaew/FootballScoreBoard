namespace SportRadar.FootballScoreBoard;

public class MatchReportService : IMatchReportService
{
    private readonly IMatchRepository _matchRepository;

    public MatchReportService(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public IEnumerable<Match> Summary()
    {
        return _matchRepository.GetAll();
    }
}