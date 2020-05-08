using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Tournament
    {
        public Tournament()
        {
            Match = new HashSet<Match>();
            TournamentDraw = new HashSet<TournamentDraw>();
            TournamentGroup = new HashSet<TournamentGroup>();
            TournamentTeam = new HashSet<TournamentTeam>();
        }

        public int Id { get; set; }
        public string TournamentName { get; set; }
        public string TournamentDescription { get; set; }
        public DateTime? SceduledDate { get; set; }
        public string ContactPerson { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public int? EntryFee { get; set; }

        public ICollection<Match> Match { get; set; }
        public ICollection<TournamentDraw> TournamentDraw { get; set; }
        public ICollection<TournamentGroup> TournamentGroup { get; set; }
        public ICollection<TournamentTeam> TournamentTeam { get; set; }
    }
}
