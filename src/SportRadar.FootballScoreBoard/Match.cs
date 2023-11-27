namespace SportRadar.FootballScoreBoard;

/// <summary>
/// Information about the match.
/// </summary>
public record Match
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

    public Match UpdateScore(int homeTeamScore, int awayTeamScore)
    {
        VerifyScore(homeTeamScore, awayTeamScore);
        return this with
        {
            HomeTeamScore = homeTeamScore,
            AwayTeamScore = awayTeamScore
        };
    }

    public Match FinishMatch(int homeTeamFinalScore, int awayTeamFinalScore)
    {
        return FinishMatch(homeTeamFinalScore, awayTeamFinalScore, null);
    }

    public Match FinishMatch(int homeTeamFinalScore, int awayTeamFinalScore, DateTimeOffset? endTime)
    {
        if (EndTime.HasValue)
            throw new ArgumentException("Can't finish the match that's already finished.", nameof(endTime));

        VerifyScore(homeTeamFinalScore, awayTeamFinalScore);
        if (endTime.HasValue && endTime.Value < StartTime)
            throw new ArgumentException("End time cannot be less than start time.", nameof(endTime));

        return this with
        {
            HomeTeamScore = homeTeamFinalScore,
            AwayTeamScore = awayTeamFinalScore,
            EndTime = endTime == null ? DateTimeOffset.Now : endTime
        };
    }

    public Guid Id { get; init; }
    public string HomeTeam { get; init; }
    public string AwayTeam { get; init; }
    public int HomeTeamScore { get; init; }
    public int AwayTeamScore { get; init; }
    public DateTimeOffset CreatedTime { get; init; }
    public DateTimeOffset StartTime { get; init; }
    public DateTimeOffset? EndTime { get; init; }

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
