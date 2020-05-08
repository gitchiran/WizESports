using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Team
    {
        public Team()
        {
            MatchScore = new HashSet<MatchScore>();
            Player = new HashSet<Player>();
            TeamTournamentGroup = new HashSet<TeamTournamentGroup>();
            TournamentTeam = new HashSet<TournamentTeam>();
        }

        public int Id { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool? IsActive { get; set; }
        public decimal? Score { get; set; }
        public int? Rank { get; set; }
        public int? ClanId { get; set; }
        public int? UserId { get; set; }
        public int? ContactPerson { get; set; }
        public string LogoPath { get; set; }

        public Clan Clan { get; set; }
        public ContactPerson ContactPersonNavigation { get; set; }
        public User User { get; set; }
        public ICollection<MatchScore> MatchScore { get; set; }
        public ICollection<Player> Player { get; set; }
        public ICollection<TeamTournamentGroup> TeamTournamentGroup { get; set; }
        public ICollection<TournamentTeam> TournamentTeam { get; set; }
    }
}
