using Infrastructure.Interfaces;
using NativeApp.Factories;
using NativeApp.Helpers;
using NativeApp.Interfaces;
using NativeApp.MVVM.Converters;
using NativeApp.MVVM.Models;
using NativeApp.Services;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;
public sealed class TimelineViewModel : ViewModelBase
{
    private readonly ITimelineRepository _timelineRepository;
    private readonly IServiceProvider _serviceProvider;
    private ObservableList<PostViewModel> _postsViewModels = new(100);

    public TimelineViewModel(
        INavigationService navigationService,
        ITimelineRepository timelineRepository,
        IServiceProvider serviceProvider) : base(navigationService)
    {
        _timelineRepository = timelineRepository;
        _serviceProvider = serviceProvider;
    }

    public async Task UpdateTimeline(Guid? lastSeenGuid)
    {
        var posts = (await _timelineRepository
            .GetTimelinePosts(lastSeenGuid, lastSeenGuid is null ? 100 : 5)).Map();


        var threshold = (posts.Count() + _postsViewModels.Count) - 100;
        if(threshold > 0)
        {
            _postsViewModels.RemoveRange(_postsViewModels.Count - 1 - threshold, threshold);
        }

        foreach (var post in posts)
        {
            var postViewModel = _serviceProvider.GetRequiredService<PostViewModel>();
            postViewModel.Post = post;

            _postsViewModels.Insert(0, postViewModel);
        }
    }

    public ObservableList<PostViewModel> PostViewModels
    {
        get => _postsViewModels;
        set => TrySetValue(ref _postsViewModels, value);
    }
}
