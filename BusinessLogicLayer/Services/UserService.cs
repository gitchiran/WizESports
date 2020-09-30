using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserDao _userDao;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            _userDao = new UserDao(configuration);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userDao.GetUsers();
        }

        public User GetUser(int userId)
        {
            return _userDao.GetUser(userId);
        }

        public User GetUser(string username)
        {
            return _userDao.GetUser(username);
        }

        public User GetUser(string username, string password)
        {
            return _userDao.GetUser(username, password);
        }

        public User GetUserByUserName(string username)
        {
            return _userDao.GetUserByUserName(username);
        }

        public User GetUserByEmail(string email)
        {
            return _userDao.GetUserByEmail(email);
        }

        public bool LockUser(string username)
        {
            return _userDao.LockUser(username);
        }
        public int SaveUser(User user)
        {
            if(user != null)
            {
                return _userDao.SaveUser(user);
            }
            else
            {
                return 0;
            }
            
        }

        public int ValidateUser(User user)
        {
            if (user != null)
            {
                return _userDao.ValidateUser(user);
            }
            else
            {
                return 0;
            }

        }

        public bool UpdateUser(User user)
        {
            if (user != null)
            {
                return _userDao.UpdateUser(user);
            }
            else
            {
                return false;
            }
        }

        public bool UpdateLastLogin(User user)
        {
            if (user != null)
            {
                user.LastLoginDate = DateTime.UtcNow;
                return _userDao.UpdateUser(user);
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            if (userId > 0)
            {
                return _userDao.DeleteUser(userId);
            }
            else
            {
                return false;
            }
        }

        public bool ResetPassword(PasswordReset reset)
        {
            if(reset != null)
            {
                return _userDao.ResetPassword(reset);
            }
            else
            {
                return false;
            }
            
        }

        public bool LockUnlockUser(int userId, bool isLocked)
        {
            if (userId > 0)
            {
                return _userDao.LockUnlockUser(userId, isLocked);
            }
            else
            {
                return false;
            }
        }
    }
}
