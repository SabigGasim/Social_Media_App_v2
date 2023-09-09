using Domain.Enums;
using Humanizer;
using System.Globalization;

namespace NativeApp.MVVM.Converters;
public class DurationToStringConverter : IValueConverter
{
    private readonly Dictionary<Duration, string> _convertCache = new();
    private readonly Dictionary<string, Duration> _convertBackCache = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is Duration duration)
        {
            if(_convertCache.TryGetValue(duration, out string? result))
            {
                return result;
            }

            var name = duration.Humanize(LetterCasing.Sentence);
            _convertCache.Add(duration, name);
            
            return name;
        }

        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is string str)
        {
            if (_convertBackCache.TryGetValue(str, out Duration result))
            {
                return result;
            }

            var duration = Enum.Parse<Duration>(str.Dehumanize());
            _convertBackCache.Add(str, result);

            return duration;
        }

        return Duration.Unspecified;
    }
}
