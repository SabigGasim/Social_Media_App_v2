using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels.Settings;
public class MutedAndBlockedViewModel : SettingsViewModelBase<MutedAndBlockedModel>
{
    private MutedAndBlockedModel? _mutedAndBlcoked; 
    
    public override MutedAndBlockedModel? Model
    {
        get => _mutedAndBlcoked; set => TrySetValue(ref _mutedAndBlcoked, value);
    }
}

