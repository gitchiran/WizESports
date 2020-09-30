using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class User
    {
        public User()
        {
            Clan = new HashSet<Clan>();
            MatchScore = new HashSet<MatchScore>();
            Team = new HashSet<Team>();
            TournamentDraw = new HashSet<TournamentDraw>();
            TournamentGroup = new HashSet<TournamentGroup>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? RoleId { get; set; }
        public bool? IsLocked { get; set; }
        public string Token { get; set; }
        public bool? IsVerified { get; set; }

        public UserRole Role { get; set; }
        public ICollection<Clan> Clan { get; set; }
        public ICollection<MatchScore> MatchScore { get; set; }
        public ICollection<Team> Team { get; set; }
        public ICollection<TournamentDraw> TournamentDraw { get; set; }
        public ICollection<TournamentGroup> TournamentGroup { get; set; }
    }
}
