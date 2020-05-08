using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class PlayerDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public PlayerDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Player> GetPlayers(int teamId)
        {
            try
            {
                return db.Player.Where(p => p.TeamId == teamId);
            }
            catch (Exception)
            {
                return new List<Player>();
            }
        }

        public bool SavePlayer(Player player)
        {
            try
            {
                int isSaved = 0;
                db.Player.Add(player);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePlayer(Player player)
        {
            try
            {
                int isUpdated = 0;
                db.Player.Update(player);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePlayer(int playerId)
        {
            try
            {
                int isDeleted = 0;
                var player = db.Player.FirstOrDefault(p => p.Id == playerId);
                if (player != null)
                {
                    db.Player.Remove(player);
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
