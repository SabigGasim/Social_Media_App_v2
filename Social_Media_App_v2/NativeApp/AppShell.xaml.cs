using NativeApp.Constants;
using NativeApp.MVVM.Views;

namespace NativeApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routes.RegisterRoutes();
    }
}
