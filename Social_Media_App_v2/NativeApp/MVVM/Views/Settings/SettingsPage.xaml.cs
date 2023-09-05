using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views.Settings;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}