using AutoOglasiSource.Model.Advertisement;
using AutoOglasiSource.ViewModel;
using Mopups.Services;

namespace AutoOglasiSource.View;

public partial class FilterPopupPage 
{
    public FilterPopupPage(AdvertisementsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var specParams = new AdvertisementSpecParams
        {
            PriceFrom = decimal.TryParse(PriceFromEntry.Text, out var priceFrom) ? priceFrom : default(decimal?),
            PriceTo = decimal.TryParse(PriceToEntry.Text, out var priceTo) ? priceTo : default(decimal?),
            MileageFrom = decimal.TryParse(MileageFromEntry.Text, out var mileageFrom) ? mileageFrom : default(decimal?),
            MileageTo = decimal.TryParse(MileageToEntry.Text, out var mileageTo) ? mileageTo : default(decimal?),
        };

        await ((AdvertisementsViewModel)BindingContext).GetAdvertisementsAsync(specParams);
        await MopupService.Instance.PopAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}

