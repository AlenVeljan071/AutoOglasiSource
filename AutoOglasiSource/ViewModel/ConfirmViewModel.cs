using AutoOglasiSource.Model.Account;
using AutoOglasiSource.Resources.Languages;
using AutoOglasiSource.Services;
using AutoOglasiSource.Translate;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace AutoOglasiSource.ViewModel
{
    [QueryProperty(nameof(User), "User")]
    public partial class ConfirmViewModel : BaseViewModel
    {
        private readonly IRestDataService _restDataService;
        IConnectivity _connectivity;

        public ConfirmViewModel(IRestDataService restDataService, IConnectivity connectivity)
        {
            _restDataService = restDataService;
            _connectivity = connectivity;
        }

        [ObservableProperty]
        LoginModel user = new();

        [ObservableProperty]
        string code;
        [ObservableProperty]
        string code1;
        [ObservableProperty]
        string code2;
        [ObservableProperty]
        string code3;
        [ObservableProperty]
        string code4;

        

        [RelayCommand]
        async Task VerifyUserAsync()
        {
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                Code = Code1 + Code2 + Code3 + Code4;
                var loginResult = await _restDataService.VerifyUserAsync(User.Email, Code);
                if (loginResult.Success)
                {
                    await Shell.Current.GoToAsync("//MainPage");
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

        [RelayCommand]
        async Task ResendVerifyCodeAsync()
        {
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                var loginResult = await _restDataService.ResendVerifyCodeAsync(User.Email);
                if (loginResult.Success)
                {
                    await Shell.Current.DisplayAlert("Code sending to: ", loginResult.Email, "OK");
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

        [RelayCommand]
        async Task TranslateToBiH()
        {
            var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("bs", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("bs-Latn-BA");

            LocalizationResourceManager.Instance.SetCulture(switchToCulture);
        }
        [RelayCommand]
        async Task TranslateToEn()
        {
            var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("en", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("en-US");

            LocalizationResourceManager.Instance.SetCulture(switchToCulture);
        }
    }
}
