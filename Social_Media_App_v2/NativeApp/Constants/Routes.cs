using System.Reflection;

namespace NativeApp.Constants;

internal class Routes
{
    internal readonly string CommentsPage = typeof(MVVM.Views.CommentsPage).FullName!;
    internal readonly string RepliesPage = typeof(MVVM.Views.RepliesPage).FullName!;
    internal readonly string MediaViewer = typeof(MVVM.Views.PostMediaViewer).FullName!;
    internal readonly string ProfilePage = typeof(MVVM.Views.ProfilePage).FullName!;

    internal static void RegisterRoutes()
    {
        var fields = typeof(Routes).GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy);

        foreach (var field in fields)
        {
            if (field.IsLiteral)
            {
                var pageName = field.Name;
                var pageFullName = (string)field.GetRawConstantValue()!;
                var type = Type.GetType(pageFullName);

                Routing.RegisterRoute(pageName, type);
            }
        }
    }
}
