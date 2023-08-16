using NativeApp.Constants;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class CommentViewModel : ViewModelBase
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private CommentModel? _comment = new();
    private ICommand? _replyButtonClickedCommand;
    private ICommand? _likeButtonClickedCommand;

    public CommentViewModel(INavigateCommandFactory navigateCommandFactory)
    {
        _navigateCommandFactory = navigateCommandFactory;
        
        InitializeComands();
    }


    public CommentModel? Comment 
    { 
        get => _comment;
        set => TrySetValue(ref _comment, value);
    }

    public ICommand? ReplyButtonClickedCommand => _replyButtonClickedCommand;

    public ICommand? LikeButtonClickedCommand => _likeButtonClickedCommand;

    private void InitializeComands()
    {
        _replyButtonClickedCommand = _navigateCommandFactory
            .Create(nameof(CommentViewModel), this, Routes.RepliesPage);

        _likeButtonClickedCommand = new Command(() =>
        {
            Comment!.IsLiked = !Comment.IsLiked;
            Comment.Likes += Comment!.IsLiked ? 1 : -1;

            OnPropertyChanged(nameof(Comment));
        });
    }
}
