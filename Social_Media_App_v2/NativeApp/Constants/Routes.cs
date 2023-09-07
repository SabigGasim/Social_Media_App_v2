using System.Reflection;

namespace NativeApp.Constants;

internal class Routes
{
    internal static readonly string CommentsPage = typeof(MVVM.Views.CommentsPage).FullName!;
    internal static readonly string RepliesPage = typeof(MVVM.Views.RepliesPage).FullName!;
    internal static readonly string MediaViewer = typeof(MVVM.Views.PostMediaViewer).FullName!;
    internal static readonly string ProfilePage = typeof(MVVM.Views.ProfilePage).FullName!;
    internal static readonly string SettingsPage = typeof(MVVM.Views.Settings.SettingsPage).FullName!;
    internal static readonly string AccountInfoPage = typeof(MVVM.Views.Settings.AccountInformation.AccountInfoPage).FullName!;
    internal static readonly string ChangeUsernamePage = typeof(MVVM.Views.Settings.AccountInformation.ChangeUsernamePage).FullName!;
    internal static readonly string ChangeNicknamePage = typeof(MVVM.Views.Settings.AccountInformation.ChangeNicknamePage).FullName!;
    internal static readonly string ChangeDateOfBirthPage = typeof(MVVM.Views.Settings.AccountInformation.ChangeDateOfBirthPage).FullName!;
    internal static readonly string NotificationFiltersPage = typeof(MVVM.Views.Settings.Notifications.NotificationFiltersPage).FullName!;
    internal static readonly string NotificationMethodsPage = typeof(MVVM.Views.Settings.Notifications.NotificationMethodsPage).FullName!;
    internal static readonly string MutedAccountsPage = typeof(MVVM.Views.Settings.MutedAndBlocked.MutedAccountsPage).FullName!;
    internal static readonly string BlockedAccountsPage = typeof(MVVM.Views.Settings.MutedAndBlocked.BlockedAccountsPage).FullName!;

    internal static void RegisterRoutes()
    {
        var fields = typeof(Routes).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy);

        foreach (var field in fields)
        {
            if (!field.IsInitOnly)
            {
                continue;
            }

            var pageFullName = (string)field.GetValue(null)!;
            if(!pageFullName.StartsWith("NativeApp.MVVM.Views.", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var type = Type.GetType(pageFullName);
            Routing.RegisterRoute(pageFullName, type);
        }
    }
}
