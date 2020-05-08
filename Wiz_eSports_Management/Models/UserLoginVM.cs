using System.ComponentModel.DataAnnotations;

namespace Wiz_eSports_Management.Models
{
    public class UserLoginVM
    {
        [Required(ErrorMessage = "Please enter a valid username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a valid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
