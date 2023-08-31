using Domain.Common;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels.Settings.Notifications;
public class NotificationFiltersViewModel : 
    SubSettingsViewModelBase<NotificationSettingsViewModel, NotificationSettingsModel>
{
    private NotificationTypes? _filters;

    public NotificationFiltersViewModel(
        INavigationService navigationService,
        NotificationSettingsViewModel viewModel) : base(navigationService, viewModel)
    {

    }

    public NotificationTypes? Filters 
    { 
        get => _filters; 
        private set => TrySetValue(ref _filters, value); 
    }

    protected override Task<Result> UpdateSettings()
    {
        //1 - Copy the old value of the AccountInfo
        //2 - Do the required updates
        //3 - Send an UPDATE reuqest to the server
        //4 - if succeded, update the viewModel.

        var notificationSettings = GetModifiedNotificationSettings();

        SetNotificationSettingsOnMainThread(notificationSettings);

        return base.UpdateSettings();
    }

    protected override void SetOriginalSettings()
    {
        this.Filters = this.Model!.Types.DeepCopy();
    }

    private NotificationSettingsModel GetModifiedNotificationSettings()
    {
        var model = Model!.DeepCopy();
        model.Types = Filters;
        return model;
    }

    private void SetNotificationSettingsOnMainThread(NotificationSettingsModel notificationSettingsModel)
    {
        if (MainThread.IsMainThread)
        {
            base.Model = notificationSettingsModel;
            return;
        }

        MainThread.BeginInvokeOnMainThread(() => base.Model = notificationSettingsModel);
    }
}
