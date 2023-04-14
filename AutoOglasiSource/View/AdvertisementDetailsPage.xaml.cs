using AutoOglasiSource.ViewModel;

namespace AutoOglasiSource.View;

public partial class AdvertisementDetailsPage : ContentPage
{
    public AdvertisementDetailsPage(AdvertisementDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}