using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace AutoOglasiSource.ViewModel
{

    public partial class MyAdvertisementsListViewModel : BaseViewModel
    {
        public ObservableCollection<AdvertisementListModel> Advertisements { get; } = new();
        IRestDataService _restDataService;
        IConnectivity _connectivity;
        public MyAdvertisementsListViewModel(IRestDataService restDataService, IConnectivity connectivity)
        {
            _restDataService = restDataService;
            _connectivity = connectivity;
            Task.Run(async () => await GetMyAdvertisementsAsync(SpecParams));
        }

        [ObservableProperty]
        AdvertisementModel advertisement = new();
        [ObservableProperty]
        AdvertisementListModel advertisementList;

        [ObservableProperty]
        AdvertisementSpecParams specParams = new();

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetMyAdvertisementDetailAsync(AdvertisementListModel advertisementList)
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

                Advertisement = await _restDataService.GetAdvertisementByIdAsync(advertisementList.Id);

                await Shell.Current.GoToAsync(nameof(MyAdvertisementPage), true, new Dictionary<string, object>
                {
                  { "Advertisement", Advertisement}
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get advertisements: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }


        [RelayCommand]
        public async Task GetMyAdvertisementsAsync(AdvertisementSpecParams specParams)
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
                    SpecParams.ApplicationUserId = (Convert.ToInt32(userId));
                }
                if (specParams != null) SpecParams = specParams;
                var advertisements = await _restDataService.GetAdvertisementListAsync(SpecParams);

                if (Advertisements.Count != 0)
                    Advertisements.Clear();

                foreach (var advertisement in advertisements)
                    Advertisements.Add(advertisement);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
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
