using NativeApp.MVVM.ViewModels.Settings.AccountInfo;

namespace NativeApp.MVVM.Views.Settings.AccountInformation;

public partial class ChangeDateOfBirthPage : ContentPage
{
	public ChangeDateOfBirthPage(ChangeDateOfBirthViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}