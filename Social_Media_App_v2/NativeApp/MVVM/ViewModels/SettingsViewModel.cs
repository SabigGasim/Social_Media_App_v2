﻿using Domain.Common;
using Domain.Enums;
using Infrastructure.Interfaces;
using NativeApp.Helpers;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.MVVM.ViewModels.Settings;
using NativeApp.MVVM.ViewModels.Settings.AccountInfo;
using NativeApp.MVVM.ViewModels.Settings.Notifications;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;
public class SettingsViewModel : ViewModelBase
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private readonly IServiceProvider _serviceProvider;
    private AccountInfoViewModel? _accountInfo;
    private NotificationSettingsViewModel? _notificationSettings;
    private MutedAndBlockedViewModel? _mutedAndBlocked;
    private PrivacyAndSecurityViewModel? _privacyAndSecurityModel;
    private ICommand? _tableViewOptionClickedCommand;

    public SettingsViewModel(
        INavigateCommandFactory navigateCommandFactory,
        IServiceProvider serviceProvider,
        AccountInfoViewModel accuontInfoViewModel,
        NotificationSettingsViewModel notificationSettingsViewModel,
        MutedAndBlockedViewModel mutedAndBlockedViewModel,
        PrivacyAndSecurityViewModel privacyAndSecurityViewModel)
    {
        _navigateCommandFactory = navigateCommandFactory;
        _serviceProvider = serviceProvider;

        _accountInfo = accuontInfoViewModel;
        _notificationSettings = notificationSettingsViewModel;
        _mutedAndBlocked = mutedAndBlockedViewModel;
        _privacyAndSecurityModel = privacyAndSecurityViewModel;

        ConfigureDefaultSettings().GetAwaiter().GetResult();

        IntializeCommands();
    }

    public AccountInfoViewModel? AccountInfoViewModel
    {
        get => _accountInfo;
        set => TrySetValue(ref _accountInfo, value);
    }

    public NotificationSettingsViewModel? NotificationSettingsViewModel
    {
        get => _notificationSettings;
        set => TrySetValue(ref _notificationSettings, value);
    }

    public MutedAndBlockedViewModel? MutedAndBlockedViewModel
    {
        get => _mutedAndBlocked;
        set => TrySetValue(ref _mutedAndBlocked, value);
    }

    public PrivacyAndSecurityViewModel? PrivacyAndSecurityViewModel
    {
        get => _privacyAndSecurityModel;
        set => TrySetValue(ref _privacyAndSecurityModel, value);
    }

    public ICommand? TableViewOptionClickedCommand
    {
        get => _tableViewOptionClickedCommand;
        set => TrySetValue(ref _tableViewOptionClickedCommand, value);
    }


    private void IntializeCommands()
    {
        TableViewOptionClickedCommand = _navigateCommandFactory.Create();
    }

    private async Task ConfigureDefaultSettings()
    {
        var accountInfo = new AccountInfoModel
        {
            DateOfBirth = new DateOnly(2007, 5, 15),
            Email = "test@test.com",
            PhoneNumber = "+69123456789",
            Username = "BarbieGirlFan74",
            Nickname = "ابن مطحس"
        };

        var userLookupRepository = _serviceProvider.GetRequiredService<IUserLookupRepository>();
        var muted = (await userLookupRepository.FindManyByUsername("a")).Value!.Map();
        var blocked = (await userLookupRepository.FindManyByUsername("b")).Value!.Map();

        var mutedAndBlocked = new MutedAndBlockedModel
        {
            MutedUsers = new RangeObservableCollection<UserModel>(muted),
            BlockedUsers = new RangeObservableCollection<UserModel>(blocked)
        };

        var notificationSettings = new NotificationSettingsModel
        {
            Methods = new NotificationMethods
            {
                AppAlerts = true,
                Email = false,
                PushNotifications = false,
                SMS = false
            },
            Types = new NotificationTypes
            {
                ContentSuggestion = true,
                DirectMessages = true,
                GroupMessages = true,
                NewFollows = true,
                NewPostsFromFriends = true,
                FollowRequests = true,
                AcceptedFollowReuqests = true,
            }
        };

        var privacyAndSecurity = new PrivacyAndSecurityModel
        {
            Privacy = Privacy.Public,
            TwoFactorEnabled = false
        };

        _accountInfo!.Model = accountInfo;
        _mutedAndBlocked!.Model = mutedAndBlocked;
        _notificationSettings!.Model = notificationSettings;
        _privacyAndSecurityModel!.Model = privacyAndSecurity;
    }
}