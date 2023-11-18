using MyTasker.Core.Enums;
using System.Text.Json.Serialization;

namespace MyTasker.Core.Models;

public class SettingsModel : BaseModel
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("theme")]
    public MyTaskTheme Theme { get; set; }
}
