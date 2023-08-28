using NativeApp.MVVM.ViewModels.Settings.AccountInfo;

namespace NativeApp.MVVM.Views.Settings.AccountInformation;

public partial class AccountInfoPage : ContentPage
{
    public AccountInfoPage(AccountInfoViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}