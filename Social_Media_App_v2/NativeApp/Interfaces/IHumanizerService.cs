using NativeApp.MVVM.ViewModels;

namespace NativeApp.Interfaces;

public interface IHumanizerService
{
    public string Humanize(DateTime dateTime);
    public string Humanize(DateTimeOffset dateTime);
    public string Humanize(int number);
    public string Humanize(long number);
}