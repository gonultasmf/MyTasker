using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Models;
using MyTasker.Mobile.Views;

namespace MyTasker.Mobile.ViewModels;

public partial class ListTaskViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<TaskModel> _tasks;

    public ListTaskViewModel()
    {
        GetTasks(DateTime.Now);
    }

    [RelayCommand]
    public async Task GotoDetailTaskPage(int id)
    {
        DetailTaskViewModel.Id = id;

        await Shell.Current.GoToAsync(nameof(DetailTaskPage));
    }

    [RelayCommand]
    public async void GetTasks(DateTime date)
    {
        Tasks = await HttpClientHelper.SendAsync<List<TaskModel>>(App.BaseUrl + "/Task/GetAllWithDay?date=" + date.ToShortDateString(), HttpMethod.Get);
    }
}
