using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class TeamTournamentGroupService
    {
        private readonly IConfiguration _configuration;
        private readonly TeamTournamentGroupDao _teamTournamentGroupDao;

        public TeamTournamentGroupService(IConfiguration configuration)
        {
            _configuration = configuration;
            _teamTournamentGroupDao = new TeamTournamentGroupDao(configuration);
        }
        public IEnumerable<TeamTournamentGroup> GetTeamTournamentGroupes()
        {
            return _teamTournamentGroupDao.GetTeamTournamentGroupes();
        }

        public IEnumerable<TeamTournamentGroup> GetTeamTournamentGroupes(int groupId)
        {
            return _teamTournamentGroupDao.GetTeamTournamentGroupes(groupId);
        }

        public TeamTournamentGroup GetTeamTournamentGroup(int teamTournamentGroupId)
        {
            return _teamTournamentGroupDao.GetTeamTournamentGroup(teamTournamentGroupId);
        }

        public bool SaveTeamTournamentGroup(TeamTournamentGroup teamTournamentGroup)
        {
            if(teamTournamentGroup != null)
            {
                return _teamTournamentGroupDao.SaveTeamTournamentGroup(teamTournamentGroup);
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTeamTournamentGroup(TeamTournamentGroup teamTournamentGroup)
        {
            if (teamTournamentGroup != null)
            {
                return _teamTournamentGroupDao.UpdateTeamTournamentGroup(teamTournamentGroup);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTeamTournamentGroup(int teamTournamentGroupId)
        {
            if (teamTournamentGroupId > 0)
            {
                return _teamTournamentGroupDao.DeleteTeamTournamentGroup(teamTournamentGroupId);
            }
            else
            {
                return false;
            }
        }
    }
}
