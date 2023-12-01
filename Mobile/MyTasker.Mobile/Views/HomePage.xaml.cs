using MyTasker.Mobile.ViewModels;
using Plugin.LocalNotification;

namespace MyTasker.Mobile.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
    }

    private async void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
    {
        if (e.IsTapped)
        {
            DetailTaskViewModel.Id = e.Request.NotificationId;

            await Shell.Current.GoToAsync(nameof(DetailTaskPage));
        }
    }
}