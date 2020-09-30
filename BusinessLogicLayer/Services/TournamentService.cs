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
        public IEnumerable<Tournament> GetUpcomingTournamentbySchedule()
        {
            return _tournamentDao.GetUpcomingTournamentbySchedule();
        }

        public IEnumerable<TournamentTeam> GetTeamTournaments(int TeamId)
        {
            return _tournamentDao.GetTeamTournaments(TeamId);
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

        public bool UpdateTournamentEndDate(int tournamentId,string EndDate)
        {
            if (tournamentId > 0)
            {
                return _tournamentDao.UpdateTournamentEndDate(tournamentId, EndDate);
            }
            else
            {
                return false;
            }
        }

        public bool SaveTournamentDraw(TournamentDraw tournamentDraw)
        {
            if (tournamentDraw != null)
            {
                return _tournamentDao.SaveTournamentDraw(tournamentDraw);
            }
            else
            {
                return false;
            }
        }
        public bool DeleteTournamentDraw(int DrawId)
        {
            if (DrawId > 0)
            {
                return _tournamentDao.DeleteTournamentDraw(DrawId);
            }
            else
            {
                return false;
            }
        }

        public int GetEnrollmentStatus(int tournamentId, int TeamId)
        {
            return _tournamentDao.GetEnrollmentStatus(tournamentId, TeamId);
        }
    }
}
