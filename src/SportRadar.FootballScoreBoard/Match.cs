namespace SportRadar.FootballScoreBoard;

public class Match
{
    public Match(string homeTeam, string awayTeam, int homeScore, int awayScore)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        HomeScore = homeScore;
        AwayScore = awayScore;
        CreatedTime = StartTime = DateTimeOffset.Now;
        EndTime = default;
    }

    public string HomeTeam { get; private set; }

    public string AwayTeam { get; private set; }

    public int HomeScore { get; private set; }

    public int AwayScore { get; private set; }

    public DateTimeOffset CreatedTime { get; private set; }

    public DateTimeOffset StartTime { get; private set; }

    public DateTimeOffset? EndTime { get; private set; }
}