namespace SportRadar.FootballScoreBoard;

public class Match
{
    public Match(string homeTeam, string awayTeam)
        : this(homeTeam, awayTeam, 0, 0)
    {
    }

    public Match(string homeTeam, string awayTeam, int homeScore, int awayScore)
    {
        if (string.IsNullOrEmpty(homeTeam))
            throw new ArgumentException("Value cannot be null or empty.", nameof(homeTeam));

        if (string.IsNullOrEmpty(awayTeam))
            throw new ArgumentException("Value cannot be null or empty.", nameof(homeTeam));

        Id = Guid.NewGuid();
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        HomeScore = homeScore;
        AwayScore = awayScore;
        CreatedTime = StartTime = DateTimeOffset.Now;
        EndTime = default;
    }

    public Guid Id { get; private set; }

    public string HomeTeam { get; private set; }

    public string AwayTeam { get; private set; }

    public int HomeScore { get; private set; }

    public int AwayScore { get; private set; }

    public DateTimeOffset CreatedTime { get; private set; }

    public DateTimeOffset StartTime { get; private set; }

    public DateTimeOffset? EndTime { get; private set; }
}