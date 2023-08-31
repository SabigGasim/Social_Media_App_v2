using NativeApp.MVVM.ViewModels.Settings.AccountInfo;

namespace NativeApp.MVVM.Views.Settings.AccountInformation;

public partial class ChangeUsernamePage : ContentPage
{public ChangeUsernamePage(ChangeUsernameViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}