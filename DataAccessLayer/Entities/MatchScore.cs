using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class MatchScore
    {
        public int Id { get; set; }
        public int? MatchId { get; set; }
        public int? TeamId { get; set; }
        public decimal? Score { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Match Match { get; set; }
        public Team Team { get; set; }
        public User UpdatedByNavigation { get; set; }
    }
}
