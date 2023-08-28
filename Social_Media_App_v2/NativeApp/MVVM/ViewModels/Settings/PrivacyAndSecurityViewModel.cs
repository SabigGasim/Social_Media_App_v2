using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels.Settings;
public class PrivacyAndSecurityViewModel : SettingsViewModelBase<PrivacyAndSecurityModel>
{
    private PrivacyAndSecurityModel? _privacyAndSecurity;

    public override PrivacyAndSecurityModel? Model
    {
        get => _privacyAndSecurity;
        set => TrySetValue(ref _privacyAndSecurity, value);
    }
}