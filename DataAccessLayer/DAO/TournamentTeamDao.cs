using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class TournamentTeamDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public TournamentTeamDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<TournamentTeam> GetTournamentTeams()
        {
            try
            {
                return db.TournamentTeam.Include(e => e.Tournament).Include(e => e.Team).ToList();
            }
            catch (Exception)
            {
                return new List<TournamentTeam>();
            }
        }

        public IEnumerable<TournamentTeam> GetTournamentTeams(int tournamnetId)
        {
            try
            {
                return db.TournamentTeam.Where(t => t.TournamentId == tournamnetId).Include(t => t.Team).ToList();
            }
            catch (Exception)
            {
                return new List<TournamentTeam>();
            }
        }

        public IEnumerable<TournamentTeam> GetTournamentsByTeam(int teamId)
        {
            try
            {
                return db.TournamentTeam.Where(t => t.TeamId == teamId).Include(t => t.Tournament).ToList();
            }
            catch (Exception)
            {
                return new List<TournamentTeam>();
            }
        }

        public TournamentTeam GetTournamentTeam(int tournamentTeamId)
        {
            try
            {
                return db.TournamentTeam.FirstOrDefault(t => t.Id == tournamentTeamId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveTournamentTeam(TournamentTeam tournamentTeam)
        {
            try
            {
                int isSaved = 0;
                db.TournamentTeam.Add(tournamentTeam);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTournamentTeam(TournamentTeam tournamentTeam)
        {
            try
            {
                int isUpdated = 0;
                db.TournamentTeam.Update(tournamentTeam);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTournamentTeam(int tournamentTeamId)
        {
            try
            {
                int isDeleted = 0;
                var tournamentTeam = db.TournamentTeam.FirstOrDefault(t => t.Id == tournamentTeamId);
                if (tournamentTeam != null)
                {
                    db.TournamentTeam.Remove(tournamentTeam);
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
