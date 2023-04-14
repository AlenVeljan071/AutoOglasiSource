using AutoOglasiSource.Model;
using AutoOglasiSource.Model.Account;
using AutoOglasiSource.Resources.Languages;
using AutoOglasiSource.Services;
using AutoOglasiSource.Translate;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace AutoOglasiSource.ViewModel
{
    public partial class SignupViewModel : BaseViewModel
    {
        private readonly IRestDataService _restDataService;
        IConnectivity _connectivity;

        public SignupViewModel(IRestDataService restDataService, IConnectivity connectivity)
        {
            _restDataService = restDataService;
            _connectivity = connectivity;
        }

        [ObservableProperty]
        SignupRequest userReq = new SignupRequest();

        [ObservableProperty]
        AddressEntity addressReq = new AddressEntity();

        [ObservableProperty]
        string avatarbase64string;


        [ObservableProperty]
        LoginModel user;


        [RelayCommand]
        async Task SignupAsync()
        {
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                userReq.Avatar = Avatarbase64string;
                userReq.Address = AddressReq;
               
                var loginResult = await _restDataService.SignupAsync(userReq);
                User = loginResult;
                if (loginResult.Success)
                {
                   
                    //await Shell.Current.GoToAsync(nameof(ConfirmPage), true, new Dictionary<string, object>
                    //{
                    //  { "User" , User}
                    //});
                }

                
                else
                {
                    await Shell.Current.DisplayAlert("Error", loginResult.ErrorMessage, "OK");
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
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

        [RelayCommand]
        async Task TranslateToBiH()
        {
            var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("bs", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("bs-Latn-BA");

            LocalizationResourceManager.Instance.SetCulture(switchToCulture);
            await Task.Delay(12);
        }
        [RelayCommand]
        async Task TranslateToEn()
        {
            var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("en", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("en-US");

            LocalizationResourceManager.Instance.SetCulture(switchToCulture);
            await Task.Delay(12);
        }
    }
}
