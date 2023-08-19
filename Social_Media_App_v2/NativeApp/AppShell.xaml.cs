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
}
