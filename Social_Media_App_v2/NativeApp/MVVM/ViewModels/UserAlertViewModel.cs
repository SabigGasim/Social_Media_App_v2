using NativeApp.Constants;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class UserAlertViewModel : AlertViewModelBase<UserAlertModel>
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private UserAlertModel? _userAlertModel = new();
    private Lazy<ICommand?>? _profileImageButtonClicked;
    private ICommand? _alertClickedCommand;

    public UserAlertViewModel(INavigateCommandFactory navigateCommandFactory)
    {
        _navigateCommandFactory = navigateCommandFactory;

        InitializeCommands();
    }

    public override UserAlertModel? Alert 
    { 
        get => _userAlertModel;
        set => TrySetValue(ref _userAlertModel, value); 
    }

    public ICommand? ProfileImageButtonClicked => _profileImageButtonClicked!.Value;

    public override ICommand? AlertClickedCommand => _alertClickedCommand;

    private void InitializeCommands()
    {
        _profileImageButtonClicked = new(() => _navigateCommandFactory
            .Create(nameof(UserModel), _userAlertModel!.User, Routes.ProfilePage));
    }
}
