using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AutoOglasiSource.ViewModel
{
    public partial class AdvertisementsViewModel : BaseViewModel
    {
        public ObservableCollection<AdvertisementListModel> Advertisements { get; } = new();
        IRestDataService _restDataService;
        IConnectivity _connectivity;
        IPopupNavigation _popupNavigation;
        public AdvertisementsViewModel(IRestDataService restDataService, IConnectivity connectivity, IPopupNavigation popupNavigation)
        {
           
            _restDataService = restDataService;
            _connectivity = connectivity;
            _popupNavigation = popupNavigation;
            Task.Run(async () => await GetAdvertisementsAsync(SpecParams));
            SpecParams.PageIndex = 1;
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
        async Task GetAdvertisementDetailAsync(AdvertisementListModel advertisementList)
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

                await Shell.Current.GoToAsync(nameof(AdvertisementDetailsPage), true, new Dictionary<string, object>
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
        public async Task GetAdvertisementsAsync(AdvertisementSpecParams specParams)
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
            
                if(specParams != null) SpecParams = specParams;
                SpecParams.PageIndex = 1;
                var advertisements = await _restDataService.GetAdvertisementListAsync(SpecParams);

                if (Advertisements.Count != 0)
                    Advertisements.Clear();

                foreach (var advertisement in advertisements)
                    Advertisements.Add(advertisement);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get ads: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public async Task GetMoreAdvertisementsAsync(AdvertisementSpecParams specParams)
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
              
                
                if (specParams != null) SpecParams = specParams;
                SpecParams.PageIndex++;
                
                var advertisements = await _restDataService.GetAdvertisementListAsync(SpecParams);

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

        [RelayCommand]
        void GoToFilterPage()
        {
            var filterPage = new FilterPopupPage(this);
            _popupNavigation.PushAsync(filterPage);
        }
    }
}
