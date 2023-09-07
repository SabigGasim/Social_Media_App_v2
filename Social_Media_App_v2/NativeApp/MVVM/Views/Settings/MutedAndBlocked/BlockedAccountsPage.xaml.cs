using NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;

namespace NativeApp.MVVM.Views.Settings.MutedAndBlocked;

public partial class BlockedAccountsPage : ContentPage
{
	public BlockedAccountsPage(BlockedAccountsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

    }
}