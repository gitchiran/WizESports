using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Model
{
    public class LoggedUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string LoginError { get; set; }
    }
}
