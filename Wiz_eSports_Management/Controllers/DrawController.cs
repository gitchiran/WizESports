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
using DataAccessLayer.DAO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Wiz_eSports_Management.Controllers
{
    public class DrawController : Controller
    {
        private readonly ILogger<DrawController> _logger;
        private readonly IConfiguration _configuration;

        private readonly TournamentGroupService _tournamentGroupService;
        private readonly TournamentTeamService _tournamentTeamService;
        private readonly TournamentDrawService _tournamentDrawService;
        private readonly TournamentService _tournamentService;
        private readonly TeamService _teamService;
        private readonly ISession _session;

        public DrawController(IConfiguration configuration, ILogger<DrawController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _teamService = new TeamService(configuration);
            _tournamentGroupService = new TournamentGroupService(configuration);
            _tournamentTeamService = new TournamentTeamService(configuration);
            _tournamentDrawService = new TournamentDrawService(configuration);
            _tournamentService = new TournamentService(configuration);

            _session = httpContextAccessor.HttpContext.Session;
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

        public JsonResult GetTournamentDrawDetails(int tournamentId, int groupId, int teamId)
        {
            try
            {
                var TournamentDraw = _tournamentDrawService.GetTournamentDrawDetails(tournamentId, groupId, teamId).ToList();

                List<TournamentDrawData> tournamentdrawlst = new List<TournamentDrawData>();

                for (var i = 0; i < TournamentDraw.Count(); i++)
                {
                    TournamentDrawData tournamentdraw = new TournamentDrawData();

                    tournamentdraw.Id = TournamentDraw[i].Id;
                    tournamentdraw.TeamId = TournamentDraw[i].TeamId;
                    tournamentdraw.TournamentId = TournamentDraw[i].TournamentId;
                    tournamentdraw.GroupId = TournamentDraw[i].GroupId;
                    tournamentdraw.TeamName = TournamentDraw[i].Team.TeamName;
                    tournamentdraw.DrawDate = Convert.ToDateTime(TournamentDraw[i].DrawDate).ToString("dd-MM-yyyy HH:mm");
                    tournamentdraw.TournamentName = TournamentDraw[i].Tournament.TournamentName;

                    var GroupName = TournamentDraw[i].Tournament.TournamentGroup.Where(x => x.Id == tournamentdraw.GroupId).Select(n => n.GroupName);

                    for (int j = 0; j < GroupName.ToList().Count(); j++)
                    {
                        tournamentdraw.GroupName = GroupName.ToList()[j].ToString();
                    }

                    tournamentdrawlst.Add(tournamentdraw);
                }

                return Json(tournamentdrawlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult GetTournamentTeamDrawDetails(int tournamentId)
        {
            try
            {
                int teamId = 0;
                int? GroupId = 0;
                var loggedUser = SessionUser.GetUser(_session);
                if (loggedUser != null)
                {
                    var team = _teamService.GetTeamByUser(loggedUser.UserId);
                    teamId = team != null ? team.Id : 0;
                }
                var TournamentGroup= _tournamentDrawService.GetTournamentTeamGroupDetails(tournamentId, teamId).ToList();

                if(TournamentGroup.Count()>0)
                {
                    GroupId = TournamentGroup[0].GroupId;
                }

                var TournamentDraw = _tournamentDrawService.GetTournamentDrawDetails(tournamentId, GroupId, 0).ToList();

                List<TournamentDrawData> tournamentdrawlst = new List<TournamentDrawData>();

                for (var i = 0; i < TournamentDraw.Count(); i++)
                {
                    TournamentDrawData tournamentdraw = new TournamentDrawData();

                    tournamentdraw.Id = TournamentDraw[i].Id;
                    tournamentdraw.TeamId = TournamentDraw[i].TeamId;
                    tournamentdraw.TournamentId = TournamentDraw[i].TournamentId;
                    tournamentdraw.GroupId = TournamentDraw[i].GroupId;
                    tournamentdraw.TeamName = TournamentDraw[i].Team.TeamName;
                    tournamentdraw.DrawDate = Convert.ToDateTime(TournamentDraw[i].DrawDate).ToString("dd-MM-yyyy HH:mm");
                    tournamentdraw.TournamentName=TournamentDraw[i].Tournament.TournamentName;

                    var GroupName = TournamentDraw[i].Tournament.TournamentGroup.Where(x => x.Id == tournamentdraw.GroupId).Select(n => n.GroupName);

                    for (int j = 0; j < GroupName.ToList().Count(); j++)
                    {
                        tournamentdraw.GroupName = GroupName.ToList()[j].ToString();
                    }

                    tournamentdrawlst.Add(tournamentdraw);
                }

                return Json(tournamentdrawlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult GetTournamentIndexDrawDetails()
        {
            try
            {
                int teamId = 0, tournamentId=0;
                int? GroupId = 0;
                string tournamentName = "";
                var loggedUser = SessionUser.GetUser(_session);
                if (loggedUser != null)
                {
                    var team = _teamService.GetTeamByUser(loggedUser.UserId);
                    teamId = team != null ? team.Id : 0;
                }

                var Tournament = _tournamentService.GetUpcomingTournamentbySchedule().ToList();

                for (var k = 0; k < Tournament.Count(); k++)
                {
                    tournamentId = Tournament[k].Id;
                    tournamentName = Tournament[k].TournamentName;
                }

                var TournamentDraw = _tournamentDrawService.GetTournamentDrawDetails(tournamentId, 0, 0).ToList();

                List<TournamentDrawData> tournamentdrawlst = new List<TournamentDrawData>();

                for (var i = 0; i < TournamentDraw.Count(); i++)
                {
                    TournamentDrawData tournamentdraw = new TournamentDrawData();

                    tournamentdraw.Id = TournamentDraw[i].Id;
                    tournamentdraw.TeamId = TournamentDraw[i].TeamId;
                    tournamentdraw.TournamentId = TournamentDraw[i].TournamentId;
                    tournamentdraw.GroupId = TournamentDraw[i].GroupId;
                    tournamentdraw.TeamName = TournamentDraw[i].Team.TeamName;
                    tournamentdraw.DrawDate = Convert.ToDateTime(TournamentDraw[i].DrawDate).ToString("dd-MM-yyyy HH:mm");
                    tournamentdraw.TournamentName = TournamentDraw[i].Tournament.TournamentName;

                    var GroupName = TournamentDraw[i].Tournament.TournamentGroup.Where(x => x.Id == tournamentdraw.GroupId).Select(n => n.GroupName);

                    for (int j = 0; j < GroupName.ToList().Count(); j++)
                    {
                        tournamentdraw.GroupName = GroupName.ToList()[j].ToString();
                    }

                    tournamentdrawlst.Add(tournamentdraw);
                }

                return Json(tournamentdrawlst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult AddTournamentDraw(int tournamentId, int groupId, int teamId, string MatchDate)
        {
            try
            {
                TournamentDraw tournamentdraw = new TournamentDraw();
                bool isSaved = false;

                tournamentdraw.TournamentId = tournamentId;
                tournamentdraw.GroupId = groupId;
                tournamentdraw.TeamId = teamId;
                tournamentdraw.IsActive = true;
                //////tournamentdraw.DrawDate = DateTime.UtcNow;
                tournamentdraw.DrawDate = Convert.ToDateTime(MatchDate);
                tournamentdraw.CreatedBy = SessionUser.GetUserId(_session);

                isSaved = _tournamentDrawService.SaveTournamentDraw(tournamentdraw);

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

        public JsonResult DeleteTournamentDraw(int DrawId)
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