using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class TournamentService
    {
        private readonly IConfiguration _configuration;
        private readonly TournamentDao _tournamentDao;

        public TournamentService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tournamentDao = new TournamentDao(configuration);
        }

        public IEnumerable<Tournament> GetTournaments()
        {
            return _tournamentDao.GetTournaments();
        }

        public IEnumerable<Tournament> GetUpcomingTournaments() 
        {
            return _tournamentDao.GetUpcomingTournaments();
        }

        public Tournament GetTournament(int tournamentId)
        {
            return _tournamentDao.GetTournament(tournamentId);
        }

        public bool SaveTournament(Tournament tournament)
        {
            if(tournament != null)
            {
                return _tournamentDao.SaveTournament(tournament);
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTournament(Tournament tournament)
        {
            if (tournament != null)
            {
                return _tournamentDao.UpdateTournament(tournament);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTournament(int tournamentId)
        {
            if (tournamentId > 0)
            {
                return _tournamentDao.DeleteTournament(tournamentId);
            }
            else
            {
                return false;
            }
        }
    }
}
