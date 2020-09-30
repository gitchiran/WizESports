using System;

namespace DataAccessLayer.Entities
{
    public partial class TournamentDraw
    {
        public int Id { get; set; }
        public DateTime? DrawDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? TournamentId { get; set; }
        public int? GroupId { get; set; }
        public int? TeamId { get; set; }
        public bool? IsActive { get; set; }
        public User CreatedByNavigation { get; set; }
        public Tournament Tournament { get; set; }
        ////public TournamentGroup TournamentGroup { get; set; }
        public Team Team { get; set; }
    }

    public partial class TournamentDrawData
    {
        public int Id { get; set; }
        public int? TournamentId { get; set; }
        public string TournamentName { get; set; }
        public int? GroupId { get; set; }
        public int? TeamId { get; set; }
        public string DrawDate { get; set; }
        public string GroupName { get; set; }
        public string TeamName { get; set; }

    }

    public partial class TournamentDraw1
    {
        public int TournamentId { get; set; }
        public int GroupId { get; set; }
        public int TeamId { get; set; }
    }
}
