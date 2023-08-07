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

    public ICommand? ReplyButtonClickedCommand
    {
        get => _replyButtonClickedCommand;
        set => TrySetValue(ref _replyButtonClickedCommand, value);
    }
    
    
    private void InitializeComands()
    {
        _replyButtonClickedCommand = _navigateCommandFactory
            .Create(nameof(CommentViewModel), this, Routes.RepliesPage);
    }
}
