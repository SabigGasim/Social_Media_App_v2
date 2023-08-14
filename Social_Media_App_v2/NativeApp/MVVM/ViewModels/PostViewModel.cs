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
    private ICommand? _likeButtonClickedCommand;

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

    public ICommand? CommentsButtonClickedCommand => _commentsButtonClickedCommand;

    public ICommand? PostsButtonClickedCommand => _postsButtonClickedCommand;

    public ICommand? LikeButtonClickedCommand => _likeButtonClickedCommand;

    public ICommand? ProfileButtonClickedCommand => _profileButtonClickedCommand;

    private void InitializeCommands()
    {
        _commentsButtonClickedCommand = 
            _navigateCommandFactory.Create(nameof(PostViewModel), this, Routes.CommentsPage);

        _profileButtonClickedCommand ??=
            _navigateCommandFactory.Create(nameof(UserModel), Routes.ProfilePage);

        _likeButtonClickedCommand = new Command(() =>
        {
            Post!.IsLiked = !Post.IsLiked;
            OnPropertyChanged(nameof(Post));
        });

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
