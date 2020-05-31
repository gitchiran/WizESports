using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Wiz_eSports_Management.Common;
using Wiz_eSports_Management.Enums;
using Wiz_eSports_Management.Models;
using Wiz_eSports_Management.Models.Configurations;

namespace Wiz_eSports_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISession _session;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ConfigurationSettings _configSettings;
        private readonly SmptConfiguration _smptConfiguration;

        private readonly AuthenticateService _authenticateService;
        private readonly UserService _userService;
        private readonly ContactPersonService _contactPersonService;
        private readonly TeamService _teamService;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment, IOptions<ConfigurationSettings> configSettings, IOptions<SmptConfiguration> smtpConfig)
        {
            _configuration = configuration;
            _logger = logger;
            _session = httpContextAccessor.HttpContext.Session;
            _hostEnvironment = hostEnvironment;
            _configSettings = configSettings.Value;
            _smptConfiguration = smtpConfig.Value;

            _authenticateService = new AuthenticateService(configuration);
            _userService = new UserService(configuration);
            _contactPersonService = new ContactPersonService(configuration);
            _teamService = new TeamService(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Unautherized()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }

        public JsonResult UserRegistration(IList<IFormFile> teamLogoFile, UserRegistrationVM userRegistrationVM)
        {
            try
            {
                string token = Guid.NewGuid().ToString();

                User user = new User();
                user.Token = token;
                user.Username = userRegistrationVM.Username;
                user.Password = Crypto.ComputeHash(userRegistrationVM.Password);
                user.Email = userRegistrationVM.Email;
                user.RoleId = 2;
                user.IsActive = true;
                user.IsLocked = false;
                user.IsVerified = false;
                user.RegisteredDate = DateTime.UtcNow;

                int userId = _userService.SaveUser(user);

                ContactPerson contactPerson = new ContactPerson();
                contactPerson.Cpname = userRegistrationVM.ContactName;
                contactPerson.Email = string.IsNullOrEmpty(userRegistrationVM.ContactEmail) ? userRegistrationVM.Email : userRegistrationVM.ContactEmail;
                contactPerson.Nic = userRegistrationVM.ContactNic;
                contactPerson.PhoneNumber = userRegistrationVM.ContactPhone;

                int contactId = _contactPersonService.SaveContactPerson(contactPerson);

                string filePath = _hostEnvironment.WebRootPath + $@"/UserContent/Teams/{user.Id}";
                string Attachments = WizFileHandling.UploadAttachments(teamLogoFile, filePath, true);

                Team team = new Team();
                team.TeamName = userRegistrationVM.TeamName;
                team.TeamDescription = userRegistrationVM.TeamName;
                team.LogoPath = Attachments;
                team.IsActive = true;
                team.RegistrationDate = DateTime.UtcNow;
                team.UserId = userId;
                team.ContactPerson = contactId;

                _teamService.SaveTeam(team);

                bool emailSent = false;

                if (userId > 0 && !string.IsNullOrEmpty(user.Password))
                {
                    string body = "<p>Hi " + user.Username + ", " +
                        "<br> Please use the following link to verify your email and activate your wiz account" +

                        "<br><br><a href='" + _configSettings.URLApplication.ToString() + "User/ActivateUser?id=" + Crypto.Encrypt(user.Email) + "'> Activate My Wix Account </a>" +

                        "<br><br> Thank you for using Wiz, Have an awesome day.";


                    emailSent = Email.SendEmail(user.Email, _smptConfiguration.User, "Wiz - Email Verfication", body, _smptConfiguration.Pass, _smptConfiguration.Server, _smptConfiguration.Port);
                }

                return Json(new { status = 200, userId = user.Id, emailSent = emailSent, emailAddress = user.Email });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, userId = 0, emailSent = false, emailAddress = string.Empty });
            }
        }

        public JsonResult ResetPassword(UserLoginVM userLoginVM)
        {
            try
            {

                if (!string.IsNullOrEmpty(userLoginVM.Password))
                {
                    PasswordReset reset = new PasswordReset()
                    {
                        Password = Crypto.ComputeHash(userLoginVM.Password),
                        Username = userLoginVM.Username
                    };

                    bool updated = _userService.ResetPassword(reset);

                    return Json(new { status = 200, updated = updated });
                }

                return Json(new { status = 201, updated = false });

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, updated = false });
            }
        }

        public async Task<ActionResult> UserLogin(UserLoginVM userVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string hashedPassword = Crypto.ComputeHash(userVM.Password);

                    bool isAuthenticated = _authenticateService.Authenticate(userVM.Username, hashedPassword);

                    if (isAuthenticated)
                    {
                        var user = _userService.GetUser(userVM.Username, hashedPassword);

                        if (user.IsLocked.HasValue && user.IsLocked.Value)
                        {
                            ModelState.AddModelError("", "Your account has been blocked! Check your email to unlock it.");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimProperty.UserName.ToString(), user.Username),
                                new Claim(ClaimProperty.UserId.ToString(), user.Id.ToString()),
                                new Claim(ClaimProperty.Email.ToString(), user.Email),
                                new Claim(ClaimProperty.Role.ToString(), user.RoleId.ToString())
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            LoggedUser loggedUser = new LoggedUser()
                            {
                                UserId = user.Id,
                                UserName = user.Username,
                                RoleId = user.RoleId.Value,
                                Email = user.Email
                            };

                            _session.SetSession("LoggedUser", loggedUser);

                            _session.SetInt32("UnsuccessfullyAttemptCount", 0);

                            _userService.UpdateLastLogin(user);

                            TempData["UserRole"] = user.RoleId;

                            if (user.RoleId == 1)
                            {
                                return RedirectToAction("Index", "Tournament");
                            }
                            else
                            {
                                return RedirectToAction("Home", "Team");
                            }
                        }
                    }
                    else
                    {
                        bool isLocked = LockUserByUnsuccesfullyAttempts(userVM.Username);

                        if (isLocked)
                        {
                            ModelState.AddModelError("", "Your account has been locked!.Please check your email to unlock the account.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Authentication Failed");
                        }
                    }
                }

                return RedirectToAction("Login", "Home", userVM);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                ViewBag.Error = ex.InnerException + " " + ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        private bool LockUserByUnsuccesfullyAttempts(string UserName)
        {
            try
            {
                User user = _userService.GetUser(UserName);

                if (user != null)
                {
                    int count = 0;

                    if (HttpContext.Session.Get("UnsuccessfullyAttemptCount") != null)
                    {
                        count = int.Parse(HttpContext.Session.GetInt32("UnsuccessfullyAttemptCount").ToString());
                    }

                    count += 1;

                    HttpContext.Session.SetInt32("UnsuccessfullyAttemptCount", count);

                    int maxUnsuccessfullyAttemptsGranted = int.Parse(_configSettings.UnsuccessfullyAttempts.ToString());

                    if (count >= maxUnsuccessfullyAttemptsGranted)
                    {
                        bool isLocked = _userService.LockUser(user.Username);
                        if(isLocked)
                        {
                            string subject = "Your Wiz Account is locked";
                            string encriptedEmail = Crypto.Encrypt(user.Email);
                            string siteUrl = _configSettings.URLApplication.ToString();
                            string body = "Please click on the link to unlock your account. <br/>" +
                                           "<a href='" + siteUrl + "User/UnlockUser?id=" + encriptedEmail + "'>Unlock My Wiz Account</a>  ";
                            Email.SendEmail(user.Email, _smptConfiguration.User, subject, body, _smptConfiguration.Pass, _smptConfiguration.Server, _smptConfiguration.Port);
                            return true;
                        }
                    }

                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return false;
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }

            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
