using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Models;
using MyTasker.Mobile.Views;

namespace MyTasker.Mobile.ViewModels;

public partial class ListTaskWithStatusViewModel : BaseViewModel
{
    public static int StatusValue { get; set; }

    [ObservableProperty]
    private List<TaskModel> _tasks;

    public ListTaskWithStatusViewModel()
    {
        GetTasks();
    }

    private async void GetTasks()
    {
        Tasks = await HttpClientHelper.SendAsync<List<TaskModel>>(App.BaseUrl + "/Task/GetAll/" + StatusValue, HttpMethod.Get);
    }

    [RelayCommand]
    public async Task GotoDetailTaskPage(int id)
    {
        DetailTaskViewModel.Id = id;

        await Shell.Current.GoToAsync(nameof(DetailTaskPage));
    }
}
