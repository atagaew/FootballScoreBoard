namespace SportRadar.FootballScoreBoard;

/// <summary>
/// Describe repository for matches stored in the memory.
/// </summary>
internal class MatchInMemoryRepository : IMatchRepository
{
    private readonly List<Match> _matches;

    public MatchInMemoryRepository()
    {
        _matches = new List<Match>();
    }

    /// <summary>
    /// Add match to memory collection.
    /// </summary>
    public void Add(Match match)
    {
        _matches.Add(match);
    }

    /// <summary>
    /// Update match in memory collection.
    /// </summary>
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

    /// <summary>
    /// Returns match by id.
    /// </summary>
    public Match GetMatch(Guid id)
    {
        return _matches.FirstOrDefault(m => m.Id == id);
    }

    /// <summary>
    /// Returns all matches.
    /// </summary>
    public IEnumerable<Match> GetAll()
    {
        return _matches;
    }
}