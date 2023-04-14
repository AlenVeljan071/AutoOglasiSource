using AutoOglasiSource.Model.Advertisement;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AutoOglasiSource.ViewModel
{
    [QueryProperty(nameof(Advertisement), "Advertisement")]
    public partial class AdvertisementDetailsViewModel : BaseViewModel
    {
        public AdvertisementDetailsViewModel()
        {
            IsBusy = true;
            IsBusy = false;
        }

        [ObservableProperty]
        AdvertisementModel advertisement;

        [ObservableProperty]
        bool isRefreshing;
    }
}

