using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using Wiz_eSports_Management.Common;

namespace Wiz_eSports_Management.Controllers
{
    public class TeamController : Controller
    {
        private readonly ILogger<TeamController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISession _session;

        private readonly TeamService _teamService;
        private readonly PlayerService _playerService;
        private readonly TournamentTeamService _tournamentTeamService;

        public TeamController(IConfiguration configuration, ILogger<TeamController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _session = httpContextAccessor.HttpContext.Session;

            _teamService = new TeamService(configuration);
            _playerService = new PlayerService(configuration);
            _tournamentTeamService = new TournamentTeamService(configuration);
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
                if(isSaved)
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