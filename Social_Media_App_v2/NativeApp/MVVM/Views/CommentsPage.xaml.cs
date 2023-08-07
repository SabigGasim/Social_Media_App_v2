using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class CommentsPage : ContentPage, IQueryAttributable
{
    private readonly PostCommentsViewModel _viewModel;

    public CommentsPage(PostCommentsViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _viewModel.PostViewModel = query[nameof(PostViewModel)] as PostViewModel;
        _viewModel.UpdateComments(null, 20).GetAwaiter().GetResult();

        BindingContext = _viewModel;
    }
}