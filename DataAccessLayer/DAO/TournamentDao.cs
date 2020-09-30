using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class TournamentDao : BaseDao
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

        public IEnumerable<Tournament> GetUpcomingTournamentbySchedule()
        {
            try
            {
                return db.Tournament.Where(t => t.IsActive == true).Where(t => t.SceduledDate > DateTime.Now).OrderBy(t => t.SceduledDate).Take(1).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<TournamentTeam> GetTeamTournaments(int TeamId)
        {
            try
            {
                return db.TournamentTeam.Where(e => e.TeamId == TeamId && e.IsPaymentVerifiedByAdmin=='V').Include(x=>x.Tournament).ToList();
            }
            catch (Exception)
            {
                return new List<TournamentTeam>();
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
            catch (Exception ex)
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

        public bool UpdateTournamentEndDate(int tournamentId,string EndDate)
        {
            try
            {
                int isUpdated = 0;
                var tournament = db.Tournament.FirstOrDefault(t => t.Id == tournamentId);
                if (tournament != null)
                {
                    tournament.EndDate = Convert.ToDateTime(EndDate);
                    db.Tournament.Update(tournament);
                    isUpdated = db.SaveChanges();
                }

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TournamentDraw> GetTournamentDrawDetails(int tournamentId)
        {
            try
            {
                return db.TournamentDraw.Where(t => t.TournamentId == tournamentId && t.IsActive == true).ToList();
            }
            catch (Exception)
            {
                return new List<TournamentDraw>();
            }
        }

        public bool SaveTournamentDraw(TournamentDraw tournamentDraw)
        {
            try
            {
                int isSaved = 0;
                db.TournamentDraw.Add(tournamentDraw);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTournamentDraw(int tournamentDrawId)
        {
            try
            {
                int isDeleted = 0;
                var tournamentDraw = db.TournamentDraw.FirstOrDefault(t => t.Id == tournamentDrawId);
                if (tournamentDraw != null)
                {
                    tournamentDraw.IsActive = false;
                    db.TournamentDraw.Update(tournamentDraw);
                    isDeleted = db.SaveChanges();
                }

                return isDeleted > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetEnrollmentStatus(int tournamentId, int TeamId)
        {
            try
            {
                return db.TournamentTeam.Where(t => t.TournamentId == tournamentId && t.TeamId == TeamId).Count();
            }
            catch (Exception)
            {
                return 1;
            }
        }

        ////////public IEnumerable<Tournament> GetEndDateStatus(int tournamentId)
        ////////{
        ////////    try
        ////////    {
        ////////        return db.Tournament.Where(t => t.Id == tournamentId);
        ////////    }
        ////////    catch (Exception)
        ////////    {
        ////////        return new List<Tournament>();
        ////////    }
        ////////}
    }
}
