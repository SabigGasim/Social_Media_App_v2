using NativeApp.Constants;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class UserViewModel : ViewModelBase
{
    private INavigateCommandFactory _navigateCommandFactory;
    private ICommand? _profileIconClickedCommand;
    private UserModel? _user;
    private ICommand? _unMuteUserCommand;

    public UserViewModel(INavigateCommandFactory navigateCommandFactory)
    {
        _navigateCommandFactory = navigateCommandFactory;
    }

    public UserModel? User 
    { 
        get => _user; 
        set => TrySetValue(ref _user, value); 
    }

    public bool IsDisplayingUserProfile { get; set; }

    public ICommand? ProfileIconClickedCommand 
    {
        get
        {
            if (!IsDisplayingUserProfile)
            {
                return _profileIconClickedCommand ??= 
                    _navigateCommandFactory.Create(nameof(UserModel), this.User!, Routes.ProfilePage);
            }

            return _profileIconClickedCommand ??= _navigateCommandFactory
                .Create(nameof(MediaListModel), new MediaListModel{User!.Profile.Icon}, Routes.MediaViewer);
        }
    }

    public ICommand? UnMuteUserCommand => _unMuteUserCommand ??= new Command(() =>
    {
        this.User!.IsMuted = false;
        OnPropertyChanged(nameof(User));
    });
}
