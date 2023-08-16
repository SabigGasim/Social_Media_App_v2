using Infrastructure.Interfaces;
using NativeApp.Helpers;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;
public class CommentRepliesViewModel : ViewModelBase
{
    private readonly IRepliesRepository _repository;
    private readonly IServiceProvider _serviceProvider;
    private ICommand? _sendReplyCommand;
    private RangeObservableCollection<ReplyViewModel>? _replies;
    private CommentViewModel? _comment;
    private UserModel? _replyingTo;

    public CommentRepliesViewModel(
        IRepliesRepository repository,
        IServiceProvider serviceProvider)
    {
        _repository = repository;
        _serviceProvider = serviceProvider;

        InitializeCommands();
    }

    public RangeObservableCollection<ReplyViewModel>? Replies 
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
                _replies ??= new();
            }

            TrySetValue(ref _comment, value);
        } 
    }

    public UserModel? ReplyingTo
    {
        get
        {
            if (_replyingTo is null)
            {
                ReplyingTo = CommentViewModel!.Comment!.User;
                return _replyingTo;
            }

            return _replyingTo;
        }
        set => TrySetValue(ref _replyingTo, value);
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

    public ICommand? SendReplyCommand => _sendReplyCommand;

    private void InitializeCommands()
    {
        _sendReplyCommand = new Command(async (param) =>
        {
            var text = (string)param;

            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            await AddReply(text);
        });
    }

    private async Task AddReply(string text)
    {
        var result = await _repository.AddReply(ReplyingTo!.Id, CommentViewModel!.Comment!.Id, text);
        if (!result.Success || result.Value is null)
        {
            return;
        }

        var vm = _serviceProvider.GetRequiredService<ReplyViewModel>();
        vm.Reply = result.Value.Map();

        Replies!.Insert(0, vm);
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
                replyViewModel.CommentRepliesViewModel = this;

                return replyViewModel;
            });
    }
}
