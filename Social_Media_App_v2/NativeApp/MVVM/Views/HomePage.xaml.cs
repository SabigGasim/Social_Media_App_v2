using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class HomePage : ContentPage
{
	public HomePage(
		TimelineViewModel viewModel)
	{
		InitializeComponent();

		viewModel.UpdateTimeline(null).GetAwaiter().GetResult();

        BindingContext = viewModel;
	}
}