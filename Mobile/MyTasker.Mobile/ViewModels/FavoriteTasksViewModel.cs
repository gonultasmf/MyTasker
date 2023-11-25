using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Models;
using MyTasker.Mobile.Views;

namespace MyTasker.Mobile.ViewModels;

public partial class FavoriteTasksViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<TaskModel> _favorites;

    public FavoriteTasksViewModel()
    {
        GetFavoriteTasks();
    }

    private async void GetFavoriteTasks()
    {
        Favorites = await HttpClientHelper.SendAsync<List<TaskModel>>(App.BaseUrl + "/Task/GetFavorites", HttpMethod.Get);
    }

    [RelayCommand]
    public async Task GotoDetailTaskPage(int id)
    {
        DetailTaskViewModel.Id = id;

        await Shell.Current.GoToAsync(nameof(DetailTaskPage));
    }
}
