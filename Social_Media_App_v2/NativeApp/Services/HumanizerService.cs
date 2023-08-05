using Humanizer;
using Humanizer.Localisation;
using NativeApp.Interfaces;
using NativeApp.MVVM.ViewModels;

namespace NativeApp.Services;

public class HumanizerService : IHumanizerService
{
    public string Humanize(DateTime dateTime)
    {
        var timeSpace = DateTime.Now - dateTime;

        if (Math.Abs(timeSpace.Ticks) < TimeSpan.TicksPerSecond)
        {
            return "Now";
        }

        var since = timeSpace.Humanize(maxUnit: TimeUnit.Year, minUnit: TimeUnit.Second);
        return since;
    }

    public string Humanize(DateTimeOffset dateTime)
    {
        var timeSpace = DateTimeOffset.Now - dateTime;

        if (Math.Abs(timeSpace.Ticks) < TimeSpan.TicksPerSecond)
        {
            return "Now";
        }

        var since = timeSpace.Humanize(maxUnit: TimeUnit.Year, minUnit: TimeUnit.Second);
        return since;
    }

    public string Humanize(int number)
    {
        switch (number)
        {
            case < 1000:
                return number.ToString();
            case < 10000:
                return string.Format("{0:#,.##}K", number - 5);
            case < 100000:
                return string.Format("{0:#,.#}K", number - 50);
            case < 1000000:
                return string.Format("{0:#,.}K", number - 500);
            case < 10000000:
                return string.Format("{0:#,,.##}M", number - 5000);
            case < 100000000:
                return string.Format("{0:#,,.#}M", number - 50000);
            case < 1000000000:
                return string.Format("{0:#,,.}M", number - 500000);
            default:
                return string.Format("{0:#,,,.##}B", number - 5000000);
        }
    }

    public string Humanize(long number)
    {
        switch (number)
        {
            case < 1000:
                return number.ToString();
            case < 10000:
                return string.Format("{0:#,.##}K", number - 5);
            case < 100000:
                return string.Format("{0:#,.#}K", number - 50);
            case < 1000000:
                return string.Format("{0:#,.}K", number - 500);
            case < 10000000:
                return string.Format("{0:#,,.##}M", number - 5000);
            case < 100000000:
                return string.Format("{0:#,,.#}M", number - 50000);
            case < 1000000000:
                return string.Format("{0:#,,.}M", number - 500000);
            default:
                return string.Format("{0:#,,,.##}B", number - 5000000);
        }
    }
}
