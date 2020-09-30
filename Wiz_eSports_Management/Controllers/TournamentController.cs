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
using System.Linq;


namespace Wiz_eSports_Management.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ILogger<TournamentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ISession _session;

        private readonly TeamService _teamService;
        private readonly TournamentService _tournamentService;
        private readonly TournamentTeamService _tournamentTeamService;

        public TournamentController(IConfiguration configuration, ILogger<TournamentController> logger, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _session = httpContextAccessor.HttpContext.Session;

            _teamService = new TeamService(configuration);
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
                    tournaments = _tournamentService.GetTournaments();
                }
                else
                {
                    tournaments = _tournamentService.GetUpcomingTournaments();
                }

                ViewBag.UserRole = roleId;
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

        public JsonResult GetUpcomingTournamentsJson()
        {
            try
            {
                var tournaments = _tournamentService.GetUpcomingTournaments().ToList();

                List<TournamentData> tournamentlst = new List<TournamentData>();

                for (var i = 0; i < tournaments.Count(); i++)
                {
                    if (tournaments[i].SceduledDate > DateTime.Now)
                    {
                        TournamentData tournamentData = new TournamentData();

                        ////tournamentData = tournaments[i].Tournament;

                        tournamentData.Id = tournaments[i].Id;
                        tournamentData.TournamentName = tournaments[i].TournamentName;
                        tournamentData.TournamentDescription = tournaments[i].TournamentDescription;
                        tournamentData.SceduledDate = Convert.ToDateTime(tournaments[i].SceduledDate).ToString("yyyy-MM-dd");
                        tournamentData.ContactPerson = tournaments[i].ContactPerson;
                        tournamentData.CreatedDate = Convert.ToDateTime(tournaments[i].CreatedDate).ToString("yyyy-MM-dd");
                        tournamentData.CreatedBy = tournaments[i].CreatedBy;
                        tournamentData.IsActive = tournaments[i].IsActive;
                        tournamentData.EntryFee = tournaments[i].EntryFee;

                        tournamentlst.Add(tournamentData);
                    }
                }

                return Json(tournamentlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult GetTeamPastRegisteredTournaments(int tournamentId)
        {
            try
            {
                int teamId = 0;
                var loggedUser = SessionUser.GetUser(_session);
                if (loggedUser != null)
                {
                    var team = _teamService.GetTeamByUser(loggedUser.UserId);
                    teamId = team != null ? team.Id : 0;
                }
                var tournaments = _tournamentService.GetTeamTournaments(teamId).ToList();

                List<TournamentData> tournamentlst = new List<TournamentData>();

                for (var i = 0; i < tournaments.Count(); i++)
                {
                    if (tournaments[i].Tournament.SceduledDate < DateTime.Now)
                    {
                        TournamentData tournamentData = new TournamentData();

                        tournamentData.Id = tournaments[i].Tournament.Id;
                        tournamentData.TournamentName = tournaments[i].Tournament.TournamentName;
                        tournamentData.TournamentDescription = tournaments[i].Tournament.TournamentDescription;
                        tournamentData.SceduledDate = Convert.ToDateTime(tournaments[i].Tournament.SceduledDate).ToString("yyyy-MM-dd");
                        tournamentData.ContactPerson = tournaments[i].Tournament.ContactPerson;
                        tournamentData.CreatedDate = Convert.ToDateTime(tournaments[i].Tournament.CreatedDate).ToString("yyyy-MM-dd");
                        tournamentData.CreatedBy = tournaments[i].Tournament.CreatedBy;
                        tournamentData.IsActive = tournaments[i].Tournament.IsActive;
                        tournamentData.EntryFee = tournaments[i].Tournament.EntryFee;

                        tournamentlst.Add(tournamentData);
                    }
                }

                return Json(tournamentlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult GetTeamUpcomingRegisteredTournaments()
        {
            try
            {
                int teamId = 0;
                var loggedUser = SessionUser.GetUser(_session);
                if (loggedUser != null)
                {
                    var team = _teamService.GetTeamByUser(loggedUser.UserId);
                    teamId = team != null ? team.Id : 0;
                }
                var tournaments = _tournamentService.GetTeamTournaments(teamId).ToList();

                List<TournamentData> tournamentlst = new List<TournamentData>();

                for (var i = 0; i < tournaments.Count(); i++)
                {
                    if (tournaments[i].Tournament.SceduledDate > DateTime.Now)
                    {
                        TournamentData tournamentData = new TournamentData();

                        ////tournamentData = tournaments[i].Tournament;

                        tournamentData.Id = tournaments[i].Tournament.Id;
                        tournamentData.TournamentName = tournaments[i].Tournament.TournamentName;
                        tournamentData.TournamentDescription = tournaments[i].Tournament.TournamentDescription;
                        tournamentData.SceduledDate = Convert.ToDateTime(tournaments[i].Tournament.SceduledDate).ToString("yyyy-MM-dd");
                        tournamentData.ContactPerson = tournaments[i].Tournament.ContactPerson;
                        tournamentData.CreatedDate = Convert.ToDateTime(tournaments[i].Tournament.CreatedDate).ToString("yyyy-MM-dd");
                        tournamentData.CreatedBy = tournaments[i].Tournament.CreatedBy;
                        tournamentData.IsActive = tournaments[i].Tournament.IsActive;
                        tournamentData.EntryFee = tournaments[i].Tournament.EntryFee;

                        tournamentlst.Add(tournamentData);
                    }
                }

                return Json(tournamentlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult GetTeamUpcomingUnregisteredTournaments()
        {
            try
            {
                int teamId = 0;
                var loggedUser = SessionUser.GetUser(_session);
                if (loggedUser != null)
                {
                    var team = _teamService.GetTeamByUser(loggedUser.UserId);
                    teamId = team != null ? team.Id : 0;
                }
                var tournaments = _tournamentService.GetUpcomingTournaments().ToList();

                List<TournamentData> tournamentlst = new List<TournamentData>();

                for (var i = 0; i < tournaments.Count(); i++)
                {
                    int IsEnrolled = 1;

                    IsEnrolled = _tournamentService.GetEnrollmentStatus(tournaments[i].Id, teamId);

                    if (IsEnrolled == 0)
                    {
                        TournamentData tournamentData = new TournamentData();

                        ////tournamentData = tournaments[i];

                        tournamentData.Id = tournaments[i].Id;
                        tournamentData.TournamentName = tournaments[i].TournamentName;
                        tournamentData.TournamentDescription = tournaments[i].TournamentDescription;
                        tournamentData.SceduledDate = Convert.ToDateTime(tournaments[i].SceduledDate).ToString("yyyy-MM-dd");
                        tournamentData.ContactPerson = tournaments[i].ContactPerson;
                        tournamentData.CreatedDate = Convert.ToDateTime(tournaments[i].CreatedDate).ToString("yyyy-MM-dd");
                        tournamentData.CreatedBy = tournaments[i].CreatedBy;
                        tournamentData.IsActive = tournaments[i].IsActive;
                        tournamentData.EntryFee = tournaments[i].EntryFee;

                        tournamentlst.Add(tournamentData);
                    }
                }

                return Json(tournamentlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
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

        public IActionResult EditTournamentPopup(int TournamentId)
        {
            try
            {
                Tournament tournament = _tournamentService.GetTournament(TournamentId);
                return PartialView("_EditTournamentPopup", tournament);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return NoContent();
            }
        }

        public IActionResult EnrollTournamentPopup()
        {
            try
            {
                TournamentTeam tournament = new TournamentTeam();
                return PartialView("_EnrollmentPopup", tournament);
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
                ////////TournamentData TData = new TournamentData();
                ////////var tournamentData = _tournamentService.GetTournament(tournament.Id);


                ////////tournament.IsActive = tournamentData.IsActive;
                ////////tournament.CreatedDate = tournamentData.CreatedDate;
                ////////tournament.CreatedBy = tournamentData.CreatedBy;
                ////////tournament.EndDate = tournamentData.EndDate;
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

        public JsonResult UpdateTournamentEndDate(int tournamentId, string EndDate)
        {
            try
            {

                bool isUpdated = false;
                isUpdated = _tournamentService.UpdateTournamentEndDate(tournamentId, EndDate);

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

        public JsonResult GetEnrollmentStatus(int TournamentId)
        {
            try
            {
                int IsEnrolled = 1;

                int teamId = 0;
                var loggedUser = SessionUser.GetUser(_session);
                if (loggedUser != null)
                {
                    var team = _teamService.GetTeamByUser(loggedUser.UserId);
                    teamId = team != null ? team.Id : 0;
                }

                IsEnrolled = _tournamentService.GetEnrollmentStatus(TournamentId, teamId);

                if (IsEnrolled == 0)
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

        public JsonResult GetTournamentEndDate(int TournamentId)
        {
            try
            {
                int IsEnrolled = 1;
                string EndDate = "";
                var Tournament = _tournamentService.GetTournament(TournamentId);

                if (Tournament.EndDate != null)
                {
                    EndDate = Convert.ToDateTime(Tournament.EndDate).ToString("dd-MM-yyyy HH:mm");
                }

                return Json(EndDate);
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
                int teamId = 0;
                var loggedUser = SessionUser.GetUser(_session);
                if (loggedUser != null)
                {
                    var team = _teamService.GetTeamByUser(loggedUser.UserId);
                    teamId = team != null ? team.Id : 0;
                    tournamentEnrollmentVM.TeamId = teamId;
                }
                bool isEnrolled = false;
                string filePath = _hostEnvironment.WebRootPath + $@"/UserContent/Tournaments/{tournamentEnrollmentVM.TournamentId}/{tournamentEnrollmentVM.TeamId}";
                string Attachments = WizFileHandling.UploadAttachments(paymentImageFile, filePath, true);

                var tournamentTeam = new TournamentTeam();
                tournamentTeam.TournamentId = tournamentEnrollmentVM.TournamentId;
                tournamentTeam.TeamId = tournamentEnrollmentVM.TeamId;
                //////tournamentTeam.PaymentType = (int)tournamentEnrollmentVM.PaymentType;
                //////tournamentTeam.IsPaymentMade = tournamentEnrollmentVM.IsPaymentMade;
                //////tournamentTeam.PaymentProof = Attachments;
                tournamentTeam.PaymentType = 1;
                tournamentTeam.IsPaymentMade = true;
                tournamentTeam.EnrollmentDate = DateTime.UtcNow;
                tournamentTeam.IsPaymentVerifiedByAdmin = 'N';
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