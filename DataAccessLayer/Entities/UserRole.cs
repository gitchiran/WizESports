using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class UserRole
    {
        public UserRole()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserRole1 { get; set; }
        public string Description { get; set; }

        public ICollection<User> User { get; set; }
    }
}
