using NativeApp.MVVM.ViewModels.Settings.Notifications;

namespace NativeApp.MVVM.Views.Settings.Notifications;

public partial class NotificationMethodsPage : ContentPage
{
	public NotificationMethodsPage(NotificationMethodsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}