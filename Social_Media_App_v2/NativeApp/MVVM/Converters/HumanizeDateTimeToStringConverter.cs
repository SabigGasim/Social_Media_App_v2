using NativeApp.Interfaces;
using System.Globalization;

namespace NativeApp.MVVM.Converters;
public class HumanizeDateTimeToStringConverter : IValueConverter
{
    private readonly IHumanizerService _dateTimeHumanizerService;

    public HumanizeDateTimeToStringConverter()
    {
        _dateTimeHumanizerService = DependencyService.Get<IHumanizerService>();
    }

    public HumanizeDateTimeToStringConverter(IHumanizerService dateTimeHumanizerService)
    {
        _dateTimeHumanizerService = dateTimeHumanizerService;
    }


    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return default!;
        }

        if(value is DateTime dateTime)
        {
            return _dateTimeHumanizerService.Humanize(dateTime);
        }

        if(value is DateTimeOffset dateTimeOffset)
        {
            return _dateTimeHumanizerService.Humanize(dateTimeOffset);
        }

        return default!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
