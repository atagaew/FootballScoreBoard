namespace SportRadar.FootballScoreBoard;

/// <summary>
/// Returns actual matches.
/// </summary>
public interface IMatchReportService
{
    /// <summary>
    /// Returns summary report of inprogress matches.
    /// </summary>
    IEnumerable<Match> Summary();

    int? GetTeamScores(string teamName);
}