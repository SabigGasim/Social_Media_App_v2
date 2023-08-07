using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NativeApp.MVVM.ViewModels;

public interface IPropertyChangedNotifier : INotifyPropertyChanged
{
    bool TrySetValue<T>(ref T? property, T? value, [CallerMemberName] string? propertyName = null);
}

public class PropertyChangedNotifier : IPropertyChangedNotifier
{
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

    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
}