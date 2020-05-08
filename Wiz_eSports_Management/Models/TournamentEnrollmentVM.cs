using Wiz_eSports_Management.Enums;

namespace Wiz_eSports_Management.Models
{
    public class TournamentEnrollmentVM
    {
        public int TeamId { get; set; }
        public int TournamentId { get; set; }
        public PaymentMethod PaymentType { get; set; }
        public bool IsPaymentMade { get; set; }
    }
}
