using Domain.Enums;
using NativeApp.Constants;
using NativeApp.Infrastructure.Data.Persistence;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.MVVM.Views;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;
public class ShellViewModel : ViewModelBase
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private readonly IServiceProvider _serviceProvider;
    private ICommand? _accountManagerButtonClicked;
    private ICommand? _profileFlyoutItemClickedCommand;
    private ICommand? _settingsFlyoutItemClickedCommand;

    public ShellViewModel(
        INavigateCommandFactory navigateCommandFactory,
        BindableAccountsAccessor accountsAccessor,
        IServiceProvider serviceProvider)
    {
        _navigateCommandFactory = navigateCommandFactory;
        AccountsAccessor = accountsAccessor;
        _serviceProvider = serviceProvider;

        AddFakeAccounts();
        InitializeCommands();
    }

    public BindableAccountsAccessor AccountsAccessor { get; private init; }

    public ICommand? AccountManagerButtonClicked => _accountManagerButtonClicked;
    public ICommand? ProfileFlyoutItemClickedCommand => _profileFlyoutItemClickedCommand;

    private void InitializeCommands()
    {
        _accountManagerButtonClicked = _navigateCommandFactory.Create(() => new AccountsPopup(this), 
            (UserViewModel userViewModel) => 
            {
                if(AccountsAccessor.CurrentAccount != userViewModel)
                {
                    AccountsAccessor.CurrentAccount = userViewModel;
                }
            });

        _profileFlyoutItemClickedCommand =
            _navigateCommandFactory.Create(nameof(UserModel), Routes.ProfilePage, () =>
            {
                Shell.Current.FlyoutIsPresented = false;
                return AccountsAccessor.CurrentAccount!.User!;
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


        AccountsAccessor.Accounts!.Add(userViewModel1);
        AccountsAccessor.Accounts!.Add(userViewModel2);
        AccountsAccessor.CurrentAccount = AccountsAccessor.Accounts[0];
    }
}
