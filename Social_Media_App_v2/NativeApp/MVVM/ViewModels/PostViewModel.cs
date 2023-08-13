using NativeApp.Constants;
using NativeApp.Factories;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.Services;
using System.Reflection;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class PostViewModel : ViewModelBase
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private ICommand? _postsButtonClickedCommand;
    private ICommand? _commentsButtonClickedCommand;
    private PostModel? _post;
    
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

    private void InitializeCommands()
    {
        _commentsButtonClickedCommand = _navigateCommandFactory.Create("PostViewModel", this, Routes.CommentsPage);


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
