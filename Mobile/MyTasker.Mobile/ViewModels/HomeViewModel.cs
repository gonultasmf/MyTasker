using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Models;

namespace MyTasker.Mobile.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private SettingsModel _settings;

    [ObservableProperty]
    private List<TaskModel> _tasks;

    [ObservableProperty]
    private string _userText;

    public HomeViewModel()
    {
        GetHomePageDetails();
    }

    [RelayCommand]
    public async Task Search(string searchText)
    {
        var tasks = await HttpClientHelper.SendAsync<List<TaskModel>>(App.BaseUrl + $"/Task/GetAllSearch/{searchText}", HttpMethod.Get);
    }

    private async void GetHomePageDetails()
    {
        Tasks = await HttpClientHelper.SendAsync<List<TaskModel>>(App.BaseUrl + "/Task/GetAllWithToday", HttpMethod.Get);
        Settings = await HttpClientHelper.SendAsync<SettingsModel>(App.BaseUrl + "/Settings", HttpMethod.Get);
        UserText = GetUserText();
    }


    private string GetUserText()
    {
        var time = DateTime.Now.Hour;

        if (time >= 5 && time < 12)
            return $"Good Morning, {Settings.UserName}!";
        else if (time >= 12 && time < 18)
            return $"Good Afternoon, {Settings.UserName}!";
        else
            return $"Good Night, {Settings.UserName}!";
    }
}
