using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class ContactPerson
    {
        public ContactPerson()
        {
            Clan = new HashSet<Clan>();
            Team = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string Cpname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Nic { get; set; }

        public ICollection<Clan> Clan { get; set; }
        public ICollection<Team> Team { get; set; }
    }
}
