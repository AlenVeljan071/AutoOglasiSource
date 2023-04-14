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
        Task<List<AdvertisementListModel>> GetAdvertisementListAsync(AdvertisementSpecParams specParams);
        Task<AdvertisementModel> GetAdvertisementByIdAsync(int id);
    }
}
