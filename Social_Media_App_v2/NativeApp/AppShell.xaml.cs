using NativeApp.Constants;
using NativeApp.MVVM.Views;

namespace NativeApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(Routes.CommentsPage, typeof(CommentsPage));
        Routing.RegisterRoute(Routes.RepliesPage, typeof(RepliesPage));
    }
}
