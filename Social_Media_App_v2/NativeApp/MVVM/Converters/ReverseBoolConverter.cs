using System.Globalization;

namespace NativeApp.MVVM.Converters;

public class ReverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is bool b)
        {
            return !b;
        }

        return default!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            return !b;
        }

        return default!;
    }
}
