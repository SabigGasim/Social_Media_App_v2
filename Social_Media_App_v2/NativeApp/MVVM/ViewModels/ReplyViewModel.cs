using NativeApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public ICommand? ReplyButtonClickedCommand => _replyButtonClickedCommand;

    public ICommand? LikeButtonClickedCommand => _likeButtonClickedCommand;

    private void InitializeCommands()
    {
        _likeButtonClickedCommand = new Command(() =>
        {
            Reply!.IsLiked = !Reply.IsLiked;
            OnPropertyChanged(nameof(Reply));
        });
    }
}
