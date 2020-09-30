namespace Wiz_eSports_Management.Models
{
    public class UserRegistrationVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TeamName { get; set; }
        public string TeamLogo { get; set; }
        public string TeamDescription { get; set; }
        public string ContactName { get; set; }
        public string ContactNic { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public bool TandC { get; set; }
    }
}
