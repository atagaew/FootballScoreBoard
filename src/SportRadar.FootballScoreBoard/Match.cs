namespace SportRadar.FootballScoreBoard;

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

    public Match FinishMatch(int homeTeamFinalScore, int awayTeamFinalScore)
    {
        return FinishMatch(homeTeamFinalScore, awayTeamFinalScore, null);
    }

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

    public Guid Id { get; private set; }

    public string HomeTeam { get; private set; }

    public string AwayTeam { get; private set; }

    public int HomeTeamScore { get; private set; }

    public int AwayTeamScore { get; private set; }

    public DateTimeOffset CreatedTime { get; private set; }

    public DateTimeOffset StartTime { get; private set; }

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