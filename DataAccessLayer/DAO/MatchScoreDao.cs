using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class MatchScoreDao:BaseDao
    {
        private readonly IConfiguration _configuration;

        public MatchScoreDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<MatchScore> GetMatchScores()
        {
            try
            {
                return db.MatchScore.Include(e => e.Team).ToList();
            }
            catch (Exception)
            {
                return new List<MatchScore>();
            }
        }

        public IEnumerable<MatchScore> GetMatchScores(int matchId)
        {
            try
            {
                return db.MatchScore.Where(t => t.MatchId == matchId).Include(t => t.Team).ToList();
            }
            catch (Exception)
            {
                return new List<MatchScore>();
            }
        }

        public IEnumerable<MatchScore> GetMatchScoresByTeam(int teamId)
        {
            try
            {
                return db.MatchScore.Where(t => t.TeamId == teamId).Include(t => t.Team).ToList();
            }
            catch (Exception)
            {
                return new List<MatchScore>();
            }
        }

        public MatchScore GetMatchScore(int matchScoreId)
        {
            try
            {
                return db.MatchScore.FirstOrDefault(t => t.Id == matchScoreId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveMatchScore(MatchScore matchScore)
        {
            try
            {
                int isSaved = 0;
                db.MatchScore.Add(matchScore);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMatchScore(MatchScore matchScore)
        {
            try
            {
                int isUpdated = 0;
                db.MatchScore.Update(matchScore);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMatchScore(int matchScoreId)
        {
            try
            {
                int isDeleted = 0;
                var matchScore = db.MatchScore.FirstOrDefault(t => t.Id == matchScoreId);
                if (matchScore != null)
                {
                    db.MatchScore.Remove(matchScore);
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
