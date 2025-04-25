using NativeApp.MVVM.ViewModels.Settings.Notifications;

namespace NativeApp.MVVM.Views.Settings.Notifications;

public partial class NotificationFiltersPage : ContentPage
{
    public NotificationFiltersPage(NotificationFiltersViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}