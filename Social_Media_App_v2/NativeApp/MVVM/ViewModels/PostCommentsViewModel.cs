using Infrastructure.Interfaces;
using NativeApp.Helpers;
using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels;

public partial class PostCommentsViewModel : ViewModelBase
{
    private ObservableList<CommentViewModel>? _comments = new();
    private PostViewModel? _postViewModel;
    private readonly ICommentsRepository _repository;
    private readonly IServiceProvider _serviceProvider;

    public PostCommentsViewModel(
        ICommentsRepository repository,
        IServiceProvider serviceProvider)
    {
        _repository = repository;
        _serviceProvider = serviceProvider;
    }

    public async Task UpdateComments(Guid? lastSeenComment, int numberOfCommnets)
    {
        var result = await _repository.GetComments(_postViewModel!.Post!.Id, lastSeenComment, numberOfCommnets);
        if (!result.Success)
        {
            return;
        }

        var commentViewModels = MapCommentViewModels(result.Value!.Map());

        Comments?.InsertRange(0, commentViewModels.Reverse());
    }
    
    public ObservableList<CommentViewModel>? Comments
    {
        get => _comments;
        set => TrySetValue(ref _comments, value);
    }

    public PostViewModel? PostViewModel
    {
        get => _postViewModel;
        set => TrySetValue(ref _postViewModel, value);
    }

    private IEnumerable<CommentViewModel> MapCommentViewModels(IEnumerable<CommentModel> comments)
    {
        if(comments is null)
        {
            return Array.Empty<CommentViewModel>();
        }

        return comments
            .Select(comment =>
            {
                var commentViewModel = _serviceProvider.GetRequiredService<CommentViewModel>();
                commentViewModel.Comment = comment;

                return commentViewModel;
            });
    }
}
