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

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.Any())
        {
            return;
        }

        _viewModel.PostViewModel = query[nameof(PostViewModel)] as PostViewModel;
        
        await _viewModel.UpdateComments(null, 20);

        BindingContext = _viewModel;

        query.Clear();
    }
}