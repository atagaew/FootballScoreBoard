namespace SportRadar.FootballScoreBoard;

public class Match
{
    public Match(string homeTeam, string awayTeam)
        : this(homeTeam, awayTeam, 0, 0)
    {
    }

    public Match(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore)
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
        CreatedTime = StartTime = DateTimeOffset.Now;
        EndTime = default;
    }

    private static void VerifyScore(int homeScore, int awayScore)
    {
        if (homeScore < 0)
            throw new ArgumentException("Score cannot be less than zero.", nameof(homeScore));

        if (awayScore < 0)
            throw new ArgumentException("Score cannot be less than zero.", nameof(awayScore));
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

    public Guid Id { get; private set; }

    public string HomeTeam { get; private set; }

    public string AwayTeam { get; private set; }

    public int HomeTeamScore { get; private set; }

    public int AwayTeamScore { get; private set; }

    public DateTimeOffset CreatedTime { get; private set; }

    public DateTimeOffset StartTime { get; private set; }

    public DateTimeOffset? EndTime { get; private set; }
}