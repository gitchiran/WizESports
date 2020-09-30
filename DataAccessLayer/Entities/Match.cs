using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Match
    {
        public Match()
        {
            MatchScore = new HashSet<MatchScore>();
        }

        public int Id { get; set; }
        public string MatchDescription { get; set; }
        public DateTime? MatchDate { get; set; }
        public int TournamentId { get; set; }
        public bool? IsActive { get; set; }

        public Tournament Tournament { get; set; }
        public ICollection<MatchScore> MatchScore { get; set; }
        
    }
}
