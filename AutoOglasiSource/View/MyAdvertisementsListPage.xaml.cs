using AutoOglasiSource.ViewModel;

namespace AutoOglasiSource.View;

public partial class MyAdvertisementsListPage : ContentPage
{
	public MyAdvertisementsListPage(MyAdvertisementsListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}