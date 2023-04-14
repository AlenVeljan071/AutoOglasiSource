using System.Reflection.Metadata.Ecma335;

namespace AutoOglasiSource.Model.Account
{
    public class LoginModel
    {
        public int AplicationUserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
