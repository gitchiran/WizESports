using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class TeamTournamentGroupDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public TeamTournamentGroupDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<TeamTournamentGroup> GetTeamTournamentGroupes()
        {
            try
            {
                return db.TeamTournamentGroup.Include(e => e.Team).Include(e => e.Group).ToList();
            }
            catch (Exception)
            {
                return new List<TeamTournamentGroup>();
            }
        }

        public IEnumerable<TeamTournamentGroup> GetTeamTournamentGroupes(int groupId)
        {
            try
            {
                return db.TeamTournamentGroup.Where(t => t.GroupId == groupId).Include(e => e.Team).Include(e => e.Group).ToList();
            }
            catch (Exception)
            {
                return new List<TeamTournamentGroup>();
            }
        }

        public TeamTournamentGroup GetTeamTournamentGroup(int teamTournamentGroupId)
        {
            try
            {
                return db.TeamTournamentGroup.FirstOrDefault(t => t.Id == teamTournamentGroupId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveTeamTournamentGroup(TeamTournamentGroup teamTournamentGroup)
        {
            try
            {
                int isSaved = 0;
                db.TeamTournamentGroup.Add(teamTournamentGroup);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTeamTournamentGroup(TeamTournamentGroup teamTournamentGroup)
        {
            try
            {
                int isUpdated = 0;
                db.TeamTournamentGroup.Update(teamTournamentGroup);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTeamTournamentGroup(int teamTournamentGroupId)
        {
            try
            {
                int isDeleted = 0;
                var teamTournamentGroup = db.TeamTournamentGroup.FirstOrDefault(t => t.Id == teamTournamentGroupId);
                if (teamTournamentGroup != null)
                {
                    db.TeamTournamentGroup.Remove(teamTournamentGroup);
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
