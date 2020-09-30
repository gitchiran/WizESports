using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class TeamDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public TeamDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Team> GetTeams()
        {
            try
            {
                return db.Team.Where(e => e.IsActive == true).ToList();
            }
            catch (Exception)
            {
                return new List<Team>();
            }
        }

        public IEnumerable<Team> GetTeams(int clanId)
        {
            try
            {
                return db.Team.Where(t => t.ClanId == clanId).Include(t => t.Clan).ToList();
            }
            catch (Exception)
            {
                return new List<Team>();
            }
        }

        public Team GetTeam(int teamId)
        {
            try
            {
                return db.Team.Include(t => t.ContactPersonNavigation).FirstOrDefault(t => t.Id == teamId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Team GetTeamByUser(int userId)
        {
            try
            {
                //////return db.Team.FirstOrDefault(t => t.UserId == userId);
                return db.Team.Include(x=>x.ContactPersonNavigation).FirstOrDefault(t => t.UserId == userId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveTeam(Team team)
        {
            try
            {
                int isSaved = 0;
                db.Team.Add(team);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTeam(Team team)
        {
            try
            {
                int isUpdated = 0;
                db.Team.Update(team);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTeam(int teamId)
        {
            try
            {
                int isDeleted = 0;
                var team = db.Team.FirstOrDefault(t => t.Id == teamId);
                if (team != null)
                {
                    team.IsActive = false;
                    db.Team.Update(team);
                    isDeleted = db.SaveChanges();
                }

                return isDeleted > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePaymentVerificationStatus(int Id,char IsPaymentVerifiedByAdmin,string AdminComments)
        {
            try
            {
                int isUpdated = 0;
                var team = db.TournamentTeam.FirstOrDefault(t => t.Id == Id);
                if (team != null)
                {
                    team.IsPaymentVerifiedByAdmin = IsPaymentVerifiedByAdmin;
                    team.AdminComments = AdminComments;
                    db.TournamentTeam.Update(team);
                    isUpdated = db.SaveChanges();
                }

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
