using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Wiz_eSports_Management.Controllers
{
    public class ClanController : Controller
    {
        private readonly ILogger<ClanController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ClanService _clanService;
        private readonly TeamService _teamService;
        private readonly PlayerService _playerService;

        public ClanController(IConfiguration configuration, ILogger<ClanController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _clanService = new ClanService(configuration);
            _teamService = new TeamService(configuration);
            _playerService = new PlayerService(configuration);
        }

        public IActionResult Index()
        {
            try
            {
                var clans = _clanService.GetClans();
                return View(clans);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Details(int clanId)
        {
            try
            {
                var clan = _clanService.GetClan(clanId);
                return View(clan);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult GetTeams(int clanId)
        {
            try
            {
                var teams = _teamService.GetTeams(clanId);
                return PartialView("_TeamList", teams);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return NoContent();
            }
        }

        public IActionResult TeamDetails(int teamId)
        {
            try
            {
                var team = _teamService.GetTeam(teamId);
                return View(team);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult GetPlayers(int teamId)
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
    }
}