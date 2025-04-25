using Infrastructure.Interfaces;
using NativeApp.Helpers;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class ProfileViewModel : ViewModelBase
{
    private RangeObservableCollection<PostViewModel>? _postViewModels = new();
    private UserViewModel? _userViewModel;
    private readonly IPostRepository _repository;
    private readonly IServiceProvider _serviceProvider;

    public ProfileViewModel(
        IPostRepository repository,
        IServiceProvider serviceProvider,
        UserViewModel userViewModel)
    {
        _repository = repository;
        _serviceProvider = serviceProvider;
        _userViewModel = userViewModel;
    }

    public async Task UpdatePosts(Guid? lastSeenPost, int numberOfPosts)
    {
        var result = await _repository.GetUserPosts(UserViewModel!.User!.Id, lastSeenPost, numberOfPosts);
        if (!result.Success)
        {
            return;
        }

        var posts = result.Value!.Map().ToList();
        if (posts.Count <= 0 || posts.Count >= 350)
        {
            return;
        }

        var postViewModels = posts.Select(post =>
        {
            var postViewModel = _serviceProvider.GetRequiredService<PostViewModel>();
            postViewModel!.Post = post;
            return postViewModel;
        });


        PostViewModels!.AddRange(postViewModels.Reverse());
    }

    public UserViewModel? UserViewModel 
    { 
        get => _userViewModel; 
        set => TrySetValue(ref _userViewModel, value); 
    }

    public RangeObservableCollection<PostViewModel>? PostViewModels
    {
        get => _postViewModels;
        set => TrySetValue(ref _postViewModels, value);
    }
}
