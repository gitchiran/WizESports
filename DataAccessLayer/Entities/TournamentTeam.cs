﻿using System;
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
        public char IsPaymentVerifiedByAdmin { get; set; }
        public string AdminComments { get; set; }

        public Team Team { get; set; }
        public Tournament Tournament { get; set; }
    }

    public partial class TournamentTeamData
    {
        public int Id { get; set; }
        public int? TournamentId { get; set; }
        public string TournamentName { get; set; }
        public int? TeamId { get; set; }
        public string TeamName { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string EnrollmentDateString { get; set; }
        public int? PaymentType { get; set; }
        public string PaymentTypeString { get; set; }
        public bool? IsPaymentMade { get; set; }
        public string IsPaymentMadeString { get; set; }
        public string PaymentProof { get; set; }
        public char IsPaymentVerifiedByAdmin { get; set; }
        public string AdminComments { get; set; }

    }
}
