using MyTasker.Core.Enums;

namespace MyTasker.Core.Models;

public class SettingsModel : BaseModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public MyTaskTheme Theme { get; set; }
}
