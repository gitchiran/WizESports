using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class TournamentTeamService
    {
        private readonly IConfiguration _configuration;
        private readonly TournamentTeamDao _tournamentTeamDao;

        public TournamentTeamService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tournamentTeamDao = new TournamentTeamDao(configuration);
        }

        public IEnumerable<TournamentTeam> GetTournamentTeams()
        {
            return _tournamentTeamDao.GetTournamentTeams();
        }

        public IEnumerable<TournamentTeam> GetTournamentTeams(int tournamnetId)
        {
            return _tournamentTeamDao.GetTournamentTeams(tournamnetId);
        }

        public IEnumerable<TournamentTeam> GetTournamentsByTeam(int teamId)
        {
            return _tournamentTeamDao.GetTournamentsByTeam(teamId);
        }

        public TournamentTeam GetTournamentTeam(int tournamentTeamId)
        {
            return _tournamentTeamDao.GetTournamentTeam(tournamentTeamId);
        }

        public bool SaveTournamentTeam(TournamentTeam tournamentTeam)
        {
            if(tournamentTeam != null)
            {
                return _tournamentTeamDao.SaveTournamentTeam(tournamentTeam);
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTournamentTeam(TournamentTeam tournamentTeam)
        {
            if (tournamentTeam != null)
            {
                return _tournamentTeamDao.UpdateTournamentTeam(tournamentTeam);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTournamentTeam(int tournamentTeamId)
        {
            if (tournamentTeamId > 0)
            {
                return _tournamentTeamDao.DeleteTournamentTeam(tournamentTeamId);
            }
            else
            {
                return false;
            }
        }
    }
}
