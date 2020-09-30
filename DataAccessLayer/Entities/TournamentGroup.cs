using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataAccessLayer.Entities
{
    public partial class TournamentGroup
    {

        public TournamentGroup()
        {
            TeamTournamentGroup = new HashSet<TeamTournamentGroup>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? TournamentId { get; set; }
        public bool? IsActive { get; set; }

        public User CreatedByNavigation { get; set; }

        public Tournament Tournament { get; set; }
        //////public ICollection<TournamentDraw> TournamentDraw { get; set; }
        public ICollection<TeamTournamentGroup> TeamTournamentGroup { get; set; }
    }

    public partial class TournamentGroupData
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? TournamentId { get; set; }
        public bool? IsActive { get; set; }
    }
}

