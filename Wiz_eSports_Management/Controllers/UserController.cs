using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using Wiz_eSports_Management.Common;

namespace Wiz_eSports_Management.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public UserController(IConfiguration configuration, ILogger<UserController> logger)
        {
            _configuration = configuration;
            _logger = logger;

            _userService = new UserService(configuration);
        }

        public IActionResult Index()
        {
            try
            {
                var users = _userService.GetUsers();
                return View(users);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult ActivateUser(string id)
        {
            try
            {
                string email = Crypto.Decrypt(id);
                var user = _userService.GetUserByEmail(email);
                if(user != null)
                {
                    user.IsVerified = true;
                    _userService.UpdateUser(user);
                    return View();
                }
                else
                {
                    return RedirectToAction("Unautherized", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult UnlockUser(string id)
        {
            try
            {
                string email = Crypto.Decrypt(id);
                var user = _userService.GetUserByEmail(email);
                if (user != null)
                {
                    user.IsLocked = false;
                    _userService.UpdateUser(user);
                    return View();
                }
                else
                {
                    return RedirectToAction("Unautherized", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public JsonResult GetUser()
        {
            try
            {
                var users = _userService.GetUsers();
                return Json(users);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult AddUser(User user)
        {
            try
            {
                int userId = 0;
                userId = _userService.SaveUser(user);

                if (userId > 0)
                {
                    return Json(new { status = 200, message = "success" });
                }
                else
                {
                    return Json(new { status = 201, message = "failed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult UpdateUser(User user)
        {
            try
            {
                bool isUpdated = false;
                isUpdated = _userService.UpdateUser(user);

                if (isUpdated)
                {
                    return Json(new { status = 200, message = "success" });
                }
                else
                {
                    return Json(new { status = 201, message = "failed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult DeleteUser(int userId)
        {
            try
            {
                bool isDeleted = false;
                isDeleted = _userService.DeleteUser(userId);

                if (isDeleted)
                {
                    return Json(new { status = 200, message = "success" });
                }
                else
                {
                    return Json(new { status = 201, message = "failed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult LockUnlockUser(int userId, bool isLocked)
        {
            try
            {
                bool isUpdated = false;
                isUpdated = _userService.LockUnlockUser(userId, isLocked);

                if (isUpdated)
                {
                    return Json(new { status = 200, message = "success" });
                }
                else
                {
                    return Json(new { status = 201, message = "failed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }
    }
}