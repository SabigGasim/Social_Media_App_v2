using Domain.Enums;
using NativeApp.MVVM.Models;
using Domain.Common;
using NativeApp.Interfaces;

namespace NativeApp.MVVM.ViewModels.Settings.PrivacyAndSecurity;

public class AudienceViewModel : SubSettingsViewModelBase<PrivacyAndSecurityViewModel, PrivacyAndSecurityModel>
{
    private Privacy _privacy;

    public AudienceViewModel(
        INavigationService navigationService,
        PrivacyAndSecurityViewModel viewModel) : base(navigationService, viewModel)
    {

    }

    public Privacy Privacy
    {
        get => _privacy;
        set => TrySetValue(ref _privacy, value);
    }

    protected override Task<Result> UpdateSettings()
    {
        //1 - Copy the old value of the AccountInfo
        //2 - Do the required updates
        //3 - Send an UPDATE reuqest to the server
        //4 - if succeded, update the viewModel.

        var privacyModel = GetModifiedPrivacySettings();

        SetPrivacyOnMainThread(privacyModel);

        return base.UpdateSettings();
    }

    protected override void SetOriginalSettings()
    {
        this.Privacy = this.Model!.Privacy;
    }

    private PrivacyAndSecurityModel GetModifiedPrivacySettings()
    {
        var model = Model!.DeepCopy();
        model.Privacy = this.Privacy;
        return model;
    }

    private void SetPrivacyOnMainThread(PrivacyAndSecurityModel model)
    {
        if (MainThread.IsMainThread)
        {
            base.Model = model;
            return;
        }

        MainThread.BeginInvokeOnMainThread(() => base.Model = model);
    }
}
