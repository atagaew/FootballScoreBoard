namespace SportRadar.FootballScoreBoard;

public interface IMatchRepository
{
    void Add(Match match);
    
    void Update(Match match);

    IEnumerable<Match> GetAll();
    Match GetMatch(Guid id);
}