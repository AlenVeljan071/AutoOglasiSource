using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace AutoOglasiSource.ViewModel
{
    [QueryProperty(nameof(Id), "Id")]
    public partial class AddPhotosViewModel : BaseViewModel
    {

        IRestDataService _restDataService;
        IConnectivity _connectivity;

        public AddPhotosViewModel(IRestDataService restDataService, IConnectivity connectivity)
        {
            _restDataService = restDataService;
            _connectivity = connectivity;
        }

        [ObservableProperty]
        int id;

        [ObservableProperty]
        AdvertisementModel advertisement = new();

        [ObservableProperty]
        AdsImage image = new();

        [ObservableProperty]
        byte[] photoBytes;

        [ObservableProperty]
        bool isRefreshing;

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

                Image.AdvertisementId = Id;
                Image.Image = Convert.ToBase64String(photoBytes);

                if (IsBusy)
                    return;

                try
                {
                    if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        await DisplayLoginMessage("No connectivity!,Please check internet and try again.");
                        return;
                    }

                    IsBusy = true;

                    var response = await _restDataService.AddPhotoToAdvertisementAsync(Image);
                    if (response.Success)
                    {
                        await DisplayLoginMessage("Photo succcesfuly uploaded, upload another if you want.");
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
        }

        [RelayCommand]
        async Task GetAdvertisementAsync()
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
                PhotoBytes = null;
                image = null;
                
                Advertisement = await _restDataService.GetAdvertisementByIdAsync(Id);

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
       public async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Photo upload Result", message, "OK");
        }
    }
}
