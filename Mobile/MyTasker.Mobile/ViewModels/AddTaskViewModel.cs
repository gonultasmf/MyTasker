using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Enums;
using MyTasker.Core.Models;
using System.Text.Json;

namespace MyTasker.Mobile.ViewModels;

public partial class AddTaskViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _content;

    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    private TimeSpan _time;

    [ObservableProperty]
    private string _color;



    [RelayCommand]
    public async Task Save()
    {
        string message = string.Empty;
        TaskModel model = new()
        {
            Color = Color,
            Content = Content,
            IsActive = true,
            IsFavourite = false,
            Status = MyTaskStatus.Backlog,
            Title = Title,
            TaskDate = DateTime.Parse(Date.ToShortDateString() + " " + Time.ToString())
        };

        var control = model.TryParseToJson(out string jsonModel);
        if (!control)
            jsonModel = JsonSerializer.Serialize(model);

        var result = await HttpClientHelper.SendAsync<string>(App.BaseUrl + "/Task", HttpMethod.Post, jsonModel, ClientContentType.Json);
        if (result == null || !bool.Parse(result))
            message = "Task don't save!";
        else
            message = "Task saved!";

        SemanticScreenReader.Announce(message);
        var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 20);

        await toast.Show();

        App.Current.MainPage = new AppShell();
    }

    [RelayCommand]
    public void SetColor(object model)
    {
        Color = model?.ToString();
    }
}
