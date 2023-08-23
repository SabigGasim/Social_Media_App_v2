using Domain.Common;
using NativeApp.Interfaces;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings;
public abstract class SettingsViewModelBase : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public SettingsViewModelBase(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public abstract ICommand? SaveSettingsCommand { get; }
    
    protected virtual async Task<Result> UpdateSettings()
    {
        await _navigationService.PopAsync();
        return Results.Success();
    }
}
