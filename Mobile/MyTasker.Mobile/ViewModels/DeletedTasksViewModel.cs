using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Models;
using MyTasker.Mobile.Views;
using MyTasker.Mobile.Extensions;

namespace MyTasker.Mobile.ViewModels;

public partial class DeletedTasksViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<TaskModel> _deletedTasks;

    public DeletedTasksViewModel()
    {
        GetDeletedTasks();
    }

    private async void GetDeletedTasks()
    {
        DeletedTasks = await HttpClientHelper.SendAsync<List<TaskModel>>(App.BaseUrl + "/Task/GetTrash", HttpMethod.Get);
    }

    [RelayCommand]
    public async Task GotoDetailTaskPage(int id)
    {
        DetailTaskViewModel.Id = id;

        await Shell.Current.GoToAsync(nameof(DetailTaskPage));
    }

    [RelayCommand]
    public async Task Remove(TaskModel model)
    {
        await Task.Run(async () =>
        {
            string message = string.Empty;

            var result = await HttpClientHelper.SendAsync<string>(App.BaseUrl + "/Task/" + model.Id, HttpMethod.Delete);
            if (result == null || !bool.Parse(result))
                message = "Task don't deleted!";
            else
                message = "Task deleted!";

            SemanticScreenReader.Announce(message);
            var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 20);

            await toast.Show();
        }).ContinueInMainThreadWith(GetDeletedTasks);
    }

    [RelayCommand]
    public async Task Restore(TaskModel model)
    {
        await Task.Run(async () =>
        {
            string message = string.Empty;

            var result = await HttpClientHelper.SendAsync<string>(App.BaseUrl + "/Task/RestoreTask/" + model.Id, HttpMethod.Get);
            if (result == null || !bool.Parse(result))
                message = "Task don't restored!";
            else
                message = "Task restored!";

            SemanticScreenReader.Announce(message);
            var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 20);

            await toast.Show();
        }).ContinueInMainThreadWith(GetDeletedTasks);
    }
}
