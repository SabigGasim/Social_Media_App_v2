using NativeApp.MVVM.ViewModels.Settings.PrivacyAndSecurity;

namespace NativeApp.MVVM.Views.Settings.PrivacyAndSecurity;

public partial class AudiencePage : ContentPage
{
	public AudiencePage(AudienceViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}