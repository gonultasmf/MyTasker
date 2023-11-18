using MyTasker.Mobile.ViewModels;

namespace MyTasker.Mobile.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}