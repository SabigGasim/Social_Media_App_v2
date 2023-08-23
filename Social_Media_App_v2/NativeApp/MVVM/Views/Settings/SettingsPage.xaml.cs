using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views.Settings;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsViewModel _viewModel;

    public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;

		BindingContext = _viewModel;
    }
}