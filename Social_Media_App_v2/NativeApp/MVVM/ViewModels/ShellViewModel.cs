using Domain.Enums;
using NativeApp.Constants;
using NativeApp.Helpers;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.MVVM.Views;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;
public class ShellViewModel : ViewModelBase
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private readonly IServiceProvider _serviceProvider;
    private RangeObservableCollection<UserViewModel>? _accounts = new();
    private ICommand? _accountManagerButtonClicked;
    private ICommand? _profileFlyoutItemClickedCommand;
    private ICommand? _settingsFlyoutItemClickedCommand;
    private UserViewModel? _currentAccount;

    public ShellViewModel(
        INavigateCommandFactory navigateCommandFactory,
        IServiceProvider serviceProvider)
    {
        _navigateCommandFactory = navigateCommandFactory;
        _serviceProvider = serviceProvider;

        AddFakeAccounts();
        InitializeCommands();
    }
    

    public RangeObservableCollection<UserViewModel>? Accounts 
    {
        get => _accounts;
        set => TrySetValue(ref _accounts, value);
    }

    public UserViewModel? CurrentAccount 
    { 
        get => _currentAccount; 
        set => TrySetValue(ref _currentAccount, value); 
    }

    public ICommand? AccountManagerButtonClicked => _accountManagerButtonClicked;
    public ICommand? ProfileFlyoutItemClickedCommand => _profileFlyoutItemClickedCommand;
    public ICommand? SettingsFlyoutItemClickedCommand => _settingsFlyoutItemClickedCommand;

    private void InitializeCommands()
    {
        _accountManagerButtonClicked = _navigateCommandFactory.Create(() => new AccountsPopup(this), 
            (UserViewModel userViewModel) => 
            {
                if(_currentAccount != userViewModel)
                {
                    CurrentAccount = userViewModel;
                }
            });

        _profileFlyoutItemClickedCommand =
            _navigateCommandFactory.Create(nameof(UserModel), Routes.ProfilePage, () =>
            {
                Shell.Current.FlyoutIsPresented = false;
                return this.CurrentAccount!.User!;
            });
    }

    private void AddFakeAccounts()
    {
        var userViewModel1 = _serviceProvider.GetRequiredService<UserViewModel>();
        var userViewModel2 = _serviceProvider.GetRequiredService<UserViewModel>();

        userViewModel1.User = new UserModel
        {
            Id = Guid.NewGuid(),
            UserName = "me",
            Nickname = "JUST ME",
            IsUserBeingFollowed = false,
            Profile = new ProfileModel
            {
                Description = "it's just a test account",
                Icon = new MediaModel
                {
                    Url = "https://media.istockphoto.com/id/1397556857/vector/avatar-13.jpg?s=612x612&w=0&k=20&c=n4kOY_OEVVIMkiCNOnFbCxM0yQBiKVea-ylQG62JErI="
                },
            },
            FollowersCount = 42069,
            FollowingCount = 69,
            PostsCount = 15,
            AccountPrivacy = Privacy.Public,
            State = AccountState.Active
        };

        userViewModel2.User = new UserModel
        {
            Id = Guid.NewGuid(),
            UserName = "SY7K",
            Nickname = "Sabig Gasim | سابق قاسم",
            IsUserBeingFollowed = false,
            Profile = new ProfileModel
            {
                Description = "it's just a test account",
                Icon = new MediaModel
                {
                    Url = "https://media.istockphoto.com/id/1397556857/vector/avatar-13.jpg?s=612x612&w=0&k=20&c=n4kOY_OEVVIMkiCNOnFbCxM0yQBiKVea-ylQG62JErI="
                },
            },
            FollowersCount = 50_245,
            FollowingCount = 103,
            PostsCount = 15,
            AccountPrivacy = Privacy.Public,
            State = AccountState.Active
        };


        _accounts!.Add(userViewModel1);
        _accounts!.Add(userViewModel2);
        _currentAccount = _accounts[0];
    }
}
