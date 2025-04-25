using NativeApp.Interfaces;
using System.Globalization;

namespace NativeApp.MVVM.Converters;
public class HumanizeIntToStringConverter : IValueConverter
{
    private readonly IHumanizerService _humanizer;

    public HumanizeIntToStringConverter()
    {
        _humanizer = DependencyService.Get<IHumanizerService>();
    }

    public HumanizeIntToStringConverter(IHumanizerService dateTimeHumanizerService)
    {
        _humanizer = dateTimeHumanizerService;
    }


    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int i)
        {
            return _humanizer.Humanize(i);
        }

        if (value is long l)
        {
            return _humanizer.Humanize(l);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
