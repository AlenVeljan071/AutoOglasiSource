using AutoOglasiSource.ViewModel;

namespace AutoOglasiSource.View;

public partial class UserPage : ContentPage
{
	public UserPage(UserViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

   











}