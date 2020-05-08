using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class ClanService
    {
        private readonly IConfiguration _configuration;
        private readonly ClanDao _clanDao;

        public ClanService(IConfiguration configuration)
        {
            _configuration = configuration;
            _clanDao = new ClanDao(configuration);
        }

        public Clan GetClan(int clanId)
        {
            return _clanDao.GetClan(clanId);
        }

        public IEnumerable<Clan> GetClans()
        {
            return _clanDao.GetClans();
        }

        public bool SaveClan(Clan clan)
        {
            if(clan != null)
            {
                return _clanDao.SaveClan(clan);
            }
            else
            {
                return false;
            }
            
        }

        public bool UpdateClan(Clan clan)
        {
            if (clan != null)
            {
                return _clanDao.UpdateClan(clan);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteClan(int clanId)
        {
            if (clanId > 0)
            {
                return _clanDao.DeleteClan(clanId);
            }
            else
            {
                return false;
            }
        }
    }
}
