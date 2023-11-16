namespace SportRadar.FootballScoreBoard;

/// <summary>
/// Provides functionality for managing matches.
/// </summary>
public interface IMatchService
{
    /// <summary>
    /// Store the match in the library.
    /// </summary>
    void Create(Match matchInfo);

    /// <summary>
    /// Update existing match in the library.
    /// </summary>
    void Update(Match matchInfo);

    /// <summary>
    /// Finish existing match in the library.
    /// </summary>
    void Finish(Match matchInfo);
}