using AutoOglasiSource.ViewModel;

namespace AutoOglasiSource.View;

public partial class AddPage : ContentPage
{
	public AddPage(AddAdvertisementViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}