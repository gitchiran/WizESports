using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Wiz_eSports_Management.Common;
using Wiz_eSports_Management.Models;
using System.Linq;
using Wiz_eSports_Management.Models.Configurations;
using System.Globalization;

namespace Wiz_eSports_Management.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ISession _session;
        private readonly ConfigurationSettings _configSettings;
        private readonly SmptConfiguration _smptConfiguration;

        private readonly AdminSettingsService _adminSettingsService;

        public ContactController(IConfiguration configuration, ILogger<ContactController> logger, IWebHostEnvironment hostEnvironment, IOptions<ConfigurationSettings> configSettings, IOptions<SmptConfiguration> smtpConfig)
        {
            _logger = logger;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _configSettings = configSettings.Value;
            _smptConfiguration = smtpConfig.Value;
            _adminSettingsService = new AdminSettingsService(configuration);
        }

        public JsonResult ContactForm(IList<IFormFile> contactPageFile, Contact ContactDetails)
        {
            bool emailSent = false;
            try
            {
                string FPath = "";
                string filePath = _hostEnvironment.WebRootPath + $@"/UserContent/ContactForm/" + ContactDetails.Email + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_MM_ss");
                string Attachments = WizFileHandling.UploadAttachments(contactPageFile, filePath, true);

                if (contactPageFile.Count>0)
                {
                    FPath = filePath + "/" + Attachments;
                }               

                string body = "<p>Dear Admin, " +
                    "<br> There is an enquiry as below:" +

                    "<br><br> Name: " + ContactDetails.Name +

                    "<br><br> Email: " + ContactDetails.Email +

                     "<br><br> Message: " + ContactDetails.Message +

                    "<br><br> Regards,<br> Trecco Team.";


                ////emailSent = Email.SendEmail(ContactDetails.Email, _smptConfiguration.User, "Trecco - Contact Enquiry", body, _smptConfiguration.Pass, _smptConfiguration.Server, _smptConfiguration.Port, FPath);
                emailSent = Email.SendEmail(_configSettings.SendToEmail, _smptConfiguration.User, "Trecco - Contact Enquiry", body, _smptConfiguration.Pass, _smptConfiguration.Server, _smptConfiguration.Port, FPath);

                return Json(new { status = 200, emailSent = emailSent, emailAddress = ContactDetails.Email });

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, userId = 0, emailSent = false, emailAddress = string.Empty });
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}