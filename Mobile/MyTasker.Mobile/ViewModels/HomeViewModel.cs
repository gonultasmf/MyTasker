using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Models;
using MyTasker.Mobile.Views;
using Plugin.LocalNotification;

namespace MyTasker.Mobile.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    private SettingsModel _settings;

    [ObservableProperty]
    private List<TaskModel> _tasks;

    [ObservableProperty]
    private string _userText;

    [ObservableProperty]
    private string _thisMonthTasksCountText;

    public HomeViewModel()
    {
        GetHomePageDetails();
    }

    [RelayCommand]
    public async Task Search(string searchText)
    {
        var tasks = await HttpClientHelper.SendAsync<List<TaskModel>>(App.BaseUrl + $"/Task/GetAllSearch/{searchText}", HttpMethod.Get);
    }

    [RelayCommand]
    public async Task GotoStatusPage(string status)
    {
        ListTaskWithStatusViewModel.StatusValue = int.Parse(status);

        await Shell.Current.GoToAsync(nameof(ListTaskWithStatusPage));
    }

    [RelayCommand]
    public async Task GotoDetailTaskPage(int id)
    {
        DetailTaskViewModel.Id = id;

        await Shell.Current.GoToAsync(nameof(DetailTaskPage));
    }

    [RelayCommand]
    public async Task GotoListTaskPage()
    {
        await Shell.Current.GoToAsync(nameof(ListTaskPage));
    }

    [RelayCommand]
    public async Task GotoAddTaskPage()
    {
        await Shell.Current.GoToAsync(nameof(AddTaskPage));
    }

    private async void GetHomePageDetails()
    {
        Tasks = await HttpClientHelper.SendAsync<List<TaskModel>>(App.BaseUrl + "/Task/GetAllWithToday", HttpMethod.Get);
        Settings = await HttpClientHelper.SendAsync<SettingsModel>(App.BaseUrl + "/Settings", HttpMethod.Get);
        UserText = GetUserText();
        ThisMonthTasksCountText = await GetThisMonthTasksCountAsync();
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


    private async Task<string> GetThisMonthTasksCountAsync()
    {
        var count = await HttpClientHelper.SendAsync<string>(App.BaseUrl + $"/Task/GetThisMonthTasksCount", HttpMethod.Get);

        return $"You have {count} tasks this month!";
    }
}
