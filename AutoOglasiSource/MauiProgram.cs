using AutoOglasiSource.Services;
using AutoOglasiSource.View;
using AutoOglasiSource.ViewModel;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;

namespace AutoOglasiSource;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMopups()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IRestDataService, RestDataService>();
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);
       
		builder.Services.AddTransient<MainPage>();
        builder.Services.AddSingleton<AdvertisementDetailsPage>();
        builder.Services.AddSingleton<AddPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddTransient<UserPage>();
        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<AddPhotosPage>();
        builder.Services.AddSingleton<MyAdvertisementsListPage>();
        builder.Services.AddSingleton<MyAdvertisementPage>();

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddTransient<AdvertisementsViewModel>();
        builder.Services.AddSingleton<AdvertisementDetailsViewModel>();
        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddSingleton<RegisterViewModel>();
        builder.Services.AddSingleton<AddAdvertisementViewModel>();
        builder.Services.AddTransient<AddPhotosViewModel>();
        builder.Services.AddTransient<MyAdvertisementsListViewModel>();
        builder.Services.AddTransient<MyAdvertisementPageViewModel>();



        return builder.Build();
	}
}
