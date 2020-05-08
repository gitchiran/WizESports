using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class PasswordReset
    {
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
