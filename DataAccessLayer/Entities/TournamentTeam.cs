using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class TournamentTeam
    {
        public int Id { get; set; }
        public int? TournamentId { get; set; }
        public int? TeamId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public int? PaymentType { get; set; }
        public bool? IsPaymentMade { get; set; }
        public string PaymentProof { get; set; }

        public Team Team { get; set; }
        public Tournament Tournament { get; set; }
    }
}
