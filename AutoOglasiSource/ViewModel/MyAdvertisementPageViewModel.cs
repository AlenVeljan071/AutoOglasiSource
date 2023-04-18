using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Responses;
using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;


namespace AutoOglasiSource.ViewModel
{
    [QueryProperty(nameof(Advertisement), "Advertisement")]
    public partial class MyAdvertisementPageViewModel : BaseViewModel
    {
        IRestDataService _restDataService;
        IPopupNavigation _popupNavigation;
        public MyAdvertisementPageViewModel(IRestDataService restDataService, IPopupNavigation popupNavigation)
        {
            _restDataService = restDataService;
            _popupNavigation = popupNavigation;
            IsDeleteButtonVisible = false;
        }

        [ObservableProperty]
        int id;

        [ObservableProperty]
        AdvertisementModel advertisement;

        [ObservableProperty]
        ImageResponse selectedImageresponse = new();

        [ObservableProperty]
        bool _isDeleteButtonVisible = false;

        [ObservableProperty]
        bool _isSelected = false;
       
        public async Task LoadAdvertisementData()
        {
            var advertisement = await _restDataService.GetAdvertisementByIdAsync(Advertisement.Id);
            Advertisement = advertisement;
        }

        [RelayCommand]
        void GoToUpdatePricePage()
        {
              var filterPage = new UpdatePricePopupPage(this, _restDataService);
              _popupNavigation.PushAsync(filterPage);
        }

        [RelayCommand]
        async void DeleteAdvertisement()
        {
            bool isUserConfirmed = await Shell.Current.DisplayAlert("Delete", "Are you sure!", "OK", "Cancel");
            if (isUserConfirmed)
            {
                if (IsBusy)
                    return;
                try
                {

                    IsBusy = true;

                    var response =  await _restDataService.DeleteAdvertisementAsync(Advertisement.Id);
                    if (response.Error == "Deleted")
                    {
                        await DisplayLoginMessage(response.Error);
                        await Shell.Current.GoToAsync(nameof(MyAdvertisementsListPage));
                    }
                    else
                    {
                        await DisplayLoginMessage(response.Error);
                    }
                }

                catch (Exception ex)
                {
                    await DisplayLoginMessage(ex.Message);
                }
                finally
                {
                    IsBusy = false;
                }
              
            }
           
        }

         private async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Register Attempt Result", message, "OK");

        }

        [RelayCommand]
         void SelectedImage(ImageResponse image)
        {

            image.IsSelected = !image.IsSelected;
            IsDeleteButtonVisible = image.IsSelected; 
            SelectedImageresponse = image;
        }

        [RelayCommand]
        async void AddImage()
        {
            Id = Advertisement.Id;
            await Shell.Current.GoToAsync(nameof(AddPhotosPage), true, new Dictionary<string, object>
                {
                  { "Id", Id}
                });
        }
        [RelayCommand]
        async void DeleteImage()
        {
            bool isUserConfirmed = await Shell.Current.DisplayAlert("Delete", "Are you sure!", "OK", "Cancel");
            if (isUserConfirmed)
            {
                if (IsBusy)
                    return;
                try
                {

                    IsBusy = true;

                    var response = await _restDataService.DeletePhotoAsync(SelectedImageresponse.Id);
                    if (response.Error == "Deleted")
                    {
                        await DisplayLoginMessage(response.Error);
                    }
                    else
                    {
                        await DisplayLoginMessage(response.Error);
                    }
                }

                catch (Exception ex)
                {
                    await DisplayLoginMessage(ex.Message);
                }
                finally
                {
                    IsBusy = false;
                }

            }
        }
    }
    
}
