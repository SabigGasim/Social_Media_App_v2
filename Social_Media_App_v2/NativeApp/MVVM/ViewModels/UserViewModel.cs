using NativeApp.Constants;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class UserViewModel : ViewModelBase
{
    private INavigateCommandFactory _navigateCommandFactory;
    private ICommand? _profileIconClickedCommand;
    private UserModel? _user;
    private ICommand? _unMuteUserCommand;
    private ICommand? _blockOrUnblockUserCommand;
    private ICommand? _followOrUnFollowCommand;

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

    public ICommand? MuteOrUnmuteUserCommand => _unMuteUserCommand ??= new Command(() =>
    {
        this.User!.IsMuted = !this.User!.IsMuted;
        OnPropertyChanged(nameof(User));
    });

    public ICommand? BlockOrUnblockUserCommand => _blockOrUnblockUserCommand ??= new Command(() =>
    {
        this.User!.IsBlocked = !this.User.IsBlocked;
        OnPropertyChanged(nameof(User));
    });

    public ICommand? FollowOrUnFollowCommand => _followOrUnFollowCommand ??= new Command(() =>
    {
        this!.User!.IsUserBeingFollowed = !this.User!.IsUserBeingFollowed;
        OnPropertyChanged(nameof(User));
    });
}
