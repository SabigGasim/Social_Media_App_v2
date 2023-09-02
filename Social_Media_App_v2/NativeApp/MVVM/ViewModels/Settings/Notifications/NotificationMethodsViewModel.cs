using Domain.Common;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.MVVM.ViewModels.Settings.AccountInfo;
using System.ComponentModel;

namespace NativeApp.MVVM.ViewModels.Settings.Notifications;
public class NotificationMethodsViewModel : 
    SubSettingsViewModelBase<NotificationSettingsViewModel,  NotificationSettingsModel>, IDisposable
{
    private NotificationMethods? _methods;
    private Func<AccountInfoModel?> _accountInfoGetter;
    private readonly Subscriber _accountInfoSubscriber;
    private readonly PropertyChangedEventHandler OnAccountInfoChanged;

    public NotificationMethodsViewModel(
        INavigationService navigationService,
        NotificationSettingsViewModel viewModel,
        AccountInfoViewModel accountInfoViewModel) : base(navigationService, viewModel)
    {
        _accountInfoGetter = () => accountInfoViewModel.Model;
        _accountInfoSubscriber = accountInfoViewModel.Subscriber;
        OnAccountInfoChanged = (sender, args) =>
        {
            if (args.PropertyName == nameof(AccountInfoViewModel.Model))
            {
                OnPropertyChanged(nameof(AccountInfo));
            }
        };

        SubscribeToAccountInfo();
    }

    public NotificationMethods? Methods
    {
        get => _methods;
        private set => TrySetValue(ref _methods, value);
    }

    public AccountInfoModel? AccountInfo => _accountInfoGetter();

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
        this.Methods = this.Model!.Methods.DeepCopy();
    }

    private NotificationSettingsModel GetModifiedNotificationSettings()
    {
        var model = Model!.DeepCopy();
        model.Methods = Methods;
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

    private void SubscribeToAccountInfo() => _accountInfoSubscriber.Subscribe(OnAccountInfoChanged);
    private void UnsubscribeFromAccountInfo() => _accountInfoSubscriber.Unsubscribe(OnAccountInfoChanged);
    
    public new void Dispose()
    {
        UnsubscribeFromAccountInfo();
    }
}
