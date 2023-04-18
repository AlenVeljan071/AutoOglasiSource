using AutoOglasiSource.Services;
using AutoOglasiSource.ViewModel;
using Mopups.Services;

namespace AutoOglasiSource.View;

public partial class UpdatePricePopupPage 
{
    IRestDataService _restDataService;
    public UpdatePricePopupPage(MyAdvertisementPageViewModel viewModel, IRestDataService restDataService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _restDataService = restDataService;
    }

    private async void UpdatePrice_Clicked(object sender, EventArgs e)
    {

        var viewModel = (MyAdvertisementPageViewModel)BindingContext;
        var price = decimal.TryParse(PriceEntry.Text, out var priceAdd) ? priceAdd : default(decimal?);
        var advertisementId = viewModel.Advertisement.Id;

        await _restDataService.UpdatePriceAsync(advertisementId, priceAdd);
        await viewModel.LoadAdvertisementData();
        await MopupService.Instance.PopAsync();
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }

   
}