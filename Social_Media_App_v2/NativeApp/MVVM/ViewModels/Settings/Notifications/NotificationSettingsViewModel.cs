using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels.Settings.Notifications;

public class NotificationSettingsViewModel : SettingsViewModelBase<NotificationSettingsModel>
{
    private NotificationSettingsModel? _notificationSettingsModel;

    public override NotificationSettingsModel? Model
    {
        get => _notificationSettingsModel;
        set => TrySetValue(ref _notificationSettingsModel, value);
    }
}
