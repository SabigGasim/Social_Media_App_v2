using Infrastructure.Interfaces;
using NativeApp.Helpers;
using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels;
public class CommentRepliesViewModel : ViewModelBase
{
    private readonly IRepliesRepository _repository;
    private readonly IServiceProvider _serviceProvider;
    private ObservableList<ReplyViewModel>? _replies;
    private CommentViewModel? _comment;

    public CommentRepliesViewModel(
        IRepliesRepository repository,
        IServiceProvider serviceProvider)
    {
        _repository = repository;
        _serviceProvider = serviceProvider;
    }

    public ObservableList<ReplyViewModel>? Replies 
    { 
        get => _replies; 
        set => TrySetValue(ref _replies, value); 
    }

    public CommentViewModel? CommentViewModel
    {
        get => _comment;
        set 
        {
            if(value is not null)
            {
                _replies ??= new(value!.Comment!.RepliesCount);
            }

            TrySetValue(ref _comment, value);
        } 
    }

    public async Task UpdateReplies(Guid? lastSeenReply)
    {
        var result = await _repository.GetReplies(CommentViewModel!.Comment!.Id, lastSeenReply, 10);
        if (!result.Success)
        {
            return;
        }

        var replies = MapRepliesViewModels(result.Value!.Reverse().Map());

        Replies!.InsertRange(0, replies);
    }

    private IEnumerable<ReplyViewModel> MapRepliesViewModels(IEnumerable<ReplyModel> replies)
    {
        if (replies is null)
        {
            return Array.Empty<ReplyViewModel>();
        }

        return replies
            .Select(reply =>
            {
                var replyViewModel = _serviceProvider.GetRequiredService<ReplyViewModel>();
                replyViewModel.Reply = reply;

                return replyViewModel;
            });
    }
}
