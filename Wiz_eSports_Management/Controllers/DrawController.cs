using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Wiz_eSports_Management.Controllers
{
    public class DrawController : Controller
    {
        private readonly ILogger<DrawController> _logger;
        private readonly IConfiguration _configuration;

        private readonly TournamentGroupService _tournamentGroupService;
        private readonly TournamentTeamService _tournamentTeamService;
        private readonly TournamentDrawService _tournamentDrawService;

        public DrawController(IConfiguration configuration, ILogger<DrawController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _tournamentGroupService = new TournamentGroupService(configuration);
            _tournamentTeamService = new TournamentTeamService(configuration);
            _tournamentDrawService = new TournamentDrawService(configuration);
        }

        public IActionResult Index(int tournamentId)
        {
            try
            {
                var draw = _tournamentDrawService.GetTournamentDraws(tournamentId);
                return View(draw);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public JsonResult AddDraw(TournamentDraw tournamentDraw)
        {
            try
            {
                bool isSaved = false;
                isSaved = _tournamentDrawService.SaveTournamentDraw(tournamentDraw);

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

        public JsonResult UpdateDraw(TournamentDraw tournamentDraw)
        {
            try
            {
                bool isUpdated = false;
                isUpdated = _tournamentDrawService.UpdateTournamentDraw(tournamentDraw);

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

        public JsonResult DeleteDraw(int DrawId)
        {
            try
            {
                bool isDeleted = false;
                isDeleted = _tournamentDrawService.DeleteTournamentDraw(DrawId);

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