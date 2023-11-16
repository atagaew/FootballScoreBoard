namespace SportRadar.FootballScoreBoard;

/// <summary>
/// Information about the match.
/// </summary>
public class Match
{
    public Match(string homeTeam, string awayTeam)
        : this(homeTeam, awayTeam, 0, 0)
    {
    }

    public Match(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore)
        : this(homeTeam, awayTeam, homeTeamScore, awayTeamScore, null)
    {
    }

    public Match(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore, DateTimeOffset? startTime)
    {
        if (string.IsNullOrEmpty(homeTeam))
            throw new ArgumentException("Value cannot be null or empty.", nameof(homeTeam));

        if (string.IsNullOrEmpty(awayTeam))
            throw new ArgumentException("Value cannot be null or empty.", nameof(homeTeam));

        VerifyScore(homeTeamScore, awayTeamScore);

        Id = Guid.NewGuid();
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        HomeTeamScore = homeTeamScore;
        AwayTeamScore = awayTeamScore;
        CreatedTime = DateTimeOffset.Now;
        StartTime = (DateTimeOffset)(startTime.HasValue ? startTime : CreatedTime);
        EndTime = default;
    }

    /// <summary>
    /// Creates copy of the match with updated score.
    /// </summary>
    public Match UpdateScore(int homeTeamScore, int awayTeamScore)
    {
        VerifyScore(homeTeamScore, awayTeamScore);
        return new Match(HomeTeam, AwayTeam, homeTeamScore, awayTeamScore)
        {
            CreatedTime = CreatedTime,
            StartTime = StartTime,
            EndTime = EndTime,
            Id = Id
        };
    }

    /// <summary>
    /// Creates copy of the finished match with updated score.
    /// </summary>
    public Match FinishMatch(int homeTeamFinalScore, int awayTeamFinalScore)
    {
        return FinishMatch(homeTeamFinalScore, awayTeamFinalScore, null);
    }

    /// <summary>
    /// Creates copy of the finished match with updated score.
    /// </summary>
    public Match FinishMatch(int homeTeamFinalScore, int awayTeamFinalScore, DateTimeOffset? endTime)
    {
        if (EndTime.HasValue)
            throw new ArgumentException("Can't finish the match that's alrady finished.", nameof(endTime));

        VerifyScore(homeTeamFinalScore, awayTeamFinalScore);
        if (endTime.HasValue && endTime.Value < StartTime)
            throw new ArgumentException("End time cannot be less than start time.", nameof(endTime));

        return new Match(HomeTeam, AwayTeam, homeTeamFinalScore, awayTeamFinalScore)
        {
            CreatedTime = CreatedTime,
            StartTime = StartTime,
            EndTime = endTime == null ? DateTimeOffset.Now : endTime,
            Id = Id
        };
    }

    /// <summary>
    /// Gets internal id of the match.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets home team name.
    /// </summary>
    public string HomeTeam { get; private set; }

    /// <summary>
    /// Gets away team name.
    /// </summary>
    public string AwayTeam { get; private set; }

    /// <summary>
    /// Gets home team score.
    /// </summary>
    public int HomeTeamScore { get; private set; }

    /// <summary>
    /// Gets away team score.
    /// </summary>
    public int AwayTeamScore { get; private set; }

    /// <summary>
    /// Gets time of match record initial creation.
    /// </summary>
    public DateTimeOffset CreatedTime { get; private set; }

    /// <summary>
    /// Gets time when the match was started.
    /// </summary>
    public DateTimeOffset StartTime { get; private set; }

    /// <summary>
    /// Gets time when the match was finished.
    /// </summary>
    public DateTimeOffset? EndTime { get; private set; }

    private static void VerifyScore(int homeScore, int awayScore)
    {
        if (homeScore < 0)
            throw new ArgumentException("Score cannot be less than zero.", nameof(homeScore));

        if (awayScore < 0)
            throw new ArgumentException("Score cannot be less than zero.", nameof(awayScore));
    }

    public override string ToString()
    {
        return $"{HomeTeam} {HomeTeamScore} - {AwayTeam} {AwayTeamScore}";
    }
}