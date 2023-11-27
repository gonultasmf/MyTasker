using MyTasker.Mobile.Controls;
using MyTasker.Mobile.ViewModels;

namespace MyTasker.Mobile.Views;

public partial class ListTaskPage : ContentPage
{
	public ListTaskPage(ListTaskViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void CalendarView_Loaded(object sender, EventArgs e)
    {
		var model = sender as CalendarView;
		model.collView.ScrollTo(model.SelectedDate.Date.Day);
    }
}