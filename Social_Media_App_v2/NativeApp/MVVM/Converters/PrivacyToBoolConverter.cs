using Domain.Enums;
using System.Globalization;

namespace NativeApp.MVVM.Converters;
public class PrivacyToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is Privacy privacy)
        {
            return privacy == Privacy.Private;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is bool isPrivate)
        {
            return isPrivate ? Privacy.Private : Privacy.Public;
        }

        return default(Privacy);
    }
}
