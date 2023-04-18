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
                }
                else
                {
                    var assembly = typeof(App).Assembly;
                    var imageStream = assembly.GetManifestResourceStream("AutoOglasiSource.Resources.Images.uploadavatar.png");
                    var imageData = new byte[imageStream.Length];
                    imageStream.Read(imageData, 0, imageData.Length);
                    User.Avatar = imageData;
                    User.Email = "Guest";
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
        async void Logout()
        {
            SecureStorage.Remove("oauth_token");
            await Shell.Current.GoToAsync(nameof(UserPage));
        }


        public async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("User Attempt Result", message, "OK");
        }
    }
}
