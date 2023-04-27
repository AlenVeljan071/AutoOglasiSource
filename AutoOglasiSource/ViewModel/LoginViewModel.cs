using AutoOglasiSource.Model.Account;
using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoOglasiSource.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        IRestDataService _restDataService;
        IConnectivity _connectivity;
        public LoginViewModel(IRestDataService restDataService, IConnectivity connectivity)
        {
            _restDataService = restDataService;
            _connectivity = connectivity;
        }

        [ObservableProperty]
        string email;

       
        [ObservableProperty]
        string password;

        [ObservableProperty]
        UserModel user;

        [ObservableProperty]
        bool isRefreshing;


        [RelayCommand]
        public async Task LoginAsync()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await DisplayLoginMessage("Invalid Login Attempt");
            }

            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayLoginMessage("No connectivity!,Please check internet and try again.");
                    return;
                }

                IsBusy = true;

                var response = await _restDataService.Login(Email, Password);
                if (response.Success)
                {

                    await Shell.Current.GoToAsync(nameof(RegisterPage));
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

        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Login Attempt Result", message, "OK");
            Password = string.Empty;
        }
    }
}
