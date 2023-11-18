using CommunityToolkit.Mvvm.ComponentModel;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Models;

namespace MyTasker.Mobile.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private SettingsModel _settings;

    [ObservableProperty]
    private List<TaskModel> _tasks;

    public HomeViewModel()
    {
        GetHomePageDetails();
    }

    private async void GetHomePageDetails()
    {
        Tasks = await HttpClientHelper.SendAsync<List<TaskModel>>(App.BaseUrl + "/Task/GetAllWithToday", HttpMethod.Get);
        Settings = await HttpClientHelper.SendAsync<SettingsModel>(App.BaseUrl + "/Settings", HttpMethod.Get);
    }
}
