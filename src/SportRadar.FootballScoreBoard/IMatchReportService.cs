namespace SportRadar.FootballScoreBoard;

public interface IMatchReportService
{
    IEnumerable<Match> Summary();
}