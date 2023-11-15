namespace SportRadar.FootballScoreBoard;

public class MatchInMemoryRepository : IMatchRepository
{
    private readonly List<Match> _matches;

    public MatchInMemoryRepository()
    {
        _matches = new List<Match>();
    }

    public void Add(Match match)
    {
        _matches.Add(match);
    }

    public IEnumerable<Match> GetAll()
    {
        return _matches;
    }
}