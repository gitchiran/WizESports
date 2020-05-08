using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Clan
    {
        public Clan()
        {
            Team = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string ClanName { get; set; }
        public string ClanDescription { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int? UserId { get; set; }
        public int? ContactPerson { get; set; }
        public string LogoPath { get; set; }
        public bool? IsActive { get; set; }

        public ContactPerson ContactPersonNavigation { get; set; }
        public User User { get; set; }
        public ICollection<Team> Team { get; set; }
    }
}
