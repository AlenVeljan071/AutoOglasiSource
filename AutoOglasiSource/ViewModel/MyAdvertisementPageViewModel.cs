using AutoOglasiSource.Model;
using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Model.Enums;
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
        IConnectivity _connectivity;
        public MyAdvertisementPageViewModel(IRestDataService restDataService, IPopupNavigation popupNavigation, IConnectivity connectivity)
        {
            _restDataService = restDataService;
            _popupNavigation = popupNavigation;
            IsDeleteButtonVisible = true;
            _connectivity = connectivity;
        }

        [ObservableProperty]
        int id;

        [ObservableProperty]
        AdvertisementModel advertisement;

        [ObservableProperty]
        AdvertisementEditModel advertisementEdit = new();

        [ObservableProperty]
        ImageResponse selectedImageresponse = new();

        [ObservableProperty]
        bool _isDeleteButtonVisible = false;

        [ObservableProperty]
        bool _isSelected = false;

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        public async Task LoadAdvertisementData()
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
                var advertisement = await _restDataService.GetAdvertisementByIdAsync(Advertisement.Id);
                Advertisement = advertisement;
            }
            catch (Exception ex)
            {
                await DisplayLoginMessage(ex.Message);
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
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
                        await LoadAdvertisementData();
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
                    IsRefreshing = false;
                }

            }
        }

        [RelayCommand]
        async void EditAdvertisement()
        {
            AdvertisementEdit = new AdvertisementEditModel
            {
                Id = Advertisement.Id,
                Mileage = Advertisement.Mileage,
                Abs = Advertisement.Abs.Value,
                Address = Advertisement.Address,
                Airbag = Advertisement.Airbag.Value,
                Alarm = Advertisement.Alarm.Value,
                AluminiumRims = Advertisement.AluminiumRims.Value,
                ParkAssist = Advertisement.ParkAssist.Value,
                CCM = Advertisement.CCM.Value,
                CentralLock = Advertisement.CentralLock.Value,
                Color = Advertisement.Color.Id,
                CruiseControl = Advertisement.CruiseControl.Value,
                Damaged = Advertisement.Damaged.Value,
                DigitalClimate = Advertisement.DigitalClimate.Value,
                DpfFilter = Advertisement.DpfFilter.Value,
                Drive = (Drive)Advertisement.Drive.Id,
                ElectricMirrors = Advertisement.ElectricMirrors.Value,
                ElectricWindows = Advertisement.ElectricWindows.Value,
                Emission = Advertisement.Emission.Id,
                Fuel = (Fuel)Advertisement.Fuel.Id,
                Gear = Advertisement.Gear.Id,
                Seat = Advertisement.Seat.Id,
                HorsePower = Advertisement.HorsePower,
                KW = Advertisement.KW,
                Name = Advertisement.Name,
                Navigation = Advertisement.Navigation.Value,
                Note = Advertisement.Note,
                ParkingSensors = (ParkingSensors)Advertisement.ParkingSensors.Id,
                Price = Advertisement.Price,
                Registered = Advertisement.Registered.Value,
                RemoteUnlocking = Advertisement.RemoteUnlocking.Value,
                SeatHeating = Advertisement.SeatHeating.Value,
                ServiceBook = Advertisement.ServiceBook.Value,
                Shifter = Advertisement.Shifter.Id,
                SteeringWheelControls = Advertisement.SteeringWheelControls.Value,
                Weight = Advertisement.Weight,
                Year = Advertisement.Year,

        };

          
            await Shell.Current.GoToAsync(nameof(EditPage), true, new Dictionary<string, object>
                {
                  { "AdvertisementEdit", AdvertisementEdit}
                });


        }

    }
    
}
