using MyTasker.Core.Enums;
using System.Text.Json.Serialization;

namespace MyTasker.Core.Models;

public class TaskModel : BaseModel
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("taskDate")]
    public DateTime TaskDate { get; set; }

    [JsonPropertyName("isFavourite")]
    public bool IsFavourite { get; set; }

    [JsonPropertyName("status")]
    public MyTaskStatus Status { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }
}
