using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NativeApp.MVVM.ViewModels;

public interface IPropertyChangedNotifier : INotifyPropertyChanged
{
    bool TrySetValue<T>(ref T? property, T? value, [CallerMemberName] string? propertyName = null);
}

public class PropertyChangedNotifier : ObservableObject, IPropertyChangedNotifier
{
    public PropertyChangedNotifier()
    {
        Subscriber = new(this);
    }

    public bool TrySetValue<T>(ref T? property, T? value, [CallerMemberName] string? propertyName = null)
    {
        if(property is null || !property!.Equals(value)!)
        {
            property = value;
            OnPropertyChanged(propertyName!);
            return true;
        }

        return false;
    }

    public Subscriber Subscriber { get; private init; } = default!;
}