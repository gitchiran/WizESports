using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using Wiz_eSports_Management.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MimeKit;
using System.IO;


namespace Wiz_eSports_Management.Controllers
{
    public class TeamController : Controller
    {
        private readonly ILogger<TeamController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISession _session;

        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly TeamService _teamService;
        private readonly PlayerService _playerService;
        private readonly TournamentTeamService _tournamentTeamService;
        private readonly TournamentDrawService _tournamentDrawService;

        public TeamController(IConfiguration configuration, ILogger<TeamController> logger, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _session = httpContextAccessor.HttpContext.Session;

            _hostEnvironment = hostEnvironment;
            _teamService = new TeamService(configuration);
            _playerService = new PlayerService(configuration);
            _tournamentTeamService = new TournamentTeamService(configuration);
            _tournamentDrawService = new TournamentDrawService(configuration);
        }

        public IActionResult Index()
        {
            try
            {
                var teams = _teamService.GetTeams();
                return View(teams);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult RegisteredTournaments()
        {
            try
            {
                var teams = _teamService.GetTeams();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Home()
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
                return RedirectToAction("Details", "Team", teamId);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult GetTournamentTeams(int tournamentId)
        {
            try
            {
                var teams = _tournamentTeamService.GetTournamentTeams(tournamentId);
                return PartialView("_TournamentTeamList", teams);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return NoContent();
            }
        }

        public JsonResult GetRegisteredTournamentTeams(int tournamentId)
        {
            try
            {
                var teams = _tournamentTeamService.GetTournamentTeams(tournamentId).ToList();

                List<TournamentTeamData> tournamentteamlst = new List<TournamentTeamData>();

                for (var i = 0; i < teams.Count(); i++)
                {
                    ////string filePath = _hostEnvironment.WebRootPath + $@"/UserContent/Tournaments/{tournamentId}/{teams[i].TeamId}/";
                    string filePath = $@"/UserContent/Tournaments/{tournamentId}/{teams[i].TeamId}/";
                    TournamentTeamData tournamentteam = new TournamentTeamData();
                    int? PaymentType = 0;
                    tournamentteam.Id = teams[i].Id;
                    tournamentteam.TournamentId = teams[i].TournamentId;
                    ////tournamentteam.TournamentName = teams[i].Tournament.TournamentName;
                    tournamentteam.TeamId = teams[i].TeamId;
                    tournamentteam.TeamName = teams[i].Team.TeamName;
                    tournamentteam.EnrollmentDateString = Convert.ToDateTime(teams[i].EnrollmentDate).ToString("dd-MM-yyyy");
                    PaymentType = teams[i].PaymentType;
                    tournamentteam.IsPaymentVerifiedByAdmin = teams[i].IsPaymentVerifiedByAdmin;

                    if (PaymentType == 1)
                    {
                        tournamentteam.PaymentTypeString = "Bank";
                    }
                    else if (PaymentType == 2)
                    {
                        tournamentteam.PaymentTypeString = "Card";
                    }
                    else if (PaymentType == 3)
                    {
                        tournamentteam.PaymentTypeString = "PayPal";
                    }

                    tournamentteam.IsPaymentMade = teams[i].IsPaymentMade;

                    if (teams[i].IsPaymentMade == false)
                    {
                        tournamentteam.IsPaymentMadeString = "No";
                    }
                    else if (teams[i].IsPaymentMade == true)
                    {
                        tournamentteam.IsPaymentMadeString = "Yes";
                    }
                    tournamentteam.PaymentProof = filePath + teams[i].PaymentProof;

                    tournamentteamlst.Add(tournamentteam);
                }

                return Json(tournamentteamlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult GetMatchRegisteredTournamentTeams(int tournamentId, int groupid)
        {
            try
            {
                var teams = _tournamentTeamService.GetTournamentTeams(tournamentId).ToList();

                var TournamentDraw = _tournamentDrawService.GetTournamentDrawDetails(tournamentId, groupid, 0).ToList();

                List<TournamentTeamData> tournamentteamlst = new List<TournamentTeamData>();

                for (var i = 0; i < teams.Count(); i++)
                {
                    TournamentTeamData tournamentteam = new TournamentTeamData();

                    tournamentteam.TeamId = teams[i].TeamId;
                    tournamentteam.TeamName = teams[i].Team.TeamName;

                    int Count = 0;

                    for (var j = 0; j < TournamentDraw.Count(); j++)
                    {
                        int? TeamId = TournamentDraw[j].TeamId;

                        if (teams[i].TeamId == TeamId)
                        {
                            tournamentteamlst.Add(tournamentteam);
                        }
                    }

                    //////if (Count == 0)
                    //////{
                    //////    tournamentteamlst.Add(tournamentteam);
                    //////}

                }

                return Json(tournamentteamlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult GetDrawRegisteredTournamentTeams(int tournamentId, int groupid)
        {
            try
            {
                var teams = _tournamentTeamService.GetTournamentTeams(tournamentId).ToList();

                var TournamentDraw = _tournamentDrawService.GetTournamentDrawDetails(tournamentId, 0, 0).ToList();

                List<TournamentTeamData> tournamentteamlst = new List<TournamentTeamData>();

                for (var i = 0; i < teams.Count(); i++)
                {
                    TournamentTeamData tournamentteam = new TournamentTeamData();

                    tournamentteam.TeamId = teams[i].TeamId;
                    tournamentteam.TeamName = teams[i].Team.TeamName;

                    int Count = 0;

                    for (var j = 0; j < TournamentDraw.Count(); j++)
                    {
                        int? TeamId = TournamentDraw[j].TeamId;

                        if(teams[i].TeamId==TeamId)
                        {
                            Count++;
                        }
                    }

                    if(Count==0)
                    {
                        tournamentteamlst.Add(tournamentteam);
                    }
                    
                }

                return Json(tournamentteamlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult UpdateTeamPaymentVerificationStatus(int Id, char IsPaymentVerifiedByAdmin, string AdminComments)
        {
            try
            {
                bool isUpdated = false;
                isUpdated = _teamService.UpdatePaymentVerificationStatus(Id, IsPaymentVerifiedByAdmin, AdminComments);
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

        public IActionResult Download(string DocumentPath)
        {
            string DocPath = _hostEnvironment.WebRootPath + DocumentPath;
            return PhysicalFile(DocPath, MimeTypes.GetMimeType(DocumentPath), Path.GetFileName(DocumentPath));
        }

        ////////public FileResult Download(string DocumentPath)
        ////////{
        ////////    string DocPath = System.Net.HttpContext.Current.Server.MapPath(DocumentPath);
        ////////    return File((DocumentPath), System.Net.Mime.MediaTypeNames.Application.Octet, "PaymentProof");
        ////////}

        public IActionResult Details(int teamId)
        {
            try
            {
                Team team;
                var user = SessionUser.GetUser(_session);

                if (teamId == 0)
                {
                    if (user != null && user.RoleId == 2)
                    {
                        team = _teamService.GetTeamByUser(user.UserId);
                    }
                    else
                    {
                        return RedirectToAction("PageNotFound", "Home");
                    }
                }
                else
                {
                    team = _teamService.GetTeam(teamId);
                }

                ViewBag.UserRole = user != null ? user.RoleId : 0;

                return View(team);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public JsonResult UpdateTeam(Team team)
        {
            try
            {
                bool isUpdated = false;
                isUpdated = _teamService.UpdateTeam(team);
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

        public JsonResult DeleteTeam(int teamId)
        {
            try
            {
                bool isDeleted = false;
                isDeleted = _teamService.DeleteTeam(teamId);
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

        public IActionResult GetPlayersView(int teamId)
        {
            try
            {
                var players = _playerService.GetPlayers(teamId);
                return PartialView("_PlayerList", players);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return NoContent();
            }
        }

        public JsonResult GetPlayers(int teamId)
        {
            try
            {
                var players = _playerService.GetPlayers(teamId);
                return Json(players);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult SavePlayer(Player player)
        {
            try
            {
                bool isSaved = false;
                isSaved = _playerService.SavePlayer(player);
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

        public JsonResult UpdatePlayer(Player player)
        {
            try
            {
                bool isSaved = false;
                isSaved = _playerService.UpdatePlayer(player);
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

        public JsonResult DeletePlayer(int playerId)
        {
            try
            {
                bool isDeleted = false;
                isDeleted = _playerService.DeletePlayer(playerId);

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


    }
}