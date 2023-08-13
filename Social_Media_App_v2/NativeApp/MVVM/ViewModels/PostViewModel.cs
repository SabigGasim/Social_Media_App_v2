using NativeApp.Constants;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class PostViewModel : ViewModelBase
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private ICommand? _postsButtonClickedCommand;
    private ICommand? _commentsButtonClickedCommand;
    private PostModel? _post;
    private ICommand? _profileButtonClickedCommand;

    public PostViewModel(INavigateCommandFactory navigateCommandFactory)
    {
        _navigateCommandFactory = navigateCommandFactory;

        InitializeCommands();
    }

    public PostModel? Post
    {
        get => _post;
        set => TrySetValue(ref _post, value);
    }

    public ICommand? CommentsButtonClickedCommand
    {
        get => _commentsButtonClickedCommand;
        set => TrySetValue(ref _commentsButtonClickedCommand, value);
    }

    public ICommand? PostsButtonClickedCommand
    {
        get => _postsButtonClickedCommand;
        set => TrySetValue(ref _postsButtonClickedCommand, value);
    }


    public ICommand? ProfileButtonClickedCommand => _profileButtonClickedCommand;

    private void InitializeCommands()
    {
        _commentsButtonClickedCommand = 
            _navigateCommandFactory.Create(nameof(PostViewModel), this, Routes.CommentsPage);

        _profileButtonClickedCommand ??=
            _navigateCommandFactory.Create(nameof(UserModel), Routes.ProfilePage);

        _postsButtonClickedCommand = 
            _navigateCommandFactory.Create("params", Routes.MediaViewer, (context) =>
            {
                var media = this.Post!.Media!;
                var index = media.IndexOf((MediaModel)context!);
                

                return new Dictionary<string, object>
                {
                    ["Index"] = index,
                    [nameof(MediaListModel)] = media
                };
            });
    }
}
