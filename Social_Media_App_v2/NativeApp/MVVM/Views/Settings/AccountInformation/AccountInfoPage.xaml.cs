using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class AccountInfoPage : ContentPage
{
	public AccountInfoPage(IServiceProvider serviceProvider)
	{
		InitializeComponent();

		var viewModel = serviceProvider.GetRequiredService<SettingsViewModel>();

		BindingContext = viewModel.AccountInfoViewModel;
    }
}