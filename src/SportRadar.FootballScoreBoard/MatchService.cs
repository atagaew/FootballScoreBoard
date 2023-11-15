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

        public void Update(Match matchInfo)
        {
            _matchRepository.Update(matchInfo);
        }
    }
}