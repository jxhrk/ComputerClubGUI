using Avalonia;
using Avalonia.Controls;
using System;

namespace CompClubGUI.Admin;

public partial class DateTimeSelector : UserControl
{

    public DateTimeSelector()
    {
        InitializeComponent();

        Loaded += DateTimeSelector_Loaded;
    }

    public static readonly StyledProperty<DateTime?> DataDateTimeProperty =
    AvaloniaProperty.Register<DateTimeSelector, DateTime?>(nameof(DataDateTime), defaultValue: null);

    public DateTime? DataDateTime
    {
        get => GetValue(DataDateTimeProperty);
        set => SetValue(DataDateTimeProperty, value);

    }

    private void DateTimeSelector_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataDateTime != null)
        {
            DatePick.SelectedDate = DataDateTime;
            TimePick.SelectedTime = TimeOnly.FromDateTime(DataDateTime.Value).ToTimeSpan();
        }

        DatePick.SelectedDateChanged += DatePick_SelectedDateChanged;
        TimePick.SelectedTimeChanged += TimePick_SelectedTimeChanged;
    }

    private void DatePick_SelectedDateChanged(object? sender, SelectionChangedEventArgs e) => UpdateProperty();

    private void TimePick_SelectedTimeChanged(object? sender, TimePickerSelectedValueChangedEventArgs e) => UpdateProperty();


    private void UpdateProperty()
    {
        DataDateTime = DateOnly.FromDateTime(DatePick.SelectedDate.Value).ToDateTime(TimeOnly.FromTimeSpan(TimePick.SelectedTime.Value));
    }
}