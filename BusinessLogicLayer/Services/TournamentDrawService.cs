using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class TournamentDrawService
    {
        private readonly IConfiguration _configuration;
        private readonly TournamentDrawDao _tournamentDrawDao;

        public TournamentDrawService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tournamentDrawDao = new TournamentDrawDao(configuration);
        }

        public IEnumerable<TournamentDraw> GetTournamentDraws()
        {
            return _tournamentDrawDao.GetTournamentDraws();
        }

        public TournamentDraw GetTournamentDraws(int tournamentId)
        {
            return _tournamentDrawDao.GetTournamentDraws(tournamentId);
        }

        public IEnumerable<TournamentDraw> GetTournamentDrawDetails(int tournamentId, int? groupId, int teamId)
        {
            return _tournamentDrawDao.GetTournamentDrawDetails(tournamentId, groupId, teamId);
        }

        public IEnumerable<TournamentDraw> GetTournamentTeamGroupDetails(int tournamentId, int teamId)
        {
            return _tournamentDrawDao.GetTournamentTeamGroupDetails(tournamentId, teamId);
        }

        public TournamentDraw GetTournamentDraw(int drawId)
        {
            return _tournamentDrawDao.GetTournamentDraw(drawId);
        }

        public bool SaveTournamentDraw(TournamentDraw tournamentDraw)
        {
            if(tournamentDraw != null)
            {
                return _tournamentDrawDao.SaveTournamentDraw(tournamentDraw);
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTournamentDraw(TournamentDraw tournamentDraw)
        {
            if (tournamentDraw != null)
            {
                return _tournamentDrawDao.UpdateTournamentDraw(tournamentDraw);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTournamentDraw(int drawId)
        {
            if (drawId > 0)
            {
                return _tournamentDrawDao.DeleteTournamentDraw(drawId);
            }
            else
            {
                return false;
            }
        }
    }
}
