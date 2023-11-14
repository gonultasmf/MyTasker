using MyTasker.Core.Enums;

namespace MyTasker.Core.Models;

public class TaskModel : BaseModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime TaskDate { get; set; }
    public bool IsFavourite { get; set; }
    public MyTaskStatus Status { get; set; }
    public string Color { get; set; }
}
