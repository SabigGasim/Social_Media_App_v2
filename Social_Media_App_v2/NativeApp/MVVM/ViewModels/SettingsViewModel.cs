using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;
public class SettingsViewModel : ViewModelBase
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private AccountInfoModel? _accountInfo = new();
    private NotificationSettingsModel? _notificationSettings = new();
    private MutedAndBlockedModel? _mutedAndBlocked = new();
    private PrivacyAndSecurityModel? _privacyAndSecurityModel = new();
    private ICommand? _tableViewOptionClickedCommand;

    public SettingsViewModel(
        INavigateCommandFactory navigateCommandFactory)
    {
        _navigateCommandFactory = navigateCommandFactory;

        IntializeCommands();
    }

    public AccountInfoModel? AccountInfo
    {
        get => _accountInfo; 
        set => TrySetValue(ref _accountInfo, value);
    }

    public NotificationSettingsModel? NotificationSettings
    {
        get => _notificationSettings; 
        set => TrySetValue(ref _notificationSettings, value);
    }

    public MutedAndBlockedModel? MutedAndBlocked
    {
        get => _mutedAndBlocked; 
        set => TrySetValue(ref _mutedAndBlocked, value);
    }

    public PrivacyAndSecurityModel? PrivacyAndSecurityModel
    {
        get => _privacyAndSecurityModel; 
        set => TrySetValue(ref _privacyAndSecurityModel, value);
    }

    public ICommand? TableViewOptionClickedCommand => _tableViewOptionClickedCommand;

    
    private void IntializeCommands()
    {
        _tableViewOptionClickedCommand = _navigateCommandFactory.Create();
    }
}
