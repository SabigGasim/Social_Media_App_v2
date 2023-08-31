using NativeApp.MVVM.ViewModels.Settings.AccountInfo;

namespace NativeApp.MVVM.Views.Settings.AccountInformation;

public partial class ChangeNicknamePage : ContentPage
{
    public ChangeNicknamePage(ChangeNicknameViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}