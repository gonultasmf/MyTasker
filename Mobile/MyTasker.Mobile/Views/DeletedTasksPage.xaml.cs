using MyTasker.Mobile.ViewModels;

namespace MyTasker.Mobile.Views;

public partial class DeletedTasksPage : ContentPage
{
	public DeletedTasksPage(DeletedTasksViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}