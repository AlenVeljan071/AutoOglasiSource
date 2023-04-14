using AutoOglasiSource.ViewModel;

namespace AutoOglasiSource.View;

public partial class MainPage : ContentPage
{

	public MainPage(AdvertisementsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
   
}

