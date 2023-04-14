using AutoOglasiSource.Responses;

namespace AutoOglasiSource.Model.Account
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressEntity Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? UpCount { get; set; }
        public int? DownCount { get; set; }
        public int? Rating { get; set; }
        public byte[] Avatar { get; set; }
    }
}
