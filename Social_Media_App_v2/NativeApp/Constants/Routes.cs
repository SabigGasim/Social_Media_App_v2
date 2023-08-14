using System.Reflection;

namespace NativeApp.Constants;

internal class Routes
{
    internal const string CommentsPage = nameof(MVVM.Views.CommentsPage);
    internal const string RepliesPage = nameof(MVVM.Views.RepliesPage);
    internal const string MediaViewer = nameof(MVVM.Views.PostMediaViewer);
    internal const string ProfilePage = nameof(MVVM.Views.ProfilePage);

    internal static void RegisterRoutes()
    {
        var fields = typeof(Routes).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy);

        foreach (var field in fields)
        {
            if (field.IsLiteral && !field.IsInitOnly)
            {
                var pageName = (string)field.GetRawConstantValue()!;
                var type = Type.GetType($"NativeApp.MVVM.Views.{pageName}");
                Routing.RegisterRoute(pageName, type);
            }
        }
    }
}
