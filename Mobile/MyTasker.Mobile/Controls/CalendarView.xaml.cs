using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyTasker.Mobile.Controls;

public partial class CalendarView : StackLayout
{
    public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
       nameof(SelectedDate),
       typeof(DateTime),
       declaringType: typeof(CalendarView),
       defaultBindingMode: BindingMode.TwoWay,
       defaultValue: DateTime.Now,
       propertyChanged: SelectedDatePropertyChanged);

    private static void SelectedDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var controls = (CalendarView)bindable;
        if (newValue != null)
        {
            var newDate = (DateTime)newValue;

            if (controls._tempDate.Month == newDate.Month && controls._tempDate.Year == newDate.Year)
            {
                var currentDate = controls.Dates.FirstOrDefault(x => x.Date == newDate.Date);
                if (currentDate != null)
                {
                    foreach (var item in controls.Dates)
                    {
                        item.IsCurrentDate = false;
                    }
                    currentDate.IsCurrentDate = true;
                }
            }
            else
            {
                controls.BindDates(newDate);
            }
        }
    }

    public static readonly BindableProperty SelectedDateCommandProperty = BindableProperty.Create(
        nameof(SelectedDateCommand),
        typeof(ICommand),
        declaringType: typeof(CalendarView));

    public ICommand SelectedDateCommand
    {
        get => (ICommand)GetValue(SelectedDateCommandProperty);
        set => SetValue(SelectedDateCommandProperty, value);
    }

    public DateTime SelectedDate
    {
        get => (DateTime)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    public event EventHandler<DateTime> OnDateSelected;

    public CollectionView collView { get; set; }

    public ObservableCollection<CalendarModel> Dates { get; set; } = new ObservableCollection<CalendarModel>();

	private DateTime _tempDate;

	public CalendarView()
	{
		InitializeComponent();
		BindDates(DateTime.Now);
        collView = colDate;
	}

	private void BindDates(DateTime date)
	{
		Dates.Clear();

		for (int day = 1; day < DateTime.DaysInMonth(date.Year,date.Month); day++)
		{
			Dates.Add(new()
			{
				Date = new DateTime(date.Year, date.Month, day),
			});
		}
		var selectedDate = Dates.FirstOrDefault(x => x.Date.Date == date.Date);
		if (selectedDate != null)
		{
			selectedDate.IsCurrentDate = true;
			_tempDate = selectedDate.Date;
		}
	}

    public ICommand CurrentDateCommand => new Command<CalendarModel>((currentDate) =>
    {
        _tempDate = currentDate.Date;
        SelectedDate = currentDate.Date;
        OnDateSelected?.Invoke(null, currentDate.Date);
        SelectedDateCommand?.Execute(currentDate.Date);
    });


    public ICommand NextMonthCommand => new Command(() =>
    {
        _tempDate = _tempDate.AddMonths(1);
        BindDates(_tempDate);
    });


    public ICommand PreviousMonthCommand => new Command(() =>
    {
        _tempDate = _tempDate.AddMonths(-1);
        BindDates(_tempDate);
    });
}