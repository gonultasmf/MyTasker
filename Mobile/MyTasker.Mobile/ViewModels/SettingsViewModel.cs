using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FmgLib.HttpClientHelper;
using MyTasker.Core.Enums;
using MyTasker.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;

namespace MyTasker.Mobile.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    [ObservableProperty]
    private SettingsModel _settings;

    private MyTaskTheme _theme;

    public SettingsViewModel()
    {
        GetSettings();
    }

    private async void GetSettings()
    {
        Settings = await HttpClientHelper.SendAsync<SettingsModel>(App.BaseUrl + "/Settings", HttpMethod.Get);
    }

    [RelayCommand]
    public void SetTheme(object theme)
    {
        if (Enum.TryParse(theme.ToString(), out MyTaskTheme result))
            _theme = result;
    }

    [RelayCommand]
    public async Task Save()
    {
        string message = string.Empty;

        Settings.Theme = _theme;

        var control = Settings.TryParseToJson(out string jsonModel);
        if (!control)
            jsonModel = JsonSerializer.Serialize(Settings);

        var result = await HttpClientHelper.SendAsync<string>(App.BaseUrl + "/Settings", HttpMethod.Put, jsonModel, ClientContentType.Json);
        if (result == null || !bool.Parse(result))
            message = "Settings don't save!";
        else
            message = "Settings saved!";

        SemanticScreenReader.Announce(message);
        var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 20);

        await toast.Show();

        App.Current.UserAppTheme = _theme == MyTaskTheme.Dark ? AppTheme.Dark : AppTheme.Light;
        App.Current.MainPage = new AppShell();
    }
}
