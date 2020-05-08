using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAO
{
    public class TournamentGroupDao: BaseDao
    {
        private readonly IConfiguration _configuration;

        public TournamentGroupDao(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<TournamentGroup> GetTournamentGroups()
        {
            try
            {
                return db.TournamentGroup.Include(t => t.Tournament).ToList();
            }
            catch (Exception)
            {
                return new List<TournamentGroup>();
            }
        }

        public IEnumerable<TournamentGroup> GetTournamentGroups(int tournamentId)
        {
            try
            {
                return db.TournamentGroup.Where(t => t.TournamentId == tournamentId).Include(t => t.Tournament).ToList();
            }
            catch (Exception)
            {
                return new List<TournamentGroup>();
            }
        }

        public TournamentGroup GetTournamentGroup(int groupId)
        {
            try
            {
                return db.TournamentGroup.FirstOrDefault(t => t.Id == groupId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveTournamentGroup(TournamentGroup tournamentGroup)
        {
            try
            {
                int isSaved = 0;
                db.TournamentGroup.Add(tournamentGroup);
                isSaved = db.SaveChanges();

                return isSaved > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTournamentGroup(TournamentGroup tournamentGroup)
        {
            try
            {
                int isUpdated = 0;
                db.TournamentGroup.Update(tournamentGroup);
                isUpdated = db.SaveChanges();

                return isUpdated > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTournamentGroup(int groupId)
        {
            try
            {
                int isDeleted = 0;
                var tournamentGroup = db.TournamentGroup.FirstOrDefault(t => t.Id == groupId);
                if (tournamentGroup != null)
                {
                    tournamentGroup.IsActive = false;
                    db.TournamentGroup.Update(tournamentGroup);
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
