using System.Globalization;

namespace NativeApp.MVVM.Converters;
public class FirstStringItemConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is IEnumerable<string> list)
        {
            return list.FirstOrDefault() ?? "";
        }

        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
