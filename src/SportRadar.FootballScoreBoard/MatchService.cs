using System.Text.RegularExpressions;

namespace SportRadar.FootballScoreBoard
{
    internal class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        /// <summary>
        /// Create new match in the library.
        /// </summary>
        public void Create(Match matchInfo)
        {
            _matchRepository.Add(matchInfo);
        }

        /// <summary>
        /// Update existing match in the library.
        /// </summary>
        public void Update(Match matchInfo)
        {
            CheckTheMatchIsInProgress(matchInfo);

            _matchRepository.Update(matchInfo);
        }

        /// <summary>
        /// Finish existing match in the library.
        /// </summary>
        public void Finish(Match matchInfo)
        {
            CheckTheMatchIsInProgress(matchInfo);

            if (!matchInfo.EndTime.HasValue)
                throw new ArgumentException("Finished match should have the EndTime", nameof(matchInfo.EndTime));

            _matchRepository.Update(matchInfo);
        }

        private Match CheckTheMatchIsInProgress(Match matchInfo)
        {
            var existingMatch = _matchRepository.GetMatch(matchInfo.Id);
            if (existingMatch == null || existingMatch.EndTime.HasValue)
                throw new InvalidOperationException($"Match with id {matchInfo.Id} not found");

            return existingMatch;
        }
    }
}