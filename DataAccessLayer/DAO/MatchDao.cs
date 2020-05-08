using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class MatchDao: BaseDao
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
    }
}
