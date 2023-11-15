namespace SportRadar.FootballScoreBoard;

public interface IMatchRepository
{
    void Add(Match match);

    IEnumerable<Match> GetAll();
}