using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class TournamentGroupService
    {
        private readonly IConfiguration _configuration;
        private readonly TournamentGroupDao _tournamentGroupDao;

        public TournamentGroupService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tournamentGroupDao = new TournamentGroupDao(configuration);
        }

        public IEnumerable<TournamentGroup> GetTournamentGroups()
        {
            return _tournamentGroupDao.GetTournamentGroups();
        }

        public IEnumerable<TournamentGroup> GetTournamentGroups(int tournamentId)
        {
            return _tournamentGroupDao.GetTournamentGroups(tournamentId);
        }

        public TournamentGroup GetTournamentGroup(int groupId)
        {
            return _tournamentGroupDao.GetTournamentGroup(groupId);
        }

        public bool SaveTournamentGroup(TournamentGroup tournamentGroup)
        {
            if(tournamentGroup != null)
            {
                return _tournamentGroupDao.SaveTournamentGroup(tournamentGroup);
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTournamentGroup(TournamentGroup tournamentGroup)
        {
            if (tournamentGroup != null)
            {
                return _tournamentGroupDao.UpdateTournamentGroup(tournamentGroup);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTournamentGroup(int groupId)
        {
            if (groupId > 0)
            {
                return _tournamentGroupDao.DeleteTournamentGroup(groupId);
            }
            else
            {
                return false;
            }
        }
    }
}
