using AutoOglasiSource.Model.Account;
using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IdentityModel.Tokens.Jwt;

namespace AutoOglasiSource.ViewModel
{
    public partial class UserViewModel : BaseViewModel
    {
        IRestDataService _restDataService;
        IConnectivity _connectivity;
        
        public UserViewModel(IRestDataService restDataService, IConnectivity connectivity)
        {
            _restDataService = restDataService;
            _connectivity = connectivity;
            Task.Run(async () => await GetUser());
           
        }

        [ObservableProperty]
        AdvertisementSpecParams specParams = new();


        [ObservableProperty]
        int id;

        [ObservableProperty]
        string userEmail;

        [ObservableProperty]
        UserModel user = new();

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetUser()
        {
            if (IsBusy)
                return;
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");
                if (oauthToken != null)
                {
                    var jsonToken = new JwtSecurityTokenHandler().ReadToken(oauthToken) as JwtSecurityToken;
                    var userId = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals("UserId"))?.Value;
                    User = await _restDataService.GetUserByIdAsync(Convert.ToInt32(userId));
                    UserEmail = User.Email;
                }
                else
                {
                    UserEmail = "Guest";
                }
               
                
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }

        }

        [RelayCommand]
        async Task GoToLogin()
        {
           
           await Shell.Current.GoToAsync(nameof(LoginPage));
          
        }
        [RelayCommand]
        async Task GoToRegister()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        [RelayCommand]
        async Task GoToMyAdvertisements()
        {
            string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");
            if (oauthToken != null)
            {
               await Shell.Current.GoToAsync(nameof(MyAdvertisementsListPage));
            }
            else
            {
                await DisplayLoginMessage("Please login");
            }
        }

        [RelayCommand]
        void Logout()
        {
            SecureStorage.Remove("oauth_token");

            User = null;
            IsRefreshing = true;
            SpecParams = null;
            Id = 0;
        }
        public async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("User Attempt Result", message, "OK");
        }
    }
}
