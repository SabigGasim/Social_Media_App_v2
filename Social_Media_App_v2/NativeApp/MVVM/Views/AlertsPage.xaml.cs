using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class AlertsPage : ContentPage
{
    public AlertsPage(AlertsViewModel viewModel)
	{
		InitializeComponent();

        viewModel.UpdateAlerts(null, 5).GetAwaiter().GetResult();

        BindingContext = viewModel;
    }
}