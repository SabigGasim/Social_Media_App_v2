﻿using Infrastructure.Interfaces;
using NativeApp.Constants;
using NativeApp.Helpers;
using NativeApp.Interfaces;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;
public sealed class TimelineViewModel : ViewModelBase
{
    private readonly ITimelineRepository _timelineRepository;
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private readonly IServiceProvider _serviceProvider;
    private RangeObservableCollection<PostViewModel>? _postViewModels = new();
    private ICommand? _goToAddPostPageCommand;

    public TimelineViewModel(
        ITimelineRepository timelineRepository,
        INavigateCommandFactory navigateCommandFactory,
        IServiceProvider serviceProvider)
    {
        _timelineRepository = timelineRepository;
        _navigateCommandFactory = navigateCommandFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task UpdateTimeline(Guid? lastSeenGuid)
    {
        var posts = (await _timelineRepository
            .GetTimelinePosts(lastSeenGuid, lastSeenGuid is null ? 100 : 5)).Map();


        var threshold = (posts.Count() + PostViewModels!.Count) - 100;
        if(threshold > 0)
        {
            PostViewModels!.RemoveRange(PostViewModels!.Count - 1 - threshold, threshold);
        }

        foreach (var post in posts)
        {
            var postViewModel = _serviceProvider.GetRequiredService<PostViewModel>();
            postViewModel!.Post = post;

            PostViewModels?.Insert(0, postViewModel);
        }
    }

    public RangeObservableCollection<PostViewModel>? PostViewModels
    {
        get => _postViewModels;
        set => TrySetValue(ref _postViewModels, value);
    }

    public ICommand? GoToAddPostPageCommand =>
        _goToAddPostPageCommand ??= _navigateCommandFactory.Create(Routes.AddPostPage);

    public Task AddPost(PostViewModel post)
    {
        PostViewModels!.Insert(0, post);

        return Task.CompletedTask;
    }
}
