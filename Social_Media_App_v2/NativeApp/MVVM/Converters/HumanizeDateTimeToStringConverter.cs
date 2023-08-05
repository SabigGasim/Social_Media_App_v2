using NativeApp.Interfaces;
using NativeApp.MVVM.ViewModels;
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
        if (value is null || value is not DateTime dateTime)
        {
            return null;
        }

        return _dateTimeHumanizerService.Humanize(dateTime);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
