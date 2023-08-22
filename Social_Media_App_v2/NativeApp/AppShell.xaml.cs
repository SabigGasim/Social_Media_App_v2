using NativeApp.Constants;
using NativeApp.MVVM.ViewModels;

namespace NativeApp;

public partial class AppShell : Shell
{
    public AppShell(ShellViewModel viewModel)
    {
        InitializeComponent();

        Routes.RegisterRoutes();

        BindingContext = viewModel;
    }

    private void SettingsFlyoutItem_Clicked(object sender, EventArgs e) 
    {
        CurrentItem = SettingsItem;
        FlyoutIsPresented = false;
    }
}
