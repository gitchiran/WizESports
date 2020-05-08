using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class MatchScoreService
    {
        private readonly IConfiguration _configuration;
        private readonly MatchScoreDao _matchScoreDao;

        public MatchScoreService(IConfiguration configuration)
        {
            _configuration = configuration;
            _matchScoreDao = new MatchScoreDao(configuration);
        }

        public IEnumerable<MatchScore> GetMatchScores()
        {
            return _matchScoreDao.GetMatchScores();
        }

        public IEnumerable<MatchScore> GetMatchScores(int matchId)
        {
            return _matchScoreDao.GetMatchScores(matchId);
        }

        public IEnumerable<MatchScore> GetMatchScoresByTeam(int teamId)
        {
            return _matchScoreDao.GetMatchScoresByTeam(teamId);
        }

        public MatchScore GetMatchScore(int matchScoreId)
        {
            return _matchScoreDao.GetMatchScore(matchScoreId);
        }

        public bool SaveMatchScore(MatchScore matchScore)
        {
            if(matchScore != null)
            {
                return _matchScoreDao.SaveMatchScore(matchScore);
            }
            else
            {
                return false;
            }
            
        }

        public bool UpdateMatchScore(MatchScore matchScore)
        {
            if (matchScore != null)
            {
                return _matchScoreDao.UpdateMatchScore(matchScore);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteMatchScore(int matchScoreId)
        {
            if (matchScoreId > 0)
            {
                return _matchScoreDao.DeleteMatchScore(matchScoreId);
            }
            else
            {
                return false;
            }
        }
    }
}
