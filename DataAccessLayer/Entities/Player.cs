using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public partial class Player
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string PlayerName { get; set; }

        public string PlayerTag { get; set; }

        public string Nic { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
