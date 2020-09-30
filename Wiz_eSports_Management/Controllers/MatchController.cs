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
using System.Collections.Immutable;

namespace Wiz_eSports_Management.Controllers
{
    public class MatchController : Controller
    {
        private readonly ILogger<MatchController> _logger;
        private readonly IConfiguration _configuration;
        private readonly MatchService _matchService;
        private readonly MatchScoreService _matchScoreService;
        private readonly ISession _session;
        private readonly TeamService _teamService;
        private readonly TournamentDrawService _tournamentDrawService;

        public MatchController(IConfiguration configuration, ILogger<MatchController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _teamService = new TeamService(configuration);
            _matchService = new MatchService(configuration);
            _matchScoreService = new MatchScoreService(configuration);
            _tournamentDrawService = new TournamentDrawService(configuration);

            _session = httpContextAccessor.HttpContext.Session;
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

        public JsonResult GetTournamentMatchScoreDetails(int tournamentId, int teamId)
        {
            try
            {
                var TournamentMatchScore = _matchService.GetTournamentMatchScoreDetails(tournamentId, teamId, "").ToList();

                List<MatchScoreData> matchscorelst = new List<MatchScoreData>();

                for (var i = 0; i < TournamentMatchScore.Count(); i++)
                {
                    MatchScoreData matchscore = new MatchScoreData();

                    matchscore.Id = TournamentMatchScore[i].Id;
                    matchscore.TeamId = TournamentMatchScore[i].TeamId;
                    matchscore.TournamentId = TournamentMatchScore[i].TournamentId;
                    matchscore.TeamName = TournamentMatchScore[i].Team.TeamName;
                    matchscore.ScoreType = TournamentMatchScore[i].ScoreType;
                    matchscore.Score = TournamentMatchScore[i].Score;

                    matchscorelst.Add(matchscore);
                }

                return Json(matchscorelst);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult GetTournamentMatchScoreRankingDetails(int tournamentId)
        {
            try
            {
                int teamId = 0;
                List<MatchScoreData> matchscorelst = new List<MatchScoreData>();
                var loggedUser = SessionUser.GetUser(_session);
                if (loggedUser != null)
                {
                    var team = _teamService.GetTeamByUser(loggedUser.UserId);
                    teamId = team != null ? team.Id : 0;
                }

                var TournamentMatchScore = _matchService.GetTournamentMatchScoreDetails(tournamentId, 0, "").ToList();

                var MatchGroup = TournamentMatchScore.GroupBy(x => x.TeamId).ToList();

                for (var i = 0; i < MatchGroup.Count(); i++)
                {
                    MatchScoreData matchScoreData = new MatchScoreData();
                    decimal Score = 0;
                    int Killings = 0;
                    int Wins = 0;
                    var MatchGroupData = MatchGroup[i].ToList();
                    for (var j = 0; j < MatchGroup[i].Count(); j++)
                    {
                        Score = Score + Convert.ToDecimal(MatchGroupData[j].Score);

                        if (MatchGroupData[j].ScoreType == "Kill")
                        {
                            Killings = Killings + Convert.ToInt32(MatchGroupData[j].Score);
                        }

                        if (MatchGroupData[j].ScoreType == "Wins")
                        {
                            Wins = Wins + Convert.ToInt32(MatchGroupData[j].Score);
                        }
                    }

                    matchScoreData.TeamId = MatchGroupData[0].TeamId;
                    matchScoreData.TeamName = MatchGroupData[0].Team.TeamName;
                    matchScoreData.Score = Score - Wins;
                    matchScoreData.Kills = Killings;
                    matchScoreData.Wins = Wins;
                    matchScoreData.TotalCount = MatchGroup.Count();

                    matchscorelst.Add(matchScoreData);
                }

                var ranks = matchscorelst.Select(c => new { c.TeamId, c.TeamName, c.Score, c.Kills, c.Wins, c.TotalCount, Rank = TournamentMatchScore.Count(c2 => c2.Score > c.Score) + 1 }).OrderBy(d => d.Rank).ToList();

                return Json(ranks);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult GetTournamentMatchScoreClanRank(string section, int teamId = 0)
        {
            try
            {
                int tournamentId = 0, GroupId = 0;
                string tournamentName = "", GroupName = "";
                List<MatchScoreData> matchscorelst = new List<MatchScoreData>();
                var loggedUser = SessionUser.GetUser(_session);
                int SessTeamId = teamId;
                if (teamId == 0)
                {
                    if (loggedUser != null)
                    {
                        var team = _teamService.GetTeamByUser(loggedUser.UserId);
                        teamId = team != null ? team.Id : 0;
                    }
                }

                if (section == "TournamentbySchedule")
                {
                    var Tournament = _matchService.GetRecentTournamentbySchedule().ToList();

                    for (var k = 0; k < Tournament.Count(); k++)
                    {
                        tournamentId = Tournament[k].Id;
                        tournamentName = Tournament[k].TournamentName;
                    }
                }

                var TournamentMatchScore = _matchService.GetTournamentMatchScoreDetails(tournamentId, 0, "").ToList();

                var MatchGroup = TournamentMatchScore.GroupBy(x => x.TeamId).ToList();

                for (var i = 0; i < MatchGroup.Count(); i++)
                {
                    MatchScoreData matchScoreData = new MatchScoreData();
                    decimal Score = 0;
                    int Killings = 0;
                    int Wins = 0;
                    var MatchGroupData = MatchGroup[i].ToList();
                    for (var j = 0; j < MatchGroup[i].Count(); j++)
                    {
                        Score = Score + Convert.ToDecimal(MatchGroupData[j].Score);

                        if (MatchGroupData[j].ScoreType == "Kill")
                        {
                            Killings = Killings + Convert.ToInt32(MatchGroupData[j].Score);
                        }

                        if (MatchGroupData[j].ScoreType == "Wins")
                        {
                            Wins = Wins + Convert.ToInt32(MatchGroupData[j].Score);
                        }
                    }

                    if (teamId != MatchGroupData[0].TeamId)
                    {
                        teamId = Convert.ToInt32(MatchGroupData[0].TeamId);
                        var TournamentGroup = _tournamentDrawService.GetTournamentTeamGroupDetails(tournamentId, teamId).ToList();

                        if (TournamentGroup.Count() > 0)
                        {
                            GroupId = Convert.ToInt32(TournamentGroup[0].GroupId);
                            GroupName = TournamentGroup[0].Tournament.TournamentGroup.Where(x => x.Id == GroupId).ToList()[0].GroupName;
                        }
                    }

                    matchScoreData.TeamId = MatchGroupData[0].TeamId;
                    matchScoreData.TeamName = MatchGroupData[0].Team.TeamName;
                    matchScoreData.Score = Score - Wins;
                    matchScoreData.Kills = Killings;
                    matchScoreData.Wins = Wins;
                    matchScoreData.TotalCount = MatchGroup.Count();
                    matchScoreData.TournamentId = tournamentId;
                    matchScoreData.TournamentName = tournamentName;
                    matchScoreData.GroupId = GroupId;
                    matchScoreData.GroupName = GroupName;

                    matchscorelst.Add(matchScoreData);
                }

                if (section == "Team")
                {
                    var team = _teamService.GetTeamByUser(loggedUser.UserId);

                    if (SessTeamId == 0)
                    {
                        teamId = team != null ? team.Id : 0;
                    }
                    else
                    {
                        teamId = SessTeamId;
                    }
                    var ranks = matchscorelst.Select(c => new { c.TeamId, c.TeamName, c.GroupId, c.GroupName, c.TournamentId, c.TournamentName, c.Score, c.Kills, c.Wins, c.TotalCount, Rank = matchscorelst.Count(c2 => c2.Score > c.Score) + 1 }).OrderBy(d => d.Rank).ToList();

                    ranks = ranks.Where(x => x.TeamId == teamId).ToList();

                    return Json(ranks);
                }
                //////else if (section == "TournamentbySchedule")
                //////{
                //////    var ranks = matchscorelst.Select(c => new { c.TeamId, c.TeamName, c.Score, c.Kills, c.TotalCount, Rank = TournamentMatchScore.Count(c2 => c2.Score > c.Score) + 1 }).OrderBy(d => d.Rank).ToList();
                //////    return Json(ranks);
                //////}
                else
                {
                    var ranks = matchscorelst.Take(25).Select(c => new { c.TeamId, c.TeamName, c.GroupId, c.GroupName, c.TournamentId, c.TournamentName, c.Score, c.Kills, c.Wins, c.TotalCount, Rank = matchscorelst.Count(c2 => c2.Score > c.Score) + 1 }).OrderBy(d => d.Rank).ToList();
                    return Json(ranks);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult AddTournamentMatchScore(int tournamentId, string ScoreType, int teamId, decimal Score)
        {
            try
            {
                MatchScore matchscore = new MatchScore();
                bool isSaved = false;

                matchscore.TournamentId = tournamentId;
                matchscore.TeamId = teamId;
                matchscore.ScoreType = ScoreType;
                matchscore.Score = Score;
                matchscore.IsActive = true;
                matchscore.UpdatedDate = DateTime.UtcNow;
                matchscore.UpdatedBy = SessionUser.GetUserId(_session);

                var TournamentMatchScoreCount = _matchService.GetTournamentMatchScoreDetails(tournamentId, teamId, ScoreType).ToList().Count();


                if (TournamentMatchScoreCount == 0)
                {
                    isSaved = _matchService.SaveMatchScore(matchscore);

                    if (isSaved)
                    {
                        return Json(new { status = 200, message = "success" });
                    }
                    else
                    {
                        return Json(new { status = 201, message = "failed" });
                    }
                }
                else
                {
                    return Json(new { status = 202, message = "duplicate" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return Json(new { status = 500, message = "error" });
            }
        }

        public JsonResult UpdateTournamentMatchScore(int Id, decimal Score)
        {
            try
            {
                bool isUpdated = false;
                MatchScore matchscore = new MatchScore();

                matchscore.Id = Id;
                matchscore.Score = Score;
                isUpdated = _matchService.UpdateMatchScore(matchscore);

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

        public JsonResult DeleteMatchScore(int matchId)
        {
            try
            {
                bool isDeleted = false;
                isDeleted = _matchService.DeleteMatchScore(matchId);

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