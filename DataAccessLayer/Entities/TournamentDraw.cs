using System;

namespace DataAccessLayer.Entities
{
    public partial class TournamentDraw
    {
        public int Id { get; set; }
        public DateTime? DrawDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? TournamentId { get; set; }
        public bool? IsActive { get; set; }

        public User CreatedByNavigation { get; set; }
        public Tournament Tournament { get; set; }
    }
}
