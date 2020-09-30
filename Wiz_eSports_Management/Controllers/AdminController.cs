using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Wiz_eSports_Management.Common;
using Wiz_eSports_Management.Models;
using System.Linq;

namespace Wiz_eSports_Management.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ISession _session;

        private readonly AdminSettingsService _adminSettingsService;

        public AdminController(IConfiguration configuration, ILogger<AdminController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _adminSettingsService = new AdminSettingsService(configuration);
        }

        public JsonResult GetAdminData()
        {
            try
            {
                List<AdminSettings> adminSettingsData = new List<AdminSettings>();
                var adminSettings = _adminSettingsService.GetAdminSettings();

                adminSettingsData.Add(adminSettings);
                return Json(adminSettingsData);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult AddAdminData(string YouTubeUrl)
        {
            try
            {
                bool isSaved = false;
                AdminSettings adminSettingsData = new AdminSettings();
                adminSettingsData.YoutubeUrl = YouTubeUrl;

                isSaved = _adminSettingsService.UpdateAdminSettings(adminSettingsData);

                if (isSaved)
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

        public IActionResult Index()
        {
            return View();
        }
    }
}