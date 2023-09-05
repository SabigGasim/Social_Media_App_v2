using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;
public class MutedAndBlockedViewModel : ViewModelBase
{
    private MutedAndBlockedModel? _mutedAndBlcoked;

    public MutedAndBlockedModel? Model
    {
        get => _mutedAndBlcoked;
        set => TrySetValue(ref _mutedAndBlcoked, value);
    }
}

