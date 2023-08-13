using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class UserViewModel : ViewModelBase
{
    private INavigateCommandFactory _navigateCommandFactory;
    private ICommand? _profileIconClickedCommand;
    private UserModel? _user;

    public UserViewModel(INavigateCommandFactory navigateCommandFactory)
    {
        _navigateCommandFactory = navigateCommandFactory;
    }

    public UserModel? User 
    { 
        get => _user; 
        set => TrySetValue(ref _user, value); 
    }

    public ICommand? ProfileIconClickedCommand => _profileIconClickedCommand;
}
