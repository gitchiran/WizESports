using BusinessLogicLayer.Services;
using DataAccessLayer.DAO;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Wiz_eSports_Management.Controllers
{
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IConfiguration _configuration;

        private readonly TournamentGroupService _tournamentGroupService;
        private readonly TeamTournamentGroupService _teamTournamentGroupService;

        public GroupController(IConfiguration configuration, ILogger<GroupController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _tournamentGroupService = new TournamentGroupService(configuration);
            _teamTournamentGroupService = new TeamTournamentGroupService(configuration);
        }

        public IActionResult Index(int tournamentId)
        {
            try
            {
                var groups = _tournamentGroupService.GetTournamentGroups(tournamentId);
                return View(groups);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return RedirectToAction("Error", "Home");
            }
        }

        public JsonResult GetTournamentGroups(int tournamentId)
        {
            try
            {
                var groups = _tournamentGroupService.GetTournamentGroups(tournamentId).ToList();

                List<TournamentGroupData> tournamentgrouplst = new List<TournamentGroupData>();               

                for (var i = 0; i < groups.Count(); i++)
                {
                    TournamentGroupData tournamentgroup = new TournamentGroupData();

                    tournamentgroup.Id = groups[i].Id;
                    tournamentgroup.GroupName = groups[i].GroupName;
                    tournamentgroup.TournamentId = groups[i].TournamentId;
                    tournamentgroup.CreatedDate = groups[i].CreatedDate;
                    tournamentgroup.IsActive = groups[i].IsActive;

                    tournamentgrouplst.Add(tournamentgroup);
                }

                return Json(tournamentgrouplst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public IActionResult TournamentGroups()
        {
            try
            {
                return PartialView("_TournamentGroups", null);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return NoContent();
            }
        }

        public JsonResult AddGroup(TournamentGroup tournamentGroup)
        {
            try
            {
                bool isSaved = false;
                tournamentGroup.IsActive = true;
                isSaved = _tournamentGroupService.SaveTournamentGroup(tournamentGroup);

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

        public JsonResult UpdateGroup(TournamentGroup tournamentGroup)
        {
            try
            {
                bool isUpdated = false;
                isUpdated = _tournamentGroupService.UpdateTournamentGroup(tournamentGroup);

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

        public JsonResult DeleteGroup(int groupId)
        {
            try
            {
                bool isDeleted = false;
                isDeleted = _tournamentGroupService.DeleteTournamentGroup(groupId);

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

        public IActionResult GetTournamentGroupsList(int tournamentId)
        {
            try
            {
                var groups = _tournamentGroupService.GetTournamentGroups(tournamentId);
                return PartialView("_TournamentGroupList", groups);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return NoContent();
            }
        }

        public JsonResult AddTeamGroup(TeamTournamentGroup teamTournamentGroup)
        {
            try
            {
                bool isSaved = false;
                isSaved = _teamTournamentGroupService.SaveTeamTournamentGroup(teamTournamentGroup);

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

        public JsonResult UpdateTeamGroup(TeamTournamentGroup teamTournamentGroup)
        {
            try
            {
                bool isUpdated = false;
                isUpdated = _teamTournamentGroupService.UpdateTeamTournamentGroup(teamTournamentGroup);

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

        public JsonResult DeleteTeamGroup(int teamTournamentGroupId)
        {
            try
            {
                bool isDeleted = false;
                isDeleted = _teamTournamentGroupService.DeleteTeamTournamentGroup(teamTournamentGroupId);

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

        public IActionResult GetTournamentGroupsWithTeams(int groupId)
        {
            try
            {
                var groupWithTeams = _teamTournamentGroupService.GetTeamTournamentGroupes(groupId);
                return PartialView("_TournamentGroupWithTeamList", groupWithTeams);
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