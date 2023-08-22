using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}