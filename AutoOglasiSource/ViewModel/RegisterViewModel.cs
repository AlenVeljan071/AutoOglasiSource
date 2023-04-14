using AutoOglasiSource.Model;
using AutoOglasiSource.Model.Account;
using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoOglasiSource.ViewModel
{
    public partial class RegisterViewModel : BaseViewModel
    {
        IRestDataService _restDataService;
        IConnectivity _connectivity;
        public RegisterViewModel(IRestDataService restDataService, IConnectivity connectivity)
        {
            _restDataService = restDataService;
            _connectivity = connectivity;
        }

        [ObservableProperty]
        SignupRequest user = new();
       
        [ObservableProperty]
        AddressEntity addressReq = new ();

        [ObservableProperty]
        string avatarbase64string;

        [ObservableProperty]
        bool isRefreshing;


        [RelayCommand]
        public async Task RegisterAsync()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.FirstName) || string.IsNullOrWhiteSpace(User.LastName) || string.IsNullOrWhiteSpace(User.Password) || string.IsNullOrWhiteSpace(User.Phone) || string.IsNullOrWhiteSpace(AddressReq.Address) || string.IsNullOrWhiteSpace(AddressReq.City) || string.IsNullOrWhiteSpace(AddressReq.Country))
            {
                await DisplayLoginMessage("Invalid Register Attempt");
            }

            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayLoginMessage("No connectivity!,Please check internet and try again.");
                    return;
                }

                IsBusy = true;
                User.Address = AddressReq;
                User.Avatar = Avatarbase64string;
                var response = await _restDataService.SignupAsync(User);
                if (response.Success)
                {
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                }
                else
                {
                    await DisplayLoginMessage(response.ErrorMessage);
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

        [ObservableProperty]
        byte[] photoBytes;

        [RelayCommand]
        async Task ImageUpload()
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Select a photo"
            });

            if (photo != null)
            {
                // Convert the photo stream to a byte array
                byte[] photoBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await photo.OpenReadAsync().Result.CopyToAsync(memoryStream);
                    photoBytes = memoryStream.ToArray();
                    PhotoBytes = photoBytes;
                }


                Avatarbase64string = Convert.ToBase64String(photoBytes);


            }
        }

        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Register Attempt Result", message, "OK");
        }
    }
}
