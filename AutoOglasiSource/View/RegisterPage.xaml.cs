using AutoOglasiSource.ViewModel;

namespace AutoOglasiSource.View;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }

   
}
