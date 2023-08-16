using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class RepliesPage : ContentPage, IQueryAttributable
{
    private readonly CommentRepliesViewModel _viewModel;

    public RepliesPage(CommentRepliesViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.Any())
        {
            return;
        }

        _viewModel.CommentViewModel = query[nameof(CommentViewModel)] as CommentViewModel;
        
        await _viewModel.UpdateReplies(null);
        
        BindingContext = _viewModel;

        query.Clear();
    }

    private void ReplyEntry_Completed(object sender, EventArgs e)
    {
        _viewModel.SendReplyCommand!.Execute(((Editor)sender).Text);
    }
}