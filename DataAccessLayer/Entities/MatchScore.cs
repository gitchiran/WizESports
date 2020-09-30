using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class MatchScore
    {
        public int Id { get; set; }
        public int? TournamentId { get; set; }
        public int? MatchId { get; set; }
        public int? TeamId { get; set; }
        public string ScoreType { get; set; }
        public decimal? Score { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public Match Match { get; set; }
        public Team Team { get; set; }

        ////public TournamentGroup TournamentGroup { get; set; }
        public User UpdatedByNavigation { get; set; }
    }

    public partial class MatchScoreData
    {
        public int Id { get; set; }
        public int? TournamentId { get; set; }
        public string TournamentName { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public int? TeamId { get; set; }
        public string TeamName { get; set; }
        public string ScoreType { get; set; }
        public decimal? Score { get; set; }
        public int Rank { get; set; }
        public int Kills { get; set; }
        public int Wins { get; set; }
        public int TotalCount { get; set; }
    }
}
