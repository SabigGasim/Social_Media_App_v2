using Domain.Common;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings;
public class PrivacyAndSecurityViewModel : SettingsViewModelBase
{
    private ICommand? _saveSettingsCommand;
    private PrivacyAndSecurityModel? _privacyAndSecurity;

    public PrivacyAndSecurityViewModel(INavigationService navigationService) : base(navigationService)
    {
    }

    public PrivacyAndSecurityModel? PrivacyAndSecurity
    {
        get => _privacyAndSecurity;
        set => TrySetValue(ref _privacyAndSecurity, value);
    }
    
    public override ICommand? SaveSettingsCommand => _saveSettingsCommand ??= new Command(async () =>
    {
        var result = await UpdateSettings();
        if (result.Success)
        {
            return;
        }
    
        var errorMessage = string.Join('\n', result.Errors!.Select(e => e.Message));
    
        Shell.Current.CurrentPage.Dispatcher
        .Dispatch(() => Shell.Current
        .DisplayAlert("Error", errorMessage, "Cancel"));
    });
    
    protected override Task<Result> UpdateSettings()
    {
        //1 - Update settings on the server
    
    
        //2 - if succeeds, update on the UI thread
        Shell.Current.CurrentPage.Dispatcher.Dispatch(() => OnPropertyChanged(nameof(PrivacyAndSecurity)));
    
        return base.UpdateSettings();
    }
}