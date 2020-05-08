using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class AuthenticateDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public AuthenticateDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public bool Authenticate(string userName, string password)
        {
            try
            {
                bool isAuthenticated = false;
                var user = db.User.FirstOrDefault(u => u.Username.Equals(userName) && u.Password.Equals(password) && u.IsActive == true && u.IsVerified == true && u.IsLocked == false);

                if (user!= null)
                {
                    isAuthenticated = true;
                }

                return isAuthenticated;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Register(User user)
        {
            try
            {
                int i = 0;
                db.User.Add(user);
                i = db.SaveChanges();
                return i > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
