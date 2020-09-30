using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class AdminSettingsDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public AdminSettingsDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public AdminSettings GetAdminSettings()
        {
            try
            {
                return db.AdminSettings.FirstOrDefault();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public int SaveAdminSettings(AdminSettings adminSettings)
        {
            try
            {
                int contactId = 0;
                if(adminSettings != null)
                {
                    db.AdminSettings.Add(adminSettings);
                    db.SaveChanges();
                    contactId = adminSettings.Id;
                }
                

                return contactId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool UpdateAdminSettings(AdminSettings adminSettings)
        {
            try
            {
                int isUpdated = 0;
                var AdminData = db.AdminSettings.FirstOrDefault();
                AdminData.YoutubeUrl = adminSettings.YoutubeUrl;
                db.AdminSettings.Update(AdminData);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
