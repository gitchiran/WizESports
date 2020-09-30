using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class TournamentDrawDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public TournamentDrawDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<TournamentDraw> GetTournamentDraws()
        {
            try
            {
                return db.TournamentDraw.Include(t => t.Tournament).ToList();
            }
            catch (Exception)
            {
                return new List<TournamentDraw>();
            }
        }

        public TournamentDraw GetTournamentDraws(int tournamentId)
        {
            try
            {
                return db.TournamentDraw.FirstOrDefault(t => t.TournamentId == tournamentId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<TournamentDraw> GetTournamentDrawDetails(int tournamentId, int? groupId, int teamId)
        {
            try
            {
                if(teamId>0)
                {
                    return db.TournamentDraw.Where(t => t.TournamentId == tournamentId && t.GroupId==groupId && t.TeamId==teamId && t.IsActive == true).Include(t => t.Tournament).Include(t => t.Tournament.TournamentGroup).Include(t => t.Team).ToList();
                }
                else if(groupId>0)
                {
                    return db.TournamentDraw.Where(t => t.TournamentId == tournamentId && t.GroupId == groupId && t.IsActive == true).Include(t => t.Tournament).Include(t => t.Tournament.TournamentGroup).Include(t => t.Team).ToList();
                }
                else
                {
                    return db.TournamentDraw.Where(t => t.TournamentId == tournamentId && t.IsActive == true).Include(t => t.Tournament).Include(t => t.Tournament.TournamentGroup).Include(t => t.Team).ToList();
                }                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<TournamentDraw> GetTournamentTeamGroupDetails(int tournamentId, int teamId)
        {
            try
            {
                return db.TournamentDraw.Where(t => t.TournamentId == tournamentId && t.TeamId == teamId && t.IsActive == true).Include(t => t.Tournament).Include(t => t.Tournament.TournamentGroup).Include(t => t.Team).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TournamentDraw GetTournamentDraw(int drawId)
        {
            try
            {
                return db.TournamentDraw.FirstOrDefault(t => t.Id == drawId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveTournamentDraw(TournamentDraw tournamentDraw)
        {
            try
            {
                int isSaved = 0;
                db.TournamentDraw.Add(tournamentDraw);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTournamentDraw(TournamentDraw tournamentDraw)
        {
            try
            {
                int isUpdated = 0;
                db.TournamentDraw.Update(tournamentDraw);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTournamentDraw(int drawId)
        {
            try
            {
                int isDeleted = 0;
                var TournamentDraw = db.TournamentDraw.FirstOrDefault(t => t.Id == drawId);
                if (TournamentDraw != null)
                {
                    TournamentDraw.IsActive = false;
                    db.TournamentDraw.Update(TournamentDraw);
                    isDeleted = db.SaveChanges();
                }

                return isDeleted > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
