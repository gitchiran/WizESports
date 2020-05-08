using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class TeamService
    {
        private readonly IConfiguration _configuration;
        private readonly TeamDao _teamDao;

        public TeamService(IConfiguration configuration)
        {
            _configuration = configuration;
            _teamDao = new TeamDao(configuration);
        }

        public IEnumerable<Team> GetTeams()
        {
            return _teamDao.GetTeams();
        }

        public IEnumerable<Team> GetTeams(int clanId)
        {
            return _teamDao.GetTeams(clanId);
        }

        public Team GetTeam(int teamId)
        {
            return _teamDao.GetTeam(teamId);
        }

        public Team GetTeamByUser(int userId)
        {
            return _teamDao.GetTeamByUser(userId);
        }

        public bool SaveTeam(Team team)
        {
            if(team != null)
            {
                return _teamDao.SaveTeam(team);
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTeam(Team team)
        {
            if (team != null)
            {
                return _teamDao.UpdateTeam(team);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTeam(int teamId)
        {
            if (teamId > 0)
            {
                return _teamDao.DeleteTeam(teamId);
            }
            else
            {
                return false;
            }
        }
    }
}
