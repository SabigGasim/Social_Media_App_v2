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
    
    //I'm sorry but... maui forces you to do this
    //this property is used to not throw null exception when passing a query parameter through shell
    public static PostViewModel? Empty;

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
                var index = this.Post!.Media!.IndexOf((MediaModel)context!);
                var post = this.Post!;

                return new Dictionary<string, object>
                {
                    ["Index"] = index,
                    [nameof(PostModel)] = post
                };
            });
    }
}
