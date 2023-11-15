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

    public void Update(Match match)
    {
        var findIndex = _matches.FindIndex(m => m.Id == match.Id);
        if (findIndex >= 0)
        {
            _matches[findIndex] = match;
        }
        else
        { 
            throw new InvalidOperationException($"Match with id {match.Id} not found");
        }
    }

    public IEnumerable<Match> GetAll()
    {
        return _matches;
    }
}