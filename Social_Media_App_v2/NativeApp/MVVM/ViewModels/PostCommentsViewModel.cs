using Domain.Common;
using Domain.Entities;
using Infrastructure.Interfaces;
using NativeApp.Helpers;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public partial class PostCommentsViewModel : ViewModelBase
{
    private RangeObservableCollection<CommentViewModel>? _comments = new();
    private PostViewModel? _postViewModel;
    private ICommand? _sendCommentCommand;
    private readonly ICommentsRepository _repository;
    private readonly IServiceProvider _serviceProvider;

    public PostCommentsViewModel(
        ICommentsRepository repository,
        IServiceProvider serviceProvider)
    {
        _repository = repository;
        _serviceProvider = serviceProvider;

        InitializeCommands();
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
    
    public RangeObservableCollection<CommentViewModel>? Comments
    {
        get => _comments;
        set => TrySetValue(ref _comments, value);
    }

    public PostViewModel? PostViewModel
    {
        get => _postViewModel;
        set => TrySetValue(ref _postViewModel, value);
    }

    public ICommand? SendCommentCommand => _sendCommentCommand;

    private void InitializeCommands()
    {
        _sendCommentCommand = new Command(async (param) =>
        {
            var text = (string)param;
            
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            await AddComment(text);
        });
    }

    private async Task AddComment(string text)
    {
        var commentDto = await _repository.AddComment(PostViewModel!.Post!.Id, text);
        if(!commentDto.Success || commentDto.Value is null)
        {
            return;
        }

        var vm = _serviceProvider.GetRequiredService<CommentViewModel>();
        vm.Comment = commentDto.Value.Map();

        Comments!.Insert(0, vm);
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
