using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class TournamentDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public TournamentDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Tournament> GetTournaments()
        {
            try
            {
                return db.Tournament.Where(e => e.IsActive == true).ToList();
            }
            catch (Exception)
            {
                return new List<Tournament>();
            }
        }

        public IEnumerable<Tournament> GetUpcomingTournaments()
        {
            try
            {
                return db.Tournament.Where(e => e.IsActive == true && e.SceduledDate > DateTime.Now).ToList();
            }
            catch (Exception)
            {
                return new List<Tournament>();
            }
        }

        public Tournament GetTournament(int tournamentId)
        {
            try
            {
                return db.Tournament.FirstOrDefault(t => t.Id == tournamentId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveTournament(Tournament tournament)
        {
            try
            {
                int isSaved = 0;
                db.Tournament.Add(tournament);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTournament(Tournament tournament)
        {
            try
            {
                int isUpdated = 0;
                db.Tournament.Update(tournament);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTournament(int tournamentId)
        {
            try
            {
                int isDeleted = 0;
                var tournament = db.Tournament.FirstOrDefault(t => t.Id == tournamentId);
                if (tournament != null)
                {
                    tournament.IsActive = false;
                    db.Tournament.Update(tournament);
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
