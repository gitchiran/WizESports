using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class MatchService
    {
        private readonly IConfiguration _configuration;
        private readonly MatchDao _matchDao;

        public MatchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _matchDao = new MatchDao(configuration);
        }

        public IEnumerable<Match> GetMatches()
        {
            return _matchDao.GetMatches();
        }

        public IEnumerable<Match> GetMatches(int tournamentId)
        {
            return _matchDao.GetMatches(tournamentId);
        }

        public IEnumerable<Match> GetMatches(DateTime matchDate)
        {
            return _matchDao.GetMatches(matchDate);
        }

        public Match GetMatch(int matchId)
        {
            return _matchDao.GetMatch(matchId);
        }

        public bool SaveMatch(Match match)
        {
            if (match != null)
            {
                return _matchDao.SaveMatch(match);
            }
            else
            {
                return false;
            }

        }

        public bool UpdateMatch(Match match)
        {
            if (match != null)
            {
                return _matchDao.UpdateMatch(match);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteMatch(int matchId)
        {
            if (matchId > 0)
            {
                return _matchDao.DeleteMatch(matchId);
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<MatchScore> GetTournamentMatchScoreDetails(int tournamentId, int teamId, string ScoreType)
        {
            return _matchDao.GetTournamentMatchScoreDetails(tournamentId, teamId, ScoreType);
        }

        public IEnumerable<Tournament> GetRecentTournamentbySchedule()
        {
            return _matchDao.GetRecentTournamentbySchedule();
        }

        public IEnumerable<TournamentGroup> GetTournamentGroupName(int GroupId)
        {
            return _matchDao.GetTournamentGroupName(GroupId);
        }

        public IEnumerable<MatchScore> GetTournamentMatchScoreDetailsbyGroup(int tournamentId, int teamId)
        {
            return _matchDao.GetTournamentMatchScoreDetailsbyGroup(tournamentId, teamId);
        }

        public bool SaveMatchScore(MatchScore match)
        {
            if (match != null)
            {
                return _matchDao.SaveMatchScore(match);
            }
            else
            {
                return false;
            }

        }

        public bool UpdateMatchScore(MatchScore match)
        {
            if (match != null)
            {
                return _matchDao.UpdateMatchScore(match);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteMatchScore(int matchId)
        {
            if (matchId > 0)
            {
                return _matchDao.DeleteMatchScore(matchId);
            }
            else
            {
                return false;
            }
        }
    }
}
