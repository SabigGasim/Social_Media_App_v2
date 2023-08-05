using NativeApp.Interfaces;
using NativeApp.Services;

namespace NativeApp.MVVM.ViewModels;

public abstract class ViewModelBase : PropertyChangedNotifier
{
    protected readonly INavigationService _navigationService;

    public ViewModelBase(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}
