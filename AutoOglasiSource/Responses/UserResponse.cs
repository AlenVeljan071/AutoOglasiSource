using AutoOglasiSource.Model;
using AutoOglasiSource.Responses;

namespace AutoOglasiSource.ResponseModel
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressEntity Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int UpCount { get; set; }
        public int DownCount { get; set; }
        public int Rating { get; set; }
        public string Avatar { get; set; }
        public List<ImageResponse> SavedImages { get; set; }
    }
}
