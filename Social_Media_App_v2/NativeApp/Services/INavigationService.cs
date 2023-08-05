using NativeApp.Interfaces;

namespace NativeApp.Services;

public class MauiNavigationService : INavigationService
{
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null) =>
        routeParameters is not null
            ? Shell.Current.GoToAsync(route, routeParameters)
            : Shell.Current.GoToAsync(route);

    public Task PopAsync(string route = "..") => Shell.Current.GoToAsync(route);
}
