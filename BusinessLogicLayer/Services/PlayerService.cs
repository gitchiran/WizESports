using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class PlayerService
    {
        private readonly IConfiguration _configuration;
        private readonly PlayerDao _playerDao;

        public PlayerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _playerDao = new PlayerDao(configuration);
        }

        public IEnumerable<Player> GetPlayers(int teamId)
        {
            return _playerDao.GetPlayers(teamId);
        }

        public bool SavePlayer(Player player)
        {
            if(player != null)
            {
                return _playerDao.SavePlayer(player);
            }
            else
            {
                return false;
            }
            
        }

        public bool UpdatePlayer(Player player)
        {
            if (player != null)
            {
                return _playerDao.UpdatePlayer(player);
            }
            else
            {
                return false;
            }
        }

        public bool DeletePlayer(int playerId)
        {
            if(playerId > 0)
            {
                return _playerDao.DeletePlayer(playerId);
            }
            else
            {
                return false;
            }
        }
    }
}
