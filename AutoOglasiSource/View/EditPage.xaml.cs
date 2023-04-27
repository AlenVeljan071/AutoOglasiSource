using AutoOglasiSource.ViewModel;

namespace AutoOglasiSource.View;

public partial class EditPage : ContentPage
{
	public EditPage(EditAdvertisementVIewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}