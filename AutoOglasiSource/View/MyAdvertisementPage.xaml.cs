using AutoOglasiSource.Responses;
using AutoOglasiSource.ViewModel;

namespace AutoOglasiSource.View;

public partial class MyAdvertisementPage : ContentPage
{
	public MyAdvertisementPage(MyAdvertisementPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}