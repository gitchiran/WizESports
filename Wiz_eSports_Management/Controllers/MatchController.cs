using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Wiz_eSports_Management.Controllers
{
    public class MatchController : Controller
    {
        private readonly ILogger<MatchController> _logger;
        private readonly IConfiguration _configuration;
        private readonly MatchService _matchService;
        private readonly MatchScoreService _matchScoreService;

        public MatchController(IConfiguration configuration, ILogger<MatchController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _matchService = new MatchService(configuration);
            _matchScoreService = new MatchScoreService(configuration);
        }

        public IActionResult Index(int tournamentId)
        {
            try
            {
                var groups = _matchService.GetMatches(tournamentId);
                return View(groups);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}