using CommunityToolkit.Maui.Views;
using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class AccountsPopup : Popup
{
	public AccountsPopup(ShellViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await this.CloseAsync((UserViewModel)((BindableObject)sender).BindingContext);
    }
}