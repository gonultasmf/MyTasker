namespace MyTasker.Mobile.Controls;

public class CalendarModel : PropertyChangedModel
{
    public DateTime Date { get; set; }

    private bool _isCurrentDate;

    public bool IsCurrentDate
    {
        get => _isCurrentDate;
        set => SetProperty(ref _isCurrentDate, value);
    }
}
