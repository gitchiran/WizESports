using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class UserDao : BaseDao
    {
        private readonly IConfiguration _configuration;

        public UserDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return db.User.Where(e => e.IsActive == true).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public User GetUser(int userId)
        {
            try
            {
                return db.User.FirstOrDefault(u => u.Id == userId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUser(string username)
        {
            try
            {
                return db.User.FirstOrDefault(u => u.Username.Equals(username));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUser(string username, string password)
        {
            try
            {
                return db.User.FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUserByUserName(string username)
        {
            try
            {
                return db.User.FirstOrDefault(u => u.Username.Equals(username));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return db.User.FirstOrDefault(u => u.Email.Equals(email));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool LockUser(string username)
        {
            try
            {
                int isLocked = 0;
                var user = db.User.FirstOrDefault(u => u.Username.Equals(username));
                if (user != null)
                {
                    user.IsLocked = true;
                    db.User.Update(user);
                    isLocked = db.SaveChanges();
                }

                return isLocked > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int SaveUser(User user)
        {
            try
            {
                int userId = 0;
                if (user != null)
                {
                    db.User.Add(user);
                    db.SaveChanges();
                    userId = user.Id;
                }

                return userId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ValidateUser(User user)
        {
            try
            {
                int userCount = 0;
                if (user != null)
                {
                    userCount = db.User.Where(x => x.Email == user.Email || x.Username == user.Username).Count();
                }

                return userCount;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                int isUpdated = 0;
                if (user != null)
                {
                    db.User.Update(user);
                    isUpdated = db.SaveChanges();
                }

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                int isDeleted = 0;
                var user = db.User.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    user.IsActive = false;
                    db.User.Update(user);

                    if (user.RoleId == 2)
                    {
                        var team = db.Team.FirstOrDefault(e => e.UserId == userId);
                        if (team != null)
                        {
                            team.IsActive = false;
                            db.Team.Update(team);
                        }
                    }

                    isDeleted = db.SaveChanges();
                }

                return isDeleted > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ResetPassword(PasswordReset reset)
        {
            try
            {
                int isPasswordReset = 0;
                User user = db.User.FirstOrDefault(u => u.Username.Equals(reset.Username));
                if (user != null)
                {
                    user.Password = reset.Password;
                    user.Token = string.Empty;
                    db.User.Update(user);
                    isPasswordReset = db.SaveChanges();
                }

                return isPasswordReset > 0;
            }

            catch (Exception)
            {
                return false;
            }
        }

        public bool LockUnlockUser(int userId, bool isLocked)
        {
            try
            {
                int isDeleted = 0;
                var user = db.User.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    user.IsLocked = isLocked;
                    db.User.Update(user);
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
