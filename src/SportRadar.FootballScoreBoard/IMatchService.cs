namespace SportRadar.FootballScoreBoard;

public interface IMatchService
{
    void Create(Match matchInfo);
    
    void Update(Match matchInfo);

    void Finish(Match matchInfo);
}