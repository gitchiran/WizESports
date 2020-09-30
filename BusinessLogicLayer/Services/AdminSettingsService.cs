using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;

namespace BusinessLogicLayer.Services
{
    public class AdminSettingsService
    {
        private readonly IConfiguration _configuration;
        private readonly AdminSettingsDao _adminSettingsDao;

        public AdminSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _adminSettingsDao = new AdminSettingsDao(configuration);
        }

        public AdminSettings GetAdminSettings()
        {
            return _adminSettingsDao.GetAdminSettings();
        }

        public int SaveAdminSettings(AdminSettings adminSettings)
        {
            if(adminSettings != null)
            {
                return _adminSettingsDao.SaveAdminSettings(adminSettings);
            }
            else
            {
                return 0;
            }
            
        }

        public bool UpdateAdminSettings(AdminSettings adminSettings)
        {
            if (adminSettings != null)
            {
                return _adminSettingsDao.UpdateAdminSettings(adminSettings);
            }
            else
            {
                return false;
            }
        }
    }
}
