using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class MatchDao : BaseDao
    {
        private readonly IConfiguration _configuration;

        public MatchDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Match> GetMatches()
        {
            try
            {
                return db.Match.Include(e => e.Tournament).ToList();
            }
            catch (Exception)
            {
                return new List<Match>();
            }
        }

        public IEnumerable<Match> GetMatches(int tournamentId)
        {
            try
            {
                return db.Match.Where(t => t.TournamentId == tournamentId).Include(t => t.Tournament).ToList();
            }
            catch (Exception)
            {
                return new List<Match>();
            }
        }

        public IEnumerable<Match> GetMatches(DateTime matchDate)
        {
            try
            {
                return db.Match.Where(t => t.MatchDate == matchDate).Include(t => t.Tournament).ToList();
            }
            catch (Exception)
            {
                return new List<Match>();
            }
        }

        public Match GetMatch(int matchId)
        {
            try
            {
                return db.Match.FirstOrDefault(t => t.Id == matchId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<MatchScore> GetTournamentMatchScoreDetails(int tournamentId, int teamId, string ScoreType)
        {
            try
            {
                if (ScoreType != "")
                {
                    return db.MatchScore.Where(t => t.TournamentId == tournamentId && t.TeamId == teamId && t.ScoreType == ScoreType && t.IsActive == true).Include(t => t.Team).ToList();
                }
                else if (teamId > 0)
                {
                    return db.MatchScore.Where(t => t.TournamentId == tournamentId && t.TeamId == teamId && t.IsActive == true).Include(t => t.Team).ToList();
                }
                else if (tournamentId > 0)
                {
                    return db.MatchScore.Where(t => t.TournamentId == tournamentId && t.IsActive == true).Include(t => t.Team).ToList();
                }
                else
                {
                    return db.MatchScore.Where(t => t.IsActive == true).Include(t => t.Team).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Tournament> GetRecentTournamentbySchedule()
        {
            try
            {
                return db.Tournament.Where(t => t.IsActive == true).OrderByDescending(t=>t.EndDate).Take(1).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }        

        public IEnumerable<TournamentGroup> GetTournamentGroupName(int GroupId)
        {
            try
            {
                return db.TournamentGroup.Where(t => t.Id == GroupId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IEnumerable<MatchScore> GetTournamentMatchScoreDetailsbyGroup(int tournamentId, int teamId)
        {
            try
            {
                if (teamId > 0)
                {
                    return db.MatchScore.Where(t => t.TournamentId == tournamentId && t.TeamId == teamId && t.IsActive == true).Include(t => t.Team).ToList();
                }
                else if (tournamentId > 0)
                {
                    return db.MatchScore.Where(t => t.TournamentId == tournamentId && t.IsActive == true).Include(t => t.Team).ToList();
                }
                else
                {
                    return db.MatchScore.Where(t => t.IsActive == true).Include(t => t.Team).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool SaveMatch(Match match)
        {
            try
            {
                int isSaved = 0;
                db.Match.Add(match);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMatch(Match match)
        {
            try
            {
                int isUpdated = 0;
                db.Match.Update(match);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMatch(int matchId)
        {
            try
            {
                int isDeleted = 0;
                var match = db.Match.FirstOrDefault(t => t.Id == matchId);
                if (match != null)
                {
                    match.IsActive = false;
                    db.Match.Update(match);
                    isDeleted = db.SaveChanges();
                }

                return isDeleted > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SaveMatchScore(MatchScore match)
        {
            try
            {
                int isSaved = 0;
                db.MatchScore.Add(match);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMatchScore(MatchScore match)
        {
            try
            {
                int isUpdated = 0;
                var matchscore = db.MatchScore.FirstOrDefault(t => t.Id == match.Id);
                matchscore.Score = match.Score;
                db.MatchScore.Update(matchscore);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMatchScore(int matchId)
        {
            try
            {
                int isDeleted = 0;
                var match = db.MatchScore.FirstOrDefault(t => t.Id == matchId);
                if (match != null)
                {
                    match.IsActive = false;
                    db.MatchScore.Update(match);
                    isDeleted = db.SaveChanges();
                }

                return isDeleted > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
