using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class AddPostPage : ContentPage
{
	public AddPostPage(AddPostViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}