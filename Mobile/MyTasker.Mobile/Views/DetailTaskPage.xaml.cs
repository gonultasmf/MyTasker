using MyTasker.Mobile.ViewModels;

namespace MyTasker.Mobile.Views;

public partial class DetailTaskPage : ContentPage
{
	public DetailTaskPage(DetailTaskViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}