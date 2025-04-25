using NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;

namespace NativeApp.MVVM.Views.Settings.MutedAndBlocked;

public partial class MutedWordsPage : ContentPage
{
	public MutedWordsPage(MutedWordsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}