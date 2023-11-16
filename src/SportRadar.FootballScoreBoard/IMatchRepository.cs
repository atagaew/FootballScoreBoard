namespace SportRadar.FootballScoreBoard;

/// <summary>
/// Describes repository to store matches.
/// </summary>
public interface IMatchRepository
{
    /// <summary>
    /// Add match to repository.
    /// </summary>
    void Add(Match match);
    
    /// <summary>
    /// Update match in repository.
    /// </summary>
    void Update(Match match);

    /// <summary>
    /// Get all matches from repository.
    /// </summary>
    IEnumerable<Match> GetAll();

    /// <summary>
    /// Get match by id.
    /// </summary>
    Match GetMatch(Guid id);
}