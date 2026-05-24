using System.Collections.Generic;
using AvaloniaFluentUI.Locale;
using Gallery.Controls;

namespace Gallery.Views;

public partial class DateTimeView : ViewBase
{
    public DateTimeView() : base("DateTime")
    {
        InitializeComponent();

        Title = LocalizationService.Instance.GetString("DateTime");

        CodeCards = new Dictionary<string, CodeCard>()
        {
            { "CalendarDatePicker", CalendarDatePickerCard },
            { "DatePicker", DatePickerCard },
            { "TimePicker", TimePickerCard }
        };
    }
}
