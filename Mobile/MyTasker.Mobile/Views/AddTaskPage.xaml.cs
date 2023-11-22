using MyTasker.Mobile.ViewModels;

namespace MyTasker.Mobile.Views;

public partial class AddTaskPage : ContentPage
{
	public AddTaskPage(AddTaskViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        DateField.Date = DateTime.Now;
        DateField.MinimumDate = DateTime.Now;
		TimeField.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
		ColorButton.SelectedIndex = 0;
	}
}