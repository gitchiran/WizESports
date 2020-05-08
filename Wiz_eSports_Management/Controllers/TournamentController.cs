using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Wiz_eSports_Management.Common;
using Wiz_eSports_Management.Models;

namespace Wiz_eSports_Management.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ILogger<TournamentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ISession _session;

        private readonly TournamentService _tournamentService;
        private readonly TournamentTeamService _tournamentTeamService;

        public TournamentController(IConfiguration configuration, ILogger<TournamentController> logger, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _session = httpContextAccessor.HttpContext.Session;

            _tournamentService = new TournamentService(configuration);
            _tournamentTeamService = new TournamentTeamService(configuration);
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<Tournament> tournaments;
                int roleId = SessionUser.GetUserRoleId(_session);

                if (roleId == 1)
                {
                    tournaments  = _tournamentService.GetTournaments();
                }
                else
                {
                    tournaments = _tournamentService.GetUpcomingTournaments();
                }

                return View(tournaments);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Details(int tournamentId)
        {
            try
            {
                var tournament = _tournamentService.GetTournament(tournamentId);
                ViewBag.UserRole = SessionUser.GetUserRoleId(_session);
                return View(tournament);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult GetTournaments()
        {
            try
            {
                IEnumerable<Tournament> tournaments;
                int roleId = SessionUser.GetUserRoleId(_session);

                if (roleId == 1)
                {
                    tournaments = _tournamentService.GetTournaments();
                }
                else
                {
                    tournaments = _tournamentService.GetUpcomingTournaments();
                }

                return PartialView("_TournamentList", tournaments); 
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return NoContent();
            }
        }

        public IActionResult GetUpcomingTournaments()
        {
            try
            {
                var tournaments = _tournamentService.GetUpcomingTournaments();
                return PartialView("_UpcomingTournaments", tournaments);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return NoContent();
            }
        }

        public IActionResult AddTournamentPopup()
        {
            try
            {
                Tournament tournament = new Tournament();
                return PartialView("_AddTournamentPopup", tournament);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return NoContent();
            }
        }

        public JsonResult AddTournament(Tournament tournament)
        {
            try
            {
                bool isSaved = false;

                tournament.IsActive = true;
                tournament.CreatedDate = DateTime.UtcNow;
                tournament.CreatedBy = SessionUser.GetUserId(_session);

                isSaved = _tournamentService.SaveTournament(tournament);

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

        public JsonResult UpdateTournament(Tournament tournament)
        {
            try
            {
                bool isUpdated = false;
                isUpdated = _tournamentService.UpdateTournament(tournament);

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

        public JsonResult DeleteTournament(int tournamentId)
        {
            try
            {
                bool isDeleted = false;
                isDeleted = _tournamentService.DeleteTournament(tournamentId);

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

        public JsonResult Enroll(IList<IFormFile> paymentImageFile, TournamentEnrollmentVM tournamentEnrollmentVM)
        {
            try
            {
                bool isEnrolled = false;
                string filePath = _hostEnvironment.WebRootPath + $@"/UserContent/Tournaments/{tournamentEnrollmentVM.TournamentId}/{tournamentEnrollmentVM.TeamId}";
                string Attachments = WizFileHandling.UploadAttachments(paymentImageFile, filePath, true);

                var tournamentTeam = new TournamentTeam();
                tournamentTeam.TournamentId = tournamentEnrollmentVM.TournamentId;
                tournamentTeam.TeamId = tournamentEnrollmentVM.TournamentId;
                tournamentTeam.PaymentType = (int)tournamentEnrollmentVM.PaymentType;
                tournamentTeam.IsPaymentMade = tournamentEnrollmentVM.IsPaymentMade;
                tournamentTeam.EnrollmentDate = DateTime.UtcNow;
                tournamentTeam.PaymentProof = Attachments;

                isEnrolled = _tournamentTeamService.SaveTournamentTeam(tournamentTeam);

                if (isEnrolled)
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