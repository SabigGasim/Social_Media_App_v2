using Domain.Common;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings;

public class NotificationSettingsViewModel : SettingsViewModelBase
{
    private ICommand? _saveSettingsCommand;
    private NotificationSettingsModel? _notificationSettings;

    public NotificationSettingsViewModel(INavigationService navigationService) : base(navigationService)
    {
    }

    public NotificationSettingsModel? NotificationSettings
    {
        get => _notificationSettings;
        set => TrySetValue(ref _notificationSettings, value);
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


        //2 - if succeeded, update on the UI thread
        Shell.Current.CurrentPage.Dispatcher.Dispatch(() => OnPropertyChanged(nameof(NotificationSettings)));

        return base.UpdateSettings();
    }
}
