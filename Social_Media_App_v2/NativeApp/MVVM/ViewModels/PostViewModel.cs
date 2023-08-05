using NativeApp.Factories;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;
public class PostViewModel : ViewModelBase
{
    private readonly IRouteParametersFactory _routeParametersFactory;
    private ICommand _postsButtonClickedCommand;
    private PostModel _post = new();
    private ICommand _commentsButtonClickedCommand;

    public PostViewModel(
        INavigationService navigationService,
        IRouteParametersFactory routeParametersFactory
        ) : base(navigationService)
    {
        _routeParametersFactory = routeParametersFactory;

        InitializeCommands();
    }

    public PostModel Post
    {
        get => _post;
        set => TrySetValue(ref _post, value);
    }

    public ICommand CommentsButtonClickedCommand
    {
        get => _commentsButtonClickedCommand;
        set => TrySetValue(ref _commentsButtonClickedCommand, value);
    }

    public ICommand PostsButtonClickedCommand
    {
        get => _postsButtonClickedCommand;
        set => TrySetValue(ref _postsButtonClickedCommand, value);
    }

    private void InitializeCommands()
    {
        //_commentsButtonClickedCommand = new Command(async (post) =>
        //{
        //    var routeParam = _routeParametersFactory.Create(nameof(CommentsViewModel.Post), (PostModel)post);
        //
        //    await _navigationService.NavigateToAsync(Routes.CommentsPage, routeParam);
        //});

        _postsButtonClickedCommand = new Command(async (image) =>
        {
            var routeParam = _routeParametersFactory.Create("Image", image as ImageSource);

            await _navigationService.NavigateToAsync("mediaViewer", routeParam);
        });
    }
}
