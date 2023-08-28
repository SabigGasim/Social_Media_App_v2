namespace NativeApp.MVVM.ViewModels.Settings;

public abstract class SettingsViewModelBase<TModel> : ViewModelBase
{
    public abstract TModel? Model { get; set; }
}
