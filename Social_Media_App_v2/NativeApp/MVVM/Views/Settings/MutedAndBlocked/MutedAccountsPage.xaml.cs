using NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;

namespace NativeApp.MVVM.Views.Settings.MutedAndBlocked;

public partial class MutedAccountsPage : ContentPage
{
	public MutedAccountsPage(MutedAccountsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}