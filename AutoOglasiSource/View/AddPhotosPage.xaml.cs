using AutoOglasiSource.ViewModel;

namespace AutoOglasiSource.View;

public partial class AddPhotosPage : ContentPage
{
	public AddPhotosPage(AddPhotosViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        NavigationPage.SetHasBackButton(this, false);
    }
}