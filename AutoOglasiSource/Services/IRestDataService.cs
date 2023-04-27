using AutoOglasiSource.Model;
using AutoOglasiSource.Model.Account;
using AutoOglasiSource.Model.Advertisement;

namespace AutoOglasiSource.Services
{
    public interface IRestDataService
    {
        Task<LoginModel> Login(string email, string password);
        Task<LoginModel> SignupAsync(SignupRequest signupTrainerRequest);
        Task<LoginModel> VerifyUserAsync(string email, string code);
        Task<LoginModel> ForgotPasswordAsync(string email);
        Task<LoginModel> ResendVerifyCodeAsync(string email);
        Task<LoginModel> SetNewPassword(string email, string code, string password);
        Task<UserModel> GetUserByIdAsync(int id);
        Task<CreateModel> CreateAdvertisementAsync(AdvertisementRequestModel advRequest);
        Task<AddImageModel> AddPhotoToAdvertisementAsync(AdsImage image);
        Task<ErrorData> DeletePhotoAsync(int id);
        Task<CreateModel> UpdateAdvertisementAsync(AdvertisementEditModel advRequest);
        Task<ErrorData> UpdatePriceAsync(int id, decimal image);
        Task<ErrorData> DeleteAdvertisementAsync(int id);
        Task<List<AdvertisementListModel>> GetAdvertisementListAsync(AdvertisementSpecParams specParams);
        Task<AdvertisementModel> GetAdvertisementByIdAsync(int id);
        Task<List<VehicleBrand>> GetVehicleBrandCatIdAsync(int id);
        Task<List<VehicleModel>> GetVehicleModelByBrandIdAsync(int id);
        Task<ErrorData> RatingAsync(int entityTypeRatingId, int entityTypeId, int thumb);
    }
}
