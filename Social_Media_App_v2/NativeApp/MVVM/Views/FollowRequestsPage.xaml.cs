using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class FollowRequestsPage : ContentPage
{
	public FollowRequestsPage(FollowRequestViewModel viewModel)
	{
		InitializeComponent();

		if(viewModel.Requests is null || viewModel.Requests!.Count <= 0)
		{
			viewModel.UpdateFollowReuqests(null, 20).GetAwaiter().GetResult();
		}

		BindingContext = viewModel;
    }
}