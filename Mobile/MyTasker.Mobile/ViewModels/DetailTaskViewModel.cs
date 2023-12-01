using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Enums;
using MyTasker.Core.Models;
using Plugin.LocalNotification;
using System.Text.Json;

namespace MyTasker.Mobile.ViewModels;

public partial class DetailTaskViewModel : BaseViewModel
{
    public static int Id { get; set; }

    [ObservableProperty]
    private ImageSource _favorite;

    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    private TimeSpan _time;

    [ObservableProperty]
    private TaskModel _model;

    private bool isFavorite;

    private MyTaskStatus _status;


    public DetailTaskViewModel()
    {
        GetModelDetails();
    }

    private async void GetModelDetails()
    {
        Model = await HttpClientHelper.SendAsync<TaskModel>(App.BaseUrl + "/Task/" + Id, HttpMethod.Get);
        Date = Model.TaskDate.Date;
        Time = Model.TaskDate.TimeOfDay;
        isFavorite = Model.IsFavourite;
        SetImageFavorite(isFavorite);
    }

    [RelayCommand]
    public async Task Save()
    {
        string message = string.Empty;

        Model.TaskDate = DateTime.Parse(Date.ToShortDateString() + " " + Time.ToString());
        Model.IsFavourite = isFavorite;
        Model.Status = _status;

        var control = Model.TryParseToJson(out string jsonModel);
        if (!control)
            jsonModel = JsonSerializer.Serialize(Model);

        var result = await HttpClientHelper.SendAsync<string>(App.BaseUrl + "/Task", HttpMethod.Put, jsonModel, ClientContentType.Json);
        if (result == null || !bool.Parse(result))
            message = "Task don't save!";
        else
        {
            message = "Task saved!";

            var request = new NotificationRequest
            {
                NotificationId = Model.Id,
                Title = "My Tasker",
                Subtitle = Model.Title,
                Description = Model.Content,
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = Model.TaskDate
                }
            };
            LocalNotificationCenter.Current.Show(request);
        }

        SemanticScreenReader.Announce(message);
        var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 20);

        await toast.Show();

        App.Current.MainPage = new AppShell();
    }

    [RelayCommand]
    public async Task Remove()
    {
        string message = string.Empty;

        Model.IsActive = false;

        var control = Model.TryParseToJson(out string jsonModel);
        if (!control)
            jsonModel = JsonSerializer.Serialize(Model);

        var result = await HttpClientHelper.SendAsync<string>(App.BaseUrl + "/Task", HttpMethod.Put, jsonModel, ClientContentType.Json);
        if (result == null || !bool.Parse(result))
            message = "Task don't move to Trash!";
        else
            message = "Task move to Trash!";

        SemanticScreenReader.Announce(message);
        var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 25);

        await toast.Show();

        App.Current.MainPage = new AppShell();
    }

    [RelayCommand]
    public void SetColor(object model)
    {
        Model.Color = model?.ToString();
    }

    [RelayCommand]
    public void SetFavorite()
    {
        isFavorite = !isFavorite;
        SetImageFavorite(isFavorite);
    }

    [RelayCommand]
    public void SetStatus(object status)
    {
        if (Enum.TryParse(status.ToString(), out MyTaskStatus result))
            _status = result;
    }

    private void SetImageFavorite(bool control)
    {
        if (control)
            Favorite = "favorite_true.png";
        else
            Favorite = "favorite_false.png";
    }
}
