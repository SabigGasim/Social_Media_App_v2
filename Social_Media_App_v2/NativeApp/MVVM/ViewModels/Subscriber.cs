using System.ComponentModel;

namespace NativeApp.MVVM.ViewModels;
public class Subscriber
{
    private readonly INotifyPropertyChanged _notifier;

    public Subscriber(INotifyPropertyChanged notifier)
    {
        _notifier = notifier;
    }

    public void Subscribe(PropertyChangedEventHandler handler) => _notifier.PropertyChanged += handler;

    public void Unsubscribe(PropertyChangedEventHandler handler) => _notifier.PropertyChanged -= handler;
}
