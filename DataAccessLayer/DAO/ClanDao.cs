using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class ClanDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public ClanDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Clan> GetClans()
        {
            try
            {
                return db.Clan.ToList();
            }
            catch (Exception)
            {
                return new List<Clan>();
            }
        }

        public Clan GetClan(int clanId)
        {
            try
            {
                return db.Clan.FirstOrDefault(c => c.Id == clanId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveClan(Clan clan)
        {
            try
            {
                int isSaved = 0;
                db.Clan.Add(clan);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateClan(Clan clan)
        {
            try
            {
                int isUpdated = 0;
                db.Clan.Update(clan);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteClan(int clanId)
        {
            try
            {
                int isDeleted = 0;
                var clan = db.Clan.FirstOrDefault(c => c.Id == clanId);
                if(clan != null)
                {
                    clan.IsActive = false;
                    db.Clan.Update(clan);
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
