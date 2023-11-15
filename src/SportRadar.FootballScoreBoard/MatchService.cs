namespace SportRadar.FootballScoreBoard
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public void Create(Match matchInfo)
        {
            _matchRepository.Add(matchInfo);
        }
    }
}