using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.View;

namespace AutoOglasiSource;

public partial class AppShell : Shell
{
  

    public AppShell()
	{
		InitializeComponent();
       
        Routing.RegisterRoute(nameof(AdvertisementDetailsPage), typeof(AdvertisementDetailsPage));
        Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
    }
}
