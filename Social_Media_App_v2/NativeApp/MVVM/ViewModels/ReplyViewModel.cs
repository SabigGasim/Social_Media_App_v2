using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;
public class ReplyViewModel : ViewModelBase
{
    private ReplyModel? _reply;
    private ICommand? _replyButtonClickedCommand;
    private ICommand? _likeButtonClickedCommand;

    public ReplyViewModel()
    {
        InitializeCommands();
    }

    public ReplyModel? Reply
    {
        get => _reply;
        set => TrySetValue(ref _reply, value);
    }

    public CommentRepliesViewModel? CommentRepliesViewModel { get; set; }

    public ICommand? ReplyButtonClickedCommand => _replyButtonClickedCommand;

    public ICommand? LikeButtonClickedCommand => _likeButtonClickedCommand;

    private void InitializeCommands()
    {
        _likeButtonClickedCommand = new Command(() =>
        {
            Reply!.IsLiked = !Reply.IsLiked;
            Reply!.Likes += Reply!.IsLiked ? 1 : -1;

            OnPropertyChanged(nameof(Reply));
        });

        _replyButtonClickedCommand = new Command((param) =>
        {
            CommentRepliesViewModel!.ReplyingTo = (UserModel)param;
        });
    }
}
