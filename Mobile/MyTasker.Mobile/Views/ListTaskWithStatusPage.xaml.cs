using MyTasker.Mobile.ViewModels;

namespace MyTasker.Mobile.Views;

public partial class ListTaskWithStatusPage : ContentPage
{
	public ListTaskWithStatusPage(ListTaskWithStatusViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}