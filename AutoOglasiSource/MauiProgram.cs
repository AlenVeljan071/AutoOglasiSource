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
        builder.Services.AddTransient<AdvertisementDetailsPage>();
        builder.Services.AddTransient<AddPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<UserPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<AddPhotosPage>();
        builder.Services.AddTransient<MyAdvertisementsListPage>();
        builder.Services.AddTransient<MyAdvertisementPage>();
        builder.Services.AddTransient<EditPage>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<AdvertisementsViewModel>();
        builder.Services.AddTransient<AdvertisementDetailsViewModel>();
        builder.Services.AddTransient<UserViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<AddAdvertisementViewModel>();
        builder.Services.AddTransient<AddPhotosViewModel>();
        builder.Services.AddTransient<MyAdvertisementsListViewModel>();
        builder.Services.AddTransient<MyAdvertisementPageViewModel>();
        builder.Services.AddTransient<EditAdvertisementVIewModel>();



        return builder.Build();
	}
}
