using AutoOglasiSource.Model.Account;
using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;

namespace AutoOglasiSource.ViewModel
{
    [QueryProperty(nameof(Advertisement), "Advertisement")]
    public partial class AdvertisementDetailsViewModel : BaseViewModel
    {
        IRestDataService _restDataService;
        IConnectivity _connectivity;
        public AdvertisementDetailsViewModel(IRestDataService restDataService, IConnectivity connectivity)
        {
            IsBusy = true;
            IsBusy = false;
            _restDataService = restDataService;
            _connectivity = connectivity;
        }

        [ObservableProperty]
        AdvertisementModel advertisement;

        [ObservableProperty]
        UserModel user;

          [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        bool isVisible;

     

        [RelayCommand]
        public async Task OnLabelTappedAsync()
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
                IsVisible = !IsVisible;

                 if(IsVisible == true)
                {
                    User = await _restDataService.GetUserByIdAsync(Advertisement.ApplicationUserId);
                  
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get user: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public async Task LikeAsync()
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
                    var userIdResponse = (Convert.ToInt32(userId));
                    if (userIdResponse != Advertisement.ApplicationUserId)
                    {
                        var response = await _restDataService.RatingAsync(1, Advertisement.ApplicationUserId, 1);

                        User = await _restDataService.GetUserByIdAsync(Advertisement.ApplicationUserId);
                    }
                }



            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get user: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public async Task DislikeAsync()
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
                    var userIdResponse = (Convert.ToInt32(userId));
                    if (userIdResponse != Advertisement.ApplicationUserId)
                    {
                        var response = await _restDataService.RatingAsync(1, Advertisement.ApplicationUserId, -1);

                        User = await _restDataService.GetUserByIdAsync(Advertisement.ApplicationUserId);
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get user: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

    }  
   
}

