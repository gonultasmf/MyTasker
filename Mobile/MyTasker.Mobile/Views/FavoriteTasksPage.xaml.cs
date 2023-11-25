using MyTasker.Mobile.ViewModels;

namespace MyTasker.Mobile.Views;

public partial class FavoriteTasksPage : ContentPage
{
	public FavoriteTasksPage(FavoriteTasksViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}